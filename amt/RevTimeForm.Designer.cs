namespace amt
{
    partial class RevTimeForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.data_no_cb = new System.Windows.Forms.ComboBox();
            this.wave_pictureBox = new System.Windows.Forms.PictureBox();
            this.update_btn = new System.Windows.Forms.Button();
            this.selected_ch_lbl = new System.Windows.Forms.Label();
            this.rev_st_tb = new System.Windows.Forms.TextBox();
            this.rev_ed_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gr_time_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.calc_ed_time_tb = new System.Windows.Forms.TextBox();
            this.calc_st_time_tb = new System.Windows.Forms.TextBox();
            this.log_disp_cb = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.db_range_tb = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.db_grid_tb = new System.Windows.Forms.TextBox();
            this.rev_cb = new System.Windows.Forms.CheckBox();
            this.rev_time_lbl = new System.Windows.Forms.Label();
            this.calc_rev_time = new System.Windows.Forms.Label();
            this.lms_cb = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.amp_tb = new System.Windows.Forms.TextBox();
            this.save_image_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wave_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // data_no_cb
            // 
            this.data_no_cb.FormattingEnabled = true;
            this.data_no_cb.Items.AddRange(new object[] {
            "data1",
            "data2",
            "data3"});
            this.data_no_cb.Location = new System.Drawing.Point(12, 8);
            this.data_no_cb.Name = "data_no_cb";
            this.data_no_cb.Size = new System.Drawing.Size(87, 20);
            this.data_no_cb.TabIndex = 0;
            this.data_no_cb.SelectedIndexChanged += new System.EventHandler(this.data_no_cb_SelectedIndexChanged);
            // 
            // wave_pictureBox
            // 
            this.wave_pictureBox.Location = new System.Drawing.Point(12, 88);
            this.wave_pictureBox.Name = "wave_pictureBox";
            this.wave_pictureBox.Size = new System.Drawing.Size(665, 334);
            this.wave_pictureBox.TabIndex = 1;
            this.wave_pictureBox.TabStop = false;
            this.wave_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.wave_pictureBox_paint);
            // 
            // update_btn
            // 
            this.update_btn.Location = new System.Drawing.Point(94, 36);
            this.update_btn.Name = "update_btn";
            this.update_btn.Size = new System.Drawing.Size(54, 20);
            this.update_btn.TabIndex = 2;
            this.update_btn.Text = "Update";
            this.update_btn.UseVisualStyleBackColor = true;
            this.update_btn.Click += new System.EventHandler(this.update_btn_Click);
            // 
            // selected_ch_lbl
            // 
            this.selected_ch_lbl.AutoSize = true;
            this.selected_ch_lbl.Location = new System.Drawing.Point(105, 12);
            this.selected_ch_lbl.Name = "selected_ch_lbl";
            this.selected_ch_lbl.Size = new System.Drawing.Size(35, 12);
            this.selected_ch_lbl.TabIndex = 3;
            this.selected_ch_lbl.Text = "label1";
            // 
            // rev_st_tb
            // 
            this.rev_st_tb.Enabled = false;
            this.rev_st_tb.Location = new System.Drawing.Point(395, 27);
            this.rev_st_tb.Name = "rev_st_tb";
            this.rev_st_tb.Size = new System.Drawing.Size(45, 19);
            this.rev_st_tb.TabIndex = 4;
            this.rev_st_tb.Text = "-5";
            // 
            // rev_ed_tb
            // 
            this.rev_ed_tb.Enabled = false;
            this.rev_ed_tb.Location = new System.Drawing.Point(395, 47);
            this.rev_ed_tb.Name = "rev_ed_tb";
            this.rev_ed_tb.Size = new System.Drawing.Size(45, 19);
            this.rev_ed_tb.TabIndex = 5;
            this.rev_ed_tb.Text = "-25";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rev. start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "dB";
            // 
            // gr_time_tb
            // 
            this.gr_time_tb.Location = new System.Drawing.Point(241, 61);
            this.gr_time_tb.Name = "gr_time_tb";
            this.gr_time_tb.Size = new System.Drawing.Size(41, 19);
            this.gr_time_tb.TabIndex = 8;
            this.gr_time_tb.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "dB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rev. end";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Grid time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(513, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "End time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(638, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "ms";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(638, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "ms";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(513, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "Start time";
            // 
            // calc_ed_time_tb
            // 
            this.calc_ed_time_tb.Enabled = false;
            this.calc_ed_time_tb.Location = new System.Drawing.Point(575, 46);
            this.calc_ed_time_tb.Name = "calc_ed_time_tb";
            this.calc_ed_time_tb.Size = new System.Drawing.Size(57, 19);
            this.calc_ed_time_tb.TabIndex = 14;
            this.calc_ed_time_tb.Text = "800";
            this.calc_ed_time_tb.TextChanged += new System.EventHandler(this.calc_ed_time_tb_TextChanged);
            // 
            // calc_st_time_tb
            // 
            this.calc_st_time_tb.Enabled = false;
            this.calc_st_time_tb.Location = new System.Drawing.Point(575, 26);
            this.calc_st_time_tb.Name = "calc_st_time_tb";
            this.calc_st_time_tb.Size = new System.Drawing.Size(57, 19);
            this.calc_st_time_tb.TabIndex = 13;
            this.calc_st_time_tb.Text = "200";
            // 
            // log_disp_cb
            // 
            this.log_disp_cb.AutoSize = true;
            this.log_disp_cb.Location = new System.Drawing.Point(22, 39);
            this.log_disp_cb.Name = "log_disp_cb";
            this.log_disp_cb.Size = new System.Drawing.Size(42, 16);
            this.log_disp_cb.TabIndex = 20;
            this.log_disp_cb.Text = "Log";
            this.log_disp_cb.UseVisualStyleBackColor = true;
            this.log_disp_cb.CheckedChanged += new System.EventHandler(this.log_disp_cb_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(288, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "dB";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(200, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "Range";
            // 
            // db_range_tb
            // 
            this.db_range_tb.Location = new System.Drawing.Point(241, 12);
            this.db_range_tb.Name = "db_range_tb";
            this.db_range_tb.Size = new System.Drawing.Size(41, 19);
            this.db_range_tb.TabIndex = 21;
            this.db_range_tb.Text = "60";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(288, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "dB";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(200, 36);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 12);
            this.label15.TabIndex = 25;
            this.label15.Text = "Grid";
            // 
            // db_grid_tb
            // 
            this.db_grid_tb.Location = new System.Drawing.Point(241, 33);
            this.db_grid_tb.Name = "db_grid_tb";
            this.db_grid_tb.Size = new System.Drawing.Size(41, 19);
            this.db_grid_tb.TabIndex = 24;
            this.db_grid_tb.Text = "10";
            // 
            // rev_cb
            // 
            this.rev_cb.AutoSize = true;
            this.rev_cb.Enabled = false;
            this.rev_cb.Location = new System.Drawing.Point(336, 8);
            this.rev_cb.Name = "rev_cb";
            this.rev_cb.Size = new System.Drawing.Size(46, 16);
            this.rev_cb.TabIndex = 27;
            this.rev_cb.Text = "Rev.";
            this.rev_cb.UseVisualStyleBackColor = true;
            this.rev_cb.CheckedChanged += new System.EventHandler(this.rev_cb_CheckedChanged);
            // 
            // rev_time_lbl
            // 
            this.rev_time_lbl.AutoSize = true;
            this.rev_time_lbl.Location = new System.Drawing.Point(333, 70);
            this.rev_time_lbl.Name = "rev_time_lbl";
            this.rev_time_lbl.Size = new System.Drawing.Size(66, 12);
            this.rev_time_lbl.TabIndex = 28;
            this.rev_time_lbl.Text = "Rev. Time =";
            this.rev_time_lbl.Visible = false;
            // 
            // calc_rev_time
            // 
            this.calc_rev_time.AutoSize = true;
            this.calc_rev_time.Location = new System.Drawing.Point(513, 70);
            this.calc_rev_time.Name = "calc_rev_time";
            this.calc_rev_time.Size = new System.Drawing.Size(66, 12);
            this.calc_rev_time.TabIndex = 29;
            this.calc_rev_time.Text = "Rev. Time =";
            this.calc_rev_time.Visible = false;
            // 
            // lms_cb
            // 
            this.lms_cb.AutoSize = true;
            this.lms_cb.Enabled = false;
            this.lms_cb.Location = new System.Drawing.Point(517, 8);
            this.lms_cb.Name = "lms_cb";
            this.lms_cb.Size = new System.Drawing.Size(48, 16);
            this.lms_cb.TabIndex = 30;
            this.lms_cb.Text = "LMS.";
            this.lms_cb.UseVisualStyleBackColor = true;
            this.lms_cb.CheckedChanged += new System.EventHandler(this.lms_cb_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(92, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "Amp.";
            // 
            // amp_tb
            // 
            this.amp_tb.Location = new System.Drawing.Point(128, 63);
            this.amp_tb.Name = "amp_tb";
            this.amp_tb.Size = new System.Drawing.Size(41, 19);
            this.amp_tb.TabIndex = 31;
            this.amp_tb.Text = "1.0";
            // 
            // save_image_btn
            // 
            this.save_image_btn.Location = new System.Drawing.Point(12, 61);
            this.save_image_btn.Name = "save_image_btn";
            this.save_image_btn.Size = new System.Drawing.Size(74, 21);
            this.save_image_btn.TabIndex = 33;
            this.save_image_btn.Text = "Save Image";
            this.save_image_btn.UseVisualStyleBackColor = true;
            this.save_image_btn.Click += new System.EventHandler(this.save_image_btn_Click);
            // 
            // RevTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 434);
            this.Controls.Add(this.save_image_btn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.amp_tb);
            this.Controls.Add(this.lms_cb);
            this.Controls.Add(this.calc_rev_time);
            this.Controls.Add(this.rev_time_lbl);
            this.Controls.Add(this.rev_cb);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.db_grid_tb);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.db_range_tb);
            this.Controls.Add(this.log_disp_cb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.calc_ed_time_tb);
            this.Controls.Add(this.calc_st_time_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gr_time_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rev_ed_tb);
            this.Controls.Add(this.rev_st_tb);
            this.Controls.Add(this.selected_ch_lbl);
            this.Controls.Add(this.update_btn);
            this.Controls.Add(this.wave_pictureBox);
            this.Controls.Add(this.data_no_cb);
            this.Name = "RevTimeForm";
            this.Text = "Rev. Time";
            ((System.ComponentModel.ISupportInitialize)(this.wave_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox data_no_cb;
        private System.Windows.Forms.PictureBox wave_pictureBox;
        private System.Windows.Forms.Button update_btn;
        private System.Windows.Forms.Label selected_ch_lbl;
        private System.Windows.Forms.TextBox rev_st_tb;
        private System.Windows.Forms.TextBox rev_ed_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gr_time_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox calc_ed_time_tb;
        private System.Windows.Forms.TextBox calc_st_time_tb;
        private System.Windows.Forms.CheckBox log_disp_cb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox db_range_tb;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox db_grid_tb;
        private System.Windows.Forms.CheckBox rev_cb;
        private System.Windows.Forms.Label rev_time_lbl;
        private System.Windows.Forms.Label calc_rev_time;
        private System.Windows.Forms.CheckBox lms_cb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox amp_tb;
        private System.Windows.Forms.Button save_image_btn;
    }
}