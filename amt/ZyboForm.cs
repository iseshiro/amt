using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace amt
{
    public partial class ZyboForm : Form
    {
        private Amt parent = null;
        TcpClient tcp = null;
        NetworkStream ns = null;
        int word_cnt = 0;
        double amp32_1;
        int[] index_header = new int[10] { 0, 1, 2, 3, 4, 5, 6, 8, 10, 12 };

        public ZyboForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            amp32_1 = parent.asio.get_amp32_1();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void button_ad_satrt_Click(object sender, EventArgs e)
        {
            textBox_message.AppendText("AD Start\n" + textBox_ip_adrs.Text);

            try
            {
                byte[] snd_buf = System.Text.Encoding.ASCII.GetBytes("start");
                byte[] data1 = new byte[64];

                ns.Write(snd_buf, 0, snd_buf.Length);
                ns.Read(data1, 0, data1.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void button_download_Click(object sender, EventArgs e)
        {
            WavStream ws = new WavStream(parent);
            int output_ch_count = 80;
            FileStream[] wav_out_fs = null;
            BinaryWriter[] wav_out_bw = null;
            wav_out_fs = new FileStream[output_ch_count];
            wav_out_bw = new BinaryWriter[output_ch_count];
            for (int och = 0; och < output_ch_count; och++)
            {
                string filename = textBox_path.Text + "\\" + textBox_filename.Text + (och+1).ToString("00") + ".wav";
                wav_out_fs[och] = ws.WavHeaderWrite(filename, word_cnt, true);
                wav_out_bw[och] = new BinaryWriter(wav_out_fs[och]);
            }

            int read_size = 324;
            int write_remain = 0;
            int sector = 204800;
            int sector_intvl = 512;
            int sector_count = 8;
            int CHECK_FIFO_SIZE = sector_intvl * sector_count * 2;
            int chk_rd_ptr = 0;
            int chk_wr_ptr = 0;
            byte[] raw_buf = new byte[324];
            int[] wav_buf = new int[81];
            byte[] check_fifo = new byte[CHECK_FIFO_SIZE];
            byte[] data2 = new byte[8192];
            int check_val;
            for (int ii = 0; ii < word_cnt; ii++)
            {
                while (write_remain < read_size)
                {
                    string cmd = "download " + sector.ToString() + " " + sector_count.ToString();
                    byte[] snd_buf = Encoding.ASCII.GetBytes(cmd);
                    ns.Write(snd_buf, 0, snd_buf.Length);
                    ns.Read(data2, 0, data2.Length);
                    Array.Copy(data2, 0, check_fifo, chk_wr_ptr, sector_intvl * sector_count);
                    sector += sector_count;
                    write_remain += sector_intvl * sector_count;
                    chk_wr_ptr += sector_intvl * sector_count;
                    if ((int)chk_wr_ptr >= CHECK_FIFO_SIZE)
                    {
                        chk_wr_ptr = 0;
                    }
                }

                int next_ptr = chk_rd_ptr + read_size;
                if (next_ptr < CHECK_FIFO_SIZE)
                {
                    Array.Copy(check_fifo, chk_rd_ptr, raw_buf, 0, read_size);
                    chk_rd_ptr = next_ptr;
                }
                else
                {
                    int src_size = CHECK_FIFO_SIZE - chk_rd_ptr;
                    Array.Copy(check_fifo, chk_rd_ptr, raw_buf, 0, src_size);
                    Array.Copy(check_fifo, 0, raw_buf, src_size, read_size - src_size);
                    chk_rd_ptr = read_size - src_size;
                }
                write_remain -= read_size;

                check_val = ii + 1;
                int data_val = BitConverter.ToInt32(raw_buf, 0);
                if (data_val != check_val)
                {
                    textBox_message.AppendText("Invalid Data at word " + ii.ToString() + " header : " + data_val.ToString("x8") + " != " + check_val.ToString("x8") + "\r\n");
                    return;
                }
                for (int jj = 0; jj < output_ch_count; jj++)
                {
                    int header_val = index_header[jj / 8];
                    check_val = (header_val << 19) + ii * 8 + jj % 8 + 1;
                    data_val = BitConverter.ToInt32(raw_buf, (jj + 1) * sizeof(int));
                    if (data_val != check_val)
                    {
                        textBox_message.AppendText("Invalid Data at word " + ii.ToString() + ", ch = " + (jj+1).ToString() + " : " + data_val.ToString("x8") + " != " + check_val.ToString("x8") + "\r\n");
                        return;
                    }
                    float ff = (float)((double)data_val * amp32_1);
                    wav_out_bw[jj].Write(ff);
                }
                label_progress.Text = (ii+1).ToString() + "/" + word_cnt.ToString();
                progressBar_download.Value = ii + 1;
                label_progress.Invalidate();
            }
            for (int och = 0; och < output_ch_count; och++)
            {
                wav_out_bw[och].Close(); ;
            }
            MessageBox.Show("Download", "受信データ表示");

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            if (tcp != null) tcp.Close();
            Dispose(true);
        }


        /// <summary>
        /// ファイル選択
        /// </summary>
        /// <returns>string:選択されたファイルのフルパス キャンセル時はEmpty</returns>
        private void button_save_folder_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "ファイル選択ダイアログ";
            openFileDialog1.Filter = "テキストファイル(*.wav)|*.wav|すべてのファイル(*.*)|*.*";
            openFileDialog1.InitialDirectory = @"C:\temp\";

            //ファイル選択ダイアログを開く
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_path.Text = openFileDialog1.FileName;
            }
        }

        private void button_check_Click(object sender, EventArgs e)
        {
            int port_no = System.Convert.ToInt32(textBox_port_no.Text);
            textBox_message.AppendText("IP Address : " + textBox_ip_adrs.Text + "\n");
            textBox_message.AppendText("Port number : " + textBox_port_no.Text + "\n");
            textBox_message.AppendText("Work folder : " + textBox_path.Text + "\n");
            textBox_message.AppendText("Outpuf file : " + label_filename.Text + "\n");

            try
            {
                if(tcp == null)
                {
                    tcp = new TcpClient(textBox_ip_adrs.Text, port_no);
                    ns = tcp.GetStream();
                }

                byte[] snd_buf = System.Text.Encoding.ASCII.GetBytes("getword");
                byte[] data1 = new byte[64];
                ns.Write(snd_buf, 0, snd_buf.Length);
                ns.Read(data1, 0, data1.Length);
                word_cnt = BitConverter.ToInt32(data1, 0);
                textBox_message.AppendText("Word count : " + word_cnt.ToString());
                progressBar_download.Maximum = word_cnt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox_filename_TextChanged(object sender, EventArgs e)
        {
            label_filename.Text = textBox_filename.Text + "01.wav ... " + textBox_filename.Text + "80.wav";
        }
    }
}
