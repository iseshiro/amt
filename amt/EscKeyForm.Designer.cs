namespace amt
{
    partial class EscKeyForm
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
            this.script_break_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // script_break_btn
            // 
            this.script_break_btn.Location = new System.Drawing.Point(70, 58);
            this.script_break_btn.Name = "script_break_btn";
            this.script_break_btn.Size = new System.Drawing.Size(103, 32);
            this.script_break_btn.TabIndex = 0;
            this.script_break_btn.Text = "STOP";
            this.script_break_btn.UseVisualStyleBackColor = true;
            this.script_break_btn.Click += new System.EventHandler(this.script_break_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Script test1 is now executing.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "To stop the script, push stop button.";
            // 
            // EscKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 102);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.script_break_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EscKeyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button script_break_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}