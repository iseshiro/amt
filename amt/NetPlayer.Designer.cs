namespace amt
{
    partial class NetPlayer
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
            this.song_set_db_btn = new System.Windows.Forms.Button();
            this.filter_set_db_btn = new System.Windows.Forms.Button();
            this.start_server_btn = new System.Windows.Forms.Button();
            this.ConsoleMsg = new System.Windows.Forms.TextBox();
            this.filter_db_file_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Filter_load_db_btn = new System.Windows.Forms.Button();
            this.Song_load_db_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.song_db_file_tb = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // song_set_db_btn
            // 
            this.song_set_db_btn.Location = new System.Drawing.Point(317, 29);
            this.song_set_db_btn.Name = "song_set_db_btn";
            this.song_set_db_btn.Size = new System.Drawing.Size(39, 20);
            this.song_set_db_btn.TabIndex = 0;
            this.song_set_db_btn.Text = "Set";
            this.song_set_db_btn.UseVisualStyleBackColor = true;
            this.song_set_db_btn.Click += new System.EventHandler(this.Song_db_set_btn_Clic);
            // 
            // filter_set_db_btn
            // 
            this.filter_set_db_btn.Location = new System.Drawing.Point(317, 4);
            this.filter_set_db_btn.Name = "filter_set_db_btn";
            this.filter_set_db_btn.Size = new System.Drawing.Size(39, 19);
            this.filter_set_db_btn.TabIndex = 1;
            this.filter_set_db_btn.Text = "Set";
            this.filter_set_db_btn.UseVisualStyleBackColor = true;
            this.filter_set_db_btn.Click += new System.EventHandler(this.Filter_db_set_btn_Clic);
            // 
            // start_server_btn
            // 
            this.start_server_btn.Enabled = false;
            this.start_server_btn.Location = new System.Drawing.Point(425, 23);
            this.start_server_btn.Name = "start_server_btn";
            this.start_server_btn.Size = new System.Drawing.Size(94, 25);
            this.start_server_btn.TabIndex = 2;
            this.start_server_btn.Text = "Start Server";
            this.start_server_btn.UseVisualStyleBackColor = true;
            this.start_server_btn.Click += new System.EventHandler(this.Start_server_btn_Click);
            // 
            // ConsoleMsg
            // 
            this.ConsoleMsg.Location = new System.Drawing.Point(8, 56);
            this.ConsoleMsg.Multiline = true;
            this.ConsoleMsg.Name = "ConsoleMsg";
            this.ConsoleMsg.Size = new System.Drawing.Size(511, 112);
            this.ConsoleMsg.TabIndex = 4;
            // 
            // filter_db_file_tb
            // 
            this.filter_db_file_tb.Location = new System.Drawing.Point(78, 6);
            this.filter_db_file_tb.Name = "filter_db_file_tb";
            this.filter_db_file_tb.Size = new System.Drawing.Size(228, 19);
            this.filter_db_file_tb.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filter DB";
            // 
            // Filter_load_db_btn
            // 
            this.Filter_load_db_btn.Location = new System.Drawing.Point(362, 5);
            this.Filter_load_db_btn.Name = "Filter_load_db_btn";
            this.Filter_load_db_btn.Size = new System.Drawing.Size(46, 19);
            this.Filter_load_db_btn.TabIndex = 7;
            this.Filter_load_db_btn.Text = "Load";
            this.Filter_load_db_btn.UseVisualStyleBackColor = true;
            this.Filter_load_db_btn.Click += new System.EventHandler(this.Filter_load_db_btn_Click);
            // 
            // Song_load_db_btn
            // 
            this.Song_load_db_btn.Location = new System.Drawing.Point(362, 29);
            this.Song_load_db_btn.Name = "Song_load_db_btn";
            this.Song_load_db_btn.Size = new System.Drawing.Size(46, 19);
            this.Song_load_db_btn.TabIndex = 10;
            this.Song_load_db_btn.Text = "Load";
            this.Song_load_db_btn.UseVisualStyleBackColor = true;
            this.Song_load_db_btn.Click += new System.EventHandler(this.Song_load_db_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Song DB";
            // 
            // song_db_file_tb
            // 
            this.song_db_file_tb.Location = new System.Drawing.Point(78, 31);
            this.song_db_file_tb.Name = "song_db_file_tb";
            this.song_db_file_tb.Size = new System.Drawing.Size(228, 19);
            this.song_db_file_tb.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 178);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStrip1.Size = new System.Drawing.Size(527, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(60, 16);
            // 
            // NetPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 200);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Song_load_db_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.song_db_file_tb);
            this.Controls.Add(this.Filter_load_db_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filter_db_file_tb);
            this.Controls.Add(this.ConsoleMsg);
            this.Controls.Add(this.start_server_btn);
            this.Controls.Add(this.filter_set_db_btn);
            this.Controls.Add(this.song_set_db_btn);
            this.Name = "NetPlayer";
            this.Text = "Network Player";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button song_set_db_btn;
        private System.Windows.Forms.Button filter_set_db_btn;
        private System.Windows.Forms.Button start_server_btn;
        private System.Windows.Forms.TextBox ConsoleMsg;
        private System.Windows.Forms.TextBox filter_db_file_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Filter_load_db_btn;
        private System.Windows.Forms.Button Song_load_db_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox song_db_file_tb;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}