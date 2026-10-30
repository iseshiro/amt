using System;
using System.Windows.Forms;
using Asio_io;

namespace amt
{
    /// <summary>
    /// DataControlForm の概要の説明です。
    /// </summary>
    public class DataControlForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ok_btn;
		private System.Windows.Forms.Button cancel_btn;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton PinkNoise_rb;
		private System.Windows.Forms.RadioButton WhiteNoise_rb;
		private System.Windows.Forms.TextBox Frequency_tb;
		private asio_io myasio;
        private ChannelInfo cinfo;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox amp_tb;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox pulse_delay_tb;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FFTpnt_tb;
        private Amt parent;
        private TabControl signal_tab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TextBox tsp_phase_denom_tb;
        private TextBox tsp_phase_num_tb;
        private Label label11;
        private Label label12;
        private TextBox tsp_shift_tb;
        private Label label6;
        private CheckBox hilo_cb;
        private GroupBox pink_type_gb;
        private RadioButton voss_rb;
        private RadioButton pole_rb;
        private TabPage tabPage5;
        private TextBox freq_to_tb;
        private Label label7;
        private TextBox freq_from_tb;
        private Label label2;
        private Label label10;
        private Label label13;
        private TextBox level_to_tb;
        private TextBox level_from_tb;
        private Label label15;
        private Label label9;
        private TextBox filt_fftpnt_tb;
        private Label label8;
        private TabPage tabPage6;
        private Label label19;
        private Label label18;
        private TextBox period_point_tb;
        private TextBox period_ms_tb;
        private Label label17;
        private Label label16;
        private Label label14;
        private TextBox min_val_tb;
        private TextBox max_val_tb;
        private RadioButton min2max_rb;
        private RadioButton max2min_rb;
        private Label label20;
        private TextBox pulse_width_tb;
        public bool signal_created = false;

        public DataControlForm(Amt Parent, ChannelInfo Cinfo)
		{
			InitializeComponent();
			parent = Parent;
			myasio = parent.asio;
            cinfo = Cinfo;
            period_point_tb.Text = "8192";
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.PinkNoise_rb = new System.Windows.Forms.RadioButton();
            this.WhiteNoise_rb = new System.Windows.Forms.RadioButton();
            this.Frequency_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.amp_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pulse_delay_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FFTpnt_tb = new System.Windows.Forms.TextBox();
            this.signal_tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.pulse_width_tb = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pink_type_gb = new System.Windows.Forms.GroupBox();
            this.voss_rb = new System.Windows.Forms.RadioButton();
            this.pole_rb = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.hilo_cb = new System.Windows.Forms.CheckBox();
            this.tsp_shift_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_denom_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_num_tb = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.filt_fftpnt_tb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.level_to_tb = new System.Windows.Forms.TextBox();
            this.level_from_tb = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.freq_to_tb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.freq_from_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.min2max_rb = new System.Windows.Forms.RadioButton();
            this.max2min_rb = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.period_point_tb = new System.Windows.Forms.TextBox();
            this.period_ms_tb = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.min_val_tb = new System.Windows.Forms.TextBox();
            this.max_val_tb = new System.Windows.Forms.TextBox();
            this.signal_tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pink_type_gb.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(213, 188);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(102, 36);
            this.ok_btn.TabIndex = 0;
            this.ok_btn.Text = "OK";
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(352, 188);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(102, 36);
            this.cancel_btn.TabIndex = 1;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click_1);
            // 
            // PinkNoise_rb
            // 
            this.PinkNoise_rb.Location = new System.Drawing.Point(18, 48);
            this.PinkNoise_rb.Name = "PinkNoise_rb";
            this.PinkNoise_rb.Size = new System.Drawing.Size(153, 24);
            this.PinkNoise_rb.TabIndex = 4;
            this.PinkNoise_rb.Text = "Pink Noise";
            this.PinkNoise_rb.CheckedChanged += new System.EventHandler(this.PinkNoise_rb_CheckedChanged);
            // 
            // WhiteNoise_rb
            // 
            this.WhiteNoise_rb.Checked = true;
            this.WhiteNoise_rb.Location = new System.Drawing.Point(18, 15);
            this.WhiteNoise_rb.Name = "WhiteNoise_rb";
            this.WhiteNoise_rb.Size = new System.Drawing.Size(153, 24);
            this.WhiteNoise_rb.TabIndex = 5;
            this.WhiteNoise_rb.TabStop = true;
            this.WhiteNoise_rb.Text = "White Noise";
            // 
            // Frequency_tb
            // 
            this.Frequency_tb.Location = new System.Drawing.Point(198, 20);
            this.Frequency_tb.Name = "Frequency_tb";
            this.Frequency_tb.Size = new System.Drawing.Size(98, 25);
            this.Frequency_tb.TabIndex = 6;
            this.Frequency_tb.Text = "1000";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(58, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Freqency [Hz]";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Amp";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // amp_tb
            // 
            this.amp_tb.Location = new System.Drawing.Point(86, 192);
            this.amp_tb.Name = "amp_tb";
            this.amp_tb.Size = new System.Drawing.Size(103, 25);
            this.amp_tb.TabIndex = 13;
            this.amp_tb.Text = "1.0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(58, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 16;
            this.label4.Text = "Delay";
            // 
            // pulse_delay_tb
            // 
            this.pulse_delay_tb.Location = new System.Drawing.Point(134, 20);
            this.pulse_delay_tb.Name = "pulse_delay_tb";
            this.pulse_delay_tb.Size = new System.Drawing.Size(103, 25);
            this.pulse_delay_tb.TabIndex = 15;
            this.pulse_delay_tb.Text = "0";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 27);
            this.label5.TabIndex = 20;
            this.label5.Text = "FFT point";
            // 
            // FFTpnt_tb
            // 
            this.FFTpnt_tb.Location = new System.Drawing.Point(24, 42);
            this.FFTpnt_tb.Name = "FFTpnt_tb";
            this.FFTpnt_tb.Size = new System.Drawing.Size(93, 25);
            this.FFTpnt_tb.TabIndex = 19;
            this.FFTpnt_tb.Text = "8192";
            this.FFTpnt_tb.TextChanged += new System.EventHandler(this.FFTPoint_Check);
            // 
            // signal_tab
            // 
            this.signal_tab.Controls.Add(this.tabPage1);
            this.signal_tab.Controls.Add(this.tabPage2);
            this.signal_tab.Controls.Add(this.tabPage3);
            this.signal_tab.Controls.Add(this.tabPage4);
            this.signal_tab.Controls.Add(this.tabPage5);
            this.signal_tab.Controls.Add(this.tabPage6);
            this.signal_tab.Location = new System.Drawing.Point(19, 18);
            this.signal_tab.Name = "signal_tab";
            this.signal_tab.SelectedIndex = 0;
            this.signal_tab.Size = new System.Drawing.Size(539, 160);
            this.signal_tab.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.pulse_width_tb);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.pulse_delay_tb);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(531, 128);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pulse";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(269, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 24);
            this.label20.TabIndex = 18;
            this.label20.Text = "Width";
            // 
            // pulse_width_tb
            // 
            this.pulse_width_tb.Location = new System.Drawing.Point(346, 20);
            this.pulse_width_tb.Name = "pulse_width_tb";
            this.pulse_width_tb.Size = new System.Drawing.Size(102, 25);
            this.pulse_width_tb.TabIndex = 17;
            this.pulse_width_tb.Text = "1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pink_type_gb);
            this.tabPage2.Controls.Add(this.WhiteNoise_rb);
            this.tabPage2.Controls.Add(this.PinkNoise_rb);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(531, 128);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Random noise";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pink_type_gb
            // 
            this.pink_type_gb.Controls.Add(this.voss_rb);
            this.pink_type_gb.Controls.Add(this.pole_rb);
            this.pink_type_gb.Location = new System.Drawing.Point(166, 21);
            this.pink_type_gb.Name = "pink_type_gb";
            this.pink_type_gb.Size = new System.Drawing.Size(253, 84);
            this.pink_type_gb.TabIndex = 6;
            this.pink_type_gb.TabStop = false;
            this.pink_type_gb.Text = "Pinking Filter Type";
            this.pink_type_gb.Visible = false;
            // 
            // voss_rb
            // 
            this.voss_rb.Checked = true;
            this.voss_rb.Location = new System.Drawing.Point(19, 54);
            this.voss_rb.Name = "voss_rb";
            this.voss_rb.Size = new System.Drawing.Size(168, 26);
            this.voss_rb.TabIndex = 8;
            this.voss_rb.TabStop = true;
            this.voss_rb.Text = "Voss algorithm";
            // 
            // pole_rb
            // 
            this.pole_rb.Location = new System.Drawing.Point(19, 22);
            this.pole_rb.Name = "pole_rb";
            this.pole_rb.Size = new System.Drawing.Size(221, 32);
            this.pole_rb.TabIndex = 7;
            this.pole_rb.Text = "Traditional Pole Model";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Frequency_tb);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(531, 128);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pure tone";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.hilo_cb);
            this.tabPage4.Controls.Add(this.tsp_shift_tb);
            this.tabPage4.Controls.Add(this.tsp_phase_denom_tb);
            this.tabPage4.Controls.Add(this.tsp_phase_num_tb);
            this.tabPage4.Controls.Add(this.FFTpnt_tb);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(531, 128);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "TSP";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // hilo_cb
            // 
            this.hilo_cb.AutoSize = true;
            this.hilo_cb.Location = new System.Drawing.Point(251, 78);
            this.hilo_cb.Name = "hilo_cb";
            this.hilo_cb.Size = new System.Drawing.Size(144, 22);
            this.hilo_cb.TabIndex = 33;
            this.hilo_cb.Text = "High ---> Low";
            this.hilo_cb.UseVisualStyleBackColor = true;
            // 
            // tsp_shift_tb
            // 
            this.tsp_shift_tb.Location = new System.Drawing.Point(326, 38);
            this.tsp_shift_tb.Name = "tsp_shift_tb";
            this.tsp_shift_tb.Size = new System.Drawing.Size(84, 25);
            this.tsp_shift_tb.TabIndex = 32;
            this.tsp_shift_tb.Text = "0.125";
            // 
            // tsp_phase_denom_tb
            // 
            this.tsp_phase_denom_tb.Location = new System.Drawing.Point(229, 40);
            this.tsp_phase_denom_tb.Name = "tsp_phase_denom_tb";
            this.tsp_phase_denom_tb.Size = new System.Drawing.Size(54, 25);
            this.tsp_phase_denom_tb.TabIndex = 28;
            this.tsp_phase_denom_tb.Text = "8";
            // 
            // tsp_phase_num_tb
            // 
            this.tsp_phase_num_tb.Location = new System.Drawing.Point(149, 40);
            this.tsp_phase_num_tb.Name = "tsp_phase_num_tb";
            this.tsp_phase_num_tb.Size = new System.Drawing.Size(53, 25);
            this.tsp_phase_num_tb.TabIndex = 26;
            this.tsp_phase_num_tb.Text = "7";
            this.tsp_phase_num_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(200, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 42);
            this.label11.TabIndex = 29;
            this.label11.Text = "/";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(142, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 30);
            this.label12.TabIndex = 27;
            this.label12.Text = "Phase Range";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(323, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 30);
            this.label6.TabIndex = 31;
            this.label6.Text = "Shift";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.filt_fftpnt_tb);
            this.tabPage5.Controls.Add(this.label8);
            this.tabPage5.Controls.Add(this.label10);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.level_to_tb);
            this.tabPage5.Controls.Add(this.level_from_tb);
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.freq_to_tb);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.freq_from_tb);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Location = new System.Drawing.Point(4, 28);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(531, 128);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Filt";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // filt_fftpnt_tb
            // 
            this.filt_fftpnt_tb.Location = new System.Drawing.Point(157, 81);
            this.filt_fftpnt_tb.Name = "filt_fftpnt_tb";
            this.filt_fftpnt_tb.Size = new System.Drawing.Size(93, 25);
            this.filt_fftpnt_tb.TabIndex = 21;
            this.filt_fftpnt_tb.Text = "8192";
            this.filt_fftpnt_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(46, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 26);
            this.label8.TabIndex = 22;
            this.label8.Text = "FFT point";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(306, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 18);
            this.label10.TabIndex = 11;
            this.label10.Text = "dB";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(189, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 18);
            this.label13.TabIndex = 10;
            this.label13.Text = "-";
            // 
            // level_to_tb
            // 
            this.level_to_tb.Location = new System.Drawing.Point(227, 48);
            this.level_to_tb.Name = "level_to_tb";
            this.level_to_tb.Size = new System.Drawing.Size(63, 25);
            this.level_to_tb.TabIndex = 9;
            this.level_to_tb.Text = "-11";
            this.level_to_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // level_from_tb
            // 
            this.level_from_tb.Location = new System.Drawing.Point(109, 48);
            this.level_from_tb.Name = "level_from_tb";
            this.level_from_tb.Size = new System.Drawing.Size(62, 25);
            this.level_from_tb.TabIndex = 7;
            this.level_from_tb.Text = "0";
            this.level_from_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(45, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 18);
            this.label15.TabIndex = 6;
            this.label15.Text = "Level";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(306, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 18);
            this.label9.TabIndex = 5;
            this.label9.Text = "kHz";
            // 
            // freq_to_tb
            // 
            this.freq_to_tb.Location = new System.Drawing.Point(227, 12);
            this.freq_to_tb.Name = "freq_to_tb";
            this.freq_to_tb.Size = new System.Drawing.Size(63, 25);
            this.freq_to_tb.TabIndex = 3;
            this.freq_to_tb.Text = "24";
            this.freq_to_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(189, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "-";
            // 
            // freq_from_tb
            // 
            this.freq_from_tb.Location = new System.Drawing.Point(109, 12);
            this.freq_from_tb.Name = "freq_from_tb";
            this.freq_from_tb.Size = new System.Drawing.Size(62, 25);
            this.freq_from_tb.TabIndex = 1;
            this.freq_from_tb.Text = "6";
            this.freq_from_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Freq";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.min2max_rb);
            this.tabPage6.Controls.Add(this.max2min_rb);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.period_point_tb);
            this.tabPage6.Controls.Add(this.period_ms_tb);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Controls.Add(this.label14);
            this.tabPage6.Controls.Add(this.min_val_tb);
            this.tabPage6.Controls.Add(this.max_val_tb);
            this.tabPage6.Location = new System.Drawing.Point(4, 28);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(531, 128);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Saw";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // min2max_rb
            // 
            this.min2max_rb.AutoSize = true;
            this.min2max_rb.Location = new System.Drawing.Point(238, 45);
            this.min2max_rb.Name = "min2max_rb";
            this.min2max_rb.Size = new System.Drawing.Size(135, 22);
            this.min2max_rb.TabIndex = 10;
            this.min2max_rb.TabStop = true;
            this.min2max_rb.Text = "Min ---> Max";
            this.min2max_rb.UseVisualStyleBackColor = true;
            // 
            // max2min_rb
            // 
            this.max2min_rb.AutoSize = true;
            this.max2min_rb.Checked = true;
            this.max2min_rb.Location = new System.Drawing.Point(238, 12);
            this.max2min_rb.Name = "max2min_rb";
            this.max2min_rb.Size = new System.Drawing.Size(135, 22);
            this.max2min_rb.TabIndex = 9;
            this.max2min_rb.TabStop = true;
            this.max2min_rb.Text = "Max ---> Min";
            this.max2min_rb.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(398, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 18);
            this.label19.TabIndex = 8;
            this.label19.Text = "point";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(219, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 18);
            this.label18.TabIndex = 7;
            this.label18.Text = "ms";
            // 
            // period_point_tb
            // 
            this.period_point_tb.Location = new System.Drawing.Point(285, 81);
            this.period_point_tb.Name = "period_point_tb";
            this.period_point_tb.Size = new System.Drawing.Size(104, 25);
            this.period_point_tb.TabIndex = 6;
            this.period_point_tb.TextChanged += new System.EventHandler(this.period_point_tb_TextChanged);
            // 
            // period_ms_tb
            // 
            this.period_ms_tb.Location = new System.Drawing.Point(106, 81);
            this.period_ms_tb.Name = "period_ms_tb";
            this.period_ms_tb.Size = new System.Drawing.Size(104, 25);
            this.period_ms_tb.TabIndex = 5;
            this.period_ms_tb.TextChanged += new System.EventHandler(this.period_ms_tb_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(37, 86);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 18);
            this.label17.TabIndex = 4;
            this.label17.Text = "Period";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 48);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 18);
            this.label16.TabIndex = 3;
            this.label16.Text = "Min value";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 18);
            this.label14.TabIndex = 2;
            this.label14.Text = "Max value";
            // 
            // min_val_tb
            // 
            this.min_val_tb.Location = new System.Drawing.Point(106, 44);
            this.min_val_tb.Name = "min_val_tb";
            this.min_val_tb.Size = new System.Drawing.Size(104, 25);
            this.min_val_tb.TabIndex = 1;
            this.min_val_tb.Text = "-1.0";
            // 
            // max_val_tb
            // 
            this.max_val_tb.Location = new System.Drawing.Point(106, 6);
            this.max_val_tb.Name = "max_val_tb";
            this.max_val_tb.Size = new System.Drawing.Size(104, 25);
            this.max_val_tb.TabIndex = 0;
            this.max_val_tb.Text = "1.0";
            // 
            // DataControlForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(574, 246);
            this.ControlBox = false;
            this.Controls.Add(this.signal_tab);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.amp_tb);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Name = "DataControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Signal";
            this.signal_tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.pink_type_gb.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


        private enum SignalType { PureTone, TSP, Pink_Pole, Pink_Voss, White, Pulse, Filter, Saw };
		private void makeSignal(SignalType stype)
		{

            double amp = System.Convert.ToDouble(amp_tb.Text);
            uint rndm_coef;
            switch (stype)
            {
                case SignalType.PureTone:
                    double freq = System.Convert.ToDouble(Frequency_tb.Text);
                    if (freq > parent.sample_freq / 2)
                    {
                        parent.Cnsl_msg("Error: Invalid Frequency:" + Frequency_tb.Text);
                    }
                    myasio.makePureTone(amp, freq, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makePureTone" + parent.ScriptSeparator + amp.ToString() + parent.ScriptSeparator + freq.ToString(), cinfo);
                    parent.PostCreateSignal(cinfo, "PureTone was created.");
                    break;
                case SignalType.TSP:
                    int fftpoint = System.Convert.ToInt32(FFTpnt_tb.Text);
                    int fftp = (int)Math.Pow(2.0, (double)((int)(Math.Log10((double)fftpoint) / Math.Log10(2.0) + 0.5)));
                    if (fftp != fftpoint)
                    {
                        parent.Cnsl_msg("Error: Invalid FFT point:" + FFTpnt_tb.Text);
                    }
                    int coef_m_numer = System.Convert.ToInt32(tsp_phase_num_tb.Text);
                    int coef_m_denom = System.Convert.ToInt32(tsp_phase_denom_tb.Text);
                    double coef_shift = System.Convert.ToDouble(tsp_shift_tb.Text);
                    int flag = (hilo_cb.Checked ? 1 : -1);

                    myasio.makeTSP(fftpoint, (int)cinfo.type, cinfo.btn_no, amp, coef_m_numer, coef_m_denom, coef_shift, flag);
                    if (parent.script_recording) parent.MyScripts_Add("makeTSP" + parent.ScriptSeparator + fftpoint.ToString()
                        + parent.ScriptSeparator + amp.ToString()
                        + parent.ScriptSeparator + coef_m_numer.ToString()
                        + parent.ScriptSeparator + coef_m_denom.ToString()
                        + parent.ScriptSeparator + coef_shift.ToString()
                        + parent.ScriptSeparator + flag.ToString(), cinfo
                        );
                    parent.PostCreateSignal(cinfo, "TSP was created.");
                    break;
                case SignalType.Pink_Pole:
                    rndm_coef = (uint)Environment.TickCount;
                    myasio.makePinkNoise_Pole(amp, rndm_coef, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makePinkNoise" + parent.ScriptSeparator + amp.ToString() + parent.ScriptSeparator + rndm_coef.ToString() + parent.ScriptSeparator + "Pole", cinfo);
                    parent.PostCreateSignal(cinfo, "Pink Noise (Traditional Pole Model) was created.");
                    break;
                case SignalType.Pink_Voss:
                    rndm_coef = (uint)Environment.TickCount;
                    myasio.makePinkNoise_Voss(amp, rndm_coef, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makePinkNoise" + parent.ScriptSeparator + amp.ToString() + parent.ScriptSeparator + rndm_coef.ToString() + parent.ScriptSeparator + "Voss", cinfo);
                    parent.PostCreateSignal(cinfo, "Pink Noise (Voss algorithm) was created.");
                    break;
                case SignalType.White:
                    rndm_coef = (uint)Environment.TickCount;
                    myasio.makeWhiteNoise(amp, rndm_coef, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makeWhiteNoise" + parent.ScriptSeparator + amp.ToString() + parent.ScriptSeparator + rndm_coef.ToString(), cinfo);
                    parent.PostCreateSignal(cinfo, "White Noise was created.");
                    break;
                case SignalType.Pulse:
                    int delay = System.Convert.ToInt32(pulse_delay_tb.Text);
                    int width = System.Convert.ToInt32(pulse_width_tb.Text);
                    myasio.makePulse(amp, delay, width, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makePulse" + parent.ScriptSeparator + amp.ToString() + parent.ScriptSeparator + delay.ToString() + parent.ScriptSeparator + width.ToString(), cinfo);
                    parent.PostCreateSignal(cinfo, "Pulse was created.");
                    break;
                case SignalType.Filter:
                    int freq_from = System.Convert.ToInt32(freq_from_tb.Text);
                    int freq_to = System.Convert.ToInt32(freq_to_tb.Text);
                    int level_from = System.Convert.ToInt32(level_from_tb.Text);
                    int level_to = System.Convert.ToInt32(level_to_tb.Text);
                    fftpoint = System.Convert.ToInt32(filt_fftpnt_tb.Text);
                    myasio.makeFilter(freq_from, freq_to, level_from, level_to, fftpoint, (int)cinfo.type, cinfo.btn_no);
                    if (parent.script_recording) parent.MyScripts_Add("makeFilter" + parent.ScriptSeparator + freq_from.ToString() + parent.ScriptSeparator + freq_to.ToString() + parent.ScriptSeparator + level_from.ToString() + parent.ScriptSeparator + level_to.ToString() + parent.ScriptSeparator + fftpoint.ToString(), cinfo);
                    parent.PostCreateSignal(cinfo, "Filter was created.");
                    break;
                case SignalType.Saw:
                    double max_val = System.Convert.ToDouble(max_val_tb.Text);
                    double min_val = System.Convert.ToDouble(min_val_tb.Text);
                    int period_point = System.Convert.ToInt32(period_point_tb.Text);
                    bool max2min = max2min_rb.Checked;
                    myasio.makeSaw((int)cinfo.type, cinfo.btn_no, max_val, min_val, period_point, max2min);
                    if (parent.script_recording) parent.MyScripts_Add("makeSaw" + parent.ScriptSeparator + max_val.ToString() + parent.ScriptSeparator + min_val.ToString() + parent.ScriptSeparator + period_point.ToString() + parent.ScriptSeparator + max2min.ToString(), cinfo);
                    parent.PostCreateSignal(cinfo, "Saw was created.");
                    break;
            }
            signal_created = true;
        }
        private void FFTPoint_Check(object sender, System.EventArgs e)
        {
            ok_btn.Enabled = false;
            if (FFTpnt_tb.Text == "") return;
            int fftpoint = System.Convert.ToInt32(FFTpnt_tb.Text);
            int fftp = (int)Math.Pow(2.0, (double)((int)(Math.Log10((double)fftpoint) / Math.Log10(2.0) + 0.5)));
            ok_btn.Enabled = (fftp == fftpoint);
            if (fftpoint < 64) ok_btn.Enabled = false;
        }

		private void ok_btn_Click(object sender, System.EventArgs e)
		{
			SignalType st = SignalType.PureTone;

            if (signal_tab.SelectedTab.Text == "Pure tone")
            {
				st = SignalType.PureTone;
            }
            else if (signal_tab.SelectedTab.Text == "Random noise")
            {
                if (PinkNoise_rb.Checked)
                {
                    st = (pole_rb.Checked? SignalType.Pink_Pole:SignalType.Pink_Voss) ;
                }
                else if (WhiteNoise_rb.Checked) st = SignalType.White;
            }
            else if (signal_tab.SelectedTab.Text == "Pulse")
            {
				st = SignalType.Pulse;
            }
            else if (signal_tab.SelectedTab.Text == "TSP")
            {
                st = SignalType.TSP;
            }
            else if (signal_tab.SelectedTab.Text == "Filt")
            {
                st = SignalType.Filter;
            }
            else if (signal_tab.SelectedTab.Text == "Saw")
            {
                st = SignalType.Saw;
            }

			makeSignal(st);
				
			Dispose();
		}

		private void cancel_btn_Click_1(object sender, System.EventArgs e)
		{
			Dispose();
		}

        private void PinkNoise_rb_CheckedChanged(object sender, EventArgs e)
        {
            pink_type_gb.Visible = PinkNoise_rb.Checked;
        }

        private bool force_change = false;
        private void period_point_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (period_point_tb.Text == "")
            {
                period_point_tb.Text = "0";
                return;
            }
            int period_pnt = System.Convert.ToInt32(period_point_tb.Text);
            double period_ms = parent.Get_time_ms(period_pnt);
            force_change = true;
            period_ms_tb.Text = period_ms.ToString("0.000");
        }

        private void period_ms_tb_TextChanged(object sender, EventArgs e)
        {
            if (force_change)
            {
                force_change = false;
                return;
            }
            if (period_ms_tb.Text == "")
            {
                period_ms_tb.Text = "0";
                return;
            }
            double period_ms = System.Convert.ToDouble(period_ms_tb.Text);
            int period_pnt = parent.Get_time_point(period_ms);
            force_change = true;
            period_point_tb.Text = period_pnt.ToString();
        }
	}
}
