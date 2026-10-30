using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// ProgressBarForm の概要の説明です。
	/// </summary>
	public class ProgressBarForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProgressBarForm(int Start, int End)
		{
			InitializeComponent();
			progressBar1.Minimum = Start;
			progressBar1.Maximum = End;
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 16);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(312, 24);
			this.progressBar1.TabIndex = 0;
			// 
			// ProgressBarForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(344, 56);
			this.ControlBox = false;
			this.Controls.Add(this.progressBar1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressBarForm";
			this.ResumeLayout(false);

		}
		#endregion

		public void SetBar(int Curr)
		{
			progressBar1.Value = Curr;
		}
	}
}
