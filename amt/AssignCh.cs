using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace amt
{
    public partial class AssignCh : Form
    {
        private Amt parent = null;

        public AssignCh(Amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            if (parent.assign_in_ch != "")
            {
                in_ch_tb.Text = parent.assign_in_ch;
            }
            else
            {
                in_ch_tb.Text = "1-" + parent.asio.in_ch_cnt.ToString();
            }
            if (parent.assign_out_ch != "")
            {
                out_ch_tb.Text = parent.assign_out_ch;
            }
            else
            {
                out_ch_tb.Text = "1-" + parent.asio.out_ch_cnt.ToString();
            }
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            parent.assign_in_ch = in_ch_tb.Text;
            parent.assign_out_ch = out_ch_tb.Text;
            Dispose();
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            in_ch_tb.Text = "1-" + parent.asio.in_ch_cnt.ToString();
            out_ch_tb.Text = "1-" + parent.asio.out_ch_cnt.ToString();
        }
    }
}
