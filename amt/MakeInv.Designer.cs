namespace amt
{
    partial class MakeInv
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
            this.sp_count_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mic_count_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imp_leng_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fftpoint_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.scut_count_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.set_work_folder_btn = new System.Windows.Forms.Button();
            this.calc_start_btn = new System.Windows.Forms.Button();
            this.inv_system_status = new System.Windows.Forms.StatusStrip();
            this.imp_meas_status_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.check_folder_btn = new System.Windows.Forms.Button();
            this.folder_name_lbl = new System.Windows.Forms.Label();
            this.file_name_lbl = new System.Windows.Forms.Label();
            this.file_name_tb = new System.Windows.Forms.TextBox();
            this.inv_system_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp_count_tb
            // 
            this.sp_count_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sp_count_tb.Location = new System.Drawing.Point(124, 17);
            this.sp_count_tb.Name = "sp_count_tb";
            this.sp_count_tb.Size = new System.Drawing.Size(50, 19);
            this.sp_count_tb.TabIndex = 55;
            this.sp_count_tb.Text = "96";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "Sp. count : ";
            // 
            // mic_count_tb
            // 
            this.mic_count_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mic_count_tb.Location = new System.Drawing.Point(124, 45);
            this.mic_count_tb.Name = "mic_count_tb";
            this.mic_count_tb.Size = new System.Drawing.Size(50, 19);
            this.mic_count_tb.TabIndex = 57;
            this.mic_count_tb.Text = "80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "Mic. count";
            // 
            // imp_leng_tb
            // 
            this.imp_leng_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imp_leng_tb.Location = new System.Drawing.Point(124, 76);
            this.imp_leng_tb.Name = "imp_leng_tb";
            this.imp_leng_tb.Size = new System.Drawing.Size(50, 19);
            this.imp_leng_tb.TabIndex = 59;
            this.imp_leng_tb.Text = "2048";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "Imp. length";
            // 
            // fftpoint_tb
            // 
            this.fftpoint_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fftpoint_tb.Location = new System.Drawing.Point(124, 105);
            this.fftpoint_tb.Name = "fftpoint_tb";
            this.fftpoint_tb.Size = new System.Drawing.Size(50, 19);
            this.fftpoint_tb.TabIndex = 61;
            this.fftpoint_tb.Text = "8192";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 12);
            this.label4.TabIndex = 60;
            this.label4.Text = "FFT point";
            // 
            // scut_count_tb
            // 
            this.scut_count_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scut_count_tb.Location = new System.Drawing.Point(124, 134);
            this.scut_count_tb.Name = "scut_count_tb";
            this.scut_count_tb.Size = new System.Drawing.Size(50, 19);
            this.scut_count_tb.TabIndex = 63;
            this.scut_count_tb.Text = "8000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 12);
            this.label5.TabIndex = 62;
            this.label5.Text = "S-cut count";
            // 
            // set_work_folder_btn
            // 
            this.set_work_folder_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.set_work_folder_btn.Location = new System.Drawing.Point(14, 167);
            this.set_work_folder_btn.Name = "set_work_folder_btn";
            this.set_work_folder_btn.Size = new System.Drawing.Size(94, 28);
            this.set_work_folder_btn.TabIndex = 64;
            this.set_work_folder_btn.Text = "Work folder";
            this.set_work_folder_btn.Click += new System.EventHandler(this.set_work_folder_btn_Click);
            // 
            // calc_start_btn
            // 
            this.calc_start_btn.Enabled = false;
            this.calc_start_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calc_start_btn.Location = new System.Drawing.Point(274, 125);
            this.calc_start_btn.Name = "calc_start_btn";
            this.calc_start_btn.Size = new System.Drawing.Size(94, 28);
            this.calc_start_btn.TabIndex = 66;
            this.calc_start_btn.Text = "Calc start";
            this.calc_start_btn.Click += new System.EventHandler(this.calc_start_btn_Click);
            // 
            // inv_system_status
            // 
            this.inv_system_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imp_meas_status_lbl});
            this.inv_system_status.Location = new System.Drawing.Point(0, 269);
            this.inv_system_status.Name = "inv_system_status";
            this.inv_system_status.Size = new System.Drawing.Size(416, 22);
            this.inv_system_status.TabIndex = 67;
            this.inv_system_status.Text = "statusStrip1";
            // 
            // imp_meas_status_lbl
            // 
            this.imp_meas_status_lbl.Name = "imp_meas_status_lbl";
            this.imp_meas_status_lbl.Size = new System.Drawing.Size(54, 17);
            this.imp_meas_status_lbl.Text = "Status: ";
            // 
            // check_folder_btn
            // 
            this.check_folder_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_folder_btn.Location = new System.Drawing.Point(102, 229);
            this.check_folder_btn.Name = "check_folder_btn";
            this.check_folder_btn.Size = new System.Drawing.Size(94, 28);
            this.check_folder_btn.TabIndex = 68;
            this.check_folder_btn.Text = "Check folder";
            this.check_folder_btn.Click += new System.EventHandler(this.check_folder_btn_Click);
            // 
            // folder_name_lbl
            // 
            this.folder_name_lbl.AutoSize = true;
            this.folder_name_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folder_name_lbl.Location = new System.Drawing.Point(122, 175);
            this.folder_name_lbl.Name = "folder_name_lbl";
            this.folder_name_lbl.Size = new System.Drawing.Size(74, 12);
            this.folder_name_lbl.TabIndex = 69;
            this.folder_name_lbl.Text = "Folder name: ";
            // 
            // file_name_lbl
            // 
            this.file_name_lbl.AutoSize = true;
            this.file_name_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name_lbl.Location = new System.Drawing.Point(122, 198);
            this.file_name_lbl.Name = "file_name_lbl";
            this.file_name_lbl.Size = new System.Drawing.Size(61, 12);
            this.file_name_lbl.TabIndex = 70;
            this.file_name_lbl.Text = "File name: ";
            // 
            // file_name_tb
            // 
            this.file_name_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name_tb.Location = new System.Drawing.Point(189, 195);
            this.file_name_tb.Name = "file_name_tb";
            this.file_name_tb.Size = new System.Drawing.Size(163, 19);
            this.file_name_tb.TabIndex = 71;
            // 
            // MakeInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 291);
            this.Controls.Add(this.file_name_tb);
            this.Controls.Add(this.file_name_lbl);
            this.Controls.Add(this.folder_name_lbl);
            this.Controls.Add(this.check_folder_btn);
            this.Controls.Add(this.inv_system_status);
            this.Controls.Add(this.calc_start_btn);
            this.Controls.Add(this.set_work_folder_btn);
            this.Controls.Add(this.scut_count_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fftpoint_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.imp_leng_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mic_count_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sp_count_tb);
            this.Controls.Add(this.label3);
            this.Name = "MakeInv";
            this.Text = "Inverse system";
            this.inv_system_status.ResumeLayout(false);
            this.inv_system_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sp_count_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mic_count_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imp_leng_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fftpoint_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox scut_count_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button set_work_folder_btn;
        private System.Windows.Forms.Button calc_start_btn;
        private System.Windows.Forms.StatusStrip inv_system_status;
        private System.Windows.Forms.ToolStripStatusLabel imp_meas_status_lbl;
        private System.Windows.Forms.Button check_folder_btn;
        private System.Windows.Forms.Label folder_name_lbl;
        private System.Windows.Forms.Label file_name_lbl;
        private System.Windows.Forms.TextBox file_name_tb;
    }
}