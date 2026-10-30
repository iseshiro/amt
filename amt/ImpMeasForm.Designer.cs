namespace amt
{
    partial class ImpMeasForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.output_start_tb = new System.Windows.Forms.TextBox();
            this.output_end_tb = new System.Windows.Forms.TextBox();
            this.input_start_tb = new System.Windows.Forms.TextBox();
            this.input_end_tb = new System.Windows.Forms.TextBox();
            this.hilo_cb = new System.Windows.Forms.CheckBox();
            this.tsp_shift_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_denom_tb = new System.Windows.Forms.TextBox();
            this.tsp_phase_num_tb = new System.Windows.Forms.TextBox();
            this.tsp_fftpnt_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.amp_tb = new System.Windows.Forms.TextBox();
            this.ok_btn = new System.Windows.Forms.Button();
            this.groupBox_tsp = new System.Windows.Forms.GroupBox();
            this.work_ch_for_tsp_lbl = new System.Windows.Forms.Label();
            this.groupBox_deconv = new System.Windows.Forms.GroupBox();
            this.work_ch_for_deconv_lbl = new System.Windows.Forms.Label();
            this.dcnv_fftpoint_tb = new System.Windows.Forms.TextBox();
            this.dcnv_freq_ed_tb = new System.Windows.Forms.TextBox();
            this.dcnv_freq_st_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dcnv_scale_tb = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.folder_brows_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imp_meas_status_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.sync_ave_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ir_length_tb = new System.Windows.Forms.TextBox();
            this.save_tsp_cb = new System.Windows.Forms.CheckBox();
            this.tsp_wave_box = new System.Windows.Forms.PictureBox();
            this.imp_wave_box = new System.Windows.Forms.PictureBox();
            this.draw_wave_cb = new System.Windows.Forms.CheckBox();
            this.simulout_cb = new System.Windows.Forms.CheckBox();
            this.trig_cb = new System.Windows.Forms.CheckBox();
            this.trig_ch_tb = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.trig_width_tb = new System.Windows.Forms.TextBox();
            this.groupBox_tsp.SuspendLayout();
            this.groupBox_deconv.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsp_wave_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imp_wave_box)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output channel : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Input channel : ";
            // 
            // output_start_tb
            // 
            this.output_start_tb.Location = new System.Drawing.Point(103, 14);
            this.output_start_tb.Name = "output_start_tb";
            this.output_start_tb.Size = new System.Drawing.Size(34, 19);
            this.output_start_tb.TabIndex = 6;
            this.output_start_tb.Text = "1";
            // 
            // output_end_tb
            // 
            this.output_end_tb.Location = new System.Drawing.Point(146, 14);
            this.output_end_tb.Name = "output_end_tb";
            this.output_end_tb.Size = new System.Drawing.Size(34, 19);
            this.output_end_tb.TabIndex = 7;
            // 
            // input_start_tb
            // 
            this.input_start_tb.Location = new System.Drawing.Point(102, 38);
            this.input_start_tb.Name = "input_start_tb";
            this.input_start_tb.Size = new System.Drawing.Size(34, 19);
            this.input_start_tb.TabIndex = 8;
            this.input_start_tb.Text = "1";
            // 
            // input_end_tb
            // 
            this.input_end_tb.Location = new System.Drawing.Point(146, 39);
            this.input_end_tb.Name = "input_end_tb";
            this.input_end_tb.Size = new System.Drawing.Size(34, 19);
            this.input_end_tb.TabIndex = 9;
            // 
            // hilo_cb
            // 
            this.hilo_cb.AutoSize = true;
            this.hilo_cb.Location = new System.Drawing.Point(150, 55);
            this.hilo_cb.Name = "hilo_cb";
            this.hilo_cb.Size = new System.Drawing.Size(99, 16);
            this.hilo_cb.TabIndex = 42;
            this.hilo_cb.Text = "High ---> Low";
            this.hilo_cb.UseVisualStyleBackColor = true;
            // 
            // tsp_shift_tb
            // 
            this.tsp_shift_tb.Location = new System.Drawing.Point(197, 28);
            this.tsp_shift_tb.Name = "tsp_shift_tb";
            this.tsp_shift_tb.Size = new System.Drawing.Size(52, 19);
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
            // tsp_fftpnt_tb
            // 
            this.tsp_fftpnt_tb.Location = new System.Drawing.Point(8, 30);
            this.tsp_fftpnt_tb.Name = "tsp_fftpnt_tb";
            this.tsp_fftpnt_tb.Size = new System.Drawing.Size(58, 19);
            this.tsp_fftpnt_tb.TabIndex = 34;
            this.tsp_fftpnt_tb.Text = "8192";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 35;
            this.label6.Text = "FFT point";
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
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(82, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.TabIndex = 37;
            this.label12.Text = "Phase Range";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(195, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 20);
            this.label7.TabIndex = 40;
            this.label7.Text = "Shift";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(17, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 45;
            this.label8.Text = "Amp";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // amp_tb
            // 
            this.amp_tb.Location = new System.Drawing.Point(57, 53);
            this.amp_tb.Name = "amp_tb";
            this.amp_tb.Size = new System.Drawing.Size(64, 19);
            this.amp_tb.TabIndex = 44;
            this.amp_tb.Text = "0.8";
            // 
            // ok_btn
            // 
            this.ok_btn.Enabled = false;
            this.ok_btn.Location = new System.Drawing.Point(501, 121);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(76, 27);
            this.ok_btn.TabIndex = 43;
            this.ok_btn.Text = "START";
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // groupBox_tsp
            // 
            this.groupBox_tsp.Controls.Add(this.tsp_fftpnt_tb);
            this.groupBox_tsp.Controls.Add(this.tsp_shift_tb);
            this.groupBox_tsp.Controls.Add(this.work_ch_for_tsp_lbl);
            this.groupBox_tsp.Controls.Add(this.tsp_phase_denom_tb);
            this.groupBox_tsp.Controls.Add(this.tsp_phase_num_tb);
            this.groupBox_tsp.Controls.Add(this.label6);
            this.groupBox_tsp.Controls.Add(this.label8);
            this.groupBox_tsp.Controls.Add(this.label7);
            this.groupBox_tsp.Controls.Add(this.amp_tb);
            this.groupBox_tsp.Controls.Add(this.label12);
            this.groupBox_tsp.Controls.Add(this.label11);
            this.groupBox_tsp.Controls.Add(this.hilo_cb);
            this.groupBox_tsp.Location = new System.Drawing.Point(205, 12);
            this.groupBox_tsp.Name = "groupBox_tsp";
            this.groupBox_tsp.Size = new System.Drawing.Size(263, 100);
            this.groupBox_tsp.TabIndex = 46;
            this.groupBox_tsp.TabStop = false;
            this.groupBox_tsp.Text = "TSP signal";
            // 
            // work_ch_for_tsp_lbl
            // 
            this.work_ch_for_tsp_lbl.AutoSize = true;
            this.work_ch_for_tsp_lbl.Location = new System.Drawing.Point(13, 79);
            this.work_ch_for_tsp_lbl.Name = "work_ch_for_tsp_lbl";
            this.work_ch_for_tsp_lbl.Size = new System.Drawing.Size(97, 12);
            this.work_ch_for_tsp_lbl.TabIndex = 57;
            this.work_ch_for_tsp_lbl.Text = "Work ch. for TSP :";
            // 
            // groupBox_deconv
            // 
            this.groupBox_deconv.Controls.Add(this.work_ch_for_deconv_lbl);
            this.groupBox_deconv.Controls.Add(this.dcnv_fftpoint_tb);
            this.groupBox_deconv.Controls.Add(this.dcnv_freq_ed_tb);
            this.groupBox_deconv.Controls.Add(this.dcnv_freq_st_tb);
            this.groupBox_deconv.Controls.Add(this.label1);
            this.groupBox_deconv.Controls.Add(this.label9);
            this.groupBox_deconv.Controls.Add(this.dcnv_scale_tb);
            this.groupBox_deconv.Controls.Add(this.label13);
            this.groupBox_deconv.Controls.Add(this.label14);
            this.groupBox_deconv.Location = new System.Drawing.Point(487, 12);
            this.groupBox_deconv.Name = "groupBox_deconv";
            this.groupBox_deconv.Size = new System.Drawing.Size(263, 82);
            this.groupBox_deconv.TabIndex = 51;
            this.groupBox_deconv.TabStop = false;
            this.groupBox_deconv.Text = "Deconvolution";
            // 
            // work_ch_for_deconv_lbl
            // 
            this.work_ch_for_deconv_lbl.AutoSize = true;
            this.work_ch_for_deconv_lbl.Location = new System.Drawing.Point(13, 59);
            this.work_ch_for_deconv_lbl.Name = "work_ch_for_deconv_lbl";
            this.work_ch_for_deconv_lbl.Size = new System.Drawing.Size(148, 12);
            this.work_ch_for_deconv_lbl.TabIndex = 58;
            this.work_ch_for_deconv_lbl.Text = "Work ch. for Deconvolution :";
            // 
            // dcnv_fftpoint_tb
            // 
            this.dcnv_fftpoint_tb.Enabled = false;
            this.dcnv_fftpoint_tb.Location = new System.Drawing.Point(8, 30);
            this.dcnv_fftpoint_tb.Name = "dcnv_fftpoint_tb";
            this.dcnv_fftpoint_tb.Size = new System.Drawing.Size(58, 19);
            this.dcnv_fftpoint_tb.TabIndex = 34;
            this.dcnv_fftpoint_tb.Text = "131072";
            // 
            // dcnv_freq_ed_tb
            // 
            this.dcnv_freq_ed_tb.Location = new System.Drawing.Point(136, 30);
            this.dcnv_freq_ed_tb.Name = "dcnv_freq_ed_tb";
            this.dcnv_freq_ed_tb.Size = new System.Drawing.Size(48, 19);
            this.dcnv_freq_ed_tb.TabIndex = 38;
            this.dcnv_freq_ed_tb.Text = "24000";
            // 
            // dcnv_freq_st_tb
            // 
            this.dcnv_freq_st_tb.Location = new System.Drawing.Point(86, 30);
            this.dcnv_freq_st_tb.Name = "dcnv_freq_st_tb";
            this.dcnv_freq_st_tb.Size = new System.Drawing.Size(33, 19);
            this.dcnv_freq_st_tb.TabIndex = 36;
            this.dcnv_freq_st_tb.Text = "0";
            this.dcnv_freq_st_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "FFT point";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(200, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 18);
            this.label9.TabIndex = 45;
            this.label9.Text = "Scale";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dcnv_scale_tb
            // 
            this.dcnv_scale_tb.Location = new System.Drawing.Point(202, 30);
            this.dcnv_scale_tb.Name = "dcnv_scale_tb";
            this.dcnv_scale_tb.Size = new System.Drawing.Size(47, 19);
            this.dcnv_scale_tb.TabIndex = 44;
            this.dcnv_scale_tb.Text = "0.8";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(82, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 20);
            this.label13.TabIndex = 37;
            this.label13.Text = "Freq Range [Hz]";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(120, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 20);
            this.label14.TabIndex = 39;
            this.label14.Text = "-";
            // 
            // folder_brows_btn
            // 
            this.folder_brows_btn.Location = new System.Drawing.Point(341, 121);
            this.folder_brows_btn.Name = "folder_brows_btn";
            this.folder_brows_btn.Size = new System.Drawing.Size(106, 27);
            this.folder_brows_btn.TabIndex = 49;
            this.folder_brows_btn.Text = "Set Work Folder";
            this.folder_brows_btn.Click += new System.EventHandler(this.set_imp_folder_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(137, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 53;
            this.label10.Text = "-";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imp_meas_status_lbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.TabIndex = 54;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // imp_meas_status_lbl
            // 
            this.imp_meas_status_lbl.Name = "imp_meas_status_lbl";
            this.imp_meas_status_lbl.Size = new System.Drawing.Size(45, 17);
            this.imp_meas_status_lbl.Text = "Status: ";
            // 
            // sync_ave_tb
            // 
            this.sync_ave_tb.Location = new System.Drawing.Point(153, 67);
            this.sync_ave_tb.Name = "sync_ave_tb";
            this.sync_ave_tb.Size = new System.Drawing.Size(34, 19);
            this.sync_ave_tb.TabIndex = 59;
            this.sync_ave_tb.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "Synchronous Averaging : ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(35, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 12);
            this.label15.TabIndex = 61;
            this.label15.Text = "IR length : ";
            // 
            // ir_length_tb
            // 
            this.ir_length_tb.Location = new System.Drawing.Point(103, 97);
            this.ir_length_tb.Name = "ir_length_tb";
            this.ir_length_tb.Size = new System.Drawing.Size(85, 19);
            this.ir_length_tb.TabIndex = 62;
            this.ir_length_tb.Text = "4096";
            this.ir_length_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // save_tsp_cb
            // 
            this.save_tsp_cb.AutoSize = true;
            this.save_tsp_cb.Location = new System.Drawing.Point(197, 132);
            this.save_tsp_cb.Name = "save_tsp_cb";
            this.save_tsp_cb.Size = new System.Drawing.Size(74, 16);
            this.save_tsp_cb.TabIndex = 63;
            this.save_tsp_cb.Text = "Save TSP";
            this.save_tsp_cb.UseVisualStyleBackColor = true;
            // 
            // tsp_wave_box
            // 
            this.tsp_wave_box.Location = new System.Drawing.Point(15, 163);
            this.tsp_wave_box.Name = "tsp_wave_box";
            this.tsp_wave_box.Size = new System.Drawing.Size(363, 207);
            this.tsp_wave_box.TabIndex = 64;
            this.tsp_wave_box.TabStop = false;
            this.tsp_wave_box.Paint += new System.Windows.Forms.PaintEventHandler(this.tsp_wave_box_Paint);
            // 
            // imp_wave_box
            // 
            this.imp_wave_box.Location = new System.Drawing.Point(385, 163);
            this.imp_wave_box.Name = "imp_wave_box";
            this.imp_wave_box.Size = new System.Drawing.Size(365, 207);
            this.imp_wave_box.TabIndex = 65;
            this.imp_wave_box.TabStop = false;
            this.imp_wave_box.Paint += new System.Windows.Forms.PaintEventHandler(this.imp_wave_box_Paint);
            // 
            // draw_wave_cb
            // 
            this.draw_wave_cb.AutoSize = true;
            this.draw_wave_cb.Location = new System.Drawing.Point(623, 127);
            this.draw_wave_cb.Name = "draw_wave_cb";
            this.draw_wave_cb.Size = new System.Drawing.Size(80, 16);
            this.draw_wave_cb.TabIndex = 66;
            this.draw_wave_cb.Text = "Draw wave";
            this.draw_wave_cb.UseVisualStyleBackColor = true;
            this.draw_wave_cb.CheckedChanged += new System.EventHandler(this.draw_wave_cb_CheckedChanged);
            // 
            // simulout_cb
            // 
            this.simulout_cb.AutoSize = true;
            this.simulout_cb.Location = new System.Drawing.Point(37, 132);
            this.simulout_cb.Name = "simulout_cb";
            this.simulout_cb.Size = new System.Drawing.Size(130, 16);
            this.simulout_cb.TabIndex = 67;
            this.simulout_cb.Text = "Simultaneous Output";
            this.simulout_cb.UseVisualStyleBackColor = true;
            // 
            // trig_cb
            // 
            this.trig_cb.AutoSize = true;
            this.trig_cb.Location = new System.Drawing.Point(489, 99);
            this.trig_cb.Name = "trig_cb";
            this.trig_cb.Size = new System.Drawing.Size(84, 16);
            this.trig_cb.TabIndex = 68;
            this.trig_cb.Text = "Trigger ch.";
            this.trig_cb.UseVisualStyleBackColor = true;
            // 
            // trig_ch_tb
            // 
            this.trig_ch_tb.Location = new System.Drawing.Point(575, 97);
            this.trig_ch_tb.Name = "trig_ch_tb";
            this.trig_ch_tb.Size = new System.Drawing.Size(70, 19);
            this.trig_ch_tb.TabIndex = 69;
            this.trig_ch_tb.Text = "63";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(652, 100);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 70;
            this.label16.Text = "Width";
            // 
            // trig_width_tb
            // 
            this.trig_width_tb.Location = new System.Drawing.Point(690, 97);
            this.trig_width_tb.Name = "trig_width_tb";
            this.trig_width_tb.Size = new System.Drawing.Size(60, 19);
            this.trig_width_tb.TabIndex = 71;
            this.trig_width_tb.Text = "480";
            this.trig_width_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ImpMeasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 399);
            this.Controls.Add(this.trig_width_tb);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.trig_ch_tb);
            this.Controls.Add(this.trig_cb);
            this.Controls.Add(this.simulout_cb);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.draw_wave_cb);
            this.Controls.Add(this.imp_wave_box);
            this.Controls.Add(this.tsp_wave_box);
            this.Controls.Add(this.save_tsp_cb);
            this.Controls.Add(this.ir_length_tb);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sync_ave_tb);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.folder_brows_btn);
            this.Controls.Add(this.groupBox_deconv);
            this.Controls.Add(this.groupBox_tsp);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.input_end_tb);
            this.Controls.Add(this.input_start_tb);
            this.Controls.Add(this.output_end_tb);
            this.Controls.Add(this.output_start_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "ImpMeasForm";
            this.Text = "Impulse Measurement";
            this.groupBox_tsp.ResumeLayout(false);
            this.groupBox_tsp.PerformLayout();
            this.groupBox_deconv.ResumeLayout(false);
            this.groupBox_deconv.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsp_wave_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imp_wave_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox output_start_tb;
        private System.Windows.Forms.TextBox output_end_tb;
        private System.Windows.Forms.TextBox input_start_tb;
        private System.Windows.Forms.TextBox input_end_tb;
        private System.Windows.Forms.CheckBox hilo_cb;
        private System.Windows.Forms.TextBox tsp_shift_tb;
        private System.Windows.Forms.TextBox tsp_phase_denom_tb;
        private System.Windows.Forms.TextBox tsp_phase_num_tb;
        private System.Windows.Forms.TextBox tsp_fftpnt_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox amp_tb;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.GroupBox groupBox_tsp;
        private System.Windows.Forms.GroupBox groupBox_deconv;
        private System.Windows.Forms.TextBox dcnv_freq_ed_tb;
        private System.Windows.Forms.TextBox dcnv_freq_st_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox dcnv_scale_tb;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox dcnv_fftpoint_tb;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button folder_brows_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel imp_meas_status_lbl;
        private System.Windows.Forms.Label work_ch_for_deconv_lbl;
        private System.Windows.Forms.Label work_ch_for_tsp_lbl;
        private System.Windows.Forms.TextBox sync_ave_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox ir_length_tb;
        private System.Windows.Forms.CheckBox save_tsp_cb;
        private System.Windows.Forms.PictureBox tsp_wave_box;
        private System.Windows.Forms.PictureBox imp_wave_box;
        private System.Windows.Forms.CheckBox draw_wave_cb;
        private System.Windows.Forms.CheckBox simulout_cb;
        private System.Windows.Forms.CheckBox trig_cb;
        private System.Windows.Forms.TextBox trig_ch_tb;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox trig_width_tb;
    }
}