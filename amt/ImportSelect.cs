using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// ImportSelect の概要の説明です。
	/// </summary>
	public class ImportSelect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ok_btn;
		private System.Windows.Forms.RadioButton ch1_btn;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RadioButton ch3_btn;
		private Amt parent;

		public ImportSelect(Amt Parent)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			parent=Parent;
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
            this.ok_btn = new System.Windows.Forms.Button();
            this.ch3_btn = new System.Windows.Forms.RadioButton();
            this.ch1_btn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(38, 85);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(106, 36);
            this.ok_btn.TabIndex = 1;
            this.ok_btn.Text = "OK";
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // ch3_btn
            // 
            this.ch3_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ch3_btn.Location = new System.Drawing.Point(12, 43);
            this.ch3_btn.Name = "ch3_btn";
            this.ch3_btn.Size = new System.Drawing.Size(185, 36);
            this.ch3_btn.TabIndex = 2;
            this.ch3_btn.Text = "Adjust buffer size";
            // 
            // ch1_btn
            // 
            this.ch1_btn.Checked = true;
            this.ch1_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ch1_btn.Location = new System.Drawing.Point(12, 12);
            this.ch1_btn.Name = "ch1_btn";
            this.ch1_btn.Size = new System.Drawing.Size(128, 36);
            this.ch1_btn.TabIndex = 0;
            this.ch1_btn.TabStop = true;
            this.ch1_btn.Text = "Nothing";
            // 
            // ImportSelect
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(188, 135);
            this.ControlBox = false;
            this.Controls.Add(this.ch3_btn);
            this.Controls.Add(this.ch1_btn);
            this.Controls.Add(this.ok_btn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select import method";
            this.ResumeLayout(false);

		}
		#endregion

		private void ok_btn_Click(object sender, System.EventArgs e)
		{
			if(ch1_btn.Checked) parent.ip_selected=1;
			else if(ch3_btn.Checked) parent.ip_selected=3;
			Dispose( true );
		}
    }
}
