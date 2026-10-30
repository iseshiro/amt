using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Asio_io;
using System.Threading;
using Newtonsoft.Json;
using System.Text;

namespace amt
{
    public partial class EvalTest : Form
    {
        private struct ContentsDatabase
        {
            public string home_directory;
            public List<Sound> sound;
        }

        private struct Sound
        {
            public int id;
            public string type;
            public string sub_directory;
            public string filename;
            public int length;
            public int samplfreq;
            public int channel;
        }

        private Amt parent = null;
        private ContentsDatabase contents_db;
        private NetworkTcp network_tcp = null;
        private asio_io myasio = null;
        private HashSet<int> hsTable = null;
        public EvalTest(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            for (int ch = 0; ch < parent.out_btn_cnt; ch++)
            {
                if (!parent.OutBufCreate(parent.ch_info_out[ch], ChannelInfo.StreamType.wavread_asioout))
                {
                    return;
                }
            }
            contents_db_file_tb.Text = parent.contents_db_file;
            hsTable = new HashSet<int>();
        }

        private bool server_started = false;
        private void start_server_btn_Click(object sender, EventArgs e)
        {
            if (server_started)
            {
                start_server_btn.Text = "Start server";
                server_started = false;
                network_tcp.StopServer();
            }
            else
            {
                network_tcp = new NetworkTcp(parent, ConsoleMsg, new NetworkTcp.ExecCmdDelegate(Exec_cmd_evaltest));
                start_server_btn.Text = "Stop server";
                server_started = true;
                network_tcp.StartServer(false);
            }
        }

        private void contents_set_db_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.OpenFileDialog("Song Json file", "*.json"))
            {
                contents_db_file_tb.Text = parent.fd.filename;
                Context_db_set(contents_db_file_tb.Text);
            }
        }

        private void Context_load_db_btn_Click(object sender, EventArgs e)
        {
            Context_db_set(contents_db_file_tb.Text);
        }

        private void Context_db_set(string db_file)
        {
            if (!FileProcess.Exists(db_file))
            {
                ConsoleMsg.AppendText("Error: " + db_file + " not exist\r\n");
                return;
            }

            parent.contents_db_file = db_file;

            StreamReader sr = new StreamReader(db_file, Encoding.GetEncoding("utf-8"));
            string responseBody = sr.ReadToEnd();
            sr.Close();

            contents_db = JsonConvert.DeserializeObject<ContentsDatabase>(responseBody);
            //            Exec_cmd_evaltest("play 4 1 1 96");
        }

    private void Exec_cmd_evaltest(string cmd_text)
        {
            ArrayList argv = parent.Separate_arg(cmd_text);
            string cmd = (string)argv[0];
            string send_data = "";
            if (cmd == "assign")
            {
                int sound_id = Convert.ToInt32((string)argv[1]);
                string file_ch = (string)argv[2];
                string asio_ch = (string)argv[3];
                send_data = Assign(sound_id, file_ch, asio_ch);
                network_tcp.AmtNetWrite(send_data);
            }
            else if (cmd == "play")
            {
                string start_stop = (string)argv[1];
                if (start_stop == "start")
                {
                    Thread controlstart_thread = new Thread(new ThreadStart(ControlStart));
                    controlstart_thread.Name = "ControlStart";
                    controlstart_thread.Start();
                    parent.PlayControlStart();
                }
                else if (start_stop == "stop")
                {
                    myasio.set_stop(true);
                    network_tcp.AmtNetWrite("stop");
                }
            }
            else
            {
                network_tcp.AmtNetWrite("unknown command");
            }
        }
        public void ControlStart()
        {
            while (myasio.get_stop())
            {
                Thread.Sleep(1);
            }
            while (!myasio.get_stop())
            {
                Thread.Sleep(10);
            }
            foreach (int ch in hsTable)
            {
                parent.asio.stream_close((int)ChannelInfo.BufferType.output, ch - 1);
            }
            hsTable.Clear();
            network_tcp.AmtNetWrite("stop");
        }

        private string Assign(int sound_id, string file_ch, string asio_ch)
        {
            string home_dir = contents_db.home_directory;
            if (contents_db.sound.Count < sound_id) return "Error: sound_id not exist.";
            string type = contents_db.sound[sound_id - 1].type;
            string sub_dir = contents_db.sound[sound_id - 1].sub_directory;
            string filename = contents_db.sound[sound_id - 1].filename;
            int channel = contents_db.sound[sound_id - 1].channel;
            int sfreq = contents_db.sound[sound_id - 1].samplfreq;
            if (sfreq != parent.sample_freq) return "ERROR: Invalid sample frequency.";
            int[] file_ch_list = parent.Text2intarray(file_ch);
            int[] asio_ch_list = parent.Text2intarray(asio_ch);
            if (file_ch_list.Length != asio_ch_list.Length && file_ch_list.Length != 1) return "ERROR: Invalid channel assign.";
            if (file_ch_list.Length == asio_ch_list.Length)
            {
                int length = -1;
                for (int i=0;i< asio_ch_list.Length;i++)
                {
                    int file_ch_no = file_ch_list[i];
                    int asio_ch_no = asio_ch_list[i];
                    hsTable.Add(asio_ch_no);
                    string full_path = "";
                    if (type == "Single")
                    {
                        full_path = home_dir + "\\" + sub_dir + "\\" + filename;
                        unsafe
                        {
                            fixed (Char* ss = full_path.ToCharArray())
                            {
                                length = parent.asio.stream_open_mch_out(ss, file_ch_no, asio_ch_no);
                            }
                        }
                    }
                    else
                    {
                        string tt = "{1-" + channel.ToString() + "}";
                        full_path = home_dir + "\\" + sub_dir + "\\" + filename.Replace(tt, file_ch_no.ToString());
                        unsafe
                        {
                            fixed (Char* ss = full_path.ToCharArray())
                            {
                                length = parent.asio.stream_open_mch_out(ss, 1, asio_ch_no);
                            }
                        }
                    }
                    if (length < 0)
                    {
                        parent.Cnsl_msg("Error : File Name (" + full_path + ")\r\n");
                        return "NG";
                    }
                }
            }
            else if (file_ch_list.Length == 1)
            {
                int length = -1;
                int file_ch_no = file_ch_list[0];
                for (int i = 0; i < asio_ch_list.Length; i++)
                {
                    int asio_ch_no = asio_ch_list[i];
                    hsTable.Add(asio_ch_no);
                    string full_path = "";
                    if (type == "Single")
                    {
                        full_path = home_dir + "\\" + sub_dir + "\\" + filename;
                        unsafe
                        {
                            fixed (Char* ss = full_path.ToCharArray())
                            {
                                length = parent.asio.stream_open_mch_out(ss, file_ch_no, asio_ch_no);
                            }
                        }
                    }
                    else
                    {
                        string tt = "{1-" + channel.ToString() + "}";
                        full_path = home_dir + "\\" + sub_dir + "\\" + filename.Replace(tt, file_ch_no.ToString());
                        unsafe
                        {
                            fixed (Char* ss = full_path.ToCharArray())
                            {
                                length = parent.asio.stream_open_mch_out(ss, 1, asio_ch_no);
                            }
                        }
                    }
                    if (length < 0)
                    {
                        parent.Cnsl_msg("Error : File Name (" + full_path + ")\r\n");
                        return "NG";
                    }
                }
            }

            return "OK";
        }

        public void CreateContentsDB()
        {
            if (parent.Set_work_folder())
            {
                DirectoryInfo dir = new DirectoryInfo(parent.work_folder);
                HashSet<string> hsTable = new HashSet<string>();
                int dir_count = 0;
                int song_id = 1;
                foreach (FileInfo sound_file in dir.GetFiles("*.wav", SearchOption.AllDirectories))
                {
                    if (hsTable.Add(sound_file.DirectoryName))
                    {
                        dir_count++;
                    }
                }
                dir = new DirectoryInfo(parent.work_folder);
                string base_dir = dir.FullName;

                contents_db = new ContentsDatabase()
                {
                    home_directory = base_dir
                };

                contents_db.sound = new List<Sound>();

                foreach (string filepath in hsTable)
                {
                    DirectoryInfo song_dir = new DirectoryInfo(filepath);
                    // -----
                    FileInfo[] sound_files = song_dir.GetFiles("*.wav");
                    int song_count = sound_files.Length;
                    string file_ext = song_count.ToString() + ".wav";
                    FileInfo[] last_files = song_dir.GetFiles("*" + file_ext);
                    bool file_single = false;
                    int wav_leng_1st = 0;
                    int sfreq_1st = 0;
                    int ch_cnt_1st = 0;
                    string file_1st = "";
                    if (last_files.Length > 0)
                    {
                        string file_name = last_files[last_files.Length - 1].Name;
                        int digit_cnt = file_name.Length - file_ext.Length;
                        file_1st = file_name.Remove(digit_cnt);
                        WavStream ws = new WavStream(parent);
                        for (int ii = 1; ii <= song_count; ii++)
                        {
                            file_name = song_dir.FullName + "\\" + file_1st + ii.ToString() + ".wav";
                            if (FileProcess.Exists(file_name))
                            {
                                ws.WavHeaderCheck(file_name);
                                int wav_leng = ws.WordCount();
                                int sfreq = ws.SampleFrequency();
                                int ch_cnt = ws.Channel();
                                if (ii == 1)
                                {
                                    wav_leng_1st = wav_leng;
                                    sfreq_1st = sfreq;
                                    ch_cnt_1st = ch_cnt;
                                }
                                else
                                {
                                    if (wav_leng_1st != wav_leng || sfreq_1st != sfreq || ch_cnt_1st != ch_cnt)
                                    {
                                        file_single = true;
                                    }
                                }
                            }
                            else
                                file_single = true;
                        }
                    }
                    else file_single = true;
                    if (!file_single)
                    {
                        Sound pp;
                        pp.id = song_id;
                        pp.type = "Multiple";
                        pp.sub_directory = song_dir.FullName.Replace(base_dir + "\\", "");
                        pp.filename = file_1st + "{1-" + song_count.ToString() + "}.wav";
                        pp.length = wav_leng_1st;
                        pp.samplfreq = sfreq_1st;
                        pp.channel = song_count;
                        contents_db.sound.Add(pp);
                        song_id++;
                    }
                    else
                    {
                        foreach (FileInfo sound_file in sound_files)
                        {
                            string file_name = song_dir.FullName + "\\" + sound_file.Name;
                            WavStream ws = new WavStream(parent);
                            if (!ws.WavHeaderCheck(file_name)) continue;
                            Sound pp;
                            pp.id = song_id;
                            pp.type = "Multiple";
                            pp.sub_directory = song_dir.FullName.Replace(base_dir + "\\", "");
                            pp.filename = sound_file.Name;
                            pp.length = ws.WordCount();
                            pp.samplfreq = ws.SampleFrequency();
                            pp.channel = ws.Channel();
                            contents_db.sound.Add(pp);
                            song_id++;
                        }
                    }
                }
                string formattedJson = JsonConvert.SerializeObject(contents_db, Formatting.Indented);
                parent.contents_db_file = parent.work_folder + @"\contents_db.json";
                StreamWriter myStreamWriter = new System.IO.StreamWriter(parent.contents_db_file, false, System.Text.Encoding.UTF8);
                myStreamWriter.Write(formattedJson);
                myStreamWriter.Close();
            }
        }
    }
}
