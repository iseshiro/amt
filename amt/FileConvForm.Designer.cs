namespace amt
{
    partial class FileConvForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calc_start_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.source_path_check_btn = new System.Windows.Forms.Button();
            this.inverse_path_check_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.output_path_set_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.inch_cnt_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.outch_cnt_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.max_val_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.filt_size_lbl = new System.Windows.Forms.Label();
            this.src_path_ok_lbl = new System.Windows.Forms.Label();
            this.inv_filt_ok_lbl = new System.Windows.Forms.Label();
            this.out_path_ok_lbl = new System.Windows.Forms.Label();
            this.pref_size_cb = new System.Windows.Forms.ComboBox();
            this.keep_mid_file_cb = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.source_path_tb = new System.Windows.Forms.TextBox();
            this.source_path_set_btn = new System.Windows.Forms.Button();
            this.filter_path_tb = new System.Windows.Forms.TextBox();
            this.output_path_tb = new System.Windows.Forms.TextBox();
            this.inverse_path_set_btn = new System.Windows.Forms.Button();
            this.fft_level_ud = new System.Windows.Forms.NumericUpDown();
            this.fft_level_lbl = new System.Windows.Forms.Label();
            this.is_inv_cb = new System.Windows.Forms.CheckBox();
            this.source_calib_btn = new System.Windows.Forms.Button();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.bw_check = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).BeginInit();
            this.SuspendLayout();
            // 
            // calc_start_btn
            // 
            this.calc_start_btn.Enabled = false;
            this.calc_start_btn.Location = new System.Drawing.Point(396, 288);
            this.calc_start_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.calc_start_btn.Name = "calc_start_btn";
            this.calc_start_btn.Size = new System.Drawing.Size(153, 34);
            this.calc_start_btn.TabIndex = 113;
            this.calc_start_btn.Text = "Calc start";
            this.calc_start_btn.UseVisualStyleBackColor = true;
            this.calc_start_btn.Click += new System.EventHandler(this.Calc_start_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 121;
            this.label1.Text = "Source path";
            // 
            // source_path_check_btn
            // 
            this.source_path_check_btn.Location = new System.Drawing.Point(153, 76);
            this.source_path_check_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.source_path_check_btn.Name = "source_path_check_btn";
            this.source_path_check_btn.Size = new System.Drawing.Size(87, 34);
            this.source_path_check_btn.TabIndex = 123;
            this.source_path_check_btn.Text = "Check";
            this.source_path_check_btn.UseVisualStyleBackColor = true;
            this.source_path_check_btn.Click += new System.EventHandler(this.source_path_check_btn_Click);
            // 
            // inverse_path_check_btn
            // 
            this.inverse_path_check_btn.Location = new System.Drawing.Point(153, 183);
            this.inverse_path_check_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.inverse_path_check_btn.Name = "inverse_path_check_btn";
            this.inverse_path_check_btn.Size = new System.Drawing.Size(87, 34);
            this.inverse_path_check_btn.TabIndex = 126;
            this.inverse_path_check_btn.Text = "Check";
            this.inverse_path_check_btn.UseVisualStyleBackColor = true;
            this.inverse_path_check_btn.Click += new System.EventHandler(this.inverse_path_check_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(13, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 124;
            this.label2.Text = "Filter path";
            // 
            // output_path_set_btn
            // 
            this.output_path_set_btn.Location = new System.Drawing.Point(153, 292);
            this.output_path_set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.output_path_set_btn.Name = "output_path_set_btn";
            this.output_path_set_btn.Size = new System.Drawing.Size(87, 34);
            this.output_path_set_btn.TabIndex = 129;
            this.output_path_set_btn.Text = "Set";
            this.output_path_set_btn.UseVisualStyleBackColor = true;
            this.output_path_set_btn.Click += new System.EventHandler(this.Output_path_check_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(10, 226);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 127;
            this.label3.Text = "Output path";
            // 
            // inch_cnt_tb
            // 
            this.inch_cnt_tb.Location = new System.Drawing.Point(530, 14);
            this.inch_cnt_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.inch_cnt_tb.Name = "inch_cnt_tb";
            this.inch_cnt_tb.Size = new System.Drawing.Size(72, 25);
            this.inch_cnt_tb.TabIndex = 131;
            this.inch_cnt_tb.Text = "3";
            this.inch_cnt_tb.TextChanged += new System.EventHandler(this.Mic_count_tb_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(386, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 130;
            this.label4.Text = "Input ch. count";
            // 
            // outch_cnt_tb
            // 
            this.outch_cnt_tb.Location = new System.Drawing.Point(530, 50);
            this.outch_cnt_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.outch_cnt_tb.Name = "outch_cnt_tb";
            this.outch_cnt_tb.Size = new System.Drawing.Size(72, 25);
            this.outch_cnt_tb.TabIndex = 133;
            this.outch_cnt_tb.Text = "4";
            this.outch_cnt_tb.TextChanged += new System.EventHandler(this.Sp_count_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(381, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 132;
            this.label5.Text = "Ouput ch count";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(418, 128);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.TabIndex = 139;
            this.label11.Text = "Frame size";
            // 
            // max_val_tb
            // 
            this.max_val_tb.Enabled = false;
            this.max_val_tb.Location = new System.Drawing.Point(533, 190);
            this.max_val_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.max_val_tb.Name = "max_val_tb";
            this.max_val_tb.Size = new System.Drawing.Size(72, 25);
            this.max_val_tb.TabIndex = 141;
            this.max_val_tb.Text = "0.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(420, 194);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 142;
            this.label6.Text = "Max value";
            // 
            // filt_size_lbl
            // 
            this.filt_size_lbl.AutoSize = true;
            this.filt_size_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.filt_size_lbl.Location = new System.Drawing.Point(343, 162);
            this.filt_size_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.filt_size_lbl.Name = "filt_size_lbl";
            this.filt_size_lbl.Size = new System.Drawing.Size(170, 20);
            this.filt_size_lbl.TabIndex = 143;
            this.filt_size_lbl.Text = "Filter length = 4096";
            // 
            // src_path_ok_lbl
            // 
            this.src_path_ok_lbl.AutoSize = true;
            this.src_path_ok_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.src_path_ok_lbl.Location = new System.Drawing.Point(250, 82);
            this.src_path_ok_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.src_path_ok_lbl.Name = "src_path_ok_lbl";
            this.src_path_ok_lbl.Size = new System.Drawing.Size(35, 20);
            this.src_path_ok_lbl.TabIndex = 144;
            this.src_path_ok_lbl.Text = "OK";
            this.src_path_ok_lbl.Visible = false;
            // 
            // inv_filt_ok_lbl
            // 
            this.inv_filt_ok_lbl.AutoSize = true;
            this.inv_filt_ok_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.inv_filt_ok_lbl.Location = new System.Drawing.Point(250, 189);
            this.inv_filt_ok_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.inv_filt_ok_lbl.Name = "inv_filt_ok_lbl";
            this.inv_filt_ok_lbl.Size = new System.Drawing.Size(35, 20);
            this.inv_filt_ok_lbl.TabIndex = 145;
            this.inv_filt_ok_lbl.Text = "OK";
            this.inv_filt_ok_lbl.Visible = false;
            // 
            // out_path_ok_lbl
            // 
            this.out_path_ok_lbl.AutoSize = true;
            this.out_path_ok_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.out_path_ok_lbl.Location = new System.Drawing.Point(250, 298);
            this.out_path_ok_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.out_path_ok_lbl.Name = "out_path_ok_lbl";
            this.out_path_ok_lbl.Size = new System.Drawing.Size(35, 20);
            this.out_path_ok_lbl.TabIndex = 146;
            this.out_path_ok_lbl.Text = "OK";
            this.out_path_ok_lbl.Visible = false;
            // 
            // pref_size_cb
            // 
            this.pref_size_cb.FormattingEnabled = true;
            this.pref_size_cb.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
            this.pref_size_cb.Location = new System.Drawing.Point(528, 127);
            this.pref_size_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pref_size_cb.Name = "pref_size_cb";
            this.pref_size_cb.Size = new System.Drawing.Size(99, 26);
            this.pref_size_cb.TabIndex = 147;
            this.pref_size_cb.SelectedIndexChanged += new System.EventHandler(this.pref_size_cb_SelectedIndexChanged);
            // 
            // keep_mid_file_cb
            // 
            this.keep_mid_file_cb.AutoSize = true;
            this.keep_mid_file_cb.Location = new System.Drawing.Point(423, 234);
            this.keep_mid_file_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.keep_mid_file_cb.Name = "keep_mid_file_cb";
            this.keep_mid_file_cb.Size = new System.Drawing.Size(135, 22);
            this.keep_mid_file_cb.TabIndex = 148;
            this.keep_mid_file_cb.Text = "Keep Mid File";
            this.keep_mid_file_cb.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 336);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            this.statusStrip1.Size = new System.Drawing.Size(647, 32);
            this.statusStrip1.TabIndex = 149;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(161, 25);
            this.toolStripStatusLabel1.Text = "Check for calc start";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(333, 24);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // source_path_tb
            // 
            this.source_path_tb.Location = new System.Drawing.Point(13, 38);
            this.source_path_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.source_path_tb.Name = "source_path_tb";
            this.source_path_tb.Size = new System.Drawing.Size(279, 25);
            this.source_path_tb.TabIndex = 150;
            this.source_path_tb.Text = "..\\..\\..\\..\\check\\sig_mic_1.wav";
            // 
            // source_path_set_btn
            // 
            this.source_path_set_btn.Location = new System.Drawing.Point(38, 76);
            this.source_path_set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.source_path_set_btn.Name = "source_path_set_btn";
            this.source_path_set_btn.Size = new System.Drawing.Size(87, 34);
            this.source_path_set_btn.TabIndex = 151;
            this.source_path_set_btn.Text = "Set";
            this.source_path_set_btn.UseVisualStyleBackColor = true;
            this.source_path_set_btn.Click += new System.EventHandler(this.source_path_set_btn_Click);
            // 
            // filter_path_tb
            // 
            this.filter_path_tb.Location = new System.Drawing.Point(13, 147);
            this.filter_path_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.filter_path_tb.Name = "filter_path_tb";
            this.filter_path_tb.Size = new System.Drawing.Size(279, 25);
            this.filter_path_tb.TabIndex = 152;
            this.filter_path_tb.Text = "..\\..\\..\\..\\check\\inv_s1_m1.double";
            // 
            // output_path_tb
            // 
            this.output_path_tb.Location = new System.Drawing.Point(13, 255);
            this.output_path_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.output_path_tb.Name = "output_path_tb";
            this.output_path_tb.Size = new System.Drawing.Size(279, 25);
            this.output_path_tb.TabIndex = 153;
            this.output_path_tb.Text = "..\\..\\..\\..\\check\\out_1.wav";
            // 
            // inverse_path_set_btn
            // 
            this.inverse_path_set_btn.Location = new System.Drawing.Point(38, 183);
            this.inverse_path_set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.inverse_path_set_btn.Name = "inverse_path_set_btn";
            this.inverse_path_set_btn.Size = new System.Drawing.Size(87, 34);
            this.inverse_path_set_btn.TabIndex = 154;
            this.inverse_path_set_btn.Text = "Set";
            this.inverse_path_set_btn.UseVisualStyleBackColor = true;
            this.inverse_path_set_btn.Click += new System.EventHandler(this.inverse_path_set_btn_Click);
            // 
            // fft_level_ud
            // 
            this.fft_level_ud.Location = new System.Drawing.Point(528, 88);
            this.fft_level_ud.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.fft_level_ud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fft_level_ud.Name = "fft_level_ud";
            this.fft_level_ud.Size = new System.Drawing.Size(75, 25);
            this.fft_level_ud.TabIndex = 156;
            this.fft_level_ud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fft_level_ud.ValueChanged += new System.EventHandler(this.fft_level_ud_ValueChanged);
            // 
            // fft_level_lbl
            // 
            this.fft_level_lbl.AutoSize = true;
            this.fft_level_lbl.Location = new System.Drawing.Point(423, 92);
            this.fft_level_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.fft_level_lbl.Name = "fft_level_lbl";
            this.fft_level_lbl.Size = new System.Drawing.Size(85, 18);
            this.fft_level_lbl.TabIndex = 155;
            this.fft_level_lbl.Text = "FFT Level";
            // 
            // is_inv_cb
            // 
            this.is_inv_cb.AutoSize = true;
            this.is_inv_cb.Location = new System.Drawing.Point(153, 123);
            this.is_inv_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.is_inv_cb.Name = "is_inv_cb";
            this.is_inv_cb.Size = new System.Drawing.Size(127, 22);
            this.is_inv_cb.TabIndex = 157;
            this.is_inv_cb.Text = "Inverse filter";
            this.is_inv_cb.UseVisualStyleBackColor = true;
            this.is_inv_cb.CheckedChanged += new System.EventHandler(this.is_inv_cb_CheckedChanged);
            // 
            // source_calib_btn
            // 
            this.source_calib_btn.Location = new System.Drawing.Point(295, 76);
            this.source_calib_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.source_calib_btn.Name = "source_calib_btn";
            this.source_calib_btn.Size = new System.Drawing.Size(87, 34);
            this.source_calib_btn.TabIndex = 158;
            this.source_calib_btn.Text = "Calib";
            this.source_calib_btn.UseVisualStyleBackColor = true;
            this.source_calib_btn.Click += new System.EventHandler(this.source_calib_btn_Click);
            // 
            // bw
            // 
            this.bw.WorkerReportsProgress = true;
            this.bw.WorkerSupportsCancellation = true;
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // bw_check
            // 
            this.bw_check.WorkerReportsProgress = true;
            this.bw_check.WorkerSupportsCancellation = true;
            this.bw_check.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_Check_DoWork);
            this.bw_check.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_check_ProgressChanged);
            this.bw_check.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_check_RunWorkerCompleted);
            // 
            // FileConvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 368);
            this.Controls.Add(this.source_calib_btn);
            this.Controls.Add(this.fft_level_ud);
            this.Controls.Add(this.fft_level_lbl);
            this.Controls.Add(this.inverse_path_set_btn);
            this.Controls.Add(this.output_path_tb);
            this.Controls.Add(this.filter_path_tb);
            this.Controls.Add(this.source_path_set_btn);
            this.Controls.Add(this.source_path_tb);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.keep_mid_file_cb);
            this.Controls.Add(this.pref_size_cb);
            this.Controls.Add(this.out_path_ok_lbl);
            this.Controls.Add(this.inv_filt_ok_lbl);
            this.Controls.Add(this.src_path_ok_lbl);
            this.Controls.Add(this.filt_size_lbl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.max_val_tb);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.outch_cnt_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inch_cnt_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.output_path_set_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inverse_path_check_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.source_path_check_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calc_start_btn);
            this.Controls.Add(this.is_inv_cb);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FileConvForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Convolution";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calc_start_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button output_path_set_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inch_cnt_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox outch_cnt_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox max_val_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label filt_size_lbl;
        private System.Windows.Forms.Label src_path_ok_lbl;
        private System.Windows.Forms.Label inv_filt_ok_lbl;
        private System.Windows.Forms.Label out_path_ok_lbl;
        private System.Windows.Forms.ComboBox pref_size_cb;
        private System.Windows.Forms.CheckBox keep_mid_file_cb;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button source_path_set_btn;
        private System.Windows.Forms.Button inverse_path_set_btn;
        private System.Windows.Forms.Button inverse_path_check_btn;
        private System.Windows.Forms.NumericUpDown fft_level_ud;
        private System.Windows.Forms.Label fft_level_lbl;
        private System.Windows.Forms.CheckBox is_inv_cb;
        public System.Windows.Forms.TextBox source_path_tb;
        public System.Windows.Forms.TextBox filter_path_tb;
        public System.Windows.Forms.TextBox output_path_tb;
        public System.Windows.Forms.Button source_path_check_btn;
        private System.Windows.Forms.Button source_calib_btn;
        private System.ComponentModel.BackgroundWorker bw;
        private System.ComponentModel.BackgroundWorker bw_check;
    }
}