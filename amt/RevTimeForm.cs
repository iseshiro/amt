using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Asio_io;

namespace amt
{
    public partial class RevTimeForm : Form
    {
        private ChannelInfo select_ch;
        private Amt parent = null;
        private asio_io myasio = null;
        private const int start_time = 0;
        private int end_time = 20;
        private int grid_time = 5;
        private bool selected = false;
        private double smpl_freq;
        private int calc_start_time = 0;
        private int calc_end_time = 20;
        private int db_range = 80;
        private int db_grid = 10;
        private double[] imp_log_data;
        private double[] rev_log_data;
        private double linear_amp = 1.0;

        private int rev_st = -5;
        private int rev_ed = -25;
        private int rev_st_pnt;
        private int rev_ed_pnt;

        private const int PLENG = 1000;

        private const int yohaku_left = 20;
        private const int yohaku_right = 15;
        private const int yohaku_top = 10;
        private const int yohaku_down = 20;

        public RevTimeForm(Amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            myasio = parent.asio;
            smpl_freq = parent.sample_freq;

            grid_time = System.Convert.ToInt32(gr_time_tb.Text);
            end_time = myasio.GetDataLength() * 1000 / (int)smpl_freq;

            calc_end_time = System.Convert.ToInt32(calc_ed_time_tb.Text);
            if (calc_end_time > end_time)
            {
                calc_end_time = end_time;
                calc_ed_time_tb.Text = end_time.ToString();
            }

            imp_log_data = new double[PLENG];
            rev_log_data = new double[PLENG];
            data_no_cb.SelectedIndex = 0;
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            update();
        }

        private double rev_c1 = 0.0;
        private double rev_c2 = 0.0;

        private void update()
        {
            double[] imp_data;
            double[] rev_data;
            int decimation;

            if (!selected) return;

            grid_time = System.Convert.ToInt32(gr_time_tb.Text);
            calc_start_time = System.Convert.ToInt32(calc_st_time_tb.Text);
            calc_end_time = System.Convert.ToInt32(calc_ed_time_tb.Text);
            db_range = System.Convert.ToInt32(db_range_tb.Text);
            db_grid = System.Convert.ToInt32(db_grid_tb.Text);
            rev_st = System.Convert.ToInt32(rev_st_tb.Text);
            rev_ed = System.Convert.ToInt32(rev_ed_tb.Text);
            linear_amp = System.Convert.ToDouble(amp_tb.Text);

            int end_time_pnt = (int)((double)end_time * smpl_freq / 1000.0);
            decimation = end_time_pnt / PLENG;

            imp_data = new double[PLENG];
            rev_data = new double[PLENG];
 
            double sum = 0.0;

            for (int i = 0; i < PLENG; i++)
            {
                double ddd = 0.0;
                for (int j = 0; j < decimation; j++)
                {
                    int pnt = i * decimation + j;
                    double dd = myasio.data(pnt, (int)select_ch.type, select_ch.btn_no);
                    ddd += dd * dd;
                }
                imp_data[i] = ddd;
                if (ddd > 0.0) imp_log_data[i] = 10.0 * Math.Log10(ddd);
                else imp_log_data[i] = -db_range;
                sum += ddd;
            }

            double max_val = get_max(imp_log_data, PLENG);
            for (int i = 0; i < PLENG; i++)
            {
                imp_log_data[i] -= max_val;
                rev_data[i] = sum;
                sum -= imp_data[i];
            }
            double ttt = 10.0 * Math.Log10(rev_data[0]);

            rev_st_pnt = -1;
            rev_ed_pnt = -1;
            for (int i = 0; i < PLENG; i++)
            {
                if(rev_data[i]>0) rev_log_data[i] = 10.0 * Math.Log10(rev_data[i]) - ttt;
                else rev_log_data[i] = -db_range;
                if (rev_log_data[i] < rev_st && rev_st_pnt < 0)
                {
                    rev_st_pnt = i;
                }
                else if (rev_log_data[i] < rev_ed && rev_ed_pnt < 0)
                {
                    rev_ed_pnt = i;
                }
            }
            double rev_scale = 60.0 / (rev_st - rev_ed);
            double rev_time = (double)(rev_ed_pnt - rev_st_pnt) * (double)decimation / (double)smpl_freq * rev_scale;
            rev_time_lbl.Text = "Rev. Time = "+rev_time.ToString("0.000")+" s";

            int calc_start_time_pnt = (int)((double)calc_start_time * smpl_freq / 1000.0 / decimation);
            int calc_end_time_pnt = (int)((double)calc_end_time * smpl_freq / 1000.0 / decimation);
            if (calc_end_time_pnt > PLENG) calc_end_time_pnt = PLENG;
            int calc_leng = calc_end_time_pnt - calc_start_time_pnt;

            double e = get_sum(imp_log_data, calc_start_time_pnt, calc_end_time_pnt);
            double f = get_sum1(imp_log_data, calc_start_time_pnt, calc_end_time_pnt);

            double a = get_sum2(calc_start_time_pnt, calc_end_time_pnt);
            double b = calc_leng + 1;
            double c = get_sum3(calc_start_time_pnt, calc_end_time_pnt);
            double d = a;

            double adbc = 1.0 / (a * d - b * c);
            rev_c1 = adbc * (e * d - b * f);
            rev_c2 = adbc * (a * f - e * c);

            rev_time = -60.0 / rev_c1 / smpl_freq * decimation;
            calc_rev_time.Text = "Rev. Time = " + rev_time.ToString("0.000") + " s";

            wave_pictureBox.Invalidate();
        }

        private double get_max(double[] data, int leng)
        {
            double max_val = data[0];
            for (int i = 0; i < leng; i++)
            {
                if (data[i] > max_val) max_val = data[i];
            }
            return max_val;
        }

        private double get_sum(double[] data, int st, int ed)
        {
            double sum = 0.0;
            for (int i = st; i < ed; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        private double get_sum1(double[] data, int st, int ed)
        {
            double sum = 0.0;
            for (int i = st; i < ed; i++)
            {
                sum += data[i] * (double)i;
            }
            return sum;
        }

        private double get_sum2(int st, int ed)
        {
            double sum = 0.0;
            for (int i = st; i < ed; i++)
            {
                sum += (double)i;
            }
            return sum;
        }

        private double get_sum3(int st, int ed)
        {
            double sum = 0.0;
            for (int i = st; i < ed; i++)
            {
                sum += (double)i * (double)i;
            }
            return sum;
        }

        private void wave_pictureBox_paint(object sender, PaintEventArgs e)
        {
            if (!selected) return;

            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;

            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_down;
            if (log_disp_cb.Checked)
            {
                log_display(e.Graphics, width, height);
            }
            else
            {
                liniear_display(e.Graphics, width, height);
            }
        }

        private void time_axis_display(Graphics g, int width, int height)
        {
            Pen pen = new Pen(Color.Black, 2.0F);   
            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            pen = new Pen(Color.Black, 1.0F);
            pen.DashStyle = DashStyle.Dot;
            int grid_count = end_time / grid_time;
            bool grid_ms = (grid_count <= 10);
            int yyy = yohaku_top + height + 2;
            for (int grid = start_time + grid_time; grid < end_time; grid += grid_time)
            {
                int x1 = width * (grid - start_time) / (end_time - start_time);
                Point[] pnts = { new Point(x1 + yohaku_left, yohaku_top), new Point(x1 + yohaku_left, yohaku_top + height) };
                g.DrawLines(pen, pnts);

                if (grid_ms)
                {
                    g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), x1 + yohaku_left, yyy, format);
                }
                else
                {
                    double gg = (double)grid / 1000.0;
                    g.DrawString(gg.ToString("0.0"), this.Font, new SolidBrush(Color.Black), x1 + yohaku_left, yyy, format);
                }
            }

            format.Alignment = StringAlignment.Near;
            g.DrawString(start_time.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left, yyy, format);
            format.Alignment = StringAlignment.Far;

            //string etime = (grid_ms ? end_time.ToString() : ((double)end_time/1000.0).ToString());
            //g.DrawString(etime, this.Font, new SolidBrush(Color.Black), yohaku_left + width + 10, yyy, format);
            string yaxis = (grid_ms ? "[ms]" : "[s]");
            g.DrawString(yaxis, this.Font, new SolidBrush(Color.Black), yohaku_left + width + 15, yyy, format);
        }

        private void log_display(Graphics g, int width, int height)
        {
            Pen pen = new Pen(Color.Black, 1.0F), pen1 = new Pen(Color.Red, 1.0F);
            int x1, x2, y1, y2, z1 = 0, z2;

            time_axis_display(g, width, height);

            pen.DashStyle = DashStyle.Dot;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            int xxx = yohaku_left;
            for (int grid = -db_grid; grid > -db_range; grid -= db_grid)
            {
                x1 = yohaku_top - height * grid / db_range;
                Point[] pnts = { new Point(yohaku_left, x1), new Point(yohaku_left + width, x1) };
                g.DrawLines(pen, pnts);
                g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), xxx, x1 - this.Font.Height, format);
            }

            g.DrawString("0", this.Font, new SolidBrush(Color.Black), xxx, yohaku_top, format);
            g.DrawString("-" + db_range.ToString(), this.Font, new SolidBrush(Color.Black), xxx, yohaku_top + height - this.Font.Height, format);

            pen = new Pen(Color.Blue, 1.0F);
            pen.DashStyle = DashStyle.Solid;

            double h_scale = (double)height / (double)db_range;
            double w_scale = (double)width / (double)PLENG;

            x1 = yohaku_left;
            if (imp_log_data[0] < -db_range) y1 = yohaku_top + height;
            else y1 = yohaku_top - (int)(h_scale * imp_log_data[0]);

            if (rev_cb.Checked)
            {
                if (rev_log_data[0] < -db_range) z1 = yohaku_top + height;
                else z1 = yohaku_top - (int)(h_scale * rev_log_data[0]);
            }

            for (int i = 1; i < PLENG; i++)
            {
                x2 = (int)(w_scale * (double)i) + yohaku_left;

                if (imp_log_data[i] < -db_range) y2 = yohaku_top + height;
                else y2 = yohaku_top - (int)(h_scale * imp_log_data[i]);

                if (rev_log_data[i] < -db_range) z2 = yohaku_top + height;
                else z2 = yohaku_top - (int)(h_scale * rev_log_data[i]);

                Point[] points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                g.DrawLines(pen, points);
                y1 = y2;

                if (rev_cb.Checked)
                {
                    points = new Point[] { new Point(x1, z1), new Point(x2, z2) };
                    g.DrawLines(pen1, points);
                    z1 = z2;
                }
                x1 = x2;
            }

            if (rev_cb.Checked)
            {
                pen = new Pen(Color.Pink, 2.0F);
                pen.DashStyle = DashStyle.DashDot;

                x1 = (int)(w_scale * (double)rev_st_pnt) + yohaku_left;
                Point[] points = new Point[] { new Point(x1, yohaku_top), new Point(x1, yohaku_top + height) };
                g.DrawLines(pen, points);

                x2 = (int)(w_scale * (double)rev_ed_pnt) + yohaku_left;
                points = new Point[] { new Point(x2, yohaku_top), new Point(x2, yohaku_top + height) };
                g.DrawLines(pen, points);

            }

            if (lms_cb.Checked)
            {
                y1 = yohaku_top - (int)(h_scale * rev_c2);

                double ed_db = rev_c1 * PLENG + rev_c2;
                if (ed_db > -db_range)
                {
                    x2 = yohaku_left + width;
                    y2 = yohaku_top - (int)(h_scale * ed_db);
                }
                else
                {
                    x2 = yohaku_left - (int)((double)width * db_range / ed_db);
                    y2 = yohaku_top + height;
                }
                Point[] pnts = new Point[] { new Point(yohaku_left, y1), new Point(x2, y2) };
                pen = new Pen(Color.ForestGreen, 2.0F);
                pen.DashStyle = DashStyle.DashDot;
                g.DrawLines(pen, pnts);
            }
        }

        private void liniear_display(Graphics g, int width, int height)
        {
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;

            time_axis_display(g, width, height);

            Point[] points = { new Point(yohaku_left, yohaku_top + height / 2), new Point(yohaku_left + width, yohaku_top + height / 2) };
            g.DrawLines(pen, points);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            g.DrawString("0", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height / 2 - this.Font.Height / 2, format);
            g.DrawString("1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top, format);
            g.DrawString("-1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height - this.Font.Height, format);

            pen = new Pen(Color.Blue, 1.0F);
            pen.DashStyle = DashStyle.Solid;

            int start_pnt = parent.Get_time_point(start_time);
            int end_pnt = parent.Get_time_point(end_time);

            if (start_pnt < 0) start_pnt = 0;
            if (end_pnt > myasio.GetDataLength()) end_pnt = myasio.GetDataLength();

            double h_scale = (double)(height / 2) * linear_amp;
            double w_scale = (double)width / (double)(end_pnt - start_pnt);
            if (w_scale > 0.1)
            {
                x1 = yohaku_left;
                y1 = yohaku_top + height / 2 - (int)(myasio.data(start_pnt, (int)select_ch.type, select_ch.btn_no) * h_scale); ;

                for (int i = start_pnt + 1; i < end_pnt; i++)
                {
                    x2 = (int)(w_scale * (double)(i - start_pnt)) + yohaku_left;
                    y2 = yohaku_top + height / 2 - (int)(myasio.data(i, (int)select_ch.type, select_ch.btn_no) * h_scale);

                    points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                    g.DrawLines(pen, points);
                    x1 = x2;
                    y1 = y2;
                }
            }
            else
            {
                int st_pnt, ed_pnt;
                double w_scale_1 = (double)(end_pnt - start_pnt) / (double)width, min, max;
                for (int i = 0; i < width; i++)
                {
                    x1 = yohaku_left + i;
                    st_pnt = (int)((double)i * w_scale_1) + start_pnt;
                    ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_pnt;
                    unsafe
                    {
                        myasio.getminmax(st_pnt, ed_pnt, (int)select_ch.type, select_ch.btn_no, &min, &max);
                    }

                    y1 = yohaku_top + height / 2 - (int)(min * h_scale);
                    y2 = yohaku_top + height / 2 - (int)(max * h_scale);
                    points = new Point[] { new Point(x1, y1), new Point(x1, y2) };
                    g.DrawLines(pen, points);
                }
            }
        }

        private void data_no_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tt = "";
            select_ch = parent.data_ch[data_no_cb.SelectedIndex + 1];
            selected = (data_no_cb.SelectedIndex >= 0);

            if (selected) tt = "Ch. " + select_ch.ToString();
            else tt = "---";
            selected_ch_lbl.Text = tt;

            log_disp_cb.Checked = false;

            update();
        }

        private void log_disp_cb_CheckedChanged(object sender, EventArgs e)
        {
            rev_cb.Enabled = log_disp_cb.Checked;
            rev_cb.Checked = false;
            lms_cb.Checked = false;
            wave_pictureBox.Invalidate();
        }

        private void rev_cb_CheckedChanged(object sender, EventArgs e)
        {
            rev_time_lbl.Visible = rev_cb.Checked;
            rev_st_tb.Enabled = rev_cb.Checked;
            rev_ed_tb.Enabled = rev_cb.Checked;
            lms_cb.Enabled = rev_cb.Checked;
            wave_pictureBox.Invalidate();
        }

        private void lms_cb_CheckedChanged(object sender, EventArgs e)
        {
            calc_rev_time.Visible = lms_cb.Checked;
            calc_st_time_tb.Enabled = lms_cb.Checked;
            calc_ed_time_tb.Enabled = lms_cb.Checked;
            wave_pictureBox.Invalidate();
        }

        private void calc_ed_time_tb_TextChanged(object sender, EventArgs e)
        {
            if (calc_ed_time_tb.Text == "")
            {
                calc_ed_time_tb.Text = end_time.ToString();
            }
            else
            {
                calc_end_time = System.Convert.ToInt32(calc_ed_time_tb.Text);
            }
            if (calc_end_time > end_time)
            {
                calc_end_time = end_time;
                calc_ed_time_tb.Text = end_time.ToString();
            }

        }

        private void save_image_btn_Click(object sender, EventArgs e)
        {
            Bitmap saveImg = new Bitmap(wave_pictureBox.Width, wave_pictureBox.Height);
            Graphics g = Graphics.FromImage(saveImg);

            int width = wave_pictureBox.Width - yohaku_left - yohaku_right;
            int height = wave_pictureBox.Height - yohaku_top - yohaku_down;
            g.FillRectangle(Brushes.White, 0, 0, wave_pictureBox.Width, wave_pictureBox.Height);
            if (log_disp_cb.Checked)
            {
                log_display(g, width, height);
            }
            else
            {
                liniear_display(g, width, height);
            }
            g.Dispose();
            saveImg.Save(@"c:\temp\save_img1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            saveImg.Dispose();
        }
    }
}