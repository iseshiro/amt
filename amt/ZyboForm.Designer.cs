namespace amt
{
    partial class ZyboForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_ad_satrt = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_close = new System.Windows.Forms.Button();
            this.textBox_ip_adrs = new System.Windows.Forms.TextBox();
            this.textBox_port_no = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_set_folder = new System.Windows.Forms.Button();
            this.button_check = new System.Windows.Forms.Button();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_filename = new System.Windows.Forms.Label();
            this.progressBar_download = new System.Windows.Forms.ProgressBar();
            this.label_progress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_ad_satrt
            // 
            this.button_ad_satrt.Location = new System.Drawing.Point(29, 170);
            this.button_ad_satrt.Name = "button_ad_satrt";
            this.button_ad_satrt.Size = new System.Drawing.Size(129, 45);
            this.button_ad_satrt.TabIndex = 0;
            this.button_ad_satrt.Text = "AD Start";
            this.button_ad_satrt.UseVisualStyleBackColor = true;
            this.button_ad_satrt.Click += new System.EventHandler(this.button_ad_satrt_Click);
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(29, 248);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(129, 45);
            this.button_download.TabIndex = 1;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(191, 156);
            this.textBox_message.Multiline = true;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(475, 166);
            this.textBox_message.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(561, 343);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(105, 39);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // textBox_ip_adrs
            // 
            this.textBox_ip_adrs.Location = new System.Drawing.Point(154, 25);
            this.textBox_ip_adrs.Name = "textBox_ip_adrs";
            this.textBox_ip_adrs.Size = new System.Drawing.Size(224, 25);
            this.textBox_ip_adrs.TabIndex = 5;
            this.textBox_ip_adrs.Text = "192.168.0.10";
            // 
            // textBox_port_no
            // 
            this.textBox_port_no.Location = new System.Drawing.Point(497, 25);
            this.textBox_port_no.Name = "textBox_port_no";
            this.textBox_port_no.Size = new System.Drawing.Size(67, 25);
            this.textBox_port_no.TabIndex = 6;
            this.textBox_port_no.Text = "7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port No.";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(191, 67);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(475, 25);
            this.textBox_path.TabIndex = 9;
            this.textBox_path.Text = "C:\\temp\\zybo";
            // 
            // button_set_folder
            // 
            this.button_set_folder.Location = new System.Drawing.Point(29, 60);
            this.button_set_folder.Name = "button_set_folder";
            this.button_set_folder.Size = new System.Drawing.Size(140, 39);
            this.button_set_folder.TabIndex = 10;
            this.button_set_folder.Text = "Select folder";
            this.button_set_folder.UseVisualStyleBackColor = true;
            this.button_set_folder.Click += new System.EventHandler(this.button_save_folder_Click);
            // 
            // button_check
            // 
            this.button_check.Location = new System.Drawing.Point(43, 112);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(105, 43);
            this.button_check.TabIndex = 11;
            this.button_check.Text = "Check";
            this.button_check.UseVisualStyleBackColor = true;
            this.button_check.Click += new System.EventHandler(this.button_check_Click);
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(260, 109);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(118, 25);
            this.textBox_filename.TabIndex = 12;
            this.textBox_filename.Text = "outfile_";
            this.textBox_filename.TextChanged += new System.EventHandler(this.textBox_filename_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "File Name";
            // 
            // label_filename
            // 
            this.label_filename.AutoSize = true;
            this.label_filename.Location = new System.Drawing.Point(405, 112);
            this.label_filename.Name = "label_filename";
            this.label_filename.Size = new System.Drawing.Size(236, 18);
            this.label_filename.TabIndex = 14;
            this.label_filename.Text = "outfile_01.wav ... outfile_80.wav";
            // 
            // progressBar_download
            // 
            this.progressBar_download.Location = new System.Drawing.Point(29, 341);
            this.progressBar_download.Name = "progressBar_download";
            this.progressBar_download.Size = new System.Drawing.Size(497, 33);
            this.progressBar_download.Step = 1;
            this.progressBar_download.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_download.TabIndex = 15;
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(26, 304);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(153, 18);
            this.label_progress.TabIndex = 16;
            this.label_progress.Text = "1920000 / 1920000";
            // 
            // ZyboForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 394);
            this.ControlBox = false;
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.progressBar_download);
            this.Controls.Add(this.label_filename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_filename);
            this.Controls.Add(this.button_check);
            this.Controls.Add(this.button_set_folder);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port_no);
            this.Controls.Add(this.textBox_ip_adrs);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.button_ad_satrt);
            this.Name = "ZyboForm";
            this.Text = "Control ZYBO";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ad_satrt;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.TextBox textBox_ip_adrs;
        private System.Windows.Forms.TextBox textBox_port_no;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_set_folder;
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_filename;
        private System.Windows.Forms.ProgressBar progressBar_download;
        private System.Windows.Forms.Label label_progress;
    }
}