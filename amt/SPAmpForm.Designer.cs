namespace amt
{
    partial class SPAmpForm
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
            this.volume_tb = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // volume_tb
            // 
            this.volume_tb.Location = new System.Drawing.Point(43, 25);
            this.volume_tb.Name = "volume_tb";
            this.volume_tb.Size = new System.Drawing.Size(92, 19);
            this.volume_tb.TabIndex = 0;
            this.volume_tb.Text = "0";
            this.volume_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(185, 22);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(79, 25);
            this.send_btn.TabIndex = 1;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // SPAmpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 68);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.volume_tb);
            this.Name = "SPAmpForm";
            this.Text = "Set Speaker Amp Gain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox volume_tb;
        private System.Windows.Forms.Button send_btn;
    }
}