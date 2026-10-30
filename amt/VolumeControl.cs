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
    public partial class VolumeControl : Form
    {
        private amt parent = null;
        private int asio_vol_ch;

        public VolumeControl(amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            asio_vol_ch = parent.asio.in_ch_cnt + 64;
            if (parent.asio.out_ch_cnt < 64)
            {
                buf_set_btn.Enabled = false;
                vol_set_btn.Enabled = false;
            }
            else if (parent.asio.have_buffer(asio_vol_ch))
            {
                buf_set_btn.Enabled = false;
                vol_set_btn.Enabled = true;
            }
            else
            {
                buf_set_btn.Enabled = true;
                vol_set_btn.Enabled = false;
            }
        }

        private void vol_ctrl_tb_Scroll(object sender, EventArgs e)
        {
            vol_val_tb.Text = vol_ctrl_tb.Value.ToString();
        }

        private void vol_set_btn_Click(object sender, EventArgs e)
        {
            int amp_db = System.Convert.ToInt32(vol_val_tb.Text);
            Damp_vol_lbl.Text = "Damp vol = " + vol_val_tb.Text + " dB";
            vol_ctrl_tb.Value = amp_db;
            double amp = Math.Pow(10, (double)amp_db*0.05);
            parent.asio.change_amp(asio_vol_ch, amp);
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buf_set_btn_Click(object sender, EventArgs e)
        {
            if (parent.DataCreate(parent.ch_info_vol))
            {
                parent.asio.makeDC(1.0, asio_vol_ch);
                vol_set_btn.Enabled = true;
            }
        }
    }
}
