//#define D_DEBUG

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Asio_io;

namespace amt
{
    /// <summary>
    /// ProcessWave の概要の説明です。
    /// </summary>
    public class ProcessWave : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button start_btn;
		private System.Windows.Forms.PictureBox waveformBox;
        private Amt parent;
		private asio_io myasio;

		// Adjust parameters
		public int firtap;
		public double tspamp;
		public int delay_point;
		public double delay_amp;
		public double mu=0.00001;
		public int start_time=0;
		public int end_time=20;
		public double magnify=1.0;
        public ChannelInfo mic_ch;
        public ChannelInfo sp1_ch;
        public ChannelInfo sp2_ch;
        private ChannelInfo work_ch5;
        private ChannelInfo work_ch6;
        private ChannelInfo work_ch1;
        private ChannelInfo work_ch4;
        private ChannelInfo work_ch2;
        private ChannelInfo work_ch3;
        private int iteration;
        private bool adaptation_continue;
        private const double freq_upper_limit = 20000.0;
        private const double freq_lower_limit = 200.0;


		// Drawing parameters
		private int grid_time=5;
		private ChannelInfo draw_ch = null;
		private ChannelInfo draw_ch1 = null;
		private int yohaku_left=20;
		private int yohaku_right=10;
		private int yohaku_top=10;
		private int yohaku_down=20;
		private int wavebox_width;
		private int wavebox_height;
		private int wavebox_middle;

		#region controls

        private System.Windows.Forms.Label pointing_lbl;
		private System.Windows.Forms.Button cursor_left_btn;
		private System.Windows.Forms.Button cursor_right_btn;
		private System.Windows.Forms.TextBox delay_point_tb;
		private System.Windows.Forms.TextBox delay_amp_tb;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox mu_tb;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox disp_end_time_tb;
		private System.Windows.Forms.TextBox disp_start_time_tb;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox display_magnify_tb;
        private System.Windows.Forms.Label label10;
		#endregion
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox iteration_tb;
		private System.Windows.Forms.RadioButton adaptation_restart_rb;
		private System.Windows.Forms.RadioButton adaptation_continue_rb;
		private System.Windows.Forms.GroupBox adaptation_gb;

        public bool success = true;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox firtap_tb;
        private int fftpoint;
        public string message;
		private int tsp_fftpoint=2;
        private System.Windows.Forms.TextBox tspamp_tb;
        private RadioButton left_rb;
        private RadioButton right_rb;
        private Button disp_update_btn;
        private Button connect_btn;
		private System.Windows.Forms.Label label19;
        private TcpClient TcpClientA;
        private NetworkStream MyStream = null;
        private byte[] RcvBuf = new byte[200];
        private float[] fir_l1_coef;
        private float[] fir_l2_coef;
        private float[] fir_r1_coef;
        private float[] fir_r2_coef;
        private Int16[] FirCoef;
        private int fir1_max_val, fir1_min_val, fir1_max_pnt, fir1_min_pnt;
        private int fir2_max_val, fir2_min_val, fir2_max_pnt, fir2_min_pnt;
        private int fir3_max_val, fir3_min_val, fir3_max_pnt, fir3_min_pnt;
        private int fir4_max_val, fir4_min_val, fir4_max_pnt, fir4_min_pnt;
        private ProgressBar progressBar1;
        private Button fir1_pulse_btn;
        private Button etoe_test_btn;
        private Button save_fir_btn;

        enum SetDampFIRStatus { fir_left1, fir_left2, fir_right1, fir_right2 };

		public ProcessWave(Amt Parent)
		{
			InitializeComponent();

			parent=Parent;
			myasio= parent.asio;
            fftpoint = parent.fftpoint;
            while (tsp_fftpoint < myasio.GetDataLength()) tsp_fftpoint *= 2;
            if (tsp_fftpoint > myasio.GetDataLength()) tsp_fftpoint /= 2;

			firtap = System.Convert.ToInt32(firtap_tb.Text);
			tspamp = System.Convert.ToDouble(tspamp_tb.Text);
			delay_point = System.Convert.ToInt32(delay_point_tb.Text);
			delay_amp = System.Convert.ToDouble(delay_amp_tb.Text);
			mu = System.Convert.ToDouble(mu_tb.Text);
			start_time = System.Convert.ToInt32(disp_start_time_tb.Text);
			end_time = System.Convert.ToInt32(disp_end_time_tb.Text);
			magnify = System.Convert.ToDouble(display_magnify_tb.Text);
            iteration = System.Convert.ToInt32(iteration_tb.Text);
			adaptation_continue = adaptation_continue_rb.Checked;

            mic_ch = parent.Find_ch_info("IN", 65); parent.DataCreate(mic_ch, ChannelInfo.StreamType.asioin);
            sp1_ch = parent.Find_ch_info("OUT", 1); parent.DataCreate(sp1_ch, ChannelInfo.StreamType.asioout);
            sp2_ch = parent.Find_ch_info("OUT", 2); parent.DataCreate(sp2_ch, ChannelInfo.StreamType.asioout);
            work_ch1 = parent.Find_ch_info("WORK", 1); parent.DataCreate(work_ch1, ChannelInfo.StreamType.no_io); // h1
            work_ch2 = parent.Find_ch_info("WORK", 2); parent.DataCreate(work_ch2, ChannelInfo.StreamType.no_io); // h1_d : ClearBothSides(h1, start, end)
            work_ch3 = parent.Find_ch_info("WORK", 3); parent.DataCreate(work_ch3, ChannelInfo.StreamType.no_io); // h2
            work_ch4 = parent.Find_ch_info("WORK", 4); parent.DataCreate(work_ch4, ChannelInfo.StreamType.no_io); // h1 - h1_d
            work_ch5 = parent.Find_ch_info("WORK", 5); parent.DataCreate(work_ch5, ChannelInfo.StreamType.no_io); // fir_coef
            work_ch6 = parent.Find_ch_info("WORK", 6); parent.DataCreate(work_ch6, ChannelInfo.StreamType.no_io);

			wavebox_width = waveformBox.Size.Width-yohaku_left-yohaku_right;
			wavebox_height = waveformBox.Size.Height-yohaku_top-yohaku_down;
			wavebox_middle = yohaku_top+wavebox_height/2;

            TcpClientA = new TcpClient();
            FirCoef = new Int16[firtap * 4];
            fir_l1_coef = new float[firtap];
            fir_l2_coef = new float[firtap];
            fir_r1_coef = new float[firtap];
            fir_r2_coef = new float[firtap];
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
            this.waveformBox = new System.Windows.Forms.PictureBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.pointing_lbl = new System.Windows.Forms.Label();
            this.cursor_left_btn = new System.Windows.Forms.Button();
            this.cursor_right_btn = new System.Windows.Forms.Button();
            this.delay_point_tb = new System.Windows.Forms.TextBox();
            this.delay_amp_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mu_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.disp_end_time_tb = new System.Windows.Forms.TextBox();
            this.disp_start_time_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.display_magnify_tb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.iteration_tb = new System.Windows.Forms.TextBox();
            this.adaptation_restart_rb = new System.Windows.Forms.RadioButton();
            this.adaptation_continue_rb = new System.Windows.Forms.RadioButton();
            this.adaptation_gb = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.firtap_tb = new System.Windows.Forms.TextBox();
            this.tspamp_tb = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.left_rb = new System.Windows.Forms.RadioButton();
            this.right_rb = new System.Windows.Forms.RadioButton();
            this.disp_update_btn = new System.Windows.Forms.Button();
            this.connect_btn = new System.Windows.Forms.Button();
            this.save_fir_btn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.fir1_pulse_btn = new System.Windows.Forms.Button();
            this.etoe_test_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.waveformBox)).BeginInit();
            this.adaptation_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // waveformBox
            // 
            this.waveformBox.BackColor = System.Drawing.SystemColors.Control;
            this.waveformBox.Location = new System.Drawing.Point(8, 8);
            this.waveformBox.Name = "waveformBox";
            this.waveformBox.Size = new System.Drawing.Size(512, 368);
            this.waveformBox.TabIndex = 0;
            this.waveformBox.TabStop = false;
            this.waveformBox.Paint += new System.Windows.Forms.PaintEventHandler(this.waveformBox_Paint);
            this.waveformBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.waveformBox_MouseDown);
            this.waveformBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.waveformBox_MouseUp);
            // 
            // start_btn
            // 
            this.start_btn.Enabled = false;
            this.start_btn.Location = new System.Drawing.Point(543, 210);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(71, 32);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "Start";
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // pointing_lbl
            // 
            this.pointing_lbl.Location = new System.Drawing.Point(72, 376);
            this.pointing_lbl.Name = "pointing_lbl";
            this.pointing_lbl.Size = new System.Drawing.Size(448, 16);
            this.pointing_lbl.TabIndex = 4;
            this.pointing_lbl.Text = "point=100 (10ms)";
            // 
            // cursor_left_btn
            // 
            this.cursor_left_btn.Enabled = false;
            this.cursor_left_btn.Location = new System.Drawing.Point(56, 392);
            this.cursor_left_btn.Name = "cursor_left_btn";
            this.cursor_left_btn.Size = new System.Drawing.Size(16, 16);
            this.cursor_left_btn.TabIndex = 6;
            this.cursor_left_btn.Text = "<";
            this.cursor_left_btn.Click += new System.EventHandler(this.cursor_left_btn_Click);
            // 
            // cursor_right_btn
            // 
            this.cursor_right_btn.Enabled = false;
            this.cursor_right_btn.Location = new System.Drawing.Point(80, 392);
            this.cursor_right_btn.Name = "cursor_right_btn";
            this.cursor_right_btn.Size = new System.Drawing.Size(16, 16);
            this.cursor_right_btn.TabIndex = 7;
            this.cursor_right_btn.Text = ">";
            this.cursor_right_btn.Click += new System.EventHandler(this.cursor_right_btn_Click);
            // 
            // delay_point_tb
            // 
            this.delay_point_tb.Location = new System.Drawing.Point(592, 32);
            this.delay_point_tb.Name = "delay_point_tb";
            this.delay_point_tb.Size = new System.Drawing.Size(40, 19);
            this.delay_point_tb.TabIndex = 8;
            this.delay_point_tb.Text = "30";
            this.delay_point_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.delay_point_tb.TextChanged += new System.EventHandler(this.delay_point_tb_TextChanged);
            // 
            // delay_amp_tb
            // 
            this.delay_amp_tb.Location = new System.Drawing.Point(592, 56);
            this.delay_amp_tb.Name = "delay_amp_tb";
            this.delay_amp_tb.Size = new System.Drawing.Size(40, 19);
            this.delay_amp_tb.TabIndex = 9;
            this.delay_amp_tb.Text = "0.5";
            this.delay_amp_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.delay_amp_tb.TextChanged += new System.EventHandler(this.delay_amp_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(544, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Delay";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(552, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Amp";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(568, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "m";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // mu_tb
            // 
            this.mu_tb.Location = new System.Drawing.Point(592, 80);
            this.mu_tb.Name = "mu_tb";
            this.mu_tb.Size = new System.Drawing.Size(40, 19);
            this.mu_tb.TabIndex = 12;
            this.mu_tb.Text = "2";
            this.mu_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mu_tb.TextChanged += new System.EventHandler(this.mu_tb_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(648, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "End [ms]";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(640, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Start [ms]";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // disp_end_time_tb
            // 
            this.disp_end_time_tb.Location = new System.Drawing.Point(712, 56);
            this.disp_end_time_tb.Name = "disp_end_time_tb";
            this.disp_end_time_tb.Size = new System.Drawing.Size(40, 19);
            this.disp_end_time_tb.TabIndex = 15;
            this.disp_end_time_tb.Text = "38";
            this.disp_end_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.disp_end_time_tb.TextChanged += new System.EventHandler(this.disp_end_time_tb_TextChanged);
            // 
            // disp_start_time_tb
            // 
            this.disp_start_time_tb.Location = new System.Drawing.Point(712, 32);
            this.disp_start_time_tb.Name = "disp_start_time_tb";
            this.disp_start_time_tb.Size = new System.Drawing.Size(40, 19);
            this.disp_start_time_tb.TabIndex = 14;
            this.disp_start_time_tb.Text = "35";
            this.disp_start_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.disp_start_time_tb.TextChanged += new System.EventHandler(this.disp_start_time_tb_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(656, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Magnify";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // display_magnify_tb
            // 
            this.display_magnify_tb.Location = new System.Drawing.Point(712, 80);
            this.display_magnify_tb.Name = "display_magnify_tb";
            this.display_magnify_tb.Size = new System.Drawing.Size(40, 19);
            this.display_magnify_tb.TabIndex = 18;
            this.display_magnify_tb.Text = "5";
            this.display_magnify_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.display_magnify_tb.TextChanged += new System.EventHandler(this.display_magnify_tb_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(664, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "Display Setting";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(520, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 16);
            this.label15.TabIndex = 35;
            this.label15.Text = "Iteration";
            this.label15.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // iteration_tb
            // 
            this.iteration_tb.Location = new System.Drawing.Point(592, 129);
            this.iteration_tb.Name = "iteration_tb";
            this.iteration_tb.Size = new System.Drawing.Size(40, 19);
            this.iteration_tb.TabIndex = 34;
            this.iteration_tb.Text = "40";
            this.iteration_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iteration_tb.TextChanged += new System.EventHandler(this.iteration_tb_TextChanged);
            // 
            // adaptation_restart_rb
            // 
            this.adaptation_restart_rb.Location = new System.Drawing.Point(8, 15);
            this.adaptation_restart_rb.Name = "adaptation_restart_rb";
            this.adaptation_restart_rb.Size = new System.Drawing.Size(70, 16);
            this.adaptation_restart_rb.TabIndex = 36;
            this.adaptation_restart_rb.Text = "Restart";
            this.adaptation_restart_rb.CheckedChanged += new System.EventHandler(this.adaptation_restart_rb_CheckedChanged);
            // 
            // adaptation_continue_rb
            // 
            this.adaptation_continue_rb.Checked = true;
            this.adaptation_continue_rb.Location = new System.Drawing.Point(8, 30);
            this.adaptation_continue_rb.Name = "adaptation_continue_rb";
            this.adaptation_continue_rb.Size = new System.Drawing.Size(70, 15);
            this.adaptation_continue_rb.TabIndex = 37;
            this.adaptation_continue_rb.TabStop = true;
            this.adaptation_continue_rb.Text = "Continue";
            // 
            // adaptation_gb
            // 
            this.adaptation_gb.Controls.Add(this.adaptation_restart_rb);
            this.adaptation_gb.Controls.Add(this.adaptation_continue_rb);
            this.adaptation_gb.Enabled = false;
            this.adaptation_gb.Location = new System.Drawing.Point(642, 197);
            this.adaptation_gb.Name = "adaptation_gb";
            this.adaptation_gb.Size = new System.Drawing.Size(80, 48);
            this.adaptation_gb.TabIndex = 38;
            this.adaptation_gb.TabStop = false;
            this.adaptation_gb.Text = "Adaptation";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(536, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 43;
            this.label11.Text = "FIR Tap";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // firtap_tb
            // 
            this.firtap_tb.Enabled = false;
            this.firtap_tb.Location = new System.Drawing.Point(592, 8);
            this.firtap_tb.Name = "firtap_tb";
            this.firtap_tb.Size = new System.Drawing.Size(40, 19);
            this.firtap_tb.TabIndex = 42;
            this.firtap_tb.Text = "256";
            this.firtap_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.firtap_tb.TextChanged += new System.EventHandler(this.firtap_tb_TextChanged);
            // 
            // tspamp_tb
            // 
            this.tspamp_tb.Location = new System.Drawing.Point(592, 104);
            this.tspamp_tb.Name = "tspamp_tb";
            this.tspamp_tb.Size = new System.Drawing.Size(40, 19);
            this.tspamp_tb.TabIndex = 53;
            this.tspamp_tb.Text = "0.99";
            this.tspamp_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tspamp_tb.TextChanged += new System.EventHandler(this.tspamp_tb_TextChanged);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(534, 104);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 16);
            this.label19.TabIndex = 54;
            this.label19.Text = "TSP amp";
            this.label19.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // left_rb
            // 
            this.left_rb.AutoSize = true;
            this.left_rb.Checked = true;
            this.left_rb.Location = new System.Drawing.Point(571, 162);
            this.left_rb.Name = "left_rb";
            this.left_rb.Size = new System.Drawing.Size(43, 16);
            this.left_rb.TabIndex = 55;
            this.left_rb.TabStop = true;
            this.left_rb.Text = "Left";
            this.left_rb.UseVisualStyleBackColor = true;
            // 
            // right_rb
            // 
            this.right_rb.AutoSize = true;
            this.right_rb.Location = new System.Drawing.Point(681, 162);
            this.right_rb.Name = "right_rb";
            this.right_rb.Size = new System.Drawing.Size(50, 16);
            this.right_rb.TabIndex = 56;
            this.right_rb.Text = "Right";
            this.right_rb.UseVisualStyleBackColor = true;
            // 
            // disp_update_btn
            // 
            this.disp_update_btn.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.disp_update_btn.Location = new System.Drawing.Point(704, 104);
            this.disp_update_btn.Name = "disp_update_btn";
            this.disp_update_btn.Size = new System.Drawing.Size(47, 19);
            this.disp_update_btn.TabIndex = 57;
            this.disp_update_btn.Text = "Update";
            this.disp_update_btn.UseVisualStyleBackColor = true;
            this.disp_update_btn.Click += new System.EventHandler(this.disp_update_btn_Click);
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(543, 257);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(71, 24);
            this.connect_btn.TabIndex = 58;
            this.connect_btn.Text = "Connect";
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // save_fir_btn
            // 
            this.save_fir_btn.Location = new System.Drawing.Point(642, 257);
            this.save_fir_btn.Name = "save_fir_btn";
            this.save_fir_btn.Size = new System.Drawing.Size(71, 24);
            this.save_fir_btn.TabIndex = 59;
            this.save_fir_btn.Text = "Save FIR";
            this.save_fir_btn.Click += new System.EventHandler(this.save_fir_btn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(543, 296);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(169, 20);
            this.progressBar1.Step = 4;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 60;
            // 
            // fir1_pulse_btn
            // 
            this.fir1_pulse_btn.Location = new System.Drawing.Point(554, 332);
            this.fir1_pulse_btn.Name = "fir1_pulse_btn";
            this.fir1_pulse_btn.Size = new System.Drawing.Size(150, 25);
            this.fir1_pulse_btn.TabIndex = 61;
            this.fir1_pulse_btn.Text = "FIR1:Data1, FIR2: Data2";
            this.fir1_pulse_btn.Click += new System.EventHandler(this.fir1_pulse_btn_Click);
            // 
            // etoe_test_btn
            // 
            this.etoe_test_btn.Location = new System.Drawing.Point(543, 376);
            this.etoe_test_btn.Name = "etoe_test_btn";
            this.etoe_test_btn.Size = new System.Drawing.Size(71, 24);
            this.etoe_test_btn.TabIndex = 62;
            this.etoe_test_btn.Text = "EtoE test";
            this.etoe_test_btn.Click += new System.EventHandler(this.etoe_test_btn_Click);
            // 
            // ProcessWave
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(760, 416);
            this.Controls.Add(this.etoe_test_btn);
            this.Controls.Add(this.fir1_pulse_btn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.save_fir_btn);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.disp_update_btn);
            this.Controls.Add(this.right_rb);
            this.Controls.Add(this.left_rb);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tspamp_tb);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.firtap_tb);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.adaptation_gb);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.iteration_tb);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.display_magnify_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.disp_end_time_tb);
            this.Controls.Add(this.disp_start_time_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mu_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delay_amp_tb);
            this.Controls.Add(this.delay_point_tb);
            this.Controls.Add(this.cursor_right_btn);
            this.Controls.Add(this.cursor_left_btn);
            this.Controls.Add(this.pointing_lbl);
            this.Controls.Add(this.waveformBox);
            this.Name = "ProcessWave";
            this.Text = "ProcessWave";
            ((System.ComponentModel.ISupportInitialize)(this.waveformBox)).EndInit();
            this.adaptation_gb.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

#if(DEBUG)
		public string working_dir = @"C:\Documents and Settings\ise\My Documents\Work\Source";
#else
		public string working_dir = System.Windows.Forms.Application.StartupPath;
#endif

		private void start_btn_Click(object sender, System.EventArgs e)
		{
            Start_Proc_2way();
        }


        enum ProcStatus { measure_sp1, target_start_sp1, target_end_sp1, measure_sp2, target_start_sp2, target_end_sp2, start_adaptation, stop_adaptation };
        private ProcStatus procStatus = ProcStatus.measure_sp1;
        private void Start_Proc_2way()
        {
            switch (procStatus)
			{
                case ProcStatus.measure_sp1:
                    firtap_tb.Enabled = false;
                    tspamp_tb.Enabled = false;
                    delay_point_tb.Enabled = true;
                    delay_amp_tb.Enabled = true;
                    mu_tb.Enabled = true;

                    //					withInv_cb.Enabled = false;
                   left_rb.Enabled = false;
                    right_rb.Enabled = false;

                    // 1. Measure impulse response for SP.1
                    //
                    procStatus = ProcStatus.target_start_sp1;
                    {
                        if (left_rb.Checked)
                        {
                            myasio.makeTSP(tsp_fftpoint, (int)sp1_ch.type, sp1_ch.btn_no, tspamp);
                            myasio.data_clear((int)sp2_ch.type, sp2_ch.btn_no);
                            SetDampFIR_Clear();
                            SetDampFIR_Pulse(SetDampFIRStatus.fir_left1, delay_point, (float)delay_amp);
                            SendDampFIR();
                            parent.PlayStart();
                            parent.BufDeconvolution(sp1_ch, mic_ch, work_ch1, 1.0, freq_lower_limit, freq_upper_limit);
                        }
                        else
                        {
                            myasio.makeTSP(tsp_fftpoint, (int)sp2_ch.type, sp2_ch.btn_no, tspamp);
                            myasio.data_clear((int)sp1_ch.type, sp1_ch.btn_no);
                            SetDampFIR_Clear();
                            SetDampFIR_Pulse(SetDampFIRStatus.fir_right1, delay_point, (float)delay_amp);
                            SendDampFIR();
                            parent.PlayStart();
                            parent.BufDeconvolution(sp2_ch, mic_ch, work_ch1, 1.0, freq_lower_limit, freq_upper_limit);
                        }
                        parent.SetButtonStatus_Signal(work_ch1); // h1
                        draw_ch = work_ch1; // h1
                        draw_ch1 = null;
                    }
                    grid_time = get_gridtime(end_time - start_time);
                    waveformBox.Invalidate();
                    start_btn.Text = "Target start point";
                    break;
                case ProcStatus.target_start_sp1:
                    target_start_point = pointing_pnt;
                    start_btn.Text = "Target end point";
                    pointing_lbl.Text = "Set target start point=" + target_start_point.ToString();
                    procStatus = ProcStatus.target_end_sp1;
                    break;
                case ProcStatus.target_end_sp1:
                    target_end_point = pointing_pnt;
                    start_btn.Text = "Measure SP2";
                    pointing_lbl.Text = "Set target point=" + target_start_point.ToString() + "-" + target_end_point.ToString();
                    cursor_left_btn.Enabled = false;
                    cursor_right_btn.Enabled = false;
                    pointing_pnt = -1;

                    
                    myasio.CopyData((int)work_ch1.type, work_ch1.btn_no, (int)work_ch2.type, work_ch2.btn_no);   // h1 ---> work_ch2
                    myasio.ClearBothSides((int)work_ch2.type, work_ch2.btn_no, target_start_point, target_end_point);
                    parent.SetButtonStatus_Signal(work_ch2);
                    draw_ch1 = work_ch2; // h1_d : ClearBothSides(h1, start, end)
                    procStatus = ProcStatus.measure_sp2;

                    myasio.CopyData((int)work_ch1.type, work_ch1.btn_no, (int)work_ch4.type, work_ch4.btn_no);   // h1 ---> work_ch4
                    myasio.SubData((int)work_ch2.type, work_ch2.btn_no, (int)work_ch4.type, work_ch4.btn_no); // work_ch4 == h1 - h1_d
                    parent.SetButtonStatus_Signal(work_ch4); // work_ch4 == h1 - h1_d
                    first_dd = myasio.GetPower_t((int)work_ch4.type, work_ch4.btn_no, fftpoint);
                    waveformBox.Invalidate();
                    break;
                case ProcStatus.measure_sp2:
                    // 2. Measure impulse response for SP.2
                    //
//                    procStatus = ProcStatus.target_start_sp2;
                    {
                        if (left_rb.Checked)
                        {
                            myasio.makeTSP(tsp_fftpoint, (int)sp1_ch.type, sp1_ch.btn_no, tspamp);
                            myasio.data_clear((int)sp2_ch.type, sp2_ch.btn_no);
                            SetDampFIR_Clear();
                            SetDampFIR_Pulse(SetDampFIRStatus.fir_left2, 0, 1f);
                            SendDampFIR();
                            parent.PlayStart();
                            parent.BufDeconvolution(sp1_ch, mic_ch, work_ch3, 1.0, freq_lower_limit, freq_upper_limit);
                        }
                        else
                        {
                            myasio.makeTSP(tsp_fftpoint, (int)sp2_ch.type, sp2_ch.btn_no, tspamp);
                            myasio.data_clear((int)sp1_ch.type, sp1_ch.btn_no);
                            SetDampFIR_Clear();
                            SetDampFIR_Pulse(SetDampFIRStatus.fir_right2, 0, 1f);
                            SendDampFIR();
                            parent.PlayStart();
                            parent.BufDeconvolution(sp2_ch, mic_ch, work_ch3, 1.0, freq_lower_limit, freq_upper_limit);
                        }
                        parent.SetButtonStatus_Signal(work_ch3);
                        // -- 2.8 Display wave
                        draw_ch = work_ch3; // h2
                        draw_ch1 = null;
                        grid_time = get_gridtime(end_time - start_time);
                        waveformBox.Invalidate();
//                        start_btn.Text = "Target start point";

                    }
                //    break;
                //case ProcStatus.target_start_sp2:
                //    target_start_point = pointing_pnt;
                //    start_btn.Text = "Target end point";
                //    pointing_lbl.Text = "Set target start point=" + target_start_point.ToString();
                //    procStatus = ProcStatus.target_end_sp2;
                //    break;
                //case ProcStatus.target_end_sp2:
                //    target_end_point = pointing_pnt;
                    start_btn.Text = "Start adaptation";
                //    pointing_lbl.Text = "Set target point=" + target_start_point.ToString() + "-" + target_end_point.ToString();
                //    cursor_left_btn.Enabled = false;
                //    cursor_right_btn.Enabled = false;
                //    pointing_pnt = -1;

                //    myasio.ClearBothSides(parent.find_asio_ch(work_ch3), target_start_point, target_end_point);
                //    parent.SetButtonStatus_Signal(work_ch3);
                //    draw_ch1 = parent.find_asio_ch(work_ch3); // h1_d : ClearBothSides(h1, start, end)
                    procStatus = ProcStatus.start_adaptation;

                    // -- Calc fir1: work_ch6(fir1) = work_ch4(h1-h1_d)/work_ch3(h2)
                    parent.BufDeconvolution(work_ch3, work_ch4, work_ch6, 1.0, freq_lower_limit, freq_upper_limit);
                    myasio.data_clear((int)work_ch5.type, work_ch5.btn_no);
                    myasio.SubData((int)work_ch6.type, work_ch6.btn_no, (int)work_ch5.type, work_ch5.btn_no); // work_ch5 = -work_ch6(fir1)
                    parent.SetButtonStatus_Signal(work_ch6); // err = h1 - h1_d
                    parent.SetButtonStatus_Signal(work_ch5); // fir
                    break;
                case ProcStatus.start_adaptation:
                    // 3. Start Adaptation
                    //
                    procStatus = ProcStatus.stop_adaptation;
                    start_btn.Text = "Now adapting ...";
                    go_adaptation = true;
                    start_btn.Enabled = false;
                    Thread st = new Thread(new ThreadStart(Start_Adaptation));
                    st.Name = "Start_Adaptation";
                    st.Start();
                    break;
                case ProcStatus.stop_adaptation:
                    // 4. Stop Adaptation
                    //
                    start_btn.Text = "Start adaptation";
                    adaptation_gb.Enabled = true;
                    go_adaptation = false;
                    start_btn.Enabled = true;
                    procStatus = ProcStatus.start_adaptation;
                    break;
			}
		}

        private bool go_adaptation = false;
        public int counter = 0;
        public double first_dd = 1.0;
        public double last_dd = 1.0;
        private void Start_Adaptation()
		{
			if(!go_adaptation) return;

			if(!adaptation_continue)
			{
				counter=0;
			}
            ChannelInfo fir_ch = work_ch5;
            draw_ch = work_ch1;   // h1
            draw_ch1 = work_ch4;
            double fir_max_val = myasio.GetMaxVal((int)fir_ch.type, fir_ch.btn_no);

//            for (int jj = 0; jj < iteration; jj++)
            {
                SetDampFIR_Clear();
                if (left_rb.Checked)
                {
                    myasio.makeTSP(tsp_fftpoint, (int)sp1_ch.type, sp1_ch.btn_no, tspamp);
                    myasio.data_clear((int)sp2_ch.type, sp2_ch.btn_no);
                    SetDampFIR_Pulse(SetDampFIRStatus.fir_left1, delay_point, (float)delay_amp);
                    SetDampFIR_Coef(SetDampFIRStatus.fir_left2, fir_ch);
                    SendDampFIR();
                    MinMaxFIR();
                    parent.PlayStart();
                    parent.BufDeconvolution(sp1_ch, mic_ch, work_ch1, 1.0, freq_lower_limit, freq_upper_limit);
                }
                else
                {
                    myasio.makeTSP(tsp_fftpoint, (int)sp2_ch.type, sp2_ch.btn_no, tspamp);
                    myasio.data_clear((int)sp1_ch.type, sp1_ch.btn_no);
                    SetDampFIR_Pulse(SetDampFIRStatus.fir_right1, delay_point, (float)delay_amp);
                    SetDampFIR_Coef(SetDampFIRStatus.fir_right2, fir_ch);
                    SendDampFIR();
                    MinMaxFIR();
                    parent.PlayStart();
                    parent.BufDeconvolution(sp2_ch, mic_ch, work_ch1, 1.0, freq_lower_limit, freq_upper_limit);
                }

                myasio.CopyData((int)work_ch1.type, work_ch1.btn_no, (int)work_ch4.type, work_ch4.btn_no);   // h1 ---> work_ch4
                myasio.SubData((int)work_ch2.type, work_ch2.btn_no, (int)work_ch4.type, work_ch4.btn_no); // work_ch4 - work_ch2(h1_d)
                parent.SetButtonStatus_Signal(work_ch4); // err = h1 - h1_d

                last_dd = myasio.GetPower_t((int)work_ch4.type, work_ch4.btn_no, fftpoint);
                double logd = 10.0 * Math.Log10(last_dd / first_dd);
                counter++;

                // -- 8. Update h1 (   work_ch5 = adapt(err, h2)
                myasio.SetMaxVal((int)fir_ch.type, fir_ch.btn_no);
                fir_max_val = myasio.GetMaxVal((int)fir_ch.type, fir_ch.btn_no);
//                myasio.AdaptiveFIR(parent.find_asio_ch(work_ch4), parent.find_asio_ch(work_ch3), fir_ch, mu, firtap);

                // -- Calc fir1: work_ch6(fir1) = work_ch4(h1-h1_d)/work_ch3(h2)
//                parent.BufDeconvolution(work_ch3, work_ch4, work_ch6, 1.0, freq_lower_limit, freq_upper_limit);
//                myasio.SubData(parent.find_asio_ch(work_ch6), parent.find_asio_ch(work_ch5)); // work_ch5 - work_ch6(fir1)

                myasio.SetMaxVal((int)fir_ch.type, fir_ch.btn_no);
                fir_max_val = myasio.GetMaxVal((int)fir_ch.type, fir_ch.btn_no);

                pointing_lbl.Text = counter.ToString() + ": Noise [dB]=" + logd.ToString("0.00") + ", fir max val=" + fir_max_val.ToString("0.00000");

                waveformBox.Refresh();
            }
            parent.SetAllButtonStatus();
            Start_Proc_2way();
        }

        private void SetDampFIR_Clear()
        {
            clear_coef(ref fir_l1_coef);
            clear_coef(ref fir_l2_coef);
            clear_coef(ref fir_r1_coef);
            clear_coef(ref fir_r2_coef);
        }

        private void clear_coef(ref float[] data)
        {
            for (int i = 0; i < firtap; i++)
            {
                data[i] = 0f;
            }
        }

        private void SetDampFIR_Coef(SetDampFIRStatus stts, ChannelInfo cinfo)
        {
            switch (stts)
            {
                case SetDampFIRStatus.fir_left1: get_data(ref fir_l1_coef, cinfo); break;
                case SetDampFIRStatus.fir_left2: get_data(ref fir_l2_coef, cinfo); break;
                case SetDampFIRStatus.fir_right1: get_data(ref fir_r1_coef, cinfo); break;
                case SetDampFIRStatus.fir_right2: get_data(ref fir_r2_coef, cinfo); break;
            }
        }

        private void get_data(ref float[] data, ChannelInfo cinfo)
        {
            int offset = 0;

            unsafe
            {
                fixed (float* dd = data)
                {
                    myasio.get_data((int)cinfo.type, cinfo.btn_no, dd, firtap, offset);
                }
            }
        }

		private int get_gridtime(int end_time)
		{
			int dd= (int)Math.Log10((double)end_time);
			int ddd = (int)Math.Pow(10.0, (double)(dd-1));
			if(ddd<=0) return end_time;
			while(end_time/ddd>10) ddd*=2;
			return (int)ddd;
		}
		private void waveformBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			System.Windows.Forms.PictureBox ss = (System.Windows.Forms.PictureBox)sender;
			Pen pen = new Pen(Color.Black, 2.0F);
			int x1,x2,y2;

			g.DrawRectangle(pen, yohaku_left, yohaku_top, wavebox_width, wavebox_height);

			Point [] points = {new Point(yohaku_left, wavebox_middle), new Point(yohaku_left+wavebox_width, wavebox_middle)};
			g.DrawLines(pen, points);


			StringFormat format = new StringFormat();
			format.Alignment=StringAlignment.Center;

			for(int grid=start_time+grid_time;grid<end_time;grid+=grid_time)
			{
				pen.DashStyle = DashStyle.Dot;
				x1 = wavebox_width*(grid-start_time)/(end_time-start_time);
				Point [] pnts = {new Point(x1+yohaku_left, yohaku_top), new Point(x1+yohaku_left, yohaku_top+wavebox_height)};
				g.DrawLines(pen, pnts);
				g.DrawString(grid.ToString(), this.Font, new SolidBrush(Color.Black), x1+yohaku_left, yohaku_top+wavebox_height+2, format);
			}
			format.Alignment=StringAlignment.Near;
			g.DrawString(start_time.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top+wavebox_height+2, format);
			format.Alignment=StringAlignment.Far;
			g.DrawString(end_time.ToString(), this.Font, new SolidBrush(Color.Black), yohaku_left+wavebox_width, yohaku_top+wavebox_height+2, format);

			g.DrawString("0", this.Font, new SolidBrush(Color.Black), yohaku_left, wavebox_middle-this.Font.Height/2, format);
			g.DrawString("1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top, format);
			g.DrawString("-1", this.Font, new SolidBrush(Color.Black), yohaku_left, yohaku_top+wavebox_height-this.Font.Height, format);
			g.DrawString("[ms]", this.Font, new SolidBrush(Color.Black), ss.Size.Width-40, ss.Size.Height-this.Font.Height, format);

			//
			//
			if(draw_ch==null) return;
			int start_pnt = start_time*parent.sample_freq / 1000;
			int end_pnt = end_time*parent.sample_freq / 1000;
			if(start_pnt<0) start_pnt=0;
			if(end_pnt>myasio.GetDataLength()) end_pnt=myasio.GetDataLength();

			double h_scale=(double)(wavebox_height/2)*magnify;
			double w_scale=(double)wavebox_width/(double)(end_pnt-start_pnt);
			DrawSignal(g, draw_ch, h_scale, w_scale, start_pnt, end_pnt, new Pen(Color.Blue, 1.0F));
			if(draw_ch1!=null) DrawSignal(g, draw_ch1, h_scale, w_scale, start_pnt, end_pnt, new Pen(Color.Red, 1.0F));
			if(pointing_pnt>=0)
			{
				x2=(int)(w_scale*(double)(pointing_pnt-start_pnt))+yohaku_left-1;
				y2=wavebox_middle-(int)(myasio.data(pointing_pnt, (int)draw_ch.type, draw_ch.btn_no) *h_scale);
				pen = new Pen(Color.Red, 1.0F);
				g.DrawEllipse(pen, x2, y2, 4, 4);
			}
		}
		private void DrawSignal(Graphics g, ChannelInfo cinfo, double h_scale, double w_scale, int start_pnt, int end_pnt, Pen pen)
		{
			pen.DashStyle = DashStyle.Solid;

			int x1, y1, x2, y2;
			Point[] points;
			if(w_scale>0.1)
			{
				x1=yohaku_left;
				y1=wavebox_middle-(int)(myasio.data(start_pnt, (int)cinfo.type, cinfo.btn_no) *h_scale);;

				for(int i=start_pnt+1;i<end_pnt;i++)
				{
					x2=(int)(w_scale*(double)(i-start_pnt))+yohaku_left;
					y2=wavebox_middle-(int)(myasio.data(i, (int)cinfo.type, cinfo.btn_no) *h_scale);
				
					points = new Point[]{new Point(x1, y1), new Point(x2, y2)};
					g.DrawLines(pen, points);
					x1=x2;
					y1=y2;
				}
			}
			else
			{
				int st_pnt,ed_pnt;
				double w_scale_1=(double)(end_pnt-start_pnt)/(double)wavebox_width,min,max;
				for(int i=0;i<wavebox_width;i++)
				{
					x1=yohaku_left+i;
					st_pnt=(int)((double)i*w_scale_1)+start_pnt;
					ed_pnt=(int)((double)(i+1)*w_scale_1)+start_pnt;
					unsafe
					{
						myasio.getminmax(st_pnt, ed_pnt, (int)cinfo.type, cinfo.btn_no, &min, &max);
					}
				
					y1=wavebox_middle-(int)(min*h_scale);
					y2=wavebox_middle-(int)(max*h_scale);
					points = new Point[]{new Point(x1, y1), new Point(x1, y2)};
					g.DrawLines(pen, points);
				}
			}
		}
		private int   dragFromX;
		private void waveformBox_MouseDown(object sender, MouseEventArgs e) 
		{
			dragFromX = e.X + waveformBox.Left;
		}
		private int pointing_pnt = -1;
		private int target_start_point = 0;
		private int target_end_point = 0;
		private void waveformBox_MouseUp(object sender, MouseEventArgs e) 
		{
			int start_pnt = start_time*parent.sample_freq/1000;
			int end_pnt = end_time*parent.sample_freq/1000;
			double h_scale=(double)(wavebox_height/2)*magnify;
			double w_scale=(double)wavebox_width/(double)(end_pnt-start_pnt);

			if(start_pnt<0) start_pnt=0;
			if(end_pnt>myasio.GetDataLength()) end_pnt=myasio.GetDataLength();
			double new_start_pnt = (double)(dragFromX-waveformBox.Left-yohaku_left)/w_scale+(double)start_pnt;
            double new_end_pnt = (double)(e.X - yohaku_left) / w_scale + (double)start_pnt;
			if( new_end_pnt>new_start_pnt+5.0 )
			{
				start_time = (int)(new_start_pnt*1000.0/parent.sample_freq+0.5);
				end_time = (int)(new_end_pnt*1000.0/parent.sample_freq+0.5);
				grid_time=get_gridtime(end_time-start_time);
			}
			else 
			{
                //pointing_lbl.Visible = true;
				pointing_pnt = (int)((double)(dragFromX-waveformBox.Left-yohaku_left)/w_scale)+start_pnt;
				double time = (double)pointing_pnt*1000.0/parent.sample_freq;
				double val = myasio.data(pointing_pnt, (int)draw_ch.type, draw_ch.btn_no);
				pointing_lbl.Text = "Point="+pointing_pnt.ToString()+" ("+time.ToString("0.00")+" ms) : Val="+val.ToString("0.0000");
				cursor_left_btn.Enabled = true;
				cursor_right_btn.Enabled = true;
			}
			waveformBox.Invalidate();
		}

		private void cursor_left_btn_Click(object sender, System.EventArgs e)
		{
			if(pointing_pnt>=0) pointing_pnt--;		
			double time = (double)pointing_pnt*1000.0/parent.sample_freq;
			double val = myasio.data(pointing_pnt, (int)draw_ch.type, draw_ch.btn_no);
			pointing_lbl.Text = "Point="+pointing_pnt.ToString()+" ("+time.ToString("0.00")+" ms) : Val="+val.ToString("0.0000");
			waveformBox.Invalidate();
		}

		private void cursor_right_btn_Click(object sender, System.EventArgs e)
		{
			int end_pnt = end_time*parent.sample_freq/1000;
			if(pointing_pnt<(end_pnt-1)) pointing_pnt++;		
			double time = (double)pointing_pnt*1000.0/parent.sample_freq;
			double val = myasio.data(pointing_pnt, (int)draw_ch.type, draw_ch.btn_no);
			pointing_lbl.Text = "Point="+pointing_pnt.ToString()+" ("+time.ToString("0.00")+" ms) : Val="+val.ToString("0.0000");
			waveformBox.Invalidate();
		}

		private void delay_point_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(delay_point_tb.Text=="") return;
			delay_point = System.Convert.ToInt32(delay_point_tb.Text);
		}

		private void delay_amp_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(delay_amp_tb.Text=="") return;
			delay_amp = System.Convert.ToDouble(delay_amp_tb.Text);
		}

		private void mu_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(mu_tb.Text=="") return;
			mu = System.Convert.ToDouble(mu_tb.Text);
		}

		private void disp_start_time_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(disp_start_time_tb.Text=="") return;
			start_time = System.Convert.ToInt32(disp_start_time_tb.Text);
        }

		private void disp_end_time_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(disp_end_time_tb.Text=="") return;
			end_time = System.Convert.ToInt32(disp_end_time_tb.Text);
        }

		private void display_magnify_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(display_magnify_tb.Text=="") return;
			magnify = System.Convert.ToDouble(display_magnify_tb.Text);
        }

		private void adaptation_restart_rb_CheckedChanged(object sender, System.EventArgs e)
		{
			adaptation_continue = adaptation_continue_rb.Checked;
		}

		private void firtap_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(firtap_tb.Text=="") return;
			firtap = System.Convert.ToInt32(firtap_tb.Text);
		}

		private void tspamp_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(tspamp_tb.Text=="") return;
			tspamp = System.Convert.ToDouble(tspamp_tb.Text);
        }

        private void disp_update_btn_Click(object sender, EventArgs e)
        {
            waveformBox.Invalidate();
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            connect_btn.Enabled = false;
            try
            {
                string ip_name = "PIC-UIO";
                int port = 50001;
                TcpClientA.Connect(ip_name, port);
                parent.Cnsl_msg("Connect!\n");
                MyStream = TcpClientA.GetStream();
                start_btn.Enabled = true;
            }
            catch (Exception ex)
            {
                parent.Cnsl_msg(ex.ToString());
                connect_btn.Enabled = true;
            }
        }

        private void SetDampFIR_Pulse(SetDampFIRStatus stts, int dpnt, float amp)
        {
            Int16[] FirCoef = new Int16[firtap * 4];
            if (stts == SetDampFIRStatus.fir_left1) fir_l1_coef[dpnt] = amp;
            if (stts == SetDampFIRStatus.fir_left2) fir_l2_coef[dpnt] = amp;
            if (stts == SetDampFIRStatus.fir_right1) fir_r1_coef[dpnt] = amp;
            if (stts == SetDampFIRStatus.fir_right2) fir_r2_coef[dpnt] = amp;
        }

        private void WriteCoef(StreamWriter sw, float[] coef)
        {
            string ss = "";
            for (int j = 0; j < 256; ++j)
            {
                Int16 ii = (Int16)(coef[j] * 32767.0);
                ss = ss + ii.ToString() + ",";
                if ((j+1) % 16 == 0)
                {
                    sw.Write(ss + "\r\n");
                    ss = "";
                }
            }
            sw.Write(ss + "\r\n");
        }

        private void TextDampFIR(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding("Shift_JIS"));

            if (left_rb.Checked)
            {
                WriteCoef(streamWriter, fir_l1_coef);
                WriteCoef(streamWriter, fir_l2_coef);
            }
            else
            {
                WriteCoef(streamWriter, fir_r1_coef);
                WriteCoef(streamWriter, fir_r2_coef);
            }
            streamWriter.Close();
        }

        private void SendDampFIR()
        {
            int kkk = 0;
            for (int j = 0; j < 64; ++j)
            {
                FirCoef[j + 192 + kkk] = (Int16)(fir_l1_coef[j] * 32767.0);
                FirCoef[j + 128 + kkk] = (Int16)(fir_l1_coef[j + 64] * 32767.0);
                FirCoef[j + 64 + kkk] = (Int16)(fir_l1_coef[j + 128] * 32767.0);
                FirCoef[j + kkk] = (Int16)(fir_l1_coef[j + 192] * 32767.0);
            }
            kkk = 256;
            for (int j = 0; j < 64; ++j)
            {
                FirCoef[j + 192 + kkk] = (Int16)(fir_l2_coef[j] * 32767.0);
                FirCoef[j + 128 + kkk] = (Int16)(fir_l2_coef[j + 64] * 32767.0);
                FirCoef[j + 64 + kkk] = (Int16)(fir_l2_coef[j + 128] * 32767.0);
                FirCoef[j + kkk] = (Int16)(fir_l2_coef[j + 192] * 32767.0);
            }
            kkk = 512;
            for (int j = 0; j < 64; ++j)
            {
                FirCoef[j + 192 + kkk] = (Int16)(fir_r1_coef[j] * 32767.0);
                FirCoef[j + 128 + kkk] = (Int16)(fir_r1_coef[j + 64] * 32767.0);
                FirCoef[j + 64 + kkk] = (Int16)(fir_r1_coef[j + 128] * 32767.0);
                FirCoef[j + kkk] = (Int16)(fir_r1_coef[j + 192] * 32767.0);
            }
            kkk = 768;
            for (int j = 0; j < 64; ++j)
            {
                FirCoef[j + 192 + kkk] = (Int16)(fir_r2_coef[j] * 32767.0);
                FirCoef[j + 128 + kkk] = (Int16)(fir_r2_coef[j + 64] * 32767.0);
                FirCoef[j + 64 + kkk] = (Int16)(fir_r2_coef[j + 128] * 32767.0);
                FirCoef[j + kkk] = (Int16)(fir_r2_coef[j + 192] * 32767.0);
            }

            byte[] FIRMode_ON = System.Text.Encoding.ASCII.GetBytes("SF1");

            FIRMode_ON[2] = 1024 / 16;
            MyStream.Write(FIRMode_ON, 0, FIRMode_ON.Length);
            MyStream.Read(RcvBuf, 0, RcvBuf.Length);
            System.Threading.Thread.Sleep(20);
            if (RcvBuf[2] != 1)
            {
                parent.Cnsl_msg("Error: FIR Mode");
                return;
            }

            progressBar1.Maximum = 1024 / 16 - 1;
            for (int i = 0; i < 1024 / 16; i++)
            {
                progressBar1.Value = i;
                int ii = FIRTransfer(FirCoef, i * 16, 16);
                if( ii>0 )
                {
                    parent.Cnsl_msg("FIRTransEnable trial="+ii.ToString()+"\n");
                }
                System.Threading.Thread.Sleep(10);
            }
            parent.Cnsl_msg("FIR coef transfer\n");
        }

        private void MinMaxFIR()
        {
            int kkk = 0;
            fir1_max_val = fir1_min_val = FirCoef[kkk];
            fir1_max_pnt = fir1_min_pnt = kkk;
            for (int j = kkk + 1; j < kkk + 256; ++j)
            {
                if (FirCoef[j] > fir1_max_val)
                {
                    fir1_max_pnt = j;
                    fir1_max_val = FirCoef[j];
                }
                if (FirCoef[j] < fir1_min_val)
                {
                    fir1_min_pnt = j;
                    fir1_min_val = FirCoef[j];
                }
            }
            parent.Cnsl_msg("FIR1 max:"+fir1_max_val.ToString()+","+fir1_max_pnt.ToString()+", min:"+fir1_min_val.ToString()+","+fir1_min_pnt.ToString()+", ");

            kkk = 256;
            fir2_max_val = fir2_min_val = FirCoef[kkk];
            fir2_max_pnt = fir2_min_pnt = kkk;
            for (int j = kkk + 1; j < kkk + 256; ++j)
            {
                if (FirCoef[j] > fir2_max_val)
                {
                    fir2_max_pnt = j;
                    fir2_max_val = FirCoef[j];
                }
                if (FirCoef[j] < fir2_min_val)
                {
                    fir2_min_pnt = j;
                    fir2_min_val = FirCoef[j];
                }
            }
            parent.Cnsl_msg("FIR2 max:" + fir2_max_val.ToString() + "," + fir2_max_pnt.ToString() + ", min:" + fir2_min_val.ToString() + "," + fir2_min_pnt.ToString() + "\n");

            kkk = 512;
            fir3_max_val = fir3_min_val = FirCoef[kkk];
            fir3_max_pnt = fir3_min_pnt = kkk;
            for (int j = kkk + 1; j < kkk + 256; ++j)
            {
                if (FirCoef[j] > fir3_max_val)
                {
                    fir3_max_pnt = j;
                    fir3_max_val = FirCoef[j];
                }
                if (FirCoef[j] < fir3_min_val)
                {
                    fir3_min_pnt = j;
                    fir3_min_val = FirCoef[j];
                }
            }
            parent.Cnsl_msg("FIR3 max:" + fir3_max_val.ToString() + "," + fir3_max_pnt.ToString() + ", min:" + fir3_min_val.ToString() + "," + fir3_min_pnt.ToString() + ", ");

            kkk = 768;
            fir4_max_val = fir4_min_val = FirCoef[kkk];
            fir4_max_pnt = fir4_min_pnt = kkk;
            for (int j = kkk + 1; j < kkk + 256; ++j)
            {
                if (FirCoef[j] > fir4_max_val)
                {
                    fir4_max_pnt = j;
                    fir4_max_val = FirCoef[j];
                }
                if (FirCoef[j] < fir4_min_val)
                {
                    fir4_min_pnt = j;
                    fir4_min_val = FirCoef[j];
                }
            }
            parent.Cnsl_msg("FIR4 max:" + fir4_max_val.ToString() + "," + fir4_max_pnt.ToString() + ", min:" + fir4_min_val.ToString() + "," + fir4_min_pnt.ToString() + "\n");
        }

        private int FIRTransfer(Int16[] FirCoef, int start, int leng)
        {
            int trial = 0;
            int byte_size = leng * 2 + 3;
            byte[] FirCoef_b = new byte[byte_size];
            byte[] command = System.Text.Encoding.ASCII.GetBytes("SG");
            FirCoef_b[0] = command[0];
            FirCoef_b[1] = command[1];
            FirCoef_b[2] = (byte)(start / 16);
            for (int j = 0; j < leng; ++j)
            {
                byte[] b = BitConverter.GetBytes(FirCoef[start + j]);
                FirCoef_b[j * 2 + 3] = b[1];
                FirCoef_b[j * 2 + 4] = b[0];
            }
            try
            {
                while (FIRTransEnable() > 0 && trial < 10) trial++;
                MyStream.Write(FirCoef_b, 0, byte_size);
                System.Threading.Thread.Sleep(10);
                MyStream.Read(RcvBuf, 0, RcvBuf.Length);
                System.Threading.Thread.Sleep(40);
            }
            catch (Exception ex)
            {
                parent.Cnsl_msg(ex.ToString());
            }
            return trial;
        }

        private int FIRTransEnable()
        {
            byte[] FirCoef_b = new byte[3];
            byte[] command = System.Text.Encoding.ASCII.GetBytes("SH");
            FirCoef_b[0] = command[0];
            FirCoef_b[1] = command[1];
            try
            {
                MyStream.Write(FirCoef_b, 0, 3);
                System.Threading.Thread.Sleep(10);
                MyStream.Read(RcvBuf, 0, RcvBuf.Length);
                System.Threading.Thread.Sleep(40);
            }
            catch (Exception ex)
            {
                parent.Cnsl_msg(ex.ToString());
            }
            return RcvBuf[2];
        }

		private void iteration_tb_TextChanged(object sender, System.EventArgs e)
		{
			if(iteration_tb.Text=="") return;
			iteration = System.Convert.ToInt32(iteration_tb.Text);
        }

        private void save_fir_btn_Click(object sender, EventArgs e)
        {
            if (parent.fd.SaveFileDialog("Text file name", "txt") == false)
            {
                parent.Cnsl_msg("Canceled.\r\n");
                return;
            }
            string selected_folder = Path.GetDirectoryName(parent.fd.filename);
            parent.Cnsl_msg("Work folder = " + selected_folder + "\n");
            parent.work_folder = selected_folder;

            TextDampFIR(parent.fd.filename);
            parent.Cnsl_msg("Output TXT file name: " + parent.fd.filename + "\r\n");
        }

        private void fir1_pulse_btn_Click(object sender, EventArgs e)
        {
            if (parent.data_ch[1] == null || parent.data_ch[2] == null)
            {
                parent.Cnsl_msg("Error: Set both data1 and data2\r\n");
                return;
            }
            ChannelInfo fir1_ch = parent.data_ch[1];
            ChannelInfo fir2_ch = parent.data_ch[2];
            SetDampFIR_Clear();
            if (left_rb.Checked)
            {
                SetDampFIR_Coef(SetDampFIRStatus.fir_left1, fir1_ch);
                SetDampFIR_Coef(SetDampFIRStatus.fir_left2, fir2_ch);
            }
            else
            {
                SetDampFIR_Coef(SetDampFIRStatus.fir_right1, fir1_ch);
                SetDampFIR_Coef(SetDampFIRStatus.fir_right2, fir2_ch);
            }
            SendDampFIR();
        }

        struct aa
        {
            public int point;
            public double value;

            public aa(int pnt, double val)
            {
                point = pnt;
                value = val;
            }
        };

        struct bb
        {
            public int point;
            public double count;

            public bb(int pnt, int cnt)
            {
                point = pnt;
                count = cnt;
            }
        };
        private void etoe_test_btn_Click(object sender, EventArgs e)
        {
            int iteration = 100;
            int[] max_val = new int[iteration];

            myasio.makeTSP(tsp_fftpoint, (int)sp1_ch.type, sp1_ch.btn_no, tspamp);
            parent.SetButtonStatus_Signal(sp1_ch);
            ArrayList data = new ArrayList();
            for (int i = 0; i < iteration; i++)
            {
                parent.PlayStart();
                parent.BufDeconvolution(sp1_ch, mic_ch, work_ch1, 1.0, freq_lower_limit, freq_upper_limit);
                myasio.SetMaxVal((int)work_ch1.type, work_ch1.btn_no);
                int id = myasio.GetMaxID((int)work_ch1.type, work_ch1.btn_no);
                double val = myasio.GetMaxVal((int)work_ch1.type, work_ch1.btn_no);
                data.Add(new aa(id, val));
                parent.Cnsl_msg("etoe test : " + i.ToString() + ": index=" + id.ToString() + ": val=" + val.ToString() + "\r\n");
            }
            parent.SetButtonStatus_Signal(mic_ch);
            parent.SetButtonStatus_Signal(work_ch1);
            ArrayList ddd = new ArrayList();
            foreach(aa aa1 in data)
            {
                bool have_data = false;
                for(int i=0;i<ddd.Count;i++)
                {
                    bb bb1 = (bb)ddd[i];
                    if (bb1.point == aa1.point)
                    {
                        bb1.count++;
                        have_data = true;
                    }
                }
                if(have_data == false)
                {
                    ddd.Add(new bb(aa1.point, 1));
                }
            }
            if(ddd.Count == 1)
                parent.Cnsl_msg("etoe test OK\r\n");
            else
                parent.Cnsl_msg("etoe test NG : " + ddd.Count.ToString() + "\r\n");

        }
    }
}
