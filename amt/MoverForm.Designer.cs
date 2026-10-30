namespace amt
{
    partial class MoverForm
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
            this.mover_start_btn = new System.Windows.Forms.Button();
            this.ConsoleMsg1 = new System.Windows.Forms.TextBox();
            this.workfolder_tb = new System.Windows.Forms.TextBox();
            this.project_name_tb = new System.Windows.Forms.TextBox();
            this.user_name_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mover_start_btn
            // 
            this.mover_start_btn.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.mover_start_btn.Location = new System.Drawing.Point(507, 61);
            this.mover_start_btn.Name = "mover_start_btn";
            this.mover_start_btn.Size = new System.Drawing.Size(125, 34);
            this.mover_start_btn.TabIndex = 0;
            this.mover_start_btn.Text = "Start";
            this.mover_start_btn.UseVisualStyleBackColor = true;
            this.mover_start_btn.Click += new System.EventHandler(this.mover_start_btn_Click);
            // 
            // ConsoleMsg1
            // 
            this.ConsoleMsg1.Location = new System.Drawing.Point(12, 104);
            this.ConsoleMsg1.Multiline = true;
            this.ConsoleMsg1.Name = "ConsoleMsg1";
            this.ConsoleMsg1.Size = new System.Drawing.Size(660, 334);
            this.ConsoleMsg1.TabIndex = 1;
            // 
            // workfolder_tb
            // 
            this.workfolder_tb.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.workfolder_tb.Location = new System.Drawing.Point(165, 23);
            this.workfolder_tb.Name = "workfolder_tb";
            this.workfolder_tb.Size = new System.Drawing.Size(350, 27);
            this.workfolder_tb.TabIndex = 2;
            this.workfolder_tb.Text = "C:\\temp\\workspace";
            // 
            // project_name_tb
            // 
            this.project_name_tb.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.project_name_tb.Location = new System.Drawing.Point(165, 60);
            this.project_name_tb.Name = "project_name_tb";
            this.project_name_tb.Size = new System.Drawing.Size(143, 27);
            this.project_name_tb.TabIndex = 3;
            this.project_name_tb.Text = "test11";
            // 
            // user_name_lbl
            // 
            this.user_name_lbl.AutoSize = true;
            this.user_name_lbl.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.user_name_lbl.Location = new System.Drawing.Point(12, 23);
            this.user_name_lbl.Name = "user_name_lbl";
            this.user_name_lbl.Size = new System.Drawing.Size(126, 20);
            this.user_name_lbl.TabIndex = 4;
            this.user_name_lbl.Text = "Working folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label1.Location = new System.Drawing.Point(18, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Project name";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.button1.Location = new System.Drawing.Point(526, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Set";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MoverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_name_lbl);
            this.Controls.Add(this.project_name_tb);
            this.Controls.Add(this.workfolder_tb);
            this.Controls.Add(this.ConsoleMsg1);
            this.Controls.Add(this.mover_start_btn);
            this.Name = "MoverForm";
            this.Text = "MoverForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mover_start_btn;
        private System.Windows.Forms.TextBox ConsoleMsg1;
        private System.Windows.Forms.TextBox workfolder_tb;
        private System.Windows.Forms.TextBox project_name_tb;
        private System.Windows.Forms.Label user_name_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}