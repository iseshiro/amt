using System;
using System.Windows.Forms;
using System.IO;
using Asio_io;

namespace amt
{
    public partial class ConvSet : Form
    {
        private Amt parent = null;
        private int audio_out_ch = 0;
        private int max_in_ch = 0;
        private int max_out_ch = 0;
        public int rtconv_inch = 1;
        public int rtconv_outch = 0;
        private const int rtconv_fft_level_max = 32;
        private int cpu_core;
        private asio_io myasio = null;
        private bool rtconv_single_mode = false;
        private bool imp_path_ok = false;
        private bool init_ok = false;
        private bool src_ok = false;
        private bool file_input = false;
        private SourcePath src_path;
        private int src_id = -1;
        private int max_length = 0;
        private bool is_inv = false;
        private RealtimeConv rtconv = null;

        public ConvSet(Amt Parent, bool is_file_input)
        {
            InitializeComponent();
            if (is_file_input) Height = 347;
            else Height = 243;

            parent = Parent;
            myasio = parent.asio;

            max_in_ch = parent.in_btn_cnt;
            if (parent.out_btn_cnt < 96)
            {
                rtconv_outch = max_out_ch = audio_out_ch = parent.out_btn_cnt;
            }
            else
            {
                rtconv_outch = max_out_ch = audio_out_ch = 96;
            }
            cpu_core = myasio.core_count();
            file_input = is_file_input;

            // IR ListViewコントロールのプロパティを設定
            IR_listView.FullRowSelect = true;
            IR_listView.GridLines = true;
            IR_listView.Sorting = SortOrder.Ascending;
            IR_listView.View = View.Details;
            // 列（コラム）ヘッダの作成
            ColumnHeader IR_columnID = new ColumnHeader();
            ColumnHeader IR_columnIName = new ColumnHeader();
//            ColumnHeader IR_columnEQ = new ColumnHeader();
            ColumnHeader IR_columnFullPath = new ColumnHeader();
            IR_columnID.Text = "ID";
            IR_columnID.Width = 30;
            IR_columnIName.Text = "Name";
            IR_columnIName.Width = 100;
            //IR_columnEQ.Text = "EQ type";
            //IR_columnEQ.Width = 60;
            IR_columnFullPath.Text = "Full Path";
            IR_columnFullPath.Width = 300;
            ColumnHeader[] IR_colHeaderRegValue = { IR_columnID, IR_columnIName, IR_columnFullPath };
            IR_listView.Columns.AddRange(IR_colHeaderRegValue);

            if (file_input)
            {
                // src ListViewコントロールのプロパティを設定
                src_listView.FullRowSelect = true;
                src_listView.GridLines = true;
                src_listView.Sorting = SortOrder.Ascending;
                src_listView.View = View.Details;
                // 列（コラム）ヘッダの作成
                ColumnHeader src_columnID = new ColumnHeader();
                ColumnHeader src_columnName = new ColumnHeader();
                ColumnHeader src_columnFullPath = new ColumnHeader();
                ColumnHeader src_columnStartTime = new ColumnHeader();
                ColumnHeader src_columnStopTime = new ColumnHeader();
                src_columnID.Text = "ID";
                src_columnID.Width = 30;
                src_columnName.Text = "Name";
                src_columnName.Width = 100;
                src_columnFullPath.Text = "Full Path";
                src_columnFullPath.Width = 300;
                src_columnStartTime.Text = "Start";
                src_columnStartTime.Width = 60;
                src_columnStopTime.Text = "Stop";
                src_columnStopTime.Width = 60;
                ColumnHeader[] src_colHeaderRegValue = { src_columnID, src_columnName, src_columnFullPath, src_columnStartTime, src_columnStopTime };
                src_listView.Columns.AddRange(src_colHeaderRegValue);
            }

            src_path = new SourcePath(parent);
            rtconv = new RealtimeConv(parent, rtconv_inch, rtconv_outch, 1);

            fft_level_ud.Value = 1;
            in_ch_tb.Text = rtconv_inch.ToString();
            out_ch_tb.Text = audio_out_ch.ToString();
            update_info();
        }

        private void update_info()
        {
            int p_size = myasio.GetPreferredSize();
            int leng = myasio.GetDataLength();
            CPUCoreToolStripMenuItem.Text = "CPU core = " + cpu_core.ToString();
            DataLengthToolStripMenuItem.Text = "Data Length = " + leng.ToString();
            FFTPointToolStripMenuItem.Text = "FFT Point = " + rtconv.GetFFTPoint();
            ASIOBufSizeToolStripMenuItem.Text = "ASIO Buffer Size = " + p_size.ToString();
            convLengthToolStripMenuItem.Text = "Conv. Length = " + rtconv.GetConvLeng().ToString();
            message_toolStripStatusLabel.Text = "Required Data Length= " + rtconv.GetMinBufSize();
            set_btn_ok();
        }

        private void imp_path_btn_Click(object sender, EventArgs e)
        {
            string filt = "";
            is_inv = inv_filt_cb.Checked;
            if (is_inv)
            {
                filt = "*.dat;*.double";
            }
            else
            {
                filt = "wav";
            }
            bool result = src_path.get_filter_path(rtconv_inch, rtconv_outch, filt, is_inv);

            if (result)
            {
                string filename = "";
                for (int ich = 1; ich <= rtconv_inch; ich++)
                {
                    for (int och = 1; och <= rtconv_outch; och++)
                    {
                        if (rtconv_single_mode && ich != och) break;
                        filename = src_path.filt_filename(ich, och, is_inv);
                        if (!FileProcess.Exists(filename))
                        {
                            string message = "ファイルがありません。 : " + filename;
                            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                FileInfo cFileInfo = new FileInfo(src_path.filt_filename(1, 1, is_inv));

                // ListViewコントロールにデータを追加します。
                int item_count = IR_listView.Items.Count + 1;
                string[] item1 = { item_count.ToString(), cFileInfo.Directory.Name, filename };
                IR_listView.Items.Add(new ListViewItem(item1));
                IR_listView.Items[IR_listView.Items.Count - 1].Selected = true;

                imp_path_ok = true;
                set_btn_ok();
            }
            else
            {
                string message = "ファイル名が不適切です。 : " + parent.fd.filename;
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void in_ch_tb_TextChanged(object sender, EventArgs e)
        {
            if (in_ch_tb.Text != "")
            {
                rtconv_inch = System.Convert.ToInt32(in_ch_tb.Text);
                if (rtconv_inch > max_in_ch)
                {
                    rtconv_inch = max_in_ch;
                    in_ch_tb.Text = max_in_ch.ToString();
                }
                if (rtconv_single_mode)
                {
                    out_ch_tb.Text = in_ch_tb.Text;
                }
                rtconv.SetChannel(rtconv_inch, rtconv_outch);
            }
        }

        private void fft_level_p_btn_Click(object sender, EventArgs e)
        {
            int ii = rtconv.AddFFTLevel(1);
            fft_level_lbl.Text = "FFT Level : " + ii.ToString();
        }

        private void set_btn_ok()
        {
            bool ret;
            if (file_input && (src_id < 0) ) ret = false;
            else ret = imp_path_ok;
            set_btn.Enabled = ret;
            check_ir_btn.Enabled = ret;
            inv_filt_cb.Enabled = !ret;
        }

        private void set_btn_Click(object sender, EventArgs e)
        {
            bool ok_flg = false;

            if (file_input)
            {
                if ((src_id >= 0) && imp_path_ok)
                {
                    init_ok = rtconv.Init_f(src_path, rtconv_single_mode, is_inv, 0, toolStripProgressBar1);
                    int length = src_path.stream_open(src_id, rtconv_inch);
                    src_ok = (length > 0);
                    ok_flg = init_ok && src_ok;
                }
            }
            else
            {
                if (imp_path_ok)
                {
                    ok_flg = rtconv.Init_f(src_path, rtconv_single_mode, is_inv, 0, toolStripProgressBar1);
                }
            }
            if (ok_flg)
            {
                parent.rtconv_zitter_lbl.Text = "Real Time Convolution : READY";
                parent.rtconv_zitter_lbl.Visible = true;
                parent.repeat_mode_cb.Checked = true;
                parent.asio.set_repeat_mode(true);
            }
        }

        private void fft_level_ud_ValueChanged(object sender, EventArgs e)
        {
            int ii = (int)fft_level_ud.Value;
            int jj = rtconv.SetFFTLevel(ii);
            fft_level_ud.Value = jj;
            update_info();
        }

        private void out_ch_tb_TextChanged(object sender, EventArgs e)
        {
            if (out_ch_tb.Text != "")
            {
                rtconv_outch = System.Convert.ToInt32(out_ch_tb.Text);
                if (rtconv_outch > max_out_ch)
                {
                    rtconv_outch = max_out_ch;
                    out_ch_tb.Text = max_out_ch.ToString();
                }
                rtconv.SetChannel(rtconv_inch, rtconv_outch);
            }
        }

        private void single_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (single_rb.Checked)
            {
                rtconv_single_mode = true;
                out_ch_tb.Enabled = false;
                out_ch_tb.Text = in_ch_tb.Text;
            }
            else
            {
                rtconv_single_mode = false;
                out_ch_tb.Enabled = true;
            }
        }

        private void src_path_btn_Click(object sender, EventArgs e)
        {
            src_id = src_path.get_source_path(rtconv_inch, "wav");

            if (src_id >= 0)
            {
                string filename = src_path.src_filename(src_id, 1);
                FileInfo cFileInfo = new FileInfo(filename);

                // ListViewコントロールにデータを追加します。
                int id = src_listView.Items.Count + 1;
                double stop_sec = -1.0f;

                foreach (src_info p in src_path.src_list)
                {
                    if (p.id == id)
                    {
                        p.stop_time = (float)src_path.src_length / (float)src_path.src_sfreq;
                        stop_sec = p.stop_time;
                        break;
                    }
                }
                double start_sec = 0;
                string[] item1 = { id.ToString(), cFileInfo.Directory.Name, filename, start_sec.ToString(), stop_sec.ToString() };
                src_listView.Items.Add(new ListViewItem(item1));
                src_listView.Items[src_listView.Items.Count - 1].Selected = true;

                set_btn_ok();
            }
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void IR_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IR_listView.SelectedIndices.Count == 0) return;
            src_path.filt_id = IR_listView.SelectedIndices[0];
            message_toolStripStatusLabel.Text = IR_listView.Items[src_path.filt_id].SubItems[1].Text;
        }

        private void src_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (src_listView.SelectedIndices.Count == 0) return;
            int src_id = src_listView.SelectedIndices[0];
            selected_src_lbl.Visible = true;
            selected_src_lbl.Text = src_listView.Items[src_id].SubItems[1].Text;
        }
        private void check_ir_btn_Click(object sender, EventArgs e)
        {
            if (IR_listView.SelectedIndices.Count == 0) return;
            src_path.filt_id = IR_listView.SelectedIndices[0];
            double p_scale = 100.0 / (double)(rtconv_inch * rtconv_outch);
            for (int ich = 1; ich <= rtconv_inch; ich++)
            {
                int p_val = (int)(p_scale * (double)ich);
                toolStripProgressBar1.Value = p_val;
                for (int och = 1; och <= rtconv_outch; och++)
                {
                    if (rtconv_single_mode && ich != och) break;
                    string filename = src_path.filt_filename(ich, och, is_inv);
                    int length = 0;
                    unsafe
                    {
                        ChannelInfo cinfo = parent.ch_info_work[0];
                        fixed (Char* ss = filename.ToCharArray())
                        {
                            length = parent.asio.effective_ir_length(ss, (int)cinfo.type, cinfo.btn_no, is_inv);
                        }
                    }
                    if (length > max_length)
                    {
                        max_length = length;
                    }
                }
            }
            int conv_leng = 0;
            int fft_lvl = 0;
            int fftpnt_1 = myasio.GetPreferredSize()*2;
            while (conv_leng < max_length)
            {
                conv_leng += fftpnt_1;
                fftpnt_1 *= 2;
                fft_lvl++;
            }

            message_toolStripStatusLabel.Text = "Recommended FFT level = " + fft_lvl.ToString();
        }

        private void fft_level_lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
