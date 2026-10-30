using System;
using System.Windows.Forms;
using System.IO;
using Asio_io;

namespace amt
{
    public partial class SaveForm : Form
    {
        private Amt parent;
        private asio_io myasio;
        private int input_start = 1;
        private int input_end;
        private int folder_no = 1;

        public SaveForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;

            input_end = parent.in_btn_cnt;
            input_start_tb.Text = input_start.ToString();
            input_end_tb.Text = input_end.ToString();
            folder_no_tb.Text = folder_no.ToString();

            status_text_lbl.Text = "Work folder : " + parent.work_folder;

            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }

        private void MakeFolder_In(int folder_no)
        {
            string folder_name = String.Format(foldername_rule_tb.Text, folder_no);
            string full_path = parent.work_folder + @"\" + folder_name;
            if (!Directory.Exists(full_path))
            {
                Directory.CreateDirectory(full_path);
            }
        }

        private string SetFullPath1(int folder_no, string rule, int ch_st, int ch_ed)
        {
            string folder_name = String.Format(foldername_rule_tb.Text, folder_no);
            string file_name_st = String.Format(rule, ch_st);
            string file_name_ed = String.Format(rule, ch_ed);
            return parent.work_folder + @"\" + folder_name + @"\[" + file_name_st + "-" + file_name_ed + "]";
        }

        private string SetFullPath(int folder_no, string rule, int ch_no)
        {
            string folder_name = String.Format(foldername_rule_tb.Text, folder_no);
            string file_name = String.Format(rule, ch_no);
            return parent.work_folder + @"\" + folder_name + @"\" + file_name;
        }

        private void check_btn_Click(object sender, EventArgs e)
        {
            int error = 0;
            int folder_no = System.Convert.ToInt32(folder_no_tb.Text);
            MakeFolder_In(folder_no);
            for (int i = input_start; i <= input_end; i++)
            {
                ChannelInfo cinfo = parent.ch_info_in[i];
                if (!myasio.have_buffer((int)cinfo.type, cinfo.btn_no))
                {
                    parent.Cnsl_msg("Error: No buffer: Input Ch." + i.ToString() + "\n");
                    error++;
                }
                string fullpath = SetFullPath(folder_no, input_filename_rule_tb.Text, i);
                if (FileProcess.Exists(fullpath))
                {
                    parent.Cnsl_msg("Error: File Exist: " + fullpath + "\n");
                    error++;
                }
            }
            if (error > 0)
            {
                status_text_lbl.Text = "Error : count = " + error.ToString();
            }
            else
            {
                status_text_lbl.Text = "Check OK";
                save_btn.Enabled = true;
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            int folder_no = System.Convert.ToInt32(folder_no_tb.Text);
            for (int i = input_start; i <= input_end; i++)
            {
                string fullpath = SetFullPath(folder_no, input_filename_rule_tb.Text, i);
                status_text_lbl.Text = "Saving : " + fullpath;
                statusStrip1.Update();

                ChannelInfo cinfo = parent.ch_info_in[i];
                parent.WavExport(fullpath, cinfo, true);
            }
            status_text_lbl.Text = "Save done";
            save_btn.Enabled = false;
            folder_no++;
            folder_no_tb.Text = folder_no.ToString();
        }

        private void folder_no_tb_TextChanged(object sender, EventArgs e)
        {
            folder_no = System.Convert.ToInt32(folder_no_tb.Text);
            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }

        private void input_start_tb_TextChanged(object sender, EventArgs e)
        {
            if (input_start_tb.Text == "") return;
            input_start = System.Convert.ToInt32(input_start_tb.Text);
            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }

        private void input_end_tb_TextChanged(object sender, EventArgs e)
        {
            if (input_end_tb.Text == "") return;
            input_end = System.Convert.ToInt32(input_end_tb.Text);
            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }

        private void input_filename_rule_tb_TextChanged(object sender, EventArgs e)
        {
            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }

        private void foldername_rule_tb_TextChanged(object sender, EventArgs e)
        {
            input_fullpath_last_lbl.Text = SetFullPath1(folder_no, input_filename_rule_tb.Text, input_start, input_end);
        }
    }
}
