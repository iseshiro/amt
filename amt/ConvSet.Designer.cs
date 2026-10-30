namespace amt
{
    partial class ConvSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvSet));
            this.imp_path_btn = new System.Windows.Forms.Button();
            this.set_btn = new System.Windows.Forms.Button();
            this.in_ch_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fft_level_lbl = new System.Windows.Forms.Label();
            this.fft_level_ud = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.out_ch_tb = new System.Windows.Forms.TextBox();
            this.single_rb = new System.Windows.Forms.RadioButton();
            this.multiple_rb = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.src_path_btn = new System.Windows.Forms.Button();
            this.IR_listView = new System.Windows.Forms.ListView();
            this.close_btn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CPUCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ASIOBufSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FFTPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.message_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.src_listView = new System.Windows.Forms.ListView();
            this.selected_src_lbl = new System.Windows.Forms.Label();
            this.check_ir_btn = new System.Windows.Forms.Button();
            this.inv_filt_cb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imp_path_btn
            // 
            this.imp_path_btn.Location = new System.Drawing.Point(20, 116);
            this.imp_path_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.imp_path_btn.Name = "imp_path_btn";
            this.imp_path_btn.Size = new System.Drawing.Size(153, 34);
            this.imp_path_btn.TabIndex = 109;
            this.imp_path_btn.Text = "Add IR";
            this.imp_path_btn.UseVisualStyleBackColor = true;
            this.imp_path_btn.Click += new System.EventHandler(this.imp_path_btn_Click);
            // 
            // set_btn
            // 
            this.set_btn.Enabled = false;
            this.set_btn.Location = new System.Drawing.Point(312, 66);
            this.set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_btn.Name = "set_btn";
            this.set_btn.Size = new System.Drawing.Size(118, 34);
            this.set_btn.TabIndex = 100;
            this.set_btn.Text = "Set";
            this.set_btn.UseVisualStyleBackColor = true;
            this.set_btn.Click += new System.EventHandler(this.set_btn_Click);
            // 
            // in_ch_tb
            // 
            this.in_ch_tb.Location = new System.Drawing.Point(312, 12);
            this.in_ch_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.in_ch_tb.Name = "in_ch_tb";
            this.in_ch_tb.Size = new System.Drawing.Size(72, 25);
            this.in_ch_tb.TabIndex = 121;
            this.in_ch_tb.TextChanged += new System.EventHandler(this.in_ch_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 123;
            this.label2.Text = "Input ch.";
            // 
            // fft_level_lbl
            // 
            this.fft_level_lbl.AutoSize = true;
            this.fft_level_lbl.Location = new System.Drawing.Point(20, 16);
            this.fft_level_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.fft_level_lbl.Name = "fft_level_lbl";
            this.fft_level_lbl.Size = new System.Drawing.Size(85, 18);
            this.fft_level_lbl.TabIndex = 126;
            this.fft_level_lbl.Text = "FFT Level";
            this.fft_level_lbl.Click += new System.EventHandler(this.fft_level_lbl_Click);
            // 
            // fft_level_ud
            // 
            this.fft_level_ud.Location = new System.Drawing.Point(125, 12);
            this.fft_level_ud.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.fft_level_ud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fft_level_ud.Name = "fft_level_ud";
            this.fft_level_ud.Size = new System.Drawing.Size(75, 25);
            this.fft_level_ud.TabIndex = 143;
            this.fft_level_ud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fft_level_ud.ValueChanged += new System.EventHandler(this.fft_level_ud_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(407, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 147;
            this.label1.Text = "Output ch.";
            // 
            // out_ch_tb
            // 
            this.out_ch_tb.Location = new System.Drawing.Point(512, 12);
            this.out_ch_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.out_ch_tb.Name = "out_ch_tb";
            this.out_ch_tb.Size = new System.Drawing.Size(72, 25);
            this.out_ch_tb.TabIndex = 146;
            this.out_ch_tb.TextChanged += new System.EventHandler(this.out_ch_tb_TextChanged);
            // 
            // single_rb
            // 
            this.single_rb.AutoSize = true;
            this.single_rb.Location = new System.Drawing.Point(23, 21);
            this.single_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.single_rb.Name = "single_rb";
            this.single_rb.Size = new System.Drawing.Size(78, 22);
            this.single_rb.TabIndex = 156;
            this.single_rb.TabStop = true;
            this.single_rb.Text = "Single";
            this.single_rb.UseVisualStyleBackColor = true;
            this.single_rb.CheckedChanged += new System.EventHandler(this.single_rb_CheckedChanged);
            // 
            // multiple_rb
            // 
            this.multiple_rb.AutoSize = true;
            this.multiple_rb.Checked = true;
            this.multiple_rb.Location = new System.Drawing.Point(123, 21);
            this.multiple_rb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.multiple_rb.Name = "multiple_rb";
            this.multiple_rb.Size = new System.Drawing.Size(91, 22);
            this.multiple_rb.TabIndex = 157;
            this.multiple_rb.TabStop = true;
            this.multiple_rb.Text = "Multiple";
            this.multiple_rb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.single_rb);
            this.groupBox1.Controls.Add(this.multiple_rb);
            this.groupBox1.Location = new System.Drawing.Point(20, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(253, 58);
            this.groupBox1.TabIndex = 158;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conv. mode";
            // 
            // src_path_btn
            // 
            this.src_path_btn.Location = new System.Drawing.Point(20, 272);
            this.src_path_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_path_btn.Name = "src_path_btn";
            this.src_path_btn.Size = new System.Drawing.Size(153, 34);
            this.src_path_btn.TabIndex = 164;
            this.src_path_btn.Text = "Add Source";
            this.src_path_btn.UseVisualStyleBackColor = true;
            this.src_path_btn.Click += new System.EventHandler(this.src_path_btn_Click);
            // 
            // IR_listView
            // 
            this.IR_listView.Location = new System.Drawing.Point(20, 159);
            this.IR_listView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.IR_listView.Name = "IR_listView";
            this.IR_listView.Size = new System.Drawing.Size(586, 102);
            this.IR_listView.TabIndex = 165;
            this.IR_listView.UseCompatibleStateImageBehavior = false;
            this.IR_listView.View = System.Windows.Forms.View.Details;
            this.IR_listView.SelectedIndexChanged += new System.EventHandler(this.IR_listView_SelectedIndexChanged);
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(470, 66);
            this.close_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(117, 34);
            this.close_btn.TabIndex = 166;
            this.close_btn.Text = "Close";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripDropDownButton1,
            this.message_toolStripStatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            this.statusStrip1.Size = new System.Drawing.Size(634, 30);
            this.statusStrip1.TabIndex = 167;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 25);
            this.toolStripStatusLabel1.Text = "Info :";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CPUCoreToolStripMenuItem,
            this.ASIOBufSizeToolStripMenuItem,
            this.FFTPointToolStripMenuItem,
            this.DataLengthToolStripMenuItem,
            this.convLengthToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton1.Text = "Info.";
            // 
            // CPUCoreToolStripMenuItem
            // 
            this.CPUCoreToolStripMenuItem.Enabled = false;
            this.CPUCoreToolStripMenuItem.Name = "CPUCoreToolStripMenuItem";
            this.CPUCoreToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.CPUCoreToolStripMenuItem.Text = "CPU Core=8";
            // 
            // ASIOBufSizeToolStripMenuItem
            // 
            this.ASIOBufSizeToolStripMenuItem.Enabled = false;
            this.ASIOBufSizeToolStripMenuItem.Name = "ASIOBufSizeToolStripMenuItem";
            this.ASIOBufSizeToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.ASIOBufSizeToolStripMenuItem.Text = "ASIO Buffer Size";
            // 
            // FFTPointToolStripMenuItem
            // 
            this.FFTPointToolStripMenuItem.Enabled = false;
            this.FFTPointToolStripMenuItem.Name = "FFTPointToolStripMenuItem";
            this.FFTPointToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.FFTPointToolStripMenuItem.Text = "FFT Point=1024";
            // 
            // DataLengthToolStripMenuItem
            // 
            this.DataLengthToolStripMenuItem.Enabled = false;
            this.DataLengthToolStripMenuItem.Name = "DataLengthToolStripMenuItem";
            this.DataLengthToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.DataLengthToolStripMenuItem.Text = "Data Length=131072";
            // 
            // convLengthToolStripMenuItem
            // 
            this.convLengthToolStripMenuItem.Name = "convLengthToolStripMenuItem";
            this.convLengthToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.convLengthToolStripMenuItem.Text = "Conv. Length = 4096";
            // 
            // message_toolStripStatusLabel
            // 
            this.message_toolStripStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.message_toolStripStatusLabel.Name = "message_toolStripStatusLabel";
            this.message_toolStripStatusLabel.Size = new System.Drawing.Size(256, 25);
            this.message_toolStripStatusLabel.Text = "Required Data Length=131072";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 24);
            // 
            // src_listView
            // 
            this.src_listView.Location = new System.Drawing.Point(20, 315);
            this.src_listView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_listView.Name = "src_listView";
            this.src_listView.Size = new System.Drawing.Size(586, 102);
            this.src_listView.TabIndex = 169;
            this.src_listView.UseCompatibleStateImageBehavior = false;
            this.src_listView.View = System.Windows.Forms.View.Details;
            this.src_listView.SelectedIndexChanged += new System.EventHandler(this.src_listView_SelectedIndexChanged);
            // 
            // selected_src_lbl
            // 
            this.selected_src_lbl.AutoSize = true;
            this.selected_src_lbl.Location = new System.Drawing.Point(187, 279);
            this.selected_src_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.selected_src_lbl.Name = "selected_src_lbl";
            this.selected_src_lbl.Size = new System.Drawing.Size(102, 18);
            this.selected_src_lbl.TabIndex = 170;
            this.selected_src_lbl.Text = "Selected src";
            this.selected_src_lbl.Visible = false;
            // 
            // check_ir_btn
            // 
            this.check_ir_btn.Location = new System.Drawing.Point(470, 117);
            this.check_ir_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.check_ir_btn.Name = "check_ir_btn";
            this.check_ir_btn.Size = new System.Drawing.Size(117, 34);
            this.check_ir_btn.TabIndex = 171;
            this.check_ir_btn.Text = "Check IR";
            this.check_ir_btn.UseVisualStyleBackColor = true;
            this.check_ir_btn.Click += new System.EventHandler(this.check_ir_btn_Click);
            // 
            // inv_filt_cb
            // 
            this.inv_filt_cb.AutoSize = true;
            this.inv_filt_cb.Location = new System.Drawing.Point(190, 123);
            this.inv_filt_cb.Name = "inv_filt_cb";
            this.inv_filt_cb.Size = new System.Drawing.Size(127, 22);
            this.inv_filt_cb.TabIndex = 172;
            this.inv_filt_cb.Text = "Inverse filter";
            this.inv_filt_cb.UseVisualStyleBackColor = true;
            // 
            // ConvSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 458);
            this.ControlBox = false;
            this.Controls.Add(this.inv_filt_cb);
            this.Controls.Add(this.check_ir_btn);
            this.Controls.Add(this.selected_src_lbl);
            this.Controls.Add(this.src_listView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.IR_listView);
            this.Controls.Add(this.src_path_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.out_ch_tb);
            this.Controls.Add(this.fft_level_ud);
            this.Controls.Add(this.fft_level_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.in_ch_tb);
            this.Controls.Add(this.imp_path_btn);
            this.Controls.Add(this.set_btn);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ConvSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Realtime Convolver Setting";
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button imp_path_btn;
        private System.Windows.Forms.Button set_btn;
        private System.Windows.Forms.TextBox in_ch_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label fft_level_lbl;
        private System.Windows.Forms.NumericUpDown fft_level_ud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox out_ch_tb;
        private System.Windows.Forms.RadioButton single_rb;
        private System.Windows.Forms.RadioButton multiple_rb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button src_path_btn;
        private System.Windows.Forms.ListView IR_listView;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem DataLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CPUCoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem ASIOBufSizeToolStripMenuItem;
        private System.Windows.Forms.ListView src_listView;
        private System.Windows.Forms.Label selected_src_lbl;
        private System.Windows.Forms.Button check_ir_btn;
        private System.Windows.Forms.ToolStripMenuItem FFTPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel message_toolStripStatusLabel;
        private System.Windows.Forms.CheckBox inv_filt_cb;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem convLengthToolStripMenuItem;
    }
}