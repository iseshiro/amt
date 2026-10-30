using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using Asio_io;

namespace amt
{
    public partial class ImpMeasForm : Form
    {
        private Amt parent = null;
        private asio_io myasio = null;
        private int dleng;
        private int tsp_fftpoint;
        private int dcnv_fftpnt;
        private string selected_folder;
        private int work_ch_for_tsp = 1;
        private int work_ch_for_deconv = 2;
        private int sync_ave_cnt = 1;
        private int ir_length = 4096;
        private bool start = false;
        private List<int> trig_chs = new List<int>();
        private const double trig_amp = 0.5;   // DAmp TRIG_OUT is high while upper 4 bits of ch63 are non-zero (>= 0.125)

        public ImpMeasForm(Amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            myasio = parent.asio;

            int ii = parent.in_btn_cnt;
            input_end_tb.Text = ii.ToString();
            ii = parent.out_btn_cnt;
            output_end_tb.Text = ii.ToString();
            // ch63 of each MADI (63, 127, ...) drives DAmp TRIG_OUT
            string trig_ch_str = "";
            for (int ch = 63; ch <= ii; ch += 64)
            {
                if (trig_ch_str != "") trig_ch_str += ",";
                trig_ch_str += ch.ToString();
            }
            if (trig_ch_str != "") trig_ch_tb.Text = trig_ch_str;
            selected_folder = "";
            work_ch_for_deconv_lbl.Text = "Work ch. for Deconv. = " + work_ch_for_deconv.ToString();
            work_ch_for_tsp_lbl.Text = "Work ch. for TSP = " + work_ch_for_tsp.ToString();

            int freq_ed = (parent.sample_freq / 2);
            dcnv_freq_ed_tb.Text = freq_ed.ToString();
            imp_meas_status_lbl.Text ="";

            dleng = myasio.GetDataLength();
            dcnv_fftpnt = parent.Max_fftpoint(dleng);
            dcnv_fftpoint_tb.Text = dcnv_fftpnt.ToString();
            tsp_fftpoint = dcnv_fftpnt / 4;
            tsp_fftpnt_tb.Text = tsp_fftpoint.ToString();

            tsp_wave_box.Enabled = false;
            imp_wave_box.Enabled = false;
            this.Size = new Size(this.Size.Width, 216);
        }

        private void set_imp_folder_btn_Click(object sender, EventArgs e)
        {
            if (parent.Set_work_folder())
            {
                selected_folder = parent.work_folder;
                if (!Directory.Exists(selected_folder + @"\imp\") && !Directory.Exists(selected_folder + @"\tsp\"))
                {
                    imp_meas_status_lbl.Text = "Work folder : " + selected_folder;
                    ok_btn.Enabled = true;
                }
            }
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            ok_btn.Enabled = false;
            parent.myProperty.Save();

            PrepareMeasurement();
            imp_meas_status_lbl.Text = "Measurement: Start";
            statusStrip1.Update();
            if (simulout_cb.Checked) StartMeasurement_simultaneous();
            else StartMeasurement_sequencial();
            imp_meas_status_lbl.Text = "Measurement Finish";
            statusStrip1.Update();
        }

        private int out_ch_start;
        private int out_ch_end;
        private int in_ch_start;
        private int in_ch_end;
        private float[][] imp_data;
        private float[][] tsp_data;
        private float[] imp_data1;
        private float[] tsp_data1;

        private void PrepareMeasurement()
        {
            out_ch_start = System.Convert.ToInt32(output_start_tb.Text);
            out_ch_end = System.Convert.ToInt32(output_end_tb.Text);
            for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
            {
                parent.DataCreate(parent.ch_info_out[out_ch-1], ChannelInfo.StreamType.asioout);
            }
            PrepareTrigger();
            in_ch_start = System.Convert.ToInt32(input_start_tb.Text);
            in_ch_end = System.Convert.ToInt32(input_end_tb.Text);
            for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
            {
                parent.DataCreate(parent.ch_info_in[in_ch-1], ChannelInfo.StreamType.asioin);
            }

            parent.DataCreate(parent.ch_info_work[work_ch_for_tsp - 1], ChannelInfo.StreamType.no_io);
            double amp = System.Convert.ToDouble(amp_tb.Text);
            int coef_m_numer = System.Convert.ToInt32(tsp_phase_num_tb.Text);
            int coef_m_denom = System.Convert.ToInt32(tsp_phase_denom_tb.Text);
            double coef_shift = System.Convert.ToDouble(tsp_shift_tb.Text);
            int flag = (hilo_cb.Checked ? 1 : -1);
            tsp_fftpoint = System.Convert.ToInt32(tsp_fftpnt_tb.Text);
            ChannelInfo cinfo = parent.ch_info_work[work_ch_for_tsp - 1];
            myasio.makeTSP(tsp_fftpoint, (int)cinfo.type, cinfo.btn_no, amp, coef_m_numer, coef_m_denom, coef_shift, flag);

            parent.DataCreate(parent.ch_info_work[work_ch_for_deconv - 1], ChannelInfo.StreamType.no_io);
            if (save_tsp_cb.Checked)
            {
                Directory.CreateDirectory(selected_folder + @"\tsp\");
            }
            Directory.CreateDirectory(selected_folder + @"\imp\");

            sync_ave_cnt = System.Convert.ToInt32(sync_ave_tb.Text);
            ir_length = System.Convert.ToInt32(ir_length_tb.Text);

            imp_data = new float[in_ch_end - in_ch_start + 1][];
            for (int ii = 0; ii < in_ch_end - in_ch_start + 1; ii++)
            {
                imp_data[ii] = new float[ir_length];
            }
            imp_data1 = new float[ir_length];

            if (save_tsp_cb.Checked)
            {
                tsp_data = new float[in_ch_end - in_ch_start + 1][];
                for (int ii = 0; ii < in_ch_end - in_ch_start + 1; ii++)
                {
                    tsp_data[ii] = new float[dleng];
                }
            }
            clear_imp_data();
            if (save_tsp_cb.Checked || draw_wave_cb.Checked)
            {
                tsp_data1 = new float[dleng];
            }
         }

        private void PrepareTrigger()
        {
            trig_chs.Clear();
            if (!trig_cb.Checked) return;
            int width = System.Convert.ToInt32(trig_width_tb.Text);
            foreach (string str in trig_ch_tb.Text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int ch = System.Convert.ToInt32(str);
                if (ch < 1 || ch > parent.out_btn_cnt || trig_chs.Contains(ch)) continue;
                trig_chs.Add(ch);
                ChannelInfo cinfo = parent.ch_info_out[ch - 1];
                parent.DataCreate(cinfo, ChannelInfo.StreamType.asioout);
                myasio.makePulse(trig_amp, 0, width, (int)cinfo.type, cinfo.btn_no);
            }
        }

        private void ClearTrigger()
        {
            foreach (int ch in trig_chs)
            {
                ChannelInfo cinfo = parent.ch_info_out[ch - 1];
                myasio.data_clear((int)cinfo.type, cinfo.btn_no);
            }
        }

        private void clear_imp_data()
        {
            for (int ii = 0; ii < in_ch_end - in_ch_start + 1; ii++)
            {
                for (int jj = 0; jj < ir_length; jj++)
                {
                    imp_data[ii][jj] = 0.0f;
                }
                if (save_tsp_cb.Checked)
                {
                    for (int jj = 0; jj < dleng; jj++)
                    {
                        tsp_data[ii][jj] = 0.0f;
                    }
                }
            }
        }

        private void StartMeasurement_simultaneous()
        {
            double dcnv_scale = System.Convert.ToDouble(dcnv_scale_tb.Text);
            double dcnv_freq_st = System.Convert.ToDouble(dcnv_freq_st_tb.Text);
            double dcnv_freq_ed = System.Convert.ToDouble(dcnv_freq_ed_tb.Text);

            bool succeed = false;
            ChannelInfo asio_ch_tsp = parent.ch_info_work[work_ch_for_tsp - 1];
            ChannelInfo asio_ch_dcnv = parent.ch_info_work[work_ch_for_deconv - 1];
            start = true;
            string outch = "";

            for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
            {
                if (trig_chs.Contains(out_ch)) continue;
                // Buffer create, TSP create
                ChannelInfo asio_ch_out = parent.ch_info_out[out_ch - 1];
                myasio.CopyData((int)asio_ch_tsp.type, asio_ch_tsp.btn_no, (int)asio_ch_out.type, asio_ch_out.btn_no);
                outch += out_ch.ToString();
            }
            clear_imp_data();
            int ref_ch = out_ch_start;
            while (trig_chs.Contains(ref_ch)) ref_ch++;
            ChannelInfo ach_out = parent.ch_info_out[ref_ch - 1];

            for (int sync_cnt = 0; sync_cnt < sync_ave_cnt; sync_cnt++)
            {
                int ss = sync_cnt + 1;
                imp_meas_status_lbl.Text = "Measurement: OUT=" + outch + " : SYNC=" + ss.ToString();   /// TODO: REFRESH
                // PlayStart
                tsp_wave_box.Image = null;
                parent.PlayStart();
                for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
                {
                    tsp_wave_box.Refresh();
                    ChannelInfo asio_ch_in = parent.ch_info_in[in_ch - 1];
                    if (save_tsp_cb.Checked || draw_wave_cb.Checked)
                    {
                        unsafe
                        {
                            fixed (float* dd = tsp_data1)
                            {
                                myasio.get_data((int)asio_ch_in.type, asio_ch_in.btn_no, dd, dleng, 0);
                            }
                        }
                        if (save_tsp_cb.Checked)
                        {
                            for (int jj = 0; jj < dleng; jj++)
                            {
                                tsp_data[in_ch - in_ch_start][jj] += tsp_data1[jj];
                            }
                        }
                    }
                    // Deconvolution
                    double new_scale = parent.asio.Deconvolution((int)ach_out.type, ach_out.btn_no, (int)asio_ch_in.type, asio_ch_in.btn_no, (int)asio_ch_dcnv.type, asio_ch_dcnv.btn_no, dcnv_fftpnt, dcnv_scale, dcnv_freq_st, dcnv_freq_ed);
                    unsafe
                    {
                        fixed (float* dd = imp_data1)
                        {
                            myasio.get_data((int)asio_ch_dcnv.type, asio_ch_dcnv.btn_no, dd, ir_length, 0);
                        }
                    }
                    imp_wave_box.Refresh();
                    for (int jj = 0; jj < ir_length; jj++)
                    {
                        imp_data[in_ch - in_ch_start][jj] += imp_data1[jj];
                    }
                }
            }
            for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
            {
                if (save_tsp_cb.Checked)
                {
                    // Save TSP Wav Data
                    String full_path_tsp = selected_folder + "\\tsp\\tsp_s" + outch + "_m" + in_ch.ToString() + ".wav";
                    succeed = parent.WavFloatExport(full_path_tsp, tsp_data[in_ch - in_ch_start], dleng);
                }
                // Save IMP Wav Data
                String full_path_imp = selected_folder + "\\imp\\imp_s" + outch + "_m" + in_ch.ToString() + ".wav";
                succeed = parent.WavFloatExport(full_path_imp, imp_data[in_ch - in_ch_start], ir_length);
                if (!succeed) break;
            }
            // Buffer Clear
            for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
            {
                // Buffer create, TSP create
                ChannelInfo asio_ch_out = parent.ch_info_out[out_ch - 1];
                myasio.data_clear((int)asio_ch_out.type, asio_ch_out.btn_no);
            }
            ClearTrigger();
            start = false;
        }

        private void StartMeasurement_sequencial()
        {
            double dcnv_scale = System.Convert.ToDouble(dcnv_scale_tb.Text);
            double dcnv_freq_st = System.Convert.ToDouble(dcnv_freq_st_tb.Text);
            double dcnv_freq_ed = System.Convert.ToDouble(dcnv_freq_ed_tb.Text);

            bool succeed = false;
            ChannelInfo asio_ch_tsp = parent.ch_info_work[work_ch_for_tsp - 1];
            ChannelInfo asio_ch_dcnv = parent.ch_info_work[work_ch_for_deconv - 1];
            start = true;

            for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
            {
                if (trig_chs.Contains(out_ch)) continue;
                // Buffer create, TSP create
                ChannelInfo asio_ch_out = parent.ch_info_out[out_ch - 1];
                myasio.CopyData((int)asio_ch_tsp.type, asio_ch_tsp.btn_no, (int)asio_ch_out.type, asio_ch_out.btn_no);
                clear_imp_data();

                for (int sync_cnt = 0; sync_cnt < sync_ave_cnt; sync_cnt++)
                {
                    int ss = sync_cnt + 1;
                    imp_meas_status_lbl.Text = "Measurement: OUT=" + out_ch.ToString() + " : SYNC=" + ss.ToString();   /// TODO: REFRESH
                    // PlayStart
                    tsp_wave_box.Image = null;
                    parent.PlayStart();
                    for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
                    {
                        tsp_wave_box.Refresh();
                        ChannelInfo asio_ch_in = parent.ch_info_in[in_ch - 1];
                        if (save_tsp_cb.Checked || draw_wave_cb.Checked)
                        {
                            unsafe
                            {
                                fixed (float* dd = tsp_data1)
                                {
                                    myasio.get_data((int)asio_ch_in.type, asio_ch_in.btn_no, dd, dleng, 0);
                                }
                            }
                            if (save_tsp_cb.Checked)
                            {
                                for (int jj = 0; jj < dleng; jj++)
                                {
                                    tsp_data[in_ch - in_ch_start][jj] += tsp_data1[jj];
                                }
                            }
                        }
                        // Deconvolution
                        double new_scale = parent.asio.Deconvolution((int)asio_ch_out.type, asio_ch_out.btn_no, (int)asio_ch_in.type, asio_ch_in.btn_no, (int)asio_ch_dcnv.type, asio_ch_dcnv.btn_no, dcnv_fftpnt, dcnv_scale, dcnv_freq_st, dcnv_freq_ed);
                        unsafe
                        {
                            fixed (float* dd = imp_data1)
                            {
                                myasio.get_data((int)asio_ch_dcnv.type, asio_ch_dcnv.btn_no, dd, ir_length, 0);
                            }
                        }
                        imp_wave_box.Refresh();
                        for (int jj = 0; jj < ir_length; jj++)
                        {
                            imp_data[in_ch - in_ch_start][jj] += imp_data1[jj];
                        }
                    }
                }
                for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
                {
                    if (save_tsp_cb.Checked)
                    {
                        // Save TSP Wav Data
                        String full_path_tsp = selected_folder + "\\tsp\\tsp_s" + out_ch.ToString() + "_m" + in_ch.ToString() + ".wav";
                        succeed = parent.WavFloatExport(full_path_tsp, tsp_data[in_ch - in_ch_start], dleng);
                    }
                    // Save IMP Wav Data
                    String full_path_imp = selected_folder + "\\imp\\imp_s" + out_ch.ToString() + "_m" + in_ch.ToString() + ".wav";
                    succeed = parent.WavFloatExport(full_path_imp, imp_data[in_ch - in_ch_start], ir_length);
                    if (!succeed) break;
                }
                // Buffer Clear
                myasio.data_clear((int)asio_ch_out.type, asio_ch_out.btn_no);

                if (!succeed) break;
            }
            ClearTrigger();
            start = false;
        }

        private void imp_wave_box_Paint(object sender, PaintEventArgs e)
        {
            if (!start || !draw_wave_cb.Checked) return;
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 20;
            int yohaku_right = 10;
            int yohaku_top = 10;
            int yohaku_bottom = 20;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;


            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            Point[] points = { new Point(yohaku_left, yohaku_top + height / 2), new Point(yohaku_left + width, yohaku_top + height / 2) };
            g.DrawLines(pen, points);


            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            pen = new Pen(Color.Blue, 1.0F);
            pen.DashStyle = DashStyle.Solid;

            int start_time_point = 0;
            int end_time_point = ir_length;
            double h_scale = (double)(height / 2) * 32;
//            double h_scale = (double)(height / 2) * magnify;
            double w_scale = (double)width / (double)(end_time_point - start_time_point);
            if (w_scale > 0.1)
            {
                x1 = yohaku_left;
                y1 = yohaku_top + height / 2 - (int)(imp_data1[start_time_point] * h_scale); ;

                for (int i = start_time_point + 1; i < end_time_point; i++)
                {
                    x2 = (int)(w_scale * (double)(i - start_time_point)) + yohaku_left;
                    y2 = yohaku_top + height / 2 - (int)(imp_data1[i] * h_scale);

                    points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                    g.DrawLines(pen, points);
                    x1 = x2;
                    y1 = y2;
                }
            }
            else
            {
                int st_pnt, ed_pnt;
                double w_scale_1 = (double)(end_time_point - start_time_point) / (double)width, min, max;
                for (int i = 0; i < width; i++)
                {
                    x1 = yohaku_left + i;
                    st_pnt = (int)((double)i * w_scale_1) + start_time_point;
                    ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_time_point;
                    min = max = imp_data1[st_pnt];
                    for (int j = st_pnt + 1; j < ed_pnt; j++)
                    {
                        double dd = imp_data1[j];
                        if (dd < min) min = dd;
                        if (dd > max) max = dd;
                    }
                    y1 = yohaku_top + height / 2 - (int)(min * h_scale);
                    y2 = yohaku_top + height / 2 - (int)(max * h_scale);
                    points = new Point[] { new Point(x1, y1), new Point(x1, y2) };
                    g.DrawLines(pen, points);
                }
            }
        }

        private void tsp_wave_box_Paint(object sender, PaintEventArgs e)
        {
            if (!start || !draw_wave_cb.Checked) return;
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 20;
            int yohaku_right = 10;
            int yohaku_top = 10;
            int yohaku_bottom = 20;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;


            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            Point[] points = { new Point(yohaku_left, yohaku_top + height / 2), new Point(yohaku_left + width, yohaku_top + height / 2) };
            g.DrawLines(pen, points);


            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            pen = new Pen(Color.Blue, 1.0F);
            pen.DashStyle = DashStyle.Solid;

            double magnify = 1.0;
            int start_time_point = 0;
            int end_time_point = dleng;
            double h_scale = (double)(height / 2) * magnify;
            double w_scale = (double)width / (double)(end_time_point - start_time_point);
            if (w_scale > 0.1)
            {
                x1 = yohaku_left;
                y1 = yohaku_top + height / 2 - (int)(tsp_data1[start_time_point] * h_scale); ;

                for (int i = start_time_point + 1; i < end_time_point; i++)
                {
                    x2 = (int)(w_scale * (double)(i - start_time_point)) + yohaku_left;
                    y2 = yohaku_top + height / 2 - (int)(tsp_data1[i] * h_scale);

                    points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                    g.DrawLines(pen, points);
                    x1 = x2;
                    y1 = y2;
                }
            }
            else
            {
                int st_pnt, ed_pnt;
                double w_scale_1 = (double)(end_time_point - start_time_point) / (double)width, min, max;
                for (int i = 0; i < width; i++)
                {
                    x1 = yohaku_left + i;
                    st_pnt = (int)((double)i * w_scale_1) + start_time_point;
                    ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_time_point;
                    min = max = tsp_data1[st_pnt];
                    for (int j = st_pnt + 1; j < ed_pnt; j++)
                    {
                        double dd = tsp_data1[j];
                        if (dd < min) min = dd;
                        if (dd > max) max = dd;
                    }
                    y1 = yohaku_top + height / 2 - (int)(min * h_scale);
                    y2 = yohaku_top + height / 2 - (int)(max * h_scale);
                    points = new Point[] { new Point(x1, y1), new Point(x1, y2) };
                    g.DrawLines(pen, points);
                }
            }
        }

        private void draw_wave_cb_CheckedChanged(object sender, EventArgs e)
        {
            if( draw_wave_cb.Checked)
            {
                this.Size = new Size(this.Size.Width, 438);
            }
            else
            {
                this.Size = new Size(this.Size.Width, 216);
            }
        }
    }
}
