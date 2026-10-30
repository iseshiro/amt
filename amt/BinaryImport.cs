using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace amt
{
	/// <summary>
	/// BinaryImport の概要の説明です。
	/// </summary>
	public class BinaryImport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ok_btn;
		private System.Windows.Forms.Button cancel_btn;
		private System.Windows.Forms.TextBox channel_count_tb;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox channel_no_tb;
		private System.Windows.Forms.Label fileinfo_lbl;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int Filesize;
		private int word_count;
		private System.Windows.Forms.Label word_cnt_lbl;
		private System.Windows.Forms.Label File_size_lbl;
		private int channel_count=1;
		private int channel_number=1;
		private string Filename;
		private Amt parent;
		private System.Windows.Forms.CheckBox byte_swap_cb;
        private ChannelInfo cinfo;
        private double amp16;

        public BinaryImport(ChannelInfo Cinfo, string filename, long filesize, Amt Parent)
		{
			InitializeComponent();
			Filename = filename;
            fileinfo_lbl.Text = "Filename: " + filename;
            File_size_lbl.Text = "Filesize: " + filesize.ToString();
			Filesize = (int)filesize;
			word_count = Filesize/2/channel_count;
			word_cnt_lbl.Text = "Word count: "+word_count.ToString();
			parent = Parent;
            cinfo = Cinfo;
            amp16 = parent.asio.get_amp16();
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
			this.fileinfo_lbl = new System.Windows.Forms.Label();
			this.ok_btn = new System.Windows.Forms.Button();
			this.cancel_btn = new System.Windows.Forms.Button();
			this.channel_count_tb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.channel_no_tb = new System.Windows.Forms.TextBox();
			this.word_cnt_lbl = new System.Windows.Forms.Label();
			this.File_size_lbl = new System.Windows.Forms.Label();
			this.byte_swap_cb = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// fileinfo_lbl
			// 
			this.fileinfo_lbl.Location = new System.Drawing.Point(16, 16);
			this.fileinfo_lbl.Name = "fileinfo_lbl";
			this.fileinfo_lbl.Size = new System.Drawing.Size(384, 20);
			this.fileinfo_lbl.TabIndex = 0;
			this.fileinfo_lbl.Text = "File name:";
			// 
			// ok_btn
			// 
			this.ok_btn.Location = new System.Drawing.Point(200, 96);
			this.ok_btn.Name = "ok_btn";
			this.ok_btn.TabIndex = 1;
			this.ok_btn.Text = "OK";
			this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
			// 
			// cancel_btn
			// 
			this.cancel_btn.Location = new System.Drawing.Point(288, 96);
			this.cancel_btn.Name = "cancel_btn";
			this.cancel_btn.TabIndex = 2;
			this.cancel_btn.Text = "Cancel";
			this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
			// 
			// channel_count_tb
			// 
			this.channel_count_tb.Location = new System.Drawing.Point(104, 80);
			this.channel_count_tb.Name = "channel_count_tb";
			this.channel_count_tb.Size = new System.Drawing.Size(32, 19);
			this.channel_count_tb.TabIndex = 3;
			this.channel_count_tb.Text = "1";
			this.channel_count_tb.TextChanged += new System.EventHandler(this.channel_count_tb_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Channel count";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Channel number";
			// 
			// channel_no_tb
			// 
			this.channel_no_tb.Location = new System.Drawing.Point(104, 104);
			this.channel_no_tb.Name = "channel_no_tb";
			this.channel_no_tb.Size = new System.Drawing.Size(32, 19);
			this.channel_no_tb.TabIndex = 5;
			this.channel_no_tb.Text = "1";
			this.channel_no_tb.TextChanged += new System.EventHandler(this.channel_no_tb_TextChanged);
			// 
			// word_cnt_lbl
			// 
			this.word_cnt_lbl.Location = new System.Drawing.Point(16, 60);
			this.word_cnt_lbl.Name = "word_cnt_lbl";
			this.word_cnt_lbl.Size = new System.Drawing.Size(144, 16);
			this.word_cnt_lbl.TabIndex = 9;
			this.word_cnt_lbl.Text = "Word count:";
			// 
			// File_size_lbl
			// 
			this.File_size_lbl.Location = new System.Drawing.Point(16, 40);
			this.File_size_lbl.Name = "File_size_lbl";
			this.File_size_lbl.Size = new System.Drawing.Size(144, 16);
			this.File_size_lbl.TabIndex = 10;
			this.File_size_lbl.Text = "File size:";
			// 
			// byte_swap_cb
			// 
			this.byte_swap_cb.Checked = true;
			this.byte_swap_cb.CheckState = System.Windows.Forms.CheckState.Checked;
			this.byte_swap_cb.Location = new System.Drawing.Point(208, 60);
			this.byte_swap_cb.Name = "byte_swap_cb";
			this.byte_swap_cb.Size = new System.Drawing.Size(80, 20);
			this.byte_swap_cb.TabIndex = 11;
			this.byte_swap_cb.Text = "Byte swap";
			// 
			// BinaryImport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(408, 133);
			this.Controls.Add(this.byte_swap_cb);
			this.Controls.Add(this.File_size_lbl);
			this.Controls.Add(this.word_cnt_lbl);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.channel_no_tb);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.channel_count_tb);
			this.Controls.Add(this.cancel_btn);
			this.Controls.Add(this.ok_btn);
			this.Controls.Add(this.fileinfo_lbl);
			this.Name = "BinaryImport";
			this.Text = "BinaryImport";
			this.ResumeLayout(false);

		}
		#endregion

		private void channel_count_tb_TextChanged(object sender, System.EventArgs e)
		{
			channel_count = System.Convert.ToInt32(channel_count_tb.Text);
			if(channel_count>0)
			{
				word_count = Filesize/2/channel_count;
				word_cnt_lbl.Text = "Word count: "+word_count.ToString();
			}		
		}

        public void GetRead(int dleng, bool byteswap)
		{
			FileStream fs = FileProcess.OpenRead(Filename);
            short[] buf;
            float[] data;
            data = new float[dleng];
			buf = new short[channel_count];
            if (byteswap)
			{
				for(int i=0;i<dleng;i++)
				{
					for(int j=0;j<channel_count;j++)
					{
						buf[j] = get_short_swap(fs);
					}
                    data[i] = (float)(amp16 * buf[channel_number - 1]);
				}
			}
			else
			{
				for(int i=0;i<dleng;i++)
				{
					for(int j=0;j<channel_count;j++)
					{
						buf[j] = get_short(fs);
					}
                    data[i] = (float)(amp16 * buf[channel_number - 1]);
                }
			}
			unsafe
			{
				fixed(float *dd=data)
				{
                    parent.asio.load_data((int)cinfo.type, cinfo.btn_no, dd, word_count, 1, 1);
				}
			}
            parent.SetButtonStatus_Signal(cinfo);
		}

		private void ok_btn_Click(object sender, System.EventArgs e)
		{
            int dleng = parent.asio.GetDataLength();
			if(dleng>word_count)
			{
				dleng=word_count;
			}
			GetRead(dleng, byte_swap_cb.Checked);
			if(parent.script_recording)
			{
                string parameters = "BinRead";
                parameters += parent.ScriptSeparator + Filename;
				parameters += parent.ScriptSeparator+Filesize.ToString();
                parameters += parent.ScriptSeparator + dleng.ToString();
                parameters += parent.ScriptSeparator + byte_swap_cb.Checked.ToString();
                parent.MyScripts_Add(parameters, cinfo);
			}

			Dispose();
		}

		private short get_short(FileStream fsg)
		{
			byte[] wh=new byte[2];
            Byte2int16 aa = new Byte2int16();
			int read = fsg.Read (wh, 0, 2);
			aa.b0=wh[0];
			aa.b1=wh[1];
			return aa.s0;
		}

		private short get_short_swap(FileStream fsg)
		{
			byte[] wh=new byte[2];
            Byte2int16 aa = new Byte2int16();
			int read = fsg.Read (wh, 0, 2);
			aa.b0=wh[1];
			aa.b1=wh[0];
			return aa.s0;
		}

		private void channel_no_tb_TextChanged(object sender, System.EventArgs e)
		{
			channel_number = System.Convert.ToInt32(channel_no_tb.Text);
		}

		private void cancel_btn_Click(object sender, System.EventArgs e)
		{
			Dispose();
		}
	}
}
