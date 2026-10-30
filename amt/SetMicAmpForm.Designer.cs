namespace amt
{
    partial class SetMicAmpForm
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
            this.led3_cb = new System.Windows.Forms.CheckBox();
            this.led2_cb = new System.Windows.Forms.CheckBox();
            this.led1_cb = new System.Windows.Forms.CheckBox();
            this.led4_cb = new System.Windows.Forms.CheckBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gain_tb = new System.Windows.Forms.TextBox();
            this.madi_no_cb = new System.Windows.Forms.ComboBox();
            this.board_no_cb = new System.Windows.Forms.ComboBox();
            this.ch_no_cb = new System.Windows.Forms.ComboBox();
            this.overload_cb = new System.Windows.Forms.ComboBox();
            this.cm_servo_cb = new System.Windows.Forms.ComboBox();
            this.dc_servo_cb = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // led3_cb
            // 
            this.led3_cb.AutoSize = true;
            this.led3_cb.Location = new System.Drawing.Point(357, 86);
            this.led3_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.led3_cb.Name = "led3_cb";
            this.led3_cb.Size = new System.Drawing.Size(80, 22);
            this.led3_cb.TabIndex = 5;
            this.led3_cb.Text = "LED 3";
            this.led3_cb.UseVisualStyleBackColor = true;
            // 
            // led2_cb
            // 
            this.led2_cb.AutoSize = true;
            this.led2_cb.Checked = true;
            this.led2_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.led2_cb.Location = new System.Drawing.Point(357, 53);
            this.led2_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.led2_cb.Name = "led2_cb";
            this.led2_cb.Size = new System.Drawing.Size(80, 22);
            this.led2_cb.TabIndex = 4;
            this.led2_cb.Text = "LED 2";
            this.led2_cb.UseVisualStyleBackColor = true;
            // 
            // led1_cb
            // 
            this.led1_cb.AutoSize = true;
            this.led1_cb.Location = new System.Drawing.Point(357, 20);
            this.led1_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.led1_cb.Name = "led1_cb";
            this.led1_cb.Size = new System.Drawing.Size(80, 22);
            this.led1_cb.TabIndex = 3;
            this.led1_cb.Text = "LED 1";
            this.led1_cb.UseVisualStyleBackColor = true;
            // 
            // led4_cb
            // 
            this.led4_cb.AutoSize = true;
            this.led4_cb.Checked = true;
            this.led4_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.led4_cb.Location = new System.Drawing.Point(357, 119);
            this.led4_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.led4_cb.Name = "led4_cb";
            this.led4_cb.Size = new System.Drawing.Size(80, 22);
            this.led4_cb.TabIndex = 6;
            this.led4_cb.Text = "LED 4";
            this.led4_cb.UseVisualStyleBackColor = true;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(226, 161);
            this.send_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(125, 34);
            this.send_btn.TabIndex = 7;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 170);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "0-63";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Gain";
            // 
            // gain_tb
            // 
            this.gain_tb.Location = new System.Drawing.Point(91, 166);
            this.gain_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gain_tb.Name = "gain_tb";
            this.gain_tb.Size = new System.Drawing.Size(59, 25);
            this.gain_tb.TabIndex = 11;
            this.gain_tb.Text = "25";
            this.gain_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // madi_no_cb
            // 
            this.madi_no_cb.FormattingEnabled = true;
            this.madi_no_cb.Items.AddRange(new object[] {
            "MADI 1",
            "MADI 2"});
            this.madi_no_cb.Location = new System.Drawing.Point(21, 18);
            this.madi_no_cb.Name = "madi_no_cb";
            this.madi_no_cb.Size = new System.Drawing.Size(104, 26);
            this.madi_no_cb.TabIndex = 14;
            // 
            // board_no_cb
            // 
            this.board_no_cb.FormattingEnabled = true;
            this.board_no_cb.Items.AddRange(new object[] {
            "Board 1",
            "Board 2",
            "Board 3",
            "Board 4",
            "Board 5",
            "Board 6",
            "Board 7",
            "Board 8"});
            this.board_no_cb.Location = new System.Drawing.Point(21, 61);
            this.board_no_cb.Name = "board_no_cb";
            this.board_no_cb.Size = new System.Drawing.Size(104, 26);
            this.board_no_cb.TabIndex = 15;
            // 
            // ch_no_cb
            // 
            this.ch_no_cb.FormattingEnabled = true;
            this.ch_no_cb.Items.AddRange(new object[] {
            "Mic. 1",
            "Mic. 2",
            "Mic. 3",
            "Mic. 4",
            "Mic. 5",
            "Mic. 6",
            "Mic. 7",
            "Mic. 8"});
            this.ch_no_cb.Location = new System.Drawing.Point(21, 104);
            this.ch_no_cb.Name = "ch_no_cb";
            this.ch_no_cb.Size = new System.Drawing.Size(104, 26);
            this.ch_no_cb.TabIndex = 16;
            // 
            // overload_cb
            // 
            this.overload_cb.FormattingEnabled = true;
            this.overload_cb.Items.AddRange(new object[] {
            "Overload 5.1V",
            "Overload 4.0V"});
            this.overload_cb.Location = new System.Drawing.Point(155, 104);
            this.overload_cb.Name = "overload_cb";
            this.overload_cb.Size = new System.Drawing.Size(175, 26);
            this.overload_cb.TabIndex = 19;
            // 
            // cm_servo_cb
            // 
            this.cm_servo_cb.FormattingEnabled = true;
            this.cm_servo_cb.Items.AddRange(new object[] {
            "CM Servo OFF",
            "CM Servo ON"});
            this.cm_servo_cb.Location = new System.Drawing.Point(155, 61);
            this.cm_servo_cb.Name = "cm_servo_cb";
            this.cm_servo_cb.Size = new System.Drawing.Size(175, 26);
            this.cm_servo_cb.TabIndex = 18;
            // 
            // dc_servo_cb
            // 
            this.dc_servo_cb.FormattingEnabled = true;
            this.dc_servo_cb.Items.AddRange(new object[] {
            "DC Servo ON",
            "DC Servo OFF"});
            this.dc_servo_cb.Location = new System.Drawing.Point(155, 18);
            this.dc_servo_cb.Name = "dc_servo_cb";
            this.dc_servo_cb.Size = new System.Drawing.Size(175, 26);
            this.dc_servo_cb.TabIndex = 17;
            // 
            // SetMicAmpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 223);
            this.Controls.Add(this.overload_cb);
            this.Controls.Add(this.cm_servo_cb);
            this.Controls.Add(this.dc_servo_cb);
            this.Controls.Add(this.ch_no_cb);
            this.Controls.Add(this.board_no_cb);
            this.Controls.Add(this.madi_no_cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gain_tb);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.led4_cb);
            this.Controls.Add(this.led3_cb);
            this.Controls.Add(this.led2_cb);
            this.Controls.Add(this.led1_cb);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "SetMicAmpForm";
            this.Text = "Set Mic Amp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox led3_cb;
        private System.Windows.Forms.CheckBox led2_cb;
        private System.Windows.Forms.CheckBox led1_cb;
        private System.Windows.Forms.CheckBox led4_cb;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gain_tb;
        private System.Windows.Forms.ComboBox madi_no_cb;
        private System.Windows.Forms.ComboBox board_no_cb;
        private System.Windows.Forms.ComboBox ch_no_cb;
        private System.Windows.Forms.ComboBox overload_cb;
        private System.Windows.Forms.ComboBox cm_servo_cb;
        private System.Windows.Forms.ComboBox dc_servo_cb;
    }
}