namespace amt
{
    partial class IR_Calc
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
            this.cali_val_tb = new System.Windows.Forms.TextBox();
            this.load_btn = new System.Windows.Forms.Button();
            this.end_ch_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.start_ch_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.data_fullpath_last_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folder_no_tb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.foldername_rule_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.input_filename_rule_tb = new System.Windows.Forms.TextBox();
            this.calc_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.source_btn = new System.Windows.Forms.Button();
            this.source_fullpath_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cali_val_tb
            // 
            this.cali_val_tb.Location = new System.Drawing.Point(12, 37);
            this.cali_val_tb.Multiline = true;
            this.cali_val_tb.Name = "cali_val_tb";
            this.cali_val_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cali_val_tb.Size = new System.Drawing.Size(83, 95);
            this.cali_val_tb.TabIndex = 0;
            // 
            // load_btn
            // 
            this.load_btn.Location = new System.Drawing.Point(16, 142);
            this.load_btn.Name = "load_btn";
            this.load_btn.Size = new System.Drawing.Size(56, 19);
            this.load_btn.TabIndex = 1;
            this.load_btn.Text = "Load";
            this.load_btn.UseVisualStyleBackColor = true;
            this.load_btn.Click += new System.EventHandler(this.load_btn_Click);
            // 
            // end_ch_tb
            // 
            this.end_ch_tb.Location = new System.Drawing.Point(69, 12);
            this.end_ch_tb.Name = "end_ch_tb";
            this.end_ch_tb.Size = new System.Drawing.Size(30, 19);
            this.end_ch_tb.TabIndex = 2;
            this.end_ch_tb.Text = "80";
            this.end_ch_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.end_ch_tb.TextChanged += new System.EventHandler(this.end_ch_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ch";
            // 
            // start_ch_tb
            // 
            this.start_ch_tb.Location = new System.Drawing.Point(16, 12);
            this.start_ch_tb.Name = "start_ch_tb";
            this.start_ch_tb.Size = new System.Drawing.Size(30, 19);
            this.start_ch_tb.TabIndex = 4;
            this.start_ch_tb.Text = "1";
            this.start_ch_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.start_ch_tb.TextChanged += new System.EventHandler(this.start_ch_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "-";
            // 
            // data_fullpath_last_lbl
            // 
            this.data_fullpath_last_lbl.AutoSize = true;
            this.data_fullpath_last_lbl.Location = new System.Drawing.Point(112, 68);
            this.data_fullpath_last_lbl.Name = "data_fullpath_last_lbl";
            this.data_fullpath_last_lbl.Size = new System.Drawing.Size(122, 12);
            this.data_fullpath_last_lbl.TabIndex = 90;
            this.data_fullpath_last_lbl.Text = "Data Full path (Last) : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 89;
            this.label3.Text = "Folder No. :";
            // 
            // folder_no_tb
            // 
            this.folder_no_tb.Location = new System.Drawing.Point(314, 49);
            this.folder_no_tb.Name = "folder_no_tb";
            this.folder_no_tb.Size = new System.Drawing.Size(33, 19);
            this.folder_no_tb.TabIndex = 88;
            this.folder_no_tb.Text = "1";
            this.folder_no_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 87;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // foldername_rule_tb
            // 
            this.foldername_rule_tb.Location = new System.Drawing.Point(252, 32);
            this.foldername_rule_tb.Name = "foldername_rule_tb";
            this.foldername_rule_tb.Size = new System.Drawing.Size(100, 19);
            this.foldername_rule_tb.TabIndex = 86;
            this.foldername_rule_tb.Text = "tsp{0:D}";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 12);
            this.label5.TabIndex = 85;
            this.label5.Text = "Folder name rule";
            // 
            // input_filename_rule_tb
            // 
            this.input_filename_rule_tb.Location = new System.Drawing.Point(135, 31);
            this.input_filename_rule_tb.Name = "input_filename_rule_tb";
            this.input_filename_rule_tb.Size = new System.Drawing.Size(100, 19);
            this.input_filename_rule_tb.TabIndex = 84;
            this.input_filename_rule_tb.Text = "infile{0:D}.wav";
            // 
            // calc_btn
            // 
            this.calc_btn.Enabled = false;
            this.calc_btn.Location = new System.Drawing.Point(114, 195);
            this.calc_btn.Name = "calc_btn";
            this.calc_btn.Size = new System.Drawing.Size(66, 23);
            this.calc_btn.TabIndex = 79;
            this.calc_btn.Text = "Calc";
            this.calc_btn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 91;
            this.label4.Text = "File name rule";
            // 
            // source_btn
            // 
            this.source_btn.Location = new System.Drawing.Point(114, 109);
            this.source_btn.Name = "source_btn";
            this.source_btn.Size = new System.Drawing.Size(66, 23);
            this.source_btn.TabIndex = 92;
            this.source_btn.Text = "Source";
            this.source_btn.UseVisualStyleBackColor = true;
            this.source_btn.Click += new System.EventHandler(this.source_btn_Click);
            // 
            // source_fullpath_lbl
            // 
            this.source_fullpath_lbl.AutoSize = true;
            this.source_fullpath_lbl.Location = new System.Drawing.Point(112, 142);
            this.source_fullpath_lbl.Name = "source_fullpath_lbl";
            this.source_fullpath_lbl.Size = new System.Drawing.Size(99, 12);
            this.source_fullpath_lbl.TabIndex = 93;
            this.source_fullpath_lbl.Text = "Source Full path : ";
            // 
            // IR_Calc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 267);
            this.Controls.Add(this.source_fullpath_lbl);
            this.Controls.Add(this.source_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.data_fullpath_last_lbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.folder_no_tb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.foldername_rule_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.input_filename_rule_tb);
            this.Controls.Add(this.calc_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.start_ch_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.end_ch_tb);
            this.Controls.Add(this.load_btn);
            this.Controls.Add(this.cali_val_tb);
            this.Name = "IR_Calc";
            this.Text = "Impulse Response Calculation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cali_val_tb;
        private System.Windows.Forms.Button load_btn;
        private System.Windows.Forms.TextBox end_ch_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox start_ch_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label data_fullpath_last_lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox folder_no_tb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox foldername_rule_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox input_filename_rule_tb;
        private System.Windows.Forms.Button calc_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button source_btn;
        private System.Windows.Forms.Label source_fullpath_lbl;

    }
}