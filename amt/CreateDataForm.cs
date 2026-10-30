using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// CreateDataForm の概要の説明です。
	/// </summary>
	public class CreateDataForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ok_button;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label msg_lbl;
		private System.Windows.Forms.TextBox Parameter_tb;
		private Amt parent;
        private int asio_buf_size;

        public CreateDataForm(Amt Parent, string msg, string init_val_txt)
		{
			InitializeComponent();
			parent = Parent;
			this.msg_lbl.Text = msg;
			this.Parameter_tb.Text = init_val_txt;
            asio_buf_size = parent.asio.GetPreferredSize();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.ok_button = new System.Windows.Forms.Button();
            this.Parameter_tb = new System.Windows.Forms.TextBox();
            this.msg_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(56, 96);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(64, 24);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "OK";
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // Parameter_tb
            // 
            this.Parameter_tb.Location = new System.Drawing.Point(56, 64);
            this.Parameter_tb.Name = "Parameter_tb";
            this.Parameter_tb.Size = new System.Drawing.Size(72, 19);
            this.Parameter_tb.TabIndex = 5;
            this.Parameter_tb.Text = "8192";
            this.Parameter_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Parameter_tb.TextChanged += new System.EventHandler(this.Parameter_tb_TextChanged);
            // 
            // msg_lbl
            // 
            this.msg_lbl.Location = new System.Drawing.Point(16, 16);
            this.msg_lbl.Name = "msg_lbl";
            this.msg_lbl.Size = new System.Drawing.Size(152, 40);
            this.msg_lbl.TabIndex = 6;
            this.msg_lbl.Text = "Please ";
            // 
            // CreateDataForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(176, 133);
            this.ControlBox = false;
            this.Controls.Add(this.msg_lbl);
            this.Controls.Add(this.Parameter_tb);
            this.Controls.Add(this.ok_button);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateDataForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ok_button_Click(object sender, System.EventArgs e)
		{
			parent.reply_msg=Parameter_tb.Text;
			Dispose();
		}

        private void Parameter_tb_TextChanged(object sender, EventArgs e)
        {
            if (Parameter_tb.Text == "")
            {
                ok_button.Enabled = false;
            }
            else
            {
                double d;
                double.TryParse(Parameter_tb.Text, out d);
                int dleng = (int)d;
                ok_button.Enabled = true;
            }
        }
	}
}
