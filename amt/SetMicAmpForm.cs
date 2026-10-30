using System;
using System.Windows.Forms;
using Asio_io;

namespace amt
{
    public partial class SetMicAmpForm : Form
    {
        private Amt parent = null;
        private asio_io myasio = null;
        public SetMicAmpForm(Amt Parent)
        {
            parent = Parent;
            myasio = parent.asio;
            InitializeComponent();
            madi_no_cb.SelectedIndex = 0;
            board_no_cb.SelectedIndex = 0;
            ch_no_cb.SelectedIndex = 0;
            dc_servo_cb.SelectedIndex = 0;
            cm_servo_cb.SelectedIndex = 1;
            overload_cb.SelectedIndex = 1;
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            int madi_no = madi_no_cb.SelectedIndex;
            int board_no = board_no_cb.SelectedIndex;
            int ch_no = ch_no_cb.SelectedIndex;
            int ctrl_val = dc_servo_cb.SelectedIndex << 2 + (1 - cm_servo_cb.SelectedIndex) << 1 + overload_cb.SelectedIndex;
            int gpo_val = 0;
            if (led1_cb.Checked) gpo_val += 1;
            if (led2_cb.Checked) gpo_val += 2;
            if (led3_cb.Checked) gpo_val += 4;
            if (led4_cb.Checked) gpo_val += 8;
            int gain_val = Int32.Parse(gain_tb.Text);
            myasio.micamp_gain_set(madi_no, board_no, ch_no, ctrl_val, gpo_val, gain_val);
        }
    }
}
