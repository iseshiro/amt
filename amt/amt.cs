using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
using System.Management;
using System.Linq;
using Asio_io;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Net.Sockets;

namespace amt
{ 
   public class Amt : System.Windows.Forms.Form
    {
        public string ver_rev = "ver. 2.2 rev. 0";

        #region Form declation
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem_File;
        private System.Windows.Forms.MenuItem menuItem_Data;
        public System.Windows.Forms.MenuItem menuItem_Exit;
        private System.Windows.Forms.MenuItem menuItem_workfolder;
        private System.Windows.Forms.MenuItem menuItem_Save;
        private System.Windows.Forms.MenuItem menuItem_Help;
        private System.Windows.Forms.MenuItem menuItem_Version;
        private System.Windows.Forms.MenuItem menuItem_FFTpoint;

        private System.Windows.Forms.TextBox ConsoleMsg;
        private IContainer components;
        private System.Windows.Forms.Label in_title;
        private System.Windows.Forms.Label out_title;
        public System.Windows.Forms.Button go_btn;

        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem_set_as;
        private System.Windows.Forms.MenuItem menuItem_clear;
        private System.Windows.Forms.MenuItem menuItem_import;
        private System.Windows.Forms.MenuItem menuItem_setdata1;
        private System.Windows.Forms.MenuItem menuItem_setdata2;
        private System.Windows.Forms.MenuItem menuItem_setdata3;
        private System.Windows.Forms.MenuItem menuItem_export;
        private System.Windows.Forms.MenuItem menuItem_delete;
        private System.Windows.Forms.Label d1_lbl;
        private System.Windows.Forms.Label d3_lbl;
        private System.Windows.Forms.Label d2_lbl;
        private System.Windows.Forms.Label[] data_lbl;
        #endregion

        public asio_io asio = null;
        public ChannelInfo[] data_ch;

        private int btn_left;
        private System.Windows.Forms.MenuItem menuItem_movef;

        public enum BufferStatus { empty, buffer, signal, sn_perfect, sn_good, sn_bad };
        //public enum FileFormat {   // See asio_io.h
        //    wav_format_error = -1,
        //    wav_format_int16_1ch = 0,
        //    wav_format_int24_1ch = 1,
        //    wav_format_float32_1ch = 2,
        //    binary_format_double_1ch = 3
        //};

        public enum ASIOmode
        {
            asiomode_normal = 0,
            //asiomode_wav_stream_read = 1,
            asiomode_stream_out = 2,
            //asiomode_double_stream_read = 3,
            asiomode_rtconv_io = 4,
            asiomode_stream_in_rtconv_out = 5,
            asiomode_rtmove = 6,
            asiomode_binary_out = 7,
            asiomode_udp = 8
        };

        public string reply_msg;
        private System.Windows.Forms.MenuItem menuItem_copyfrom;
        private System.Windows.Forms.MenuItem menuItem_copyfrom1;
        private System.Windows.Forms.MenuItem menuItem_copyfrom2;
        private System.Windows.Forms.MenuItem menuItem_copyfrom3;
        private System.Windows.Forms.MenuItem menuItem_subtract;
        private System.Windows.Forms.MenuItem menuItem_subtract1;
        private System.Windows.Forms.MenuItem menuItem_subtract2;
        private System.Windows.Forms.MenuItem menuItem_subtract3;
        private System.Windows.Forms.MenuItem menuItem_divide;
        private System.Windows.Forms.MenuItem menuItem_divide1;
        private System.Windows.Forms.MenuItem menuItem_divide2;
        private System.Windows.Forms.MenuItem menuItem_divide3;
        private System.Windows.Forms.MenuItem menuItem_bin_import;
        private System.Windows.Forms.MenuItem menuItem_wav_conv;
        public int fftpoint = 2;
        public string selected_driver_name = "";
        public bool ok_flg = false;
        public Property myProperty;
        delegate void SetTextCallback(string text);
        private MenuItem menuItem_adjustamp;
        public Label adda_point_lbl;
        public TrackBar adda_point_tb;
        private ComboBox sampl_freq_cb;
        private Label sfreq_lbl;
        private int[] sfreq_list = new int[11] { 192000, 96000, 88200, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 8000 };
        //public enum InOutMode { memory, wav_stream_read, wav_stream_write, double_stream_read };
        //private InOutMode io_mode = InOutMode.memory;

        private MenuItem menuItem_wav_import;
        private MenuItem menuItem_dec_import;
        private MenuItem menuItem_hex_import;
        private MenuItem menuItem_wavs_export;
        private MenuItem menuItem_dec_export;
        private MenuItem menuItem_hex_export;
        private TextBox stream_wcnt_tb;
        private Label stream_wcnt_lbl;
        private Label button_top_lbl;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel mode_lbl;
        private ToolStripStatusLabel dleng_lbl;
        public int pysical_memory_sizeKB;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox stream_read_amp_tb;
        private Label stream_read_amp_lbl;
        public CheckBox repeat_mode_cb;
        public string work_folder = "";
        public string app_file_path = "";
        public int default_dleng = 0;
        public double default_start_time_ms = 0.0;
        public double default_end_time_ms = 188;
        public double default_grid_time_ms = 25;
        public double default_magnify = 1.0;
        public double default_start_freq_hz = 0;
        public double default_end_freq_hz = 12000;
        public double default_grid_freq_hz = 1000;
        public int default_fft_point = 8192;
        public bool default_amp_log = false;
        public int default_disp_range_db = 100;
        public int default_grid_range_db = 10;
        public int default_level_adjust_db = 0;
        private Label stream_hhmmss_lbl;
        public int max_ch = 0;
        public Label rtconv_zitter_lbl;
        public ChannelInfo[] ch_info_in;
        public ChannelInfo[] ch_info_out;
        public ChannelInfo[] ch_info_work;
        private MenuItem menuItem_wavf_export;
        public int max_row_of_button = 16;
        public int in_btn_cnt;
        public int out_btn_cnt;
        public int work_btn_cnt = 6;
        public bool assign_ch = false;
        public string assign_in_ch = "";
        public string assign_out_ch = "";
        private bool btn_not_ready = true;
        public ChannelInfo ch_info_vol;
        public double zitter_level = 0.0;
        public int total_error = 0;
        public bool rtmove_on = false;
        public FileDialog1 fd = null;
        public string filter_db_file = "";
        public string song_db_file = "";
        public string contents_db_file = "";
        public string inv_file = "";
        public int stream_word_count;
        public int sample_freq = 48000;
        public ArrayList started_thread_list = new ArrayList();

        private MenuItem menuItem_verify;
        private MenuItem menuItem_verify1;
        private MenuItem menuItem_verify2;
        private MenuItem menuItem_verify3;
        private MenuItem menuItem3;
        private MenuItem menuItem6;
        private MenuItem menuItem8;
        private MenuItem menuItem7;
        private MenuItem menuItem_ImpMeas;
        private MenuItem menuItem_spgain;
        public Label max_val_lbl;
        private int mkl_max_thread = 0;
        private MenuItem menuItem_rtconv;
        public ConvSet convset = null;
        public bool has_dampgainch = false;
        public ToolStripProgressBar toolStripProgressBar1;
        public bool without_asio = true;
        private MenuItem menuItem_ueserver;

        public Amt(String[] args)
        {
            asio = new asio_io();
            fd = new FileDialog1();
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();
            //
            //
            this.Text = "Acoustic Measurement Tool " + ver_rev;
            pysical_memory_sizeKB = GetPhysicalMemoryKB();

            myProperty = new Property(this);
            myProperty.Load(true);

            FirstSettingForm cdata = new FirstSettingForm(this, mkl_max_thread);
            cdata.ShowDialog();
            // These need ASIO; leave them disabled when started without ASIO.
            menuItem_rtconv.Enabled = false;
            menuItem_ueserver.Enabled = !without_asio;
            menuItem_ImpMeas.Enabled = !without_asio;
            menuItem_spgain.Enabled = !without_asio;
            if (without_asio == false)
            {
                if (asio.initialize() < 0) asio_error_exit();
                sample_freq = asio.get_sfreq();
                menuItem_rtconv.Enabled = asio.rtconv_check();
                stream_wcnt_tb.Text = sample_freq.ToString();

                int idnum = -1;
                int id = 0;
                for (int i = 0; i < sfreq_list.Length; i++)
                {
                    int isfreq = sfreq_list[i];
                    if (asio.can_sfreq(isfreq))
                    {
                        sampl_freq_cb.Items.Add(isfreq.ToString());
                        if (isfreq == sample_freq) idnum = id;
                        id++;
                    }
                }
                if (idnum >= 0) sampl_freq_cb.SelectedIndex = idnum;
                else
                {
                    sampl_freq_cb.Items.Add(sample_freq.ToString());
                    sampl_freq_cb.SelectedIndex = id;
                }
            }
            else
            {
                int[] without_asio_sfreq = { 44100, 48000 };
                for(int i=0;i<without_asio_sfreq.Length;i++)
                {
                    sampl_freq_cb.Items.Add(without_asio_sfreq[i].ToString());
                }
                sampl_freq_cb.SelectedIndex = 1;
                sample_freq = System.Convert.ToInt32(sampl_freq_cb.SelectedItem);
            }
            Create_ch_info(in_btn_cnt, out_btn_cnt, work_btn_cnt);
            dleng_lbl.Text = " Data length = " + asio.GetDataLength().ToString() + "   ";

            while (fftpoint < asio.GetDataLength()) fftpoint *= 2;

            CheckForIllegalCrossThreadCalls = false;

            Setup_mainform();

            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;
        }
        public void asio_error_exit()
        {
            String error_msg = "";
            unsafe
            {
                error_msg = new String(asio.error_message());
            }
            System.Windows.Forms.MessageBox.Show("ASIO ERROR :" + error_msg);
            System.Environment.Exit(0);
        }
        public ArrayList Separate_arg(string cmd_text)
        {
            ArrayList args = new ArrayList();
            string arg = "";
            int mode = 0, index = 0, leng = 0;
            for (int i = 0; i < cmd_text.Length; i++)
            {
                char cc = cmd_text[i];
                if (cc == '"')
                {
                    if (mode == 0)
                    {
                        mode = 1;
                        index = i + 1;
                    }
                    else
                    {
                        mode = 0;
                    }
                }
                else
                {
                    if (mode == 0)
                    {
                        if (cc == ' ')
                        {
                            arg = cmd_text.Substring(index, leng);
                            args.Add(arg);
                            leng = 0;
                            index = i + 1;
                        }
                        else
                        {
                            leng++;
                        }
                    }
                    else
                    {
                        leng++;
                    }
                }
            }
            arg = cmd_text.Substring(index, leng);
            args.Add(arg);
            return args;
        }



        public ChannelInfo Find_ch_info(string buff_type, int btn_no)
        {
            switch (buff_type)
            {
                case "IN":
                    return ch_info_in[btn_no - 1];
                case "OUT":
                    return ch_info_out[btn_no - 1];
                case "WORK":
                    return ch_info_work[btn_no - 1];
            }
            return null;
        }

        private ChannelInfo Find_ch_info(Button btn)
        {
            int btn_no = Find_btn_no(btn);
            switch (btn.Name)
            {
                case "IN":
                    return ch_info_in[btn_no];
                case "OUT":
                    return ch_info_out[btn_no];
            }
            return null;
        }

        private int Find_btn_no(Button btn)
        {
            int xx = btn.Location.X;
            int yy = btn.Location.Y;
            int y_in = button_top_lbl.Location.Y;
            int btn_intvl = 4 + btn.Size.Width;

            int i_yoko_0, i_tate_0, i_tate, i_yoko, btn_no1;
            switch (btn.Name)
            {
                case "IN":
                    i_yoko_0 = ch_info_in[0].btn.Location.X;
                    i_tate_0 = ch_info_in[0].btn.Location.Y;
                    i_tate = (yy - i_tate_0) / 32;
                    i_yoko = (xx - i_yoko_0) / btn_intvl;
                    btn_no1 = max_row_of_btn * i_tate + i_yoko;
                    return btn_no1;
                case "OUT":
                    i_yoko_0 = ch_info_out[0].btn.Location.X;
                    i_tate_0 = ch_info_out[0].btn.Location.Y;
                    i_tate = (yy - i_tate_0) / 32;
                    i_yoko = (xx - i_yoko_0) / btn_intvl;
                    btn_no1 = max_row_of_btn * i_tate + i_yoko;
                    return btn_no1;
            }
            return -1;
        }
 
        public void Ch_info_clear()
        {
            for (int i = 0; i < in_btn_cnt; i++)
            {
                ch_info_in[i].FreqLevel_clear();
            }
        }
        //public int Inch(int ch) { return ch; }
        //public int Outch(int ch) { return asio.in_ch_cnt + ch; }
        //public int Workch(int ch) { return asio.in_ch_cnt + asio.out_ch_cnt + ch; }

//        public int sample_freq { return sample_freq; }
        public int Get_time_point(double time_ms) {
            return (int)(0.001 * time_ms * (double)sample_freq + 0.5);
        }
        public int Get_freq_point(double freq, int fftpnt)
        {
            return (int)((double)fftpnt * freq / sample_freq + 0.5);
        }

        public double Get_freq_hz(int point, int fftpnt)
        {
            return (double)sample_freq * (double)point / (double)fftpnt;
        }

        public int Get_max_time_point()
        {
            return asio.GetDataLength();
        }

        public double Get_time_ms(int time_point)
        {
            return (double)time_point * 1000.0 / sample_freq;
        }

        public int GetPhysicalMemoryKB()
        {
            string important_memsize_text = "0";

            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc) {
                ////���v����������
                //cnsl_msg("���v����������:"+mo["TotalVisibleMemorySize"]+"KB\r\n");
                ////���p�\�ȕ���������
                //cnsl_msg("���p�\����������:" + mo["FreePhysicalMemory"] + "KB\r\n");
                ////���v���z������
                //cnsl_msg("���v���z������:" + mo["TotalVirtualMemorySize"] + "KB\r\n");
                ////���p�\�ȉ��z������
                //cnsl_msg("���p�\���z������:" + mo["FreeVirtualMemory"] + "KB\r\n");

                important_memsize_text = mo["FreeSpaceInPagingFiles"].ToString();
                ////���̃y�[�W���X���b�v�A�E�g�����Ƀy�[�W���O�t�@�C���Ƀ}�b�v�ł���T�C�Y
                //cnsl_msg("FreeSpaceInPagingFiles:" + important_memsize_text + "KB\r\n");
                ////�y�[�W���O�t�@�C���ɕۑ��ł��鍇�v�T�C�Y
                //cnsl_msg("SizeStoredInPagingFiles:" + mo["SizeStoredInPagingFiles"] + "KB\r\n");
                ////�X���b�v�X�y�[�X�̍��v�T�C�Y
                ////�X���b�v�X�y�[�X�ƃy�[�W���O�t�@�C������ʂ���Ă��Ȃ���΁ANULL
                //if (mo["TotalSwapSpaceSize"] != null)
                //    cnsl_msg("TotalSwapSpaceSize:" + mo["TotalSwapSpaceSize"] + "KB");
            }

            return System.Convert.ToInt32(important_memsize_text);
        }

        public void Cnsl_msg(String ss)
        {
            if (this.ConsoleMsg.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Cnsl_msg);
                this.Invoke(d, new object[] { ss });
            }
            else
            {
                ConsoleMsg.AppendText(ss);
            }
        }

        private void MenuSelect(object sender, System.EventArgs e)
        {
            MessageBox.Show("A file has been opened.",
                "Instead of a new file, here's a message box.");
            //	LoadSecondMenu();
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (myProperty != null)
            {
                myProperty.Save();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Amt));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem_File = new System.Windows.Forms.MenuItem();
            this.menuItem_workfolder = new System.Windows.Forms.MenuItem();
            this.menuItem_Save = new System.Windows.Forms.MenuItem();
            this.menuItem_Exit = new System.Windows.Forms.MenuItem();
            this.menuItem_Data = new System.Windows.Forms.MenuItem();
            this.menuItem_FFTpoint = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem_spgain = new System.Windows.Forms.MenuItem();
            this.menuItem_rtconv = new System.Windows.Forms.MenuItem();
            this.menuItem_ueserver = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem_ImpMeas = new System.Windows.Forms.MenuItem();
            this.menuItem_Help = new System.Windows.Forms.MenuItem();
            this.menuItem_Version = new System.Windows.Forms.MenuItem();
            this.ConsoleMsg = new System.Windows.Forms.TextBox();
            this.go_btn = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem_set_as = new System.Windows.Forms.MenuItem();
            this.menuItem_setdata1 = new System.Windows.Forms.MenuItem();
            this.menuItem_setdata2 = new System.Windows.Forms.MenuItem();
            this.menuItem_setdata3 = new System.Windows.Forms.MenuItem();
            this.menuItem_clear = new System.Windows.Forms.MenuItem();
            this.menuItem_movef = new System.Windows.Forms.MenuItem();
            this.menuItem_delete = new System.Windows.Forms.MenuItem();
            this.menuItem_import = new System.Windows.Forms.MenuItem();
            this.menuItem_wav_import = new System.Windows.Forms.MenuItem();
            this.menuItem_dec_import = new System.Windows.Forms.MenuItem();
            this.menuItem_hex_import = new System.Windows.Forms.MenuItem();
            this.menuItem_export = new System.Windows.Forms.MenuItem();
            this.menuItem_wavs_export = new System.Windows.Forms.MenuItem();
            this.menuItem_wavf_export = new System.Windows.Forms.MenuItem();
            this.menuItem_dec_export = new System.Windows.Forms.MenuItem();
            this.menuItem_hex_export = new System.Windows.Forms.MenuItem();
            this.menuItem_copyfrom = new System.Windows.Forms.MenuItem();
            this.menuItem_copyfrom1 = new System.Windows.Forms.MenuItem();
            this.menuItem_copyfrom2 = new System.Windows.Forms.MenuItem();
            this.menuItem_copyfrom3 = new System.Windows.Forms.MenuItem();
            this.menuItem_subtract = new System.Windows.Forms.MenuItem();
            this.menuItem_subtract1 = new System.Windows.Forms.MenuItem();
            this.menuItem_subtract2 = new System.Windows.Forms.MenuItem();
            this.menuItem_subtract3 = new System.Windows.Forms.MenuItem();
            this.menuItem_verify = new System.Windows.Forms.MenuItem();
            this.menuItem_verify1 = new System.Windows.Forms.MenuItem();
            this.menuItem_verify2 = new System.Windows.Forms.MenuItem();
            this.menuItem_verify3 = new System.Windows.Forms.MenuItem();
            this.menuItem_divide = new System.Windows.Forms.MenuItem();
            this.menuItem_divide1 = new System.Windows.Forms.MenuItem();
            this.menuItem_divide2 = new System.Windows.Forms.MenuItem();
            this.menuItem_divide3 = new System.Windows.Forms.MenuItem();
            this.menuItem_bin_import = new System.Windows.Forms.MenuItem();
            this.menuItem_wav_conv = new System.Windows.Forms.MenuItem();
            this.menuItem_adjustamp = new System.Windows.Forms.MenuItem();
            this.d1_lbl = new System.Windows.Forms.Label();
            this.d3_lbl = new System.Windows.Forms.Label();
            this.d2_lbl = new System.Windows.Forms.Label();
            this.adda_point_lbl = new System.Windows.Forms.Label();
            this.adda_point_tb = new System.Windows.Forms.TrackBar();
            this.sampl_freq_cb = new System.Windows.Forms.ComboBox();
            this.sfreq_lbl = new System.Windows.Forms.Label();
            this.stream_wcnt_tb = new System.Windows.Forms.TextBox();
            this.stream_wcnt_lbl = new System.Windows.Forms.Label();
            this.button_top_lbl = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mode_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.dleng_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.stream_read_amp_tb = new System.Windows.Forms.TextBox();
            this.stream_read_amp_lbl = new System.Windows.Forms.Label();
            this.repeat_mode_cb = new System.Windows.Forms.CheckBox();
            this.stream_hhmmss_lbl = new System.Windows.Forms.Label();
            this.rtconv_zitter_lbl = new System.Windows.Forms.Label();
            this.max_val_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.adda_point_tb)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_File,
            this.menuItem_Data,
            this.menuItem3,
            this.menuItem6,
            this.menuItem7,
            this.menuItem_Help});
            // 
            // menuItem_File
            // 
            this.menuItem_File.Index = 0;
            this.menuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_workfolder,
            this.menuItem_Save,
            this.menuItem_Exit});
            this.menuItem_File.Text = "File";
            // 
            // menuItem_workfolder
            // 
            this.menuItem_workfolder.Index = 0;
            this.menuItem_workfolder.Text = "Work Folder";
            this.menuItem_workfolder.Click += new System.EventHandler(this.MenuItem_workfolder_Click);
            // 
            // menuItem_Save
            // 
            this.menuItem_Save.Index = 1;
            this.menuItem_Save.Text = "Save";
            this.menuItem_Save.Click += new System.EventHandler(this.MenuItem_Save_Click);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Index = 2;
            this.menuItem_Exit.Text = "Exit";
            this.menuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menuItem_Data
            // 
            this.menuItem_Data.Index = 1;
            this.menuItem_Data.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_FFTpoint});
            this.menuItem_Data.Text = "Set";
            // 
            // menuItem_FFTpoint
            // 
            this.menuItem_FFTpoint.Index = 0;
            this.menuItem_FFTpoint.Text = "FFTpoint";
            this.menuItem_FFTpoint.Click += new System.EventHandler(this.MenuItem_FFTpoint_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_spgain,
            this.menuItem_rtconv,
            this.menuItem_ueserver});
            this.menuItem3.Text = "BoSC";
            // 
            // menuItem_spgain
            // 
            this.menuItem_spgain.Index = 0;
            this.menuItem_spgain.Text = "Sp. Amp. Gain";
            this.menuItem_spgain.Click += new System.EventHandler(this.MenuItem_spgain_Click);
            // 
            // menuItem_rtconv
            // 
            this.menuItem_rtconv.Index = 1;
            this.menuItem_rtconv.Text = "Realtime Conv.";
            this.menuItem_rtconv.Click += new System.EventHandler(this.MenuItem_rtconv_aiao_Click);
            // 
            // menuItem_ueserver
            // 
            this.menuItem_ueserver.Index = 2;
            this.menuItem_ueserver.Text = "VR Server";
            this.menuItem_ueserver.Click += new System.EventHandler(this.menuItem_ueserver_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8});
            this.menuItem6.Text = "Analysis";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 0;
            this.menuItem8.Text = "Rev. Time";
            this.menuItem8.Click += new System.EventHandler(this.MenuItem_RevTime_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_ImpMeas});
            this.menuItem7.Text = "Measurement";
            // 
            // menuItem_ImpMeas
            // 
            this.menuItem_ImpMeas.Index = 0;
            this.menuItem_ImpMeas.Text = "IR Measurement";
            this.menuItem_ImpMeas.Click += new System.EventHandler(this.MenuItem_ImpMeas_Click);
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.Index = 5;
            this.menuItem_Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Version});
            this.menuItem_Help.Text = "Help";
            // 
            // menuItem_Version
            // 
            this.menuItem_Version.Index = 0;
            this.menuItem_Version.Text = "Version";
            this.menuItem_Version.Click += new System.EventHandler(this.MenuItem_Version_Click);
            // 
            // ConsoleMsg
            // 
            this.ConsoleMsg.Location = new System.Drawing.Point(12, 362);
            this.ConsoleMsg.Margin = new System.Windows.Forms.Padding(4);
            this.ConsoleMsg.Multiline = true;
            this.ConsoleMsg.Name = "ConsoleMsg";
            this.ConsoleMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleMsg.Size = new System.Drawing.Size(586, 140);
            this.ConsoleMsg.TabIndex = 0;
            // 
            // go_btn
            // 
            this.go_btn.Enabled = false;
            this.go_btn.Location = new System.Drawing.Point(230, 28);
            this.go_btn.Margin = new System.Windows.Forms.Padding(4);
            this.go_btn.Name = "go_btn";
            this.go_btn.Size = new System.Drawing.Size(144, 48);
            this.go_btn.TabIndex = 31;
            this.go_btn.Text = "AD/DA Start";
            this.go_btn.Click += new System.EventHandler(this.Go_btn_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_set_as,
            this.menuItem_clear,
            this.menuItem_movef,
            this.menuItem_delete,
            this.menuItem_import,
            this.menuItem_export,
            this.menuItem_copyfrom,
            this.menuItem_subtract,
            this.menuItem_verify,
            this.menuItem_divide,
            this.menuItem_bin_import,
            this.menuItem_wav_conv,
            this.menuItem_adjustamp});
            // 
            // menuItem_set_as
            // 
            this.menuItem_set_as.Index = 0;
            this.menuItem_set_as.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_setdata1,
            this.menuItem_setdata2,
            this.menuItem_setdata3});
            this.menuItem_set_as.Text = "Set as";
            // 
            // menuItem_setdata1
            // 
            this.menuItem_setdata1.Index = 0;
            this.menuItem_setdata1.Text = "Data 1";
            this.menuItem_setdata1.Click += new System.EventHandler(this.MenuItem_setdata1_Click);
            // 
            // menuItem_setdata2
            // 
            this.menuItem_setdata2.Index = 1;
            this.menuItem_setdata2.Text = "Data 2";
            this.menuItem_setdata2.Click += new System.EventHandler(this.MenuItem_setdata1_Click);
            // 
            // menuItem_setdata3
            // 
            this.menuItem_setdata3.Index = 2;
            this.menuItem_setdata3.Text = "Data 3";
            this.menuItem_setdata3.Click += new System.EventHandler(this.MenuItem_setdata1_Click);
            // 
            // menuItem_clear
            // 
            this.menuItem_clear.Index = 1;
            this.menuItem_clear.Text = "Clear";
            this.menuItem_clear.Click += new System.EventHandler(this.MenuItem_clear_Click);
            // 
            // menuItem_movef
            // 
            this.menuItem_movef.Index = 2;
            this.menuItem_movef.Text = "Move";
            this.menuItem_movef.Click += new System.EventHandler(this.MenuItem_movef_Click);
            // 
            // menuItem_delete
            // 
            this.menuItem_delete.Index = 3;
            this.menuItem_delete.Text = "Delete";
            this.menuItem_delete.Click += new System.EventHandler(this.MenuItem_delete_Click);
            // 
            // menuItem_import
            // 
            this.menuItem_import.Index = 4;
            this.menuItem_import.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_wav_import,
            this.menuItem_dec_import,
            this.menuItem_hex_import});
            this.menuItem_import.Text = "Import";
            // 
            // menuItem_wav_import
            // 
            this.menuItem_wav_import.Index = 0;
            this.menuItem_wav_import.Text = "wav";
            this.menuItem_wav_import.Click += new System.EventHandler(this.MenuItem_wav_import_Click);
            // 
            // menuItem_dec_import
            // 
            this.menuItem_dec_import.Index = 1;
            this.menuItem_dec_import.Text = "text (dec)";
            this.menuItem_dec_import.Click += new System.EventHandler(this.MenuItem_dec_import_Click);
            // 
            // menuItem_hex_import
            // 
            this.menuItem_hex_import.Index = 2;
            this.menuItem_hex_import.Text = "text (hex)";
            this.menuItem_hex_import.Click += new System.EventHandler(this.MenuItem_hex_import_Click);
            // 
            // menuItem_export
            // 
            this.menuItem_export.Index = 5;
            this.menuItem_export.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_wavs_export,
            this.menuItem_wavf_export,
            this.menuItem_dec_export,
            this.menuItem_hex_export});
            this.menuItem_export.Text = "Export";
            // 
            // menuItem_wavs_export
            // 
            this.menuItem_wavs_export.Index = 0;
            this.menuItem_wavs_export.Text = "wav (short)";
            this.menuItem_wavs_export.Click += new System.EventHandler(this.MenuItem_wavs_export_Click);
            // 
            // menuItem_wavf_export
            // 
            this.menuItem_wavf_export.Index = 1;
            this.menuItem_wavf_export.Text = "wav (float)";
            this.menuItem_wavf_export.Click += new System.EventHandler(this.MenuItem_wavf_export_Click);
            // 
            // menuItem_dec_export
            // 
            this.menuItem_dec_export.Index = 2;
            this.menuItem_dec_export.Text = "text (dec)";
            this.menuItem_dec_export.Click += new System.EventHandler(this.MenuItem_dec_export_Click);
            // 
            // menuItem_hex_export
            // 
            this.menuItem_hex_export.Index = 3;
            this.menuItem_hex_export.Text = "text (hex)";
            this.menuItem_hex_export.Click += new System.EventHandler(this.MenuItem_hex_export_Click);
            // 
            // menuItem_copyfrom
            // 
            this.menuItem_copyfrom.Index = 6;
            this.menuItem_copyfrom.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_copyfrom1,
            this.menuItem_copyfrom2,
            this.menuItem_copyfrom3});
            this.menuItem_copyfrom.Text = "Copy from";
            // 
            // menuItem_copyfrom1
            // 
            this.menuItem_copyfrom1.Index = 0;
            this.menuItem_copyfrom1.Text = "Data 1";
            this.menuItem_copyfrom1.Click += new System.EventHandler(this.MenuItem_copyfrom1_Click);
            // 
            // menuItem_copyfrom2
            // 
            this.menuItem_copyfrom2.Index = 1;
            this.menuItem_copyfrom2.Text = "Data 2";
            this.menuItem_copyfrom2.Click += new System.EventHandler(this.MenuItem_copyfrom2_Click);
            // 
            // menuItem_copyfrom3
            // 
            this.menuItem_copyfrom3.Index = 2;
            this.menuItem_copyfrom3.Text = "Data 3";
            this.menuItem_copyfrom3.Click += new System.EventHandler(this.MenuItem_copyfrom3_Click);
            // 
            // menuItem_subtract
            // 
            this.menuItem_subtract.Index = 7;
            this.menuItem_subtract.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_subtract1,
            this.menuItem_subtract2,
            this.menuItem_subtract3});
            this.menuItem_subtract.Text = "Subtract from";
            // 
            // menuItem_subtract1
            // 
            this.menuItem_subtract1.Index = 0;
            this.menuItem_subtract1.Text = "Data 1";
            this.menuItem_subtract1.Click += new System.EventHandler(this.MenuItem_subtract1_Click);
            // 
            // menuItem_subtract2
            // 
            this.menuItem_subtract2.Index = 1;
            this.menuItem_subtract2.Text = "Data 2";
            this.menuItem_subtract2.Click += new System.EventHandler(this.MenuItem_subtract2_Click);
            // 
            // menuItem_subtract3
            // 
            this.menuItem_subtract3.Index = 2;
            this.menuItem_subtract3.Text = "Data 3";
            this.menuItem_subtract3.Click += new System.EventHandler(this.MenuItem_subtract3_Click);
            // 
            // menuItem_verify
            // 
            this.menuItem_verify.Index = 8;
            this.menuItem_verify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_verify1,
            this.menuItem_verify2,
            this.menuItem_verify3});
            this.menuItem_verify.Text = "Verify with";
            // 
            // menuItem_verify1
            // 
            this.menuItem_verify1.Index = 0;
            this.menuItem_verify1.Text = "Data 1";
            this.menuItem_verify1.Click += new System.EventHandler(this.MenuItem_verify1_Click);
            // 
            // menuItem_verify2
            // 
            this.menuItem_verify2.Index = 1;
            this.menuItem_verify2.Text = "Data 2";
            this.menuItem_verify2.Click += new System.EventHandler(this.MenuItem_verify2_Click);
            // 
            // menuItem_verify3
            // 
            this.menuItem_verify3.Index = 2;
            this.menuItem_verify3.Text = "Data 3";
            this.menuItem_verify3.Click += new System.EventHandler(this.MenuItem_verify3_Click);
            // 
            // menuItem_divide
            // 
            this.menuItem_divide.Index = 9;
            this.menuItem_divide.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_divide1,
            this.menuItem_divide2,
            this.menuItem_divide3});
            this.menuItem_divide.Text = "Divide by";
            // 
            // menuItem_divide1
            // 
            this.menuItem_divide1.Index = 0;
            this.menuItem_divide1.Text = "Data 1";
            this.menuItem_divide1.Click += new System.EventHandler(this.MenuItem_divide1_Click);
            // 
            // menuItem_divide2
            // 
            this.menuItem_divide2.Index = 1;
            this.menuItem_divide2.Text = "Data 2";
            this.menuItem_divide2.Click += new System.EventHandler(this.MenuItem_divide2_Click);
            // 
            // menuItem_divide3
            // 
            this.menuItem_divide3.Index = 2;
            this.menuItem_divide3.Text = "Data 3";
            this.menuItem_divide3.Click += new System.EventHandler(this.MenuItem_divide3_Click);
            // 
            // menuItem_bin_import
            // 
            this.menuItem_bin_import.Index = 10;
            this.menuItem_bin_import.Text = "Binary Import";
            this.menuItem_bin_import.Click += new System.EventHandler(this.MenuItem_bin_import_Click);
            // 
            // menuItem_wav_conv
            // 
            this.menuItem_wav_conv.Index = 11;
            this.menuItem_wav_conv.Text = "Conv. with wav";
            this.menuItem_wav_conv.Click += new System.EventHandler(this.MenuConvWav_Click);
            // 
            // menuItem_adjustamp
            // 
            this.menuItem_adjustamp.Index = 12;
            this.menuItem_adjustamp.Text = "Multiply ...";
            this.menuItem_adjustamp.Click += new System.EventHandler(this.MenuItem_multiply_val_Click);
            // 
            // d1_lbl
            // 
            this.d1_lbl.BackColor = System.Drawing.Color.Transparent;
            this.d1_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.d1_lbl.ForeColor = System.Drawing.Color.Maroon;
            this.d1_lbl.Location = new System.Drawing.Point(246, 285);
            this.d1_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.d1_lbl.Name = "d1_lbl";
            this.d1_lbl.Size = new System.Drawing.Size(60, 18);
            this.d1_lbl.TabIndex = 36;
            this.d1_lbl.Text = "data1";
            this.d1_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // d3_lbl
            // 
            this.d3_lbl.BackColor = System.Drawing.Color.Transparent;
            this.d3_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.d3_lbl.ForeColor = System.Drawing.Color.DarkOrange;
            this.d3_lbl.Location = new System.Drawing.Point(390, 285);
            this.d3_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.d3_lbl.Name = "d3_lbl";
            this.d3_lbl.Size = new System.Drawing.Size(60, 18);
            this.d3_lbl.TabIndex = 37;
            this.d3_lbl.Text = "data3";
            this.d3_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // d2_lbl
            // 
            this.d2_lbl.BackColor = System.Drawing.Color.Transparent;
            this.d2_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.d2_lbl.ForeColor = System.Drawing.Color.Red;
            this.d2_lbl.Location = new System.Drawing.Point(318, 285);
            this.d2_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.d2_lbl.Name = "d2_lbl";
            this.d2_lbl.Size = new System.Drawing.Size(60, 18);
            this.d2_lbl.TabIndex = 38;
            this.d2_lbl.Text = "data2";
            this.d2_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // adda_point_lbl
            // 
            this.adda_point_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adda_point_lbl.Location = new System.Drawing.Point(244, 118);
            this.adda_point_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.adda_point_lbl.Name = "adda_point_lbl";
            this.adda_point_lbl.Size = new System.Drawing.Size(124, 26);
            this.adda_point_lbl.TabIndex = 45;
            this.adda_point_lbl.Text = "0";
            this.adda_point_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adda_point_tb
            // 
            this.adda_point_tb.Location = new System.Drawing.Point(207, 82);
            this.adda_point_tb.Margin = new System.Windows.Forms.Padding(4);
            this.adda_point_tb.Maximum = 100;
            this.adda_point_tb.Name = "adda_point_tb";
            this.adda_point_tb.Size = new System.Drawing.Size(183, 69);
            this.adda_point_tb.TabIndex = 46;
            this.adda_point_tb.TickStyle = System.Windows.Forms.TickStyle.None;
            this.adda_point_tb.Scroll += new System.EventHandler(this.Adda_point_tb_Scroll);
            // 
            // sampl_freq_cb
            // 
            this.sampl_freq_cb.FormattingEnabled = true;
            this.sampl_freq_cb.Location = new System.Drawing.Point(416, 45);
            this.sampl_freq_cb.Margin = new System.Windows.Forms.Padding(4);
            this.sampl_freq_cb.Name = "sampl_freq_cb";
            this.sampl_freq_cb.Size = new System.Drawing.Size(150, 26);
            this.sampl_freq_cb.TabIndex = 48;
            this.sampl_freq_cb.SelectedIndexChanged += new System.EventHandler(this.Sampl_freq_cb_SelectedIndexChanged);
            // 
            // sfreq_lbl
            // 
            this.sfreq_lbl.AutoSize = true;
            this.sfreq_lbl.Location = new System.Drawing.Point(412, 22);
            this.sfreq_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sfreq_lbl.Name = "sfreq_lbl";
            this.sfreq_lbl.Size = new System.Drawing.Size(151, 18);
            this.sfreq_lbl.TabIndex = 49;
            this.sfreq_lbl.Text = "Sampling Freq [Hz]";
            // 
            // stream_wcnt_tb
            // 
            this.stream_wcnt_tb.Location = new System.Drawing.Point(651, 102);
            this.stream_wcnt_tb.Margin = new System.Windows.Forms.Padding(4);
            this.stream_wcnt_tb.Name = "stream_wcnt_tb";
            this.stream_wcnt_tb.Size = new System.Drawing.Size(150, 25);
            this.stream_wcnt_tb.TabIndex = 51;
            this.stream_wcnt_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.stream_wcnt_tb.Visible = false;
            this.stream_wcnt_tb.TextChanged += new System.EventHandler(this.Stream_wcnt_tb_TextChanged);
            // 
            // stream_wcnt_lbl
            // 
            this.stream_wcnt_lbl.AutoSize = true;
            this.stream_wcnt_lbl.Location = new System.Drawing.Point(651, 82);
            this.stream_wcnt_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stream_wcnt_lbl.Name = "stream_wcnt_lbl";
            this.stream_wcnt_lbl.Size = new System.Drawing.Size(150, 18);
            this.stream_wcnt_lbl.TabIndex = 52;
            this.stream_wcnt_lbl.Text = "Stream word count";
            this.stream_wcnt_lbl.Visible = false;
            // 
            // button_top_lbl
            // 
            this.button_top_lbl.Location = new System.Drawing.Point(116, 168);
            this.button_top_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.button_top_lbl.Name = "button_top_lbl";
            this.button_top_lbl.Size = new System.Drawing.Size(369, 21);
            this.button_top_lbl.TabIndex = 56;
            this.button_top_lbl.Text = "This label shows the top level of the buttons";
            this.button_top_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_top_lbl.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Font = new System.Drawing.Font("���C���I", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mode_lbl,
            this.dleng_lbl,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1102, 32);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 57;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mode_lbl
            // 
            this.mode_lbl.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.mode_lbl.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.mode_lbl.Name = "mode_lbl";
            this.mode_lbl.Size = new System.Drawing.Size(176, 25);
            this.mode_lbl.Text = "   Memory in/out   ";
            // 
            // dleng_lbl
            // 
            this.dleng_lbl.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.dleng_lbl.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.dleng_lbl.Name = "dleng_lbl";
            this.dleng_lbl.Size = new System.Drawing.Size(179, 25);
            this.dleng_lbl.Text = "   Length = 8192   ";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 24);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // stream_read_amp_tb
            // 
            this.stream_read_amp_tb.Location = new System.Drawing.Point(658, 45);
            this.stream_read_amp_tb.Margin = new System.Windows.Forms.Padding(4);
            this.stream_read_amp_tb.Name = "stream_read_amp_tb";
            this.stream_read_amp_tb.Size = new System.Drawing.Size(68, 25);
            this.stream_read_amp_tb.TabIndex = 58;
            this.stream_read_amp_tb.Text = "1.0";
            this.stream_read_amp_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.stream_read_amp_tb.Visible = false;
            // 
            // stream_read_amp_lbl
            // 
            this.stream_read_amp_lbl.AutoSize = true;
            this.stream_read_amp_lbl.Location = new System.Drawing.Point(651, 22);
            this.stream_read_amp_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stream_read_amp_lbl.Name = "stream_read_amp_lbl";
            this.stream_read_amp_lbl.Size = new System.Drawing.Size(82, 18);
            this.stream_read_amp_lbl.TabIndex = 59;
            this.stream_read_amp_lbl.Text = "Amplitude";
            this.stream_read_amp_lbl.Visible = false;
            // 
            // repeat_mode_cb
            // 
            this.repeat_mode_cb.AutoSize = true;
            this.repeat_mode_cb.Location = new System.Drawing.Point(416, 102);
            this.repeat_mode_cb.Margin = new System.Windows.Forms.Padding(4);
            this.repeat_mode_cb.Name = "repeat_mode_cb";
            this.repeat_mode_cb.Size = new System.Drawing.Size(87, 22);
            this.repeat_mode_cb.TabIndex = 60;
            this.repeat_mode_cb.Text = "Repeat";
            this.repeat_mode_cb.UseVisualStyleBackColor = true;
            this.repeat_mode_cb.CheckedChanged += new System.EventHandler(this.Repeat_mode_cb_CheckedChanged);
            // 
            // stream_hhmmss_lbl
            // 
            this.stream_hhmmss_lbl.AutoSize = true;
            this.stream_hhmmss_lbl.Location = new System.Drawing.Point(812, 108);
            this.stream_hhmmss_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stream_hhmmss_lbl.Name = "stream_hhmmss_lbl";
            this.stream_hhmmss_lbl.Size = new System.Drawing.Size(76, 18);
            this.stream_hhmmss_lbl.TabIndex = 61;
            this.stream_hhmmss_lbl.Text = "hh:mm:ss";
            this.stream_hhmmss_lbl.Visible = false;
            // 
            // rtconv_zitter_lbl
            // 
            this.rtconv_zitter_lbl.BackColor = System.Drawing.Color.Transparent;
            this.rtconv_zitter_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtconv_zitter_lbl.ForeColor = System.Drawing.Color.Maroon;
            this.rtconv_zitter_lbl.Location = new System.Drawing.Point(34, 138);
            this.rtconv_zitter_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rtconv_zitter_lbl.Name = "rtconv_zitter_lbl";
            this.rtconv_zitter_lbl.Size = new System.Drawing.Size(471, 30);
            this.rtconv_zitter_lbl.TabIndex = 63;
            this.rtconv_zitter_lbl.Text = "rtconv_zitter";
            this.rtconv_zitter_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // max_val_lbl
            // 
            this.max_val_lbl.BackColor = System.Drawing.Color.Transparent;
            this.max_val_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.max_val_lbl.ForeColor = System.Drawing.Color.Maroon;
            this.max_val_lbl.Location = new System.Drawing.Point(514, 138);
            this.max_val_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.max_val_lbl.Name = "max_val_lbl";
            this.max_val_lbl.Size = new System.Drawing.Size(471, 30);
            this.max_val_lbl.TabIndex = 64;
            this.max_val_lbl.Text = "max_val";
            this.max_val_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Amt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(1102, 560);
            this.ControlBox = false;
            this.Controls.Add(this.max_val_lbl);
            this.Controls.Add(this.rtconv_zitter_lbl);
            this.Controls.Add(this.stream_hhmmss_lbl);
            this.Controls.Add(this.repeat_mode_cb);
            this.Controls.Add(this.stream_read_amp_tb);
            this.Controls.Add(this.stream_read_amp_lbl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_top_lbl);
            this.Controls.Add(this.stream_wcnt_lbl);
            this.Controls.Add(this.stream_wcnt_tb);
            this.Controls.Add(this.sfreq_lbl);
            this.Controls.Add(this.sampl_freq_cb);
            this.Controls.Add(this.adda_point_lbl);
            this.Controls.Add(this.d2_lbl);
            this.Controls.Add(this.d3_lbl);
            this.Controls.Add(this.ConsoleMsg);
            this.Controls.Add(this.go_btn);
            this.Controls.Add(this.d1_lbl);
            this.Controls.Add(this.adda_point_tb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "Amt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acoustic Measurement Tool ";
            ((System.ComponentModel.ISupportInitialize)(this.adda_point_tb)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.Run(new Amt(args));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Create_ch_info(int In_btn_cnt, int Out_btn_cnt, int Work_btn_cnt)
        {
            ch_info_in = new ChannelInfo[In_btn_cnt];
            ch_info_out = new ChannelInfo[Out_btn_cnt];
            ch_info_work = new ChannelInfo[Work_btn_cnt];
            for (int ii = 0; ii < In_btn_cnt; ii++) ch_info_in[ii] = new ChannelInfo("IN", ii + 1);
            for (int ii = 0; ii < Out_btn_cnt; ii++) ch_info_out[ii] = new ChannelInfo("OUT", ii + 1);
            for (int ii = 0; ii < Work_btn_cnt; ii++) ch_info_work[ii] = new ChannelInfo("WORK", ii + 1);
            int[] asio_in_ch_list = null;
            int[] asio_out_ch_list = null;
            if (assign_ch)
            {
                asio_in_ch_list = Text2intarray(assign_in_ch);
                asio_out_ch_list = Text2intarray(assign_out_ch);
            }
            else
            {
                asio_in_ch_list = new int[In_btn_cnt];
                asio_out_ch_list = new int[Out_btn_cnt];
                for (int i = 0; i < In_btn_cnt; i++) asio_in_ch_list[i] = i + 1;
                for (int i = 0; i < Out_btn_cnt; i++) asio_out_ch_list[i] = i + 1;
            }
            asio.create_chinfo(In_btn_cnt, Out_btn_cnt, Work_btn_cnt);
            unsafe
            {
                fixed (int* in_ch_list = asio_in_ch_list, out_ch_list = asio_out_ch_list)
                {
                    asio.assign_ch(In_btn_cnt, in_ch_list, Out_btn_cnt, out_ch_list);
                }
            }
            has_dampgainch = asio.has_damp_volume();
            menuItem_spgain.Enabled = has_dampgainch;
        }

        public int wbuf_cnt = 0;

        private void Setup_mainform()
        {
            data_ch = new ChannelInfo[4];
            for (int i = 0; i < 4; i++) data_ch[i] = null;
            d1_lbl.Visible = false;
            d2_lbl.Visible = false;
            d3_lbl.Visible = false;

            data_lbl = new System.Windows.Forms.Label[4];
            data_lbl[1] = d1_lbl;
            data_lbl[2] = d2_lbl;
            data_lbl[3] = d3_lbl;

            int max_ch = in_btn_cnt > out_btn_cnt ? in_btn_cnt : out_btn_cnt;
            max_ch = max_ch > work_btn_cnt ? max_ch : work_btn_cnt;
            max_ch = max_ch < max_row_of_button ? max_ch : max_row_of_button;
            int btn_intvl = 4;
            int btn_total_width = Size.Width - 60;
            int btn_width = (btn_total_width - btn_intvl * (max_ch - 1)) / max_ch;
            int new_size_width = Size.Width;
            if (btn_width < 40)
            {
                btn_width = 41;
                btn_total_width = btn_width * max_ch + btn_intvl * (max_ch - 1);
                new_size_width = btn_total_width + 60;
            }

            int i_tate1 = (in_btn_cnt - 1) / max_ch + 1 + (out_btn_cnt - 1) / max_ch + 1 + (work_btn_cnt - 1) / max_ch + 1;
            int new_height = button_top_lbl.Location.Y + i_tate1 * 32 + ConsoleMsg.Size.Height + statusStrip1.Size.Height + 24;
            ConsoleMsg.Size = new Size(btn_total_width, ConsoleMsg.Size.Height);
            Size = new Size(new_size_width, new_height);

            btn_intvl += btn_width;
            btn_left = 40;

            // 
            // in_title
            // 
            in_title = new System.Windows.Forms.Label();
            int y_in = button_top_lbl.Location.Y;
            in_title.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            in_title.Location = new System.Drawing.Point(8, y_in);
            in_title.Name = "in_title";
            in_title.Size = new System.Drawing.Size(24, 16);
            in_title.TabIndex = 8;
            in_title.Text = "In";
            Controls.Add(in_title);
            //
            // in_btn
            //
            int i_yoko = 0, i_tate = 0;
            for (int i = 0; i < in_btn_cnt; i++)
            {
                if (i_yoko >= max_ch)
                {
                    i_yoko = 0;
                    i_tate++;
                }
                System.Windows.Forms.Button in_btn = new System.Windows.Forms.Button()
                {
                    Location = new System.Drawing.Point(btn_left + i_yoko * btn_intvl, y_in + i_tate * 32),
                    Name = "IN",
                    Size = new System.Drawing.Size(btn_width, 20),
                    TabIndex = 11 + i,
                    Text = (i + 1).ToString()
                };
                in_btn.Click += new System.EventHandler(In_btn_Click);
                Controls.Add(in_btn);
                ch_info_in[i].btn = in_btn;
                i_yoko++;
            }
            i_yoko = 0;
            i_tate++;
            // 
            // out_title
            // 
            out_title = new System.Windows.Forms.Label()
            {
                Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
                Location = new System.Drawing.Point(0, y_in + i_tate * 32),
                Name = "label1",
                Size = new System.Drawing.Size(32, 16),
                TabIndex = 9,
                Text = "Out"
            };
            Controls.Add(out_title);
            //
            // out_btn
            //
            for (int i = 0; i < out_btn_cnt; i++)
            {
                if (i_yoko >= max_ch)
                {
                    i_yoko = 0;
                    i_tate++;
                }
                System.Windows.Forms.Button out_btn = new System.Windows.Forms.Button()
                {
                    Location = new System.Drawing.Point(btn_left + i_yoko * btn_intvl, y_in + i_tate * 32),
                    Name = "OUT",
                    Size = new System.Drawing.Size(btn_width, 20),
                    TabIndex = 11 + i + in_btn_cnt,
                    Text = (i + 1).ToString()
                };
                out_btn.Click += new System.EventHandler(Out_btn_Click);
                Controls.Add(out_btn);
                ch_info_out[i].btn = out_btn;
                i_yoko++;
            }

            d1_lbl.SendToBack();
            d2_lbl.SendToBack();
            d3_lbl.SendToBack();

            i_tate++;
            ConsoleMsg.Location = new Point(btn_left/2, y_in + i_tate * 32);
            //new_height = ConsoleMsg.Location.Y + ConsoleMsg.Size.Height + statusStrip1.Size.Height + 48;
            //Size = new Size(new_size_width, new_height);
            btn_not_ready = false;
        }

        private int max_row_of_btn = 0;
        private void Reset_inout_btn()
        {
            if (btn_not_ready) return;
            max_row_of_btn = in_btn_cnt > out_btn_cnt ? in_btn_cnt : out_btn_cnt;
            max_row_of_btn = max_row_of_btn > work_btn_cnt ? max_row_of_btn : work_btn_cnt;
            max_row_of_btn = max_row_of_btn < max_row_of_button ? max_row_of_btn : max_row_of_button;
            int btn_intvl = 4;
            int btn_total_width = Size.Width - 60;
            int btn_width = (btn_total_width - btn_intvl * (max_row_of_btn - 1)) / max_row_of_btn - 1;
            btn_intvl += btn_width;
            btn_left = 40;

            int y_in = button_top_lbl.Location.Y;
            //
            // in_btn
            //
            int i_yoko = 0, i_tate = 0;
            for (int i = 0; i < in_btn_cnt; i++)
            {
                if (ch_info_in[i].valid)
                {
                    if (i_yoko >= max_row_of_btn)
                    {
                        i_yoko = 0;
                        i_tate++;
                    }
                    ch_info_in[i].btn.Location = new System.Drawing.Point(btn_left + i_yoko * btn_intvl, y_in + i_tate * 32);
                    ch_info_in[i].btn.Size = new System.Drawing.Size(btn_width, 20);
                    i_yoko++;
                    ch_info_in[i].btn.Visible = true;
                    ch_info_in[i].btn.Text = ch_info_in[i].btn_no.ToString();
                }
                else
                {
                    ch_info_in[i].btn.Visible = false;
                }
            }
            i_yoko = 0;
            i_tate++;
            //
            // out_btn
            //
            for (int i = 0; i < out_btn_cnt; i++)
            {
                if (ch_info_out[i].valid)
                {
                    if (i_yoko >= max_row_of_btn)
                    {
                        i_yoko = 0;
                        i_tate++;
                    }
                    ch_info_out[i].btn.Location = new System.Drawing.Point(btn_left + i_yoko * btn_intvl, y_in + i_tate * 32);
                    ch_info_out[i].btn.Size = new System.Drawing.Size(btn_width, 20);
                    i_yoko++;
                    ch_info_out[i].btn.Visible = true;
                    ch_info_out[i].btn.Text = ch_info_out[i].btn_no.ToString();
                }
                else
                {
                    ch_info_out[i].btn.Visible = false;
                }
            }
        }

        #region btn_click
        private ChannelInfo drawgraph_ch;
        private void In_btn_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button s = (System.Windows.Forms.Button)sender;
            ChannelInfo cinfo = Find_ch_info(s);

            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                {
                    Cnsl_msg("show ch" + cinfo.btn_no.ToString() + " input\r\n");
                    Thread st = new Thread(new ThreadStart(Start_DrawGraph))
                    {
                        Name = "Start_DrawGraph"
                    };
                    drawgraph_ch = cinfo;
                    st.Start();
                    started_thread_list.Add(st);
                }
            }
            else
            {
                DataCreate(cinfo, ChannelInfo.StreamType.asioin);
                s.ContextMenu = this.contextMenu1;
            }
        }
        public bool OutBufCreate(ChannelInfo cinfo, ChannelInfo.StreamType type)
        {
            if (cinfo.type != ChannelInfo.BufferType.output) return false;
            return DataCreate(cinfo, type);
        }
        public bool InBufCreate(ChannelInfo cinfo, ChannelInfo.StreamType type)
        {
            if (cinfo.type != ChannelInfo.BufferType.input) return false;
            return DataCreate(cinfo, type);
        }
        public bool DataCreate(ChannelInfo cinfo, ChannelInfo.StreamType type)
        {
            int result = -1;
            bool succeed = true;
            if (!asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                result = asio.data_create((int)cinfo.type, cinfo.btn_no, (int)type);
            }
            if (result < 0)
            {
                Cnsl_msg("Buffer " + cinfo.type.ToString() + ", " + cinfo.btn_no.ToString() + " was not created\r\n");
                succeed = false;
            }
            else if(cinfo.btn != null)
            {
                SetButtonStatus(cinfo, BufferStatus.buffer, "Float data was created.");
                cinfo.btn.ContextMenu = this.contextMenu1;
                go_btn.Enabled = true;
            }
            return succeed;
        }

        private void Start_DrawGraph()
        {
            DrawGraph dd = new DrawGraph(this, drawgraph_ch);
            dd.ShowDialog();
        }

        private void SetButtonStatus(ChannelInfo cinfo, BufferStatus bs, string msg)
        {
            if (msg != "") Cnsl_msg(cinfo.Get_btn_name() + msg + "\r\n");
            Button_status(cinfo.btn, bs);
        }
        public void SetButtonStatus_Signal(ChannelInfo cinfo)
        {
            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                {
                    SetButtonStatus(cinfo, BufferStatus.signal, "");
                }
            }
        }
        public void SetAllButtonStatus()
        {
            for (int i = 0; i < in_btn_cnt; i++)
            {
                SetButtonStatus_Signal(ch_info_in[i]);
            }
            for (int i = 0; i < out_btn_cnt; i++)
            {
                SetButtonStatus_Signal(ch_info_out[i]);
            }
            for (int i = 0; i < work_btn_cnt; i++)
            {
 //               SetButtonStatus_Signal(ch_info_work[i]);
            }
        }
        public void ButtonEnable(int which, bool enable)
        {
            switch (which)
            {
                case 1:
                    for (int i = 0; i < in_btn_cnt; i++)
                    {
                        ch_info_in[i].btn.Enabled = enable;
                    }
                    break;
                case 2:
                    for (int i = 0; i < out_btn_cnt; i++)
                    {
                        ch_info_out[i].btn.Enabled = enable;
                    }
                    break;
            }
        }
        private void Out_btn_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button s = (System.Windows.Forms.Button)sender;
            ChannelInfo cinfo = Find_ch_info(s);

            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                {
                    Thread st = new Thread(new ThreadStart(Start_DrawGraph));
                    drawgraph_ch = cinfo;
                    st.Name = "Start_DrawGraph";
                    st.Start();
                    started_thread_list.Add(st);
                }
                else
                {
                    DataControlForm cdata = new DataControlForm(this, cinfo);
                    cdata.ShowDialog();
                    if (cdata.signal_created)
                    {
                        go_btn.Enabled = true;
                    }
                }
            }
            else
            {
                DataCreate(cinfo, ChannelInfo.StreamType.asioout);
                s.ContextMenu = this.contextMenu1;
            }
        }

        public void PostCreateSignal(ChannelInfo cinfo, string msg)
        {
            if (msg != "")
            {
                SetButtonStatus(cinfo, BufferStatus.signal, " : " + msg);
            }
            else
            {
                Cnsl_msg("Signal was not created\r\n");
            }
            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
        }

        #endregion

        private Thread playstart_thread = null;
        private Thread controlstart_thread = null;
        private void Go_btn_Click(object sender, System.EventArgs e)
        {
            if (asio.get_stop() == true)
            {
                PlayControlStart();
            }
            else
            {
                repeat_stop_point = asio.getADpoint();
                asio.set_stop(true);
            }
        }

        public void PlayControlStart()
        {
            playstart_thread = new Thread(new ThreadStart(PlayStart))
            {
                Name = "PlayStart"
            };
            playstart_thread.Start();
            this.go_btn.Text = "AD/DA Stop";
            controlstart_thread = new Thread(new ThreadStart(ControlStart))
            {
                Name = "ControlStart"
            };
            controlstart_thread.Start();
        }

        public int repeat_stop_point = 0;
        public int last_error_cnt = 0;
        private void ControlStart()
        {
            double pp;
            bool is_stream = asio.is_stream_audio_out();
            if(is_stream) pp = 1.0 / asio.stream_word_count();
            else pp = 1.0 / (double)asio.GetDataLength();
            while (asio.get_stop()) Thread.Sleep(100);
            while (!asio.get_stop())
            {
                int position;
                if (is_stream) position = asio.stream_file_point();
                else position = asio.getADpoint();
                adda_point_tb.Value = (int)(pp * (double)position * 100.0);
                adda_point_lbl.Text = position.ToString();
                Write_zitter_level();
                Thread.Sleep(100);
            }
            adda_point_tb.Value = 0;
            asio.setADpoint(0);
            adda_point_lbl.Text = "0";
            this.go_btn.Text = "AD/DA Start";
        }

        public void Write_zitter_level()
        {
            zitter_level = asio.asio_zitter() * 100.0;
            if (zitter_level > 0.0)
            {
                total_error = asio.rtconv_total_error();
                rtconv_zitter_lbl.Text = "Zitter Level=" + zitter_level.ToString("0.0") + "%, Error count = " + total_error.ToString();
            }
            //double max_val = asio.gpower();
            //max_val_lbl.Text = "Max val=" + max_val.ToString("0.00");
        }

        public void PlayStart()
        {
            menuItem_Exit.Enabled = false;

            //if (asio.initialize() < 0) goto asio_error;
            if (asio.create_buffers() < 0) goto asio_error;
            if (asio.start() < 0) goto asio_error;
            Cnsl_msg("ASIO start --->");
            if (asio.stop() < 0) goto asio_error;
            Cnsl_msg(" stop\r\n");
            asio.dispose_buffers();
            Cnsl_msg("Zitter Level=" + zitter_level.ToString("0.0") + "\r\n");
            if (total_error > 0)
            {
                for (int i = 0; i < asio.rt_fft_level(); i++)
                {
                    int no = i + 1;
                    int ecnt = asio.rtconv_error_cnt(i);
                    if (ecnt > 0)
                    {
                        Cnsl_msg("FFT level=" + no.ToString() + ", Error count=" + ecnt.ToString() + "\r\n");
                    }
                }
            }
            ResetButtonStatus(true, true);

            menuItem_Exit.Enabled = true;
            this.Invalidate();
//            return;

            asio_error:
            String error_msg = "";
            unsafe
            {
                error_msg = new String(asio.error_message());
            }
            Cnsl_msg(error_msg + "\r\n");
            return;
        }

        public void ResetButtonStatus(bool clear_in, bool clear_out)
        {
            if (clear_in)
            {
                for (int i = 0; i < in_btn_cnt; i++)
                {
                    ChannelInfo cinfo = ch_info_in[i];
                    if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
                    {
                        asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                        if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                        {
                            Button_status(ch_info_in[i].btn, BufferStatus.signal);
                        }
                        else
                        {
                            Button_status(ch_info_in[i].btn, BufferStatus.buffer);
                        }
                    }
                    else
                    {
                        Button_status(ch_info_in[i].btn, BufferStatus.empty);
                    }
                }
            }
            if (clear_out)
            {
                for (int j = 0; j < out_btn_cnt; j++)
                {
                    ChannelInfo cinfo = ch_info_out[j];
                    if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
                    {
                        asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                        if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                        {
                            Button_status(ch_info_out[j].btn, BufferStatus.signal);
                        }
                        else
                        {
                            Button_status(ch_info_out[j].btn, BufferStatus.buffer);
                        }
                    }
                    else
                    {
                        Button_status(ch_info_out[j].btn, BufferStatus.empty);
                    }
                }
            }
        }

        public void Created_signal(ChannelInfo cinfo)
        {
            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
            if (asio.GetMaxVal((int)cinfo.type, cinfo.btn_no) > 0.0)
            {
                SetButtonStatus(cinfo, BufferStatus.signal, " was created.");
            }
        }

        private void MenuItem_clear_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);
            DataClear(cinfo);
        }

        public void OutbufDataClear(ChannelInfo cinfo)
        {
            if (cinfo.type != ChannelInfo.BufferType.output) return;
            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                asio.data_clear((int)cinfo.type, cinfo.btn_no);
            }
            SetButtonStatus(cinfo, BufferStatus.buffer, " was cleared.");
        }
        public void DataClear(ChannelInfo cinfo)
        {
            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                asio.data_clear((int)cinfo.type, cinfo.btn_no);
            }
            SetButtonStatus(cinfo, BufferStatus.buffer, " was cleared.");
        }

        public void DataClear1(ChannelInfo cinfo, int st_point, int ed_point)
        {
            if (asio.have_buffer((int)cinfo.type, cinfo.btn_no))
            {
                asio.data_clear1((int)cinfo.type, cinfo.btn_no, st_point, ed_point);
            }
            SetButtonStatus(cinfo, BufferStatus.buffer, " was cleared : 0-" + st_point.ToString() + " and " + ed_point.ToString() + "-end");
        }

        public void SetData(int data_no, ChannelInfo cinfo)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (data_ch[i] == cinfo)
                {
                    data_lbl[i].Visible = false;
                    data_ch[i] = null;
                    break;
                }
            }
            data_ch[data_no] = cinfo;

            int xx = cinfo.btn.Location.X + cinfo.btn.Size.Width / 2 - 20;
            int yy = cinfo.btn.Location.Y - 12;
            data_lbl[data_no].Location = new System.Drawing.Point(xx, yy);
            data_lbl[data_no].Visible = true;
            data_lbl[data_no].BringToFront();

            Cnsl_msg("Data " + data_no.ToString() + " is assigned to buffer " + cinfo.type.ToString() + ", " + cinfo.btn_no.ToString() + "\r\n");
        }

        private void MenuItem_setdata1_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);
            int data_no = (int)s.Text[5] - (int)'0';
            SetData(data_no, cinfo);
        }

        private void MenuItem_delete_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);
            DataDispose(cinfo);
        }

        public void DataDispose(ChannelInfo cinfo)
        {
            asio.data_dispose((int)cinfo.type, cinfo.btn_no);
            if(cinfo.btn != null) SetButtonStatus(cinfo, BufferStatus.empty, "was deleted.");
        }

        private void MenuItem_wav_import_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.OpenFileDialog("Wav import", "*.wav"))
            {
                WavRead_menu(fd.filename, cinfo);
                if (asio.have_signal((int)cinfo.type, cinfo.btn_no))
                {
                    Button_status((Button)cm.SourceControl, BufferStatus.signal);
                }
                go_btn.Enabled = true;
            }
        }

        public void WavRead_menu(string filename, ChannelInfo cinfo)
        {
            WavStream ws = new WavStream(this);
            if (!ws.WavRead(filename, false)) return;
            ws.Load_data(cinfo);
            SetButtonStatus_Signal(cinfo);
        }

        public void TextRead_menu(string filename, ChannelInfo cinfo, bool isHex)
        {
            WavStream ws = new WavStream(this);
            ws.TextRead(filename, isHex);
            ws.Load_data(cinfo);
            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
        }

        public int Max_fftpoint(int leng)
        {
            int fftpnt = 2;
            while (fftpnt < leng) fftpnt *= 2;
            if (fftpnt > leng) fftpnt /= 2;
            return fftpnt;
        }

        private void MenuItem_FFTpoint_Click(object sender, EventArgs e)
        {
            CreateDataForm cdata = new CreateDataForm(this, "Change FFT point", fftpoint.ToString());
            cdata.ShowDialog();
            fftpoint = System.Convert.ToInt32(this.reply_msg);
            Cnsl_msg("FFT point is changed (" + fftpoint.ToString() + ")\r\n");
        }

        private void MenuItem_Exit_Click(object sender, System.EventArgs e)
        {
            if (show_save) save.Close();

            for (int i = 0; i < started_thread_list.Count; i++)
            {
                Thread st = (Thread)started_thread_list[i];
                if (st.IsAlive)
                {
                    st.Abort();
                }
            }

            asio.exit();
            asio.Dispose();
            //            Cnsl_msg("ASIO exit\r\n");

            if (sp_amp_set != null) sp_amp_set.Dispose();

            this.Dispose();
            // ASIO driver is already released and settings are saved above.
            // Environment.Exit() runs DLL_PROCESS_DETACH of every loaded DLL and some
            // ASIO driver DLLs hang there (window disappears but amt.exe stays alive),
            // so terminate the process without the DLL detach phase.
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void Button_status(Button btn, BufferStatus bs)
        {
            switch (bs)
            {
                case BufferStatus.empty:
                    btn.BackColor = System.Drawing.Color.Empty;
                    break;
                case BufferStatus.buffer:
                    btn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(255)), ((System.Byte)(128)));
                    break;
                case BufferStatus.signal:
                    btn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(128)), ((System.Byte)(128)));
                    break;
                case BufferStatus.sn_perfect:
                    btn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(250)), ((System.Byte)(240)));  // Ivory
                    break;
                case BufferStatus.sn_good:
                    btn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(100)));   // LemonChiffon
                    break;
                case BufferStatus.sn_bad:
                    btn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(230)), ((System.Byte)(230)), ((System.Byte)(250)));  // lavender
                    break;
                default:
                    break;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Reset_inout_btn();
            this.Invalidate();
        }

        public bool WavExport(string filename, ChannelInfo cinfo, bool is_float)
        {
            FileInfo fileInfo = new FileInfo(filename);
            bool succeed = true;
            try
            {
                FileStream fs = fileInfo.Open(FileMode.CreateNew);
                WavStream ws = new WavStream(this);
                ws.WavWrite(fs, cinfo, is_float);
            }
            catch (Exception e)
            {
                succeed = false;
                System.Windows.Forms.MessageBox.Show("Error: " + e.Message);
            }
            return succeed;
        }

        public bool WavFloatExport(string filename, float[] data, int d_size)
        {
            FileInfo fileInfo = new FileInfo(filename);
            bool succeed = true;
            try
            {
                FileStream fs = fileInfo.Create();
                WavStream ws = new WavStream(this);
                ws.WavFloatWrite(fs, data, d_size);
            }
            catch (Exception e)
            {
                succeed = false;
                System.Windows.Forms.MessageBox.Show("Error: " + e.Message);
            }
            return succeed;
        }

        private void MenuItem_wavs_export_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.SaveFileDialog("wav(int) export file", "wav"))
            {
                WavExport(fd.filename, cinfo, false);
            }
        }

        private void MenuItem_wavf_export_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.SaveFileDialog("wav(float) export file", "wav"))
            {
                WavExport(fd.filename, cinfo, true);
            }
        }

        private void MenuItem_movef_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            CreateDataForm cdata = new CreateDataForm(this, "Please set the position to cut : Ch." + cinfo.type.ToString() + ", " + cinfo.btn_no.ToString(), asio.GetDataLength().ToString());
            cdata.ShowDialog();
            int leng = System.Convert.ToInt32(this.reply_msg);
            asio.move_forward((int)cinfo.type, cinfo.btn_no, leng);
        }

        private void MenuItem_Version_Click(object sender, System.EventArgs e)
        {
            VersionInfo dd = new VersionInfo(ver_rev);
            dd.ShowDialog();
        }

        private void MenuItem_copyfrom1_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[1];
            CopyData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_copyfrom2_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[2];
            CopyData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_copyfrom3_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[3];
            CopyData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        public void CopyData(ChannelInfo src_cinfo, ChannelInfo dst_cinfo)
        {
            if (asio.have_buffer((int)dst_cinfo.type, dst_cinfo.btn_no))
            {
                asio.CopyData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
                Cnsl_msg("Copy : src ch=" + src_cinfo.type.ToString() + ", " + src_cinfo.btn_no.ToString() + ", dst ch=" + dst_cinfo.type.ToString() + ", " + dst_cinfo.btn_no.ToString() + "\r\n");
            }
        }

        private void MenuItem_subtract1_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[1];
            SubData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_subtract2_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[2];
            SubData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_subtract3_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[3];
            SubData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        public void SubData(ChannelInfo src_cinfo, ChannelInfo dst_cinfo)
        {
            if (asio.have_buffer((int)dst_cinfo.type, dst_cinfo.btn_no))
            {
                asio.SubData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
                Cnsl_msg("Subtract : src ch=" + src_cinfo.type.ToString() + ", " + src_cinfo.btn_no.ToString() + ", dst ch=" + dst_cinfo.type.ToString() + ", " + dst_cinfo.btn_no.ToString() + "\r\n");
            }
        }

        public double VerifyData(ChannelInfo src_cinfo, ChannelInfo dst_cinfo)
        {
            double val = -1.0;
            if (asio.have_buffer((int)dst_cinfo.type, dst_cinfo.btn_no))
            {
                val = asio.Verify((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
                Cnsl_msg("Verify : " + val.ToString("0.0") + " dB, src ch=" + src_cinfo.type.ToString() + ", " + src_cinfo.btn_no.ToString() + ", dst ch=" + dst_cinfo.type.ToString() + ", " + dst_cinfo.btn_no.ToString() + "\r\n");
            }
            return val;
        }

        private void MenuItem_divide1_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[1];
            DivData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_divide2_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[2];
            DivData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        private void MenuItem_divide3_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[3];
            DivData(src_cinfo, dst_cinfo);
            Button_status(btn, BufferStatus.signal);
        }

        public void DivData(ChannelInfo src_cinfo, ChannelInfo dst_cinfo)
        {
            if (asio.have_buffer((int)dst_cinfo.type, dst_cinfo.btn_no))
            {
                asio.DivData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no, fftpoint);
                Cnsl_msg("Divide : src ch=" + src_cinfo.type.ToString() + ", " + src_cinfo.btn_no.ToString() + ", dst ch=" + dst_cinfo.type.ToString() + ", " + dst_cinfo.btn_no.ToString() + ": fftpnt=" + fftpoint.ToString() + "\r\n");
            }
        }

        private void MenuItem_bin_import_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.OpenFileDialog("Bin import", "*.bin"))
            {
                BinaryRead(fd.filename, cinfo, true);
            }
        }

        public void BinaryRead(string filename, ChannelInfo cinfo, bool adjust_length)
        {
            FileInfo fi = new FileInfo(filename);
            long filesize = fi.Length;  			//�t�@�C���̃T�C�Y���擾
            BinaryImport BIForm = new BinaryImport(cinfo, filename, filesize, this);
            BIForm.ShowDialog();

            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
        }

        public double BufConvolution(ChannelInfo in_ch1, ChannelInfo in_ch2, ChannelInfo out_ch, double scale)
        {
            return BufConvolution(in_ch1, in_ch2, out_ch, scale, 0.0, (double)(sample_freq / 2));
        }

        public double BufConvolution(ChannelInfo in_ch1, ChannelInfo in_ch2, ChannelInfo out_ch, double scale, double freq_st, double freq_ed)
        {
            return asio.Convolution((int)in_ch1.type, in_ch1.btn_no, (int)in_ch2.type, in_ch2.btn_no, (int)out_ch.type, out_ch.btn_no, fftpoint, scale, freq_st, freq_ed);
        }

        private void MenuConvWav_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            ChannelInfo work_ch1 = Find_ch_info("WORK", 1);
            bool has_1ch_workbuf = asio.have_buffer((int)work_ch1.type, work_ch1.btn_no);
            if (!has_1ch_workbuf)
            {
                System.Windows.Forms.MessageBox.Show("workbuf(1) is not created.");
                return;
            }
            ChannelInfo work_ch2 = Find_ch_info("WORK", 2);
            bool has_2ch_workbuf = asio.have_buffer((int)work_ch2.type, work_ch2.btn_no);
            if (!has_2ch_workbuf)
            {
                System.Windows.Forms.MessageBox.Show("workbuf(2) is not created.");
                return;
            }

            if (fd.OpenFileDialog("Conv wav", "*.wav") == false)
            {
                return;
            }
            string open_file = fd.filename;
            if (fd.SaveFileDialog("Conv wav", "wav") == false)
            {
                Cnsl_msg("Canceled.\r\n");
                return;
            }

            WavConvolution(cinfo, open_file, fd.filename);
        }

        public void WavConvolution(ChannelInfo filt_ch, string in_file, string out_file)
        {
            int frame_size = asio.GetDataLength();
            if (frame_size > fftpoint / 2)
            {
                frame_size = fftpoint / 2;
                System.Windows.Forms.MessageBox.Show("Buf size = " + asio.GetDataLength().ToString() + "Frame size = " + frame_size.ToString() + ". Data must be truncated");
            }

            WavStream ws = new WavStream(this);
            FileStream infs = ws.WavOpen(in_file);
            int data_word_cnt = ws.WordCount();
            int frame_count = data_word_cnt / frame_size + 1;
            int last_frame_size = data_word_cnt % frame_size;
            if (last_frame_size == 0)
            {
                frame_count--;
                last_frame_size = frame_size;
            }

            FileStream outfs = FileProcess.OpenWrite(out_file);
            ws.PreOutWav1ch(outfs, frame_size, frame_count);

            int read_size = frame_size;
            double scale = 1.0;
            for (int frame_no = 1; frame_no <= frame_count; frame_no++)
            {
                if (frame_no == frame_count) read_size = last_frame_size;
                ws.Get_data16(infs, read_size * 2);
                ChannelInfo work1 = Find_ch_info("WORK", 1);
                ws.Load_data(work1);
                ChannelInfo work2 = Find_ch_info("WORK", 2);
                BufConvolution(work1, filt_ch, work2, scale);

                ws.OutWav1ch(outfs, work2, frame_size);
            }
            ws.OutWav1ch(outfs, null, frame_size);

            infs.Close();
            outfs.Flush();
            outfs.Close();
        }

        private void MenuItem_multiply_val_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            CreateDataForm cdata = new CreateDataForm(this, "Multiply Value", "1.0");
            cdata.ShowDialog();
            double val = System.Convert.ToDouble(this.reply_msg);
            asio.MultiplyValue((int)cinfo.type, cinfo.btn_no, val);
        }

        private void Adda_point_tb_Scroll(object sender, EventArgs e)
        {
            asio.setADpoint(adda_point_tb.Value * asio.GetDataLength() / 100);
            int position = asio.getADpoint();
            adda_point_lbl.Text = position.ToString();
        }

        private void MenuItem_dec_export_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.SaveFileDialog("Dec export file", "txt"))
            {
                TextExport(fd.filename, cinfo, false);
            }
        }

        private void MenuItem_hex_export_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.SaveFileDialog("Hex export file", "txt"))
            {
                TextExport(fd.filename, cinfo, true);
            }
        }

        public void TextExport(string filename, ChannelInfo cinfo, bool isHex)
        {
            FileInfo fileInfo = new FileInfo(filename);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            try
            {
                FileStream fs = fileInfo.Open(FileMode.CreateNew);
                WavStream ws = new WavStream(this);
                ws.WavWriteText(fs, cinfo, isHex);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + e.Message);
            }
        }

        private void MenuItem_dec_import_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.OpenFileDialog("Dec import", "*.txt"))
            {
                TextRead_menu(fd.filename, cinfo, false);
                Button_status((Button)cm.SourceControl, BufferStatus.signal);
                go_btn.Enabled = true;
            }
        }

        private void MenuItem_hex_import_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.OpenFileDialog("Hex import", "*.txt"))
            {
                TextRead_menu(fd.filename, cinfo, true);
                Button_status((Button)cm.SourceControl, BufferStatus.signal);
                go_btn.Enabled = true;
            }
        }

        private void MenuItem_RevTime_Click(object sender, EventArgs e)
        {
            bool has_data = false;
            for(int i=1;i<4;i++)
            {
                if (data_ch[i] != null) has_data = true;

            }
            if (has_data == false)
            {
                Cnsl_msg("Select data\n");
                return;
            }

            RevTimeForm revtime = new RevTimeForm(this);
            revtime.ShowDialog();
        }

        private void MenuItem_ImpMeas_Click(object sender, EventArgs e)
        {
            ImpMeasForm imp_meas = new ImpMeasForm(this);
            imp_meas.ShowDialog();
        }

        public bool Set_work_folder()
        {
            bool ok = false;
            folderBrowserDialog1.SelectedPath = work_folder;
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                work_folder = folderBrowserDialog1.SelectedPath;
                Cnsl_msg("Work folder = " + work_folder + "\n");
                ok = true;
            }
            return ok;
        }

        private void Repeat_mode_cb_CheckedChanged(object sender, EventArgs e)
        {
            asio.set_repeat_mode(repeat_mode_cb.Checked);
        }

        private void Stream_wcnt_tb_TextChanged(object sender, EventArgs e)
        {
            if (stream_wcnt_tb.Text != "")
            {
                int word_cnt = System.Convert.ToInt32(stream_wcnt_tb.Text);
                int total_sec = word_cnt / sample_freq;
                int sec = total_sec % 60;
                int total_min = total_sec / 60;
                int min = total_min % 60;
                int total_hour = total_min / 60;

                stream_hhmmss_lbl.Text = total_hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            }
        }

        private SaveForm save = null;
        private bool show_save = false;
        private void MenuItem_Save_Click(object sender, EventArgs e)
        {
            Thread save_thread = new Thread(new ThreadStart(SaveShow))
            {
                Priority = ThreadPriority.Lowest,
                Name = "SaveShow"
            };
            save_thread.Start();
        }

        private void SaveShow()
        {
            save = new SaveForm(this);
            show_save = true;
            save.ShowDialog();
            show_save = false;
        }

        private void MenuItem_workfolder_Click(object sender, EventArgs e)
        {
            Set_work_folder();
        }


        public int[] Text2intarray(string tt)
        {
            ArrayList in_ch_arraylist = new ArrayList();
            string[] in_ch_list = tt.Replace(" ", "").Split(',');
            for (int i = 0; i < in_ch_list.Length; i++)
            {
                if (in_ch_list[i].Contains('-'))
                {
                    string[] tmp1 = in_ch_list[i].Split('-');
                    int i1 = Convert.ToInt32(tmp1[0]);
                    int i2 = Convert.ToInt32(tmp1[1]);
                    for (int ii = i1; ii <= i2; ii++)
                    {
                        in_ch_arraylist.Add(ii);
                    }
                }
                else
                {
                    int ii = Convert.ToInt32(in_ch_list[i]);
                    in_ch_arraylist.Add(ii);
                }
            }
            int[] result = new int[in_ch_arraylist.Count];
            int jj = 0;
            foreach (int ii in in_ch_arraylist)
            {
                result[jj++] = ii;
            }

            return result;
        }

        private void Sampl_freq_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sfreq1 = System.Convert.ToInt32(sampl_freq_cb.SelectedItem);
            if (without_asio)
            {
                sample_freq = sfreq1;
            }
            else
            {
                sample_freq = asio.set_sfreq(sfreq1);
                if (sample_freq < 0) asio_error_exit();
            }
            Cnsl_msg("Sampling Freq. [Hz] = " + sample_freq.ToString() + "\r\n");
        }

        private void MenuItem_verify1_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[1];
            double sn = VerifyData(src_cinfo, dst_cinfo);
            Button_status_verify(btn, sn);
        }

        private void MenuItem_verify2_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[2];
            double sn = VerifyData(src_cinfo, dst_cinfo);
            Button_status_verify(btn, sn);
        }

        private void MenuItem_verify3_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            Button btn = (Button)cm.SourceControl;
            ChannelInfo dst_cinfo = Find_ch_info((Button)cm.SourceControl);
            ChannelInfo src_cinfo = data_ch[3];
            double sn = VerifyData(src_cinfo, dst_cinfo);
            Button_status_verify(btn, sn);
        }

        public void Button_status_verify(Button btn, double sn)
        {
            if (sn == 1000.0)
            {
                Button_status(btn, BufferStatus.sn_perfect);
            }
            else if (sn > 80.0)
            {
                Button_status(btn, BufferStatus.sn_good);
            }
            else
            {
                Button_status(btn, BufferStatus.sn_bad);
            }
        }

        private void Set_asio_mode(ASIOmode asio_mode)
        {
            asio.push_asio_mode((int)asio_mode);
        }

        private void MenuItem_spgain_Click(object sender, EventArgs e)
        {
            Thread st = new Thread(new ThreadStart(Spgain))
            {
                Name = "spgain"
            };
            st.Start();
            started_thread_list.Add(st);
        }

        public SPGainForm sp_amp_set = null;
        private void Spgain()
        {
            sp_amp_set = new SPGainForm(this);
            sp_amp_set.ShowDialog();
            sp_amp_set = null;
        }

#if _DEBUG
        private int print_log_line = 0;
        public void DebugLog(string logText)
        {
            StreamWriter logStream;
            if (print_log_line == 0)
            {
                // Open write
                logStream = new System.IO.StreamWriter("C:\\temp\\amt_log.txt");
            }
            else
            {
                // Open append
                logStream = new System.IO.StreamWriter("C:\\temp\\amt_log.txt", true);
            }
            print_log_line++;
            // Write Log
            logStream.WriteLine(logText);
            // Close Log
            logStream.Close();
        }
#endif

        private void Convset_go()
        {
            Set_asio_mode(Amt.ASIOmode.asiomode_rtconv_io);
            convset = new ConvSet(this, false);
            convset.ShowDialog();
            convset = null;
        }

        private void MenuItem_rtconv_aiao_Click(object sender, EventArgs e)
        {
            if (convset == null)
            {
                Thread st = new Thread(new ThreadStart(Convset_go))
                {
                    Name = "convset_go"
                };
                st.Start();
                started_thread_list.Add(st);
                while (convset == null || convset.Visible == false)
                {
                    Thread.Sleep(10);
                }
                convset.Location = new Point(this.Location.X + this.Size.Width/2, this.Location.Y + 50);
            }
            if(sp_amp_set == null)
            {
                Thread st1 = new Thread(new ThreadStart(Spgain))
                {
                    Name = "spgain"
                };
                st1.Start();
                started_thread_list.Add(st1);
                while (sp_amp_set == null || sp_amp_set.Visible == false) Thread.Sleep(10);
                sp_amp_set.Location = new Point(this.Location.X + this.Size.Width / 2 + convset.Size.Width - 15 , this.Location.Y + 50);
            }
        }

        private void menuItem_ueserver_Click(object sender, EventArgs e)
        {
            Set_asio_mode(ASIOmode.asiomode_rtmove);
            UEServerForm ueserver = new UEServerForm(this);
            ueserver.ShowDialog();
        }
    }

    public class WavInfo
    {
        public string id_name = "";
        public int id_num;
        public string full_path = "";
        public int ch;
        public int fs;
        public int word_cnt;
        public WavInfo(string FullPpath, int Ch, int Fs, int Word_cnt)
        {
            full_path = FullPpath;
            id_name = System.IO.Path.GetFileNameWithoutExtension(FullPpath);
            ch = Ch;
            fs = Fs;
            word_cnt = Word_cnt;
        }
    }

    public class ChannelInfo
    {
        //private string stream_file_name = "";
        public double max_freq = 0.0;
        public double max_level = 0.0;
//        public int asio_ch;
        public enum BufferType { input = 1, output = 2, work = 3 };
        public enum StreamType
        {
            no_io = 0,
            asioin = 1,
            asioout = 2,
            wavread = 3,
            wavread_add = 4,
            wavwrite = 5,
            wavread_asioout = 6,
            asioin_wavwrite = 7
        };
        public BufferType type;
        private string buf_type;
        public int btn_no;
        public bool valid = true;
        public Button btn;

        public ChannelInfo(string butType, int Btn_no)
        {
            btn_no = Btn_no;
            btn = null;
            buf_type = butType;
            switch (butType)
            {
                case "IN":
                    type = BufferType.input;
                    break;
                case "OUT":
                    type = BufferType.output;
                    break;
                case "WORK":
                    type = BufferType.work;
                    break;
            }
        }
       

        public string FreqLevel_info()
        {
            return max_freq.ToString("0.0") + "Hz : " + max_level.ToString("0.00") + "dB\r\n";
        }

        public string GetBufferType()
        {
            return buf_type;
        }

        public void FreqLevel_clear()
        {
            max_freq = max_level = 0.0;
        }

        public string Get_btn_name()
        {
            return buf_type + btn_no.ToString();
        }
    }

    public class RtconvInfo
    {
        public int chIndex = 0;
        public int fftlevel = 0;
        public RtconvInfo(int ch, int lev)
        {
            chIndex = ch;
            fftlevel = lev;
        }
    }

    public static class FileProcess
    {
        public static bool Exists(string file_name)
        {
            FileInfo fi = new FileInfo(file_name);
            return fi.Exists;
        }

        public static FileStream Open(string file_name, FileMode mode)
        {
            FileInfo fi = new FileInfo(file_name);
            return fi.Open(mode);
        }

        public static FileStream OpenRead(string file_name)
        {
            FileInfo fi = new FileInfo(file_name);
            return fi.OpenRead();
        }
        public static FileStream OpenWrite(string file_name)
        {
            FileInfo fi = new FileInfo(file_name);
            return fi.OpenWrite();
        }
    }
}

