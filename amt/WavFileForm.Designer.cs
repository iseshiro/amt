namespace amt
{
    partial class WavFileForm
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
            this.cancel_btn = new System.Windows.Forms.Button();
            this.full_path_tb = new System.Windows.Forms.TextBox();
            this.read_btn = new System.Windows.Forms.Button();
            this.full_path_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.read_ch_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.start_point_tb = new System.Windows.Forms.TextBox();
            this.end_point_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.end_time_tb = new System.Windows.Forms.TextBox();
            this.start_time_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.point_rb = new System.Windows.Forms.RadioButton();
            this.time_rb = new System.Windows.Forms.RadioButton();
            this.continuous_cb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(392, 78);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(52, 24);
            this.cancel_btn.TabIndex = 0;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // full_path_tb
            // 
            this.full_path_tb.Location = new System.Drawing.Point(85, 10);
            this.full_path_tb.Name = "full_path_tb";
            this.full_path_tb.Size = new System.Drawing.Size(348, 19);
            this.full_path_tb.TabIndex = 2;
            // 
            // read_btn
            // 
            this.read_btn.Location = new System.Drawing.Point(392, 48);
            this.read_btn.Name = "read_btn";
            this.read_btn.Size = new System.Drawing.Size(52, 24);
            this.read_btn.TabIndex = 1;
            this.read_btn.Text = "Read";
            this.read_btn.UseVisualStyleBackColor = true;
            this.read_btn.Click += new System.EventHandler(this.read_btn_Click);
            // 
            // full_path_btn
            // 
            this.full_path_btn.Location = new System.Drawing.Point(439, 10);
            this.full_path_btn.Name = "full_path_btn";
            this.full_path_btn.Size = new System.Drawing.Size(23, 19);
            this.full_path_btn.TabIndex = 3;
            this.full_path_btn.Text = "...";
            this.full_path_btn.UseVisualStyleBackColor = true;
            this.full_path_btn.Click += new System.EventHandler(this.full_path_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "File full path";
            // 
            // read_ch_tb
            // 
            this.read_ch_tb.Location = new System.Drawing.Point(85, 35);
            this.read_ch_tb.Name = "read_ch_tb";
            this.read_ch_tb.Size = new System.Drawing.Size(207, 19);
            this.read_ch_tb.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Read ch.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Start point";
            // 
            // start_point_tb
            // 
            this.start_point_tb.Location = new System.Drawing.Point(85, 62);
            this.start_point_tb.Name = "start_point_tb";
            this.start_point_tb.Size = new System.Drawing.Size(68, 19);
            this.start_point_tb.TabIndex = 8;
            // 
            // end_point_tb
            // 
            this.end_point_tb.Location = new System.Drawing.Point(224, 62);
            this.end_point_tb.Name = "end_point_tb";
            this.end_point_tb.Size = new System.Drawing.Size(68, 19);
            this.end_point_tb.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "End point";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "End time";
            // 
            // end_time_tb
            // 
            this.end_time_tb.Location = new System.Drawing.Point(224, 87);
            this.end_time_tb.Name = "end_time_tb";
            this.end_time_tb.Size = new System.Drawing.Size(68, 19);
            this.end_time_tb.TabIndex = 13;
            // 
            // start_time_tb
            // 
            this.start_time_tb.Location = new System.Drawing.Point(85, 87);
            this.start_time_tb.Name = "start_time_tb";
            this.start_time_tb.Size = new System.Drawing.Size(68, 19);
            this.start_time_tb.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Start time";
            // 
            // point_rb
            // 
            this.point_rb.AutoSize = true;
            this.point_rb.Checked = true;
            this.point_rb.Location = new System.Drawing.Point(305, 64);
            this.point_rb.Name = "point_rb";
            this.point_rb.Size = new System.Drawing.Size(49, 16);
            this.point_rb.TabIndex = 15;
            this.point_rb.TabStop = true;
            this.point_rb.Text = "Point";
            this.point_rb.UseVisualStyleBackColor = true;
            this.point_rb.CheckedChanged += new System.EventHandler(this.point_rb_CheckedChanged);
            // 
            // time_rb
            // 
            this.time_rb.AutoSize = true;
            this.time_rb.Location = new System.Drawing.Point(305, 88);
            this.time_rb.Name = "time_rb";
            this.time_rb.Size = new System.Drawing.Size(48, 16);
            this.time_rb.TabIndex = 16;
            this.time_rb.TabStop = true;
            this.time_rb.Text = "Time";
            this.time_rb.UseVisualStyleBackColor = true;
            // 
            // continuous_cb
            // 
            this.continuous_cb.AutoSize = true;
            this.continuous_cb.Location = new System.Drawing.Point(306, 37);
            this.continuous_cb.Name = "continuous_cb";
            this.continuous_cb.Size = new System.Drawing.Size(81, 16);
            this.continuous_cb.TabIndex = 17;
            this.continuous_cb.Text = "Continuous";
            this.continuous_cb.UseVisualStyleBackColor = true;
            // 
            // WavFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 114);
            this.Controls.Add(this.continuous_cb);
            this.Controls.Add(this.time_rb);
            this.Controls.Add(this.point_rb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.end_time_tb);
            this.Controls.Add(this.start_time_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.end_point_tb);
            this.Controls.Add(this.start_point_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.read_ch_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.full_path_btn);
            this.Controls.Add(this.full_path_tb);
            this.Controls.Add(this.read_btn);
            this.Controls.Add(this.cancel_btn);
            this.Name = "WavFileForm";
            this.Text = "Read wav file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.TextBox full_path_tb;
        private System.Windows.Forms.Button read_btn;
        private System.Windows.Forms.Button full_path_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox read_ch_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox start_point_tb;
        private System.Windows.Forms.TextBox end_point_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox end_time_tb;
        private System.Windows.Forms.TextBox start_time_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton point_rb;
        private System.Windows.Forms.RadioButton time_rb;
        private System.Windows.Forms.CheckBox continuous_cb;
    }
}