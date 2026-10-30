namespace amt
{
    partial class SaveForm
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
            this.save_btn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.input_end_tb = new System.Windows.Forms.TextBox();
            this.input_start_tb = new System.Windows.Forms.TextBox();
            this.input_filename_rule_tb = new System.Windows.Forms.TextBox();
            this.foldername_rule_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.check_btn = new System.Windows.Forms.Button();
            this.folder_no_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.input_fullpath_last_lbl = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_text_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // save_btn
            // 
            this.save_btn.Enabled = false;
            this.save_btn.Location = new System.Drawing.Point(107, 76);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(66, 23);
            this.save_btn.TabIndex = 1;
            this.save_btn.Text = "Save";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 61;
            this.label10.Text = "-";
            // 
            // input_end_tb
            // 
            this.input_end_tb.Location = new System.Drawing.Point(56, 24);
            this.input_end_tb.Name = "input_end_tb";
            this.input_end_tb.Size = new System.Drawing.Size(34, 19);
            this.input_end_tb.TabIndex = 60;
            this.input_end_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.input_end_tb.TextChanged += new System.EventHandler(this.input_end_tb_TextChanged);
            // 
            // input_start_tb
            // 
            this.input_start_tb.Location = new System.Drawing.Point(12, 23);
            this.input_start_tb.Name = "input_start_tb";
            this.input_start_tb.Size = new System.Drawing.Size(34, 19);
            this.input_start_tb.TabIndex = 59;
            this.input_start_tb.Text = "1";
            this.input_start_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.input_start_tb.TextChanged += new System.EventHandler(this.input_start_tb_TextChanged);
            // 
            // input_filename_rule_tb
            // 
            this.input_filename_rule_tb.Location = new System.Drawing.Point(133, 24);
            this.input_filename_rule_tb.Name = "input_filename_rule_tb";
            this.input_filename_rule_tb.Size = new System.Drawing.Size(100, 19);
            this.input_filename_rule_tb.TabIndex = 64;
            this.input_filename_rule_tb.Text = "infile{0:D}.wav";
            this.input_filename_rule_tb.TextChanged += new System.EventHandler(this.input_filename_rule_tb_TextChanged);
            // 
            // foldername_rule_tb
            // 
            this.foldername_rule_tb.Location = new System.Drawing.Point(250, 25);
            this.foldername_rule_tb.Name = "foldername_rule_tb";
            this.foldername_rule_tb.Size = new System.Drawing.Size(100, 19);
            this.foldername_rule_tb.TabIndex = 66;
            this.foldername_rule_tb.Text = "tsp{0:D}";
            this.foldername_rule_tb.TextChanged += new System.EventHandler(this.foldername_rule_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 12);
            this.label5.TabIndex = 65;
            this.label5.Text = "Folder name rule";
            // 
            // check_btn
            // 
            this.check_btn.Location = new System.Drawing.Point(24, 76);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(66, 23);
            this.check_btn.TabIndex = 72;
            this.check_btn.Text = "Check";
            this.check_btn.UseVisualStyleBackColor = true;
            this.check_btn.Click += new System.EventHandler(this.check_btn_Click);
            // 
            // folder_no_tb
            // 
            this.folder_no_tb.Location = new System.Drawing.Point(254, 71);
            this.folder_no_tb.Name = "folder_no_tb";
            this.folder_no_tb.Size = new System.Drawing.Size(33, 19);
            this.folder_no_tb.TabIndex = 73;
            this.folder_no_tb.Text = "1";
            this.folder_no_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.folder_no_tb.TextChanged += new System.EventHandler(this.folder_no_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 74;
            this.label2.Text = "Folder No. :";
            // 
            // input_fullpath_last_lbl
            // 
            this.input_fullpath_last_lbl.AutoSize = true;
            this.input_fullpath_last_lbl.Location = new System.Drawing.Point(13, 49);
            this.input_fullpath_last_lbl.Name = "input_fullpath_last_lbl";
            this.input_fullpath_last_lbl.Size = new System.Drawing.Size(123, 12);
            this.input_fullpath_last_lbl.TabIndex = 76;
            this.input_fullpath_last_lbl.Text = "Input Full path (Last) : ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_text_lbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 106);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(366, 23);
            this.statusStrip1.TabIndex = 77;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_text_lbl
            // 
            this.status_text_lbl.Name = "status_text_lbl";
            this.status_text_lbl.Size = new System.Drawing.Size(74, 18);
            this.status_text_lbl.Text = "Status text";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 92;
            this.label4.Text = "File name rule";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 93;
            this.label1.Text = "ch";
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 129);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.input_fullpath_last_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.folder_no_tb);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.foldername_rule_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.input_filename_rule_tb);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.input_end_tb);
            this.Controls.Add(this.input_start_tb);
            this.Controls.Add(this.save_btn);
            this.Name = "SaveForm";
            this.Text = "Save";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox input_end_tb;
        private System.Windows.Forms.TextBox input_start_tb;
        private System.Windows.Forms.TextBox input_filename_rule_tb;
        private System.Windows.Forms.TextBox foldername_rule_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button check_btn;
        private System.Windows.Forms.TextBox folder_no_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label input_fullpath_last_lbl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_text_lbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}