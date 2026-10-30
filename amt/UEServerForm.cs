using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace amt
{
    public partial class UEServerForm : Form
    {
        private Amt parent = null;
        private int frame_size = 2048;
        private UEServer ue_server = null;
        private int rtmove_mic_cnt = 0;
        private int num_of_input_ch;
        private bool server_start = false;

        private int selectedRowIndex = -1;
        private int source_count = 0;
        private bool inbuf_created = false;
        private bool outbuf_created = false;
        private int level_mode = 0;
        private int level_ch = 0;
        private bool level_is_input = true;

        private List<WavFileInfo> wavFileInfos = new List<WavFileInfo>();

        public UEServerForm(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;

            //server_src_tb.Text = System.Environment.GetEnvironmentVariable("BOSC_SRC");

            if (parent.out_btn_cnt >= 2)
            {
                mic_posi_cb.Items.Add("Binaural (2ch)");
                mic_posi_cb.SelectedIndex = 0;
                rtmove_mic_cnt = 2;
            }
            if (parent.out_btn_cnt >= 80)
            {
                mic_posi_cb.Items.Add("Fullerene (80ch)");
                mic_posi_cb.SelectedIndex = 1;
                rtmove_mic_cnt = 80;
            }
            if (parent.out_btn_cnt >= 96)
            {
                mic_posi_cb.Items.Add("Sound Cask (96ch)");
                mic_posi_cb.SelectedIndex = 2;
                rtmove_mic_cnt = 96;
            }
            for (int ii = 0; ii <= parent.in_btn_cnt; ii++)
            {
                max_input_ch_cb.Items.Add(ii);
                max_input_ch_cb.SelectedIndex = 0;
                num_of_input_ch = 0;
            }
            inout_cb.SelectedIndex = 0;
        }

        private void set_server_src_btn_Click(object sender, EventArgs e)
        {
            if (parent.Set_work_folder())
            {
                server_src_tb.Text = parent.work_folder;
                //vr_src.src_folder = server_src_tb.Text;
                GetWavFileList();

                int row = 0;
                source_count = wavFileInfos.Count;
                for (int wav_no = 0; wav_no < wavFileInfos.Count; wav_no++)
                {
                    int col = 0;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[col++].Value = true;
                    dataGridView1.Rows[row].Cells[col++].Value = (wav_no + 1).ToString();
                    dataGridView1.Rows[row].Cells[col++].Value = wavFileInfos[wav_no].name;
                    dataGridView1.Rows[row].Cells[col++].Value = wavFileInfos[wav_no].ch.ToString();
                    string wav_fs = wavFileInfos[wav_no].fs.ToString() + "Hz";
                    if (wavFileInfos[wav_no].fs == 48000)
                    {
                        wav_fs = "48kHz";
                    }
                    else if (wavFileInfos[wav_no].fs == 44100)
                    {
                        wav_fs = "44.1kHz";
                    }
                    dataGridView1.Rows[row].Cells[col++].Value = wav_fs;
                    dataGridView1.Rows[row].Cells[col++].Value = wavFileInfos[wav_no].endtime.ToString("F2") + " s";
                    dataGridView1.Rows[row].Cells[col++].Value = wavFileInfos[wav_no].wordcount.ToString();
                    row++;
                }
                server_start_btn.Enabled = (source_count > 0);
            }
        }

        private void server_start_btn_Click(object sender, EventArgs e)
        {
            server_src_tb.Enabled = false;
            mic_posi_cb.Enabled = false;
            if (server_start)
            {
                ue_server.Server_stop();
                server_start = false;
                server_start_btn.Text = "Server Start";
            }
            else
            {
                if (ue_server == null)
                {
                    string[] srcList = new string[source_count];
                    for (int wav_no = 0; wav_no < wavFileInfos.Count; wav_no++)
                    {
                        int src_no = wavFileInfos[wav_no].src_no;
                        srcList[src_no - 1] = wavFileInfos[wav_no].name;
                    }

                    ue_server = new UEServer(parent, srcList, console_msg_tb, num_of_input_ch);
                    ue_server.SetupLevelMeter(meter_val_pb, level_mode, level_is_input, level_ch);
                }

                server_start = ue_server.Server_start(mic_posi_cb.SelectedIndex, frame_size, show_msg_cb.Checked);
                server_start_btn.Text = "Server Stop";
            }
        }

        private void check_source_status()
        {
            if (rtmove_mic_cnt > 0 && inbuf_created && outbuf_created)
                server_start_btn.Enabled = true;
            else
                server_start_btn.Enabled = false;
        }

        private void set_mic_posi_btn_Click(object sender, EventArgs e)
        {
            bool succeed = false;
            for (int ch = 0; ch < rtmove_mic_cnt; ch++)
            {
                succeed = parent.OutBufCreate(parent.ch_info_out[ch], ChannelInfo.StreamType.asioout);
                if (succeed)
                    console_msg_tb.AppendText("OutBuf Ch." + (ch + 1).ToString() + " created\n");
                else
                    console_msg_tb.AppendText("OutBuf Ch." + (ch + 1).ToString() + " not created\n");
            }
            outbuf_created = true;
            set_server_src_btn.Enabled = true;
            check_source_status();
        }

        private void mic_posi_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mic_posi_cb.SelectedIndex == 0) // Binaural (2ch)
            {
                rtmove_mic_cnt = 2;
            }
            else if (mic_posi_cb.SelectedIndex == 1) // Fullerene (80ch)
            {
                rtmove_mic_cnt = 80;
            }
            else if (mic_posi_cb.SelectedIndex == 2) // Sound Cask (96ch)
            {
                rtmove_mic_cnt = 96;
            }
            check_source_status();
        }

        private void update_table()
        {

            source_count = 0;
            dataGridView1.ClearSelection();
            for (int row_idx1 = 0; row_idx1 < dataGridView1.Rows.Count; row_idx1++)
            {
                bool use = (bool)dataGridView1.Rows[row_idx1].Cells[0].EditedFormattedValue;

                //Color f_col = Color.Black;
                Color b_col = Color.White;
                string name = (string)dataGridView1.Rows[row_idx1].Cells[2].Value;
                if (use)
                {
                    source_count++;
                    dataGridView1.Rows[row_idx1].Cells[1].Value = source_count.ToString();
                    SetSourceNo(name, source_count);
                    if (row_idx1 == selectedRowIndex) b_col = Color.LightSeaGreen;
                }
                else
                {
                    dataGridView1.Rows[row_idx1].Cells[1].Value = "";
                    //f_col = Color.Gray;
                    b_col = Color.Gray;
                    SetSourceNo(name, 0);
                }
                for (int clmn_idx = 1; clmn_idx < dataGridView1.Columns.Count; clmn_idx++)
                {
                    //dataGridView1[clmn_idx, row_idx1].Style.ForeColor = f_col;
                    dataGridView1[clmn_idx, row_idx1].Style.BackColor = b_col;
                }
            }
            server_start_btn.Enabled = (source_count > 0);
            console_msg_tb.AppendText("source_count : " + source_count.ToString() + "\n");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row_idx = e.RowIndex;
            int col_idx = e.ColumnIndex;
            if (e.RowIndex >= 0)
            {
                if (col_idx == 0)
                {
                    bool use = (bool)dataGridView1.Rows[row_idx].Cells[col_idx].EditedFormattedValue;
                    //console_msg_tb.AppendText("CellContentClick : Row = " + row_idx.ToString() + ", Use = " + use.ToString() + "\n");
                    DataGridViewRow rowToMove = dataGridView1.Rows[row_idx];
                    dataGridView1.Rows.RemoveAt(row_idx);
                    int new_idx = dataGridView1.Rows.Count;
                    if (use)
                    {
                        new_idx = 0;
                    }
                    dataGridView1.Rows.Insert(new_idx, rowToMove);
                    dataGridView1.Rows[new_idx].Cells[col_idx].Value = use;
                    selectedRowIndex = -1;
                    up_btn.Visible = false;
                    down_btn.Visible = false;
                }
                else
                {
                    selectedRowIndex = row_idx;
                    up_btn.Visible = (row_idx > 0 && row_idx < source_count);
                    down_btn.Visible = (row_idx >= 0 && row_idx < (source_count - 1));
                }
                update_table();
            }
        }
        private void SetSourceNo(string name, int src_no)
        {
            for (int wav_no = 0; wav_no < wavFileInfos.Count; wav_no++)
            {
                if (name == wavFileInfos[wav_no].name)
                {
                    wavFileInfos[wav_no].src_no = src_no;
                    //console_msg_tb.AppendText("SetSourceNo : " + wav_no.ToString() + wavFileInfos[wav_no].name + ": " + ", src_no = " + src_no.ToString() + "\n");
                }
            }
        }
        private void GetWavFileList()
        {
            string src_folder = parent.work_folder;
            DirectoryInfo dir = new DirectoryInfo(src_folder);
            FileInfo[] song_files = dir.GetFiles("*.wav");

            int src_no = 1;
            for (int ii = 0; ii < song_files.Length; ii++)
            {
                if (song_files[ii].Name[0] != '.')
                {
                    WavStream ws = new WavStream(parent);
                    if (ws.WavHeaderCheck(song_files[ii].FullName))
                    {
                        string name = System.IO.Path.GetFileNameWithoutExtension(song_files[ii].FullName);
                        int word_count = ws.WordCount();
                        int fs = ws.SampleFrequency();
                        int ch = ws.Channel();
                        wavFileInfos.Add(new WavFileInfo(src_no++, name, ch, fs, word_count));
                    }
                }
            }
        }

        private void up_btn_Click(object sender, EventArgs e)
        {
            DataGridViewRow rowToMove = dataGridView1.Rows[selectedRowIndex];
            dataGridView1.Rows.RemoveAt(selectedRowIndex);
            selectedRowIndex--;
            dataGridView1.Rows.Insert(selectedRowIndex, rowToMove);
            update_table();
        }

        private void down_btn_Click(object sender, EventArgs e)
        {
            DataGridViewRow rowToMove = dataGridView1.Rows[selectedRowIndex];
            dataGridView1.Rows.RemoveAt(selectedRowIndex);
            selectedRowIndex++;
            dataGridView1.Rows.Insert(selectedRowIndex, rowToMove);
            update_table();
        }

        private void set_partnerCount_btn_Click(object sender, EventArgs e)
        {
            bool succeed = false;
            for (int ch = 0; ch < num_of_input_ch; ch++)
            {
                succeed = parent.InBufCreate(parent.ch_info_in[ch], ChannelInfo.StreamType.asioin);
                if (succeed)
                    console_msg_tb.AppendText("InBuf Ch." + (ch + 1).ToString() + " created\n");
                else
                    console_msg_tb.AppendText("InBuf Ch." + (ch + 1).ToString() + " not created\n");
            }

            inbuf_created = true;
            check_source_status();
        }

        private void max_input_ch_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            num_of_input_ch = max_input_ch_cb.SelectedIndex;
        }

        private void level_meter_set_btn_Click(object sender, EventArgs e)
        {
            string inout_mode = inout_cb.SelectedItem.ToString();
            if (inout_cb.SelectedItem != null && inout_mode == "Input")
            {
                level_is_input = true;  // Display level of input ch,
            }
            else
            {
                level_is_input = false;  // Display level of output ch,
            }
            string level_mode_s = "None";
            if (rms_rb.Checked)
            {
                level_mode = 2; // RMS
                level_mode_s = "RMS";
            }
            else
            {
                level_mode = 1; // Peak
                level_mode_s = "Peak";
            }
            int ch_no = Convert.ToInt32(ch_no_tb.Text);
            console_msg_tb.AppendText("Set Level Meter: " + level_mode_s + ", " + inout_mode + ", Ch." + ch_no.ToString() + "\n");
        }

        private void show_msg_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (ue_server != null)
            {
                ue_server.SetShowRecvMsg(show_msg_cb.Checked);
            }
        }
    }

    class WavFileInfo
    {
        public int src_no;
        public string name;
        public int ch;
        public int fs;
        public int wordcount;
        public double endtime;

        public WavFileInfo(int Src_no, string Name, int Ch, int Fs, int Wordcount)
        {
            src_no = Src_no;
            name = Name;
            ch = Ch;
            fs = Fs;
            wordcount = Wordcount;
            endtime = (double)wordcount / (double)fs;
        }
    }
}
