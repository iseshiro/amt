using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
//using System.Runtime.InteropServices;
using Asio_io;

namespace amt
{
    /// <summary>
    /// DrawGraph の概要の説明です。
    /// </summary>
    public class DrawGraph : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.PictureBox settingBox;
        //		private System.Windows.Forms.PictureBox picBoxLine;
        private System.Windows.Forms.TabPage tabSpectrum;
        private System.Windows.Forms.PictureBox spectrumBox;
        private System.Windows.Forms.TabPage tabPageWaveForm;
        private System.Windows.Forms.PictureBox waveformBox;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private asio_io myasio;
        private ChannelInfo cinfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox st_time_tb;
        private System.Windows.Forms.TextBox ed_time_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gr_time_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox gr_freq_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ed_freq_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox st_freq_tb;
        private System.Windows.Forms.Label label6;
        public double start_time = 0;
        public double end_time = 20;
        public double edit_start_time = 0;
        public double edit_end_time = 20;
        public double grid_time = 5;
        public double start_freq = 0;
        public double end_freq = 4000;
        public double grid_freq = 500;
        public int disp_range = 100;
        public int grid_range = 10;
        public int fft_point = 8192;
        private double max_time_ms;
        private int max_time_point;
        private double max_freq_hz;
        private int max_freq_point;
        public int level_adjust = 0;
        private System.Windows.Forms.TextBox fft_point_tb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label MaxVal_title;
        private System.Windows.Forms.Label MaxVal_lbl;
        private System.Windows.Forms.Label TotalPow_lbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label FreqPow_lbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabOctPow;
        private System.Windows.Forms.TextBox oct_pow_list_tb;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox amp_log_cb;
        private System.Windows.Forms.Label disp_range_lbl;
        private System.Windows.Forms.TextBox grid_range_tb;
        private System.Windows.Forms.Label grid_range_lbl;
        private System.Windows.Forms.TabPage tabRunSpe;
        private System.Windows.Forms.Button spctrgrm_btn;
        private System.Windows.Forms.TextBox offset_tb;
        private System.Windows.Forms.Label offset_lbl;
        private System.Windows.Forms.TextBox width_tb;
        private System.Windows.Forms.Label width_lbl;
        private System.Windows.Forms.CheckBox log_cb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox magnify_tb;
        private Amt parent;
        private System.Windows.Forms.TextBox level_adjust_tb;
        private System.Windows.Forms.Button load_btn;
        private System.Windows.Forms.Button store_btn;
        public double magnify = 1.0;
        private System.Windows.Forms.Label level_adjust_lbl;
        private Button fft_calc_btn;
        private Label calc_status_lbl;
        private Label label9;
        private TextBox ed_time_point_tb;
        private TextBox st_time_point_tb;
        private Label label10;
        private TextBox ed_freq_point_tb;
        private TextBox st_freq_point_tb;
        private System.Windows.Forms.TextBox disp_range_tb;
        private int start_time_point;
        private int end_time_point;
        private int edit_start_time_point;
        private int edit_end_time_point;
        private int start_freq_point;
        private TextBox gr_freq_point_tb;
        private TextBox gr_time_point_tb;
        private int end_freq_point;
        private int grid_time_point;
        private Button clear_btn;
        private Label label12;
        private TextBox edit_ed_time_point_tb;
        private TextBox edit_st_time_point_tb;
        private TextBox edit_ed_time_tb;
        private Label label14;
        private TextBox edit_st_time_tb;
        private Label label15;
        private Label label16;
        private Button update_value_btn;
        private int grid_freq_point;

        public DrawGraph(Amt Parent, ChannelInfo Cinfo)
        {
            parent = Parent;
            myasio = parent.asio;
            max_time_point = parent.Get_max_time_point();
            max_time_ms = parent.Get_time_ms(max_time_point);
            max_freq_point = fft_point/2;
            max_freq_hz = parent.Get_freq_hz(max_freq_point, fft_point);

            start_time_point = 0;
            end_time_point = max_time_point;
            edit_start_time_point = 0;
            edit_end_time_point = max_time_point/2;
            grid_time_point = end_time_point / 8;
            start_freq_point = 0;
            end_freq_point = max_freq_point;
            grid_freq_point = end_freq_point / 8;

            InitializeComponent();
            cinfo = Cinfo;
            st_time_point_tb.Text = start_time_point.ToString();
            ed_time_point_tb.Text = end_time_point.ToString();
            edit_st_time_point_tb.Text = start_time_point.ToString();
            edit_ed_time_point_tb.Text = end_time_point.ToString();
            gr_time_point_tb.Text = grid_time_point.ToString();
            st_freq_point_tb.Text = start_freq_point.ToString();
            ed_freq_point_tb.Text = end_freq_point.ToString();
            gr_freq_point_tb.Text = grid_freq_point.ToString();
            disp_range_tb.Text = disp_range.ToString();
            grid_range_tb.Text = grid_range.ToString();
            fft_point_tb.Text = fft_point.ToString();
            magnify_tb.Text = magnify.ToString();
            update_value();

        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
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

        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.settingBox = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWaveForm = new System.Windows.Forms.TabPage();
            this.waveformBox = new System.Windows.Forms.PictureBox();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.update_value_btn = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.MaxVal_lbl = new System.Windows.Forms.Label();
            this.MaxVal_title = new System.Windows.Forms.Label();
            this.clear_btn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.edit_ed_time_point_tb = new System.Windows.Forms.TextBox();
            this.edit_st_time_point_tb = new System.Windows.Forms.TextBox();
            this.edit_ed_time_tb = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.edit_st_time_tb = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.gr_freq_point_tb = new System.Windows.Forms.TextBox();
            this.gr_time_point_tb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ed_freq_point_tb = new System.Windows.Forms.TextBox();
            this.st_freq_point_tb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ed_time_point_tb = new System.Windows.Forms.TextBox();
            this.st_time_point_tb = new System.Windows.Forms.TextBox();
            this.calc_status_lbl = new System.Windows.Forms.Label();
            this.fft_calc_btn = new System.Windows.Forms.Button();
            this.store_btn = new System.Windows.Forms.Button();
            this.load_btn = new System.Windows.Forms.Button();
            this.level_adjust_tb = new System.Windows.Forms.TextBox();
            this.level_adjust_lbl = new System.Windows.Forms.Label();
            this.magnify_tb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grid_range_tb = new System.Windows.Forms.TextBox();
            this.grid_range_lbl = new System.Windows.Forms.Label();
            this.disp_range_tb = new System.Windows.Forms.TextBox();
            this.disp_range_lbl = new System.Windows.Forms.Label();
            this.amp_log_cb = new System.Windows.Forms.CheckBox();
            this.FreqPow_lbl = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TotalPow_lbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.fft_point_tb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gr_freq_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ed_freq_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.st_freq_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gr_time_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ed_time_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.st_time_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabOctPow = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.oct_pow_list_tb = new System.Windows.Forms.TextBox();
            this.tabRunSpe = new System.Windows.Forms.TabPage();
            this.log_cb = new System.Windows.Forms.CheckBox();
            this.offset_tb = new System.Windows.Forms.TextBox();
            this.offset_lbl = new System.Windows.Forms.Label();
            this.width_tb = new System.Windows.Forms.TextBox();
            this.width_lbl = new System.Windows.Forms.Label();
            this.spctrgrm_btn = new System.Windows.Forms.Button();
            this.tabSpectrum = new System.Windows.Forms.TabPage();
            this.spectrumBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.settingBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageWaveForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveformBox)).BeginInit();
            this.tabSetting.SuspendLayout();
            this.tabOctPow.SuspendLayout();
            this.tabRunSpe.SuspendLayout();
            this.tabSpectrum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spectrumBox)).BeginInit();
            this.SuspendLayout();
            // 
            // settingBox
            // 
            this.settingBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingBox.Location = new System.Drawing.Point(0, 0);
            this.settingBox.Name = "settingBox";
            this.settingBox.Size = new System.Drawing.Size(1008, 528);
            this.settingBox.TabIndex = 1;
            this.settingBox.TabStop = false;
            this.settingBox.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingBox_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageWaveForm);
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabOctPow);
            this.tabControl1.Controls.Add(this.tabRunSpe);
            this.tabControl1.Controls.Add(this.tabSpectrum);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 383);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageWaveForm
            // 
            this.tabPageWaveForm.Controls.Add(this.waveformBox);
            this.tabPageWaveForm.Location = new System.Drawing.Point(4, 28);
            this.tabPageWaveForm.Name = "tabPageWaveForm";
            this.tabPageWaveForm.Size = new System.Drawing.Size(627, 351);
            this.tabPageWaveForm.TabIndex = 3;
            this.tabPageWaveForm.Text = "Waveform";
            // 
            // waveformBox
            // 
            this.waveformBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveformBox.Location = new System.Drawing.Point(0, 0);
            this.waveformBox.Name = "waveformBox";
            this.waveformBox.Size = new System.Drawing.Size(627, 351);
            this.waveformBox.TabIndex = 0;
            this.waveformBox.TabStop = false;
            this.waveformBox.Paint += new System.Windows.Forms.PaintEventHandler(this.WaveformBox_Paint);
            this.waveformBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WaveformBox_MouseDown);
            // 
            // tabSetting
            // 
            this.tabSetting.AutoScroll = true;
            this.tabSetting.Controls.Add(this.update_value_btn);
            this.tabSetting.Controls.Add(this.label16);
            this.tabSetting.Controls.Add(this.MaxVal_lbl);
            this.tabSetting.Controls.Add(this.MaxVal_title);
            this.tabSetting.Controls.Add(this.clear_btn);
            this.tabSetting.Controls.Add(this.label12);
            this.tabSetting.Controls.Add(this.edit_ed_time_point_tb);
            this.tabSetting.Controls.Add(this.edit_st_time_point_tb);
            this.tabSetting.Controls.Add(this.edit_ed_time_tb);
            this.tabSetting.Controls.Add(this.label14);
            this.tabSetting.Controls.Add(this.edit_st_time_tb);
            this.tabSetting.Controls.Add(this.label15);
            this.tabSetting.Controls.Add(this.gr_freq_point_tb);
            this.tabSetting.Controls.Add(this.gr_time_point_tb);
            this.tabSetting.Controls.Add(this.label10);
            this.tabSetting.Controls.Add(this.ed_freq_point_tb);
            this.tabSetting.Controls.Add(this.st_freq_point_tb);
            this.tabSetting.Controls.Add(this.label9);
            this.tabSetting.Controls.Add(this.ed_time_point_tb);
            this.tabSetting.Controls.Add(this.st_time_point_tb);
            this.tabSetting.Controls.Add(this.calc_status_lbl);
            this.tabSetting.Controls.Add(this.fft_calc_btn);
            this.tabSetting.Controls.Add(this.store_btn);
            this.tabSetting.Controls.Add(this.load_btn);
            this.tabSetting.Controls.Add(this.level_adjust_tb);
            this.tabSetting.Controls.Add(this.level_adjust_lbl);
            this.tabSetting.Controls.Add(this.magnify_tb);
            this.tabSetting.Controls.Add(this.label8);
            this.tabSetting.Controls.Add(this.grid_range_tb);
            this.tabSetting.Controls.Add(this.grid_range_lbl);
            this.tabSetting.Controls.Add(this.disp_range_tb);
            this.tabSetting.Controls.Add(this.disp_range_lbl);
            this.tabSetting.Controls.Add(this.amp_log_cb);
            this.tabSetting.Controls.Add(this.FreqPow_lbl);
            this.tabSetting.Controls.Add(this.label13);
            this.tabSetting.Controls.Add(this.TotalPow_lbl);
            this.tabSetting.Controls.Add(this.label11);
            this.tabSetting.Controls.Add(this.fft_point_tb);
            this.tabSetting.Controls.Add(this.label7);
            this.tabSetting.Controls.Add(this.gr_freq_tb);
            this.tabSetting.Controls.Add(this.label4);
            this.tabSetting.Controls.Add(this.ed_freq_tb);
            this.tabSetting.Controls.Add(this.label5);
            this.tabSetting.Controls.Add(this.st_freq_tb);
            this.tabSetting.Controls.Add(this.label6);
            this.tabSetting.Controls.Add(this.gr_time_tb);
            this.tabSetting.Controls.Add(this.label3);
            this.tabSetting.Controls.Add(this.ed_time_tb);
            this.tabSetting.Controls.Add(this.label2);
            this.tabSetting.Controls.Add(this.st_time_tb);
            this.tabSetting.Controls.Add(this.label1);
            this.tabSetting.Controls.Add(this.settingBox);
            this.tabSetting.Location = new System.Drawing.Point(4, 28);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Size = new System.Drawing.Size(1008, 542);
            this.tabSetting.TabIndex = 1;
            this.tabSetting.Text = "Setting";
            // 
            // update_value_btn
            // 
            this.update_value_btn.Location = new System.Drawing.Point(869, 72);
            this.update_value_btn.Name = "update_value_btn";
            this.update_value_btn.Size = new System.Drawing.Size(101, 34);
            this.update_value_btn.TabIndex = 56;
            this.update_value_btn.Text = "Update";
            this.update_value_btn.UseVisualStyleBackColor = true;
            this.update_value_btn.Click += new System.EventHandler(this.update_value_btn_Click);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label16.Location = new System.Drawing.Point(37, 158);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(141, 36);
            this.label16.TabIndex = 55;
            this.label16.Text = "WAVE EDIT";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MaxVal_lbl
            // 
            this.MaxVal_lbl.Location = new System.Drawing.Point(170, 285);
            this.MaxVal_lbl.Name = "MaxVal_lbl";
            this.MaxVal_lbl.Size = new System.Drawing.Size(243, 36);
            this.MaxVal_lbl.TabIndex = 17;
            this.MaxVal_lbl.Text = "0";
            this.MaxVal_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MaxVal_title
            // 
            this.MaxVal_title.Location = new System.Drawing.Point(26, 285);
            this.MaxVal_title.Name = "MaxVal_title";
            this.MaxVal_title.Size = new System.Drawing.Size(140, 36);
            this.MaxVal_title.TabIndex = 16;
            this.MaxVal_title.Text = "Max Value = ";
            this.MaxVal_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clear_btn
            // 
            this.clear_btn.Location = new System.Drawing.Point(438, 194);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(66, 34);
            this.clear_btn.TabIndex = 54;
            this.clear_btn.Text = "Clear";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(307, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 21);
            this.label12.TabIndex = 53;
            this.label12.Text = "Point";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edit_ed_time_point_tb
            // 
            this.edit_ed_time_point_tb.Location = new System.Drawing.Point(310, 230);
            this.edit_ed_time_point_tb.Name = "edit_ed_time_point_tb";
            this.edit_ed_time_point_tb.Size = new System.Drawing.Size(116, 25);
            this.edit_ed_time_point_tb.TabIndex = 52;
            this.edit_ed_time_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.edit_ed_time_point_tb.TextChanged += new System.EventHandler(this.Edit_ed_time_point_tb_TextChanged);
            // 
            // edit_st_time_point_tb
            // 
            this.edit_st_time_point_tb.Location = new System.Drawing.Point(310, 192);
            this.edit_st_time_point_tb.Name = "edit_st_time_point_tb";
            this.edit_st_time_point_tb.Size = new System.Drawing.Size(116, 25);
            this.edit_st_time_point_tb.TabIndex = 51;
            this.edit_st_time_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.edit_st_time_point_tb.TextChanged += new System.EventHandler(this.Edit_st_time_point_tb_TextChanged);
            // 
            // edit_ed_time_tb
            // 
            this.edit_ed_time_tb.Location = new System.Drawing.Point(179, 228);
            this.edit_ed_time_tb.Name = "edit_ed_time_tb";
            this.edit_ed_time_tb.Size = new System.Drawing.Size(115, 25);
            this.edit_ed_time_tb.TabIndex = 50;
            this.edit_ed_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.edit_ed_time_tb.TextChanged += new System.EventHandler(this.Edit_ed_time_tb_TextChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(38, 228);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 36);
            this.label14.TabIndex = 49;
            this.label14.Text = "End Time [ms]";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edit_st_time_tb
            // 
            this.edit_st_time_tb.Location = new System.Drawing.Point(179, 192);
            this.edit_st_time_tb.Name = "edit_st_time_tb";
            this.edit_st_time_tb.Size = new System.Drawing.Size(115, 25);
            this.edit_st_time_tb.TabIndex = 48;
            this.edit_st_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.edit_st_time_tb.TextChanged += new System.EventHandler(this.Edit_st_time_tb_TextChanged);
            // 
            // label15
            // 
            this.label15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Location = new System.Drawing.Point(26, 192);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 36);
            this.label15.TabIndex = 47;
            this.label15.Text = "Start Time [ms]";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gr_freq_point_tb
            // 
            this.gr_freq_point_tb.Location = new System.Drawing.Point(725, 108);
            this.gr_freq_point_tb.Name = "gr_freq_point_tb";
            this.gr_freq_point_tb.Size = new System.Drawing.Size(115, 25);
            this.gr_freq_point_tb.TabIndex = 45;
            this.gr_freq_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gr_freq_point_tb.TextChanged += new System.EventHandler(this.Gr_freq_point_tb_TextChanged);
            // 
            // gr_time_point_tb
            // 
            this.gr_time_point_tb.Location = new System.Drawing.Point(310, 108);
            this.gr_time_point_tb.Name = "gr_time_point_tb";
            this.gr_time_point_tb.Size = new System.Drawing.Size(116, 25);
            this.gr_time_point_tb.TabIndex = 44;
            this.gr_time_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gr_time_point_tb.TextChanged += new System.EventHandler(this.Gr_time_point_tb_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(722, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 21);
            this.label10.TabIndex = 43;
            this.label10.Text = "Point";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ed_freq_point_tb
            // 
            this.ed_freq_point_tb.Location = new System.Drawing.Point(725, 74);
            this.ed_freq_point_tb.Name = "ed_freq_point_tb";
            this.ed_freq_point_tb.Size = new System.Drawing.Size(115, 25);
            this.ed_freq_point_tb.TabIndex = 42;
            this.ed_freq_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ed_freq_point_tb.TextChanged += new System.EventHandler(this.Ed_freq_point_tb_TextChanged);
            // 
            // st_freq_point_tb
            // 
            this.st_freq_point_tb.Location = new System.Drawing.Point(725, 36);
            this.st_freq_point_tb.Name = "st_freq_point_tb";
            this.st_freq_point_tb.Size = new System.Drawing.Size(115, 25);
            this.st_freq_point_tb.TabIndex = 41;
            this.st_freq_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.st_freq_point_tb.TextChanged += new System.EventHandler(this.St_freq_point_tb_TextChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(307, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 21);
            this.label9.TabIndex = 40;
            this.label9.Text = "Point";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ed_time_point_tb
            // 
            this.ed_time_point_tb.Location = new System.Drawing.Point(310, 74);
            this.ed_time_point_tb.Name = "ed_time_point_tb";
            this.ed_time_point_tb.Size = new System.Drawing.Size(116, 25);
            this.ed_time_point_tb.TabIndex = 39;
            this.ed_time_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ed_time_point_tb.TextChanged += new System.EventHandler(this.Ed_time_point_tb_TextChanged);
            // 
            // st_time_point_tb
            // 
            this.st_time_point_tb.Location = new System.Drawing.Point(310, 36);
            this.st_time_point_tb.Name = "st_time_point_tb";
            this.st_time_point_tb.Size = new System.Drawing.Size(116, 25);
            this.st_time_point_tb.TabIndex = 38;
            this.st_time_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.st_time_point_tb.TextChanged += new System.EventHandler(this.St_time_point_tb_TextChanged);
            // 
            // calc_status_lbl
            // 
            this.calc_status_lbl.AutoSize = true;
            this.calc_status_lbl.Location = new System.Drawing.Point(797, 321);
            this.calc_status_lbl.Name = "calc_status_lbl";
            this.calc_status_lbl.Size = new System.Drawing.Size(81, 18);
            this.calc_status_lbl.TabIndex = 37;
            this.calc_status_lbl.Text = "Push this.";
            this.calc_status_lbl.Visible = false;
            // 
            // fft_calc_btn
            // 
            this.fft_calc_btn.Enabled = false;
            this.fft_calc_btn.Location = new System.Drawing.Point(800, 279);
            this.fft_calc_btn.Name = "fft_calc_btn";
            this.fft_calc_btn.Size = new System.Drawing.Size(66, 35);
            this.fft_calc_btn.TabIndex = 36;
            this.fft_calc_btn.Text = "Calc";
            this.fft_calc_btn.UseVisualStyleBackColor = true;
            this.fft_calc_btn.Click += new System.EventHandler(this.Fft_calc_btn_Click);
            // 
            // store_btn
            // 
            this.store_btn.Location = new System.Drawing.Point(90, 456);
            this.store_btn.Name = "store_btn";
            this.store_btn.Size = new System.Drawing.Size(76, 34);
            this.store_btn.TabIndex = 35;
            this.store_btn.Text = "Store";
            this.store_btn.Click += new System.EventHandler(this.Store_btn_Click);
            // 
            // load_btn
            // 
            this.load_btn.Location = new System.Drawing.Point(192, 456);
            this.load_btn.Name = "load_btn";
            this.load_btn.Size = new System.Drawing.Size(77, 34);
            this.load_btn.TabIndex = 34;
            this.load_btn.Text = "Load";
            this.load_btn.Click += new System.EventHandler(this.Load_btn_Click);
            // 
            // level_adjust_tb
            // 
            this.level_adjust_tb.Location = new System.Drawing.Point(589, 459);
            this.level_adjust_tb.Name = "level_adjust_tb";
            this.level_adjust_tb.Size = new System.Drawing.Size(160, 25);
            this.level_adjust_tb.TabIndex = 33;
            this.level_adjust_tb.Text = "0";
            this.level_adjust_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.level_adjust_tb.Visible = false;
            this.level_adjust_tb.TextChanged += new System.EventHandler(this.Level_adjust_tb_TextChanged);
            // 
            // level_adjust_lbl
            // 
            this.level_adjust_lbl.Location = new System.Drawing.Point(410, 454);
            this.level_adjust_lbl.Name = "level_adjust_lbl";
            this.level_adjust_lbl.Size = new System.Drawing.Size(166, 36);
            this.level_adjust_lbl.TabIndex = 32;
            this.level_adjust_lbl.Text = "Level Adjust [dB]";
            this.level_adjust_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.level_adjust_lbl.Visible = false;
            // 
            // magnify_tb
            // 
            this.magnify_tb.Location = new System.Drawing.Point(173, 398);
            this.magnify_tb.Name = "magnify_tb";
            this.magnify_tb.Size = new System.Drawing.Size(160, 25);
            this.magnify_tb.TabIndex = 31;
            this.magnify_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.magnify_tb.TextChanged += new System.EventHandler(this.Magnify_tb_TextChanged);
            // 
            // label8
            // 
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(22, 393);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 36);
            this.label8.TabIndex = 30;
            this.label8.Text = "Magnify";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grid_range_tb
            // 
            this.grid_range_tb.Location = new System.Drawing.Point(589, 418);
            this.grid_range_tb.Name = "grid_range_tb";
            this.grid_range_tb.Size = new System.Drawing.Size(160, 25);
            this.grid_range_tb.TabIndex = 29;
            this.grid_range_tb.Text = "10";
            this.grid_range_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.grid_range_tb.Visible = false;
            this.grid_range_tb.TextChanged += new System.EventHandler(this.Grid_range_tb_TextChanged);
            // 
            // grid_range_lbl
            // 
            this.grid_range_lbl.Location = new System.Drawing.Point(435, 414);
            this.grid_range_lbl.Name = "grid_range_lbl";
            this.grid_range_lbl.Size = new System.Drawing.Size(141, 36);
            this.grid_range_lbl.TabIndex = 28;
            this.grid_range_lbl.Text = "Grid Range [dB]";
            this.grid_range_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.grid_range_lbl.Visible = false;
            // 
            // disp_range_tb
            // 
            this.disp_range_tb.Location = new System.Drawing.Point(589, 376);
            this.disp_range_tb.Name = "disp_range_tb";
            this.disp_range_tb.Size = new System.Drawing.Size(160, 25);
            this.disp_range_tb.TabIndex = 27;
            this.disp_range_tb.Text = "100";
            this.disp_range_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.disp_range_tb.Visible = false;
            this.disp_range_tb.TextChanged += new System.EventHandler(this.Disp_range_tb_TextChanged);
            // 
            // disp_range_lbl
            // 
            this.disp_range_lbl.Location = new System.Drawing.Point(381, 372);
            this.disp_range_lbl.Name = "disp_range_lbl";
            this.disp_range_lbl.Size = new System.Drawing.Size(195, 36);
            this.disp_range_lbl.TabIndex = 26;
            this.disp_range_lbl.Text = "Display Range [dB]";
            this.disp_range_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.disp_range_lbl.Visible = false;
            // 
            // amp_log_cb
            // 
            this.amp_log_cb.Location = new System.Drawing.Point(589, 328);
            this.amp_log_cb.Name = "amp_log_cb";
            this.amp_log_cb.Size = new System.Drawing.Size(179, 24);
            this.amp_log_cb.TabIndex = 25;
            this.amp_log_cb.Text = "Amp Log Scale";
            this.amp_log_cb.CheckedChanged += new System.EventHandler(this.Amp_log_cb_CheckedChanged);
            // 
            // FreqPow_lbl
            // 
            this.FreqPow_lbl.Location = new System.Drawing.Point(170, 357);
            this.FreqPow_lbl.Name = "FreqPow_lbl";
            this.FreqPow_lbl.Size = new System.Drawing.Size(153, 36);
            this.FreqPow_lbl.TabIndex = 23;
            this.FreqPow_lbl.Text = "0";
            this.FreqPow_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(26, 357);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 36);
            this.label13.TabIndex = 22;
            this.label13.Text = "Freq Power = ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalPow_lbl
            // 
            this.TotalPow_lbl.Location = new System.Drawing.Point(170, 321);
            this.TotalPow_lbl.Name = "TotalPow_lbl";
            this.TotalPow_lbl.Size = new System.Drawing.Size(153, 36);
            this.TotalPow_lbl.TabIndex = 21;
            this.TotalPow_lbl.Text = "0";
            this.TotalPow_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(26, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 36);
            this.label11.TabIndex = 20;
            this.label11.Text = "Total Power = ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fft_point_tb
            // 
            this.fft_point_tb.Location = new System.Drawing.Point(589, 285);
            this.fft_point_tb.Name = "fft_point_tb";
            this.fft_point_tb.Size = new System.Drawing.Size(160, 25);
            this.fft_point_tb.TabIndex = 15;
            this.fft_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fft_point_tb.TextChanged += new System.EventHandler(this.Fft_point_tb_TextChanged);
            this.fft_point_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(422, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 36);
            this.label7.TabIndex = 14;
            this.label7.Text = "FFT point";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gr_freq_tb
            // 
            this.gr_freq_tb.Location = new System.Drawing.Point(589, 108);
            this.gr_freq_tb.Name = "gr_freq_tb";
            this.gr_freq_tb.Size = new System.Drawing.Size(120, 25);
            this.gr_freq_tb.TabIndex = 13;
            this.gr_freq_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gr_freq_tb.TextChanged += new System.EventHandler(this.Gr_freq_tb_TextChanged);
            this.gr_freq_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(435, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 36);
            this.label4.TabIndex = 12;
            this.label4.Text = "Grid Freq. [Hz]";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ed_freq_tb
            // 
            this.ed_freq_tb.Location = new System.Drawing.Point(589, 72);
            this.ed_freq_tb.Name = "ed_freq_tb";
            this.ed_freq_tb.Size = new System.Drawing.Size(120, 25);
            this.ed_freq_tb.TabIndex = 11;
            this.ed_freq_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ed_freq_tb.TextChanged += new System.EventHandler(this.Ed_freq_tb_TextChanged);
            this.ed_freq_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(448, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 36);
            this.label5.TabIndex = 10;
            this.label5.Text = "End Freq. [Hz]";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // st_freq_tb
            // 
            this.st_freq_tb.Location = new System.Drawing.Point(589, 36);
            this.st_freq_tb.Name = "st_freq_tb";
            this.st_freq_tb.Size = new System.Drawing.Size(120, 25);
            this.st_freq_tb.TabIndex = 9;
            this.st_freq_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.st_freq_tb.TextChanged += new System.EventHandler(this.St_freq_tb_TextChanged);
            this.st_freq_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label6
            // 
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(435, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 36);
            this.label6.TabIndex = 8;
            this.label6.Text = "Start Freq. [Hz]";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gr_time_tb
            // 
            this.gr_time_tb.Location = new System.Drawing.Point(179, 108);
            this.gr_time_tb.Name = "gr_time_tb";
            this.gr_time_tb.Size = new System.Drawing.Size(115, 25);
            this.gr_time_tb.TabIndex = 7;
            this.gr_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gr_time_tb.TextChanged += new System.EventHandler(this.Gr_time_tb_TextChanged);
            this.gr_time_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(26, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 36);
            this.label3.TabIndex = 6;
            this.label3.Text = "Grid Time [ms]";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ed_time_tb
            // 
            this.ed_time_tb.Location = new System.Drawing.Point(179, 72);
            this.ed_time_tb.Name = "ed_time_tb";
            this.ed_time_tb.Size = new System.Drawing.Size(115, 25);
            this.ed_time_tb.TabIndex = 5;
            this.ed_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ed_time_tb.TextChanged += new System.EventHandler(this.Ed_time_tb_TextChanged);
            this.ed_time_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(38, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Time [ms]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // st_time_tb
            // 
            this.st_time_tb.Location = new System.Drawing.Point(179, 36);
            this.st_time_tb.Name = "st_time_tb";
            this.st_time_tb.Size = new System.Drawing.Size(115, 25);
            this.st_time_tb.TabIndex = 3;
            this.st_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.st_time_tb.TextChanged += new System.EventHandler(this.St_time_tb_TextChanged);
            this.st_time_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(26, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Time [ms]";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabOctPow
            // 
            this.tabOctPow.Controls.Add(this.button1);
            this.tabOctPow.Controls.Add(this.oct_pow_list_tb);
            this.tabOctPow.Location = new System.Drawing.Point(4, 28);
            this.tabOctPow.Name = "tabOctPow";
            this.tabOctPow.Size = new System.Drawing.Size(1008, 542);
            this.tabOctPow.TabIndex = 4;
            this.tabOctPow.Text = "Oct. Power";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Update";
            this.button1.Click += new System.EventHandler(this.Update_btn);
            // 
            // oct_pow_list_tb
            // 
            this.oct_pow_list_tb.Location = new System.Drawing.Point(26, 24);
            this.oct_pow_list_tb.Multiline = true;
            this.oct_pow_list_tb.Name = "oct_pow_list_tb";
            this.oct_pow_list_tb.Size = new System.Drawing.Size(870, 432);
            this.oct_pow_list_tb.TabIndex = 0;
            // 
            // tabRunSpe
            // 
            this.tabRunSpe.Controls.Add(this.log_cb);
            this.tabRunSpe.Controls.Add(this.offset_tb);
            this.tabRunSpe.Controls.Add(this.offset_lbl);
            this.tabRunSpe.Controls.Add(this.width_tb);
            this.tabRunSpe.Controls.Add(this.width_lbl);
            this.tabRunSpe.Controls.Add(this.spctrgrm_btn);
            this.tabRunSpe.Location = new System.Drawing.Point(4, 28);
            this.tabRunSpe.Name = "tabRunSpe";
            this.tabRunSpe.Size = new System.Drawing.Size(1008, 542);
            this.tabRunSpe.TabIndex = 5;
            this.tabRunSpe.Text = "Spectrogram";
            // 
            // log_cb
            // 
            this.log_cb.Checked = true;
            this.log_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.log_cb.Location = new System.Drawing.Point(333, 144);
            this.log_cb.Name = "log_cb";
            this.log_cb.Size = new System.Drawing.Size(179, 36);
            this.log_cb.TabIndex = 10;
            this.log_cb.Text = "Level calculation";
            // 
            // offset_tb
            // 
            this.offset_tb.Location = new System.Drawing.Point(358, 96);
            this.offset_tb.Name = "offset_tb";
            this.offset_tb.Size = new System.Drawing.Size(160, 25);
            this.offset_tb.TabIndex = 9;
            this.offset_tb.Text = "50";
            this.offset_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // offset_lbl
            // 
            this.offset_lbl.Location = new System.Drawing.Point(218, 96);
            this.offset_lbl.Name = "offset_lbl";
            this.offset_lbl.Size = new System.Drawing.Size(128, 36);
            this.offset_lbl.TabIndex = 8;
            this.offset_lbl.Text = "Offset [ms]";
            this.offset_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // width_tb
            // 
            this.width_tb.Location = new System.Drawing.Point(358, 60);
            this.width_tb.Name = "width_tb";
            this.width_tb.Size = new System.Drawing.Size(160, 25);
            this.width_tb.TabIndex = 7;
            this.width_tb.Text = "100";
            this.width_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // width_lbl
            // 
            this.width_lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.width_lbl.Location = new System.Drawing.Point(205, 60);
            this.width_lbl.Name = "width_lbl";
            this.width_lbl.Size = new System.Drawing.Size(141, 36);
            this.width_lbl.TabIndex = 6;
            this.width_lbl.Text = "Width [ms]";
            this.width_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spctrgrm_btn
            // 
            this.spctrgrm_btn.Location = new System.Drawing.Point(358, 228);
            this.spctrgrm_btn.Name = "spctrgrm_btn";
            this.spctrgrm_btn.Size = new System.Drawing.Size(154, 36);
            this.spctrgrm_btn.TabIndex = 0;
            this.spctrgrm_btn.Text = "CALC";
            this.spctrgrm_btn.Click += new System.EventHandler(this.Spctrgrm_btn_Click);
            // 
            // tabSpectrum
            // 
            this.tabSpectrum.Controls.Add(this.spectrumBox);
            this.tabSpectrum.Location = new System.Drawing.Point(4, 28);
            this.tabSpectrum.Name = "tabSpectrum";
            this.tabSpectrum.Size = new System.Drawing.Size(1008, 542);
            this.tabSpectrum.TabIndex = 2;
            this.tabSpectrum.Text = "Spectrum";
            // 
            // spectrumBox
            // 
            this.spectrumBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrumBox.Location = new System.Drawing.Point(0, 0);
            this.spectrumBox.Name = "spectrumBox";
            this.spectrumBox.Size = new System.Drawing.Size(1008, 542);
            this.spectrumBox.TabIndex = 0;
            this.spectrumBox.TabStop = false;
            this.spectrumBox.Paint += new System.Windows.Forms.PaintEventHandler(this.SpectrumBox_Paint);
            // 
            // DrawGraph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(635, 383);
            this.Controls.Add(this.tabControl1);
            this.Name = "DrawGraph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drawing Graph";
            ((System.ComponentModel.ISupportInitialize)(this.settingBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageWaveForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.waveformBox)).EndInit();
            this.tabSetting.ResumeLayout(false);
            this.tabSetting.PerformLayout();
            this.tabOctPow.ResumeLayout(false);
            this.tabOctPow.PerformLayout();
            this.tabRunSpe.ResumeLayout(false);
            this.tabRunSpe.PerformLayout();
            this.tabSpectrum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spectrumBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void WaveformBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 20;
            int yohaku_right = 10;
            int yohaku_top = 10;
            int yohaku_bottom = 20;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;


            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            Point[] points = { new Point(yohaku_left, yohaku_top + height / 2), new Point(yohaku_left + width, yohaku_top + height / 2) };
            g.DrawLines(pen, points);


            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center
            };
            for (double grid = start_time + grid_time; grid < end_time; grid += grid_time)
            {
                pen.DashStyle = DashStyle.Dot;
                x1 = (int)((double)width * (grid - start_time) / (end_time - start_time));
                Point[] pnts = { new Point(x1 + yohaku_left, yohaku_top), new Point(x1 + yohaku_left, yohaku_top + height) };
                g.DrawLines(pen, pnts);
                g.DrawString(grid.ToString("0.0"), this.Font, new SolidBrush(Color.Black), x1 + yohaku_left, yohaku_top + height + 2, format);
            }
            format.Alignment = StringAlignment.Near;
            g.DrawString(start_time.ToString("0.0"), this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height + 2, format);
            format.Alignment = StringAlignment.Far;
            g.DrawString(end_time.ToString("0.0"), this.Font, new SolidBrush(Color.Black), yohaku_left + width, yohaku_top + height + 2, format);

            g.DrawString("0", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height / 2 - this.Font.Height / 2, format);
            g.DrawString("1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top, format);
            g.DrawString("-1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height - this.Font.Height, format);
            g.DrawString("[ms]", this.Font, new SolidBrush(Color.Black), ss.Size.Width - 40, ss.Size.Height - this.Font.Height, format);

            pen = new Pen(Color.Blue, 1.0F)
            {
                DashStyle = DashStyle.Solid
            };

            //int start_pnt = parent.get_time_point(start_time);
            //int end_pnt = parent.get_time_point(end_time);

            if (start_time_point < 0) start_time_point = 0;
            if (end_time_point > myasio.GetDataLength()) end_time_point = myasio.GetDataLength();

            double h_scale = (double)(height / 2) * magnify;
            double w_scale = (double)width / (double)(end_time_point - start_time_point);
            if (w_scale > 0.1)
            {
                x1 = yohaku_left;
                y1 = yohaku_top + height / 2 - (int)(myasio.data(start_time_point, (int)cinfo.type, cinfo.btn_no) * h_scale); ;

                for (int i = start_time_point + 1; i < end_time_point; i++)
                {
                    x2 = (int)(w_scale * (double)(i - start_time_point)) + yohaku_left;
                    y2 = yohaku_top + height / 2 - (int)(myasio.data(i, (int)cinfo.type, cinfo.btn_no) * h_scale);

                    points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                    g.DrawLines(pen, points);
                    x1 = x2;
                    y1 = y2;
                }
            }
            else
            {
                int st_pnt, ed_pnt;
                double w_scale_1 = (double)(end_time_point - start_time_point) / (double)width, min, max;
                for (int i = 0; i < width; i++)
                {
                    x1 = yohaku_left + i;
                    st_pnt = (int)((double)i * w_scale_1) + start_time_point;
                    ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_time_point;
                    unsafe
                    {
                        myasio.getminmax(st_pnt, ed_pnt, (int)cinfo.type, cinfo.btn_no, &min, &max);
                    }

                    y1 = yohaku_top + height / 2 - (int)(min * h_scale);
                    y2 = yohaku_top + height / 2 - (int)(max * h_scale);
                    points = new Point[] { new Point(x1, y1), new Point(x1, y2) };
                    g.DrawLines(pen, points);
                }
            }

        }

        private void SpectrumBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
            int yohaku_left = 25;
            int yohaku_right = 10;
            int yohaku_top = 20;
            int yohaku_bottom = 20;
            int width = ss.Size.Width - yohaku_left - yohaku_right;
            int height = ss.Size.Height - yohaku_top - yohaku_bottom;
            Pen pen = new Pen(Color.Black, 2.0F);
            int x1, x2, y1, y2;


            g.DrawRectangle(pen, yohaku_left, yohaku_top, width, height);

            Point[] points;// = {new Point(yohaku_left, yohaku_top+height/2), new Point(yohaku_left+width, yohaku_top+height/2)};
            //			g.DrawLines(pen, points);

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center
            };
            for (double grid = start_freq + grid_freq; grid < end_freq; grid += grid_freq)
            {
                pen.DashStyle = DashStyle.Dot;
                x1 = (int)((double)width * grid / (end_freq - start_freq));
                Point[] pnts = { new Point(x1 + yohaku_left, yohaku_top), new Point(x1 + yohaku_left, yohaku_top + height) };
                g.DrawLines(pen, pnts);
                g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), x1 + yohaku_left, yohaku_top + height + 2, format);
            }
            format.Alignment = StringAlignment.Near;
            g.DrawString(start_freq.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height + 2, format);
            format.Alignment = StringAlignment.Far;
            //			if(DrawEndFreq_cb.Checked)
            //			{
            //				g.DrawString(end_freq.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left+width, yohaku_top+height+2, format);
            //			}

            if (!is_fft_calc)
            {
                //int start_pp = parent.get_time_point(start_time);
                //int end_pp = parent.get_time_point(end_time);
                myasio.makeFFT(fft_point, start_time_point, end_time_point, (int)cinfo.type, cinfo.btn_no);
            }

            //int start_pnt = parent.get_freq_point(start_freq, fft_point);
            //int end_pnt = parent.get_freq_point(end_freq, fft_point);
            if (start_freq_point < 0) start_freq_point = 0;
            if (end_freq_point >= fft_point / 2) end_freq_point = fft_point / 2 - 1;

            double famp_max = Math.Sqrt(myasio.famp(start_freq_point));
            double ff;
            for (int i = start_freq_point + 1; i < end_freq_point; i++)
            {
                ff = Math.Sqrt(myasio.famp(i));
                if (ff > famp_max) famp_max = ff;
            }
            if (famp_max == 0.0)
            {
                string msg = "Freq data seems to be zero";
                g.DrawString(msg, this.Font, new SolidBrush(Color.Black), yohaku_left + ss.Size.Width / 2, yohaku_top + ss.Size.Height / 2, format);
                return;
            }
            if (amp_log_cb.Checked)
            {
                double p0 = Math.Sqrt(famp_max);
                string fampt = "p0=" + p0.ToString("E3");
                g.DrawString(fampt, this.Font, new SolidBrush(Color.Black), yohaku_left + width / 4, yohaku_top - 15, format);
                g.DrawString("[dB]", this.Font, new SolidBrush(Color.Black), yohaku_left, 2, format);
                g.DrawString("[Hz]", this.Font, new SolidBrush(Color.Black), ss.Size.Width - 40, ss.Size.Height - this.Font.Height, format);

                for (int grid = 0; grid <= disp_range; grid += grid_range)
                {
                    pen.DashStyle = DashStyle.Dot;
                    x1 = yohaku_left;
                    x2 = yohaku_left + width;
                    y1 = yohaku_top + height * grid / disp_range;
                    Point[] pnts = { new Point(x1, y1), new Point(x2, y1) };
                    g.DrawLines(pen, pnts);
                    g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left, y1 - this.Font.Height / 2, format);
                }

                pen.DashStyle = DashStyle.Solid;
                pen = new Pen(Color.Blue, 1.0F);

                double h_scale = (double)height / (double)disp_range;
                double famp_max_1 = 1.0 / famp_max;
                double w_scale = (double)width / (double)(end_freq_point - start_freq_point);
                if (w_scale > 0.1)
                {
                    x1 = yohaku_left;
                    y1 = yohaku_top - (int)((10.0 * Math.Log10(myasio.famp(start_freq_point) * famp_max_1) + level_adjust) * h_scale);

                    for (int i = start_freq_point + 1; i < end_freq_point; i++)
                    {
                        x2 = (int)(w_scale * (double)i) + yohaku_left;
                        y2 = yohaku_top - (int)((10.0 * Math.Log10(myasio.famp(i) * famp_max_1) + level_adjust) * h_scale);
                        if (y2 > yohaku_top + height) y2 = yohaku_top + height;

                        points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                        g.DrawLines(pen, points);
                        x1 = x2;
                        y1 = y2;
                    }
                }
                else
                {
                    int st_pnt, ed_pnt;
                    double w_scale_1 = (double)(end_freq_point - start_freq_point) / (double)width, min, max;
                    for (int i = 0; i < width; i++)
                    {
                        x1 = yohaku_left + i;
                        st_pnt = (int)((double)i * w_scale_1) + start_freq_point;
                        ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_freq_point;
                        unsafe
                        {
                            myasio.getminmax_famp(st_pnt, ed_pnt, (int)cinfo.type, cinfo.btn_no, &min, &max);
                        }
                        if (min > 0.0)
                        {
                            y1 = yohaku_top - (int)((10.0 * Math.Log10(min * famp_max_1) + level_adjust) * h_scale);
                            if (y1 > yohaku_top + height) y1 = yohaku_top + height;
                        }
                        else
                        {
                            y1 = yohaku_top + height;
                        }
                        if (max > 0.0)
                        {
                            y2 = yohaku_top - (int)((10.0 * Math.Log10(max * famp_max_1) + level_adjust) * h_scale);
                            if (y2 > yohaku_top + height) y2 = yohaku_top + height;
                        }
                        else
                        {
                            y2 = yohaku_top + height;
                        }
                        x2 = (y1 == y2 ? x1 + 1 : x1);

                        points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                        g.DrawLines(pen, points);
                    }
                }
            }
            else
            {
                int arg = 0;
                double mod = famp_max;
                string ppp = "";
                if (mod > 10.0)
                {
                    for (; mod > 10.0; mod *= 0.1) arg++;
                    ppp = "x1e+";
                }
                else if (mod < 1.0)
                {
                    for (; mod < 1.0; mod *= 10.0) arg--;
                    ppp = "x1e-";
                    arg = -arg;
                }

                string fampt = ppp + arg.ToString();
                g.DrawString(fampt, this.Font, new SolidBrush(Color.Black), yohaku_left + 20, yohaku_top - 15, format);
                g.DrawString(mod.ToString("F2"), this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top, format);
                g.DrawString("0", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top + height - this.Font.Height, format);
                g.DrawString("[Hz]", this.Font, new SolidBrush(Color.Black), ss.Size.Width - 40, ss.Size.Height - this.Font.Height, format);

                double h_scale = (double)height / famp_max;
                double w_scale = (double)width / (double)(end_freq_point - start_freq_point);
                x1 = yohaku_left;
                y1 = yohaku_top + height - (int)(Math.Sqrt(myasio.famp(start_freq_point)) * h_scale); ;
                pen.DashStyle = DashStyle.Solid;
                pen = new Pen(Color.Blue, 1.0F);

                if (w_scale > 0.1)
                {
                    for (int i = start_freq_point + 1; i < end_freq_point; i++)
                    {
                        x2 = (int)(w_scale * (double)i) + yohaku_left;
                        y2 = yohaku_top + height - (int)(Math.Sqrt(myasio.famp(i)) * h_scale);

                        points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                        g.DrawLines(pen, points);
                        x1 = x2;
                        y1 = y2;
                    }
                }
                else
                {
                    int st_pnt, ed_pnt;
                    double w_scale_1 = (double)(end_freq_point - start_freq_point) / (double)width, min, max;
                    for (int i = 0; i < width; i++)
                    {
                        x1 = yohaku_left + i;
                        st_pnt = (int)((double)i * w_scale_1) + start_freq_point;
                        ed_pnt = (int)((double)(i + 1) * w_scale_1) + start_freq_point;
                        unsafe
                        {
                            myasio.getminmax_famp(st_pnt, ed_pnt, (int)cinfo.type, cinfo.btn_no, &min, &max);
                        }
                        if (min > 0.0)
                        {
                            y1 = yohaku_top + height - (int)(Math.Sqrt(min) * h_scale);
                            if (y1 > yohaku_top + height) y1 = yohaku_top + height;
                        }
                        else
                        {
                            y1 = yohaku_top + height;
                        }
                        if (max > 0.0)
                        {
                            y2 = yohaku_top + height - (int)(Math.Sqrt(max) * h_scale);
                            if (y2 > yohaku_top + height) y2 = yohaku_top + height;
                        }
                        else
                        {
                            y2 = yohaku_top + height;
                        }
                        x2 = (y1 == y2 ? x1 + 1 : x1);

                        points = new Point[] { new Point(x1, y1), new Point(x2, y2) };
                        g.DrawLines(pen, points);
                    }
                }
            }
        }

        private void SettingBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            double mval = myasio.GetMaxVal((int)cinfo.type, cinfo.btn_no);
            this.MaxVal_lbl.Text = mval.ToString();
            double tpow = myasio.GetPower_t((int)cinfo.type, cinfo.btn_no, fft_point);
            TotalPow_lbl.Text = tpow.ToString("E5");
            double fpow = myasio.GetPower_f(fft_point) * (double)fft_point;
            FreqPow_lbl.Text = fpow.ToString("E5");
        }

        //		private delegate void textbox_KeyPress(object sender, System.EventArgs e);
        private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private bool force_change = false;
        private void St_time_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_start_time_val = true;
            if (st_time_tb.Text == "")
            {
                st_time_tb.Text = "0";
                return;
            }
            start_time = System.Convert.ToDouble(st_time_tb.Text);
        }

        private string change_text(string src, string dst)
        {
            force_change = (dst != src);
            return src;
        }

        private void St_freq_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_start_freq_val = true;
            if (st_freq_tb.Text == "")
            {
                st_freq_tb.Text = "0";
                return;
            }
            start_freq = System.Convert.ToDouble(st_freq_tb.Text);
        }

        private void St_time_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_start_time_point = true;
            if (st_time_point_tb.Text == "")
            {
                st_time_point_tb.Text = "0";
                return;
            }
            start_time_point = System.Convert.ToInt32(st_time_point_tb.Text);
        }

        private void St_freq_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_start_freq_point = true;
            if (st_freq_point_tb.Text == "")
            {
                st_freq_point_tb.Text = "0";
                return;
            }
            start_freq_point = System.Convert.ToInt32(st_freq_point_tb.Text);
        }

        private void Ed_time_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_end_time_val = true;
            if (ed_time_tb.Text == "")
            {
                ed_time_tb.Text = max_time_ms.ToString("0.000");
                return;
            }
            end_time = System.Convert.ToDouble(ed_time_tb.Text);
            if (end_time > max_time_ms)
            {
                end_time = max_time_ms;
                force_change = true;
                ed_time_tb.Text = end_time.ToString("0.000");
            }
        }

        private void Ed_freq_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_end_freq_val = true;
            if (ed_freq_tb.Text == "")
            {
                ed_freq_tb.Text = max_freq_hz.ToString("0.000");
                return;
            }
            end_freq = System.Convert.ToDouble(ed_freq_tb.Text);
            if (end_freq > max_freq_hz)
            {
                end_freq = max_freq_hz;
                force_change = true;
                ed_freq_tb.Text = end_freq.ToString("0.000");
            }
        }

        private void Ed_time_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_end_time_point = true;
            if (ed_time_point_tb.Text == "")
            {
                ed_time_point_tb.Text = max_time_point.ToString();
                return;
            }
            end_time_point = System.Convert.ToInt32(ed_time_point_tb.Text);
            if (end_time_point > max_time_point)
            {
                end_time_point = max_time_point;
                force_change = true;
                ed_time_point_tb.Text = end_time_point.ToString();
            }
        }

        private void Ed_freq_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_end_freq_point = true;
            if (ed_freq_point_tb.Text == "")
            {
                ed_freq_point_tb.Text = max_freq_point.ToString();
                return;
            }
            end_freq_point = System.Convert.ToInt32(ed_freq_point_tb.Text);
            if (end_freq_point > max_freq_point)
            {
                end_freq_point = max_freq_point;
                force_change = true;
                ed_freq_point_tb.Text = end_freq_point.ToString();
            }
        }

        private void Gr_time_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_grid_time_val = true;
            grid_time = System.Convert.ToDouble(gr_time_tb.Text);
        }

        private void Gr_freq_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_grid_freq_val = true;
            grid_freq = System.Convert.ToDouble(gr_freq_tb.Text);
        }

        private void Gr_time_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_grid_time_point = true;
            grid_time_point = System.Convert.ToInt32(gr_time_point_tb.Text);
        }

        private void Gr_freq_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            update_grid_freq_point = true;
            grid_freq_point = System.Convert.ToInt32(gr_freq_point_tb.Text);
        }

        //		private void fft_point_tb_TextChanged(object sender, System.EventArgs e)
        //		{
        //			fft_point=System.Convert.ToInt32(fft_point_tb.Text);
        //		}

        private void WaveformBox_MouseDown(object sender, MouseEventArgs e)
        {
            int start_x = e.X;
            int start_y = e.Y;
        }

        private void Update_btn(object sender, System.EventArgs e)
        {
            if (!is_fft_calc)
            {
                //int start_pp = parent.get_time_point(start_time);
                //int end_pp = parent.get_time_point(end_time);
                myasio.makeFFT(fft_point, start_time_point, end_time_point, (int)cinfo.type, cinfo.btn_no);
            }

            //int start_pnt = parent.get_freq_point(start_freq, fft_point);
            //int end_pnt = parent.get_freq_point(end_freq, fft_point);
            if (start_freq_point < 0) start_freq_point = 0;
            if (end_freq_point >= fft_point / 2) end_freq_point = fft_point / 2 - 1;

            double famp_max = Math.Sqrt(myasio.famp(start_freq_point));
            double ff, center_freq, st_freq, ed_freq, pp;
            int st_pnt, ed_pnt;
            this.oct_pow_list_tb.AppendText("Center Freq. (Hz) : Amplitude (dB)\n");

            for (int i = 0; i < 34; i++)
            {
                center_freq = 10.0 * Math.Pow(10.0, (double)i * 0.1);
                st_freq = center_freq * Math.Pow(10.0, -0.05);
                ed_freq = center_freq * Math.Pow(10.0, 0.05);
                st_pnt = parent.Get_freq_point(st_freq, fft_point);
                ed_pnt = parent.Get_freq_point(ed_freq, fft_point);
                pp = 0.0;
                for (int j = st_pnt; j < ed_pnt; j++)
                {
                    ff = myasio.famp(j);
                    pp += ff;
                }
                if (pp > 0.0)
                {
                    pp = 10.0 * Math.Log10(pp / (ed_pnt-st_pnt+1));
                    this.oct_pow_list_tb.AppendText(center_freq.ToString("F1") + " : " + pp.ToString("F1") + "\n");
                }
                else {
                    this.oct_pow_list_tb.AppendText(center_freq.ToString("F1") + " : FFT Point is too small\n");
                }
            }
        }

        private void Amp_log_cb_CheckedChanged(object sender, System.EventArgs e)
        {
            disp_range_lbl.Visible = disp_range_tb.Visible = amp_log_cb.Checked;
            grid_range_lbl.Visible = grid_range_tb.Visible = amp_log_cb.Checked;
            level_adjust_lbl.Visible = level_adjust_tb.Visible = amp_log_cb.Checked;
        }

        private void Disp_range_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (disp_range_tb.Text == "")
            {
                disp_range_tb.Text = disp_range.ToString();
            }
            else
            {
                disp_range = System.Convert.ToInt32(disp_range_tb.Text);
            }
        }

        private void Grid_range_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (grid_range_tb.Text == "")
            {
                grid_range_tb.Text = grid_range.ToString();
            }
            else
            {
                grid_range = System.Convert.ToInt32(grid_range_tb.Text);
            }
        }

        private void Spctrgrm_btn_Click(object sender, System.EventArgs e)
        {
            int width = System.Convert.ToInt32(width_tb.Text);
            int offset = System.Convert.ToInt32(offset_tb.Text);
            double ff;
            if (parent.fd.SaveFileDialog("CSV file name", "csv") == false)
            {
                parent.Cnsl_msg("Canceled.\r\n");
                return;
            }
            StreamWriter streamWriter = new StreamWriter(parent.fd.filename, false, System.Text.Encoding.GetEncoding("Shift_JIS"));

            parent.Cnsl_msg("Output CSV file name: " + parent.fd.filename + "\r\n");

            for (double from_time = start_time; from_time < end_time; from_time += offset)
            {
                double to_time = from_time + width;
                if (to_time > end_time) to_time = end_time;
                int from_pp = parent.Get_time_point(from_time);
                int to_pp = parent.Get_time_point(to_time);

                myasio.makeFFT(fft_point, from_pp, to_pp, (int)cinfo.type, cinfo.btn_no);

                //int start_pnt = parent.get_freq_point(start_freq, fft_point);
                //int end_pnt = parent.get_freq_point(end_freq, fft_point);
                if (start_freq_point < 0) start_freq_point = 0;
                if (end_freq_point >= fft_point / 2) end_freq_point = fft_point / 2 - 1;

                for (int i = start_freq_point + 1; i < end_freq_point; i++)
                {
                    double freq = (double)i / (double)fft_point * parent.sample_freq;
                    if (log_cb.Checked) ff = 10.0 * Math.Log10(myasio.famp(i));
                    else ff = Math.Sqrt(myasio.famp(i));
                    string dtxt = from_time.ToString("0.000") + "," + freq.ToString() + "," + ff.ToString() + "\n";
                    streamWriter.Write(dtxt);
                }
            }
            streamWriter.Close();
        }

        private void Fft_point_tb_TextChanged(object sender, System.EventArgs e)
        {
            fft_point = System.Convert.ToInt32(fft_point_tb.Text);
            if (fft_point >= 524288)
            {
                fft_calc_btn.Enabled = true;
                calc_status_lbl.Text = "Push this.";
                calc_status_lbl.Visible = true;
            }
            else
            {
                fft_calc_btn.Enabled = false;
                calc_status_lbl.Visible = false;
            }
            is_fft_calc = false;
        }

        private void Magnify_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (magnify_tb.Text != "") magnify = System.Convert.ToDouble(magnify_tb.Text);
        }

        private void Level_adjust_tb_TextChanged(object sender, System.EventArgs e)
        {
            if (level_adjust_tb.Text != "") level_adjust = System.Convert.ToInt32(level_adjust_tb.Text);
        }

        private void Store_btn_Click(object sender, System.EventArgs e)
        {
            SetDefault();
            parent.myProperty.Save();
        }
        private void SetDefault()
        {
            parent.default_start_time_ms = start_time;
            parent.default_end_time_ms = end_time;
            parent.default_grid_time_ms = grid_time;
            parent.default_magnify = magnify;
            parent.default_start_freq_hz = start_freq;
            parent.default_end_freq_hz = end_freq;
            parent.default_grid_freq_hz = grid_freq;
            parent.default_fft_point = fft_point;
            parent.default_amp_log = amp_log_cb.Checked;
            parent.default_disp_range_db = disp_range;
            parent.default_grid_range_db = grid_range;
            parent.default_level_adjust_db = level_adjust;
        }

        private void Load_btn_Click(object sender, System.EventArgs e)
        {
            LoadProperty();
            update_value();
        }

        private void LoadProperty()
        {
            bool stored = parent.myProperty.Load(false);
            if (stored)
            {
                start_time = parent.default_start_time_ms;
                start_time_point = parent.Get_time_point(start_time);
                end_time = parent.default_end_time_ms;
                end_time_point = parent.Get_time_point(end_time);
                grid_time = parent.default_grid_time_ms;
                grid_time_point = parent.Get_time_point(grid_time);
                magnify = parent.default_magnify;
                fft_point = parent.default_fft_point;
                max_freq_point = fft_point / 2;
                start_freq = parent.default_start_freq_hz;
                start_freq_point = parent.Get_freq_point(start_freq, fft_point);
                end_freq = parent.default_end_freq_hz;
                end_freq_point = parent.Get_freq_point(end_freq, fft_point);
                grid_freq = parent.default_grid_freq_hz;
                grid_freq_point = parent.Get_freq_point(grid_freq, fft_point);
                amp_log_cb.Checked = parent.default_amp_log;
                disp_range = parent.default_disp_range_db;
                grid_range = parent.default_grid_range_db;
                level_adjust = parent.default_level_adjust_db;

                st_time_point_tb.Text = start_time_point.ToString();
                ed_time_point_tb.Text = end_time_point.ToString();
                gr_time_point_tb.Text = grid_time_point.ToString();
                magnify_tb.Text = magnify.ToString();
                st_freq_point_tb.Text = start_freq_point.ToString();
                ed_freq_point_tb.Text = end_freq_point.ToString();
                gr_freq_point_tb.Text = grid_freq_point.ToString();
                fft_point_tb.Text = fft_point.ToString();
                disp_range_tb.Text = disp_range.ToString();
                grid_range_tb.Text = grid_range.ToString();
                level_adjust_tb.Text = level_adjust.ToString();
            }
        }

        private bool is_fft_calc = false;
        private void Fft_calc_btn_Click(object sender, EventArgs e)
        {
            //int start_pp = parent.get_time_point(start_time);
            //int end_pp = parent.get_time_point(end_time);
            calc_status_lbl.Text = "Calcurating...";
            calc_status_lbl.Update();

            myasio.makeFFT(fft_point, start_time_point, end_time_point, (int)cinfo.type, cinfo.btn_no);

            calc_status_lbl.Text = "Calcurated";
            is_fft_calc = true;
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            myasio.data_clear2((int)cinfo.type, cinfo.btn_no, edit_start_time_point, edit_end_time_point);
        }

        private void Edit_st_time_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (edit_st_time_point_tb.Text == "")
            {
                edit_st_time_point_tb.Text = "0";
                return;
            }
            edit_start_time_point = System.Convert.ToInt32(edit_st_time_point_tb.Text);
            edit_start_time = parent.Get_time_ms(edit_start_time_point);
            force_change = true;
            edit_st_time_tb.Text = edit_start_time.ToString("0.000");
        }

        private void Edit_ed_time_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (edit_ed_time_point_tb.Text == "")
            {
                edit_ed_time_point_tb.Text = max_time_point.ToString();
                return;
            }
            edit_end_time_point = System.Convert.ToInt32(edit_ed_time_point_tb.Text);
            if (edit_end_time_point > max_time_point)
            {
                edit_end_time_point = max_time_point;
                force_change = true;
                edit_ed_time_point_tb.Text = edit_end_time_point.ToString();
            }
            edit_end_time = parent.Get_time_ms(edit_end_time_point);
            force_change = true;
            edit_ed_time_tb.Text = edit_end_time.ToString("0.000");

        }

        private void Edit_st_time_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (edit_st_time_tb.Text == "")
            {
                edit_st_time_tb.Text = "0";
                return;
            }
            edit_start_time = System.Convert.ToDouble(edit_st_time_tb.Text);
            edit_start_time_point = parent.Get_time_point(edit_start_time);
            force_change = true;
            edit_st_time_point_tb.Text = edit_start_time_point.ToString();
        }

        private void Edit_ed_time_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (edit_ed_time_tb.Text == "")
            {
                edit_ed_time_tb.Text = max_time_ms.ToString("0.000");
                return;
            }
            edit_end_time = System.Convert.ToDouble(edit_ed_time_tb.Text);
            if (edit_end_time > max_time_ms)
            {
                edit_end_time = max_time_ms;
                force_change = true;
                edit_ed_time_tb.Text = edit_end_time.ToString("0.000");
            }
            edit_end_time_point = parent.Get_time_point(edit_end_time);
            force_change = true;
            edit_ed_time_point_tb.Text = edit_end_time_point.ToString();
        }

        private bool update_start_time_val = false;
        private bool update_start_time_point = true;
        private bool update_end_time_val = false;
        private bool update_end_time_point = true;
        private bool update_grid_time_val = false;
        private bool update_grid_time_point = true;
        private bool update_start_freq_val = false;
        private bool update_start_freq_point = true;
        private bool update_end_freq_val = false;
        private bool update_end_freq_point = true;
        private bool update_grid_freq_val = false;
        private bool update_grid_freq_point = true;
        private void update_value_btn_Click(object sender, EventArgs e)
        {
            update_value();
        }

        private void update_value()
        { 
            if (update_start_time_val)
            {
                start_time_point = parent.Get_time_point(start_time);
                st_time_point_tb.Text = change_text(start_time_point.ToString(), st_time_point_tb.Text);
                update_start_time_val = false;
            }
            if (update_start_freq_val)
            {
                start_freq_point = parent.Get_freq_point(start_freq, fft_point);
                st_freq_point_tb.Text = change_text(start_freq_point.ToString(), st_freq_point_tb.Text);
                update_start_freq_val = false;
            }
            if (update_start_time_point)
            {
                start_time = parent.Get_time_ms(start_time_point);
                st_time_tb.Text = change_text(start_time.ToString("0.000"), st_time_tb.Text);
                update_start_time_point = false;
            }
            if (update_start_freq_point)
            {
                start_freq = parent.Get_freq_hz(start_freq_point, fft_point);
                st_freq_tb.Text = change_text(start_freq.ToString("0.000"), st_freq_tb.Text);
                update_start_freq_point = false;
            }
            if (update_end_time_val)
            {
                end_time_point = parent.Get_time_point(end_time);
                ed_time_point_tb.Text = change_text(end_time_point.ToString(), ed_time_point_tb.Text);
                update_end_time_val = false;
            }
            if (update_end_freq_val)
            {
                end_freq_point = parent.Get_freq_point(end_freq, fft_point);
                ed_freq_point_tb.Text = change_text(end_freq_point.ToString(), ed_freq_point_tb.Text);
                update_end_freq_val = false;
            }
            if (update_end_time_point)
            {
                end_time = parent.Get_time_ms(end_time_point);
                ed_time_tb.Text = change_text(end_time.ToString("0.000"), ed_time_tb.Text);
                update_end_time_point = false;
            }
            if (update_end_freq_point)
            {
                end_freq = parent.Get_freq_hz(end_freq_point, fft_point);
                ed_freq_tb.Text = change_text(end_freq.ToString("0.000"), ed_freq_tb.Text);
                update_end_freq_point = false;
            }
            if (update_grid_time_val)
            {
                grid_time_point = parent.Get_time_point(grid_time);
                gr_time_point_tb.Text = change_text(grid_time_point.ToString(), gr_time_point_tb.Text);
                update_grid_time_val = false;
            }
            if (update_grid_freq_val)
            {
                grid_freq_point = parent.Get_freq_point(grid_freq, fft_point);
                gr_freq_point_tb.Text = change_text(grid_freq_point.ToString(), gr_freq_point_tb.Text);
                update_grid_freq_val = false;
            }
            if (update_grid_time_point)
            {
                grid_time = parent.Get_time_ms(grid_time_point);
                gr_time_tb.Text = change_text(grid_time.ToString("0.000"), gr_time_tb.Text);
                update_grid_time_point = false;
            }
            if (update_grid_freq_point)
            {
                grid_freq = parent.Get_freq_hz(grid_freq_point, fft_point);
                gr_freq_tb.Text = change_text(grid_freq.ToString("0.000"), gr_freq_tb.Text);
                update_grid_freq_point = false;
            }
        }
    }
}
