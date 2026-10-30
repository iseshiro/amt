namespace amt
{
    partial class AssignCh
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
            this.ok_btn = new System.Windows.Forms.Button();
            this.reset_btn = new System.Windows.Forms.Button();
            this.in_ch_tb = new System.Windows.Forms.TextBox();
            this.out_ch_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(154, 90);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(63, 29);
            this.ok_btn.TabIndex = 1;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // reset_btn
            // 
            this.reset_btn.Location = new System.Drawing.Point(53, 90);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(59, 29);
            this.reset_btn.TabIndex = 4;
            this.reset_btn.Text = "Reset";
            this.reset_btn.UseVisualStyleBackColor = true;
            this.reset_btn.Click += new System.EventHandler(this.reset_btn_Click);
            // 
            // in_ch_tb
            // 
            this.in_ch_tb.Location = new System.Drawing.Point(75, 11);
            this.in_ch_tb.Name = "in_ch_tb";
            this.in_ch_tb.Size = new System.Drawing.Size(163, 19);
            this.in_ch_tb.TabIndex = 5;
            this.in_ch_tb.Text = "1-2";
            // 
            // out_ch_tb
            // 
            this.out_ch_tb.Location = new System.Drawing.Point(75, 48);
            this.out_ch_tb.Name = "out_ch_tb";
            this.out_ch_tb.Size = new System.Drawing.Size(163, 19);
            this.out_ch_tb.TabIndex = 6;
            this.out_ch_tb.Text = "1-2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "IN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "OUT";
            // 
            // AssignCh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 131);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.out_ch_tb);
            this.Controls.Add(this.in_ch_tb);
            this.Controls.Add(this.reset_btn);
            this.Controls.Add(this.ok_btn);
            this.Name = "AssignCh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIO Channel Assign";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button reset_btn;
        private System.Windows.Forms.TextBox in_ch_tb;
        private System.Windows.Forms.TextBox out_ch_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}