namespace amt
{
    partial class VolumeControl
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
            this.vol_ctrl_tb = new System.Windows.Forms.TrackBar();
            this.vol_val_tb = new System.Windows.Forms.TextBox();
            this.buf_set_btn = new System.Windows.Forms.Button();
            this.Damp_vol_lbl = new System.Windows.Forms.Label();
            this.vol_set_btn = new System.Windows.Forms.Button();
            this.close_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.vol_ctrl_tb)).BeginInit();
            this.SuspendLayout();
            // 
            // vol_ctrl_tb
            // 
            this.vol_ctrl_tb.Location = new System.Drawing.Point(29, 52);
            this.vol_ctrl_tb.Maximum = 30;
            this.vol_ctrl_tb.Minimum = -30;
            this.vol_ctrl_tb.Name = "vol_ctrl_tb";
            this.vol_ctrl_tb.Size = new System.Drawing.Size(195, 45);
            this.vol_ctrl_tb.TabIndex = 0;
            this.vol_ctrl_tb.Scroll += new System.EventHandler(this.vol_ctrl_tb_Scroll);
            // 
            // vol_val_tb
            // 
            this.vol_val_tb.Location = new System.Drawing.Point(108, 88);
            this.vol_val_tb.Name = "vol_val_tb";
            this.vol_val_tb.Size = new System.Drawing.Size(52, 19);
            this.vol_val_tb.TabIndex = 1;
            this.vol_val_tb.Text = "0";
            this.vol_val_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buf_set_btn
            // 
            this.buf_set_btn.Location = new System.Drawing.Point(12, 12);
            this.buf_set_btn.Name = "buf_set_btn";
            this.buf_set_btn.Size = new System.Drawing.Size(75, 23);
            this.buf_set_btn.TabIndex = 2;
            this.buf_set_btn.Text = "Buf Set";
            this.buf_set_btn.UseVisualStyleBackColor = true;
            this.buf_set_btn.Click += new System.EventHandler(this.buf_set_btn_Click);
            // 
            // Damp_vol_lbl
            // 
            this.Damp_vol_lbl.AutoSize = true;
            this.Damp_vol_lbl.Location = new System.Drawing.Point(40, 122);
            this.Damp_vol_lbl.Name = "Damp_vol_lbl";
            this.Damp_vol_lbl.Size = new System.Drawing.Size(91, 12);
            this.Damp_vol_lbl.TabIndex = 3;
            this.Damp_vol_lbl.Text = "Damp vol = 0 dB";
            this.Damp_vol_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vol_set_btn
            // 
            this.vol_set_btn.Location = new System.Drawing.Point(149, 117);
            this.vol_set_btn.Name = "vol_set_btn";
            this.vol_set_btn.Size = new System.Drawing.Size(75, 23);
            this.vol_set_btn.TabIndex = 4;
            this.vol_set_btn.Text = "Volume Set";
            this.vol_set_btn.UseVisualStyleBackColor = true;
            this.vol_set_btn.Click += new System.EventHandler(this.vol_set_btn_Click);
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(85, 152);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(75, 23);
            this.close_btn.TabIndex = 5;
            this.close_btn.Text = "Close";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "dB";
            // 
            // VolumeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 187);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.vol_set_btn);
            this.Controls.Add(this.Damp_vol_lbl);
            this.Controls.Add(this.buf_set_btn);
            this.Controls.Add(this.vol_val_tb);
            this.Controls.Add(this.vol_ctrl_tb);
            this.Name = "VolumeControl";
            this.Text = "VolumeControl";
            ((System.ComponentModel.ISupportInitialize)(this.vol_ctrl_tb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar vol_ctrl_tb;
        private System.Windows.Forms.TextBox vol_val_tb;
        private System.Windows.Forms.Button buf_set_btn;
        private System.Windows.Forms.Label Damp_vol_lbl;
        private System.Windows.Forms.Button vol_set_btn;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Label label2;
    }
}