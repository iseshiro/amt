using System;
using System.Windows.Forms;
using Asio_io;

namespace amt
{
    /// <summary>
    /// DSP_Form の概要の説明です。
    /// </summary>
    public class DSP_Form : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label Explanation_lbl;
//		private System.Windows.Forms.Label[] buf_lbl;
//		private System.Windows.Forms.RadioButton[] in1_radioButton;
//		private System.Windows.Forms.RadioButton[] in2_radioButton;
//		private System.Windows.Forms.RadioButton[] out_radioButton;
		private asio_io myasio;
		private System.Windows.Forms.Button cancel_btn;
		private System.Windows.Forms.Button ok_btn;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		enum Proc {Convolution, Deconvolution, Add, Multiply};
		Proc process_id;
//		int bufCnt;
		private Amt parent;
        private ChannelInfo d1_ch = null, d2_ch = null, d3_ch = null;
        private Label freq_range_lbl;
        private TextBox freq_st_tb;
        private TextBox freq_ed_tb;
        private Label scale_lbl;
        private TextBox scale_tb;
        private Label freq_fromto_lbl;

		public DSP_Form(Amt Parent, String process)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			parent = Parent;
			myasio=parent.asio;

			if(process == "Convolution") process_id=Proc.Convolution;
			else if(process == "Deconvolution") process_id=Proc.Deconvolution;
			else if(process == "Add") process_id=Proc.Add;
			else if(process == "Multiply") process_id=Proc.Multiply;
//			bufCnt=myasio.GetBufCnt();

            set_freq_range(false);

			d1_ch=parent.data_ch[1];
			d2_ch=parent.data_ch[2];
			d3_ch=parent.data_ch[3];
            if (d1_ch == null || d2_ch == null || d3_ch == null)
			{
				Explanation_lbl.Text = "Error: Please assign data";
				ok_btn.Enabled = false;
			}
			else
			{
				switch(process_id)
				{
					case Proc.Convolution:
                        Explanation_lbl.Text = "Convolution: Data3 (" + d3_ch.Get_btn_name() + ") = Data1 (" + d1_ch.Get_btn_name() + ") * Data2 (" + d2_ch.Get_btn_name() + ")";
                        set_freq_range(true);
						break;
					case Proc.Deconvolution:
						Explanation_lbl.Text = "Deconvolution: Data3 ("+d3_ch.Get_btn_name()+") = Data2 ("+d2_ch.Get_btn_name()+")/Data1 ("+d1_ch.Get_btn_name()+")";
                        set_freq_range(true);
                        break;
					case Proc.Add:
						Explanation_lbl.Text = "Add: Data3 ("+d3_ch.Get_btn_name()+") = Data1 ("+d1_ch.Get_btn_name()+")+Data2 ("+d2_ch.Get_btn_name()+")";
                        break;
					case Proc.Multiply:
						Explanation_lbl.Text = "Multiply: Data3 ("+d3_ch.Get_btn_name()+") = Data1 ("+d1_ch.Get_btn_name()+") X Data2 ("+d2_ch.Get_btn_name()+")";
						break;
					default:
						Text = "No name";
						Explanation_lbl.Text = "No explanation";
						break;
				}
			}
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
            this.cancel_btn = new System.Windows.Forms.Button();
            this.Explanation_lbl = new System.Windows.Forms.Label();
            this.ok_btn = new System.Windows.Forms.Button();
            this.freq_range_lbl = new System.Windows.Forms.Label();
            this.freq_st_tb = new System.Windows.Forms.TextBox();
            this.freq_ed_tb = new System.Windows.Forms.TextBox();
            this.freq_fromto_lbl = new System.Windows.Forms.Label();
            this.scale_lbl = new System.Windows.Forms.Label();
            this.scale_tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(380, 81);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(64, 24);
            this.cancel_btn.TabIndex = 4;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // Explanation_lbl
            // 
            this.Explanation_lbl.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Explanation_lbl.Location = new System.Drawing.Point(16, 24);
            this.Explanation_lbl.Name = "Explanation_lbl";
            this.Explanation_lbl.Size = new System.Drawing.Size(432, 40);
            this.Explanation_lbl.TabIndex = 6;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(310, 81);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(64, 24);
            this.ok_btn.TabIndex = 10;
            this.ok_btn.Text = "OK";
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // freq_range_lbl
            // 
            this.freq_range_lbl.AutoSize = true;
            this.freq_range_lbl.Location = new System.Drawing.Point(17, 77);
            this.freq_range_lbl.Name = "freq_range_lbl";
            this.freq_range_lbl.Size = new System.Drawing.Size(87, 12);
            this.freq_range_lbl.TabIndex = 11;
            this.freq_range_lbl.Text = "Freq. range [Hz]";
            // 
            // freq_st_tb
            // 
            this.freq_st_tb.Location = new System.Drawing.Point(6, 92);
            this.freq_st_tb.Name = "freq_st_tb";
            this.freq_st_tb.Size = new System.Drawing.Size(58, 19);
            this.freq_st_tb.TabIndex = 12;
            this.freq_st_tb.Text = "0";
            // 
            // freq_ed_tb
            // 
            this.freq_ed_tb.Location = new System.Drawing.Point(81, 92);
            this.freq_ed_tb.Name = "freq_ed_tb";
            this.freq_ed_tb.Size = new System.Drawing.Size(58, 19);
            this.freq_ed_tb.TabIndex = 13;
            this.freq_ed_tb.Text = "max";
            // 
            // freq_fromto_lbl
            // 
            this.freq_fromto_lbl.AutoSize = true;
            this.freq_fromto_lbl.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.freq_fromto_lbl.Location = new System.Drawing.Point(64, 92);
            this.freq_fromto_lbl.Name = "freq_fromto_lbl";
            this.freq_fromto_lbl.Size = new System.Drawing.Size(21, 21);
            this.freq_fromto_lbl.TabIndex = 14;
            this.freq_fromto_lbl.Text = "-";
            // 
            // scale_lbl
            // 
            this.scale_lbl.AutoSize = true;
            this.scale_lbl.Location = new System.Drawing.Point(186, 77);
            this.scale_lbl.Name = "scale_lbl";
            this.scale_lbl.Size = new System.Drawing.Size(67, 12);
            this.scale_lbl.TabIndex = 15;
            this.scale_lbl.Text = "Scale factor";
            // 
            // scale_tb
            // 
            this.scale_tb.Location = new System.Drawing.Point(188, 92);
            this.scale_tb.Name = "scale_tb";
            this.scale_tb.Size = new System.Drawing.Size(65, 19);
            this.scale_tb.TabIndex = 16;
            this.scale_tb.Text = "1.0";
            // 
            // DSP_Form
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(456, 117);
            this.Controls.Add(this.scale_tb);
            this.Controls.Add(this.scale_lbl);
            this.Controls.Add(this.freq_ed_tb);
            this.Controls.Add(this.freq_st_tb);
            this.Controls.Add(this.freq_range_lbl);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.Explanation_lbl);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.freq_fromto_lbl);
            this.Name = "DSP_Form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ok_btn_Click(object sender, System.EventArgs e)
		{
			switch(process_id)
			{
                case Proc.Convolution:
                    {
                        double scale = System.Convert.ToDouble(scale_tb.Text);
                        double freq_st = System.Convert.ToDouble(freq_st_tb.Text);
                        double freq_ed = System.Convert.ToDouble(freq_ed_tb.Text);
                        parent.BufConvolution(d1_ch, d2_ch, d3_ch, scale, freq_st, freq_ed);
                    }
                    break;
                case Proc.Deconvolution:
                    {
                        double scale = System.Convert.ToDouble(scale_tb.Text);
                        double freq_st = System.Convert.ToDouble(freq_st_tb.Text);
                        double freq_ed = System.Convert.ToDouble(freq_ed_tb.Text);
                        parent.BufDeconvolution(d1_ch, d2_ch, d3_ch, scale, freq_st, freq_ed);
                    }
                    break;
				case Proc.Add:
                    myasio.Add((int)d1_ch.type, d1_ch.btn_no, (int)d2_ch.type, d2_ch.btn_no, (int)d3_ch.type, d3_ch.btn_no);
					if(parent.script_recording)
					{
						parent.MyScripts_Add3("Add", d1_ch, d2_ch, d3_ch);
					}
					break;
				case Proc.Multiply:
                    myasio.Multiply((int)d1_ch.type, d1_ch.btn_no, (int)d2_ch.type, d2_ch.btn_no, (int)d3_ch.type, d3_ch.btn_no);
					if(parent.script_recording)
					{
                        parent.MyScripts_Add3("Multiply", d1_ch, d2_ch, d3_ch);
					}
					break;
				default:
					break;
			}
            parent.Created_signal(d3_ch);
			parent.Cnsl_msg(Explanation_lbl.Text);
			this.Dispose();
		}

		private void cancel_btn_Click(object sender, System.EventArgs e)
		{
            this.Dispose();
		}

        private void set_freq_range(bool visible)
        {
            freq_range_lbl.Visible = visible;
            freq_fromto_lbl.Visible = visible;
            freq_st_tb.Visible = visible;
            freq_ed_tb.Visible = visible;
            scale_lbl.Visible = visible;
            scale_tb.Visible = visible;

            freq_st_tb.Text = "0";
            int ed_freq = parent.sample_freq / 2;
            freq_ed_tb.Text = ed_freq.ToString();
        }
	}
}
