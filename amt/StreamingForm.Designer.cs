namespace amt
{
    partial class StreamingForm
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
            this.filename_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.check_btn = new System.Windows.Forms.Button();
            this.folder_brows_btn = new System.Windows.Forms.Button();
            this.end_tb = new System.Windows.Forms.TextBox();
            this.start_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.audio_input_rb = new System.Windows.Forms.RadioButton();
            this.audio_output_rb = new System.Windows.Forms.RadioButton();
            this.set_btn = new System.Windows.Forms.Button();
            this.filestream_lbl = new System.Windows.Forms.Label();
            this.amp_adjust_cb = new System.Windows.Forms.CheckBox();
            this.amp_adjust_tb = new System.Windows.Forms.TextBox();
            this.amp_adjust_lbl = new System.Windows.Forms.Label();
            this.amp_adj_err_lbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.work_folder_lbl = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dleng_lbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.net_trig_cb = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // filename_tb
            // 
            this.filename_tb.Location = new System.Drawing.Point(21, 163);
            this.filename_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.filename_tb.Name = "filename_tb";
            this.filename_tb.Size = new System.Drawing.Size(206, 25);
            this.filename_tb.TabIndex = 65;
            this.filename_tb.Text = "filename{0:D}.wav";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 18);
            this.label1.TabIndex = 66;
            this.label1.Text = "Wav File Name Format";
            // 
            // check_btn
            // 
            this.check_btn.Location = new System.Drawing.Point(158, 320);
            this.check_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(108, 44);
            this.check_btn.TabIndex = 67;
            this.check_btn.Text = "Check";
            this.check_btn.Click += new System.EventHandler(this.check_btn_Click);
            // 
            // folder_brows_btn
            // 
            this.folder_brows_btn.Location = new System.Drawing.Point(23, 320);
            this.folder_brows_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.folder_brows_btn.Name = "folder_brows_btn";
            this.folder_brows_btn.Size = new System.Drawing.Size(108, 44);
            this.folder_brows_btn.TabIndex = 49;
            this.folder_brows_btn.Text = "Set folder";
            this.folder_brows_btn.Click += new System.EventHandler(this.set_imp_folder_btn_Click);
            // 
            // end_tb
            // 
            this.end_tb.Location = new System.Drawing.Point(480, 42);
            this.end_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.end_tb.Name = "end_tb";
            this.end_tb.Size = new System.Drawing.Size(54, 25);
            this.end_tb.TabIndex = 58;
            this.end_tb.TextChanged += new System.EventHandler(this.end_tb_TextChanged);
            // 
            // start_tb
            // 
            this.start_tb.Location = new System.Drawing.Point(412, 42);
            this.start_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.start_tb.Name = "start_tb";
            this.start_tb.Size = new System.Drawing.Size(54, 25);
            this.start_tb.TabIndex = 57;
            this.start_tb.Text = "1";
            this.start_tb.TextChanged += new System.EventHandler(this.start_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(490, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 18);
            this.label5.TabIndex = 56;
            this.label5.Text = "End";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 18);
            this.label4.TabIndex = 55;
            this.label4.Text = "Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 54;
            this.label3.Text = "Channel ";
            // 
            // audio_input_rb
            // 
            this.audio_input_rb.AutoSize = true;
            this.audio_input_rb.Checked = true;
            this.audio_input_rb.Location = new System.Drawing.Point(21, 34);
            this.audio_input_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.audio_input_rb.Name = "audio_input_rb";
            this.audio_input_rb.Size = new System.Drawing.Size(70, 22);
            this.audio_input_rb.TabIndex = 63;
            this.audio_input_rb.TabStop = true;
            this.audio_input_rb.Text = "Input";
            this.audio_input_rb.UseVisualStyleBackColor = true;
            // 
            // audio_output_rb
            // 
            this.audio_output_rb.AutoSize = true;
            this.audio_output_rb.Location = new System.Drawing.Point(113, 34);
            this.audio_output_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.audio_output_rb.Name = "audio_output_rb";
            this.audio_output_rb.Size = new System.Drawing.Size(85, 22);
            this.audio_output_rb.TabIndex = 64;
            this.audio_output_rb.Text = "Output";
            this.audio_output_rb.UseVisualStyleBackColor = true;
            this.audio_output_rb.CheckedChanged += new System.EventHandler(this.output_rb_CheckedChanged);
            // 
            // set_btn
            // 
            this.set_btn.Enabled = false;
            this.set_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.set_btn.Location = new System.Drawing.Point(348, 320);
            this.set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_btn.Name = "set_btn";
            this.set_btn.Size = new System.Drawing.Size(98, 44);
            this.set_btn.TabIndex = 65;
            this.set_btn.Text = "OK";
            this.set_btn.Click += new System.EventHandler(this.set_btn_Click);
            // 
            // filestream_lbl
            // 
            this.filestream_lbl.AutoSize = true;
            this.filestream_lbl.Location = new System.Drawing.Point(20, 104);
            this.filestream_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.filestream_lbl.Name = "filestream_lbl";
            this.filestream_lbl.Size = new System.Drawing.Size(239, 18);
            this.filestream_lbl.TabIndex = 69;
            this.filestream_lbl.Text = "Audio in ---> File stream write";
            // 
            // amp_adjust_cb
            // 
            this.amp_adjust_cb.AutoSize = true;
            this.amp_adjust_cb.Location = new System.Drawing.Point(308, 206);
            this.amp_adjust_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.amp_adjust_cb.Name = "amp_adjust_cb";
            this.amp_adjust_cb.Size = new System.Drawing.Size(158, 22);
            this.amp_adjust_cb.TabIndex = 70;
            this.amp_adjust_cb.Text = "Amplitude adjust";
            this.amp_adjust_cb.UseVisualStyleBackColor = true;
            this.amp_adjust_cb.CheckedChanged += new System.EventHandler(this.amp_adjust_cb_CheckedChanged);
            // 
            // amp_adjust_tb
            // 
            this.amp_adjust_tb.Enabled = false;
            this.amp_adjust_tb.Location = new System.Drawing.Point(398, 236);
            this.amp_adjust_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.amp_adjust_tb.Name = "amp_adjust_tb";
            this.amp_adjust_tb.Size = new System.Drawing.Size(91, 25);
            this.amp_adjust_tb.TabIndex = 72;
            this.amp_adjust_tb.Text = "1";
            this.amp_adjust_tb.TextChanged += new System.EventHandler(this.amp_adjust_tb_TextChanged);
            // 
            // amp_adjust_lbl
            // 
            this.amp_adjust_lbl.AutoSize = true;
            this.amp_adjust_lbl.Enabled = false;
            this.amp_adjust_lbl.Location = new System.Drawing.Point(338, 240);
            this.amp_adjust_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.amp_adjust_lbl.Name = "amp_adjust_lbl";
            this.amp_adjust_lbl.Size = new System.Drawing.Size(45, 18);
            this.amp_adjust_lbl.TabIndex = 71;
            this.amp_adjust_lbl.Text = "Amp.";
            // 
            // amp_adj_err_lbl
            // 
            this.amp_adj_err_lbl.AutoSize = true;
            this.amp_adj_err_lbl.Enabled = false;
            this.amp_adj_err_lbl.Location = new System.Drawing.Point(355, 268);
            this.amp_adj_err_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.amp_adj_err_lbl.Name = "amp_adj_err_lbl";
            this.amp_adj_err_lbl.Size = new System.Drawing.Size(54, 18);
            this.amp_adj_err_lbl.TabIndex = 73;
            this.amp_adj_err_lbl.Text = "Error :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(355, 270);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 18);
            this.label6.TabIndex = 73;
            this.label6.Text = "Set : 0 < amp < 10";
            // 
            // work_folder_lbl
            // 
            this.work_folder_lbl.Location = new System.Drawing.Point(18, 200);
            this.work_folder_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.work_folder_lbl.Name = "work_folder_lbl";
            this.work_folder_lbl.Size = new System.Drawing.Size(278, 87);
            this.work_folder_lbl.TabIndex = 74;
            this.work_folder_lbl.Text = "Work Folder =";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dleng_lbl);
            this.groupBox4.Location = new System.Drawing.Point(322, 87);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Size = new System.Drawing.Size(235, 69);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buf Size";
            // 
            // dleng_lbl
            // 
            this.dleng_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dleng_lbl.Location = new System.Drawing.Point(15, 28);
            this.dleng_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.dleng_lbl.Name = "dleng_lbl";
            this.dleng_lbl.Size = new System.Drawing.Size(110, 27);
            this.dleng_lbl.TabIndex = 35;
            this.dleng_lbl.Text = "131072";
            this.dleng_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.audio_output_rb);
            this.groupBox2.Controls.Add(this.audio_input_rb);
            this.groupBox2.Location = new System.Drawing.Point(36, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 79);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Audio signal";
            // 
            // net_trig_cb
            // 
            this.net_trig_cb.AutoSize = true;
            this.net_trig_cb.Location = new System.Drawing.Point(308, 170);
            this.net_trig_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.net_trig_cb.Name = "net_trig_cb";
            this.net_trig_cb.Size = new System.Drawing.Size(148, 22);
            this.net_trig_cb.TabIndex = 80;
            this.net_trig_cb.Text = "Network trigger";
            this.net_trig_cb.UseVisualStyleBackColor = true;
            this.net_trig_cb.CheckedChanged += new System.EventHandler(this.net_trig_cb_CheckedChanged);
            // 
            // StreamingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 381);
            this.Controls.Add(this.net_trig_cb);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.amp_adj_err_lbl);
            this.Controls.Add(this.amp_adjust_tb);
            this.Controls.Add(this.amp_adjust_lbl);
            this.Controls.Add(this.amp_adjust_cb);
            this.Controls.Add(this.filestream_lbl);
            this.Controls.Add(this.filename_tb);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folder_brows_btn);
            this.Controls.Add(this.set_btn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.end_tb);
            this.Controls.Add(this.start_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.work_folder_lbl);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StreamingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Streaming";
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button folder_brows_btn;
        private System.Windows.Forms.TextBox end_tb;
        private System.Windows.Forms.TextBox start_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton audio_input_rb;
        private System.Windows.Forms.RadioButton audio_output_rb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button check_btn;
        private System.Windows.Forms.TextBox filename_tb;
        private System.Windows.Forms.Button set_btn;
        private System.Windows.Forms.Label filestream_lbl;
        private System.Windows.Forms.CheckBox amp_adjust_cb;
        private System.Windows.Forms.TextBox amp_adjust_tb;
        private System.Windows.Forms.Label amp_adjust_lbl;
        private System.Windows.Forms.Label amp_adj_err_lbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label work_folder_lbl;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label dleng_lbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox net_trig_cb;
    }
}