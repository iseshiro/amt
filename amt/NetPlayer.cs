using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using Asio_io;

namespace amt
{
    public partial class NetPlayer : Form
    {
        private Amt parent = null;
        private SongDatabase song_db;
        private FilterDatabase filter_db;
        private bool song_list_ok = false;
        private bool filter_list_ok = false;
        private NetworkTcp network_tcp = null;
        private asio_io myasio = null;
        private RealtimeConv rtconv = null;

#pragma warning disable 0649
        private struct SongDatabase
        {
            public string directory;
            public List<Artist> artists;
        }
#pragma warning restore 0649

        private struct Artist
        {
            public int id;
            public string name;
            public List<Album> albums;
        }

        private struct Album
        {
            public int id;
            public string name;
            public string artwork;
            public List<Song> songs;
        }

        private struct Song
        {
            public int id;
            public string name;
            public int leng;
            public int fs;
            public int ch;
        }

#pragma warning disable 0649
        private struct FilterDatabase
        {
            public string directory;
            public List<Filter> filters;
        }

        private struct Filter
        {
            public int id;
            public string name;
            public string directory;
            public List<string> detail;
            public List<Condition> condition;
        }

        private struct Condition
        {
            public int id;
            public string name;
            public string directory;
            public int length;
            public string file;
        }
#pragma warning restore 0649

        public NetPlayer(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;

            filter_db_file_tb.Text = parent.filter_db_file;
            song_db_file_tb.Text = parent.song_db_file;
        }

        private void Song_db_set(string db_file)
        {
            if (!FileProcess.Exists(db_file))
            {
                ConsoleMsg.AppendText("Error: " + db_file + " not exist\r\n");
                return;
            }

            StreamReader sr = new StreamReader(filter_db_file_tb.Text, Encoding.GetEncoding("utf-8"));
            string responsebody = sr.ReadToEnd();
            sr.Close();
            song_db = JsonConvert.DeserializeObject<SongDatabase>(responsebody);
            song_list_ok = true;
            start_server_btn.Enabled = song_list_ok && filter_list_ok;
        }

        private void Song_load_db_btn_Click(object sender, EventArgs e)
        {
            song_list_ok = false;
            Song_db_set(song_db_file_tb.Text);
        }

        private void Song_db_set_btn_Clic(object sender, EventArgs e)
        {
            song_list_ok = false;
            if (parent.fd.OpenFileDialog("Song Json file", "*.json"))
            {
                song_db_file_tb.Text = parent.fd.filename;
                Song_db_set(song_db_file_tb.Text);
            }
        }

        private void Filter_db_set(string db_file)
        {
            if (!FileProcess.Exists(db_file))
            {
                ConsoleMsg.AppendText("Error: " + db_file + " not exist\r\n");
                return;
            }

            StreamReader sr = new StreamReader(filter_db_file_tb.Text, Encoding.GetEncoding("utf-8"));
            string responsebody = sr.ReadToEnd();
            sr.Close();
            filter_db = JsonConvert.DeserializeObject<FilterDatabase>(responsebody);

            filter_list_ok = true;
            //ConsoleMsg.AppendText("Filter count = " + filter_list.Count.ToString() + "\r\n");
            start_server_btn.Enabled = song_list_ok && filter_list_ok;
        }

        private void Filter_load_db_btn_Click(object sender, EventArgs e)
        {
            filter_list_ok = false;
            Filter_db_set(filter_db_file_tb.Text);
        }

        private void Filter_db_set_btn_Clic(object sender, EventArgs e)
        {
            filter_list_ok = false;

            if (parent.fd.OpenFileDialog("Filter Json file", "*.json"))
            {
                filter_db_file_tb.Text = parent.fd.filename;
                Filter_db_set(filter_db_file_tb.Text);
            }
        }

        private bool server_started = false;
        private void Start_server_btn_Click(object sender, EventArgs e)
        {
            if(server_started)
            {
                start_server_btn.Text = "Start server";
                server_started = false;
                network_tcp.StopServer();
                ConsoleMsg.AppendText("Stop Server\r\n");
            }
            else
            {
                network_tcp = new NetworkTcp(parent, ConsoleMsg, new NetworkTcp.ExecCmdDelegate(Exec_cmd_roomsimulator));
                start_server_btn.Text = "Stop server";
                server_started = true;
                network_tcp.StartServer(false);
                ConsoleMsg.AppendText("Start Server\r\n");
            }
        }
        private string StartFilter(int room_no, int cond_no)
        {
            string base_dir = filter_db.directory;
            string filter_dir = filter_db.filters[room_no].directory;
            string cond_dir = filter_db.filters[room_no].condition[cond_no].directory;
            string cond_file = filter_db.filters[room_no].condition[cond_no].file;
            string full_path = base_dir + "\\" + filter_dir + "\\" + cond_dir + "\\" + cond_file;

            int length = filter_db.filters[room_no].condition[cond_no].length;
            int conv_leng = 0;
            int fft_lvl = 0;
            int fftpnt_1 = parent.asio.GetPreferredSize() * 2;
            while (conv_leng < length)
            {
                conv_leng += fftpnt_1;
                fftpnt_1 *= 2;
                fft_lvl++;
            }
            SourcePath src_path = new SourcePath(parent);
            if (src_path.get_filter_path2(full_path))
            {
                bool result = src_path.check_mic_sp_cnt(parent.in_btn_cnt, parent.out_btn_cnt);
                if(!result)
                {
                    ConsoleMsg.AppendText("Error: " + full_path + " not exist\r\n");
                    return "NG";
                }
                int mic_cnt = src_path.mic_count;
                int sp_cnt = src_path.sp_count;

                rtconv = new RealtimeConv(parent, mic_cnt, sp_cnt, fft_lvl);
                bool rtconv_single_mode = false;
                bool is_inv = false;
                bool init_ok = rtconv.Init_f(src_path, rtconv_single_mode, is_inv, 0, toolStripProgressBar1);
                // rtconv start
//                parent.asio.rtconv_start();   rtconv is started in PlayStart
            } else {
                string message = "ファイル名が不適切です。 : " + full_path;
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConsoleMsg.AppendText("StartFilter" + filter_dir + ":" + cond_dir + ": fft_level = " + fft_lvl.ToString() + "\r\n");
            return "OK";
        }

        private string SendArtwork(int artist_id, int album_id, bool file_info)
        {
            if (artist_id >= song_db.artists.Count)
            {
                ConsoleMsg.AppendText("Invalid artist number\r\n");
                return "Error: Invalid artist number";
            }
            if (album_id >= song_db.artists[artist_id].albums.Count)
            {
                ConsoleMsg.AppendText("Invalid album number\r\n");
                return "Error: Invalid artist number";
            }

            string base_dir = song_db.directory;
            string artist_dir = song_db.artists[artist_id].name;
            string album_dir = song_db.artists[artist_id].albums[album_id].name;
            string album_artwork = song_db.artists[artist_id].albums[album_id].artwork;
            string full_path = base_dir + "\\" + artist_dir + "\\" + album_dir + "\\" + album_artwork;
            string filename = "artwork_image_" + artist_id.ToString() + "_" + album_id.ToString() + ".jpg";
            FileInfo finfo = new FileInfo(full_path);
            if (!finfo.Exists) return "Error: file not exist";
            long file_size = finfo.Length;
            string send_message = "";
            if (file_info)
            {
                send_message = filename + ":" + file_size.ToString();
            }
            else
            {
                using (FileStream fs = finfo.OpenRead())
                {
                    byte[] buf = new byte[file_size];
                    int read_count = fs.Read(buf, 0, (int)file_size);
                    send_message = Convert.ToBase64String(buf);
                }
            }
            return send_message;
        }

        private string AttachSong(int artist_id, int album_id, int song_id)
        {
            string base_dir = song_db.directory;
            string artist_dir = song_db.artists[artist_id].name;
            string album_dir = song_db.artists[artist_id].albums[album_id].name;
            string song_dir = song_db.artists[artist_id].albums[album_id].songs[song_id].name;
            string full_path = base_dir + "\\" + artist_dir + "\\" + album_dir + "\\" + song_dir + ".wav";

            int length1 = -1;
            int length2 = -1;
            unsafe
            {
                fixed (Char* ss = full_path.ToCharArray())
                {
                    length1 = parent.asio.stream_open_mch_in(ss, 1, 1);
                    length2 = parent.asio.stream_open_mch_in(ss, 2, 2);
                }
            }
            if (length1 < 0 || length2 < 0 || length1 != length2)
            {
                parent.Cnsl_msg("Error : File Name (" + full_path + ")\r\n");
            }
            else
            {
                parent.PlayControlStart();
            }
            return "OK";
        }

        private string DettachSong()
        {
            parent.asio.stream_close((int)ChannelInfo.BufferType.output, 0);
            parent.asio.stream_close((int)ChannelInfo.BufferType.output, 1);
            return "OK";
        }

        private string CurrentPosition()
        {
            int posi = parent.asio.stream_file_point();
            return posi.ToString();
        }

         private string SelectSong(string ids_str)
        {
            string[] ids = ids_str.Split('.');

            if (ids.Length != 3)
            {
                string msg = "Error: " + ids_str;
                ConsoleMsg.AppendText(msg + "\r\n");
                return msg;
            }
            int artist_id = Convert.ToInt32(ids[0]);
            int album_id = Convert.ToInt32(ids[1]);
            int song_id = Convert.ToInt32(ids[2]);

            ConsoleMsg.AppendText("SelectSong " + ids_str + "\r\n");
            AttachSong(artist_id, album_id, song_id);
            return "OK";
        }

        private string ConfirmSong(string ids_str, string song_title)
        {
            string[] ids = ids_str.Split('.');

            if (ids.Length != 3)
            {
                string msg = "Error: " + ids_str;
                ConsoleMsg.AppendText(msg + "\r\n");
                return msg;
            }
            ConsoleMsg.AppendText("ConfirmSong " + ids_str + "\r\n");

            int artist_id = Convert.ToInt32(ids[0]);
            if (artist_id >= song_db.artists.Count) return "NG";
            int album_id = Convert.ToInt32(ids[1]);
            if (album_id >= song_db.artists[artist_id].albums.Count) return "NG";
            int song_id = Convert.ToInt32(ids[2]);
            if (song_id >= song_db.artists[artist_id].albums[album_id].songs.Count) return "NG";
            ConsoleMsg.AppendText(song_title + "==" + song_db.artists[artist_id].albums[album_id].songs[song_id].name + "\r\n");
            if (song_title == song_db.artists[artist_id].albums[album_id].songs[song_id].name) return "OK";
            return "NG";
        }

        private string UpdateArtwork(string ids_str, bool file_info)
        {
            string[] ids = ids_str.Split('.');

            if (ids.Length != 2)
            {
                string msg = "Error: " + ids_str;
                ConsoleMsg.AppendText(msg + "\r\n");
                return msg;
            }
            int artist_id = Convert.ToInt32(ids[0]);
            int album_id = Convert.ToInt32(ids[1]);

            ConsoleMsg.AppendText("UpdateArtwork " + ids_str + "\r\n");
            return SendArtwork(artist_id, album_id, file_info);
        }

        private string SelectFilter(string ids_str)
        {
            string[] ids = ids_str.Split('.');

            if (ids.Length != 2)
            {
                string msg = "Error: " + ids_str;
                ConsoleMsg.AppendText(msg + "\r\n");
                return msg;
            }
            ConsoleMsg.AppendText("SelectFilter " + ids_str + "\r\n");
            int room_no = Convert.ToInt32(ids[0]);
            int cond_no = Convert.ToInt32(ids[1]);
            return StartFilter(room_no, cond_no);
        }

        private void Exec_cmd_roomsimulator(string cmd_text)
        {
            ArrayList argv = parent.Separate_arg(cmd_text);
            string cmd = (string)argv[0];
            string send_data = "";
            if (cmd == "load_filter_json")
            {
                send_data = JsonConvert.SerializeObject(filter_db, Formatting.None);
                ConsoleMsg.AppendText("create_filter_json\r\n");
            }
            else if (cmd == "load_song_json")
            {
                send_data = JsonConvert.SerializeObject(song_db, Formatting.None);
                ConsoleMsg.AppendText("create_song_json\r\n");
            }
            else if (cmd == "select_song")
            {
                if(!parent.asio.get_stop())
                {
                    send_data = DettachSong();
                }
                send_data = SelectSong((string)argv[1]);
            }
            else if (cmd == "confirm_song")
            {
                int ii0 = cmd_text.IndexOf(' ');
                int ii1 = cmd_text.IndexOf(':');
                string ids = cmd_text.Substring(ii0 + 1, ii1 - ii0 - 1);
                string song_title = cmd_text.Substring(ii1 + 1, cmd_text.Length - ii1 - 1);
                send_data = ids + "." + ConfirmSong(ids, song_title);
            }
            else if (cmd == "file_info")
            {
                send_data = UpdateArtwork((string)argv[1], true);
            }
            else if (cmd == "file_data")
            {
                send_data = UpdateArtwork((string)argv[1], false);
            }
            else if (cmd == "select_filter")
            {
                send_data = SelectFilter((string)argv[1]);
            }
            else if (cmd == "play_stop")
            {
                send_data = DettachSong();
            }
            else if (cmd == "current_position")
            {
                send_data = CurrentPosition();
            }
            else if (cmd == "server_ready")
            {
                send_data = "yes";
            }
            else if (cmd == "disconnect")
            {
                send_data = DettachSong();
            }
            else
            {
                send_data = "unknown command";
            }
            network_tcp.AmtNetWrite(send_data);
        }

        public void CreateSongDB()
        {
            if (parent.Set_work_folder())
            {
                song_db = new SongDatabase()
                {
                    artists = new List<Artist>()
                };
                DirectoryInfo dir = new DirectoryInfo(parent.work_folder);
                int artist_id = 1;
                foreach (DirectoryInfo artist in dir.GetDirectories())
                {
                    Artist ss;
                    ss.id = artist_id++;
                    ss.name = artist.Name;
                    ss.albums = new List<Album>();
                    int album_id = 1;
                    foreach (DirectoryInfo album in artist.GetDirectories())
                    {
                        Album tt;
                        tt.id = album_id++;
                        tt.name = album.Name;
                        string artwork = "no_image";
                        FileInfo[] artwork_file = album.GetFiles("*.jpg");
                        if (artwork_file.Length > 0)
                        {
                            artwork = artwork_file[0].Name;
                        }
                        tt.artwork = artwork;
                        tt.songs = new List<Song>();
                        int song_id = 1;
                        foreach (FileInfo song_file in album.GetFiles("*.wav"))
                        {
                            if (song_file.Name[0] != '.')
                            {
                                int wav_leng = 0;
                                int Fs = 0;
                                int Ch = 0;
                                WavStream ws = new WavStream(parent);
                                if (ws.WavHeaderCheck(song_file.FullName))
                                {
                                    wav_leng = ws.WordCount();
                                    Fs = ws.SampleFrequency();
                                    Ch = ws.Channel();
                                }
                                Song pp;
                                pp.id = song_id++;
                                int len = song_file.Name.Length - 4;
                                pp.name = song_file.Name.Substring(0, len);
                                pp.leng = wav_leng;
                                pp.fs = Fs;
                                pp.ch = Ch;
                                tt.songs.Add(pp);
                            }
                            ss.albums.Add(tt);
                        }
                        song_db.artists.Add(ss);
                    }
                }
                StreamWriter myStreamWriter = new System.IO.StreamWriter(parent.work_folder + @"\song_db.json");
                string formattedJson = JsonConvert.SerializeObject(song_db, Formatting.Indented);
                myStreamWriter.Write(formattedJson);
                myStreamWriter.Close();
            }
        }
    }
}
