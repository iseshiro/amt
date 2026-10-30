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
    public partial class WavFileForm : Form
    {
        private Amt parent = null;

        public WavFileForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            if (point_rb.Checked)
            {
                start_point_tb.Enabled = true;
                end_point_tb.Enabled = true;
                start_time_tb.Enabled = false;
                end_time_tb.Enabled = false;
            }
            if (time_rb.Checked)
            {
                start_point_tb.Enabled = false;
                end_point_tb.Enabled = false;
                start_time_tb.Enabled = true;
                end_time_tb.Enabled = true;
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            parent.wav_full_path = "";
            Dispose();
        }

        private void read_btn_Click(object sender, EventArgs e)
        {
            parent.wav_full_path = full_path_tb.Text;
            parent.wav_read_ch = read_ch_tb.Text;
            if (point_rb.Checked)
            {
                parent.wav_read_point = System.Convert.ToInt32(start_point_tb.Text);
                int end_point = System.Convert.ToInt32(end_point_tb.Text);
                parent.wav_read_length = end_point - parent.wav_read_point + 1;
            }
            else
            {
                double st_time = System.Convert.ToDouble(start_time_tb.Text);
                double ed_time = System.Convert.ToDouble(end_time_tb.Text);
                int start_point = (int)(st_time * parent.sample_freq);
                int end_point = (int)(ed_time * parent.sample_freq);
                parent.wav_read_point = start_point;
                parent.wav_read_length = end_point - start_point + 1;
            }
            parent.wav_continuous = continuous_cb.Checked;
            Dispose();
        }

        private void point_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (point_rb.Checked)
            {
                start_point_tb.Enabled = true;
                end_point_tb.Enabled = true;
                start_time_tb.Enabled = false;
                end_time_tb.Enabled = false;
            }
            if (time_rb.Checked)
            {
                start_point_tb.Enabled = false;
                end_point_tb.Enabled = false;
                start_time_tb.Enabled = true;
                end_time_tb.Enabled = true;
            }
        }

        private void full_path_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Wav read", "*.wav"))
            {
                full_path_tb.Text = parent.fd.filename;
            }
        }
    }
}
