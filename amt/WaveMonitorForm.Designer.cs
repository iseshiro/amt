namespace amt
{
    partial class WaveMonitorForm
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
            this.disp_ch_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wave_mon_pictureBox = new System.Windows.Forms.PictureBox();
            this.ch_minus_btn = new System.Windows.Forms.Button();
            this.ch_plus_btn = new System.Windows.Forms.Button();
            this.ch_auto_chg_cb = new System.Windows.Forms.CheckBox();
            this.display_time_tb = new System.Windows.Forms.TextBox();
            this.display_time_lbl = new System.Windows.Forms.Label();
            this.display_unit_lbl = new System.Windows.Forms.Label();
            this.spectrum_cb = new System.Windows.Forms.CheckBox();
            this.adjust_level_lbl = new System.Windows.Forms.Label();
            this.adjust_plus_btn = new System.Windows.Forms.Button();
            this.adjust_minus_btn = new System.Windows.Forms.Button();
            this.max_freq_lbl = new System.Windows.Forms.Label();
            this.memo_reset_btn = new System.Windows.Forms.Button();
            this.cali_cnt_tb = new System.Windows.Forms.TextBox();
            this.cali_db_tb = new System.Windows.Forms.TextBox();
            this.auto_cali_cb = new System.Windows.Forms.CheckBox();
            this.auto_cali1_lbl = new System.Windows.Forms.Label();
            this.auto_cali2_lbl = new System.Windows.Forms.Label();
            this.auto_cali_ok_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wave_mon_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // disp_ch_tb
            // 
            this.disp_ch_tb.Location = new System.Drawing.Point(34, 244);
            this.disp_ch_tb.Name = "disp_ch_tb";
            this.disp_ch_tb.Size = new System.Drawing.Size(26, 19);
            this.disp_ch_tb.TabIndex = 0;
            this.disp_ch_tb.Text = "80";
            this.disp_ch_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.disp_ch_tb.TextChanged += new System.EventHandler(this.disp_ch_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ch. ";
            // 
            // wave_mon_pictureBox
            // 
            this.wave_mon_pictureBox.Location = new System.Drawing.Point(5, 6);
            this.wave_mon_pictureBox.Name = "wave_mon_pictureBox";
            this.wave_mon_pictureBox.Size = new System.Drawing.Size(624, 228);
            this.wave_mon_pictureBox.TabIndex = 2;
            this.wave_mon_pictureBox.TabStop = false;
            this.wave_mon_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.wave_mon_pictureBox_Paint);
            // 
            // ch_minus_btn
            // 
            this.ch_minus_btn.Location = new System.Drawing.Point(66, 243);
            this.ch_minus_btn.Name = "ch_minus_btn";
            this.ch_minus_btn.Size = new System.Drawing.Size(23, 21);
            this.ch_minus_btn.TabIndex = 3;
            this.ch_minus_btn.Text = "-";
            this.ch_minus_btn.UseVisualStyleBackColor = true;
            this.ch_minus_btn.Click += new System.EventHandler(this.ch_minus_btn_Click);
            // 
            // ch_plus_btn
            // 
            this.ch_plus_btn.Location = new System.Drawing.Point(88, 243);
            this.ch_plus_btn.Name = "ch_plus_btn";
            this.ch_plus_btn.Size = new System.Drawing.Size(23, 21);
            this.ch_plus_btn.TabIndex = 4;
            this.ch_plus_btn.Text = "+";
            this.ch_plus_btn.UseVisualStyleBackColor = true;
            this.ch_plus_btn.Click += new System.EventHandler(this.ch_plus_btn_Click);
            // 
            // ch_auto_chg_cb
            // 
            this.ch_auto_chg_cb.AutoSize = true;
            this.ch_auto_chg_cb.Location = new System.Drawing.Point(14, 270);
            this.ch_auto_chg_cb.Name = "ch_auto_chg_cb";
            this.ch_auto_chg_cb.Size = new System.Drawing.Size(108, 16);
            this.ch_auto_chg_cb.TabIndex = 5;
            this.ch_auto_chg_cb.Text = "Auto Ch. change";
            this.ch_auto_chg_cb.UseVisualStyleBackColor = true;
            // 
            // display_time_tb
            // 
            this.display_time_tb.Location = new System.Drawing.Point(209, 268);
            this.display_time_tb.Name = "display_time_tb";
            this.display_time_tb.Size = new System.Drawing.Size(41, 19);
            this.display_time_tb.TabIndex = 6;
            this.display_time_tb.Text = "5";
            this.display_time_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.display_time_tb.TextChanged += new System.EventHandler(this.display_time_tb_TextChanged);
            // 
            // display_time_lbl
            // 
            this.display_time_lbl.AutoSize = true;
            this.display_time_lbl.Location = new System.Drawing.Point(140, 271);
            this.display_time_lbl.Name = "display_time_lbl";
            this.display_time_lbl.Size = new System.Drawing.Size(69, 12);
            this.display_time_lbl.TabIndex = 7;
            this.display_time_lbl.Text = "Display time";
            // 
            // display_unit_lbl
            // 
            this.display_unit_lbl.AutoSize = true;
            this.display_unit_lbl.Location = new System.Drawing.Point(256, 271);
            this.display_unit_lbl.Name = "display_unit_lbl";
            this.display_unit_lbl.Size = new System.Drawing.Size(11, 12);
            this.display_unit_lbl.TabIndex = 8;
            this.display_unit_lbl.Text = "s";
            // 
            // spectrum_cb
            // 
            this.spectrum_cb.AutoSize = true;
            this.spectrum_cb.Location = new System.Drawing.Point(142, 246);
            this.spectrum_cb.Name = "spectrum_cb";
            this.spectrum_cb.Size = new System.Drawing.Size(114, 16);
            this.spectrum_cb.TabIndex = 9;
            this.spectrum_cb.Text = "Spectrum Display";
            this.spectrum_cb.UseVisualStyleBackColor = true;
            this.spectrum_cb.CheckedChanged += new System.EventHandler(this.spectrum_cb_CheckedChanged);
            // 
            // adjust_level_lbl
            // 
            this.adjust_level_lbl.AutoSize = true;
            this.adjust_level_lbl.Location = new System.Drawing.Point(281, 250);
            this.adjust_level_lbl.Name = "adjust_level_lbl";
            this.adjust_level_lbl.Size = new System.Drawing.Size(110, 12);
            this.adjust_level_lbl.TabIndex = 11;
            this.adjust_level_lbl.Text = "Adjust level = 10 dB";
            // 
            // adjust_plus_btn
            // 
            this.adjust_plus_btn.Location = new System.Drawing.Point(420, 244);
            this.adjust_plus_btn.Name = "adjust_plus_btn";
            this.adjust_plus_btn.Size = new System.Drawing.Size(23, 21);
            this.adjust_plus_btn.TabIndex = 14;
            this.adjust_plus_btn.Text = "+";
            this.adjust_plus_btn.UseVisualStyleBackColor = true;
            this.adjust_plus_btn.Click += new System.EventHandler(this.adjust_plus_btn_Click);
            // 
            // adjust_minus_btn
            // 
            this.adjust_minus_btn.Location = new System.Drawing.Point(398, 244);
            this.adjust_minus_btn.Name = "adjust_minus_btn";
            this.adjust_minus_btn.Size = new System.Drawing.Size(23, 21);
            this.adjust_minus_btn.TabIndex = 13;
            this.adjust_minus_btn.Text = "-";
            this.adjust_minus_btn.UseVisualStyleBackColor = true;
            this.adjust_minus_btn.Click += new System.EventHandler(this.adjust_minus_btn_Click);
            // 
            // max_freq_lbl
            // 
            this.max_freq_lbl.AutoSize = true;
            this.max_freq_lbl.Location = new System.Drawing.Point(281, 272);
            this.max_freq_lbl.Name = "max_freq_lbl";
            this.max_freq_lbl.Size = new System.Drawing.Size(147, 12);
            this.max_freq_lbl.TabIndex = 15;
            this.max_freq_lbl.Text = "Max freq. = 1000Hz : 76.5dB";
            // 
            // memo_reset_btn
            // 
            this.memo_reset_btn.Location = new System.Drawing.Point(472, 267);
            this.memo_reset_btn.Name = "memo_reset_btn";
            this.memo_reset_btn.Size = new System.Drawing.Size(44, 21);
            this.memo_reset_btn.TabIndex = 16;
            this.memo_reset_btn.Text = "Reset";
            this.memo_reset_btn.UseVisualStyleBackColor = true;
            this.memo_reset_btn.Click += new System.EventHandler(this.memo_reset_btn_Click);
            // 
            // cali_cnt_tb
            // 
            this.cali_cnt_tb.Location = new System.Drawing.Point(540, 243);
            this.cali_cnt_tb.Name = "cali_cnt_tb";
            this.cali_cnt_tb.Size = new System.Drawing.Size(41, 19);
            this.cali_cnt_tb.TabIndex = 17;
            this.cali_cnt_tb.Text = "10";
            this.cali_cnt_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cali_cnt_tb.TextChanged += new System.EventHandler(this.cali_cnt_tb_TextChanged);
            // 
            // cali_db_tb
            // 
            this.cali_db_tb.Location = new System.Drawing.Point(540, 268);
            this.cali_db_tb.Name = "cali_db_tb";
            this.cali_db_tb.Size = new System.Drawing.Size(41, 19);
            this.cali_db_tb.TabIndex = 18;
            this.cali_db_tb.Text = "0.2";
            this.cali_db_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cali_db_tb.TextChanged += new System.EventHandler(this.cali_db_tb_TextChanged);
            // 
            // auto_cali_cb
            // 
            this.auto_cali_cb.AutoSize = true;
            this.auto_cali_cb.Location = new System.Drawing.Point(460, 245);
            this.auto_cali_cb.Name = "auto_cali_cb";
            this.auto_cali_cb.Size = new System.Drawing.Size(74, 16);
            this.auto_cali_cb.TabIndex = 19;
            this.auto_cali_cb.Text = "Auto Cali.";
            this.auto_cali_cb.UseVisualStyleBackColor = true;
            this.auto_cali_cb.CheckedChanged += new System.EventHandler(this.auto_cali_cb_CheckedChanged);
            // 
            // auto_cali1_lbl
            // 
            this.auto_cali1_lbl.AutoSize = true;
            this.auto_cali1_lbl.Location = new System.Drawing.Point(586, 247);
            this.auto_cali1_lbl.Name = "auto_cali1_lbl";
            this.auto_cali1_lbl.Size = new System.Drawing.Size(33, 12);
            this.auto_cali1_lbl.TabIndex = 20;
            this.auto_cali1_lbl.Text = "times";
            // 
            // auto_cali2_lbl
            // 
            this.auto_cali2_lbl.AutoSize = true;
            this.auto_cali2_lbl.Location = new System.Drawing.Point(586, 272);
            this.auto_cali2_lbl.Name = "auto_cali2_lbl";
            this.auto_cali2_lbl.Size = new System.Drawing.Size(19, 12);
            this.auto_cali2_lbl.TabIndex = 21;
            this.auto_cali2_lbl.Text = "dB";
            // 
            // auto_cali_ok_lbl
            // 
            this.auto_cali_ok_lbl.AutoSize = true;
            this.auto_cali_ok_lbl.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.auto_cali_ok_lbl.ForeColor = System.Drawing.Color.Red;
            this.auto_cali_ok_lbl.Location = new System.Drawing.Point(205, 106);
            this.auto_cali_ok_lbl.Name = "auto_cali_ok_lbl";
            this.auto_cali_ok_lbl.Size = new System.Drawing.Size(218, 24);
            this.auto_cali_ok_lbl.TabIndex = 22;
            this.auto_cali_ok_lbl.Text = "CH = 1 : 76.5dB   OK";
            // 
            // WaveMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 293);
            this.Controls.Add(this.auto_cali_ok_lbl);
            this.Controls.Add(this.auto_cali2_lbl);
            this.Controls.Add(this.auto_cali1_lbl);
            this.Controls.Add(this.auto_cali_cb);
            this.Controls.Add(this.cali_db_tb);
            this.Controls.Add(this.cali_cnt_tb);
            this.Controls.Add(this.memo_reset_btn);
            this.Controls.Add(this.max_freq_lbl);
            this.Controls.Add(this.adjust_plus_btn);
            this.Controls.Add(this.adjust_minus_btn);
            this.Controls.Add(this.adjust_level_lbl);
            this.Controls.Add(this.spectrum_cb);
            this.Controls.Add(this.display_unit_lbl);
            this.Controls.Add(this.display_time_tb);
            this.Controls.Add(this.ch_auto_chg_cb);
            this.Controls.Add(this.ch_plus_btn);
            this.Controls.Add(this.ch_minus_btn);
            this.Controls.Add(this.wave_mon_pictureBox);
            this.Controls.Add(this.disp_ch_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.display_time_lbl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaveMonitorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Wave Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.wave_mon_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox disp_ch_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox wave_mon_pictureBox;
        private System.Windows.Forms.Button ch_minus_btn;
        private System.Windows.Forms.Button ch_plus_btn;
        private System.Windows.Forms.CheckBox ch_auto_chg_cb;
        private System.Windows.Forms.TextBox display_time_tb;
        private System.Windows.Forms.Label display_time_lbl;
        private System.Windows.Forms.Label display_unit_lbl;
        private System.Windows.Forms.CheckBox spectrum_cb;
        private System.Windows.Forms.Label adjust_level_lbl;
        private System.Windows.Forms.Button adjust_plus_btn;
        private System.Windows.Forms.Button adjust_minus_btn;
        private System.Windows.Forms.Label max_freq_lbl;
        private System.Windows.Forms.Button memo_reset_btn;
        private System.Windows.Forms.TextBox cali_cnt_tb;
        private System.Windows.Forms.TextBox cali_db_tb;
        private System.Windows.Forms.CheckBox auto_cali_cb;
        private System.Windows.Forms.Label auto_cali1_lbl;
        private System.Windows.Forms.Label auto_cali2_lbl;
        private System.Windows.Forms.Label auto_cali_ok_lbl;
    }
}