namespace amt
{
    partial class WavConvert
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
            this.ch2to1x2_rb = new System.Windows.Forms.RadioButton();
            this.ch1x2to2_rb = new System.Windows.Forms.RadioButton();
            this.start_btn = new System.Windows.Forms.Button();
            this.lr_file_btn = new System.Windows.Forms.Button();
            this.l_file_btn = new System.Windows.Forms.Button();
            this.r_file_btn = new System.Windows.Forms.Button();
            this.check_btn = new System.Windows.Forms.Button();
            this.set_wav_file_btn = new System.Windows.Forms.Button();
            this.resample_start_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.use_sinc_func_cb = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ch2to1x2_rb
            // 
            this.ch2to1x2_rb.AutoSize = true;
            this.ch2to1x2_rb.Checked = true;
            this.ch2to1x2_rb.Location = new System.Drawing.Point(20, 32);
            this.ch2to1x2_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ch2to1x2_rb.Name = "ch2to1x2_rb";
            this.ch2to1x2_rb.Size = new System.Drawing.Size(154, 22);
            this.ch2to1x2_rb.TabIndex = 6;
            this.ch2to1x2_rb.TabStop = true;
            this.ch2to1x2_rb.Text = "2ch --> 1ch X 2";
            this.ch2to1x2_rb.UseVisualStyleBackColor = true;
            // 
            // ch1x2to2_rb
            // 
            this.ch1x2to2_rb.AutoSize = true;
            this.ch1x2to2_rb.Location = new System.Drawing.Point(20, 64);
            this.ch1x2to2_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ch1x2to2_rb.Name = "ch1x2to2_rb";
            this.ch1x2to2_rb.Size = new System.Drawing.Size(154, 22);
            this.ch1x2to2_rb.TabIndex = 7;
            this.ch1x2to2_rb.TabStop = true;
            this.ch1x2to2_rb.Text = "1ch X 2 --> 2ch";
            this.ch1x2to2_rb.UseVisualStyleBackColor = true;
            // 
            // start_btn
            // 
            this.start_btn.Enabled = false;
            this.start_btn.Location = new System.Drawing.Point(127, 104);
            this.start_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(92, 33);
            this.start_btn.TabIndex = 8;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // lr_file_btn
            // 
            this.lr_file_btn.Location = new System.Drawing.Point(247, 104);
            this.lr_file_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lr_file_btn.Name = "lr_file_btn";
            this.lr_file_btn.Size = new System.Drawing.Size(95, 33);
            this.lr_file_btn.TabIndex = 9;
            this.lr_file_btn.Text = "LR File";
            this.lr_file_btn.UseVisualStyleBackColor = true;
            this.lr_file_btn.Click += new System.EventHandler(this.lr_file_btn_Click);
            // 
            // l_file_btn
            // 
            this.l_file_btn.Location = new System.Drawing.Point(247, 18);
            this.l_file_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.l_file_btn.Name = "l_file_btn";
            this.l_file_btn.Size = new System.Drawing.Size(95, 33);
            this.l_file_btn.TabIndex = 11;
            this.l_file_btn.Text = "L File";
            this.l_file_btn.UseVisualStyleBackColor = true;
            this.l_file_btn.Click += new System.EventHandler(this.l_file_btn_Click);
            // 
            // r_file_btn
            // 
            this.r_file_btn.Location = new System.Drawing.Point(247, 60);
            this.r_file_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.r_file_btn.Name = "r_file_btn";
            this.r_file_btn.Size = new System.Drawing.Size(95, 33);
            this.r_file_btn.TabIndex = 12;
            this.r_file_btn.Text = "R File";
            this.r_file_btn.UseVisualStyleBackColor = true;
            this.r_file_btn.Click += new System.EventHandler(this.r_file_btn_Click);
            // 
            // check_btn
            // 
            this.check_btn.Location = new System.Drawing.Point(20, 104);
            this.check_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(92, 33);
            this.check_btn.TabIndex = 15;
            this.check_btn.Text = "Check";
            this.check_btn.UseVisualStyleBackColor = true;
            this.check_btn.Click += new System.EventHandler(this.check_btn_Click);
            // 
            // set_wav_file_btn
            // 
            this.set_wav_file_btn.Location = new System.Drawing.Point(424, 32);
            this.set_wav_file_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_wav_file_btn.Name = "set_wav_file_btn";
            this.set_wav_file_btn.Size = new System.Drawing.Size(235, 33);
            this.set_wav_file_btn.TabIndex = 16;
            this.set_wav_file_btn.Text = "Set In/Out File";
            this.set_wav_file_btn.UseVisualStyleBackColor = true;
            this.set_wav_file_btn.Click += new System.EventHandler(this.set_wav_file_btn_Click);
            // 
            // resample_start_btn
            // 
            this.resample_start_btn.Enabled = false;
            this.resample_start_btn.Location = new System.Drawing.Point(424, 117);
            this.resample_start_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.resample_start_btn.Name = "resample_start_btn";
            this.resample_start_btn.Size = new System.Drawing.Size(235, 33);
            this.resample_start_btn.TabIndex = 17;
            this.resample_start_btn.Text = "Start";
            this.resample_start_btn.UseVisualStyleBackColor = true;
            this.resample_start_btn.Click += new System.EventHandler(this.resample_start_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Resample (44.1kHz <--> 48kHz)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 165);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(728, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 22);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // use_sinc_func_cb
            // 
            this.use_sinc_func_cb.AutoSize = true;
            this.use_sinc_func_cb.Checked = true;
            this.use_sinc_func_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.use_sinc_func_cb.Location = new System.Drawing.Point(439, 79);
            this.use_sinc_func_cb.Name = "use_sinc_func_cb";
            this.use_sinc_func_cb.Size = new System.Drawing.Size(177, 22);
            this.use_sinc_func_cb.TabIndex = 22;
            this.use_sinc_func_cb.Text = "Use SINC Function";
            this.use_sinc_func_cb.UseVisualStyleBackColor = true;
            // 
            // WavConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 187);
            this.Controls.Add(this.use_sinc_func_cb);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resample_start_btn);
            this.Controls.Add(this.set_wav_file_btn);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.r_file_btn);
            this.Controls.Add(this.l_file_btn);
            this.Controls.Add(this.lr_file_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.ch1x2to2_rb);
            this.Controls.Add(this.ch2to1x2_rb);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WavConvert";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "WavConvert";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ch2to1x2_rb;
        private System.Windows.Forms.RadioButton ch1x2to2_rb;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button lr_file_btn;
        private System.Windows.Forms.Button l_file_btn;
        private System.Windows.Forms.Button r_file_btn;
        private System.Windows.Forms.Button check_btn;
        private System.Windows.Forms.Button set_wav_file_btn;
        private System.Windows.Forms.Button resample_start_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox use_sinc_func_cb;
    }
}