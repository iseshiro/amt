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
        public string ver_rev = "ver. 2.1 rev. 2";

        #region Form declation
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem_File;
        private System.Windows.Forms.MenuItem menuItem_Data;
        private System.Windows.Forms.MenuItem menuItem_Processing;
        public System.Windows.Forms.MenuItem menuItem_Exit;
        private System.Windows.Forms.MenuItem menuItem_workfolder;
        private System.Windows.Forms.MenuItem menuItem_Save;
        private System.Windows.Forms.MenuItem menuItem_Conv;
        private System.Windows.Forms.MenuItem menuItem_DeConv;
        private System.Windows.Forms.MenuItem menuItem_IIR;
        private System.Windows.Forms.MenuItem menuItem_Help;
        private System.Windows.Forms.MenuItem menuItem_Version;
        private System.Windows.Forms.MenuItem menuItem_script;
        private System.Windows.Forms.MenuItem menuItem_scriptRec;
        private System.Windows.Forms.MenuItem menuItem_scriptLoad;
        private System.Windows.Forms.MenuItem menuItem_scriptRecON;
        private System.Windows.Forms.MenuItem menuItem_scriptRecOFF;
        private System.Windows.Forms.MenuItem menuItem_FFTpoint;
        private System.Windows.Forms.MenuItem menuItem_CheckBoSC;

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

        private System.Windows.Forms.MenuItem menuItem_Add;
        private System.Windows.Forms.MenuItem menuItem_Mpy;
        private int btn_left;
        private System.Windows.Forms.MenuItem menuItem_cvt;
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
        private int script_count = 0;
        private const int max_script_count = 12;
        private int script_count_init;
        public Script myScripts;
        public Script[] myScriptArray = new Script[max_script_count];
        private Shortcut[] shortcuts = new Shortcut[max_script_count] { Shortcut.F1, Shortcut.F2, Shortcut.F3, Shortcut.F4, Shortcut.F5, Shortcut.F6, Shortcut.F7, Shortcut.F8, Shortcut.F9, Shortcut.F10, Shortcut.F11, Shortcut.F12 };
        private string[] shortcutName = new string[max_script_count] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };
        public string ScriptSeparator = " ";
        delegate void SetTextCallback(string text);
        public bool script_recording = false;
        private MenuItem menuItem_stream;
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
        private MenuItem menuItem_ch_assign;
        public int in_btn_cnt;
        public int out_btn_cnt;
        public int work_btn_cnt = 6;
        public bool assign_ch = false;
        public string assign_in_ch = "";
        public string assign_out_ch = "";
        private bool btn_not_ready = true;
        public ChannelInfo ch_info_vol;
        public string script_folder = null;
        public bool script_continue = true;
        public ProcessWave pwave = null;
        private bool command_mode = false;
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

        private MenuItem menuItem1;
        private MenuItem menuItem_read_wav;
        private MenuItem menuItem_write_wav;
        private MenuItem menuItem_clearall;
        private MenuItem menuItem_verify;
        private MenuItem menuItem_verify1;
        private MenuItem menuItem_verify2;
        private MenuItem menuItem_verify3;
        private MenuItem menuItem_rtconv;
        private MenuItem menuItem_amp;
        private MenuItem menuItem3;
        private MenuItem menuItem_NetPlayer;
        private MenuItem menuItem_wav_stream;
        private MenuItem menuItem_double_stream;
        private MenuItem menuItem5;
        private MenuItem menuItem_afs;
        private MenuItem menuItem6;
        private MenuItem menuItem8;
        private MenuItem menuItem7;
        private MenuItem menuItem_ImpMeas;
        private MenuItem menuItem_IR_Calc;
        private MenuItem menuItem_InvSystemDesign;
        private MenuItem menuItem_LevelMeter;
        private MenuItem menuItem9;
        private MenuItem menuItem_spgain;
        private MenuItem menuItem_micgain;
        private MenuItem menuItem_rtconv_fpga;
        private MenuItem menuItem_rtconv_aiao;
        private MenuItem menuItem_rtconv_fiao;
        private MenuItem menuItem_FileConv;
        private MenuItem menuItem_eqtest;
        private MenuItem menuItem_geq;
        public Label max_val_lbl;
        private int mkl_max_thread = 0;
        public ConvSet convset = null;
        private bool convset_fileinput;
        private MenuItem menuItem_ARS;
        private MenuItem menuItem_evaltest;
        public bool has_dampgainch = false;
        private FileConvForm file_conv = null;
        public ToolStripProgressBar toolStripProgressBar1;
        private MenuItem menuItem_boscplayer;
        private MenuItem menuItem_wavstreaming;
        public bool without_asio = true;
        private MenuItem menuItem4;
        private MenuItem menuItem_mover_server;
        private MenuItem menuItem_ueserver;
        private MenuItem menuItem_ZYBO;
        public bool net_trig_start = false;

        public Amt(String[] args)
        {
            asio = new asio_io();
            String script_fullpath = "";
            fd = new FileDialog1();
            if (args.Length > 0)
            {
                command_mode = true;
                if (args[0] == "/c")
                {
                    file_conv = new FileConvForm(this);
                    if(args.Length > 3)
                    {
                        file_conv.source_path_tb.Text = args[1];
                        file_conv.filter_path_tb.Text = args[2];
                        file_conv.output_path_tb.Text = args[3];
                        file_conv.Calc_start(true);
                    }
                    else
                        file_conv.ShowDialog();
                    System.Environment.Exit(0);
                }
                else script_fullpath = args[0];
            }
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
            myScripts = new Script(this);

            if (script_fullpath == "")
            {
                FirstSettingForm cdata = new FirstSettingForm(this, mkl_max_thread);
                cdata.ShowDialog();
            }
            else if (selected_driver_name != "")
            {
                int result;
                unsafe
                {
                    fixed (Char* ss = selected_driver_name)
                    {
                        result = asio.infoget(ss);
                    }
                }
                if (result < 0)
                {
                    String error_msg = "";
                    unsafe
                    {
                        error_msg = new String(asio.error_message());
                    }
                    System.Windows.Forms.MessageBox.Show("Driver does not exist: " + selected_driver_name + ":" + error_msg);
                    System.Environment.Exit(0);
                }
                else
                {
                    without_asio = false;
                }
                asio.SetDataLength(default_dleng, without_asio);
            }
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

            script_count_init = menuItem_script.MenuItems.Count;
            CheckForIllegalCrossThreadCalls = false;
            SetScriptLabel();

            Setup_mainform();

            if (script_fullpath != "" && myScripts.LoadScript(script_fullpath))
            {
                myScripts.ExecScript();
                //                asio.exit();
                //                System.Environment.Exit(0);
            }

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

        public bool Exec_cmd(string cmd_text)
        {
            int[] ch_list;
            bool result = true;

            ArrayList argv = Separate_arg(cmd_text);
            string cmd = (string)(string)argv[0];
            if (cmd == "data_create")
            {
                ch_list = Text2intarray((string)argv[2]);
                ChannelInfo.StreamType ch_type = ChannelInfo.StreamType.no_io;
                if ((string)argv[1] == "IN") ch_type = ChannelInfo.StreamType.asioin;
                else if ((string)argv[1] == "OUT") ch_type = ChannelInfo.StreamType.asioout;
                for (int ii = 0; ii < ch_list.Length; ii++)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[1], ch_list[ii]);
                    if (DataCreate(cinfo, ch_type) == false) break;
                }
            }
            else if (cmd == "PlayStart") { PlayStart(); }
            else if (cmd == "Clear")
            {
                ChannelInfo cinfo = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                DataClear(cinfo);
            }
            else if (cmd == "SetData")
            {
                int data_no = System.Convert.ToInt32((string)argv[1]);
                ChannelInfo cinfo = Find_ch_info((string)argv[2], System.Convert.ToInt32((string)argv[3]));
                SetData(data_no, cinfo);
            }
            else if (cmd == "Delete")
            {
                ChannelInfo cinfo = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                DataDispose(cinfo);
            }
            else if (cmd == "WavRead")
            {
                ch_list = Text2intarray((string)argv[4]);
                if (ch_list.Length == 1)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[3], ch_list[0]);
                    string filename = (string)argv[1];
                    WavRead_script(filename, cinfo);
                }
                else
                {
                    for (int ii = 0; ii < ch_list.Length; ii++)
                    {
                        ChannelInfo cinfo = Find_ch_info((string)argv[3], ch_list[ii]);
                        string file1 = string.Format((string)argv[1], ch_list[ii]);
                        WavRead_script(file1, cinfo);
                    }
                }
            }
            else if (cmd == "WavExport")
            {
                string filename = (string)argv[1];

                ch_list = Text2intarray((string)argv[3]);
                if (ch_list.Length == 1)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[0]);
                    if (WavExport(filename, cinfo, true) == false) return false;  // Error
                }
                else
                {
                    for (int ii = 0; ii < ch_list.Length; ii++)
                    {
                        ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[ii]);
                        string file1 = string.Format((string)argv[1], ch_list[ii]);
                        if (WavExport(file1, cinfo, true) == false) return false;  // Error
                    }
                }
            }
            else if (cmd == "WavImport")
            {
                string filename = (string)argv[1];

                ch_list = Text2intarray((string)argv[3]);
                if (ch_list.Length == 1)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[0]);
                    WavRead_script(filename, cinfo);
                }
                else
                {
                    for (int ii = 0; ii < ch_list.Length; ii++)
                    {
                        ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[ii]);
                        string file1 = string.Format((string)argv[1], ch_list[ii]);
                        WavRead_script(file1, cinfo);
                    }
                }
            }
            else if (cmd == "WavVerify")
            {
                string filename = (string)argv[1];

                ch_list = Text2intarray((string)argv[3]);
                int offset = System.Convert.ToInt32((string)argv[4]);
                if (ch_list.Length == 1)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[0]);
                    WavVerify_script(filename, cinfo, offset);
                }
                else
                {
                    for (int ii = 0; ii < ch_list.Length; ii++)
                    {
                        ChannelInfo cinfo = Find_ch_info((string)argv[2], ch_list[ii]);
                        string file1 = string.Format(filename, ch_list[ii]);
                        WavVerify_script(file1, cinfo, offset);
                    }
                }
            }
            else if (cmd == "move_forward")
            {
                int leng = System.Convert.ToInt32((string)argv[1]);
                ChannelInfo cinfo = Find_ch_info((string)argv[2], System.Convert.ToInt32((string)argv[3]));
                asio.move_forward((int)cinfo.type, cinfo.btn_no, leng);
            }
            else if (cmd == "Copy")
            {
                ChannelInfo src_cinfo = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo dst_cinfo = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                asio.CopyData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
            }
            else if (cmd == "Subtract")
            {
                ChannelInfo src_cinfo = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo dst_cinfo = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                asio.SubData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
            }
            else if (cmd == "Verify")
            {
                ChannelInfo src_ch = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo dst_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                double ss = VerifyData(src_ch, dst_ch);
                Button_status_verify(dst_ch.btn, ss);
            }
            else if (cmd == "Divide")
            {
                int fftpnt = System.Convert.ToInt32((string)argv[1]);
                ChannelInfo src_cinfo = Find_ch_info((string)argv[2], System.Convert.ToInt32((string)argv[3]));
                ChannelInfo dst_cinfo = Find_ch_info((string)argv[4], System.Convert.ToInt32((string)argv[5]));
                asio.DivData((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no, fftpnt);
            }
            else if (cmd == "Convolution")
            {
                int fftpnt = System.Convert.ToInt32((string)argv[1]);
                double scale = System.Convert.ToDouble((string)argv[2]);
                double freq_st = System.Convert.ToDouble((string)argv[3]);
                double freq_ed = System.Convert.ToDouble((string)argv[4]);
                ChannelInfo src_cinfo = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));

                ch_list = Text2intarray((string)argv[10]);
                if (ch_list.Length == 1)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[9], ch_list[0]);
                    asio.Convolution((int)src_cinfo.type, src_cinfo.btn_no, (int)cinfo.type, cinfo.btn_no, (int)cinfo.type, cinfo.btn_no, fftpoint, scale, freq_st, freq_ed);
                }
                else
                {
                    for (int ii = 0; ii < ch_list.Length; ii++)
                    {
                        ChannelInfo cinfo = Find_ch_info((string)argv[9], ch_list[ii]);
                        asio.Convolution((int)src_cinfo.type, src_cinfo.btn_no, (int)cinfo.type, cinfo.btn_no, (int)cinfo.type, cinfo.btn_no, fftpoint, scale, freq_st, freq_ed);
                    }
                }
            }
            else if (cmd == "Deconvolution")
            {
                ChannelInfo in_ch1 = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo in_ch2 = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                ChannelInfo out_ch = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));
                int fftpnt = System.Convert.ToInt32((string)argv[7]);
                double scale = System.Convert.ToDouble((string)argv[8]);
                double freq_st = System.Convert.ToDouble((string)argv[9]);
                double freq_ed = System.Convert.ToDouble((string)argv[10]);
                asio.Deconvolution((int)in_ch1.type, in_ch1.btn_no, (int)in_ch2.type, in_ch2.btn_no, (int)out_ch.type, out_ch.btn_no, fftpoint, scale, freq_st, freq_ed);
                PostCreateSignal(out_ch, "Deconvolution:" + in_ch1.ToString() + "/" + in_ch2.ToString());
            }
            else if (cmd == "WavConvolution")
            {
                string in_file = (string)argv[1];
                string out_file = (string)argv[2];
                ChannelInfo filt_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                WavConvolution(filt_ch, in_file, out_file);
            }
            else if (cmd == "BinRead")
            {
                int filesize = System.Convert.ToInt32((string)argv[2]);
                int dleng = System.Convert.ToInt32((string)argv[3]);
                bool byteswap = System.Convert.ToBoolean((string)argv[4]);
                ChannelInfo cinfo = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));
                BinaryImport BIForm = new BinaryImport(cinfo, (string)argv[1], filesize, this);
                BIForm.GetRead(dleng, byteswap);
            }
            else if (cmd == "makePureTone")
            {
                double amp = System.Convert.ToDouble((string)argv[1]);
                double freq = System.Convert.ToDouble((string)argv[2]);
                ChannelInfo cinfo = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                asio.makePureTone(amp, freq, (int)cinfo.type, cinfo.btn_no);
                PostCreateSignal(cinfo, "PureTone was created.");
            }
            else if (cmd == "makeTSP")
            {
                int fftpoint = System.Convert.ToInt32((string)argv[1]);
                double tsp_amp = System.Convert.ToDouble((string)argv[2]);
                int coef_m_numer = System.Convert.ToInt32((string)argv[3]);
                int coef_m_denom = System.Convert.ToInt32((string)argv[4]);
                double coef_shift = System.Convert.ToDouble((string)argv[5]);
                int flag = System.Convert.ToInt32((string)argv[6]);
                ChannelInfo cinfo = Find_ch_info((string)argv[7], System.Convert.ToInt32((string)argv[8]));
                asio.makeTSP(fftpoint, (int)cinfo.type, cinfo.btn_no, tsp_amp, coef_m_numer, coef_m_denom, coef_shift, flag);
                PostCreateSignal(cinfo, "TSP was created.");
            }
            else if (cmd == "makePinkNoise")
            {
                double amp = System.Convert.ToDouble((string)argv[1]);
                uint rndm_coef = System.Convert.ToUInt32((string)argv[2]);
                bool is_voss_algorithm = ((string)argv[3] == "Voss");

                ch_list = Text2intarray((string)argv[5]);
                uint rndm_coef1 = rndm_coef;
                for (int ii = 0; ii < ch_list.Length; ii++)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[4], ch_list[ii]);
                    if (rndm_coef == 0) rndm_coef1 = (uint)(ii + 1);
                    if (is_voss_algorithm) asio.makePinkNoise_Voss(amp, rndm_coef1, (int)cinfo.type, cinfo.btn_no);
                    else asio.makePinkNoise_Pole(amp, rndm_coef1, (int)cinfo.type, cinfo.btn_no);
                    PostCreateSignal(cinfo, "Pink Noise (" + (string)argv[3] + ": seed=" + rndm_coef1.ToString() + ") was created.");
                }
            }
            else if (cmd == "makeWhiteNoise")
            {
                double amp = System.Convert.ToDouble((string)argv[1]);
                uint rndm_coef = System.Convert.ToUInt32((string)argv[2]);
                ChannelInfo cinfo = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                asio.makeWhiteNoise(amp, rndm_coef, (int)cinfo.type, cinfo.btn_no);
                PostCreateSignal(cinfo, "White Noise was created.");
            }
            else if (cmd == "makePulse")
            {
                double amp = System.Convert.ToDouble((string)argv[1]);
                int delay = System.Convert.ToInt32((string)argv[2]);
                int width = System.Convert.ToInt32((string)argv[3]);
                ch_list = Text2intarray((string)argv[5]);
                for (int ii = 0; ii < ch_list.Length; ii++)
                {
                    ChannelInfo cinfo = Find_ch_info((string)argv[4], ch_list[ii]);
                    asio.makePulse(amp, delay, width, (int)cinfo.type, cinfo.btn_no);
                    PostCreateSignal(cinfo, "Pulse was created.");
                }
            }
            else if (cmd == "makeFilter")
            {
                int freq_from = System.Convert.ToInt32((string)argv[1]);
                int freq_to = System.Convert.ToInt32((string)argv[2]);
                int level_from = System.Convert.ToInt32((string)argv[3]);
                int level_to = System.Convert.ToInt32((string)argv[4]);
                int fftpoint = System.Convert.ToInt32((string)argv[5]);
                ChannelInfo cinfo = Find_ch_info((string)argv[6], System.Convert.ToInt32((string)argv[7]));
                asio.makeFilter(freq_from, freq_to, level_from, level_to, fftpoint, (int)cinfo.type, cinfo.btn_no);
                PostCreateSignal(cinfo, "Filter was created.");
            }
            else if (cmd == "makeSaw")
            {
                double max_val = System.Convert.ToDouble((string)argv[1]);
                double min_val = System.Convert.ToDouble((string)argv[2]);
                int period_point = System.Convert.ToInt32((string)argv[3]);
                bool max2min = System.Convert.ToBoolean((string)argv[4]);
                ChannelInfo cinfo = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));
                asio.makeSaw((int)cinfo.type, cinfo.btn_no, max_val, min_val, period_point, max2min);
                PostCreateSignal(cinfo, "Saw was created.");
            }
            else if (cmd == "Add")
            {

                ChannelInfo d1_ch = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo d2_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                ChannelInfo d3_ch = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));
                asio.Add((int)d1_ch.type, d1_ch.btn_no, (int)d2_ch.type, d2_ch.btn_no, (int)d3_ch.type, d3_ch.btn_no);
                PostCreateSignal(d3_ch, "Add:" + d1_ch.ToString() + "+" + d2_ch.ToString());
            }
            else if (cmd == "Multiply")
            {
                ChannelInfo d1_ch = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo d2_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                ChannelInfo d3_ch = Find_ch_info((string)argv[5], System.Convert.ToInt32((string)argv[6]));
                asio.Multiply((int)d1_ch.type, d1_ch.btn_no, (int)d2_ch.type, d2_ch.btn_no, (int)d3_ch.type, d3_ch.btn_no);
                PostCreateSignal(d3_ch, "Multiply:" + d1_ch.ToString() + "+" + d2_ch.ToString());
            }
            else if (cmd == "IIRLPF" || cmd == "IIRHPF")
            {
                ChannelInfo in_ch = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo out_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                int filter_order = System.Convert.ToInt32((string)argv[5]);
                double cut_freq = System.Convert.ToDouble((string)argv[6]);
                double ripple = System.Convert.ToDouble((string)argv[7]);
                double scale = System.Convert.ToDouble((string)argv[8]);
                if (cmd == "IIRLPF")
                    asio.IIR_LPF(filter_order, cut_freq, ripple, scale, (int)in_ch.type, in_ch.btn_no, (int)out_ch.type, out_ch.btn_no);
                else
                    asio.IIR_LPF(filter_order, cut_freq, ripple, scale, (int)in_ch.type, in_ch.btn_no, (int)out_ch.type, out_ch.btn_no);
                PostCreateSignal(out_ch, cmd + ": in ch=" + in_ch.btn_no.ToString());
            }
            else if (cmd == "OCTBPF" || cmd == "IIRBPF")
            {
                ChannelInfo in_ch = Find_ch_info((string)argv[1], System.Convert.ToInt32((string)argv[2]));
                ChannelInfo out_ch = Find_ch_info((string)argv[3], System.Convert.ToInt32((string)argv[4]));
                int filter_order = System.Convert.ToInt32((string)argv[5]);
                double center_freq = System.Convert.ToDouble((string)argv[6]); // :lcut_freq for IIRBPF
                double band_width = System.Convert.ToDouble((string)argv[7]); // :hcut_freq for IIRBPF
                double ripple = System.Convert.ToDouble((string)argv[8]);
                double scale = System.Convert.ToDouble((string)argv[9]);
                if (cmd == "OCTBPF")
                    asio.IIR_OCTBPF(filter_order, center_freq, band_width, ripple, scale, (int)in_ch.type, in_ch.btn_no, (int)out_ch.type, out_ch.btn_no);
                else
                    asio.IIR_BPF(filter_order, center_freq, band_width, ripple, scale, (int)in_ch.type, in_ch.btn_no, (int)out_ch.type, out_ch.btn_no);
                PostCreateSignal(out_ch, cmd + ": in ch=" + in_ch.btn_no.ToString());
            }
            else if (cmd == "Wav2chto1ch")
            {
                string in_file = (string)argv[1];
                string out_file = (string)argv[2];
                int ch_selected = System.Convert.ToInt32((string)argv[3]);
                WavStream ws = new WavStream(this);
                ws.Wav2chto1ch_s(in_file, out_file, ch_selected);
            }
            //else if (cmd == "SetAmp")
            //{
            //    int ch = System.Convert.ToInt32((string)argv[1]);
            //    double amp = System.Convert.ToDouble((string)argv[2]);
            //    asio.SetAmp(ch, amp);
            //}
            else if (cmd == "ReadWav")
            {
                wav_full_path = (string)argv[1];
                wav_read_ch = (string)argv[2];
                double start_time = System.Convert.ToDouble((string)argv[3]);
                double end_time = System.Convert.ToDouble((string)argv[4]);
                wav_read_point = (int)(start_time * sample_freq);
                wav_read_length = (int)(end_time * sample_freq);
                wav_continuous = System.Convert.ToBoolean((string)argv[5]);
                Read_wav_cmd();
            }
            else if (cmd == "RtconvFPGAInit")
            {
                int inch = System.Convert.ToInt32((string)argv[1]);
                int outch = System.Convert.ToInt32((string)argv[2]);
                String file_path = (string)argv[3];
                Rtconv_fpga_init(file_path, inch, outch);
            }
            else if (cmd == "SPGain")
            {
                int current_val = System.Convert.ToInt32((string)argv[1]);
                asio.spamp_gain_push(current_val);
                if (asio.get_stop() == true) asio.damp_volume_set(current_val);
            }
            else
            {
                result = false;
            }

            return result;
        }

        //public int Find_asio_ch(string buff_type, int btn_no)
        //{
        //    switch (buff_type)
        //    {
        //        case "IN":
        //            return Find_asio_ch(ch_info_in[btn_no - 1]);
        //        case "OUT":
        //            return Find_asio_ch(ch_info_out[btn_no - 1]);
        //        case "WORK":
        //            return Find_asio_ch(ch_info_work[btn_no - 1]);
        //    }
        //    return 0;
        //}

        //public int Find_asio_ch(ChannelInfo info)
        //{
        //    if (info == null) return -1;
        //    return asio.find_asio_ch((int)info.type, info.btn_no);
        //}

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
            if (command_mode) return;
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem_read_wav = new System.Windows.Forms.MenuItem();
            this.menuItem_write_wav = new System.Windows.Forms.MenuItem();
            this.menuItem_Exit = new System.Windows.Forms.MenuItem();
            this.menuItem_Data = new System.Windows.Forms.MenuItem();
            this.menuItem_FFTpoint = new System.Windows.Forms.MenuItem();
            this.menuItem_ch_assign = new System.Windows.Forms.MenuItem();
            this.menuItem_clearall = new System.Windows.Forms.MenuItem();
            this.menuItem_Processing = new System.Windows.Forms.MenuItem();
            this.menuItem_Conv = new System.Windows.Forms.MenuItem();
            this.menuItem_DeConv = new System.Windows.Forms.MenuItem();
            this.menuItem_Add = new System.Windows.Forms.MenuItem();
            this.menuItem_Mpy = new System.Windows.Forms.MenuItem();
            this.menuItem_cvt = new System.Windows.Forms.MenuItem();
            this.menuItem_IIR = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem_CheckBoSC = new System.Windows.Forms.MenuItem();
            this.menuItem_rtconv = new System.Windows.Forms.MenuItem();
            this.menuItem_rtconv_fpga = new System.Windows.Forms.MenuItem();
            this.menuItem_rtconv_aiao = new System.Windows.Forms.MenuItem();
            this.menuItem_rtconv_fiao = new System.Windows.Forms.MenuItem();
            this.menuItem_FileConv = new System.Windows.Forms.MenuItem();
            this.menuItem_eqtest = new System.Windows.Forms.MenuItem();
            this.menuItem_amp = new System.Windows.Forms.MenuItem();
            this.menuItem_spgain = new System.Windows.Forms.MenuItem();
            this.menuItem_micgain = new System.Windows.Forms.MenuItem();
            this.menuItem_geq = new System.Windows.Forms.MenuItem();
            this.menuItem_NetPlayer = new System.Windows.Forms.MenuItem();
            this.menuItem_boscplayer = new System.Windows.Forms.MenuItem();
            this.menuItem_ARS = new System.Windows.Forms.MenuItem();
            this.menuItem_evaltest = new System.Windows.Forms.MenuItem();
            this.menuItem_wavstreaming = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem_mover_server = new System.Windows.Forms.MenuItem();
            this.menuItem_ueserver = new System.Windows.Forms.MenuItem();
            this.menuItem_ZYBO = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem_afs = new System.Windows.Forms.MenuItem();
            this.menuItem_IR_Calc = new System.Windows.Forms.MenuItem();
            this.menuItem_InvSystemDesign = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem_ImpMeas = new System.Windows.Forms.MenuItem();
            this.menuItem_LevelMeter = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem_script = new System.Windows.Forms.MenuItem();
            this.menuItem_scriptRec = new System.Windows.Forms.MenuItem();
            this.menuItem_scriptRecON = new System.Windows.Forms.MenuItem();
            this.menuItem_scriptRecOFF = new System.Windows.Forms.MenuItem();
            this.menuItem_scriptLoad = new System.Windows.Forms.MenuItem();
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
            this.menuItem_stream = new System.Windows.Forms.MenuItem();
            this.menuItem_wav_stream = new System.Windows.Forms.MenuItem();
            this.menuItem_double_stream = new System.Windows.Forms.MenuItem();
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
            this.menuItem_Processing,
            this.menuItem3,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem_script,
            this.menuItem_Help});
            // 
            // menuItem_File
            // 
            this.menuItem_File.Index = 0;
            this.menuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_workfolder,
            this.menuItem_Save,
            this.menuItem1,
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
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_read_wav,
            this.menuItem_write_wav});
            this.menuItem1.Text = "Wav";
            // 
            // menuItem_read_wav
            // 
            this.menuItem_read_wav.Index = 0;
            this.menuItem_read_wav.Text = "Read";
            this.menuItem_read_wav.Click += new System.EventHandler(this.MenuItem_read_wav_Click);
            // 
            // menuItem_write_wav
            // 
            this.menuItem_write_wav.Index = 1;
            this.menuItem_write_wav.Text = "Write";
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Index = 3;
            this.menuItem_Exit.Text = "Exit";
            this.menuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menuItem_Data
            // 
            this.menuItem_Data.Index = 1;
            this.menuItem_Data.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_FFTpoint,
            this.menuItem_ch_assign,
            this.menuItem_clearall});
            this.menuItem_Data.Text = "Set";
            // 
            // menuItem_FFTpoint
            // 
            this.menuItem_FFTpoint.Index = 0;
            this.menuItem_FFTpoint.Text = "FFTpoint";
            this.menuItem_FFTpoint.Click += new System.EventHandler(this.MenuItem_FFTpoint_Click);
            // 
            // menuItem_ch_assign
            // 
            this.menuItem_ch_assign.Index = 1;
            this.menuItem_ch_assign.Text = "Channel Assign";
            // 
            // menuItem_clearall
            // 
            this.menuItem_clearall.Index = 2;
            this.menuItem_clearall.Text = "Clear All";
            // 
            // menuItem_Processing
            // 
            this.menuItem_Processing.Index = 2;
            this.menuItem_Processing.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Conv,
            this.menuItem_DeConv,
            this.menuItem_Add,
            this.menuItem_Mpy,
            this.menuItem_cvt,
            this.menuItem_IIR});
            this.menuItem_Processing.Text = "Processing";
            this.menuItem_Processing.Click += new System.EventHandler(this.MenuItem_Processing_Click);
            // 
            // menuItem_Conv
            // 
            this.menuItem_Conv.Index = 0;
            this.menuItem_Conv.Text = "Convolution";
            this.menuItem_Conv.Click += new System.EventHandler(this.MenuItem_Processing_Click);
            // 
            // menuItem_DeConv
            // 
            this.menuItem_DeConv.Index = 1;
            this.menuItem_DeConv.Text = "Deconvolution";
            this.menuItem_DeConv.Click += new System.EventHandler(this.MenuItem_Processing_Click);
            // 
            // menuItem_Add
            // 
            this.menuItem_Add.Index = 2;
            this.menuItem_Add.Text = "Add";
            this.menuItem_Add.Click += new System.EventHandler(this.MenuItem_Processing_Click);
            // 
            // menuItem_Mpy
            // 
            this.menuItem_Mpy.Index = 3;
            this.menuItem_Mpy.Text = "Multiply";
            this.menuItem_Mpy.Click += new System.EventHandler(this.MenuItem_Processing_Click);
            // 
            // menuItem_cvt
            // 
            this.menuItem_cvt.Index = 4;
            this.menuItem_cvt.Text = "Wav convert";
            this.menuItem_cvt.Click += new System.EventHandler(this.MenuItem_cvt_Click);
            // 
            // menuItem_IIR
            // 
            this.menuItem_IIR.Index = 5;
            this.menuItem_IIR.Text = "IIR Filter";
            this.menuItem_IIR.Click += new System.EventHandler(this.MenuItem_IIR_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_CheckBoSC,
            this.menuItem_rtconv,
            this.menuItem_amp,
            this.menuItem_NetPlayer,
            this.menuItem4});
            this.menuItem3.Text = "BoSC";
            // 
            // menuItem_CheckBoSC
            // 
            this.menuItem_CheckBoSC.Index = 0;
            this.menuItem_CheckBoSC.Text = "Check System";
            this.menuItem_CheckBoSC.Click += new System.EventHandler(this.MenuItem_CheckBoSC_Click);
            // 
            // menuItem_rtconv
            // 
            this.menuItem_rtconv.Index = 1;
            this.menuItem_rtconv.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_rtconv_fpga,
            this.menuItem_rtconv_aiao,
            this.menuItem_rtconv_fiao,
            this.menuItem_FileConv,
            this.menuItem_eqtest});
            this.menuItem_rtconv.Text = "Realtime Conv.";
            // 
            // menuItem_rtconv_fpga
            // 
            this.menuItem_rtconv_fpga.Index = 0;
            this.menuItem_rtconv_fpga.Text = "Use FPGA";
            this.menuItem_rtconv_fpga.Click += new System.EventHandler(this.MenuItem_rtconv_fpga_Click);
            // 
            // menuItem_rtconv_aiao
            // 
            this.menuItem_rtconv_aiao.Index = 1;
            this.menuItem_rtconv_aiao.Text = "Audio in/Audio out";
            this.menuItem_rtconv_aiao.Click += new System.EventHandler(this.MenuItem_rtconv_aiao_Click);
            // 
            // menuItem_rtconv_fiao
            // 
            this.menuItem_rtconv_fiao.Index = 2;
            this.menuItem_rtconv_fiao.Text = "File in/Audio out";
            this.menuItem_rtconv_fiao.Click += new System.EventHandler(this.MenuItem_rtconv_fiao_Click);
            // 
            // menuItem_FileConv
            // 
            this.menuItem_FileConv.Index = 3;
            this.menuItem_FileConv.Text = "File in/File out";
            this.menuItem_FileConv.Click += new System.EventHandler(this.MenuItem_FileConv_Click);
            // 
            // menuItem_eqtest
            // 
            this.menuItem_eqtest.Index = 4;
            this.menuItem_eqtest.Text = "EQ. test";
            // 
            // menuItem_amp
            // 
            this.menuItem_amp.Index = 2;
            this.menuItem_amp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_spgain,
            this.menuItem_micgain,
            this.menuItem_geq});
            this.menuItem_amp.Text = "Controller";
            // 
            // menuItem_spgain
            // 
            this.menuItem_spgain.Index = 0;
            this.menuItem_spgain.Text = "Sp. Amp. Gain";
            this.menuItem_spgain.Click += new System.EventHandler(this.MenuItem_spgain_Click);
            // 
            // menuItem_micgain
            // 
            this.menuItem_micgain.Index = 1;
            this.menuItem_micgain.Text = "Mic. Amp. Gain";
            this.menuItem_micgain.Click += new System.EventHandler(this.MenuItem_micamp_Click);
            // 
            // menuItem_geq
            // 
            this.menuItem_geq.Index = 2;
            this.menuItem_geq.Text = "Equalizer";
            this.menuItem_geq.Click += new System.EventHandler(this.MenuItem_geq_Click);
            // 
            // menuItem_NetPlayer
            // 
            this.menuItem_NetPlayer.Index = 3;
            this.menuItem_NetPlayer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_boscplayer,
            this.menuItem_ARS,
            this.menuItem_evaltest,
            this.menuItem_wavstreaming});
            this.menuItem_NetPlayer.Text = "Player ";
            // 
            // menuItem_boscplayer
            // 
            this.menuItem_boscplayer.Index = 0;
            this.menuItem_boscplayer.Text = "BoSC Player";
            this.menuItem_boscplayer.Click += new System.EventHandler(this.menuItem_boscplayer_Click);
            // 
            // menuItem_ARS
            // 
            this.menuItem_ARS.Index = 1;
            this.menuItem_ARS.Text = "Audio Room Simulator";
            this.menuItem_ARS.Click += new System.EventHandler(this.menuItem_ARS_Click);
            // 
            // menuItem_evaltest
            // 
            this.menuItem_evaltest.Index = 2;
            this.menuItem_evaltest.Text = "Evalation Test";
            this.menuItem_evaltest.Click += new System.EventHandler(this.menuItem_evaltest_Click);
            // 
            // menuItem_wavstreaming
            // 
            this.menuItem_wavstreaming.Index = 3;
            this.menuItem_wavstreaming.Text = "WAV Streaming";
            this.menuItem_wavstreaming.Click += new System.EventHandler(this.menuItem_wavstreaming_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_mover_server,
            this.menuItem_ueserver,
            this.menuItem_ZYBO});
            this.menuItem4.Text = "Experiments";
            // 
            // menuItem_mover_server
            // 
            this.menuItem_mover_server.Index = 0;
            this.menuItem_mover_server.Text = "Mover";
            // 
            // menuItem_ueserver
            // 
            this.menuItem_ueserver.Index = 1;
            this.menuItem_ueserver.Text = "VR Server";
            this.menuItem_ueserver.Click += new System.EventHandler(this.menuItem_ueserver_Click);
            // 
            // menuItem_ZYBO
            // 
            this.menuItem_ZYBO.Index = 2;
            this.menuItem_ZYBO.Text = "ZYBO";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_afs,
            this.menuItem_IR_Calc,
            this.menuItem_InvSystemDesign});
            this.menuItem5.Text = "Design";
            // 
            // menuItem_afs
            // 
            this.menuItem_afs.Index = 0;
            this.menuItem_afs.Text = "Aftereffect supression";
            this.menuItem_afs.Click += new System.EventHandler(this.MenuItem_FIRDesign_Click);
            // 
            // menuItem_IR_Calc
            // 
            this.menuItem_IR_Calc.Index = 1;
            this.menuItem_IR_Calc.Text = "IR Calculation";
            this.menuItem_IR_Calc.Click += new System.EventHandler(this.MenuItem_IR_Calc_Click);
            // 
            // menuItem_InvSystemDesign
            // 
            this.menuItem_InvSystemDesign.Index = 2;
            this.menuItem_InvSystemDesign.Text = "Inv. System Design";
            this.menuItem_InvSystemDesign.Click += new System.EventHandler(this.InvSystemDesign_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
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
            this.menuItem7.Index = 6;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_ImpMeas,
            this.menuItem_LevelMeter,
            this.menuItem9});
            this.menuItem7.Text = "Measurement";
            // 
            // menuItem_ImpMeas
            // 
            this.menuItem_ImpMeas.Index = 0;
            this.menuItem_ImpMeas.Text = "IR Measurement";
            this.menuItem_ImpMeas.Click += new System.EventHandler(this.MenuItem_ImpMeas_Click);
            // 
            // menuItem_LevelMeter
            // 
            this.menuItem_LevelMeter.Index = 1;
            this.menuItem_LevelMeter.Text = "Level Meter";
            this.menuItem_LevelMeter.Click += new System.EventHandler(this.MenuItem_LevelMeter_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            this.menuItem9.Text = "Wave Monitor";
            // 
            // menuItem_script
            // 
            this.menuItem_script.Index = 7;
            this.menuItem_script.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_scriptRec,
            this.menuItem_scriptLoad});
            this.menuItem_script.Text = "Script";
            // 
            // menuItem_scriptRec
            // 
            this.menuItem_scriptRec.Index = 0;
            this.menuItem_scriptRec.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_scriptRecON,
            this.menuItem_scriptRecOFF});
            this.menuItem_scriptRec.Text = "Rec. : Off";
            // 
            // menuItem_scriptRecON
            // 
            this.menuItem_scriptRecON.Index = 0;
            this.menuItem_scriptRecON.Text = "On";
            this.menuItem_scriptRecON.Click += new System.EventHandler(this.MenuItem_scriptRecON_Click);
            // 
            // menuItem_scriptRecOFF
            // 
            this.menuItem_scriptRecOFF.Index = 1;
            this.menuItem_scriptRecOFF.Text = "Off";
            this.menuItem_scriptRecOFF.Click += new System.EventHandler(this.MenuItem_scriptRecOFF_Click);
            // 
            // menuItem_scriptLoad
            // 
            this.menuItem_scriptLoad.Index = 1;
            this.menuItem_scriptLoad.Text = "Load";
            this.menuItem_scriptLoad.Click += new System.EventHandler(this.MenuItem_scriptLoad_Click);
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.Index = 8;
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
            this.menuItem_stream,
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
            // menuItem_stream
            // 
            this.menuItem_stream.Index = 4;
            this.menuItem_stream.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_wav_stream,
            this.menuItem_double_stream});
            this.menuItem_stream.Text = "Streaming";
            // 
            // menuItem_wav_stream
            // 
            this.menuItem_wav_stream.Index = 0;
            this.menuItem_wav_stream.Text = "wav";
            // 
            // menuItem_double_stream
            // 
            this.menuItem_double_stream.Index = 1;
            this.menuItem_double_stream.Text = "double";
            // 
            // menuItem_import
            // 
            this.menuItem_import.Index = 5;
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
            this.menuItem_export.Index = 6;
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
            this.menuItem_copyfrom.Index = 7;
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
            this.menuItem_subtract.Index = 8;
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
            this.menuItem_verify.Index = 9;
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
            this.menuItem_divide.Index = 10;
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
            this.menuItem_bin_import.Index = 11;
            this.menuItem_bin_import.Text = "Binary Import";
            this.menuItem_bin_import.Click += new System.EventHandler(this.MenuItem_bin_import_Click);
            // 
            // menuItem_wav_conv
            // 
            this.menuItem_wav_conv.Index = 12;
            this.menuItem_wav_conv.Text = "Conv. with wav";
            this.menuItem_wav_conv.Click += new System.EventHandler(this.MenuConvWav_Click);
            // 
            // menuItem_adjustamp
            // 
            this.menuItem_adjustamp.Index = 13;
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
                if (script_recording) MyScripts_Add("data_create", cinfo);
            }
        }
        public void MyScripts_Add(string ss1, ChannelInfo cinfo)
        {
            myScripts.Add(ss1 + ScriptSeparator + cinfo.GetBufferType() + ScriptSeparator + cinfo.btn_no.ToString());
        }
        public void MyScripts_Add2(string ss1, ChannelInfo cinfo1, ChannelInfo cinfo2)
        {
            myScripts.Add(ss1 + ScriptSeparator + cinfo1.GetBufferType() + ScriptSeparator + cinfo1.btn_no.ToString()
                + ScriptSeparator + cinfo2.GetBufferType() + ScriptSeparator + cinfo2.btn_no.ToString());
        }
        public void MyScripts_Add3(string ss1, ChannelInfo cinfo1, ChannelInfo cinfo2, ChannelInfo cinfo3)
        {
            myScripts.Add(ss1 + ScriptSeparator + cinfo1.GetBufferType() + ScriptSeparator + cinfo1.btn_no.ToString()
                + ScriptSeparator + cinfo2.GetBufferType() + ScriptSeparator + cinfo2.btn_no.ToString()
                + ScriptSeparator + cinfo3.GetBufferType() + ScriptSeparator + cinfo3.btn_no.ToString());
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
                if (script_recording) MyScripts_Add("data_create", cinfo);
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
        private bool net_trig_start_wait = true;
        private NetworkTcp network_tcp = null;

        public void NetServerStart()
        {
            network_tcp = new NetworkTcp(this, ConsoleMsg, new NetworkTcp.ExecCmdDelegate(Exec_cmd_netstart));
            network_tcp.StartServer(false);
        }
        public void NetServerStop()
        {
            if (network_tcp != null)
            {
                network_tcp.StopServer();
                network_tcp = null;
            }
        }
        private void Exec_cmd_netstart(string cmd_text)
        {
            net_trig_start_wait = false;
        }
        private void Go_btn_Click(object sender, System.EventArgs e)
        {
            if (net_trig_start)  // Wait net start
            {
                while (net_trig_start_wait) Thread.Sleep(10);
                net_trig_start_wait = true;
            }
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
                if (show_level_meter)
                {
                    levelmeter.Redraw();
                }
                if (show_wavemonitor)
                {
                    wavemonitor.Redraw();
                }
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

            if (script_recording) myScripts.Add("PlayStart");
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

        private void MenuItem_Processing_Click(object sender, System.EventArgs e)
        {
            MenuItem mm = (MenuItem)sender;

            DSP_Form dsp = new DSP_Form(this, mm.Text);
            dsp.ShowDialog();
        }

        private void MenuItem_clear_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);
            DataClear(cinfo);
            if (script_recording) MyScripts_Add("Clear", cinfo);
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
            if (script_recording) MyScripts_Add("SetData" + ScriptSeparator + data_no.ToString(), cinfo);
        }

        private void MenuItem_delete_Click(object sender, System.EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);
            DataDispose(cinfo);
            if (script_recording) MyScripts_Add("Delete", cinfo);
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

        public void WavRead_script(string filename, ChannelInfo cinfo)
        {
            WavStream ws = new WavStream(this);
            if (!ws.WavRead(filename, false)) return;
            ws.Load_data(cinfo);
            SetButtonStatus(cinfo, BufferStatus.signal, " was created.");
            go_btn.Enabled = true;
            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
        }

        public void WavVerify_script(string filename, ChannelInfo cinfo, int offset)
        {
            WavStream ws = new WavStream(this);
            if (!ws.WavRead(filename, false)) return;
            double sn = ws.Verify_data(cinfo, offset);
            Button_status_verify(cinfo.btn, sn);
            Cnsl_msg("Verify : SN = " + sn.ToString("0.0") + "dB\r\n");
        }

        public void WavRead_menu(string filename, ChannelInfo cinfo)
        {
            WavStream ws = new WavStream(this);
            if (!ws.WavRead(filename, false)) return;
            ws.Load_data(cinfo);
            SetButtonStatus_Signal(cinfo);
            string filename1 = filename;
            if (script_recording) MyScripts_Add("WavRead" + ScriptSeparator + filename1, cinfo);
        }

        public void TextRead_menu(string filename, ChannelInfo cinfo, bool isHex)
        {
            WavStream ws = new WavStream(this);
            ws.TextRead(filename, isHex);
            ws.Load_data(cinfo);
            asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
            string filename1 = filename;
            if (script_recording)
            {
                string cmd = (isHex ? "HexRead" : "DecRead");
                MyScripts_Add(cmd + ScriptSeparator + filename1, cinfo);
            }
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
            if (script_recording) myScripts.Add("change_fftpoint" + ScriptSeparator + fftpoint.ToString());
        }

        private void MenuItem_Exit_Click(object sender, System.EventArgs e)
        {
            if (network_tcp != null) network_tcp.StopServer();
            if (show_level_meter) levelmeter.Close();
            if (show_wavemonitor) wavemonitor.Close();
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

            if (greq != null) greq.Dispose();
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
                bool succeed = WavExport(fd.filename, cinfo, false);
                if (succeed && script_recording)
                {
                    MyScripts_Add("WavExport" + ScriptSeparator + fd.filename, cinfo);
                }
            }
        }

        private void MenuItem_wavf_export_Click(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            ContextMenu cm = s.Parent.GetContextMenu();
            ChannelInfo cinfo = Find_ch_info((Button)cm.SourceControl);

            if (fd.SaveFileDialog("wav(float) export file", "wav"))
            {
                bool succeed = WavExport(fd.filename, cinfo, true);
                if (succeed && script_recording)
                {
                    MyScripts_Add("WavExport" + ScriptSeparator + fd.filename, cinfo);
                }
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
            if (script_recording) MyScripts_Add("move_forward" + ScriptSeparator + leng.ToString(), cinfo);
        }

        private void MenuItem_IIR_Click(object sender, System.EventArgs e)
        {
            IIRFilter iirfilter = new IIRFilter(this);
            iirfilter.ShowDialog();
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
                if (script_recording) MyScripts_Add2("Copy", src_cinfo, dst_cinfo);
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
                if (script_recording) MyScripts_Add2("Subtract", src_cinfo, dst_cinfo);
            }
        }

        public double VerifyData(ChannelInfo src_cinfo, ChannelInfo dst_cinfo)
        {
            double val = -1.0;
            if (asio.have_buffer((int)dst_cinfo.type, dst_cinfo.btn_no))
            {
                val = asio.Verify((int)src_cinfo.type, src_cinfo.btn_no, (int)dst_cinfo.type, dst_cinfo.btn_no);
                Cnsl_msg("Verify : " + val.ToString("0.0") + " dB, src ch=" + src_cinfo.type.ToString() + ", " + src_cinfo.btn_no.ToString() + ", dst ch=" + dst_cinfo.type.ToString() + ", " + dst_cinfo.btn_no.ToString() + "\r\n");
                if (script_recording) MyScripts_Add2("Verify", src_cinfo, dst_cinfo);
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
                if (script_recording) MyScripts_Add2("Divide" + ScriptSeparator + fftpoint.ToString(), src_cinfo, dst_cinfo);
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
            double new_scale = asio.Convolution((int)in_ch1.type, in_ch1.btn_no, (int)in_ch2.type, in_ch2.btn_no, (int)out_ch.type, out_ch.btn_no, fftpoint, scale, freq_st, freq_ed);
            if (script_recording)
            {
                MyScripts_Add3("Convolution" + ScriptSeparator + fftpoint.ToString() + ScriptSeparator + scale.ToString()
                    + ScriptSeparator + freq_st.ToString() + ScriptSeparator + freq_ed.ToString(), in_ch1, in_ch2, out_ch);
            }
            return new_scale;
        }

        public void BufDeconvolution(ChannelInfo in_ch1, ChannelInfo in_ch2, ChannelInfo out_ch)
        {
            BufDeconvolution(in_ch1, in_ch2, out_ch, 1.0, 0.0, (double)(sample_freq / 2));
        }

        public void BufDeconvolution(ChannelInfo in_ch1, ChannelInfo in_ch2, ChannelInfo out_ch, double scale, double freq_st, double freq_ed)
        {
            double new_scale = asio.Deconvolution((int)in_ch1.type, in_ch1.btn_no, (int)in_ch2.type, in_ch2.btn_no, (int)out_ch.type, out_ch.btn_no, fftpoint, scale, freq_st, freq_ed);
            if (new_scale < 1.0)
            {
                double nn = new_scale * scale;
                System.Windows.Forms.MessageBox.Show("Overflow in Deconvolution: scale should be " + nn.ToString());
            }
            else if (script_recording)
            {
                MyScripts_Add3("Deconvolution" + ScriptSeparator + fftpoint.ToString() + ScriptSeparator + scale.ToString()
                    + ScriptSeparator + freq_st.ToString() + ScriptSeparator + freq_ed.ToString(), in_ch1, in_ch2, out_ch);
            }
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
            if (script_recording) MyScripts_Add("WavConvolution" + ScriptSeparator + open_file + ScriptSeparator + fd.filename, cinfo);
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

        private void MenuItem_scriptLoad_Click(object sender, System.EventArgs e)
        {
            if (script_count >= max_script_count) return;
            if (fd.OpenFileDialog("Script Load", "*.txt"))
            {
                string script_fullpath = fd.filename;
                script_folder = System.IO.Path.GetDirectoryName(script_fullpath);
                Script newScript = new Script(this);
                if (newScript.LoadScript(script_fullpath) == false)
                {
                    return;
                }
                myScriptArray[script_count] = newScript;

                System.Windows.Forms.MenuItem menuItem_newscript;
                menuItem_newscript = new System.Windows.Forms.MenuItem();
                menuItem_script.MenuItems.Add(menuItem_script.MenuItems.Count, menuItem_newscript);

                menuItem_newscript.Shortcut = shortcuts[script_count];
                menuItem_newscript.Text = newScript.script_name + " (" + shortcutName[script_count] + ")";
                menuItem_newscript.Click += new System.EventHandler(this.MenuItem_scriptPlay_Click);

                script_count++;
            }
        }

        public EscKeyForm esc_key_form = null;
        private void MenuItem_scriptPlay_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.MenuItem menu = (System.Windows.Forms.MenuItem)sender;
            Script theScript = myScriptArray[menu.Index - script_count_init];
            Thread st = new Thread(new ThreadStart(theScript.ExecScript))
            {
                Name = "ExecScript"
            };
            st.Start();
            esc_key_form = new EscKeyForm(this, theScript.script_name);
            esc_key_form.ShowDialog();
        }

        public void CloseEscKey()
        {
            if (esc_key_form != null)
            {
                esc_key_form.Close();
                esc_key_form = null;
            }
        }

        private void MenuItem_scriptRecON_Click(object sender, EventArgs e)
        {
            script_recording = true;
            menuItem_scriptRec.Text = "Rec. : On";
            SetScriptLabel();
        }

        private void MenuItem_scriptRecOFF_Click(object sender, EventArgs e)
        {
            script_recording = false;
            menuItem_scriptRec.Text = "Rec. : Off";
            SetScriptLabel();
            if (fd.SaveFileDialog("Save script file", "txt"))
            {
                myScripts.SaveScript(fd.filename);
            }
        }

        public void SetScriptLabel()
        {
            Cnsl_msg(menuItem_scriptRec.Text + "\r\n");
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
            if (script_recording) MyScripts_Add("Mltiply value" + ScriptSeparator + val.ToString(), cinfo);
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
                if (script_recording)
                {
                    string filename = fd.filename;
                    MyScripts_Add("DecExport" + ScriptSeparator + filename, cinfo);
                }
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
                if (script_recording)
                {
                    string filename = fd.filename;
                    MyScripts_Add("HexExport" + ScriptSeparator + filename, cinfo);
                }
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

        private void MenuItem_cvt_Click(object sender, EventArgs e)
        {
            WavConvert wav_cvt = new WavConvert(this);
            wav_cvt.ShowDialog();
        }

        private void MenuItem_ImpMeas_Click(object sender, EventArgs e)
        {
            ImpMeasForm imp_meas = new ImpMeasForm(this);
            imp_meas.ShowDialog();
        }

        private void MenuItem_CheckBoSC_Click(object sender, EventArgs e)
        {
            CheckSystemForm check_system = new CheckSystemForm(this);
            check_system.ShowDialog();
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
        private LevelMeter levelmeter = null;
        private bool show_level_meter = false;

        private void MenuItem_LevelMeter_Click(object sender, EventArgs e)
        {
            Thread levelmeter_thread = new Thread(new ThreadStart(LevelMeterShow))
            {
                Priority = ThreadPriority.Lowest,
                Name = "LevelMeterShow"
            };
            levelmeter_thread.Start();
        }

        private void LevelMeterShow()
        {
            levelmeter = new LevelMeter(this);
            show_level_meter = true;
            levelmeter.ShowDialog();
            show_level_meter = false;
        }
        private WaveMonitorForm wavemonitor = null;
        public bool show_wavemonitor = false;

        private void MenuItem_waveMonitor_Click(object sender, EventArgs e)
        {
            Thread wavemonitor_thread = new Thread(new ThreadStart(WaveMonitorShow))
            {
                Priority = ThreadPriority.Lowest,
                Name = "WaveMonitorShow"
            };
            wavemonitor_thread.Start();
        }

        private void WaveMonitorShow()
        {
            wavemonitor = new WaveMonitorForm(this);
            show_wavemonitor = true;
            if (show_level_meter)
            {
                levelmeter.open_wavemonitor(true);
            }
            wavemonitor.ShowDialog();
            if (show_level_meter)
            {
                levelmeter.open_wavemonitor(false);
            }
            show_wavemonitor = false;
        }

        public string WaveMonitorInfo(ChannelInfo cinfo)
        {
            return cinfo.max_freq.ToString("0.0") + "Hz : " + cinfo.max_level.ToString("0.0") + "dB";
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

        private void MenuItem_IR_Calc_Click(object sender, EventArgs e)
        {
            IR_Calc ir_calc = new IR_Calc(this);
            ir_calc.ShowDialog();
        }

        private void InvSystemDesign_Click(object sender, EventArgs e)
        {
            MakeInv mkinv = new MakeInv(this);
            mkinv.ShowDialog();
        }

        //private void MenuItem_ch_assign_Click(object sender, EventArgs e)
        //{
        //    AssignCh assign_ch = new AssignCh(this);
        //    assign_ch.ShowDialog();

        //    Create_ch_info();
        //    Reset_inout_btn();
        //}

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

        private void MenuItem_FIRDesign_Click(object sender, EventArgs e)
        {
            if (pwave == null)
            {
                pwave = new ProcessWave(this);
            }
            if (pwave.success)
            {
                pwave.ShowDialog();
            }
            else
            {
                MessageBox.Show(pwave.message);
            }
        }

        public string wav_full_path = "";
        public string wav_read_ch;
        public int wav_read_point;
        public int wav_read_length;
        public bool wav_continuous;

        private void MenuItem_read_wav_Click(object sender, EventArgs e)
        {
            WavFileForm wavFileForm = new WavFileForm(this);
            wavFileForm.ShowDialog();
            if (wav_full_path != "")
            {
                string ch_list = wav_read_ch.Replace(" ", "");
                Read_wav_cmd();
                if (script_recording) myScripts.Add("ReadWav" + ScriptSeparator + wav_full_path + ScriptSeparator + ch_list + ScriptSeparator + wav_read_point.ToString() + ScriptSeparator + wav_read_length.ToString() + ScriptSeparator + wav_continuous.ToString());
            }
        }
        public void Read_wav_cmd()
        {
            WavStream ws = new WavStream(this);
            if (!ws.WavRead(wav_full_path, false)) return;
            int[] wav_read_ch_list = Text2intarray(wav_read_ch);
            if (wav_read_length > ws.WordCount())
            {
                wav_read_length = ws.WordCount();
            }
            if (wav_read_point + wav_read_length > asio.GetDataLength())
            {
                wav_read_length = asio.GetDataLength() - wav_read_point;
            }
            if (wav_continuous)
            {
                int lll = wav_read_length / wav_read_ch_list.Length;
                int ppp = wav_read_point;

                for (int ii = 0; ii < wav_read_ch_list.Length; ii++)
                {
                    int ch = wav_read_ch_list[ii];
                    ChannelInfo cinfo = Find_ch_info("OUT", ch);
                    ws.Load_data1(cinfo, ppp, lll);
                    SetButtonStatus(cinfo, BufferStatus.signal, " was created.");
                    asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                    ppp += lll;
                }
            }
            else
            {
                for (int ii = 0; ii < wav_read_ch_list.Length; ii++)
                {
                    int ch = wav_read_ch_list[ii];
                    ChannelInfo cinfo = Find_ch_info("OUT", ch);
                    ws.Load_data1(cinfo, wav_read_point, wav_read_length);
                    SetButtonStatus(cinfo, BufferStatus.signal, " was created.");
                    asio.SetMaxVal((int)cinfo.type, cinfo.btn_no);
                }
            }
            go_btn.Enabled = true;
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

        private void MenuItem_micamp_Click(object sender, EventArgs e)
        {
            SetMicAmpForm mic_amp_set = new SetMicAmpForm(this);
            mic_amp_set.ShowDialog();
        }

        private void Set_asio_mode(ASIOmode asio_mode)
        {
            asio.push_asio_mode((int)asio_mode);
        }

        private void MenuItem_FileConv_Click(object sender, EventArgs e)
        {
            file_conv = new FileConvForm(this);
            file_conv.ShowDialog();
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

        public GrphEqForm greq = null;
        private void Greq_go()
        {
            greq = new GrphEqForm(this, convset!=null);
            greq.ShowDialog();
            greq = null;
        }

        private void MenuItem_rtconv_fpga_Click(object sender, EventArgs e)
        {
            int rtconv_inch = 1;
            int rtconv_outch = 96;
            string file_path = "";
            if (fd.OpenFileDialog("Conv source", "*.wav"))
            {
                string filename = fd.filename;
                file_path = filename.Remove(filename.Length - 11);
                string filename2 = "";
                for (int ich = 1; ich <= rtconv_inch; ich++)
                {
                    for (int och = 1; och <= rtconv_outch; och++)
                    {
                        filename2 = file_path + ich.ToString("000") + "_" + och.ToString("000") + ".wav";
                        if (!FileProcess.Exists(filename2))
                        {
                            string message = "�t�@�C��������܂���B : " + filename2;
                            MessageBox.Show(message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            if (!Rtconv_fpga_init(file_path, rtconv_inch, rtconv_outch))
            {
                string message = "FPGA initialize : ";
                MessageBox.Show(message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private const int fpga_fir_tap = 1071;
        public bool Rtconv_fpga_init(String file_path, int rtconv_inch, int rtconv_outch)
        {
            bool check_ok = true;
            int fpga_set_ch = 62;   // FX(1) 62ch-->RX1 FPGA(B)
            if (rtconv_inch != 1) return false;

            string reload_order_file = Application.StartupPath + "\\fir3_reload_order.txt";
            StreamReader sr = new StreamReader(reload_order_file);
            string line;
            ArrayList reload_order = null;
            reload_order = new ArrayList();
            int cnt = 0;

            while ((line = sr.ReadLine()) != null)
            {
                string[] strs = line.Split(' ');
                int index = int.Parse(strs[2]);
                int coef_no = int.Parse(strs[5]);
                if (cnt == index)
                {
                    reload_order.Add(coef_no);
                    cnt++;
                }
            }

            float[] data1 = new float[rtconv_outch * fpga_fir_tap];

            for (int och = 1; och <= rtconv_outch; och++)
            {
                string filename2 = file_path + "001_" + och.ToString("000") + ".wav";
                WavStream ws = new WavStream(this);
                check_ok = ws.WavHeaderCheck(filename2);
                int word_cnt = ws.WordCount();
                if (word_cnt < fpga_fir_tap)
                {
                    Cnsl_msg("Error: Too small word count\n");
                    return false;
                }
                check_ok = ws.WavRead1ch(filename2, fpga_fir_tap);
                if (check_ok)
                {
                    ws.Get_data_3d(ref data1, och, fpga_fir_tap, reload_order);
                }
                else
                {
                    Cnsl_msg("Error: WavRead1ch\n");
                    return false;
                }
            }

            int och1 = rtconv_outch, och2 = 0;
            if (och1 > 48)
            {
                och2 = och1 - 48;
                och1 = 48;
            }
            unsafe
            {
                fixed (float* dd = data1)
                {
                    asio.rtconv_fpga_set(fpga_set_ch, dd, och1, fpga_fir_tap);
                    asio.rtconv_fpga_set(fpga_set_ch + 64, &dd[48 * fpga_fir_tap], och2, fpga_fir_tap);
                }
            }

            if (script_recording) myScripts.Add("RtconvFPGAInit" + ScriptSeparator + rtconv_inch.ToString() + ScriptSeparator + rtconv_outch.ToString() + ScriptSeparator + file_path);
            return true;
        }

        public string Out_filename(int och)
        {
            string num_str_och = "", file_path2;
            if (out_no_digit == 1) num_str_och = och.ToString("0");
            else if (out_no_digit == 2) num_str_och = och.ToString("00");
            else if (out_no_digit == 3) num_str_och = och.ToString("000");
            file_path2 = out_file_path + num_str_och + "." + out_ext;
            return file_path2;
        }

        private int out_no_digit = 0;
        public string out_file_path = "";
        private string out_ext = "";
        public bool Get_outpath_wav001(string fname)
        {
            char[] separator = new char[1] { '.' };
            string filename = fname;
            if(filename == "")
            {
                if (fd.SaveFileDialog("File convolution", "wav"))
                {
                    filename = fd.filename;
                }
                else return false;
            }
            string[] filename1 = filename.Split(separator);
            int ii = filename1.Count();
            out_ext = filename1[ii - 1];
            int digit_point = 0;
            if ((digit_point = filename.IndexOf("001")) >= 0) out_no_digit = 3;
            else if ((digit_point = filename.IndexOf("01")) >= 0) out_no_digit = 2;
            else if ((digit_point = filename.IndexOf("1")) >= 0) out_no_digit = 1;
            else
            {
                out_no_digit = 0;
                return false;
            }
            out_file_path = filename.Remove(digit_point);
            return true;
        }

        private void Convset_go()
        {
            if (convset_fileinput)
            {
                Set_asio_mode(Amt.ASIOmode.asiomode_stream_in_rtconv_out);
            }
            else
            {
                Set_asio_mode(Amt.ASIOmode.asiomode_rtconv_io);
            }

            convset = new ConvSet(this, convset_fileinput);
            convset.ShowDialog();
            convset = null;
        }

        private void MenuItem_rtconv_aiao_Click(object sender, EventArgs e)
        {
            convset_fileinput = false;
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
            //if (greq == null)
            //{
            //    Thread st1 = new Thread(new ThreadStart(greq_go));
            //    st1.Start();
            //    started_thread_list.Add(st1);
            //    while (greq == null || greq.Visible == false) Thread.Sleep(10);
            //    greq.Location = new Point(this.Location.X + 30, this.Location.Y + 94);
            //}
        }

        private void MenuItem_rtconv_fiao_Click(object sender, EventArgs e)
        {
            convset_fileinput = true;
            Thread st = new Thread(new ThreadStart(Convset_go))
            {
                Name = "convset_go"
            };
            st.Start();
            started_thread_list.Add(st);
        }

        private void MenuItem_geq_Click(object sender, EventArgs e)
        {
            Thread st = new Thread(new ThreadStart(Greq_go))
            {
                Name = "greq_go"
            };
            st.Start();
            started_thread_list.Add(st);
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

        private void menuItem_ARS_Click(object sender, EventArgs e)
        {
            if (in_btn_cnt < 2)
            {
                string message = "Input channel (" + in_btn_cnt.ToString() + ") must not less than 2.";
                MessageBox.Show(message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Set_asio_mode(ASIOmode.asiomode_stream_in_rtconv_out);
            NetPlayer netPlayer = new NetPlayer(this);
            netPlayer.ShowDialog();
        }

        private void menuItem_evaltest_Click(object sender, EventArgs e)
        {
            Set_asio_mode(ASIOmode.asiomode_stream_out);
            EvalTest evalTest = new EvalTest(this);
            evalTest.ShowDialog();
        }

        private void menuItem_ueserver_Click(object sender, EventArgs e)
        {
            Set_asio_mode(ASIOmode.asiomode_rtmove);
            UEServerForm ueserver = new UEServerForm(this);
            ueserver.ShowDialog();
        }

        private void menuItem_mover_server_Click(object sender, EventArgs e)
        {
            MoverForm mover = new MoverForm(this);
            mover.ShowDialog();
        }

        private void menuItem_ZYBO_Click(object sender, EventArgs e)
        {
            ZyboForm zybo = new ZyboForm(this);
            zybo.ShowDialog();
        }

        private void menuItem_boscplayer_Click(object sender, EventArgs e)
        {
            BoscPlayer boscPlayer = new BoscPlayer(this);
            boscPlayer.ShowDialog();
        }

        private void menuItem_wavstreaming_Click(object sender, EventArgs e)
        {
            StreamingForm streamingForm = new StreamingForm(this);
            streamingForm.ShowDialog();
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

