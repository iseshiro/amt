using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace amt
{
    public partial class GrphEqForm : Form
    {
        private double[] center_freq;
        private double[] eq_db;
        private double[] eq_val;
        private int fftpnt_disp;
        private int delay;
        private float[] xre = null;
        private float[] zre = null;
        private double sfreq;
        private double qq1;
        private double qq2;
        private Amt parent = null;
        private double track_db_max = 12.0;
        private double track_db_min = -12.0;
        private double track_db_step = 1.0;
        private int track_val_max;
        private int track_val_min;
        private bool is_convset = false;
        private RealtimeConv rtconv = null;

        public GrphEqForm(Amt Parent, bool is_ConvSet)
        {
            InitializeComponent();
            parent = Parent;
            center_freq = new double[26];
            eq_db = new double[26];
            eq_val = new double[26];
            sfreq = parent.sample_freq;

            qq1 = Math.Pow(2.0, -1.0 / 6.0);
            qq2 = Math.Pow(2.0, 1.0 / 6.0);
            rtconv = new RealtimeConv(parent, 1, 1, 1);
            init_buffers();
            for (int i = 0; i < 26; i++)
            {
                center_freq[i] = 50 * Math.Pow(2.0, (double)i / 3.0);
                eq_db[i] = 0;
            }
            update_all(1);
            int conv_leng = rtconv.GetConvLeng();
            conv_leng_tb.Text = conv_leng.ToString();
            delay = conv_leng / 2;
            delay_tb.Text = delay.ToString();
            is_convset = is_ConvSet;
            if (is_convset)
            {
                fft_level_ud.Enabled = false;
                fftpoint_tb.Enabled = true;
                delay_tb.Enabled = true;
                calc_btn.Enabled = true;
            }
        }

        private void set_label_val()
        {
            lbl_50_val.Text = eq_val[0].ToString("0.0");
            lbl_63_val.Text = eq_val[1].ToString("0.0");
            lbl_80_val.Text = eq_val[2].ToString("0.0");
            lbl_100_val.Text = eq_val[3].ToString("0.0");
            lbl_125_val.Text = eq_val[4].ToString("0.0");
            lbl_160_val.Text = eq_val[5].ToString("0.0");
            lbl_200_val.Text = eq_val[6].ToString("0.0");
            lbl_250_val.Text = eq_val[7].ToString("0.0");
            lbl_315_val.Text = eq_val[8].ToString("0.0");
            lbl_400_val.Text = eq_val[9].ToString("0.0");
            lbl_500_val.Text = eq_val[10].ToString("0.0");
            lbl_630_val.Text = eq_val[11].ToString("0.0");
            lbl_800_val.Text = eq_val[12].ToString("0.0");
            lbl_1k_val.Text = eq_val[13].ToString("0.0");
            lbl_1k25_val.Text = eq_val[14].ToString("0.0");
            lbl_1k6_val.Text = eq_val[15].ToString("0.0");
            lbl_2k_val.Text = eq_val[16].ToString("0.0");
            lbl_2k5_val.Text = eq_val[17].ToString("0.0");
            lbl_3k15_val.Text = eq_val[18].ToString("0.0");
            lbl_4k_val.Text = eq_val[19].ToString("0.0");
            lbl_5k_val.Text = eq_val[20].ToString("0.0");
            lbl_6k3_val.Text = eq_val[21].ToString("0.0");
            lbl_8k_val.Text = eq_val[22].ToString("0.0");
            lbl_10k_val.Text = eq_val[23].ToString("0.0");
            lbl_12k5_val.Text = eq_val[24].ToString("0.0");
            lbl_16k_val.Text = eq_val[25].ToString("0.0");
        }

        public string get_eqdata()
        {
            string ret = "" ;
            if (eq_db[0] != 0.0) ret += "eq_50=" + eq_db[0].ToString("0.0") + ";";
            if (eq_db[1] != 0.0) ret += "eq_63=" + eq_db[1].ToString("0.0") + ";";
            if (eq_db[2] != 0.0) ret += "eq_80=" + eq_db[2].ToString("0.0") + ";";
            if (eq_db[3] != 0.0) ret += "eq_100=" + eq_db[3].ToString("0.0") + ";";
            if (eq_db[4] != 0.0) ret += "eq_125=" + eq_db[4].ToString("0.0") + ";";
            if (eq_db[5] != 0.0) ret += "eq_160=" + eq_db[5].ToString("0.0") + ";";
            if (eq_db[6] != 0.0) ret += "eq_200=" + eq_db[6].ToString("0.0") + ";";
            if (eq_db[7] != 0.0) ret += "eq_250=" + eq_db[7].ToString("0.0") + ";";
            if (eq_db[8] != 0.0) ret += "eq_315=" + eq_db[8].ToString("0.0") + ";";
            if (eq_db[9] != 0.0) ret += "eq_400=" + eq_db[9].ToString("0.0") + ";";
            if (eq_db[10] != 0.0) ret += "eq_500=" + eq_db[10].ToString("0.0") + ";";
            if (eq_db[11] != 0.0) ret += "eq_630=" + eq_db[11].ToString("0.0") + ";";
            if (eq_db[12] != 0.0) ret += "eq_800=" + eq_db[12].ToString("0.0") + ";";
            if (eq_db[13] != 0.0) ret += "eq_1k=" + eq_db[13].ToString("0.0") + ";";
            if (eq_db[14] != 0.0) ret += "eq_1k25=" + eq_db[14].ToString("0.0") + ";";
            if (eq_db[15] != 0.0) ret += "eq_1k6=" + eq_db[15].ToString("0.0") + ";";
            if (eq_db[16] != 0.0) ret += "eq_2k=" + eq_db[16].ToString("0.0") + ";";
            if (eq_db[17] != 0.0) ret += "eq_2k5=" + eq_db[17].ToString("0.0") + ";";
            if (eq_db[18] != 0.0) ret += "eq_3k15=" + eq_db[18].ToString("0.0") + ";";
            if (eq_db[19] != 0.0) ret += "eq_4k=" + eq_db[19].ToString("0.0") + ";";
            if (eq_db[20] != 0.0) ret += "eq_5k=" + eq_db[20].ToString("0.0") + ";";
            if (eq_db[21] != 0.0) ret += "eq_6k3=" + eq_db[21].ToString("0.0") + ";";
            if (eq_db[22] != 0.0) ret += "eq_8k=" + eq_db[22].ToString("0.0") + ";";
            if (eq_db[23] != 0.0) ret += "eq_10k=" + eq_db[23].ToString("0.0") + ";";
            if (eq_db[24] != 0.0) ret += "eq_12k5=" + eq_db[24].ToString("0.0") + ";";
            if (eq_db[25] != 0.0) ret += "eq_16k=" + eq_db[25].ToString("0.0") + ";";
            return ret;
        }

        public void create_eqdata(string eq_text, int eq_data_leng)
        {   
            for (int i = 0; i < 26; i++) eq_db[i] = 0;
            if (eq_text == "flat")
            {
                rtconv.InitEQdata();
                return;
            }
            string[] eq_list = eq_text.Split(';');
            for (int i = 0; i < eq_list.Length; i++)
            {
                string[] eq_item = eq_list[i].Split('=');
                if (eq_item[0] == "eq_50") eq_db[0] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_63") eq_db[1] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_80") eq_db[2] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_100") eq_db[3] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_125") eq_db[4] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_160") eq_db[5] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_200") eq_db[6] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_250") eq_db[7] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_315") eq_db[8] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_400") eq_db[9] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_500") eq_db[10] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_630") eq_db[11] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_800") eq_db[12] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_1k") eq_db[13] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_1k25") eq_db[14] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_1k6") eq_db[15] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_2k") eq_db[16] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_2k5") eq_db[17] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_3k15") eq_db[18] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_4k") eq_db[19] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_5k") eq_db[20] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_6k3") eq_db[21] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_8k") eq_db[22] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_10k") eq_db[23] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_12k5") eq_db[24] = Convert.ToDouble(eq_item[1]);
                else if (eq_item[0] == "eq_16k") eq_db[25] = Convert.ToDouble(eq_item[1]);
            }
            rtconv.SetFFTLevel(eq_data_leng);
            init_buffers();
            update_all(1);
        }

        private void update_eqval()
        {
            float[] yre = new float[fftpnt_disp];
            float[] yim = new float[fftpnt_disp];

            for (int i = 1; i < fftpnt_disp / 2;i++ )
            {
                yre[i] = yre[fftpnt_disp - i] = (float)Math.Pow(10.0, xre[i]/20.0);
                yim[i] = yim[fftpnt_disp - i] = 0f;
            }
            yre[0] = yre[fftpnt_disp / 2] = yim[0] = yim[fftpnt_disp / 2] = 0f;

            unsafe
            {
                fixed (float* dre = yre, dim = yim)
                {
                    parent.asio.FFT(dre, dim, -fftpnt_disp);
                }
            }

            int conv_leng = rtconv.GetConvLeng();
            float[] xre1 = new float[conv_leng];
            for (int i = 0; i < delay; i++)
            {
                xre1[i] = yre[fftpnt_disp + i - delay];
            }
            for (int i = delay; i < conv_leng; i++)
            {
                xre1[i] = yre[i - delay];
            }
            double amp = 1.0 / (double)fftpnt_disp;
            for (int i = 0; i < conv_leng; i++)
            {
                yre[i] = xre1[i];
                yim[i] = 0f;
            }
            rtconv.SetEQdata(xre1, amp);
            for (int i = conv_leng; i < fftpnt_disp; i++)
            {
                yre[i] = yim[i] = 0f;
            }
            unsafe
            {
                fixed (float* dre = yre, dim = yim)
                {
                    parent.asio.FFT(dre, dim, fftpnt_disp);
                }
            }
            for (int i = 0; i < fftpnt_disp/2; i++)
            {
                float dd = yre[i] * yre[i] + yim[i] * yim[i];
                zre[i] = (float)(10.0 * Math.Log10((double)dd));
            }
            for (int j = 0; j < 26; j++)
            {
                double cfreq = (double)center_freq[j];
                double cfreq1 = cfreq * qq1;
                if (j == 0) cfreq1 = 20.0;
                double cfreq2 = cfreq * qq2;
                if (j == 25) cfreq2 = 20000.0;
                int st_pnt = (int)(cfreq1 / sfreq * (double)fftpnt_disp);
                int ed_pnt = (int)(cfreq2 / sfreq * (double)fftpnt_disp);
                double sum = 0.0;
                for (int i = st_pnt; i < ed_pnt; i++)
                {
                    sum += zre[i];
                }
                if (ed_pnt > st_pnt) eq_val[j] = sum / (double)(ed_pnt - st_pnt);
                else eq_val[j] = 0;
            }
        }

        private void init_buffers()
        {
            fftpnt_disp = 2;
            int conv_leng = rtconv.GetConvLeng();
            while (fftpnt_disp < conv_leng * 2) fftpnt_disp *= 2;
            if (fftpnt_disp < 32768) fftpnt_disp = 32768;
            xre = new float[fftpnt_disp/2];
            zre = new float[fftpnt_disp / 2];
            fftpoint_tb.Text = fftpnt_disp.ToString();
        }

        private void update_all(int update_level)
        {
            if(update_level < 2)
            {
                set_label_set();
                set_trackbar_val();
            }
            if (update_level < 3)
            {
                update_xre();
                update_eqval();
                set_label_val();
                this.Refresh();
            }
        }

        private void update_xre()
        {
            int ed_pnt = 0;
            // Create freq pattern;
            for (int j = 0; j < 26; j++)
            {
                double cfreq = (double)center_freq[j];
                double cfreq1 = cfreq * qq1;
                if (j == 0) cfreq1 = 20.0;
                double cfreq2 = cfreq * qq2;
                if (j == 25) cfreq2 = 20000.0;
                int st_pnt = (int)(cfreq1 / sfreq * (double)fftpnt_disp);
                ed_pnt = (int)(cfreq2 / sfreq * (double)fftpnt_disp);
                for (int i = st_pnt; i < ed_pnt; i++)
                {
                    xre[i] = (float)eq_db[j];
                }
            }
            for (int i = ed_pnt; i < fftpnt_disp / 2; i++)
            {
                xre[i] = (float)eq_db[25];
            }
        }

        private void common_func(int freq_no, int val)
        {
            eq_db[freq_no] = (double)val * track_db_step;
            update_all(2);
        }

        private void trackBar_50_Scroll(object sender, EventArgs e)
        {
            lbl_50_set.Text = ((double)trackBar_50.Value * track_db_step).ToString();
            common_func(0, trackBar_50.Value);
        }

        private void trackBar_63_Scroll(object sender, EventArgs e)
        {
            lbl_63_set.Text = ((double)trackBar_63.Value * track_db_step).ToString();
            common_func(1, trackBar_63.Value);
        }

        private void trackBar_80_Scroll(object sender, EventArgs e)
        {
            lbl_80_set.Text = ((double)trackBar_80.Value * track_db_step).ToString();
            common_func(2, trackBar_80.Value);
        }

        private void trackBar_100_Scroll(object sender, EventArgs e)
        {
            lbl_100_set.Text = ((double)trackBar_100.Value * track_db_step).ToString();
            common_func(3, trackBar_100.Value);
        }

        private void trackBar_125_Scroll(object sender, EventArgs e)
        {
            lbl_125_set.Text = ((double)trackBar_125.Value * track_db_step).ToString();
            common_func(4, trackBar_125.Value);
        }

        private void trackBar_160_Scroll(object sender, EventArgs e)
        {
            lbl_160_set.Text = ((double)trackBar_160.Value * track_db_step).ToString();
            common_func(5, trackBar_160.Value);
        }

        private void trackBar_200_Scroll(object sender, EventArgs e)
        {
            lbl_200_set.Text = ((double)trackBar_200.Value * track_db_step).ToString();
            common_func(6, trackBar_200.Value);
        }

        private void trackBar_250_Scroll(object sender, EventArgs e)
        {
            lbl_250_set.Text = ((double)trackBar_250.Value * track_db_step).ToString();
            common_func(7, trackBar_250.Value);
        }

        private void trackBar_315_Scroll(object sender, EventArgs e)
        {
            lbl_315_set.Text = ((double)trackBar_315.Value * track_db_step).ToString();
            common_func(8, trackBar_315.Value);
        }

        private void trackBar_400_Scroll(object sender, EventArgs e)
        {
            lbl_400_set.Text = ((double)trackBar_400.Value * track_db_step).ToString();
            common_func(9, trackBar_400.Value);
        }

        private void trackBar_500_Scroll(object sender, EventArgs e)
        {
            lbl_500_set.Text = ((double)trackBar_500.Value * track_db_step).ToString();
            common_func(10, trackBar_500.Value);
        }

        private void trackBar_630_Scroll(object sender, EventArgs e)
        {
            lbl_630_set.Text = ((double)trackBar_630.Value * track_db_step).ToString();
            common_func(11, trackBar_630.Value);
        }

        private void trackBar_800_Scroll(object sender, EventArgs e)
        {
            lbl_800_set.Text = ((double)trackBar_800.Value * track_db_step).ToString();
            common_func(12, trackBar_800.Value);
        }

        private void trackBar_1k_Scroll(object sender, EventArgs e)
        {
            lbl_1k_set.Text = ((double)trackBar_1k.Value * track_db_step).ToString();
            common_func(13, trackBar_1k.Value);
        }

        private void trackBar_1k25_Scroll(object sender, EventArgs e)
        {
            lbl_1k25_set.Text = ((double)trackBar_1k25.Value * track_db_step).ToString();
            common_func(14, trackBar_1k25.Value);
        }

        private void trackBar_1k6_Scroll(object sender, EventArgs e)
        {
            lbl_1k6_set.Text = ((double)trackBar_1k6.Value * track_db_step).ToString();
            common_func(15, trackBar_1k6.Value);
        }

        private void trackBar_2k_Scroll(object sender, EventArgs e)
        {
            lbl_2k_set.Text = ((double)trackBar_2k.Value * track_db_step).ToString();
            common_func(16, trackBar_2k.Value);
        }

        private void trackBar_2k5_Scroll(object sender, EventArgs e)
        {
            lbl_2k5_set.Text = ((double)trackBar_2k5.Value * track_db_step).ToString();
            common_func(17, trackBar_2k5.Value);
        }

        private void trackBar_3k15_Scroll(object sender, EventArgs e)
        {
            lbl_3k15_set.Text = ((double)trackBar_3k15.Value * track_db_step).ToString();
            common_func(18, trackBar_3k15.Value);
        }

        private void trackBar_4k_Scroll(object sender, EventArgs e)
        {
            lbl_4k_set.Text = ((double)trackBar_4k.Value * track_db_step).ToString();
            common_func(19, trackBar_4k.Value);
        }

        private void trackBar_5k_Scroll(object sender, EventArgs e)
        {
            lbl_5k_set.Text = ((double)trackBar_5k.Value * track_db_step).ToString();
            common_func(20, trackBar_5k.Value);
        }

        private void trackBar_6k3_Scroll(object sender, EventArgs e)
        {
            lbl_6k3_set.Text = ((double)trackBar_6k3.Value * track_db_step).ToString();
            common_func(21, trackBar_6k3.Value);
        }

        private void trackBar_8k_Scroll(object sender, EventArgs e)
        {
            lbl_8k_set.Text = ((double)trackBar_8k.Value * track_db_step).ToString();
            common_func(22, trackBar_8k.Value);
        }

        private void trackBar_10k_Scroll(object sender, EventArgs e)
        {
            lbl_10k_set.Text = ((double)trackBar_10k.Value * track_db_step).ToString();
            common_func(23, trackBar_10k.Value);
        }

        private void trackBar_12k5_Scroll(object sender, EventArgs e)
        {
            lbl_12k5_set.Text = ((double)trackBar_12k5.Value * track_db_step).ToString();
            common_func(24, trackBar_12k5.Value);
        }

        private void trackBar_16k_Scroll(object sender, EventArgs e)
        {
            lbl_16k_set.Text = ((double)trackBar_16k.Value * track_db_step).ToString();
            common_func(25, trackBar_16k.Value);
        }

        private void fft_level_ud_ValueChanged(object sender, EventArgs e)
        {
            rtconv.SetFFTLevel((int)fft_level_ud.Value);
            int conv_leng = rtconv.GetConvLeng();
            conv_leng_tb.Text = conv_leng.ToString();
            delay = conv_leng / 2;
            delay_tb.Text = delay.ToString();
            init_buffers();
            update_all(2);
        }

        private void waveformBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 25;
            int yohaku_right = 10;
            int yohaku_top = 20;
            int yohaku_bottom = 20;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;


            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            Point[] points;// = {new Point(yohaku_left, yohaku_top+height/2), new Point(yohaku_left+width, yohaku_top+height/2)};
            //			g.DrawLines(pen, points);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            double start_freq = 20.0;
            double[] grid_freq = new double[] { 20.0, 100.0, 1000.0, 10000.0, 20000.0 };
            string[] grid_str = new string[] { "20", "100", "1k", "10k", "20k" };
            double end_freq = sfreq / 2.0;
            int start_freq_point = (int)(start_freq / sfreq * fftpnt_disp);
            int end_freq_point = (int)(end_freq / sfreq * fftpnt_disp);
            double w_scale = (double)width / Math.Log10(end_freq / start_freq);
            pen.DashStyle = DashStyle.Dot;
            for (int i = 0; i < grid_freq.Length; i++)
            {
                double freq = (double)i / (double)fftpnt_disp * sfreq;
                x1 = (int)(w_scale * Math.Log10(grid_freq[i] / start_freq)) + yohaku_left;

                Point[] pnts = { new Point(x1, yohaku_top), new Point(x1, yohaku_top + height) };
                g.DrawLines(pen, pnts);
                g.DrawString(grid_str[i], this.Font, new SolidBrush(Color.Black), x1, yohaku_top + height + 2, format);
            }
           format.Alignment = StringAlignment.Far;

            g.DrawString("[dB]", this.Font, new SolidBrush(Color.Black), yohaku_left, 2, format);
            g.DrawString("[Hz]", this.Font, new SolidBrush(Color.Black), ss.Size.Width - 80, ss.Size.Height - this.Font.Height, format);

            double disp_range = track_db_max - track_db_min;
            int grid_range = 6;
            for (double grid = track_db_min; grid <= disp_range; grid += grid_range)
            {
                pen.DashStyle = DashStyle.Dot;
                x1 = yohaku_left;
                x2 = yohaku_left + width;
                y1 = yohaku_top + (int)((double)height * (1.0 - (grid - track_db_min) / disp_range));
                Point[] pnts = { new Point(x1, y1), new Point(x2, y1) };
                g.DrawLines(pen, pnts);
                g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left, y1 - this.Font.Height / 2, format);
            }

            if (xre == null) return;

            pen.DashStyle = DashStyle.Solid;
            pen = new Pen(Color.Blue, 1.0F);

            double h_scale = (double)height / (double)disp_range;

            x1 = yohaku_left;
            y1 = yohaku_top + height - (int)((xre[start_freq_point] - track_db_min) * h_scale);

            for (int i = start_freq_point + 1; i < end_freq_point; i++)
            {
                double freq = (double)i / (double)fftpnt_disp * sfreq;
                x2 = (int)(w_scale * Math.Log10(freq / start_freq)) + yohaku_left;
                y2 = yohaku_top + height - (int)((xre[i] - track_db_min) * h_scale);
                if (y2 > yohaku_top + height) y2 = yohaku_top + height;

                points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                g.DrawLines(pen, points);
                x1 = x2;
                y1 = y2;
            }

            if (zre == null) return;

            pen = new Pen(Color.Red, 1.0F);

            x1 = yohaku_left;
            y1 = yohaku_top + height - (int)((zre[start_freq_point] - track_db_min) * h_scale);

            for (int i = start_freq_point + 1; i < end_freq_point; i++)
            {
                double freq = (double)i / (double)fftpnt_disp * sfreq;
                x2 = (int)(w_scale * Math.Log10(freq / start_freq)) + yohaku_left;
                y2 = yohaku_top + height - (int)((zre[i] - track_db_min) * h_scale);
                if (y2 > yohaku_top + height) y2 = yohaku_top + height;

                points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                g.DrawLines(pen, points);
                x1 = x2;
                y1 = y2;
            }
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            rtconv.Init_eq(false);
        }

        private void set_trackbar_val()
        {
            int i = 0;
            trackBar_50.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_63.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_80.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_100.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_125.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_160.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_200.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_250.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_315.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_400.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_500.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_630.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_800.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_1k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_1k25.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_1k6.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_2k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_2k5.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_3k15.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_4k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_5k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_6k3.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_8k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_10k.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_12k5.Value = (int)(eq_db[i++] / track_db_step);
            trackBar_16k.Value = (int)(eq_db[i++] / track_db_step);
        }

        private void set_label_set()
        {
            lbl_50_set.Text = eq_db[0].ToString("0.0");
            lbl_63_set.Text = eq_db[1].ToString("0.0");
            lbl_80_set.Text = eq_db[2].ToString("0.0");
            lbl_100_set.Text = eq_db[3].ToString("0.0");
            lbl_125_set.Text = eq_db[4].ToString("0.0");
            lbl_160_set.Text = eq_db[5].ToString("0.0");
            lbl_200_set.Text = eq_db[6].ToString("0.0");
            lbl_250_set.Text = eq_db[7].ToString("0.0");
            lbl_315_set.Text = eq_db[8].ToString("0.0");
            lbl_400_set.Text = eq_db[9].ToString("0.0");
            lbl_500_set.Text = eq_db[10].ToString("0.0");
            lbl_630_set.Text = eq_db[11].ToString("0.0");
            lbl_800_set.Text = eq_db[12].ToString("0.0");
            lbl_1k_set.Text = eq_db[13].ToString("0.0");
            lbl_1k25_set.Text = eq_db[14].ToString("0.0");
            lbl_1k6_set.Text = eq_db[15].ToString("0.0");
            lbl_2k_set.Text = eq_db[16].ToString("0.0");
            lbl_2k5_set.Text = eq_db[17].ToString("0.0");
            lbl_3k15_set.Text = eq_db[18].ToString("0.0");
            lbl_4k_set.Text = eq_db[19].ToString("0.0");
            lbl_5k_set.Text = eq_db[20].ToString("0.0");
            lbl_6k3_set.Text = eq_db[21].ToString("0.0");
            lbl_8k_set.Text = eq_db[22].ToString("0.0");
            lbl_10k_set.Text = eq_db[23].ToString("0.0");
            lbl_12k5_set.Text = eq_db[24].ToString("0.0");
            lbl_16k_set.Text = eq_db[25].ToString("0.0");
        }

        private void flat_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 26; i++)
            {
                eq_db[i] = 0.0;
            }
            update_all(1);
            rtconv.InitEQdata();
        }

        private void slider_set()
        {
            track_val_max = (int)(track_db_max / track_db_step);
            track_val_min = (int)(track_db_min / track_db_step);
            max_db_lbl.Text = track_db_max.ToString() + "dB";
            min_db_lbl.Text = track_db_min.ToString() + "dB";

            trackBar_50.Maximum = track_val_max;
            trackBar_63.Maximum = track_val_max;
            trackBar_80.Maximum = track_val_max;
            trackBar_100.Maximum = track_val_max;
            trackBar_125.Maximum = track_val_max;
            trackBar_160.Maximum = track_val_max;
            trackBar_200.Maximum = track_val_max;
            trackBar_250.Maximum = track_val_max;
            trackBar_315.Maximum = track_val_max;
            trackBar_400.Maximum = track_val_max;
            trackBar_500.Maximum = track_val_max;
            trackBar_630.Maximum = track_val_max;
            trackBar_800.Maximum = track_val_max;
            trackBar_1k.Maximum = track_val_max;
            trackBar_1k25.Maximum = track_val_max;
            trackBar_1k6.Maximum = track_val_max;
            trackBar_2k.Maximum = track_val_max;
            trackBar_2k5.Maximum = track_val_max;
            trackBar_3k15.Maximum = track_val_max;
            trackBar_4k.Maximum = track_val_max;
            trackBar_5k.Maximum = track_val_max;
            trackBar_6k3.Maximum = track_val_max;
            trackBar_8k.Maximum = track_val_max;
            trackBar_10k.Maximum = track_val_max;
            trackBar_12k5.Maximum = track_val_max;
            trackBar_16k.Maximum = track_val_max;

            trackBar_50.Minimum = track_val_min;
            trackBar_63.Minimum = track_val_min;
            trackBar_80.Minimum = track_val_min;
            trackBar_100.Minimum = track_val_min;
            trackBar_125.Minimum = track_val_min;
            trackBar_160.Minimum = track_val_min;
            trackBar_200.Minimum = track_val_min;
            trackBar_250.Minimum = track_val_min;
            trackBar_315.Minimum = track_val_min;
            trackBar_400.Minimum = track_val_min;
            trackBar_500.Minimum = track_val_min;
            trackBar_630.Minimum = track_val_min;
            trackBar_800.Minimum = track_val_min;
            trackBar_1k.Minimum = track_val_min;
            trackBar_1k25.Minimum = track_val_min;
            trackBar_1k6.Minimum = track_val_min;
            trackBar_2k.Minimum = track_val_min;
            trackBar_2k5.Minimum = track_val_min;
            trackBar_3k15.Minimum = track_val_min;
            trackBar_4k.Minimum = track_val_min;
            trackBar_5k.Minimum = track_val_min;
            trackBar_6k3.Minimum = track_val_min;
            trackBar_8k.Minimum = track_val_min;
            trackBar_10k.Minimum = track_val_min;
            trackBar_12k5.Minimum = track_val_min;
            trackBar_16k.Minimum = track_val_min;
        }

        private void slider_set_btn_Click(object sender, EventArgs e)
        {
            track_db_max = Convert.ToDouble(max_db_tb.Text);
            track_db_min = Convert.ToDouble(min_db_tb.Text);
            track_db_step = Convert.ToDouble(step_db_tb.Text);
            slider_set();
        }

        private void lpf1k_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 14; i++) eq_db[i] = 0;
            for (int i = 14; i < 26; i++) eq_db[i] = track_db_min;
            update_all(1);
        }

        private void calc_btn_Click(object sender, EventArgs e)
        {
            fftpnt_disp = Convert.ToInt32(fftpoint_tb.Text);
            delay = Convert.ToInt32(delay_tb.Text);
            init_buffers();
            update_all(2);
        }
    }
}
