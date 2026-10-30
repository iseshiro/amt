using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace amt
{
    public partial class MakeInv : Form
    {
        private Amt parent = null;
        private int sp_count;
        private int mic_count;
        private int imp_leng;
        private int fftpoint;
        private int scut_count;
        private string selected_folder = "";
        private string inv_folder_name;

        public MakeInv(Amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            sp_count = System.Convert.ToInt32(sp_count_tb.Text);
            mic_count = System.Convert.ToInt32(mic_count_tb.Text);
            imp_leng = System.Convert.ToInt32(imp_leng_tb.Text);
            fftpoint = System.Convert.ToInt32(fftpoint_tb.Text);
            scut_count = System.Convert.ToInt32(scut_count_tb.Text);
        }

        private void set_work_folder_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Make inv work folder", "*.wav") == false)
            {
                string filename11 = parent.fd.filename;
                folder_name_lbl.Text = System.IO.Path.GetDirectoryName(filename11);
                file_name_tb.Text = System.IO.Path.GetFileName(filename11);
            }
        }

        private void calc_start_btn_Click(object sender, EventArgs e)
        {
            WavStream ws = new WavStream(parent);
            parent.asio.makeinv_init(sp_count, mic_count, fftpoint);

            for (int i = 1; i <= sp_count; i++)
            {
                for (int j = 1; j <= mic_count; j++)
                {
                    String full_path_imp = selected_folder + "\\imp\\imp_s" + i.ToString() + "_m" + j.ToString() + ".wav";
                    ws.WavReadInv(full_path_imp, imp_leng, i,j);
                }
            }
            parent.asio.makeinv_calc();

        }

        private void check_folder_btn_Click(object sender, EventArgs e)
        {
            int file_not_exists = 0;
            for (int i = 1; i <= sp_count; i++)
            {
                for (int j = 1; j <= mic_count; j++)
                {
                    String full_path_imp = selected_folder + "\\imp\\imp_s" + i.ToString() + "_m" + j.ToString() + ".wav";
                    if (!FileProcess.Exists(full_path_imp))
                    {
                        file_not_exists++;
                    }
                }
            }
            if (file_not_exists == 0)
            {
                inv_system_status.Text = "work folder(" + selected_folder + ") is valid.";
                inv_folder_name = selected_folder + "\\inv";
                calc_start_btn.Enabled = true;
            }
            else
            {
                inv_system_status.Text = file_not_exists.ToString() + " files do not exist.";
            }
        }
    }
}
