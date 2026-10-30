using Asio_io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace amt
{
    public partial class WavConvert : Form
    {
        private Amt parent = null;
        private string resample_in_file = "";
        private string resample_out_file = "";
        private string l_filename = "";
        private string r_filename = "";
        private string lr_filename = "";
        private asio_io myasio = null;
        private int src_sfreq = 44100;
        private int dst_sfreq = 48000;

        public WavConvert(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            WavStream ws = new WavStream(parent);
            if (ch2to1x2_rb.Checked)
            {
                ws.Wav2chto1ch(lr_filename, l_filename, r_filename);
            }
            else
            {
                ws.Wav1chto2ch(l_filename, r_filename, lr_filename);
            }
        }

        private string get_filename(bool open)
        {
            string filename = "";

            ch1x2to2_rb.Enabled = false;
            ch2to1x2_rb.Enabled = false;

            if (open)
            {
                if (parent.fd.OpenFileDialog("Convert wav file", "*.wav"))
                {
                    filename = parent.fd.filename;
                }
            }
            else
            {
                if (parent.fd.SaveFileDialog("Convert wav file", "wav"))
                {
                    filename = parent.fd.filename;
                }
            }

            return filename;
        }

        private void l_file_btn_Click(object sender, EventArgs e)
        {
            l_filename = get_filename(ch1x2to2_rb.Checked);
            toolStripStatusLabel1.Text = "File Name (L ch)" + l_filename;
        }

        private void r_file_btn_Click(object sender, EventArgs e)
        {
            r_filename = get_filename(ch1x2to2_rb.Checked);
            toolStripStatusLabel1.Text = "File Name (R ch)" + r_filename;
        }

        private void lr_file_btn_Click(object sender, EventArgs e)
        {
            lr_filename = get_filename(ch2to1x2_rb.Checked);
            toolStripStatusLabel1.Text = "File Name (LR ch)" + lr_filename;
        }

        private void check_btn_Click(object sender, EventArgs e)
        {
            bool ok_l, ok_r, ok_lr;
            ok_l = FileProcess.Exists(l_filename);
            ok_r = FileProcess.Exists(r_filename);
            ok_lr = FileProcess.Exists(lr_filename);
            if (ch1x2to2_rb.Checked)
            {
                if (!ok_l && l_filename != "") toolStripStatusLabel1.Text = "Error: Lch Filename: ";
                if (!ok_r && r_filename != "") toolStripStatusLabel1.Text = "Error: Rch Filename: ";
                if (ok_lr) toolStripStatusLabel1.Text = "Error: LRch File exist";
            }
            else
            {
                if (!ok_lr) toolStripStatusLabel1.Text = "Error: LRch Filename: ";
                if (ok_l) toolStripStatusLabel1.Text = "Error: Lch File exist";
                if (ok_r) toolStripStatusLabel1.Text = "Error: Rch File exist";
            }
            if (toolStripStatusLabel1.Text == "")
            {
                toolStripStatusLabel1.Text = "Check OK";
                start_btn.Enabled = true;
            }
        }

        private void set_wav_file_btn_Click(object sender, EventArgs e)
        {
            resample_start_btn.Enabled = false;
            bool succeed = true;
            if (parent.fd.OpenFileDialog("Source wav file (44.1kHz or 48kHz)", "*.wav"))
            {
                resample_in_file = parent.fd.filename;
                WavStream ws = new WavStream(parent);
                if (ws.WavHeaderCheck(resample_in_file))
                {
                    string name = System.IO.Path.GetFileName(resample_in_file);
                    int word_count = ws.WordCount();
                    int fs = ws.SampleFrequency();
                    int ch = ws.Channel();
                    string status = "Error:";
                    if (fs == 44100 || fs == 48000)
                    {
                        status = "OK:" + name + ", Word count = " + word_count.ToString() + ", Sample Freq = " + fs.ToString() + "Hz, Ch = " + ch.ToString();
                        if(fs == 44100)
                        {
                            resample_start_btn.Text = "Convert to fs=48kHz";
                            src_sfreq = 44100;
                            dst_sfreq = 48000;
                        }
                        else
                        {
                            resample_start_btn.Text = "Convert to fs=44.1kHz";
                            src_sfreq = 48000;
                            dst_sfreq = 44100;
                        }
                    }
                    else
                    {
                        status = "Error: Invalid sample freq : " + fs.ToString() + "Hz";
                        succeed = false;
                    }
                    toolStripStatusLabel1.Text = status;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Error: Wav Header Check";
                    succeed = false;
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Error: Open File Dialog";
                succeed = false;
            }
            if(succeed)
            {
                if (parent.fd.SaveFileDialog("Output wav file", "wav"))
                {
                    resample_out_file = parent.fd.filename;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Error: Save File Dialog";
                    succeed = false;
                }
            }
            resample_start_btn.Enabled = succeed;
        }

        private void resample_start_btn_Click(object sender, EventArgs e)
        {
            int word_count = 0;
            unsafe
            {
                fixed (Char* s1 = resample_in_file.ToCharArray(), s2 = resample_out_file.ToCharArray())
                {
                    word_count = myasio.resample(s1, s2, src_sfreq, dst_sfreq, use_sinc_func_cb.Checked);
                }
            }
            toolStripStatusLabel1.Text = "Resample complete : word count = " + word_count.ToString();
        }
    }
}