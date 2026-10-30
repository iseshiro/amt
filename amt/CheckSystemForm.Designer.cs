namespace amt
{
    partial class CheckSystemForm
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
            this.input_set_dleng_btn = new System.Windows.Forms.Button();
            this.input_dleng_lbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hilo_cb = new System.Windows.Forms.CheckBox();
            this.tsp_fftpnt_tb = new System.Windows.Forms.TextBox();
            this.tsp_shift_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_denom_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_num_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.amp_tb = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.input_end_tb = new System.Windows.Forms.TextBox();
            this.input_start_tb = new System.Windows.Forms.TextBox();
            this.output_end_tb = new System.Windows.Forms.TextBox();
            this.output_start_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.input_test_btn = new System.Windows.Forms.Button();
            this.input_test_tb = new System.Windows.Forms.TextBox();
            this.output_test_tb = new System.Windows.Forms.TextBox();
            this.output_test_btn = new System.Windows.Forms.Button();
            this.check_pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.output_dleng_lbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.output_set_dleng_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.prev_ch_btn = new System.Windows.Forms.Button();
            this.next_ch_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_pictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // input_set_dleng_btn
            // 
            this.input_set_dleng_btn.Location = new System.Drawing.Point(149, 18);
            this.input_set_dleng_btn.Name = "input_set_dleng_btn";
            this.input_set_dleng_btn.Size = new System.Drawing.Size(53, 24);
            this.input_set_dleng_btn.TabIndex = 50;
            this.input_set_dleng_btn.Text = "Set";
            this.input_set_dleng_btn.Click += new System.EventHandler(this.chg_dleng_btn_Click);
            // 
            // input_dleng_lbl
            // 
            this.input_dleng_lbl.Font = new System.Drawing.Font("ＭＳ 明朝", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.input_dleng_lbl.Location = new System.Drawing.Point(77, 22);
            this.input_dleng_lbl.Name = "input_dleng_lbl";
            this.input_dleng_lbl.Size = new System.Drawing.Size(66, 18);
            this.input_dleng_lbl.TabIndex = 35;
            this.input_dleng_lbl.Text = "131072";
            this.input_dleng_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hilo_cb);
            this.groupBox1.Controls.Add(this.tsp_fftpnt_tb);
            this.groupBox1.Controls.Add(this.tsp_shift_tb);
            this.groupBox1.Controls.Add(this.tsp_phase_denom_tb);
            this.groupBox1.Controls.Add(this.tsp_phase_num_tb);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.amp_tb);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(21, 352);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 78);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TSP signal";
            // 
            // hilo_cb
            // 
            this.hilo_cb.AutoSize = true;
            this.hilo_cb.Location = new System.Drawing.Point(126, 55);
            this.hilo_cb.Name = "hilo_cb";
            this.hilo_cb.Size = new System.Drawing.Size(99, 16);
            this.hilo_cb.TabIndex = 42;
            this.hilo_cb.Text = "High ---> Low";
            this.hilo_cb.UseVisualStyleBackColor = true;
            // 
            // tsp_fftpnt_tb
            // 
            this.tsp_fftpnt_tb.Enabled = false;
            this.tsp_fftpnt_tb.Location = new System.Drawing.Point(8, 30);
            this.tsp_fftpnt_tb.Name = "tsp_fftpnt_tb";
            this.tsp_fftpnt_tb.Size = new System.Drawing.Size(58, 19);
            this.tsp_fftpnt_tb.TabIndex = 34;
            this.tsp_fftpnt_tb.Text = "8192";
            // 
            // tsp_shift_tb
            // 
            this.tsp_shift_tb.Location = new System.Drawing.Point(185, 29);
            this.tsp_shift_tb.Name = "tsp_shift_tb";
            this.tsp_shift_tb.Size = new System.Drawing.Size(40, 19);
            this.tsp_shift_tb.TabIndex = 41;
            this.tsp_shift_tb.Text = "0.125";
            // 
            // tsp_phase_denom_tb
            // 
            this.tsp_phase_denom_tb.Location = new System.Drawing.Point(136, 30);
            this.tsp_phase_denom_tb.Name = "tsp_phase_denom_tb";
            this.tsp_phase_denom_tb.Size = new System.Drawing.Size(34, 19);
            this.tsp_phase_denom_tb.TabIndex = 38;
            this.tsp_phase_denom_tb.Text = "8";
            // 
            // tsp_phase_num_tb
            // 
            this.tsp_phase_num_tb.Location = new System.Drawing.Point(86, 30);
            this.tsp_phase_num_tb.Name = "tsp_phase_num_tb";
            this.tsp_phase_num_tb.Size = new System.Drawing.Size(33, 19);
            this.tsp_phase_num_tb.TabIndex = 36;
            this.tsp_phase_num_tb.Text = "7";
            this.tsp_phase_num_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 35;
            this.label6.Text = "FFT point";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 45;
            this.label8.Text = "Amp";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(183, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 20);
            this.label7.TabIndex = 40;
            this.label7.Text = "Shift";
            // 
            // amp_tb
            // 
            this.amp_tb.Location = new System.Drawing.Point(48, 53);
            this.amp_tb.Name = "amp_tb";
            this.amp_tb.Size = new System.Drawing.Size(64, 19);
            this.amp_tb.TabIndex = 44;
            this.amp_tb.Text = "0.8";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(82, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.TabIndex = 37;
            this.label12.Text = "Phase Range";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(118, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 28);
            this.label11.TabIndex = 39;
            this.label11.Text = "/";
            // 
            // input_end_tb
            // 
            this.input_end_tb.Location = new System.Drawing.Point(149, 47);
            this.input_end_tb.Name = "input_end_tb";
            this.input_end_tb.Size = new System.Drawing.Size(34, 19);
            this.input_end_tb.TabIndex = 60;
            this.input_end_tb.TextChanged += new System.EventHandler(this.input_end_tb_TextChanged);
            // 
            // input_start_tb
            // 
            this.input_start_tb.Location = new System.Drawing.Point(99, 47);
            this.input_start_tb.Name = "input_start_tb";
            this.input_start_tb.Size = new System.Drawing.Size(34, 19);
            this.input_start_tb.TabIndex = 59;
            this.input_start_tb.Text = "1";
            this.input_start_tb.TextChanged += new System.EventHandler(this.input_start_tb_TextChanged);
            // 
            // output_end_tb
            // 
            this.output_end_tb.Location = new System.Drawing.Point(150, 45);
            this.output_end_tb.Name = "output_end_tb";
            this.output_end_tb.Size = new System.Drawing.Size(34, 19);
            this.output_end_tb.TabIndex = 58;
            this.output_end_tb.TextChanged += new System.EventHandler(this.output_end_tb_TextChanged);
            // 
            // output_start_tb
            // 
            this.output_start_tb.Location = new System.Drawing.Point(100, 44);
            this.output_start_tb.Name = "output_start_tb";
            this.output_start_tb.Size = new System.Drawing.Size(34, 19);
            this.output_start_tb.TabIndex = 57;
            this.output_start_tb.Text = "1";
            this.output_start_tb.TextChanged += new System.EventHandler(this.output_start_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 56;
            this.label5.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "Input channel : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "Output channel : ";
            // 
            // input_test_btn
            // 
            this.input_test_btn.Location = new System.Drawing.Point(149, 72);
            this.input_test_btn.Name = "input_test_btn";
            this.input_test_btn.Size = new System.Drawing.Size(50, 24);
            this.input_test_btn.TabIndex = 63;
            this.input_test_btn.Text = "Go";
            this.input_test_btn.Click += new System.EventHandler(this.input_test_btn_Click);
            // 
            // input_test_tb
            // 
            this.input_test_tb.Location = new System.Drawing.Point(100, 69);
            this.input_test_tb.Name = "input_test_tb";
            this.input_test_tb.Size = new System.Drawing.Size(34, 19);
            this.input_test_tb.TabIndex = 66;
            this.input_test_tb.Text = "1";
            this.input_test_tb.TextChanged += new System.EventHandler(this.input_test_tb_TextChanged);
            // 
            // output_test_tb
            // 
            this.output_test_tb.Location = new System.Drawing.Point(99, 72);
            this.output_test_tb.Name = "output_test_tb";
            this.output_test_tb.Size = new System.Drawing.Size(34, 19);
            this.output_test_tb.TabIndex = 65;
            this.output_test_tb.Text = "1";
            this.output_test_tb.TextChanged += new System.EventHandler(this.output_test_tb_TextChanged);
            // 
            // output_test_btn
            // 
            this.output_test_btn.Location = new System.Drawing.Point(150, 70);
            this.output_test_btn.Name = "output_test_btn";
            this.output_test_btn.Size = new System.Drawing.Size(50, 24);
            this.output_test_btn.TabIndex = 67;
            this.output_test_btn.Text = "Go";
            this.output_test_btn.Click += new System.EventHandler(this.output_test_btn_Click);
            // 
            // check_pictureBox
            // 
            this.check_pictureBox.Location = new System.Drawing.Point(278, 5);
            this.check_pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.check_pictureBox.Name = "check_pictureBox";
            this.check_pictureBox.Size = new System.Drawing.Size(278, 437);
            this.check_pictureBox.TabIndex = 68;
            this.check_pictureBox.TabStop = false;
            this.check_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.check_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.input_dleng_lbl);
            this.groupBox2.Controls.Add(this.input_set_dleng_btn);
            this.groupBox2.Controls.Add(this.output_test_tb);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.input_start_tb);
            this.groupBox2.Controls.Add(this.input_end_tb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.input_test_btn);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(33, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 104);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 66;
            this.label1.Text = "Data Length :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.output_dleng_lbl);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.output_set_dleng_btn);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.output_test_btn);
            this.groupBox3.Controls.Add(this.input_test_tb);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.output_start_tb);
            this.groupBox3.Controls.Add(this.output_end_tb);
            this.groupBox3.Location = new System.Drawing.Point(33, 239);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 101);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output test";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 69;
            this.label4.Text = "Data Length :";
            // 
            // output_dleng_lbl
            // 
            this.output_dleng_lbl.Font = new System.Drawing.Font("ＭＳ 明朝", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.output_dleng_lbl.Location = new System.Drawing.Point(74, 17);
            this.output_dleng_lbl.Name = "output_dleng_lbl";
            this.output_dleng_lbl.Size = new System.Drawing.Size(66, 18);
            this.output_dleng_lbl.TabIndex = 67;
            this.output_dleng_lbl.Text = "131072";
            this.output_dleng_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 12);
            this.label9.TabIndex = 67;
            this.label9.Text = "Output channel : ";
            // 
            // output_set_dleng_btn
            // 
            this.output_set_dleng_btn.Location = new System.Drawing.Point(146, 14);
            this.output_set_dleng_btn.Name = "output_set_dleng_btn";
            this.output_set_dleng_btn.Size = new System.Drawing.Size(53, 24);
            this.output_set_dleng_btn.TabIndex = 68;
            this.output_set_dleng_btn.Text = "Set";
            this.output_set_dleng_btn.Click += new System.EventHandler(this.output_chg_dleng_btn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(137, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 68;
            this.label13.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 66;
            this.label10.Text = "Input channel : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.save_btn);
            this.groupBox4.Controls.Add(this.next_ch_btn);
            this.groupBox4.Controls.Add(this.prev_ch_btn);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.start_btn);
            this.groupBox4.Location = new System.Drawing.Point(33, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 90);
            this.groupBox4.TabIndex = 71;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mic Calibration";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 12);
            this.label16.TabIndex = 54;
            this.label16.Text = "Input channel = 1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 37);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 12);
            this.label17.TabIndex = 53;
            this.label17.Text = "Level = 88.1dB";
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(72, 57);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(50, 24);
            this.start_btn.TabIndex = 63;
            this.start_btn.Text = "Start";
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // prev_ch_btn
            // 
            this.prev_ch_btn.Location = new System.Drawing.Point(110, 14);
            this.prev_ch_btn.Name = "prev_ch_btn";
            this.prev_ch_btn.Size = new System.Drawing.Size(24, 20);
            this.prev_ch_btn.TabIndex = 64;
            this.prev_ch_btn.Text = "<";
            // 
            // next_ch_btn
            // 
            this.next_ch_btn.Location = new System.Drawing.Point(139, 14);
            this.next_ch_btn.Name = "next_ch_btn";
            this.next_ch_btn.Size = new System.Drawing.Size(24, 20);
            this.next_ch_btn.TabIndex = 65;
            this.next_ch_btn.Text = ">";
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(133, 57);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(50, 24);
            this.save_btn.TabIndex = 66;
            this.save_btn.Text = "Save";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.Salmon;
            this.label14.Location = new System.Drawing.Point(13, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 67;
            this.label14.Text = "GOOD";
            // 
            // CheckSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 449);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.check_pictureBox);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CheckSystemForm";
            this.Text = "Check System";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_pictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button input_set_dleng_btn;
        private System.Windows.Forms.Label input_dleng_lbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tsp_fftpnt_tb;
        private System.Windows.Forms.TextBox tsp_shift_tb;
        private System.Windows.Forms.TextBox tsp_phase_denom_tb;
        private System.Windows.Forms.TextBox tsp_phase_num_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox amp_tb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox hilo_cb;
        private System.Windows.Forms.TextBox input_end_tb;
        private System.Windows.Forms.TextBox input_start_tb;
        private System.Windows.Forms.TextBox output_end_tb;
        private System.Windows.Forms.TextBox output_start_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button input_test_btn;
        private System.Windows.Forms.TextBox input_test_tb;
        private System.Windows.Forms.TextBox output_test_tb;
        private System.Windows.Forms.Button output_test_btn;
        private System.Windows.Forms.PictureBox check_pictureBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label output_dleng_lbl;
        private System.Windows.Forms.Button output_set_dleng_btn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button next_ch_btn;
        private System.Windows.Forms.Button prev_ch_btn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button start_btn;
    }
}