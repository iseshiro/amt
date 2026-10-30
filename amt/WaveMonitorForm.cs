using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Asio_io;

namespace amt
{
    public partial class WaveMonitorForm : Form
    {
        private Amt parent;
        private int yohaku_left = 5;
        private int yohaku_right = 5;
        private int yohaku_top = 5;
        private int yohaku_bottom = 5;
        private int width;
        private int height;
        private int disp_ch = 0;
        private double disp_time = 5.0;
        private double disp_time_min;
        private int sfreq;
        private int curr_sample_pnt;
        private int pre_sample_pnt;
        private int sample_per_wpnt;
        private int total_buf_leng;
        private int curr_disp_pnt;
        private int level_adjust = 0;
        private int magnify = 1;
        private double[] buff_min;
        private double[] buff_max;
        private asio_io myasio;
        private int fftpnt = 2048;
        private double resolution_freq;
        private double max_freq;
        private double max_level;
        private int auto_cali_cnt = 20;
        private double auto_cali_db = 0.1;
        private CaliInfo[] cali_info;
        private int cali_curr_no = 0;
        private double auto_cali_cnt_1;
        

        public WaveMonitorForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            init_parameter();
        }

        public void Redraw()
        {
            wave_mon_pictureBox.Invalidate();
        }

        private void init_parameter()
        {
            disp_ch_tb.Text = disp_ch.ToString();
            display_time_tb.Text = disp_time.ToString();
            sfreq = parent.sample_freq;
            curr_sample_pnt = myasio.get_adbuf_point();
            pre_sample_pnt = curr_sample_pnt;
            total_buf_leng = myasio.GetDataLength();
            disp_time_min = 10.0 / (double)sfreq * (double)width;
            width = wave_mon_pictureBox.Size.Width - yohaku_left - yohaku_right;
            height = wave_mon_pictureBox.Size.Height - yohaku_top - yohaku_bottom;
            sample_per_wpnt = (int)(disp_time * (double)sfreq / (double)width);
            buff_min = new double[width];
            buff_max = new double[width];
            curr_disp_pnt = 0;
            SetAdjustLevel();

            display_time_lbl.Visible = !spectrum_cb.Checked;
            display_time_tb.Visible = !spectrum_cb.Checked;
            display_unit_lbl.Visible = !spectrum_cb.Checked;
            max_freq_lbl.Visible = spectrum_cb.Checked;
            memo_reset_btn.Visible = spectrum_cb.Checked;

            auto_cali_cb.Visible = spectrum_cb.Checked;
            cali_db_tb.Visible = spectrum_cb.Checked;
            cali_cnt_tb.Visible = spectrum_cb.Checked;
            auto_cali1_lbl.Visible = spectrum_cb.Checked;
            auto_cali2_lbl.Visible = spectrum_cb.Checked;
            memo_reset_btn.Text = (auto_cali_cb.Checked ? "Reset" : "Memo");
            auto_cali_ok_lbl.Visible = false;

            resolution_freq = (double)sfreq / (double)fftpnt;
            cali_db_tb.Text = auto_cali_db.ToString("0.0");
            cali_cnt_tb.Text = auto_cali_cnt.ToString();
            cali_info = new CaliInfo[auto_cali_cnt];
            for (int i = 0; i < auto_cali_cnt; i++)
            {
                cali_info[i] = new CaliInfo();
            }
            auto_cali_cnt_1 = 1.0/(double)auto_cali_cnt;
        }

        private void SetAdjustLevel()
        {
            if (spectrum_cb.Checked)
            {
                adjust_level_lbl.Text = "Adjust level = " + level_adjust.ToString() + " dB";
            }
            else
            {
                adjust_level_lbl.Text = "Magnification = " + magnify.ToString();
            }
        }

        private void wave_mon_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (spectrum_cb.Checked)
            {
                freq_signal(sender, e);
            }
            else
            {
                time_signal(sender, e);
            }
        }

        private void time_signal(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;

            if (disp_ch == 0) return;
            curr_sample_pnt = myasio.get_adbuf_point();
            int buf_update_cnt;
            if (curr_sample_pnt > pre_sample_pnt)
            {
                buf_update_cnt = (curr_sample_pnt - pre_sample_pnt) / sample_per_wpnt;
            }
            else
            {
                buf_update_cnt = (total_buf_leng + curr_sample_pnt - pre_sample_pnt) / sample_per_wpnt;
            }
            if (buf_update_cnt == 0) return;
            if (ch_auto_chg_cb.Checked && parent.max_ch > 0 && disp_ch != parent.max_ch)
            {
                int max_ch = parent.max_ch;
                disp_ch_tb.Text = max_ch.ToString();
            }
            double min, max;
            for (int i = 0; i < buf_update_cnt; i++)
            {
                int st_pnt = pre_sample_pnt;
                pre_sample_pnt += sample_per_wpnt;
                if (pre_sample_pnt > total_buf_leng) pre_sample_pnt -= total_buf_leng;
                int ed_pnt = pre_sample_pnt;
                ChannelInfo disp_cinfo = parent.ch_info_in[disp_ch - 1];
                if (st_pnt < ed_pnt)
                {
                    unsafe
                    {
                        myasio.getminmax(st_pnt, ed_pnt, (int)disp_cinfo.type, disp_cinfo.btn_no, &min, &max);
                    }
                }
                else
                {
                    double min1, max1;
                    unsafe
                    {
                        myasio.getminmax(st_pnt, total_buf_leng, (int)disp_cinfo.type, disp_cinfo.btn_no, &min, &max);
                        myasio.getminmax(0, ed_pnt, (int)disp_cinfo.type, disp_cinfo.btn_no, &min1, &max1);
                        if (min > min1) min = min1;
                        if (max < max1) max = max1;
                    }
                }
                buff_min[curr_disp_pnt] = min;
                buff_max[curr_disp_pnt] = max;
                curr_disp_pnt++;
                if (curr_disp_pnt >= width) curr_disp_pnt = 0;
            }

            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;

            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            pen.DashStyle = DashStyle.Dot;
            for (int grid = 0; grid <= (int)disp_time; grid++)
            {
                x1 = width / (int)disp_time * grid;
                Point[] pnts = { new Point(x1 + yohaku_left, yohaku_top), new Point(x1 + yohaku_left, yohaku_top + height) };
                g.DrawLines(pen, pnts);
            }

            pen = new Pen(Color.Blue, 1.0F);
            pen.DashStyle = DashStyle.Solid;

            double h_scale = (double)(height / 2 * magnify);
            for (int i = 0; i < width; i++)
            {
                int buf_pnt = curr_disp_pnt + i;
                if (buf_pnt >= width)
                {
                    buf_pnt -= width;
                }
                x1 = yohaku_left + i;

                y1 = yohaku_top + height / 2 - (int)(buff_min[buf_pnt] * h_scale);
                y2 = yohaku_top + height / 2 - (int)(buff_max[buf_pnt] * h_scale);

                x2 = (y1 == y2 ? x1 + 1 : x1);
                g.DrawLines(pen, new Point[] { new Point(x1, y1), new Point(x2, y2) });
            }
        }

        private void freq_signal(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;

            if (disp_ch == 0) return;
            int end_sample_pnt = myasio.get_adbuf_point();
            int start_sample_pnt = end_sample_pnt - fftpnt;

            if (ch_auto_chg_cb.Checked && parent.max_ch > 0 && disp_ch != parent.max_ch)
            {
                int max_ch = parent.max_ch;
                disp_ch_tb.Text = max_ch.ToString();
            }
            if (start_sample_pnt > 0)
            {
                ChannelInfo disp_cinfo = parent.ch_info_in[disp_ch - 1];
                myasio.makeFFT(fftpnt, start_sample_pnt, end_sample_pnt, (int)disp_cinfo.type, disp_cinfo.btn_no);
            }
            int freq_start_pnt = 0;
            int freq_end_pnt = fftpnt / 2 - 1;
            double famp_max = myasio.famp(freq_start_pnt);
            double ff;
            int ff_pnt = freq_start_pnt;
            for (int i = freq_start_pnt + 1; i < freq_end_pnt; i++)
            {
                ff = myasio.famp(i);
                if (ff > famp_max)
                {
                    famp_max = ff;
                    ff_pnt = i;
                }
            }
            max_freq = (double)ff_pnt * resolution_freq;
            max_level = 10.0 * Math.Log10(famp_max);

            max_freq_lbl.Text = "Max freq. = " + max_freq.ToString("0.0") + "Hz : " + max_level.ToString("0.0") + "dB";

            if (auto_cali_cb.Checked)
            {
                cali_info[cali_curr_no].ch = disp_ch;
                cali_info[cali_curr_no].level = max_level;
                cali_info[cali_curr_no].fpnt = ff_pnt;
                cali_curr_no++;
                if (cali_curr_no >= auto_cali_cnt) cali_curr_no = 0;

                int first_ch = cali_info[0].ch;
                int first_fpnt = cali_info[0].fpnt;
                double sum_level = cali_info[0].level;
                bool ok = true;
                for (int i = 1; i < auto_cali_cnt; i++)
                {
                    if (first_ch != cali_info[i].ch)
                    {
                        ok = false;
                        break;
                    }
                    if (first_fpnt != cali_info[i].fpnt)
                    {
                        ok = false;
                        break;
                    }
                    sum_level += cali_info[i].level;
                }
                sum_level *= auto_cali_cnt_1;
                for (int i = 0; i < auto_cali_cnt; i++)
                {
                    double diff = Math.Abs(sum_level - cali_info[i].level);
                    if (diff > auto_cali_db)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    parent.ch_info_in[disp_ch - 1].max_freq = max_freq;
                    parent.ch_info_in[disp_ch - 1].max_level = sum_level;
                    auto_cali_ok_lbl.Text = "CH = " + disp_ch.ToString() + " : Level = " + sum_level.ToString("0.0") + "dB OK";
                }
                auto_cali_ok_lbl.Visible = ok;
            }

            int disp_range = 100;
            int grid_range = 10;
            int grid_freq = 5000;
            double h_scale = (double)height / (double)disp_range;
            double w_scale = (double)width / (double)(freq_end_pnt - freq_start_pnt);

            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;

            int freq_width = sfreq / 2;
            for (int grid = grid_freq; grid < freq_width; grid += grid_freq)
            {
                pen.DashStyle = DashStyle.Dot;
                x1 = width * grid / freq_width;
                Point[] pnts = { new Point(x1 + yohaku_left, yohaku_top), new Point(x1 + yohaku_left, yohaku_top + height) };
                g.DrawLines(pen, pnts);
            }

            for (int grid = grid_range; grid <= disp_range; grid += grid_range)
            {
                pen.DashStyle = DashStyle.Dot;
                x1 = yohaku_left;
                x2 = yohaku_left + width;
                y1 = yohaku_top + height * grid / disp_range;
                Point[] pnts = { new Point(x1, y1), new Point(x2, y1) };
                g.DrawLines(pen, pnts);
            }

            pen.DashStyle = DashStyle.Solid;
            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            pen = new Pen(Color.Blue, 1.0F);

            x1 = yohaku_left;
            y1 = yohaku_top - (int)((10.0 * Math.Log10(myasio.famp(freq_start_pnt)) + level_adjust) * h_scale);

            for (int i = freq_start_pnt + 1; i < freq_end_pnt; i++)
            {
                x2 = (int)(w_scale * (double)i) + yohaku_left;
                y2 = yohaku_top - (int)((10.0 * Math.Log10(myasio.famp(i)) + level_adjust) * h_scale);
                if (y2 > yohaku_top + height) y2 = yohaku_top + height;

                g.DrawLines(pen, new Point[] { new Point(x1, y1), new Point(x2, y2) });
                x1 = x2;
                y1 = y2;
            }
        }

        private void disp_ch_tb_TextChanged(object sender, EventArgs e)
        {
            if (disp_ch_tb.Text == "") return;
            int ch = System.Convert.ToInt32(disp_ch_tb.Text);
            if (ch > parent.in_btn_cnt)
            {
                ch = parent.in_btn_cnt;
            }
            else if (ch <= 0)
            {
                ch = 1;
            }
            ChannelInfo cinfo = parent.ch_info_in[ch - 1];
            if (myasio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                disp_ch = ch;
                disp_ch_tb.Text = disp_ch.ToString();
                for (int i = 0; i < width; i++)
                {
                    buff_min[i] = buff_max[i] = 0.0;
                }
            }
        }

        private void display_time_tb_TextChanged(object sender, EventArgs e)
        {
            if (display_time_tb.Text == "") return;
            disp_time = System.Convert.ToDouble(display_time_tb.Text);
            if (disp_time_min > disp_time)
            {
                disp_time = disp_time_min;
                display_time_tb.Text = disp_time.ToString();
            }
            sample_per_wpnt = (int)(disp_time * (double)sfreq / (double)width);
            for (int i = 0; i < width; i++)
            {
                buff_min[i] = buff_max[i] = 0.0;
            }
        }

        private void ch_plus_btn_Click(object sender, EventArgs e)
        {
            int ch = System.Convert.ToInt32(disp_ch_tb.Text) + 1;
            if (ch > parent.in_btn_cnt)
            {
                ch = parent.in_btn_cnt;
            }
            ChannelInfo cinfo = parent.ch_info_in[ch - 1];
            if (myasio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                disp_ch = ch;
                disp_ch_tb.Text = disp_ch.ToString();
            }
        }

        private void ch_minus_btn_Click(object sender, EventArgs e)
        {
            int ch = System.Convert.ToInt32(disp_ch_tb.Text) - 1;
            if (ch <= 0)
            {
                ch = 1;
            }
            ChannelInfo cinfo = parent.ch_info_in[ch - 1];
            if (myasio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                disp_ch = ch;
                disp_ch_tb.Text = disp_ch.ToString();
            }
        }

        private void adjust_minus_btn_Click(object sender, EventArgs e)
        {
            if (spectrum_cb.Checked)
            {
                level_adjust -= 10;
            }
            else
            {
                magnify--;
                if (magnify <= 0) magnify = 1;
            }
            SetAdjustLevel();
        }

        private void adjust_plus_btn_Click(object sender, EventArgs e)
        {
            if (spectrum_cb.Checked)
            {
                level_adjust += 10;
            }
            else
            {
                magnify++;
            }
            SetAdjustLevel();
        }

        private void spectrum_cb_CheckedChanged(object sender, EventArgs e)
        {
            display_time_lbl.Visible = !spectrum_cb.Checked;
            display_time_tb.Visible = !spectrum_cb.Checked;
            display_unit_lbl.Visible = !spectrum_cb.Checked;

            auto_cali_cb.Visible = spectrum_cb.Checked;
            cali_db_tb.Visible = spectrum_cb.Checked;
            cali_cnt_tb.Visible = spectrum_cb.Checked;
            auto_cali1_lbl.Visible = spectrum_cb.Checked;
            auto_cali2_lbl.Visible = spectrum_cb.Checked;

            max_freq_lbl.Visible = spectrum_cb.Checked;
            memo_reset_btn.Visible = spectrum_cb.Checked;
            memo_reset_btn.Text = (auto_cali_cb.Checked?"Reset":"Memo");

            auto_cali_ok_lbl.Visible = false;
        }

        private void memo_reset_btn_Click(object sender, EventArgs e)
        {
            if (auto_cali_cb.Checked)
            {
                parent.Ch_info_clear();
            }
            else
            {
                parent.ch_info_in[disp_ch - 1].max_freq = max_freq;
                parent.ch_info_in[disp_ch - 1].max_level = max_level;
            }
        }

        private void cali_cnt_tb_TextChanged(object sender, EventArgs e)
        {
            if (cali_cnt_tb.Text == "") return;
            int iii = System.Convert.ToInt32(cali_cnt_tb.Text);
            if (iii > 10)
            {
                auto_cali_cnt = iii;
                cali_info = new CaliInfo[auto_cali_cnt];
                for (int i = 0; i < auto_cali_cnt; i++)
                {
                    cali_info[i] = new CaliInfo();
                }
                auto_cali_cnt_1 = 1.0 / (double)auto_cali_cnt;
            }
        }

        private void cali_db_tb_TextChanged(object sender, EventArgs e)
        {
            if (cali_db_tb.Text == "") return;
            auto_cali_db = System.Convert.ToDouble(cali_db_tb.Text);
        }

        private void auto_cali_cb_CheckedChanged(object sender, EventArgs e)
        {
            memo_reset_btn.Text = (auto_cali_cb.Checked ? "Reset" : "Memo");
        }
    }

    public class CaliInfo
    {
        public int ch;
        public int fpnt;
        public double level;

        public CaliInfo()
        {
            ch = -1;
        }
    }
}
