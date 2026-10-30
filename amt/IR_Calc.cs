using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;


namespace amt
{
    public partial class IR_Calc : Form
    {
        private Amt parent = null;
        private int start_ch;
        private int end_ch;
        private ArrayList cali_data;
        private double[] lin_val;

        public IR_Calc(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            start_ch = Convert.ToInt32(start_ch_tb.Text);
            end_ch = Convert.ToInt32(end_ch_tb.Text);
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Load", "*.txt") == false)
            {
                parent.Cnsl_msg("Canceled.\r\n");
                return;
            }
            StreamReader sr = new StreamReader(parent.fd.filename);
            string line;

            cali_data = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                string[] stArrayData = line.Split(' ');
                char[] MyChar = { 'd', 'B' };
                string val_s = stArrayData[4].TrimEnd(MyChar);
                cali_data.Add(val_s);
            }
            copy_cali_data();
        }

        private void copy_cali_data()
        {
            int i = 0;
            cali_val_tb.Text = "";
            lin_val = new double[end_ch];

            foreach (string val_s in cali_data)
            {
                if (i >= start_ch-1 && i < end_ch)
                {
                    cali_val_tb.AppendText(val_s + "\n");
                    double log_val = Convert.ToDouble(val_s);
                    lin_val[i] = Math.Pow(10.0, log_val / 20.0);
                }
                else
                {
                    lin_val[i] = 0.0;
                }
                i++;
            }
        }

        private void end_ch_tb_TextChanged(object sender, EventArgs e)
        {
            if (end_ch_tb.Text != "")
            {
                end_ch = System.Convert.ToInt32(end_ch_tb.Text);
                copy_cali_data();
            }
        }

        private void start_ch_tb_TextChanged(object sender, EventArgs e)
        {
            if (start_ch_tb.Text != "")
            {
                start_ch = System.Convert.ToInt32(start_ch_tb.Text);
                copy_cali_data();
            }
        }

        private void source_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Source", "*.wav"))
            {
                source_fullpath_lbl.Text = parent.fd.filename;
            }
        }
    }
}
