using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace amt
{
    public partial class SPGainForm : Form
    {
        private Amt parent = null;
        private double max_db = 17.75;
        public double current_db = -30.0;
        public int current_val = 0xc0;
        private double range_db = 140.0;

        public SPGainForm(Amt Parent)
        {
            parent = Parent;
            InitializeComponent();

            max_cb.SelectedIndex = 0;
            range_cb.SelectedIndex = 2;
            gain_ctrl_trackBar.Maximum = (int)(range_db * 4.0) + 1;
            gain_ctrl_trackBar.Minimum = 1;
            gain_ctrl_trackBar.Value = Gain2Value();
            max_db_lbl.Text = max_db.ToString() + "dB";
            double min_db = max_db - range_db;
            min_db_lbl.Text = min_db.ToString() + "dB";
            if (parent.has_dampgainch)
            {
                parent.asio.damp_volume_set(0x245);
            }
            set_btn.Enabled = parent.has_dampgainch;
        }

        private void gain_ctrl_trackBar_Scroll(object sender, EventArgs e)
        {
            set_gain();
            parent.asio.spamp_gain_push(current_val);
        }

        private void set_gain()
        {
            int gain_val = gain_ctrl_trackBar.Maximum - gain_ctrl_trackBar.Value + 1;
            current_db = max_db - 0.25 * ((double)gain_val - 1.0);
            current_val = 72 - (int)(current_db * 4.0);

            gain_db_lbl.Text = current_db.ToString("0.00");
        }

        private int Gain2Value()
        {
            double gain = current_db - max_db + range_db;
            int val = (int)(4.0 * gain) + 1;
            if(val < gain_ctrl_trackBar.Minimum) val = gain_ctrl_trackBar.Minimum;
            else if(val > gain_ctrl_trackBar.Maximum) val = gain_ctrl_trackBar.Maximum;
            return val;
        }

        public int set_amp_db(double amp_db)
        {
            current_db = amp_db;
            current_val = 72 - (int)(current_db * 4.0);
            int gain_val = (int)(4.0 * (max_db - current_db)) + 1;
            gain_ctrl_trackBar.Value = gain_ctrl_trackBar.Maximum - gain_val + 1;

            return current_val;
        }

        private int trackBar2Value()
        {
            return gain_ctrl_trackBar.Maximum - gain_ctrl_trackBar.Value + 1;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void mute_cb_CheckedChanged(object sender, EventArgs e)
        {
            int gain_val = 0x245;
            if (mute_cb.Checked)
            {
                gain_db_lbl.Text = "MUTE";
                label1.Visible = false;
            }
            else
            {
                gain_val = current_val;
                gain_db_lbl.Text = current_db.ToString("0.00");
                label1.Visible = true;
            }
            gain_ctrl_trackBar.Enabled = !mute_cb.Checked;
            parent.asio.spamp_gain_push(gain_val);
            if (parent.asio.get_stop() == true) parent.asio.damp_volume_set(gain_val);
        }

        private void max_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ss = max_cb.SelectedItem.ToString();
            max_db_lbl.Text = ss;
            max_db = (float)System.Convert.ToDouble(ss);
            double min_db = max_db - range_db;
            min_db_lbl.Text = min_db.ToString();
            gain_ctrl_trackBar.Value = Gain2Value();
            set_gain();
        }

        private void range_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ss = range_cb.SelectedItem.ToString();
            range_db = (float)System.Convert.ToDouble(ss);
            double min_db = max_db - range_db;
            min_db_lbl.Text = min_db.ToString();
            gain_ctrl_trackBar.Maximum = (int)(range_db * 4.0) + 1;
            gain_ctrl_trackBar.Value = Gain2Value();
            set_gain();
        }

        private void set_btn_Click(object sender, EventArgs e)
        {
            parent.asio.spamp_gain_push(current_val);
            if (parent.asio.get_stop() == true) parent.asio.damp_volume_set(current_val);
        }
    }
}
