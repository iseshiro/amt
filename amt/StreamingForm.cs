using System;
using System.Windows.Forms;
using System.IO;
using Asio_io;
using static amt.Amt;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace amt
{
    public partial class StreamingForm : Form
    {
        private Amt parent = null;
        private asio_io myasio = null;
        private int start_ch;
        private int end_ch;
        private string selected_folder = "";
        private bool file_checked_ok = false;
        private int out_ch_base;
        private string[] stream_files;
        private double amp = 1.0;
        private bool audio_in_filestream_out = true;

        public StreamingForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            start_ch = 1;
            start_tb.Text = start_ch.ToString();
            end_ch = parent.in_btn_cnt;
            end_tb.Text = end_ch.ToString();
            out_ch_base = parent.in_btn_cnt;

            stream_files = new string[end_ch + 1];
            set_length();

            amp_adjust_tb.Text = amp.ToString();

            audio_input_rb.Checked = audio_in_filestream_out;
            audio_output_rb.Checked = !audio_in_filestream_out;

            selected_folder = parent.work_folder;
            work_folder_lbl.Text = "Work folder : "+selected_folder;
        }

        private void check_btn_Click(object sender, EventArgs e)
        {
            if (selected_folder == "") return;
            bool status_ok = true;
            file_checked_ok = true;
            for (int ch = start_ch; ch <= end_ch; ch++)
            {
                string ss = String.Format(filename_tb.Text, ch);
                string full_path = selected_folder + "\\" + ss;
                bool file_exists = FileProcess.Exists(full_path);
                
                parent.Cnsl_msg(ch.ToString() + " : " + full_path);
                if (audio_in_filestream_out)
                {
                    status_ok = !file_exists;
                }
                else
                {
                    status_ok = file_exists;
                }
                parent.Cnsl_msg((status_ok?" : OK\n" :" : NG\n"));
                if (status_ok)
                {
                    stream_files[ch] = full_path;
                }
                else
                {
                    file_checked_ok = false;
                }
            }
            parent.Cnsl_msg("Check result: " + (file_checked_ok ? " : Good\n" : "Not Good\n"));
            set_btn.Enabled = file_checked_ok;
        }

        private void output_rb_CheckedChanged(object sender, EventArgs e)
        {
            end_ch = parent.out_btn_cnt;
            end_tb.Text = end_ch.ToString();

            audio_in_filestream_out = audio_input_rb.Checked;
            filestream_lbl.Text = (audio_in_filestream_out ? "Audio in ---> File write" : "File read ---> Audio out");
        }

        private void start_tb_TextChanged(object sender, EventArgs e)
        {
            if (start_tb.Text != "")
            {
                start_ch = System.Convert.ToInt32(start_tb.Text);
            }
        }

        private void end_tb_TextChanged(object sender, EventArgs e)
        {
            if (end_tb.Text != "")
            {
                end_ch = System.Convert.ToInt32(end_tb.Text);
            }
        }

        private void set_imp_folder_btn_Click(object sender, EventArgs e)
        {
            if (audio_in_filestream_out)
            {
                if (parent.Set_work_folder())
                {
                    selected_folder = parent.work_folder;
                    work_folder_lbl.Text = "Work folder : " + selected_folder;
                }
            }
            else
            {		
                System.Windows.Forms.OpenFileDialog openFileDialog1;
                openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

                //openFileDialog1.InitialDirectory = parent.work_folder;
                openFileDialog1.Filter = "wav files (*.wav)|*.wav|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                //openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    selected_folder = Path.GetDirectoryName(openFileDialog1.FileName);
                    work_folder_lbl.Text = "Work folder : " + selected_folder;
                    parent.Cnsl_msg("Work folder = " + selected_folder + "\n");
                    parent.work_folder = selected_folder;
                    string filename = Path.GetFileName(openFileDialog1.FileName);
                    string ss0 = String.Format(filename_tb.Text, 1);
                    if (filename.IndexOf("001") > 0)
                    {
                        filename_tb.Text = filename.Replace("01", "{0:D3}");
                    }
                    else if (filename.IndexOf("01") > 0)
                    {
                        filename_tb.Text = filename.Replace("01", "{0:D2}");
                    }
                    else if(filename.IndexOf("1") > 0)
                    {
                        filename_tb.Text = filename.Replace("01", "{0:D}");
                    }
                }
            }
        }

        private void set_btn_Click(object sender, EventArgs e)
        {
            if (audio_input_rb.Checked)
            {
                for (int ch = start_ch; ch <= end_ch; ch++)
                {
                    parent.DataCreate(parent.ch_info_in[ch - 1], ChannelInfo.StreamType.asioin_wavwrite);
                    //parent.Exec_stream_wav_write(parent.ch_info_in[ch - 1]);
                }
            }
            else
            {
                myasio.push_asio_mode((int)ASIOmode.asiomode_stream_out);
                int min_leng = 0;
                for (int ch = start_ch; ch <= end_ch; ch++)
                {
                    parent.DataCreate(parent.ch_info_out[ch - 1], ChannelInfo.StreamType.wavread_asioout);
                    string filename = stream_files[ch];
                    int length = 0;
                    unsafe
                    {
                        fixed (Char* ss = filename.ToCharArray())
                        {
                            length = parent.asio.stream_open_mch_out(ss, 1, ch);
                        }
                    }
                    if (length < 0)
                    {
                        parent.Cnsl_msg("Error not exist : File Name (" + filename + ")\r\n");
                        break;
                    }
                    if (ch == start_ch)
                    {
                        min_leng = length;
                    }
                    else
                    {
                        if (min_leng > length)
                        {
                            min_leng = length;
                        }
                    }
                    parent.stream_word_count = min_leng;
                    parent.asio.stream_set_leng(start_ch, end_ch, min_leng);
                }
            }
            parent.myProperty.Save();
            Close();
        }

        private void set_length()
        {
            int dleng = myasio.GetDataLength();
            dleng_lbl.Text = dleng.ToString();
            set_btn.Enabled = file_checked_ok;
        }

        private void amp_adjust_cb_CheckedChanged(object sender, EventArgs e)
        {
            amp_adjust_lbl.Enabled = amp_adjust_tb.Enabled = amp_adjust_cb.Checked;
        }

        private void amp_adjust_tb_TextChanged(object sender, EventArgs e)
        {
            if (amp_adjust_tb.Text != "")
            {
                amp = Convert.ToDouble(amp_adjust_tb.Text);
                amp_adj_err_lbl.Visible = (amp > 0.0 && amp < 10);
            }
        }

        private void net_trig_cb_CheckedChanged(object sender, EventArgs e)
        {
            parent.net_trig_start = net_trig_cb.Checked;
            if(net_trig_cb.Checked)
            {
                parent.NetServerStart();
            }
            else
            {
                parent.NetServerStop();
            }
        }
    }
}
