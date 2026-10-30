using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Asio_io;

namespace amt
{
    public partial class NetConv : Form
    {
        private Amt parent = null;
        private SourcePath src_path;
        public int rtconv_inch = 1;
        public int rtconv_outch = 0;
        private asio_io myasio = null;
        private int cpu_core;
        private bool inv_path_ok = false;
        private int src_id = -1;
//        private System.Net.Sockets.UdpClient udp_send = null;
//        private System.Net.Sockets.UdpClient udp_receive = null;
        private string ip_adress = "192.168.0.8";
        private int frame_size = 2048;
        private int port_no = 55152;
        private RealtimeConv rtconv = null;
        public NetConv(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            myasio = parent.asio;
            cpu_core = myasio.core_count();
            ipaddress_tb.Text = ip_adress;
            port_no_tb.Text = port_no.ToString();
            frame_size_tb.Text = frame_size.ToString();
            String hostname = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry ipentry = System.Net.Dns.GetHostEntry(hostname);
            string ipaddress = "";
            foreach (System.Net.IPAddress ip in ipentry.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipaddress = ip.ToString();
                    break;
                }
            }
            console_msg_tb.AppendText("IPAddress = " + ipaddress + "\r\n");
            ipaddress_tb.Text = ipaddress;
            if (parent.out_btn_cnt >= 2)
            {
                mic_posi_cb.Items.Add("Binaural (2ch)");
                mic_posi_cb.SelectedIndex = 0;
                rtconv_inch = 2;
            }
            if (parent.out_btn_cnt >= 80)
            {
                mic_posi_cb.Items.Add("Fullerene (80ch)");
                mic_posi_cb.SelectedIndex = 1;
                rtconv_inch = 80;
            }
            if (parent.out_btn_cnt >= 96)
            {
                mic_posi_cb.Items.Add("Sound Cask (96ch)");
                mic_posi_cb.SelectedIndex = 2;
                rtconv_inch = 96;
            }
            in_ch_tb.Text = rtconv_inch.ToString();
            if (parent.out_btn_cnt < 96)
            {
                rtconv_outch = parent.out_btn_cnt;
            }
            else
            {
                rtconv_outch = 96;
            }
            out_ch_tb.Text = rtconv_outch.ToString();

            // src ListViewコントロールのプロパティを設定
            src_listView.FullRowSelect = true;
            src_listView.GridLines = true;
            src_listView.Sorting = SortOrder.Ascending;
            src_listView.View = View.Details;
            // 列（コラム）ヘッダの作成
            ColumnHeader src_columnID = new ColumnHeader();
            ColumnHeader src_columnName = new ColumnHeader();
            ColumnHeader src_columnFullPath = new ColumnHeader();
            ColumnHeader src_columnStartTime = new ColumnHeader();
            ColumnHeader src_columnStopTime = new ColumnHeader();
            src_columnID.Text = "ID";
            src_columnID.Width = 30;
            src_columnName.Text = "Name";
            src_columnName.Width = 100;
            src_columnFullPath.Text = "Full Path";
            src_columnFullPath.Width = 300;
            src_columnStartTime.Text = "Start";
            src_columnStartTime.Width = 60;
            src_columnStopTime.Text = "Stop";
            src_columnStopTime.Width = 60;
            ColumnHeader[] src_colHeaderRegValue = { src_columnID, src_columnName, src_columnFullPath, src_columnStartTime, src_columnStopTime };
            src_listView.Columns.AddRange(src_colHeaderRegValue);

            src_path = new SourcePath(parent);
            rtconv = new RealtimeConv(parent, rtconv_inch, rtconv_outch, 1);
            fft_level_ud.Value = 1;
            set_button_status();
            update_info();
            src_attach_btn.Enabled = false;
            if (parent.out_btn_cnt >= 96)
            {
                mic_posi_cb.Items.Add("Sound Cask (96ch)");
                mic_posi_cb.SelectedIndex = 2;
            }
            else if (parent.out_btn_cnt >= 80)
            {
                mic_posi_cb.Items.Add("Fullerene (80ch)");
                mic_posi_cb.SelectedIndex = 1;
            }
            else if (parent.out_btn_cnt >= 2)
            {
                mic_posi_cb.Items.Add("Binaural (2ch)");
                mic_posi_cb.SelectedIndex = 0;
            }
            udp_info_lbl.Text = "";
            //debug_init();
        }
        private void set_button_status()
        {
            ipaddress_tb.Enabled = send_cb.Checked;
            port_no_tb.Enabled = send_cb.Checked || receive_cb.Checked;
            inv_path_btn.Enabled = receive_cb.Checked;
            fft_level_ud.Enabled = receive_cb.Checked;
            in_ch_tb.Enabled = receive_cb.Checked;
            out_ch_tb.Enabled = receive_cb.Checked;
            src_add_btn.Enabled = receive_cb.Checked;
            src_rm_btn.Enabled = false;
            send_sin1k_cb.Visible = send_cb.Checked;
        }
        private void update_info()
        {
            int p_size = myasio.GetPreferredSize();
            int leng = myasio.GetDataLength();
            CPUCoreToolStripMenuItem.Text = "CPU core = " + cpu_core.ToString();
            DataLengthToolStripMenuItem.Text = "Data Length = " + leng.ToString();
            FFTPointToolStripMenuItem.Text = "FFT Point = " + rtconv.GetFFTPoint();
            ASIOBufSizeToolStripMenuItem.Text = "ASIO Buffer Size = " + p_size.ToString();
            Message2_toolStripStatusLabel.Text = "Required Data Length= " + rtconv.GetMinBufSize();
            Message1_toolStripStatusLabel.Text = "Conv. Length = " + rtconv.GetConvLeng().ToString();
            server_start_btn.Enabled = set_btn_ok();
        }

        private void inv_path_btn_Click(object sender, EventArgs e)
        {
            bool is_inv = true;
            if(src_path.get_filter_path(rtconv_inch, rtconv_outch, "dat", is_inv))
            {
                bool rtconv_single_mode = false;
                int udp_ch = 1;
                inv_path_ok = rtconv.Init_f(src_path, rtconv_single_mode, is_inv, udp_ch, toolStripProgressBar1);
                server_start_btn.Enabled = set_btn_ok();
            }
            else
            {
                string message = "ファイル名が不適切です。 : " + parent.fd.filename;
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void debug_init()
        //{
        //    receive_cb.Checked = true;

        //    src_path.get_filter_path1("C:\\temp\\calib_imp_oldcask_senju\\inv_reg_optimum\\invf_s01-m01.dat", rtconv_inch, rtconv_outch, true);
        //    bool rtconv_single_mode = false;
        //    bool is_inv = true;
        //    int udp_ch = 1;
        //    inv_path_ok = rtconv.Init_f(src_path, rtconv_single_mode, is_inv, udp_ch, toolStripProgressBar1);
        //    server_start_btn.Enabled = set_btn_ok();


        //    src_id = src_path.get_source_path1(rtconv_inch);
        //    if (src_id >= 0)
        //    {
        //        // ListViewコントロールにデータを追加します。
        //        int id = src_listView.Items.Count + 1;
        //        double stop_sec = -1.0f;

        //        foreach (src_info p in src_path.src_list)
        //        {
        //            if (p.id == id)
        //            {
        //                p.stop_time = (float)src_path.src_length / (float)src_path.src_sfreq;
        //                stop_sec = p.stop_time;
        //                break;
        //            }
        //        }
        //        double start_sec = 0;
        //        string filename = src_path.src_filename(src_id, 1);
        //        FileInfo cFileInfo = new FileInfo(filename);
        //        string[] item1 = { id.ToString(), cFileInfo.Directory.Name, filename, start_sec.ToString(), stop_sec.ToString() };
        //        src_listView.Items.Add(new ListViewItem(item1));
        //        string fullpath = src_path.src_filename_c(src_id);
        //        unsafe
        //        {
        //            bool ok_flg = false;
        //            fixed (Char* path = fullpath.ToCharArray())
        //            {
        //                ok_flg = myasio.play_list_apend(path, rtconv_inch, start_sec, stop_sec);
        //            }
        //        }

        //        //                src_listView.Items[src_listView.Items.Count - 1].Selected = true;
        //        console_msg_tb.AppendText("Add:" + id.ToString() + ". " + cFileInfo.Directory.Name + "\r\n");
        //    }
        //}

        //private void CloseUDP()
        //{
        //    udp_send.Close();
        //    udp_receive.Close();
        //}
        private bool set_btn_ok()
        {
            bool ret = false;
            if (receive_cb.Checked)
            {
                ret = inv_path_ok;
            }
            if (send_cb.Checked)
            {
                ret = true;
            }
            return ret;
        }

        private void src_add_btn_Click(object sender, EventArgs e)
        {
            src_id = src_path.get_source_path(rtconv_inch, "wav");

            if (src_id >= 0)
            {
                // ListViewコントロールにデータを追加します。
                int id = src_listView.Items.Count + 1;
                double stop_sec = -1.0f;
                double total_time = 0.0;
                foreach (src_info p in src_path.src_list)
                {
                    p.stop_time = (float)src_path.src_length / (float)src_path.src_sfreq;
                    total_time += p.stop_time;
                    if (p.id == id)
                    {
                        stop_sec = p.stop_time;
                    }
                }
                int tot_sec = (int)total_time;
                int tot_min = tot_sec / 60;
                tot_sec = tot_sec % 60;
                total_time_lbl.Text = "Total time = " + tot_min.ToString("00") + ":" + tot_sec.ToString("00");

                double start_sec = 0;
                string filename = src_path.src_filename(src_id, 1);
                FileInfo cFileInfo = new FileInfo(filename);
                string[] item1 = { id.ToString(), cFileInfo.Directory.Name, filename, start_sec.ToString(), stop_sec.ToString() };
                src_listView.Items.Add(new ListViewItem(item1));
                string fullpath = src_path.src_filename_c(src_id);
                unsafe
                {
                    bool ok_flg = false;
                    fixed (Char* path = fullpath.ToCharArray())
                    {
                        ok_flg = myasio.play_list_apend(path, rtconv_inch, start_sec, stop_sec);
                    }
                }

                //                src_listView.Items[src_listView.Items.Count - 1].Selected = true;
                console_msg_tb.AppendText("Add:" + id.ToString() + ". " + cFileInfo.Directory.Name + "\r\n");
            }
        }

        bool server_start = false;
        Thread udpstart_thread = null;
        bool vrserver_start = false;
        private void server_start_btn_Click(object sender, EventArgs e)
        {
            VRServer vrserver = null;
            frame_size = System.Convert.ToInt32(frame_size_tb.Text);
            if(server_start)
            {
                server_start = false;
                myasio.set_stop(true);
                server_start_btn.Text = "Start";
                while (myasio.get_stop() == false) Thread.Sleep(1);
                if (send_cb.Checked)
                {
                    myasio.dispose_udp_sender();
                }
                if (receive_cb.Checked)
                {
                    if(vrserver != null) vrserver.Server_stop();
                    myasio.dispose_udp_receiver();
                }
            }
            else
            {
                int port_no = System.Convert.ToInt32(port_no_tb.Text);
                if (send_cb.Checked)
                {
                    server_start = true;
                    int send_asio_ch = 1;
                    //1. c++: UdpClient送信用オブジェクトを作成する
                    unsafe
                    {
                        bool ok_flg = false;
                        fixed (Char* ss = ipaddress_tb.Text.ToCharArray())
                        {
                            ok_flg = myasio.create_udp_sender(ss, port_no, send_asio_ch, send_sin1k_cb.Checked);
                        }
                        if (ok_flg == false)
                        {
                            string error_message = new String(myasio.error_message());
                            console_msg_tb.AppendText("Error: create_udp_sender : " + error_message + "\r\n");
                            return;
                        }
                    }
                }
               if(receive_cb.Checked)
                {
                    server_start = true;
                    int max_source_count = 1;
                    bool is_udp_mode = true;
                    vrserver = new VRServer(parent, null, console_msg_tb);
                    vrserver_start = vrserver.Server_start(mic_posi_cb.SelectedIndex, frame_size, max_source_count, is_udp_mode);
                    //2. c# : UdpClient受信用を作成し、ローカルエンドポイントにバインドする
                    //3. c# : Udp受信スレッドを起動、受け取ったデータはudp fifoに書き込む
                    int fifo_delay = System.Convert.ToInt32(fifo_delay_tb.Text);
                    bool ok_flg = myasio.create_udp_receiver(port_no, fifo_delay);
                    if (ok_flg == false)
                    {
                        unsafe
                        {
                            string error_message = new String(myasio.error_message());
                            console_msg_tb.AppendText("Error: create_udp_receiver : " + error_message + "\r\n");
                        }
                        return;
                    }
                     udpstart_thread = new Thread(new ThreadStart(UdpReceiveClientStart))
                    {
                        Name = "UdpReceiveClientStart"
                    };
                    udpstart_thread.Start();
                }

                if (send_cb.Checked || receive_cb.Checked)
                {
                    //5. asioのPlayを実行。callbackではオーディオ入力をudpに出力、udp FIFOをオーディオ出力する。
                    if (myasio.get_stop() == true)
                    {
                        server_start_btn.Text = "Stop";

                        parent.rtconv_zitter_lbl.Text = "Real Time Convolution : READY";
                        parent.rtconv_zitter_lbl.Visible = true;
                        parent.repeat_mode_cb.Checked = true;
                        myasio.set_repeat_mode(true);

                        Thread playstart_thread = new Thread(new ThreadStart(PlayStart));
                        playstart_thread.Name = "PlayStart";
                        playstart_thread.Start();

                        Thread controlstart_thread = new Thread(new ThreadStart(ControlStart));
                        controlstart_thread.Name = "ControlStart";
                        controlstart_thread.Start();
                    }
                }
            }
        }
        private int id_selected = -1;
        public void PlayStart()
        {
            parent.menuItem_Exit.Enabled = false;
           //if (myasio.initialize() < 0) goto asio_error;
            if (myasio.create_buffers() < 0) goto asio_error;
            if (myasio.start() < 0) goto asio_error;
            parent.Cnsl_msg("ASIO start --->");
            if (myasio.stop() < 0) goto asio_error;
            parent.Cnsl_msg(" stop\r\n");
            myasio.dispose_buffers();
            double zz = parent.zitter_level;
            parent.Cnsl_msg("Zitter Level=" + zz.ToString("0.0") + "\r\n");
            if (parent.total_error > 0)
            {
                for (int i = 0; i < myasio.rt_fft_level(); i++)
                {
                    int no = i + 1;
                    int ecnt = myasio.rtconv_error_cnt(i);
                    if (ecnt > 0)
                    {
                        parent.Cnsl_msg("FFT level=" + no.ToString() + ", Error count=" + ecnt.ToString() + "\r\n");
                    }
                }
            }
            parent.ResetButtonStatus(true, true);
            parent.menuItem_Exit.Enabled = true;
            this.Invalidate();
            return;

            asio_error:
            String error_msg = "";
            unsafe
            {
                error_msg = new String(myasio.error_message());
            }
            parent.Cnsl_msg(error_msg + "\r\n");
            return;
        }
        public void ControlStart()
        {
            double sfreq_1 = 1.0 / (double)parent.sample_freq;

            while (!myasio.get_stop())
            {
                int file_point = 0;
                int word_count = 0;
                while (word_count == 0)
                {
                    Thread.Sleep(100);
                    word_count = myasio.stream_word_count();
                    file_point = myasio.stream_file_point();
                }
                progress_tb.Value = file_point * 100 / word_count;
                int cur_sec = (int)(sfreq_1 * (double)file_point);
                int cur_min = cur_sec / 60;
                cur_sec = cur_sec % 60;
                int tot_sec = (int)(sfreq_1 * (double)word_count);
                int tot_min = tot_sec / 60;
                tot_sec = tot_sec % 60;
                current_time_lbl.Text = cur_min.ToString("00") + ":" + cur_sec.ToString("00")
                                + "/" + tot_min.ToString("00") + ":" + tot_sec.ToString("00");
                parent.Write_zitter_level();
                this.Invalidate();
            }
            progress_tb.Value = 0;
        }
        void UdpReceiveClientStart()
        {
            int udp_receive_word = 0;
            while (server_start)
            {
                int ret = myasio.udp_receive();
                if (!server_start) break;
                float max_val = myasio.udp_max_val();
                float min_val = myasio.udp_min_val();
                udp_receive_word += ret;
                udp_info_lbl.Text = "Recv word count = " + udp_receive_word.ToString() + ", Fifo max/min = " + max_val.ToString("0.0") + "/" + min_val.ToString("0.0");
            }
        }

        private void send_cb_CheckedChanged(object sender, EventArgs e)
        {
            set_button_status();
            server_start_btn.Enabled = set_btn_ok();
        }

        private void receive_cb_CheckedChanged(object sender, EventArgs e)
        {
            set_button_status();
        }

        bool src_attached = false;
        private void src_attach_btn_Click(object sender, EventArgs e)
        {
            if(src_attached)
            {
                src_attached = false;
                src_attach_btn.Text = "Attach";
                myasio.attach_song(-1);
            }
            else
            {
                src_attach_btn.Text = "Detach";
                int id = src_listView.SelectedIndices[0];
                myasio.attach_song(id);
                src_attached = true;
            }
        }

        private void src_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            src_attach_btn.Enabled = false;
            if (src_listView.SelectedIndices.Count == 0) return;
            int id = src_listView.SelectedIndices[0];
            id_selected = src_listView.Items[id].Index;
            ListViewItem itm = src_listView.Items[id];
            int song_no = id_selected + 1;
            src_attach_btn.Enabled = true;
            console_msg_tb.AppendText("Select:" + song_no.ToString() + ". " + itm.SubItems[1].Text + "\r\n");
        }

        private void mic_posi_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mic_posi_cb.SelectedIndex == 2)
            {
                if (parent.out_btn_cnt >= 96)
                {
                    rtconv_inch = 96;
                }
                else
                {
                    mic_posi_cb.SelectedIndex = 1;
                }
            }
            else if (mic_posi_cb.SelectedIndex == 1)
            {
                if (parent.out_btn_cnt >= 80)
                {
                    rtconv_inch = 80;
                }
                else
                {
                    mic_posi_cb.SelectedIndex = 0;
                }
            }
            else
            {
                rtconv_inch = 2;
            }
        }

        private void src_rm_btn_Click(object sender, EventArgs e)
        {
            myasio.play_list_remove(id_selected);
        }
    }
}
