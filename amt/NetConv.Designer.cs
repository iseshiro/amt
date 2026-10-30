namespace amt
{
    partial class NetConv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetConv));
            this.src_listView = new System.Windows.Forms.ListView();
            this.src_add_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.out_ch_tb = new System.Windows.Forms.TextBox();
            this.fft_level_ud = new System.Windows.Forms.NumericUpDown();
            this.fft_level_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipaddress_tb = new System.Windows.Forms.TextBox();
            this.inv_path_btn = new System.Windows.Forms.Button();
            this.server_start_btn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CPUCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ASIOBufSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FFTPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Message1_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Message2_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.port_no_tb = new System.Windows.Forms.TextBox();
            this.send_cb = new System.Windows.Forms.CheckBox();
            this.receive_cb = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.in_ch_tb = new System.Windows.Forms.TextBox();
            this.src_attach_btn = new System.Windows.Forms.Button();
            this.console_msg_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mic_posi_cb = new System.Windows.Forms.ComboBox();
            this.frame_size_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fifo_delay_tb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.src_rm_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progress_tb = new System.Windows.Forms.TrackBar();
            this.current_time_lbl = new System.Windows.Forms.Label();
            this.total_time_lbl = new System.Windows.Forms.Label();
            this.udp_info_lbl = new System.Windows.Forms.Label();
            this.send_sin1k_cb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progress_tb)).BeginInit();
            this.SuspendLayout();
            // 
            // src_listView
            // 
            this.src_listView.Location = new System.Drawing.Point(13, 316);
            this.src_listView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_listView.Name = "src_listView";
            this.src_listView.Size = new System.Drawing.Size(649, 102);
            this.src_listView.TabIndex = 185;
            this.src_listView.UseCompatibleStateImageBehavior = false;
            this.src_listView.View = System.Windows.Forms.View.Details;
            this.src_listView.SelectedIndexChanged += new System.EventHandler(this.src_listView_SelectedIndexChanged);
            // 
            // src_add_btn
            // 
            this.src_add_btn.Location = new System.Drawing.Point(13, 274);
            this.src_add_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_add_btn.Name = "src_add_btn";
            this.src_add_btn.Size = new System.Drawing.Size(97, 34);
            this.src_add_btn.TabIndex = 181;
            this.src_add_btn.Text = "Add";
            this.src_add_btn.UseVisualStyleBackColor = true;
            this.src_add_btn.Click += new System.EventHandler(this.src_add_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 179;
            this.label1.Text = "Output ch.";
            // 
            // out_ch_tb
            // 
            this.out_ch_tb.Location = new System.Drawing.Point(266, 27);
            this.out_ch_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.out_ch_tb.Name = "out_ch_tb";
            this.out_ch_tb.Size = new System.Drawing.Size(62, 25);
            this.out_ch_tb.TabIndex = 178;
            // 
            // fft_level_ud
            // 
            this.fft_level_ud.Location = new System.Drawing.Point(434, 25);
            this.fft_level_ud.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.fft_level_ud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fft_level_ud.Name = "fft_level_ud";
            this.fft_level_ud.Size = new System.Drawing.Size(63, 25);
            this.fft_level_ud.TabIndex = 177;
            this.fft_level_ud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fft_level_lbl
            // 
            this.fft_level_lbl.AutoSize = true;
            this.fft_level_lbl.Location = new System.Drawing.Point(339, 29);
            this.fft_level_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.fft_level_lbl.Name = "fft_level_lbl";
            this.fft_level_lbl.Size = new System.Drawing.Size(85, 18);
            this.fft_level_lbl.TabIndex = 176;
            this.fft_level_lbl.Text = "FFT Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 175;
            this.label2.Text = "IP Address";
            // 
            // ipaddress_tb
            // 
            this.ipaddress_tb.Location = new System.Drawing.Point(260, 12);
            this.ipaddress_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ipaddress_tb.Name = "ipaddress_tb";
            this.ipaddress_tb.Size = new System.Drawing.Size(176, 25);
            this.ipaddress_tb.TabIndex = 174;
            // 
            // inv_path_btn
            // 
            this.inv_path_btn.Location = new System.Drawing.Point(518, 19);
            this.inv_path_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.inv_path_btn.Name = "inv_path_btn";
            this.inv_path_btn.Size = new System.Drawing.Size(62, 34);
            this.inv_path_btn.TabIndex = 173;
            this.inv_path_btn.Text = "Set";
            this.inv_path_btn.UseVisualStyleBackColor = true;
            this.inv_path_btn.Click += new System.EventHandler(this.inv_path_btn_Click);
            // 
            // server_start_btn
            // 
            this.server_start_btn.Enabled = false;
            this.server_start_btn.Location = new System.Drawing.Point(524, 266);
            this.server_start_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.server_start_btn.Name = "server_start_btn";
            this.server_start_btn.Size = new System.Drawing.Size(138, 42);
            this.server_start_btn.TabIndex = 172;
            this.server_start_btn.Text = "Server start";
            this.server_start_btn.UseVisualStyleBackColor = true;
            this.server_start_btn.Click += new System.EventHandler(this.server_start_btn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripDropDownButton1,
            this.Message1_toolStripStatusLabel,
            this.Message2_toolStripStatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            this.statusStrip1.Size = new System.Drawing.Size(929, 30);
            this.statusStrip1.TabIndex = 190;
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
            this.DataLengthToolStripMenuItem});
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
            // Message1_toolStripStatusLabel
            // 
            this.Message1_toolStripStatusLabel.Name = "Message1_toolStripStatusLabel";
            this.Message1_toolStripStatusLabel.Size = new System.Drawing.Size(167, 25);
            this.Message1_toolStripStatusLabel.Text = "Conv. Length=4096";
            // 
            // Message2_toolStripStatusLabel
            // 
            this.Message2_toolStripStatusLabel.ActiveLinkColor = System.Drawing.Color.Red;
            this.Message2_toolStripStatusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Message2_toolStripStatusLabel.Name = "Message2_toolStripStatusLabel";
            this.Message2_toolStripStatusLabel.Size = new System.Drawing.Size(256, 25);
            this.Message2_toolStripStatusLabel.Text = "Required Data Length=131072";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 24);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 194;
            this.label3.Text = "Port No.";
            // 
            // port_no_tb
            // 
            this.port_no_tb.Location = new System.Drawing.Point(270, 49);
            this.port_no_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.port_no_tb.Name = "port_no_tb";
            this.port_no_tb.Size = new System.Drawing.Size(72, 25);
            this.port_no_tb.TabIndex = 193;
            // 
            // send_cb
            // 
            this.send_cb.AutoSize = true;
            this.send_cb.Location = new System.Drawing.Point(13, 14);
            this.send_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.send_cb.Name = "send_cb";
            this.send_cb.Size = new System.Drawing.Size(109, 22);
            this.send_cb.TabIndex = 260;
            this.send_cb.Text = "UDP send";
            this.send_cb.UseVisualStyleBackColor = true;
            this.send_cb.CheckedChanged += new System.EventHandler(this.send_cb_CheckedChanged);
            // 
            // receive_cb
            // 
            this.receive_cb.AutoSize = true;
            this.receive_cb.Location = new System.Drawing.Point(13, 43);
            this.receive_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.receive_cb.Name = "receive_cb";
            this.receive_cb.Size = new System.Drawing.Size(129, 22);
            this.receive_cb.TabIndex = 261;
            this.receive_cb.Text = "UDP receive";
            this.receive_cb.UseVisualStyleBackColor = true;
            this.receive_cb.CheckedChanged += new System.EventHandler(this.receive_cb_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 264;
            this.label4.Text = "Input ch.";
            // 
            // in_ch_tb
            // 
            this.in_ch_tb.Location = new System.Drawing.Point(98, 27);
            this.in_ch_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.in_ch_tb.Name = "in_ch_tb";
            this.in_ch_tb.Size = new System.Drawing.Size(62, 25);
            this.in_ch_tb.TabIndex = 263;
            // 
            // src_attach_btn
            // 
            this.src_attach_btn.Location = new System.Drawing.Point(227, 274);
            this.src_attach_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_attach_btn.Name = "src_attach_btn";
            this.src_attach_btn.Size = new System.Drawing.Size(97, 34);
            this.src_attach_btn.TabIndex = 265;
            this.src_attach_btn.Text = "Attach";
            this.src_attach_btn.UseVisualStyleBackColor = true;
            this.src_attach_btn.Click += new System.EventHandler(this.src_attach_btn_Click);
            // 
            // console_msg_tb
            // 
            this.console_msg_tb.Location = new System.Drawing.Point(673, 12);
            this.console_msg_tb.Multiline = true;
            this.console_msg_tb.Name = "console_msg_tb";
            this.console_msg_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_msg_tb.Size = new System.Drawing.Size(246, 406);
            this.console_msg_tb.TabIndex = 267;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 18);
            this.label5.TabIndex = 269;
            this.label5.Text = "Mic position";
            // 
            // mic_posi_cb
            // 
            this.mic_posi_cb.FormattingEnabled = true;
            this.mic_posi_cb.Location = new System.Drawing.Point(469, 48);
            this.mic_posi_cb.Name = "mic_posi_cb";
            this.mic_posi_cb.Size = new System.Drawing.Size(179, 26);
            this.mic_posi_cb.TabIndex = 268;
            this.mic_posi_cb.SelectedIndexChanged += new System.EventHandler(this.mic_posi_cb_SelectedIndexChanged);
            // 
            // frame_size_tb
            // 
            this.frame_size_tb.Location = new System.Drawing.Point(572, 12);
            this.frame_size_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.frame_size_tb.Name = "frame_size_tb";
            this.frame_size_tb.Size = new System.Drawing.Size(72, 25);
            this.frame_size_tb.TabIndex = 270;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(473, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 18);
            this.label6.TabIndex = 271;
            this.label6.Text = "Frame size";
            // 
            // fifo_delay_tb
            // 
            this.fifo_delay_tb.Location = new System.Drawing.Point(137, 84);
            this.fifo_delay_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.fifo_delay_tb.Name = "fifo_delay_tb";
            this.fifo_delay_tb.Size = new System.Drawing.Size(72, 25);
            this.fifo_delay_tb.TabIndex = 272;
            this.fifo_delay_tb.Text = "8192";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 18);
            this.label7.TabIndex = 273;
            this.label7.Text = "Fifo Delay";
            // 
            // src_rm_btn
            // 
            this.src_rm_btn.Location = new System.Drawing.Point(120, 274);
            this.src_rm_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.src_rm_btn.Name = "src_rm_btn";
            this.src_rm_btn.Size = new System.Drawing.Size(97, 34);
            this.src_rm_btn.TabIndex = 274;
            this.src_rm_btn.Text = "Remove";
            this.src_rm_btn.UseVisualStyleBackColor = true;
            this.src_rm_btn.Click += new System.EventHandler(this.src_rm_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inv_path_btn);
            this.groupBox1.Controls.Add(this.fft_level_lbl);
            this.groupBox1.Controls.Add(this.fft_level_ud);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.in_ch_tb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.out_ch_tb);
            this.groupBox1.Location = new System.Drawing.Point(33, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 64);
            this.groupBox1.TabIndex = 275;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inverse filter";
            // 
            // progress_tb
            // 
            this.progress_tb.Location = new System.Drawing.Point(14, 197);
            this.progress_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.progress_tb.Maximum = 100;
            this.progress_tb.Name = "progress_tb";
            this.progress_tb.Size = new System.Drawing.Size(539, 69);
            this.progress_tb.TabIndex = 276;
            this.progress_tb.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // current_time_lbl
            // 
            this.current_time_lbl.AutoSize = true;
            this.current_time_lbl.Location = new System.Drawing.Point(554, 204);
            this.current_time_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.current_time_lbl.Name = "current_time_lbl";
            this.current_time_lbl.Size = new System.Drawing.Size(48, 18);
            this.current_time_lbl.TabIndex = 277;
            this.current_time_lbl.Text = "00:00";
            // 
            // total_time_lbl
            // 
            this.total_time_lbl.AutoSize = true;
            this.total_time_lbl.Location = new System.Drawing.Point(347, 282);
            this.total_time_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.total_time_lbl.Name = "total_time_lbl";
            this.total_time_lbl.Size = new System.Drawing.Size(130, 18);
            this.total_time_lbl.TabIndex = 279;
            this.total_time_lbl.Text = "Total Time = 0 s";
            // 
            // udp_info_lbl
            // 
            this.udp_info_lbl.AutoSize = true;
            this.udp_info_lbl.Location = new System.Drawing.Point(240, 91);
            this.udp_info_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.udp_info_lbl.Name = "udp_info_lbl";
            this.udp_info_lbl.Size = new System.Drawing.Size(84, 18);
            this.udp_info_lbl.TabIndex = 280;
            this.udp_info_lbl.Text = "UDP_Info :";
            // 
            // send_sin1k_cb
            // 
            this.send_sin1k_cb.AutoSize = true;
            this.send_sin1k_cb.Location = new System.Drawing.Point(498, 90);
            this.send_sin1k_cb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.send_sin1k_cb.Name = "send_sin1k_cb";
            this.send_sin1k_cb.Size = new System.Drawing.Size(146, 22);
            this.send_sin1k_cb.TabIndex = 281;
            this.send_sin1k_cb.Text = "Send SIN 1kHz";
            this.send_sin1k_cb.UseVisualStyleBackColor = true;
            this.send_sin1k_cb.Visible = false;
            // 
            // NetConv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 458);
            this.Controls.Add(this.send_sin1k_cb);
            this.Controls.Add(this.udp_info_lbl);
            this.Controls.Add(this.total_time_lbl);
            this.Controls.Add(this.current_time_lbl);
            this.Controls.Add(this.progress_tb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.src_rm_btn);
            this.Controls.Add(this.fifo_delay_tb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.frame_size_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mic_posi_cb);
            this.Controls.Add(this.console_msg_tb);
            this.Controls.Add(this.src_attach_btn);
            this.Controls.Add(this.receive_cb);
            this.Controls.Add(this.send_cb);
            this.Controls.Add(this.port_no_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ipaddress_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.server_start_btn);
            this.Controls.Add(this.src_listView);
            this.Controls.Add(this.src_add_btn);
            this.Name = "NetConv";
            this.Text = "Network Convolver";
            ((System.ComponentModel.ISupportInitialize)(this.fft_level_ud)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progress_tb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView src_listView;
        private System.Windows.Forms.Button src_add_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox out_ch_tb;
        private System.Windows.Forms.NumericUpDown fft_level_ud;
        private System.Windows.Forms.Label fft_level_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipaddress_tb;
        private System.Windows.Forms.Button inv_path_btn;
        private System.Windows.Forms.Button server_start_btn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem CPUCoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ASIOBufSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FFTPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel Message1_toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel Message2_toolStripStatusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox port_no_tb;
        private System.Windows.Forms.CheckBox send_cb;
        private System.Windows.Forms.CheckBox receive_cb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox in_ch_tb;
        private System.Windows.Forms.Button src_attach_btn;
        private System.Windows.Forms.TextBox console_msg_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mic_posi_cb;
        private System.Windows.Forms.TextBox frame_size_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox fifo_delay_tb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button src_rm_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar progress_tb;
        private System.Windows.Forms.Label current_time_lbl;
        private System.Windows.Forms.Label total_time_lbl;
        private System.Windows.Forms.Label udp_info_lbl;
        private System.Windows.Forms.CheckBox send_sin1k_cb;
    }
}