namespace amt
{
    partial class SPGainForm
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
            this.close_btn = new System.Windows.Forms.Button();
            this.gain_db_lbl = new System.Windows.Forms.Label();
            this.gain_ctrl_trackBar = new System.Windows.Forms.TrackBar();
            this.mute_cb = new System.Windows.Forms.CheckBox();
            this.min_db_lbl = new System.Windows.Forms.Label();
            this.max_db_lbl = new System.Windows.Forms.Label();
            this.max_cb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.range_cb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.set_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gain_ctrl_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(60, 447);
            this.close_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(85, 34);
            this.close_btn.TabIndex = 125;
            this.close_btn.Text = "Close";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // gain_db_lbl
            // 
            this.gain_db_lbl.AutoSize = true;
            this.gain_db_lbl.Location = new System.Drawing.Point(130, 123);
            this.gain_db_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.gain_db_lbl.Name = "gain_db_lbl";
            this.gain_db_lbl.Size = new System.Drawing.Size(66, 18);
            this.gain_db_lbl.TabIndex = 128;
            this.gain_db_lbl.Text = "-127.25";
            // 
            // gain_ctrl_trackBar
            // 
            this.gain_ctrl_trackBar.Location = new System.Drawing.Point(60, 15);
            this.gain_ctrl_trackBar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gain_ctrl_trackBar.Maximum = 582;
            this.gain_ctrl_trackBar.Name = "gain_ctrl_trackBar";
            this.gain_ctrl_trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.gain_ctrl_trackBar.Size = new System.Drawing.Size(69, 246);
            this.gain_ctrl_trackBar.TabIndex = 129;
            this.gain_ctrl_trackBar.TickFrequency = 32;
            this.gain_ctrl_trackBar.Scroll += new System.EventHandler(this.gain_ctrl_trackBar_Scroll);
            // 
            // mute_cb
            // 
            this.mute_cb.AutoSize = true;
            this.mute_cb.Location = new System.Drawing.Point(18, 356);
            this.mute_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mute_cb.Name = "mute_cb";
            this.mute_cb.Size = new System.Drawing.Size(80, 22);
            this.mute_cb.TabIndex = 130;
            this.mute_cb.Text = "MUTE";
            this.mute_cb.UseVisualStyleBackColor = true;
            this.mute_cb.CheckedChanged += new System.EventHandler(this.mute_cb_CheckedChanged);
            // 
            // min_db_lbl
            // 
            this.min_db_lbl.AutoSize = true;
            this.min_db_lbl.Location = new System.Drawing.Point(118, 234);
            this.min_db_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.min_db_lbl.Name = "min_db_lbl";
            this.min_db_lbl.Size = new System.Drawing.Size(86, 18);
            this.min_db_lbl.TabIndex = 131;
            this.min_db_lbl.Text = "-127.25dB";
            // 
            // max_db_lbl
            // 
            this.max_db_lbl.AutoSize = true;
            this.max_db_lbl.Location = new System.Drawing.Point(118, 20);
            this.max_db_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.max_db_lbl.Name = "max_db_lbl";
            this.max_db_lbl.Size = new System.Drawing.Size(86, 18);
            this.max_db_lbl.TabIndex = 132;
            this.max_db_lbl.Text = "-127.25dB";
            // 
            // max_cb
            // 
            this.max_cb.FormattingEnabled = true;
            this.max_cb.Items.AddRange(new object[] {
            "-20",
            "-10",
            " 0",
            "10",
            "17.25"});
            this.max_cb.Location = new System.Drawing.Point(78, 262);
            this.max_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.max_cb.Name = "max_cb";
            this.max_cb.Size = new System.Drawing.Size(76, 26);
            this.max_cb.TabIndex = 133;
            this.max_cb.SelectedIndexChanged += new System.EventHandler(this.max_cb_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 268);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 18);
            this.label3.TabIndex = 134;
            this.label3.Text = "Max";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 309);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 18);
            this.label4.TabIndex = 135;
            this.label4.Text = "Range";
            // 
            // range_cb
            // 
            this.range_cb.FormattingEnabled = true;
            this.range_cb.Items.AddRange(new object[] {
            "100",
            "80",
            "60",
            "40",
            "20"});
            this.range_cb.Location = new System.Drawing.Point(78, 304);
            this.range_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.range_cb.Name = "range_cb";
            this.range_cb.Size = new System.Drawing.Size(76, 26);
            this.range_cb.TabIndex = 136;
            this.range_cb.SelectedIndexChanged += new System.EventHandler(this.range_cb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 18);
            this.label1.TabIndex = 137;
            this.label1.Text = "dB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 18);
            this.label2.TabIndex = 138;
            this.label2.Text = "dB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 309);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 18);
            this.label5.TabIndex = 139;
            this.label5.Text = "dB";
            // 
            // set_btn
            // 
            this.set_btn.Location = new System.Drawing.Point(60, 396);
            this.set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_btn.Name = "set_btn";
            this.set_btn.Size = new System.Drawing.Size(85, 34);
            this.set_btn.TabIndex = 140;
            this.set_btn.Text = "Set";
            this.set_btn.UseVisualStyleBackColor = true;
            this.set_btn.Click += new System.EventHandler(this.set_btn_Click);
            // 
            // SPGainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 531);
            this.ControlBox = false;
            this.Controls.Add(this.set_btn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gain_db_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.range_cb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.max_cb);
            this.Controls.Add(this.max_db_lbl);
            this.Controls.Add(this.min_db_lbl);
            this.Controls.Add(this.mute_cb);
            this.Controls.Add(this.gain_ctrl_trackBar);
            this.Controls.Add(this.close_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "SPGainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sp. Amp. Gain";
            ((System.ComponentModel.ISupportInitialize)(this.gain_ctrl_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Label gain_db_lbl;
        private System.Windows.Forms.TrackBar gain_ctrl_trackBar;
        private System.Windows.Forms.CheckBox mute_cb;
        private System.Windows.Forms.Label min_db_lbl;
        private System.Windows.Forms.Label max_db_lbl;
        private System.Windows.Forms.ComboBox max_cb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox range_cb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button set_btn;
    }
}