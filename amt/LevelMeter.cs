using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace amt
{
    public partial class LevelMeter : Form
    {
		private Amt parent;
        private const double scale = 1.0/32767.0;
        private int yohaku_left = 100;
        private int yohaku_right = 10;
        private int yohaku_top = 5;
        private int yohaku_bottom = 5;
        private int width;
        private int height;
        private double height_per_ch;
        private int height_bar;
        private Rectangle rect1;
        private StringFormat format = new StringFormat();
        private int ch_text_height;
        private double max_db;
        private double min_db;
        private double range_db;
        private SolidBrush sb;
        private SolidBrush sr;
        private double[] max_val;

        public LevelMeter(Amt Parent)
        {
			parent = Parent;
            InitializeComponent();
            init_parameter();
            format.Alignment = StringAlignment.Near;
            max_db = Math.Log10(1.0);
            min_db = Math.Log10(0.001);
            range_db = max_db - min_db;
            sb = new SolidBrush(Color.Black);
            sr = new SolidBrush(Color.Red);
            max_val = new double[parent.in_btn_cnt];

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            if (height_per_ch > 20.0)
            {
                pictureBox1.Size = new System.Drawing.Size(pictureBox1.Size.Width, 20 * parent.in_btn_cnt + yohaku_top + yohaku_bottom);
            }
            else
            {
                int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                pictureBox1.Size = new System.Drawing.Size(pictureBox1.Size.Width, h - 125);
            }
            this.ClientSize = new System.Drawing.Size(ClientSize.Width, pictureBox1.Size.Height + 50);
            close_btn.Location = new System.Drawing.Point(close_btn.Location.X, pictureBox1.Location.Y + pictureBox1.Size.Height + 10);
            reset_max_btn.Location = new System.Drawing.Point(reset_max_btn.Location.X, pictureBox1.Location.Y + pictureBox1.Size.Height + 10);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            init_parameter();
            height_bar = (int)((double)height_per_ch * 0.75);
            reset_max_val();
        }

        private void reset_max_val()
        {
            for (int i = 0; i < parent.in_btn_cnt; i++) max_val[i] = 0;
        }

        public void open_wavemonitor(bool open)
        {
            if (open)
            {
                yohaku_left = 200;
            }
            else
            {
                yohaku_left = 100;
            }
            width = pictureBox1.Size.Width - yohaku_left - yohaku_right;
        }

        private void init_parameter()
        {
            if (parent.show_wavemonitor)
            {
                yohaku_left = 200;
            }
            width = pictureBox1.Size.Width - yohaku_left - yohaku_right;
            height = pictureBox1.Size.Height - yohaku_top - yohaku_bottom;
            height_per_ch = (double)height / (double)parent.in_btn_cnt;
            ch_text_height = (int)height_per_ch / 2 - this.Font.Height / 2;
        }

        public void Redraw()
        {
            pictureBox1.Invalidate();
        }

        private int max_ch_pre = -1;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int max_ch = -1;
            double max_val_all = 0.001;

            for (int ch = 0; ch < parent.in_btn_cnt; ch++)
            {
                ChannelInfo cinfo = parent.ch_info_in[ch];
                double val = parent.asio.GetMaxValClear((int)cinfo.type, cinfo.btn_no);
                if (max_val[ch] < val) max_val[ch] = val;
                if (max_val_all < val)
                {
                    max_val_all = val;
                    max_ch = ch;
                }

                double dval = 0.0;

                if (val > 0)
                {
                    dval = (Math.Log10((double)val) - min_db) / range_db * (double)width;
                }
                string tt = cinfo.btn_no.ToString() + " : " + max_val[ch].ToString("F4") + " : " + val.ToString("F4");
                if (parent.show_wavemonitor)
                {
                    tt += " : " + parent.WaveMonitorInfo(cinfo);
                }
                int yy = yohaku_top + (int)((double)ch * height_per_ch);
                g.DrawString(tt, this.Font, (ch == max_ch_pre ? sr : sb), 10, yy + ch_text_height, format);

                rect1 = new Rectangle(yohaku_left, yy, (int)dval, height_bar);
                g.FillRectangle((ch == max_ch_pre ? Brushes.LightSalmon : Brushes.LightGreen), rect1);
            }
            max_ch_pre = max_ch;
            parent.max_ch = max_ch + 1;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void reset_max_btn_Click(object sender, EventArgs e)
        {
            reset_max_val();
        }
    }
}
