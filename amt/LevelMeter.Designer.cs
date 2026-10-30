namespace amt
{
    partial class LevelMeter
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.close_btn = new System.Windows.Forms.Button();
            this.reset_max_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(545, 673);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(255, 694);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(75, 23);
            this.close_btn.TabIndex = 3;
            this.close_btn.Text = "Close";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // reset_max_btn
            // 
            this.reset_max_btn.Location = new System.Drawing.Point(120, 694);
            this.reset_max_btn.Name = "reset_max_btn";
            this.reset_max_btn.Size = new System.Drawing.Size(75, 23);
            this.reset_max_btn.TabIndex = 4;
            this.reset_max_btn.Text = "Reset max";
            this.reset_max_btn.UseVisualStyleBackColor = true;
            this.reset_max_btn.Click += new System.EventHandler(this.reset_max_btn_Click);
            // 
            // LevelMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 722);
            this.ControlBox = false;
            this.Controls.Add(this.reset_max_btn);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "LevelMeter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LevelMeter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Button reset_max_btn;


    }
}