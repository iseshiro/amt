using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// VersionInfo の概要の説明です。
	/// </summary>
	public class VersionInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ok_btn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label version_lbl;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public VersionInfo(string verinfo)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			this.version_lbl.Text="Version "+verinfo;
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
            this.label1 = new System.Windows.Forms.Label();
            this.version_lbl = new System.Windows.Forms.Label();
            this.ok_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Acoustic Measurement Tool";
            // 
            // version_lbl
            // 
            this.version_lbl.Location = new System.Drawing.Point(32, 40);
            this.version_lbl.Name = "version_lbl";
            this.version_lbl.Size = new System.Drawing.Size(100, 23);
            this.version_lbl.TabIndex = 1;
            this.version_lbl.Text = "Version 1.1";
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(104, 80);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(56, 24);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "OK";
            this.ok_btn.Click += new System.EventHandler(this.Ok_btn_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(152, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "written by S.ISE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // VersionInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(264, 110);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.version_lbl);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VersionInfo";
            this.ResumeLayout(false);

		}
		#endregion

		private void Ok_btn_Click(object sender, System.EventArgs e)
		{
            Dispose();
		}
	}
}
