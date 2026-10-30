namespace amt
{
    partial class UEServerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.mic_posi_cb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.server_src_tb = new System.Windows.Forms.TextBox();
            this.server_start_btn = new System.Windows.Forms.Button();
            this.set_server_src_btn = new System.Windows.Forms.Button();
            this.console_msg_tb = new System.Windows.Forms.TextBox();
            this.set_mic_posi_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.use = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.src_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stop_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wordcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.up_btn = new System.Windows.Forms.Button();
            this.down_btn = new System.Windows.Forms.Button();
            this.set_partnerCount_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.max_input_ch_cb = new System.Windows.Forms.ComboBox();
            this.meter_val_pb = new System.Windows.Forms.ProgressBar();
            this.monitor_ch_lbl = new System.Windows.Forms.Label();
            this.inout_cb = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.level_meter_set_btn = new System.Windows.Forms.Button();
            this.ch_no_tb = new System.Windows.Forms.TextBox();
            this.peak_rb = new System.Windows.Forms.RadioButton();
            this.rms_rb = new System.Windows.Forms.RadioButton();
            this.show_msg_cb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 18);
            this.label4.TabIndex = 43;
            this.label4.Text = "Output mode";
            // 
            // mic_posi_cb
            // 
            this.mic_posi_cb.FormattingEnabled = true;
            this.mic_posi_cb.Location = new System.Drawing.Point(150, 12);
            this.mic_posi_cb.Name = "mic_posi_cb";
            this.mic_posi_cb.Size = new System.Drawing.Size(179, 26);
            this.mic_posi_cb.TabIndex = 42;
            this.mic_posi_cb.SelectedIndexChanged += new System.EventHandler(this.mic_posi_cb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 37;
            this.label1.Text = "Source folder";
            // 
            // server_src_tb
            // 
            this.server_src_tb.Location = new System.Drawing.Point(148, 49);
            this.server_src_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.server_src_tb.Name = "server_src_tb";
            this.server_src_tb.Size = new System.Drawing.Size(196, 25);
            this.server_src_tb.TabIndex = 36;
            // 
            // server_start_btn
            // 
            this.server_start_btn.Enabled = false;
            this.server_start_btn.Location = new System.Drawing.Point(332, 315);
            this.server_start_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.server_start_btn.Name = "server_start_btn";
            this.server_start_btn.Size = new System.Drawing.Size(137, 40);
            this.server_start_btn.TabIndex = 39;
            this.server_start_btn.Text = "Server_Start";
            this.server_start_btn.UseVisualStyleBackColor = true;
            this.server_start_btn.Click += new System.EventHandler(this.server_start_btn_Click);
            // 
            // set_server_src_btn
            // 
            this.set_server_src_btn.Enabled = false;
            this.set_server_src_btn.Location = new System.Drawing.Point(366, 49);
            this.set_server_src_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_server_src_btn.Name = "set_server_src_btn";
            this.set_server_src_btn.Size = new System.Drawing.Size(103, 28);
            this.set_server_src_btn.TabIndex = 38;
            this.set_server_src_btn.Text = "Set";
            this.set_server_src_btn.UseVisualStyleBackColor = true;
            this.set_server_src_btn.Click += new System.EventHandler(this.set_server_src_btn_Click);
            // 
            // console_msg_tb
            // 
            this.console_msg_tb.Location = new System.Drawing.Point(504, 12);
            this.console_msg_tb.Multiline = true;
            this.console_msg_tb.Name = "console_msg_tb";
            this.console_msg_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_msg_tb.Size = new System.Drawing.Size(437, 287);
            this.console_msg_tb.TabIndex = 41;
            // 
            // set_mic_posi_btn
            // 
            this.set_mic_posi_btn.Location = new System.Drawing.Point(366, 10);
            this.set_mic_posi_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_mic_posi_btn.Name = "set_mic_posi_btn";
            this.set_mic_posi_btn.Size = new System.Drawing.Size(103, 28);
            this.set_mic_posi_btn.TabIndex = 44;
            this.set_mic_posi_btn.Text = "Set";
            this.set_mic_posi_btn.UseVisualStyleBackColor = true;
            this.set_mic_posi_btn.Click += new System.EventHandler(this.set_mic_posi_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.use,
            this.src_no,
            this.filename,
            this.ch,
            this.fs,
            this.stop_time,
            this.wordcount});
            this.dataGridView1.Location = new System.Drawing.Point(12, 102);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(477, 197);
            this.dataGridView1.TabIndex = 45;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // use
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.use.DefaultCellStyle = dataGridViewCellStyle1;
            this.use.Frozen = true;
            this.use.HeaderText = "Use";
            this.use.MinimumWidth = 8;
            this.use.Name = "use";
            this.use.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.use.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.use.Width = 30;
            // 
            // src_no
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.src_no.DefaultCellStyle = dataGridViewCellStyle2;
            this.src_no.Frozen = true;
            this.src_no.HeaderText = "No.";
            this.src_no.MinimumWidth = 8;
            this.src_no.Name = "src_no";
            this.src_no.Width = 30;
            // 
            // filename
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.filename.DefaultCellStyle = dataGridViewCellStyle3;
            this.filename.HeaderText = "File Name (.wav)";
            this.filename.MinimumWidth = 8;
            this.filename.Name = "filename";
            this.filename.Width = 140;
            // 
            // ch
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ch.DefaultCellStyle = dataGridViewCellStyle4;
            this.ch.HeaderText = "Ch.";
            this.ch.MinimumWidth = 8;
            this.ch.Name = "ch";
            this.ch.Width = 30;
            // 
            // fs
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.fs.DefaultCellStyle = dataGridViewCellStyle5;
            this.fs.HeaderText = "Sample Freq.";
            this.fs.MinimumWidth = 8;
            this.fs.Name = "fs";
            this.fs.Width = 50;
            // 
            // stop_time
            // 
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.stop_time.DefaultCellStyle = dataGridViewCellStyle6;
            this.stop_time.HeaderText = "Time";
            this.stop_time.MinimumWidth = 8;
            this.stop_time.Name = "stop_time";
            this.stop_time.Width = 50;
            // 
            // wordcount
            // 
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.wordcount.DefaultCellStyle = dataGridViewCellStyle7;
            this.wordcount.HeaderText = "Word count";
            this.wordcount.MinimumWidth = 8;
            this.wordcount.Name = "wordcount";
            this.wordcount.Width = 60;
            // 
            // up_btn
            // 
            this.up_btn.Location = new System.Drawing.Point(72, 320);
            this.up_btn.Name = "up_btn";
            this.up_btn.Size = new System.Drawing.Size(79, 31);
            this.up_btn.TabIndex = 46;
            this.up_btn.Text = "Up";
            this.up_btn.UseVisualStyleBackColor = true;
            this.up_btn.Visible = false;
            this.up_btn.Click += new System.EventHandler(this.up_btn_Click);
            // 
            // down_btn
            // 
            this.down_btn.Location = new System.Drawing.Point(185, 320);
            this.down_btn.Name = "down_btn";
            this.down_btn.Size = new System.Drawing.Size(77, 31);
            this.down_btn.TabIndex = 47;
            this.down_btn.Text = "Down";
            this.down_btn.UseVisualStyleBackColor = true;
            this.down_btn.Visible = false;
            this.down_btn.Click += new System.EventHandler(this.down_btn_Click);
            // 
            // set_partnerCount_btn
            // 
            this.set_partnerCount_btn.Location = new System.Drawing.Point(238, 367);
            this.set_partnerCount_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.set_partnerCount_btn.Name = "set_partnerCount_btn";
            this.set_partnerCount_btn.Size = new System.Drawing.Size(70, 28);
            this.set_partnerCount_btn.TabIndex = 50;
            this.set_partnerCount_btn.Text = "Set";
            this.set_partnerCount_btn.UseVisualStyleBackColor = true;
            this.set_partnerCount_btn.Click += new System.EventHandler(this.set_partnerCount_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 372);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 18);
            this.label2.TabIndex = 49;
            this.label2.Text = "Max. Input Ch";
            // 
            // max_input_ch_cb
            // 
            this.max_input_ch_cb.FormattingEnabled = true;
            this.max_input_ch_cb.Location = new System.Drawing.Point(145, 367);
            this.max_input_ch_cb.Name = "max_input_ch_cb";
            this.max_input_ch_cb.Size = new System.Drawing.Size(85, 26);
            this.max_input_ch_cb.TabIndex = 48;
            this.max_input_ch_cb.SelectedIndexChanged += new System.EventHandler(this.max_input_ch_cb_SelectedIndexChanged);
            // 
            // meter_val_pb
            // 
            this.meter_val_pb.Location = new System.Drawing.Point(140, 67);
            this.meter_val_pb.Name = "meter_val_pb";
            this.meter_val_pb.Size = new System.Drawing.Size(248, 23);
            this.meter_val_pb.Step = 1;
            this.meter_val_pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.meter_val_pb.TabIndex = 51;
            // 
            // monitor_ch_lbl
            // 
            this.monitor_ch_lbl.AutoSize = true;
            this.monitor_ch_lbl.Location = new System.Drawing.Point(18, 68);
            this.monitor_ch_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.monitor_ch_lbl.Name = "monitor_ch_lbl";
            this.monitor_ch_lbl.Size = new System.Drawing.Size(33, 18);
            this.monitor_ch_lbl.TabIndex = 53;
            this.monitor_ch_lbl.Text = "Ch.";
            // 
            // inout_cb
            // 
            this.inout_cb.FormattingEnabled = true;
            this.inout_cb.Items.AddRange(new object[] {
            "Input",
            "Output"});
            this.inout_cb.Location = new System.Drawing.Point(21, 28);
            this.inout_cb.Name = "inout_cb";
            this.inout_cb.Size = new System.Drawing.Size(113, 26);
            this.inout_cb.TabIndex = 55;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.level_meter_set_btn);
            this.groupBox1.Controls.Add(this.ch_no_tb);
            this.groupBox1.Controls.Add(this.peak_rb);
            this.groupBox1.Controls.Add(this.rms_rb);
            this.groupBox1.Controls.Add(this.meter_val_pb);
            this.groupBox1.Controls.Add(this.inout_cb);
            this.groupBox1.Controls.Add(this.monitor_ch_lbl);
            this.groupBox1.Location = new System.Drawing.Point(514, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 102);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Level Meter";
            // 
            // level_meter_set_btn
            // 
            this.level_meter_set_btn.Location = new System.Drawing.Point(318, 25);
            this.level_meter_set_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.level_meter_set_btn.Name = "level_meter_set_btn";
            this.level_meter_set_btn.Size = new System.Drawing.Size(70, 28);
            this.level_meter_set_btn.TabIndex = 58;
            this.level_meter_set_btn.Text = "Set";
            this.level_meter_set_btn.UseVisualStyleBackColor = true;
            this.level_meter_set_btn.Click += new System.EventHandler(this.level_meter_set_btn_Click);
            // 
            // ch_no_tb
            // 
            this.ch_no_tb.Location = new System.Drawing.Point(61, 65);
            this.ch_no_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ch_no_tb.Name = "ch_no_tb";
            this.ch_no_tb.Size = new System.Drawing.Size(53, 25);
            this.ch_no_tb.TabIndex = 58;
            this.ch_no_tb.Text = "1";
            // 
            // peak_rb
            // 
            this.peak_rb.AutoSize = true;
            this.peak_rb.Location = new System.Drawing.Point(240, 29);
            this.peak_rb.Name = "peak_rb";
            this.peak_rb.Size = new System.Drawing.Size(70, 22);
            this.peak_rb.TabIndex = 57;
            this.peak_rb.Text = "Peak";
            this.peak_rb.UseVisualStyleBackColor = true;
            // 
            // rms_rb
            // 
            this.rms_rb.AutoSize = true;
            this.rms_rb.Checked = true;
            this.rms_rb.Location = new System.Drawing.Point(166, 29);
            this.rms_rb.Name = "rms_rb";
            this.rms_rb.Size = new System.Drawing.Size(68, 22);
            this.rms_rb.TabIndex = 56;
            this.rms_rb.TabStop = true;
            this.rms_rb.Text = "RMS";
            this.rms_rb.UseVisualStyleBackColor = true;
            // 
            // show_msg_cb
            // 
            this.show_msg_cb.AutoSize = true;
            this.show_msg_cb.Location = new System.Drawing.Point(332, 373);
            this.show_msg_cb.Name = "show_msg_cb";
            this.show_msg_cb.Size = new System.Drawing.Size(146, 22);
            this.show_msg_cb.TabIndex = 58;
            this.show_msg_cb.Text = "Show tcp recv.";
            this.show_msg_cb.UseVisualStyleBackColor = true;
            this.show_msg_cb.CheckedChanged += new System.EventHandler(this.show_msg_cb_CheckedChanged);
            // 
            // UEServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 425);
            this.Controls.Add(this.show_msg_cb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.set_partnerCount_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.max_input_ch_cb);
            this.Controls.Add(this.down_btn);
            this.Controls.Add(this.up_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.set_mic_posi_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mic_posi_cb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.server_src_tb);
            this.Controls.Add(this.server_start_btn);
            this.Controls.Add(this.set_server_src_btn);
            this.Controls.Add(this.console_msg_tb);
            this.Name = "UEServerForm";
            this.Text = "VR Server";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox mic_posi_cb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server_src_tb;
        private System.Windows.Forms.Button server_start_btn;
        private System.Windows.Forms.Button set_server_src_btn;
        private System.Windows.Forms.TextBox console_msg_tb;
        private System.Windows.Forms.Button set_mic_posi_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn use;
        private System.Windows.Forms.DataGridViewTextBoxColumn src_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch;
        private System.Windows.Forms.DataGridViewTextBoxColumn fs;
        private System.Windows.Forms.DataGridViewTextBoxColumn stop_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordcount;
        private System.Windows.Forms.Button up_btn;
        private System.Windows.Forms.Button down_btn;
        private System.Windows.Forms.Button set_partnerCount_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox max_input_ch_cb;
        private System.Windows.Forms.ProgressBar meter_val_pb;
        private System.Windows.Forms.Label monitor_ch_lbl;
        private System.Windows.Forms.ComboBox inout_cb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ch_no_tb;
        private System.Windows.Forms.RadioButton peak_rb;
        private System.Windows.Forms.RadioButton rms_rb;
        private System.Windows.Forms.Button level_meter_set_btn;
        private System.Windows.Forms.CheckBox show_msg_cb;
    }
}