using System;
using System.Windows.Forms;
using System.IO;
using Asio_io;
using System.ComponentModel;

namespace amt
{
    public partial class FileConvForm : Form
    {
        private Amt parent = null;
        //private int input_ch_count = 80;
        //private int output_ch_count = 96;
        private int input_ch_count = 3;
        private int output_ch_count = 4;
        private int pref_size;
        private int fft_point;
        private int filt_size;
        private int fifo_size;
        private int fft_level;
        private asio_io myasio = null;
        private float max_val;
        //private float max_val2;
        private int src_id = -1;
        private SourcePath src_path;
        private string calib_filename = null;
        private string calc_status = "";
        private BinaryReader fs1 = null;
        private FileStream[] wav_out_fs = null;
        private BinaryWriter[] wav_out_bw = null;
        private string mid_fname = "";
        private bool calc_proceeding = false;
        private bool check_proceeding = false;
        private System.Diagnostics.Stopwatch stopwatch;
        private bool error_stop = false;
        private string out_dir = "";

        public FileConvForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            inch_cnt_tb.Text = input_ch_count.ToString();
            outch_cnt_tb.Text = output_ch_count.ToString();
            pref_size_cb.SelectedIndex = 0;
            //pref_size_cb.SelectedIndex = 4;
            //max_val2 = 0.1f;
            //toolStripStatusLabel1.Text = "Select Directories";
            src_path = new SourcePath(parent);

            //For Debug
            //source_path_tb.Text = @"C:\temp\amt_test\sig\sig1.wav";
            //filter_path_tb.Text = @"C:\temp\amt_test\inv\inv1_1.wav";
            //output_path_tb.Text = @"C:\temp\amt_test\out\out_1.wav";
            is_inv_cb.Checked = true;
            stopwatch = new System.Diagnostics.Stopwatch();
        }

        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private class WorkerParams
        {
            //public float max_val1;
            public bool is_script;
        }

        private void Calc_start_btn_Click(object sender, EventArgs e)
        {
            if( calc_proceeding )
            {
                if( bw.IsBusy )
                {
                    bw.CancelAsync();
                }
                calc_proceeding = false;
                calc_start_btn.Text = "Calc start";
            }
            else {
                calc_proceeding = true;
                calc_start_btn.Text = "Cancel";
                Calc_start(false);
                stopwatch.Start();
            }
        }

        public void Calc_start(bool is_script)
        {
            WorkerParams wp = new WorkerParams();
            //wp.max_val1 = max_val;
            wp.is_script = is_script;

            bw.RunWorkerAsync(wp);
        }

        private void is_inv_cb_CheckedChanged(object sender, EventArgs e)
        {
            source_calib_btn.Enabled = is_inv_cb.Checked;
        }

        // 時間のかかる処理を行うメソッド
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerParams wp = (WorkerParams)e.Argument;
            BackgroundWorker worker = (BackgroundWorker)sender;

            bool check_ok = false;
            WavStream ws = null;

            if (wp.is_script)
            {
                if (source_path_check_btn_perform() == false) return;
                if (inverse_path_check_btn_perform() == false) return;
                if( parent.Get_outpath_wav001(output_path_tb.Text) == false) return;
                parent.sample_freq = 48000;
            }

            try
            {
                myasio.create_chinfo(input_ch_count, output_ch_count, 0);
                check_ok = myasio.fconv_init(fft_level, input_ch_count, output_ch_count, pref_size, fifo_size, fft_point);
                if (!check_ok)
                {
                    String error_msg = "";
                    unsafe
                    {
                        error_msg = new String(myasio.error_message());
                    }
                    throw new Exception("Error : fconv_init:" + error_msg);
                }
                // Read calib file

                double[] calib_amp = new double[input_ch_count];
                if (calib_filename == null)
                {
                    for (int i = 0; i < input_ch_count; i++) calib_amp[i] = 1.0;
                }
                else
                {
                    BinaryReader fs = new BinaryReader(FileProcess.OpenRead(calib_filename));
                    for (int i = 0; i < input_ch_count; i++) calib_amp[i] = 1.0 / fs.ReadDouble();
                }

                // Check src file

                calc_status = "Check Source Files";
                double progress_rate = 100.0 / (double)input_ch_count;
                int max_src_leng = 0;
                for (int ich = 1; ich <= input_ch_count; ich++)
                {
                    worker.ReportProgress((int)(progress_rate * (double)ich));
                    string filename = src_path.src_filename(src_id, ich);
                    int length = -1;
                    bool succeed = false;
                    unsafe
                    {
                        fixed (Char* ss = filename.ToCharArray())
                        {
                            succeed = myasio.fconv_set_src(ich - 1, ss, fft_point / 2);
                        }
                    }
                    if (!succeed)
                    {
                        String error_msg = "";
                        unsafe
                        {
                            error_msg = new String(myasio.error_message());
                        }
                        throw new Exception("File Name (" + filename + "):" + error_msg);
                    }
                    length = myasio.fconv_file_length(ich - 1);

                    if(max_src_leng < length)
                    {
                        max_src_leng = length;
                    }
                    //if (ich == 1) src_leng = length;
                    //else if (src_leng != length)
                    //{
                    //    String error_msg = "";
                    //    unsafe
                    //    {
                    //        error_msg = new String(myasio.error_message());
                    //    }
                    //    throw new Exception("Different length (" + length.ToString() + "):" + error_msg);
                    //}
                }
                // Read inv filter
                calc_status = "Read filter coefficients";
                float[] dd = new float[filt_size];
                double amp = 1.0;
                int max_filt_length = 0;

                for (int ich = 1; ich <= input_ch_count; ich++)
                {
                    worker.ReportProgress((int)(progress_rate * (double)ich));
                    amp = calib_amp[ich - 1];
                    for (int och = 1; och <= output_ch_count; och++)
                    {
                        string filename = src_path.filt_filename(ich, och, is_inv_cb.Checked);
                        if (is_inv_cb.Checked)
                        {
                            if (!FileProcess.Exists(filename))
                            {
                                throw new Exception("Read filter coefficients\n");
                            }
                            FileInfo ff = new FileInfo(filename);
                            int word_count = (int)ff.Length / 8;
                            if(max_filt_length < word_count)
                            {
                                max_filt_length = word_count;
                            }
                            BinaryReader fs = new BinaryReader(FileProcess.OpenRead(filename));
                            if (filt_size > word_count)
                            {
                                for (int i = 0; i < word_count; i++) dd[i] = (float)(amp * fs.ReadDouble());
                                for (int i = word_count; i < filt_size; i++) dd[i] = 0f;
                            }
                            else
                                for (int i = 0; i < filt_size; i++) dd[i] = (float)(amp * fs.ReadDouble());

                            for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                            {
                                bool is_last = (ich == input_ch_count) && (och == output_ch_count) && (fft_lvl == fft_level);
                                unsafe
                                {
                                    fixed (float* dd1 = dd)
                                    {
                                        parent.asio.rtconv_set(fft_lvl, dd1, ich, och, filt_size, is_last);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ws = new WavStream(parent);
                            if (!ws.WavRead(filename, false)) return;
                            int word_cnt = ws.WordCount();
                            for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                            {
                                bool is_last = (ich == input_ch_count) && (och == output_ch_count) && (fft_lvl == fft_level);
                                ws.Rtconv_set(fft_lvl, ich, och, word_cnt, is_last);
                            }
                        }
                    }
                }

                mid_fname = out_dir + @"\mid_data.dat";
                calc_status = "Convolution";
                unsafe
                {
                    fixed (Char* ss = mid_fname.ToCharArray())
                    {
                        bool ok = myasio.fconv_create_mid(ss);
                        if (!ok)
                        {
                            throw new Exception("Open mid file\n");
                        }
                    }
                }
                int conv_leng = max_src_leng + max_filt_length - 1;
                progress_rate = 100.0 / (double)conv_leng;
                int processedSamples = 0;
                int f_processedSamples = 0;
                int write_size = pref_size;
                int prev_progress_val = 0;
                int curr_progress_val = 0;
                while (f_processedSamples < conv_leng)
                {
                    curr_progress_val = (int)(progress_rate * (double)f_processedSamples);
                    if (curr_progress_val != prev_progress_val)
                    {
                        worker.ReportProgress(curr_progress_val);
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (f_processedSamples + pref_size > conv_leng)
                    {
                        write_size = conv_leng - f_processedSamples;
                    }
                    myasio.fconv_convolution(input_ch_count, output_ch_count, pref_size, write_size, processedSamples);
                    f_processedSamples += write_size;
                    processedSamples += write_size;
                    if (processedSamples == fifo_size)
                    {
                        processedSamples = 0;
                    }
                }
                max_val = myasio.fconv_close_mid();

                calc_status = "Create wav file: Max Val:" + max_val.ToString();
                progress_rate = 100.0 / (double)output_ch_count;
                ws = new WavStream(parent);
                //amp = wp.max_val1 / max_val;
                wav_out_fs = new FileStream[output_ch_count];
                wav_out_bw = new BinaryWriter[output_ch_count];
                for (int och = 0; och < output_ch_count; och++)
                {
                    curr_progress_val = (int)(progress_rate * (double)och);
                    if (curr_progress_val != prev_progress_val)
                    {
                        worker.ReportProgress(curr_progress_val);
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    string filepath = out_dir + @"\" + parent.Out_filename(och + 1);
                    wav_out_fs[och] = ws.WavHeaderWrite(filepath, conv_leng, true);
                    wav_out_bw[och] = new BinaryWriter(wav_out_fs[och]);
                }

                calc_status = "Write wav file";
                progress_rate = 100.0 / (double)conv_leng;
                fs1 = new BinaryReader(FileProcess.OpenRead(mid_fname));
                f_processedSamples = 0;
                write_size = pref_size;
                while (f_processedSamples < conv_leng)
                {
                    curr_progress_val = (int)(progress_rate * (double)f_processedSamples);
                    if (curr_progress_val != prev_progress_val)
                    {
                        worker.ReportProgress(curr_progress_val);
                    }
                    if (f_processedSamples + pref_size > conv_leng)
                    {
                        write_size = conv_leng - f_processedSamples;
                    }

                    for (int och = 0; och < output_ch_count; och++)
                    {
                        for (int i = 0; i < write_size; i++)
                        {
                            float ff = (float)fs1.ReadSingle();
                            wav_out_bw[och].Write(ff);
                        }
                    }
                    f_processedSamples += write_size;
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                for (int och = 0; och < output_ch_count; och++)
                {
                    wav_out_bw[och].Close(); ;
                }
                e.Result = "Complete file convolution.";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                error_stop = true;
            }
        }

        private void source_path_check_btn_Click(object sender, EventArgs e)
        {
            if(source_path_check_btn_perform())
            {
                src_path_ok_lbl.Visible = true;
                calc_start_btn.Enabled = src_path_ok_lbl.Visible && inv_filt_ok_lbl.Visible && out_path_ok_lbl.Visible;
                if (calc_start_btn.Enabled) toolStripStatusLabel1.Text = "Start calculation";
            }
        }
        private bool source_path_check_btn_perform()
        {
            input_ch_count = Int32.Parse(inch_cnt_tb.Text);
            src_id = src_path.set_source_path(input_ch_count, source_path_tb.Text);
            if (src_id < 0) return false;

            for (int ich = 1; ich <= input_ch_count; ich++)
            {
                string filename = src_path.src_filename(src_id, ich);
                WavStream ws = new WavStream(parent);
                if(!ws.WavHeaderCheck(filename))
                {
                    ErrorMessage("wav_header : File Name (" + filename + ")\r\n");
                    return false;
                }
                switch(ws.WavFormat())
                {
                    case WavStream.Wav_format.int8:
                        ErrorMessage("wav_format int8: (" + filename + ")\r\n");
                        return false;
                    case WavStream.Wav_format.int16:
                        ErrorMessage("wav_format int16: (" + filename + ")\r\n");
                        return false;
                    case WavStream.Wav_format.not_wav:
                        ErrorMessage("not wav_format: (" + filename + ")\r\n");
                        return false;
                }
            }
            return true;
        }
        private void inverse_path_check_btn_Click(object sender, EventArgs e)
        {
            if (check_proceeding)
            {
                if (bw_check.IsBusy)
                {
                    bw_check.CancelAsync();
                }
                check_proceeding = false;
                inverse_path_check_btn.Text = "Check";
            }
            else
            {
                check_proceeding = true;
                inverse_path_check_btn.Text = "Cancel";
                bw_check.RunWorkerAsync();
            }
        }

        private bool inverse_path_check_btn_perform()
        {
            int mic_cnt = Int32.Parse(inch_cnt_tb.Text);
            int sp_cnt = Int32.Parse(outch_cnt_tb.Text);
            return src_path.get_filter_path1(filter_path_tb.Text, mic_cnt, sp_cnt, is_inv_cb.Checked);
        }

        private void bg_Check_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            int mic_cnt = Int32.Parse(inch_cnt_tb.Text);
            int sp_cnt = Int32.Parse(outch_cnt_tb.Text);
            int curr_progress_val = 0;
            int prev_progress_val = 0;
            string file_path2 = "";
            double progress_rate = 100.0 / (double)mic_cnt;

            if (src_path.get_filter_path2(filter_path_tb.Text))
            {
                for (int ich = 1; ich <= mic_cnt; ich++)
                {
                    curr_progress_val = (int)(progress_rate * (double)ich);
                    if (curr_progress_val != prev_progress_val)
                    {
                        worker.ReportProgress(curr_progress_val);
                    }
                    for (int och = 1; och <= sp_cnt; och++)
                    {

                        file_path2 = src_path.filt_filename(ich, och, is_inv_cb.Checked);
                        if (!FileProcess.Exists(file_path2))
                        {
                            string file_name1 = Path.GetFileName(file_path2);
                            parent.Cnsl_msg("File not exists : " + file_name1 + "-");
                            if (bw_check.IsBusy)
                            {
                                bw_check.CancelAsync();
                            }
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (ich == 1 && och == 1)
                        {
                            string file_name1 = Path.GetFileName(file_path2);
                            parent.Cnsl_msg("File exists : " + file_name1 + "-");
                        }
                    }
                }
                string file_name2 = Path.GetFileName(file_path2);
                parent.Cnsl_msg(file_name2 + "\n");
            }
        }

        private void Output_path_check_btn_Click(object sender, EventArgs e)
        {
            string file_name = "";
            string dir_name = Path.GetDirectoryName(output_path_tb.Text);
            if (Directory.Exists(dir_name))
            {
                out_dir = dir_name;
                file_name = Path.GetFileName(output_path_tb.Text);
            }
            bool result = parent.Get_outpath_wav001(file_name);
            out_path_ok_lbl.Visible = result;
            if(file_name == "")
            {
                output_path_tb.Text = parent.out_file_path;
            }
            calc_start_btn.Enabled = src_path_ok_lbl.Visible && inv_filt_ok_lbl.Visible && out_path_ok_lbl.Visible;
            if(calc_start_btn.Enabled) toolStripStatusLabel1.Text = "Start calculation";
        }

        private void Mic_count_tb_TextChanged(object sender, EventArgs e)
        {
            if (inch_cnt_tb.Text != "")
            {
                input_ch_count = System.Convert.ToInt32(inch_cnt_tb.Text);
            }
        }

        private void Sp_count_tb_TextChanged(object sender, EventArgs e)
        {
            if (outch_cnt_tb.Text != "")
            {
                output_ch_count = System.Convert.ToInt32(outch_cnt_tb.Text);
            }
        }

        private void pref_size_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            pref_size = System.Convert.ToInt32(pref_size_cb.SelectedItem);
            fft_point = pref_size * 2;
            fft_level = (int)fft_level_ud.Value;
            filt_size = fft_level * (fft_level + 1) * pref_size;
            fifo_size = pref_size;
            while (fifo_size < filt_size * 4) fifo_size <<= 1;
            //if (fifo_size < fft_point * 4) fifo_size = fft_point * 4;
            filt_size_lbl.Text = "Filter size = " + filt_size.ToString() + ", Fifo size = " + fifo_size.ToString();
        }

        private void source_path_set_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Source file", "*.wav"))
            {
                source_path_tb.Text = parent.fd.filename;
            }
        }

        private void inverse_path_set_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Inverse filter file", "*.dat;*.double"))
            {
                filter_path_tb.Text = parent.fd.filename;
            }
        }

        private void fft_level_ud_ValueChanged(object sender, EventArgs e)
        {
            fft_level = (int)fft_level_ud.Value;
            filt_size = fft_level * (fft_level + 1) * pref_size;
            fifo_size = pref_size;
            while (fifo_size < filt_size) fifo_size <<= 1;
            filt_size_lbl.Text = "Filter size = " + filt_size.ToString() + ", Fifo size = " + filt_size.ToString();
        }

        private void source_calib_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Calibration Data", "*.double"))
            {
                calib_filename = parent.fd.filename;
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 進ちょく状況の表示
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = calc_status + ":" + e.ProgressPercentage.ToString() + "%";
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            if (e.Cancelled)
            {
                // 処理結果の表示
                MessageBox.Show("キャンセルされました");
                parent.Cnsl_msg("\nCanceled：" + ts.Hours + "Hours " + ts.Minutes + "Minutes" + ts.Seconds + "Seconds\n");
            }
            else if (error_stop)
            {
                parent.Cnsl_msg("\nError Stopped\n");
            }
            else
            {
                // 処理結果の表示{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒
                MessageBox.Show("正常に完了："+ ts.Hours + "時間"+ ts.Minutes + "分" + ts.Seconds + "秒");
                parent.Cnsl_msg("\nComplete calculation：" + ts.Hours + "Hours " + ts.Minutes + "Minutes" + ts.Seconds + "Seconds\n");
            }
            if (fs1 != null ) fs1.Close();
            for (int ich = 0; ich < input_ch_count; ich++)
            {
                myasio.fconv_close_src(ich);
            }
            if (wav_out_bw != null)
            {
                for (int och = 0; och < output_ch_count; och++)
                {
                    wav_out_bw[och].Close();
                }
            }
            if (wav_out_fs != null)
            {
                for (int och = 0; och < output_ch_count; och++)
                {
                    wav_out_fs[och].Close();
                }
            }
            if (!keep_mid_file_cb.Checked)
            {
                if ( FileProcess.Exists(mid_fname))
                {
                    System.IO.File.Delete(mid_fname);
                }
            }

            Dispose();
        }


        private void bw_check_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 進ちょく状況の表示
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void bw_check_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // 処理結果の表示
                MessageBox.Show("エラーで中断またはキャンセルされました");
                check_proceeding = false;
                inverse_path_check_btn.Text = "Check";
                toolStripStatusLabel1.Text = "Inv check canceled";
            }
            else
            {
                // 処理結果の表示
                inv_filt_ok_lbl.Visible = true;
                calc_start_btn.Enabled = src_path_ok_lbl.Visible && inv_filt_ok_lbl.Visible && out_path_ok_lbl.Visible;
                if (calc_start_btn.Enabled) toolStripStatusLabel1.Text = "Start calculation";
                check_proceeding = false;
                inverse_path_check_btn.Text = "Check";
                toolStripStatusLabel1.Text = "Inv check complete";
            }
        }
    }
}
