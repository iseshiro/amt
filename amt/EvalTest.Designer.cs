namespace amt
{
    partial class EvalTest
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
            this.Context_load_db_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.contents_db_file_tb = new System.Windows.Forms.TextBox();
            this.contents_set_db_btn = new System.Windows.Forms.Button();
            this.current_time_lbl = new System.Windows.Forms.Label();
            this.progress_tb = new System.Windows.Forms.TrackBar();
            this.start_server_btn = new System.Windows.Forms.Button();
            this.ConsoleMsg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.progress_tb)).BeginInit();
            this.SuspendLayout();
            // 
            // Context_load_db_btn
            // 
            this.Context_load_db_btn.Location = new System.Drawing.Point(598, 9);
            this.Context_load_db_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Context_load_db_btn.Name = "Context_load_db_btn";
            this.Context_load_db_btn.Size = new System.Drawing.Size(77, 28);
            this.Context_load_db_btn.TabIndex = 22;
            this.Context_load_db_btn.Text = "Load";
            this.Context_load_db_btn.UseVisualStyleBackColor = true;
            this.Context_load_db_btn.Click += new System.EventHandler(this.Context_load_db_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "Context DB";
            // 
            // contents_db_file_tb
            // 
            this.contents_db_file_tb.Location = new System.Drawing.Point(124, 13);
            this.contents_db_file_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.contents_db_file_tb.Name = "contents_db_file_tb";
            this.contents_db_file_tb.Size = new System.Drawing.Size(377, 25);
            this.contents_db_file_tb.TabIndex = 20;
            this.contents_db_file_tb.Text = "C:\\temp\\Music_20180108\\contents_db.json";
            // 
            // contents_set_db_btn
            // 
            this.contents_set_db_btn.Location = new System.Drawing.Point(523, 9);
            this.contents_set_db_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.contents_set_db_btn.Name = "contents_set_db_btn";
            this.contents_set_db_btn.Size = new System.Drawing.Size(65, 28);
            this.contents_set_db_btn.TabIndex = 19;
            this.contents_set_db_btn.Text = "Set";
            this.contents_set_db_btn.UseVisualStyleBackColor = true;
            this.contents_set_db_btn.Click += new System.EventHandler(this.contents_set_db_btn_Click);
            // 
            // current_time_lbl
            // 
            this.current_time_lbl.AutoSize = true;
            this.current_time_lbl.Location = new System.Drawing.Point(590, 65);
            this.current_time_lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.current_time_lbl.Name = "current_time_lbl";
            this.current_time_lbl.Size = new System.Drawing.Size(48, 18);
            this.current_time_lbl.TabIndex = 281;
            this.current_time_lbl.Text = "00:00";
            // 
            // progress_tb
            // 
            this.progress_tb.Location = new System.Drawing.Point(50, 57);
            this.progress_tb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.progress_tb.Maximum = 100;
            this.progress_tb.Name = "progress_tb";
            this.progress_tb.Size = new System.Drawing.Size(538, 69);
            this.progress_tb.TabIndex = 280;
            this.progress_tb.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // start_server_btn
            // 
            this.start_server_btn.Enabled = false;
            this.start_server_btn.Location = new System.Drawing.Point(712, 19);
            this.start_server_btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.start_server_btn.Name = "start_server_btn";
            this.start_server_btn.Size = new System.Drawing.Size(157, 38);
            this.start_server_btn.TabIndex = 282;
            this.start_server_btn.Text = "Start Server";
            this.start_server_btn.UseVisualStyleBackColor = true;
            this.start_server_btn.Click += new System.EventHandler(this.start_server_btn_Click);
            // 
            // ConsoleMsg
            // 
            this.ConsoleMsg.Location = new System.Drawing.Point(20, 112);
            this.ConsoleMsg.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ConsoleMsg.Multiline = true;
            this.ConsoleMsg.Name = "ConsoleMsg";
            this.ConsoleMsg.Size = new System.Drawing.Size(849, 166);
            this.ConsoleMsg.TabIndex = 283;
            // 
            // EvalTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 300);
            this.Controls.Add(this.ConsoleMsg);
            this.Controls.Add(this.start_server_btn);
            this.Controls.Add(this.current_time_lbl);
            this.Controls.Add(this.progress_tb);
            this.Controls.Add(this.Context_load_db_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.contents_db_file_tb);
            this.Controls.Add(this.contents_set_db_btn);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "EvalTest";
            this.Text = "EvalTest";
            ((System.ComponentModel.ISupportInitialize)(this.progress_tb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Context_load_db_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox contents_db_file_tb;
        private System.Windows.Forms.Button contents_set_db_btn;
        private System.Windows.Forms.Label current_time_lbl;
        private System.Windows.Forms.TrackBar progress_tb;
        private System.Windows.Forms.Button start_server_btn;
        private System.Windows.Forms.TextBox ConsoleMsg;
    }
}