using System;
using System.Drawing;
using System.Windows.Forms;
using Asio_io;

namespace amt
{
    public partial class CheckSystemForm : Form
    {
        private Amt parent = null;
        private asio_io myasio = null;

        private int tsp_fftpoint = 8192;
        private int input_dleng;
        private int output_dleng;
        private int g_leng_backup;
        private double[] signal_p_in;
        private double[] noise_p_in;
        private double[] signal_max_in;
        private double[] signal_p_out;
        private double[] signal_max_out;
        private double max_db = 100.0;
        private int in_ch_start;
        private int in_ch_end;
        private int in_ch_test;
        private int out_ch_start;
        private int out_ch_end;
        private int out_ch_test;
        private bool input_test_complete = false;
        private bool output_test_doing = false;

        public CheckSystemForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            in_ch_start = 1;
            input_start_tb.Text = in_ch_start.ToString();
            in_ch_test = 1;
            input_test_tb.Text = in_ch_test.ToString();
            in_ch_end = parent.in_btn_cnt;
            input_end_tb.Text = in_ch_end.ToString();
            out_ch_start = 1;
            output_start_tb.Text = out_ch_start.ToString();
            out_ch_test = 1;
            output_test_tb.Text = out_ch_test.ToString();
            out_ch_end = parent.out_btn_cnt;
            output_end_tb.Text = out_ch_end.ToString();

            g_leng_backup = myasio.GetDataLength();

            tsp_fftpnt_tb.Text = tsp_fftpoint.ToString();
            input_dleng = tsp_fftpoint * 4;
            input_dleng_lbl.Text = input_dleng.ToString();
            output_dleng = tsp_fftpoint;
            output_dleng_lbl.Text = output_dleng.ToString();

            signal_p_in = new double[parent.in_btn_cnt + 1];
            noise_p_in = new double[parent.in_btn_cnt + 1];
            signal_max_in = new double[parent.in_btn_cnt + 1];
            signal_p_out = new double[parent.out_btn_cnt + 1];
            signal_max_out = new double[parent.out_btn_cnt + 1];

            check_pictureBox.Size = new Size(Size.Width - 300, Size.Height - 50);
        }

        private void input_test_btn_Click(object sender, EventArgs e)
        {
            myasio.SetDataLength(input_dleng, parent.without_asio);

            for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
            {
                parent.DataCreate(parent.ch_info_in[in_ch-1], ChannelInfo.StreamType.asioin);
            }
            // Buffer create, TSP create
            parent.DataCreate(parent.ch_info_out[out_ch_test - 1], ChannelInfo.StreamType.asioout);

            int coef_m_numer = System.Convert.ToInt32(tsp_phase_num_tb.Text);
            int coef_m_denom = System.Convert.ToInt32(tsp_phase_denom_tb.Text);
            double coef_shift = System.Convert.ToDouble(tsp_shift_tb.Text);
            int flag = (hilo_cb.Checked ? 1 : -1);
            double amp = System.Convert.ToDouble(amp_tb.Text);
            ChannelInfo cinfo = parent.ch_info_out[out_ch_test - 1];
            myasio.makeTSP(tsp_fftpoint, (int)cinfo.type, cinfo.btn_no, amp, coef_m_numer, coef_m_denom, coef_shift, flag);
            // PlayStart
            parent.PlayStart();
            double db_val, max_val = 0.0, min_val = 0.0;
            for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
            {
                cinfo = parent.ch_info_out[in_ch - 1];
                myasio.SetOffset((int)cinfo.type, cinfo.btn_no, tsp_fftpoint * 2, tsp_fftpoint * 4);
                myasio.CorrectOffset((int)cinfo.type, cinfo.btn_no);
                db_val = 10.0 * Math.Log10(myasio.GetTimeSigPower((int)cinfo.type, cinfo.btn_no, 0, tsp_fftpoint * 2));
                if (in_ch == in_ch_start || max_val < db_val) max_val = db_val;
                signal_p_in[in_ch] = db_val;

                db_val = 10.0 * Math.Log10(myasio.GetTimeSigPower((int)cinfo.type, cinfo.btn_no, tsp_fftpoint * 2, tsp_fftpoint * 4));
                if (in_ch == in_ch_start || min_val > db_val) min_val = db_val;
                noise_p_in[in_ch] = db_val;

                myasio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                signal_max_in[in_ch] = myasio.GetMaxVal((int)cinfo.type, cinfo.btn_no);
            }
            check_pictureBox.Invalidate();

            // Buffer delete
            parent.DataDispose(parent.ch_info_out[out_ch_test - 1]);

            min_val -= 3;
            for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
            {
                signal_p_in[in_ch] -= min_val;
                noise_p_in[in_ch] -= min_val;
                parent.DataDispose(parent.ch_info_in[in_ch - 1]);
            }
            max_val -= min_val;
            max_db = (double)((int)(max_val / 10.0) * 10) + 10;

            myasio.SetDataLength(g_leng_backup, parent.without_asio);

            input_test_complete = true;
        }

        private void output_test_btn_Click(object sender, EventArgs e)
        {
            input_test_complete = false;
            output_test_doing = true;

            myasio.SetDataLength(output_dleng, parent.without_asio);

            parent.DataCreate(parent.ch_info_work[0], ChannelInfo.StreamType.no_io);
            int coef_m_numer = System.Convert.ToInt32(tsp_phase_num_tb.Text);
            int coef_m_denom = System.Convert.ToInt32(tsp_phase_denom_tb.Text);
            double coef_shift = System.Convert.ToDouble(tsp_shift_tb.Text);
            int flag = (hilo_cb.Checked ? 1 : -1);
            double amp = System.Convert.ToDouble(amp_tb.Text);
            ChannelInfo cinfo = parent.ch_info_work[0];
            myasio.makeTSP(tsp_fftpoint, (int)cinfo.type, cinfo.btn_no, amp, coef_m_numer, coef_m_denom, coef_shift, flag);

            parent.DataCreate(parent.ch_info_in[in_ch_test - 1], ChannelInfo.StreamType.asioin);
            double db_val, max_val = 0.0;
            for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
            {
                // Buffer create, TSP create
                parent.DataCreate(parent.ch_info_out[out_ch - 1], ChannelInfo.StreamType.asioout);
                ChannelInfo src_cinfo = parent.ch_info_work[0];
                ChannelInfo dst_cinfo = parent.ch_info_out[out_ch - 1];
                myasio.CopyData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
                // PlayStart
                parent.PlayStart();
                // Calc and draw SN ratio.
                cinfo = parent.ch_info_in[in_ch_test - 1];
                db_val = 10.0 * Math.Log10(myasio.GetTimeSigPower((int)cinfo.type, cinfo.btn_no, 0, tsp_fftpoint));
                if (out_ch == out_ch_start || max_val < db_val) max_val = db_val;
                signal_p_out[out_ch] = db_val;

                myasio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                signal_max_out[out_ch] = myasio.GetMaxVal((int)cinfo.type, cinfo.btn_no);
                check_pictureBox.Invalidate();
                // Buffer delete
                parent.DataDispose(parent.ch_info_out[out_ch - 1]);
            }
            parent.DataDispose(parent.ch_info_in[in_ch_test - 1]);
            parent.DataDispose(parent.ch_info_work[0]);

            myasio.SetDataLength(g_leng_backup, parent.without_asio);
        }

        private void check_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 150;
            int yohaku_right = 5;
            int yohaku_top = Font.Height;
            int yohaku_bottom = 5;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            int draw_ch_cnt = (input_test_complete? in_ch_end - in_ch_start + 1: out_ch_end - out_ch_start + 1);
            double bar_height = (double)height / draw_ch_cnt;
            int bar_yohaku = (int)bar_height / 6;
            int bar_width = (int)(bar_height * 0.6666);
            if(bar_width < 2) return;
            if (bar_width > 20)
            {
                bar_width = 20;
                bar_height = 30;
                bar_yohaku = 5;
                yohaku_top += height / 2 - (int)bar_height * draw_ch_cnt / 2;
            }

            Pen pen = new Pen(Color.Black, 2.0F);
            int x1;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            //g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);
            System.Drawing.Font check_font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            double font_y1 = yohaku_top + bar_height / 2 - check_font.Height / 2;
            double y1 = yohaku_top + bar_yohaku;

            if (input_test_complete)
            {
                for (int in_ch = in_ch_start; in_ch <= in_ch_end; in_ch++)
                {
                    x1 = (int)(signal_p_in[in_ch] / max_db * (double)width);
                    g.FillRectangle(Brushes.Green, yohaku_left, (int)y1, x1, bar_width);

                    x1 = (int)(noise_p_in[in_ch] / max_db * (double)width);
                    g.FillRectangle(Brushes.Yellow, yohaku_left, (int)y1, x1, bar_width);
                    g.DrawString(noise_p_in[in_ch].ToString("0.0"), check_font, new SolidBrush(Color.Black), 0, (int)font_y1, format);
                    g.DrawString(signal_p_in[in_ch].ToString("0.0"), check_font, new SolidBrush(Color.Black), 40, (int)font_y1, format);
                    g.DrawString(signal_max_in[in_ch].ToString("0.00"), check_font, new SolidBrush(Color.Black), 80, (int)font_y1, format);
                    g.DrawString(in_ch.ToString(), check_font, new SolidBrush(Color.Black), 120, (int)font_y1, format);

                    font_y1 += bar_height;
                    y1 += bar_height;
                }
                g.DrawString("N[dB]", check_font, new SolidBrush(Color.Black), 0, yohaku_top - check_font.Height, format);
            }
            else if (output_test_doing)
            {
                for (int out_ch = out_ch_start; out_ch <= out_ch_end; out_ch++)
                {
                    x1 = (int)(signal_p_out[out_ch] / max_db * (double)width);
                    g.FillRectangle(Brushes.Green, yohaku_left, (int)y1, x1, bar_width);

                    g.DrawString(signal_p_out[out_ch].ToString("0.0"), check_font, new SolidBrush(Color.Black), 40, (int)font_y1, format);
                    g.DrawString(signal_max_out[out_ch].ToString("0.00"), check_font, new SolidBrush(Color.Black), 80, (int)font_y1, format);
                    g.DrawString(out_ch.ToString(), check_font, new SolidBrush(Color.Black), 120, (int)font_y1, format);

                    font_y1 += bar_height;
                    y1 += bar_height;
                }
            }
            g.DrawString("S[dB]", check_font, new SolidBrush(Color.Black), 40, yohaku_top - check_font.Height, format);
            g.DrawString("Max", check_font, new SolidBrush(Color.Black), 80, yohaku_top - check_font.Height, format);
            g.DrawString("Ch.", check_font, new SolidBrush(Color.Black), 120, yohaku_top - check_font.Height, format);
            g.DrawString(max_db.ToString("0.0") + "dB", check_font, new SolidBrush(Color.Black), yohaku_left + width - 40, yohaku_top - check_font.Height, format);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            check_pictureBox.Size = new Size(Size.Width - 300, Size.Height - 50);
            check_pictureBox.Invalidate();
        }

        private void output_start_tb_TextChanged(object sender, EventArgs e)
        {
            if (output_start_tb.Text != "")
            {
                out_ch_start = System.Convert.ToInt32(output_start_tb.Text);
            }
        }

        private void output_end_tb_TextChanged(object sender, EventArgs e)
        {
            if (output_end_tb.Text != "")
            {
                out_ch_end = System.Convert.ToInt32(output_end_tb.Text);
            }
        }

        private void output_test_tb_TextChanged(object sender, EventArgs e)
        {
            if (output_test_tb.Text != "")
            {
                out_ch_test = System.Convert.ToInt32(output_test_tb.Text);
            }
        }

        private void input_start_tb_TextChanged(object sender, EventArgs e)
        {
            if (input_start_tb.Text != "")
            {
                in_ch_start = System.Convert.ToInt32(input_start_tb.Text);
            }
        }

        private void input_end_tb_TextChanged(object sender, EventArgs e)
        {
            if (input_end_tb.Text != "")
            {
                in_ch_end = System.Convert.ToInt32(input_end_tb.Text);
            }
        }

        private void input_test_tb_TextChanged(object sender, EventArgs e)
        {
            if (input_test_tb.Text != "")
            {
                in_ch_test = System.Convert.ToInt32(input_test_tb.Text);
            }
        }

        private void chg_dleng_btn_Click(object sender, EventArgs e)
        {
            CreateDataForm cdata = new CreateDataForm(parent, "Change the input data length", input_dleng.ToString());
            cdata.ShowDialog();
            input_dleng = System.Convert.ToInt32(parent.reply_msg);
            tsp_fftpoint = parent.Max_fftpoint(input_dleng) / 4;
            tsp_fftpnt_tb.Text = tsp_fftpoint.ToString();
        }

        private void output_chg_dleng_btn_Click(object sender, EventArgs e)
        {
            CreateDataForm cdata = new CreateDataForm(parent, "Change the output data length", output_dleng.ToString());
            cdata.ShowDialog();
            output_dleng = System.Convert.ToInt32(parent.reply_msg);
            tsp_fftpoint = parent.Max_fftpoint(output_dleng);
            tsp_fftpnt_tb.Text = tsp_fftpoint.ToString();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            // PlayStart
            // Analize ch
            // if OK next ch
        }
    }
}
