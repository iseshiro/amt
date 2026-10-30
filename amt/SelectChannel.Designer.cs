namespace amt
{
    partial class SelectChannel
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
            this.numericUpDown_ch = new System.Windows.Forms.NumericUpDown();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ch)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_ch
            // 
            this.numericUpDown_ch.Location = new System.Drawing.Point(65, 22);
            this.numericUpDown_ch.Name = "numericUpDown_ch";
            this.numericUpDown_ch.Size = new System.Drawing.Size(108, 25);
            this.numericUpDown_ch.TabIndex = 0;
            // 
            // ok_btn
            // 
            this.ok_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_btn.Location = new System.Drawing.Point(28, 67);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(83, 36);
            this.ok_btn.TabIndex = 1;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(134, 67);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(83, 36);
            this.cancel_btn.TabIndex = 2;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // SelectChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 117);
            this.ControlBox = false;
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.numericUpDown_ch);
            this.Name = "SelectChannel";
            this.Text = "SelectChannel";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_ch;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
    }
}