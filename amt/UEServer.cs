using System;
using System.Threading;
using Asio_io;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace amt
{
    class UEServer
    {
        private Amt parent = null;
        private NetworkTcp network_tcp = null;
        private asio_io myasio = null;
        private double mic_radius;
        private string[] vr_src;
        private int frame_size;
        private System.Windows.Forms.TextBox msgBox;
        System.Windows.Forms.ProgressBar level_meter;
        private int input_count = 0;
        private bool level_is_input = true;
        private int level_ch = 0;
        private int level_mode = 0;
        private int max_src_no = 0;

        public UEServer(Amt Parent, string[] vrs, System.Windows.Forms.TextBox message_box, int InputCount)
        {
            parent = Parent;
            myasio = parent.asio;
            vr_src = vrs;
            input_count = InputCount;
            max_src_no = vr_src.Length + input_count;
            network_tcp = new NetworkTcp(parent, message_box, new NetworkTcp.ExecCmdDelegate(Exec_cmd));
            msgBox = message_box;
        }

        public void SetupLevelMeter(System.Windows.Forms.ProgressBar level_pb, int LevelMode, bool LevelIsInput, int LevelCh)
        {
            level_meter = level_pb;
            level_mode = LevelMode;
            level_is_input = LevelIsInput;
            level_ch = LevelCh;
        }

        public void SetShowRecvMsg(bool Show_recv_msg)
        {
            network_tcp.SetShowRecvMsg(Show_recv_msg);
        }

        public bool Server_start(int mic_mode, int Frame_size, bool Show_recv_msg)
        {
            frame_size = Frame_size;
            if (!myasio.rtmove_init(mic_mode, frame_size, vr_src.Length, input_count))
            {
                return false;
            }
            if (!myasio.set_level_meter(level_is_input, level_ch, level_mode))
            {
                return false;
            }
            mic_radius = myasio.rtmove_mic_radius();
            parent.ResetButtonStatus(true, true);
            network_tcp.StartServer(Show_recv_msg);

            int max_word_cnt = 0;
            int sfreq = 48000;
            myasio.rtmove_add_vr_src(input_count);
            for (int wav_no=0; wav_no< vr_src.Length; wav_no++)
            {
                int word_cnt = 0;
                string file_path = parent.work_folder + "\\" + vr_src[wav_no] + ".wav";
                unsafe
                {
                    fixed (Char* s2 = file_path.ToCharArray())
                    {
                        word_cnt = myasio.rtmove_add_ue_src(s2, sfreq);
                    }
                }
                if (word_cnt > 0)
                {
                    if (word_cnt > max_word_cnt)
                    {
                        max_word_cnt = word_cnt;
                    }
                }
                else
                {
                    network_tcp.StopServer();
                    return false;
                }
            }

            parent.stream_word_count = max_word_cnt;
            Thread playstart_thread = GetPlaystart_thread();
            playstart_thread.Start();

            return true;
        }

        public void Server_stop()
        {
            myasio.set_stop(true);
            network_tcp.StopServer();
        }

        private string send_data = "NG";
        private void Exec_cmd(string cmd_text)
        {
            ArrayList argv = parent.Separate_arg(cmd_text);
            string cmd = (string)argv[0];
            if (cmd == "get_source_count")
            {
                send_data = vr_src.Length.ToString();
            }
            else if (cmd == "get_frame_size")
            {
                send_data = frame_size.ToString();
            }
            else if (cmd == "get_mic_radius")
            {
                send_data = mic_radius.ToString();
            }
            else if (cmd == "get_source_name")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if(check_id(src_no))
                {
                    send_data = vr_src[src_no];
                }
            }
            //else if (cmd == "get_source_time")
            //{
            //    int src_no = Convert.ToInt32((string)argv[1]) - 1;
            //    if (check_id(src_no))
            //    {
            //        send_data = vr_src[src_no].stop_time.ToString();
            //    }
            //}
            else if (cmd == "set_amp")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if (check_id(src_no))
                {
                    double new_amp = Convert.ToDouble((string)argv[2]);
                    myasio.rtmove_chg_amp(src_no, new_amp);
                    send_data = "OK";
                }
            }
            else if (cmd == "play")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if (check_id(src_no))
                {
                    double playback_delay = 0.0;
                    double playback_start = 0.0;
                    double time_to_que = 0.0;
                    int mode = 1;
                    send_data = "Play:" + (src_no + 1).ToString();
                    myasio.rtmove_play_mode(src_no, mode, playback_start, playback_delay, time_to_que);
                }
            }
            else if (cmd == "stop")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if (check_id(src_no))
                {
                    myasio.rtmove_stop(src_no);
                    send_data = "Stop:" + (src_no + 1).ToString();
                }
            }
            else if (cmd == "posi")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if (check_id(src_no))
                {
                    double ppx = Convert.ToDouble((string)argv[2]);
                    double ppy = Convert.ToDouble((string)argv[3]);
                    double ppz = Convert.ToDouble((string)argv[4]);
                    //toolStripStatusLabel1.Text = "Src : " + src_no.ToString() + ", Posi : [" + ppx.ToString("0.00") + "," + ppy.ToString("0.00") + "," + ppz.ToString("0.00") + "]";
                    myasio.rtmove_move(src_no, ppx, ppy, ppz);
                    send_data = "OK";
                }
            }
            else if (cmd == "polar")
            {
                int src_no = Convert.ToInt32((string)argv[1]) - 1;
                if (check_id(src_no) && argv.Count == 5)
                {
                    double aa = Math.PI / 180.0;
                    double rr = Convert.ToDouble((string)argv[2]);
                    double theta = Convert.ToDouble((string)argv[3]) * aa;
                    double phi = Convert.ToDouble((string)argv[4]) * aa;
                    //toolStripStatusLabel1.Text = "Src : " + src_no.ToString() + ", Posi : [" + ppx.ToString("0.00") + "," + ppy.ToString("0.00") + "," + ppz.ToString("0.00") + "]";
                    myasio.rtmove_move_polar(src_no, rr, theta, phi);
                    send_data = "OK";
                }
            }
            else if (cmd == "get_source_folder")
            {
                send_data = parent.work_folder;
            }
            else
            {
                send_data = "unknown command";
            }
            network_tcp.AmtNetWrite(send_data);
            //msgBox.AppendText("Recv: " + cmd + ", Send: " + send_data + "\n");
        }

        private bool check_id(int src_no)
        {
            bool ret = false;
            if (0 <= src_no && src_no < max_src_no)
            {
                ret = true;
            }
            else
            {
                send_data = "Error: Invalid source number";
            }
            return ret;
        }

        private Thread GetPlaystart_thread()
        {
            return new Thread(new ThreadStart(PlayStart))
            {
                Name = "PlayStart"
            };
        }

        public void PlayStart()
        {
            Thread controlstart_thread = GetControlstart_thread();
            controlstart_thread.Start();

            parent.menuItem_Exit.Enabled = false;
            //if (myasio.initialize() < 0) goto asio_error;
            if (myasio.create_buffers() < 0) goto asio_error;
            if (myasio.start() < 0) goto asio_error;
            msgBox.AppendText("ASIO start\r\n");
            if (myasio.stop() < 0) goto asio_error;
            msgBox.AppendText("ASIO stop\r\n");
            myasio.rtmove_stop(-1);
            myasio.dispose_buffers();
            double zz = parent.zitter_level;
            parent.Cnsl_msg("Zitter Level=" + zz.ToString("0.0") + "\r\n");
            parent.ResetButtonStatus(true, true);
            parent.menuItem_Exit.Enabled = true;
            //this.Invalidate();
            while (control_running) Thread.Sleep(100);

            if (myasio.check_error() == 0) return;

            asio_error:
            String error_msg = "";
            unsafe
            {
                error_msg = new String(myasio.error_message());
            }
            parent.Cnsl_msg(error_msg + "\r\n");
            return;
        }

        private Thread GetControlstart_thread()
        {
            return new Thread(new ThreadStart(ControlStart))
            {
                Name = "ControlStart"
            };
        }

        private bool control_running = false;
        public void ControlStart()
        {
            control_running = true;
            Thread.Sleep(100);
            int leng = parent.default_dleng;
            double pp = 1.0 / (double)leng;
            msgBox.AppendText("ControlStart start\r\n");

            while (!myasio.get_stop())
            {
                int position = myasio.getADpoint() % leng;
                double peak_rms = myasio.get_peak_rms();
                int level_val = 0;
                if (level_mode == 1)  // Peak
                {
                    level_val = (int)(20.0 * Math.Log10(peak_rms)) + 60;
                }
                else if(level_mode == 2)  // RMS
                {
                    level_val = (int)(10.0 * Math.Log10(peak_rms)) + 60;
                }
                if (level_val > 100) level_val = 100;
                else if (level_val < 0) level_val = 0;
                level_meter.Value = level_val;

                parent.adda_point_tb.Value = (int)(pp * (double)position * 100.0);
                parent.adda_point_lbl.Text = position.ToString();
                parent.Write_zitter_level();
                //string status_msg = "";
                //for (int src_no = 0; src_no < source_count; src_no++)
                //{
                //    int max_mic = myasio.rtmove_maxmic(src_no);
                //    if (src_no > 0) status_msg += ", ";
                //    status_msg += "Src" + (src_no + 1).ToString() + ": Max MIC" + (max_mic + 1).ToString();
                //}
                //toolStripStatusLabel1.Text = status_msg;
                Thread.Sleep(100);
            }
            myasio.setADpoint(0);
            parent.SetAllButtonStatus();
            msgBox.AppendText("ControlStart stop\r\n");
            control_running = false;
        }
    }
}
