using System;
using Asio_io;

namespace amt
{
    /// <summary>
    /// IIRFilter の概要の説明です。
    /// </summary>
    public class IIRFilter : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button start_btn;
		private System.Windows.Forms.Button close_btn;
		private System.Windows.Forms.TextBox ripple_tb;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private double ripple;
		private double cut_freq;
		private double center_freq,lcut_freq,hcut_freq,smpl_freq;
		private int filter_order;
		private Amt parent;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton lpf_rb;
		private System.Windows.Forms.RadioButton hpf_rb;
		private System.Windows.Forms.RadioButton bpf_rb;
		private System.Windows.Forms.NumericUpDown order_updown;
		private System.Windows.Forms.TextBox lcut_freq_tb;
		private System.Windows.Forms.TextBox hcut_freq_tb;
		private asio_io myasio;
		private System.Windows.Forms.Label lcut_lbl;
		private System.Windows.Forms.Label hcut_lbl;
		private System.Windows.Forms.Label center_freq_unit;
		private System.Windows.Forms.Label center_freq_lbl;
		private System.Windows.Forms.TextBox center_freq_tb;
		private System.Windows.Forms.Label band_width_unit;
		private System.Windows.Forms.Label band_width_lbl;
		private System.Windows.Forms.ComboBox band_width_cb;
		private System.Windows.Forms.Label input_signal_lbl;
		private System.Windows.Forms.Label output_signal_lbl;
		private System.Windows.Forms.CheckBox octbpf_cb;
		private ChannelInfo in_ch, out_ch;
		private System.Windows.Forms.Label maxval_lbl;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox scale_tb;
		private double[] bwidth_list;
		private double scale=1.0;
		private double maxval=0.0;
		private bool bfreq_ready=false;

		public IIRFilter(Amt Parent)
		{
			InitializeComponent();
//			this.ClientSize = new System.Drawing.Size(408, 159);

			parent = Parent;
			myasio = parent.asio;
			smpl_freq=parent.sample_freq;
            in_ch = parent.data_ch[1];
            out_ch = parent.data_ch[2];
            if (in_ch != null && out_ch != null) start_btn.Enabled = true;
            input_signal_lbl.Text = "Input Buffer No.=" + parent.data_ch[1].Get_btn_name();
            output_signal_lbl.Text = "Output Buffer No.=" + parent.data_ch[2].Get_btn_name();
			band_width_cb.Items.Add("1");
			band_width_cb.Items.Add("1/3");
			band_width_cb.Items.Add("1/6");
			band_width_cb.Items.Add("1/9");
			band_width_cb.Items.Add("1/12");
			band_width_cb.Items.Add("1/15");
			band_width_cb.SelectedIndex=1;
			bwidth_list=new double[]{1.0, 3.0, 6.0, 9.0, 12.0, 15.0};
			bfreq_ready=true;
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
            this.label4 = new System.Windows.Forms.Label();
            this.input_signal_lbl = new System.Windows.Forms.Label();
            this.output_signal_lbl = new System.Windows.Forms.Label();
            this.ripple_tb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.close_btn = new System.Windows.Forms.Button();
            this.order_updown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lcut_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lcut_freq_tb = new System.Windows.Forms.TextBox();
            this.hcut_lbl = new System.Windows.Forms.Label();
            this.hcut_freq_tb = new System.Windows.Forms.TextBox();
            this.center_freq_unit = new System.Windows.Forms.Label();
            this.center_freq_lbl = new System.Windows.Forms.Label();
            this.center_freq_tb = new System.Windows.Forms.TextBox();
            this.band_width_unit = new System.Windows.Forms.Label();
            this.band_width_lbl = new System.Windows.Forms.Label();
            this.lpf_rb = new System.Windows.Forms.RadioButton();
            this.hpf_rb = new System.Windows.Forms.RadioButton();
            this.bpf_rb = new System.Windows.Forms.RadioButton();
            this.octbpf_cb = new System.Windows.Forms.CheckBox();
            this.band_width_cb = new System.Windows.Forms.ComboBox();
            this.maxval_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scale_tb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.order_updown)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(51, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ripple";
            // 
            // input_signal_lbl
            // 
            this.input_signal_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.input_signal_lbl.Location = new System.Drawing.Point(51, 104);
            this.input_signal_lbl.Name = "input_signal_lbl";
            this.input_signal_lbl.Size = new System.Drawing.Size(218, 18);
            this.input_signal_lbl.TabIndex = 4;
            this.input_signal_lbl.Text = "Input";
            // 
            // output_signal_lbl
            // 
            this.output_signal_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.output_signal_lbl.Location = new System.Drawing.Point(51, 135);
            this.output_signal_lbl.Name = "output_signal_lbl";
            this.output_signal_lbl.Size = new System.Drawing.Size(218, 18);
            this.output_signal_lbl.TabIndex = 5;
            this.output_signal_lbl.Text = "Output";
            // 
            // ripple_tb
            // 
            this.ripple_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ripple_tb.Location = new System.Drawing.Point(146, 57);
            this.ripple_tb.Name = "ripple_tb";
            this.ripple_tb.Size = new System.Drawing.Size(115, 25);
            this.ripple_tb.TabIndex = 9;
            this.ripple_tb.Text = "0.1";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(274, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "[dB]";
            // 
            // start_btn
            // 
            this.start_btn.Enabled = false;
            this.start_btn.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.start_btn.Location = new System.Drawing.Point(474, 192);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(128, 36);
            this.start_btn.TabIndex = 14;
            this.start_btn.Text = "Start";
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // close_btn
            // 
            this.close_btn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.close_btn.Location = new System.Drawing.Point(640, 192);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(102, 36);
            this.close_btn.TabIndex = 15;
            this.close_btn.Text = "Close";
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // order_updown
            // 
            this.order_updown.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.order_updown.Location = new System.Drawing.Point(146, 18);
            this.order_updown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.order_updown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.order_updown.Name = "order_updown";
            this.order_updown.Size = new System.Drawing.Size(137, 25);
            this.order_updown.TabIndex = 18;
            this.order_updown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(51, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 18);
            this.label11.TabIndex = 17;
            this.label11.Text = "Order";
            // 
            // lcut_lbl
            // 
            this.lcut_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lcut_lbl.Location = new System.Drawing.Point(589, 72);
            this.lcut_lbl.Name = "lcut_lbl";
            this.lcut_lbl.Size = new System.Drawing.Size(46, 18);
            this.lcut_lbl.TabIndex = 21;
            this.lcut_lbl.Text = "[Hz]";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(371, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Cut off freq.";
            // 
            // lcut_freq_tb
            // 
            this.lcut_freq_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lcut_freq_tb.Location = new System.Drawing.Point(499, 63);
            this.lcut_freq_tb.Name = "lcut_freq_tb";
            this.lcut_freq_tb.Size = new System.Drawing.Size(83, 25);
            this.lcut_freq_tb.TabIndex = 20;
            this.lcut_freq_tb.Text = "1000";
            // 
            // hcut_lbl
            // 
            this.hcut_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hcut_lbl.Location = new System.Drawing.Point(730, 72);
            this.hcut_lbl.Name = "hcut_lbl";
            this.hcut_lbl.Size = new System.Drawing.Size(46, 18);
            this.hcut_lbl.TabIndex = 24;
            this.hcut_lbl.Text = "[Hz]";
            this.hcut_lbl.Visible = false;
            // 
            // hcut_freq_tb
            // 
            this.hcut_freq_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hcut_freq_tb.Location = new System.Drawing.Point(640, 63);
            this.hcut_freq_tb.Name = "hcut_freq_tb";
            this.hcut_freq_tb.Size = new System.Drawing.Size(82, 25);
            this.hcut_freq_tb.TabIndex = 23;
            this.hcut_freq_tb.Text = "2000";
            this.hcut_freq_tb.Visible = false;
            // 
            // center_freq_unit
            // 
            this.center_freq_unit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.center_freq_unit.Location = new System.Drawing.Point(589, 108);
            this.center_freq_unit.Name = "center_freq_unit";
            this.center_freq_unit.Size = new System.Drawing.Size(46, 18);
            this.center_freq_unit.TabIndex = 27;
            this.center_freq_unit.Text = "[Hz]";
            this.center_freq_unit.Visible = false;
            // 
            // center_freq_lbl
            // 
            this.center_freq_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.center_freq_lbl.Location = new System.Drawing.Point(371, 108);
            this.center_freq_lbl.Name = "center_freq_lbl";
            this.center_freq_lbl.Size = new System.Drawing.Size(111, 18);
            this.center_freq_lbl.TabIndex = 25;
            this.center_freq_lbl.Text = "Center freq.";
            this.center_freq_lbl.Visible = false;
            // 
            // center_freq_tb
            // 
            this.center_freq_tb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.center_freq_tb.Location = new System.Drawing.Point(499, 99);
            this.center_freq_tb.Name = "center_freq_tb";
            this.center_freq_tb.Size = new System.Drawing.Size(83, 25);
            this.center_freq_tb.TabIndex = 26;
            this.center_freq_tb.Text = "1000";
            this.center_freq_tb.Visible = false;
            // 
            // band_width_unit
            // 
            this.band_width_unit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.band_width_unit.Location = new System.Drawing.Point(589, 144);
            this.band_width_unit.Name = "band_width_unit";
            this.band_width_unit.Size = new System.Drawing.Size(56, 18);
            this.band_width_unit.TabIndex = 30;
            this.band_width_unit.Text = "[oct.]";
            this.band_width_unit.Visible = false;
            // 
            // band_width_lbl
            // 
            this.band_width_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.band_width_lbl.Location = new System.Drawing.Point(371, 144);
            this.band_width_lbl.Name = "band_width_lbl";
            this.band_width_lbl.Size = new System.Drawing.Size(111, 18);
            this.band_width_lbl.TabIndex = 28;
            this.band_width_lbl.Text = "Band width";
            this.band_width_lbl.Visible = false;
            // 
            // lpf_rb
            // 
            this.lpf_rb.Checked = true;
            this.lpf_rb.Location = new System.Drawing.Point(397, 12);
            this.lpf_rb.Name = "lpf_rb";
            this.lpf_rb.Size = new System.Drawing.Size(77, 36);
            this.lpf_rb.TabIndex = 35;
            this.lpf_rb.TabStop = true;
            this.lpf_rb.Text = "LPF";
            // 
            // hpf_rb
            // 
            this.hpf_rb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hpf_rb.Location = new System.Drawing.Point(474, 18);
            this.hpf_rb.Name = "hpf_rb";
            this.hpf_rb.Size = new System.Drawing.Size(76, 27);
            this.hpf_rb.TabIndex = 32;
            this.hpf_rb.Text = "HPF";
            // 
            // bpf_rb
            // 
            this.bpf_rb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bpf_rb.Location = new System.Drawing.Point(550, 18);
            this.bpf_rb.Name = "bpf_rb";
            this.bpf_rb.Size = new System.Drawing.Size(77, 27);
            this.bpf_rb.TabIndex = 33;
            this.bpf_rb.Text = "BPF";
            this.bpf_rb.CheckedChanged += new System.EventHandler(this.bpf_rb_CheckedChanged);
            // 
            // octbpf_cb
            // 
            this.octbpf_cb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.octbpf_cb.Location = new System.Drawing.Point(640, 18);
            this.octbpf_cb.Name = "octbpf_cb";
            this.octbpf_cb.Size = new System.Drawing.Size(141, 27);
            this.octbpf_cb.TabIndex = 34;
            this.octbpf_cb.Text = "Octave BPF";
            this.octbpf_cb.Visible = false;
            this.octbpf_cb.CheckedChanged += new System.EventHandler(this.octbpf_cb_CheckedChanged);
            // 
            // band_width_cb
            // 
            this.band_width_cb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.band_width_cb.Location = new System.Drawing.Point(499, 135);
            this.band_width_cb.Name = "band_width_cb";
            this.band_width_cb.Size = new System.Drawing.Size(90, 26);
            this.band_width_cb.TabIndex = 36;
            this.band_width_cb.Visible = false;
            this.band_width_cb.SelectedIndexChanged += new System.EventHandler(this.band_width_cb_SelectedIndexChanged);
            // 
            // maxval_lbl
            // 
            this.maxval_lbl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maxval_lbl.Location = new System.Drawing.Point(51, 210);
            this.maxval_lbl.Name = "maxval_lbl";
            this.maxval_lbl.Size = new System.Drawing.Size(90, 18);
            this.maxval_lbl.TabIndex = 37;
            this.maxval_lbl.Text = "Max Val = 0.0";
            this.maxval_lbl.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(51, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 38;
            this.label1.Text = "Scale :";
            // 
            // scale_tb
            // 
            this.scale_tb.Location = new System.Drawing.Point(141, 166);
            this.scale_tb.Name = "scale_tb";
            this.scale_tb.Size = new System.Drawing.Size(141, 25);
            this.scale_tb.TabIndex = 40;
            this.scale_tb.Text = "1.0";
            // 
            // IIRFilter
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(795, 256);
            this.Controls.Add(this.scale_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxval_lbl);
            this.Controls.Add(this.band_width_cb);
            this.Controls.Add(this.octbpf_cb);
            this.Controls.Add(this.bpf_rb);
            this.Controls.Add(this.hpf_rb);
            this.Controls.Add(this.lpf_rb);
            this.Controls.Add(this.band_width_unit);
            this.Controls.Add(this.band_width_lbl);
            this.Controls.Add(this.center_freq_unit);
            this.Controls.Add(this.center_freq_lbl);
            this.Controls.Add(this.center_freq_tb);
            this.Controls.Add(this.hcut_lbl);
            this.Controls.Add(this.hcut_freq_tb);
            this.Controls.Add(this.lcut_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lcut_freq_tb);
            this.Controls.Add(this.order_updown);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ripple_tb);
            this.Controls.Add(this.output_signal_lbl);
            this.Controls.Add(this.input_signal_lbl);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "IIRFilter";
            this.Text = "IIRFilter";
            ((System.ComponentModel.ISupportInitialize)(this.order_updown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void start_btn_Click(object sender, System.EventArgs e)
		{
			filter_order=(int)order_updown.Value;
			ripple=System.Convert.ToDouble(ripple_tb.Text);
            cut_freq = System.Convert.ToDouble(lcut_freq_tb.Text);
            scale = System.Convert.ToDouble(scale_tb.Text);

			if(lpf_rb.Checked)
			{
				maxval=myasio.IIR_LPF(filter_order, cut_freq, ripple, scale, (int)parent.data_ch[1].type, parent.data_ch[1].btn_no, (int)parent.data_ch[2].type, parent.data_ch[2].btn_no);
				if(parent.script_recording)
				{
					string parameters=in_ch.ToString()+parent.ScriptSeparator+out_ch.ToString();
					parameters += parent.ScriptSeparator+filter_order.ToString();
					parameters += parent.ScriptSeparator+cut_freq.ToString();
					parameters += parent.ScriptSeparator+ripple.ToString();
					parameters += parent.ScriptSeparator+scale.ToString();
					parent.myScripts.Add("IIRLPF"+parent.ScriptSeparator+parameters);
				}
			}
			else if(hpf_rb.Checked)
			{
				maxval=myasio.IIR_HPF(filter_order, cut_freq, ripple, scale, (int)parent.data_ch[1].type, parent.data_ch[1].btn_no, (int)parent.data_ch[2].type, parent.data_ch[2].btn_no);
				string parameters=in_ch.ToString()+parent.ScriptSeparator+out_ch.ToString();
				parameters += parent.ScriptSeparator+filter_order.ToString();
				parameters += parent.ScriptSeparator+cut_freq.ToString();
				parameters += parent.ScriptSeparator+ripple.ToString();
				parameters += parent.ScriptSeparator+scale.ToString();
				parent.myScripts.Add("IIRHPF"+parent.ScriptSeparator+parameters);
			}
			else if(bpf_rb.Checked)
			{
				if(octbpf_cb.Checked)
				{
					center_freq=System.Convert.ToDouble(center_freq_tb.Text);
					int ii=band_width_cb.SelectedIndex;
					double band_width=bwidth_list[ii];
					maxval=myasio.IIR_OCTBPF(filter_order, center_freq, band_width, ripple, scale, (int)parent.data_ch[1].type, parent.data_ch[1].btn_no, (int)parent.data_ch[2].type, parent.data_ch[2].btn_no);
					string parameters=in_ch.ToString()+parent.ScriptSeparator+out_ch.ToString();
					parameters += parent.ScriptSeparator+filter_order.ToString();
					parameters += parent.ScriptSeparator+center_freq.ToString();
					parameters += parent.ScriptSeparator+band_width.ToString();
					parameters += parent.ScriptSeparator+ripple.ToString();
					parameters += parent.ScriptSeparator+scale.ToString();
					parent.myScripts.Add("OCTBPF"+parent.ScriptSeparator+parameters);
				}
				else
				{
					lcut_freq=cut_freq;
					hcut_freq=System.Convert.ToDouble(hcut_freq_tb.Text);
					maxval=myasio.IIR_BPF(filter_order, lcut_freq, hcut_freq, ripple, scale, (int)parent.data_ch[1].type, parent.data_ch[1].btn_no, (int)parent.data_ch[2].type, parent.data_ch[2].btn_no);
					string parameters=in_ch.ToString()+parent.ScriptSeparator+out_ch.ToString();
					parameters += parent.ScriptSeparator+filter_order.ToString();
					parameters += parent.ScriptSeparator+lcut_freq.ToString();
					parameters += parent.ScriptSeparator+hcut_freq.ToString();
					parameters += parent.ScriptSeparator+ripple.ToString();
					parameters += parent.ScriptSeparator+scale.ToString();
					parent.myScripts.Add("IIRBPF"+parent.ScriptSeparator+parameters);
				}
			}
			maxval_lbl.Text = maxval.ToString("F4");
            maxval_lbl.Visible = true;
            parent.Created_signal(parent.data_ch[2]);
		}

		private void close_btn_Click(object sender, System.EventArgs e)
		{
			Dispose(true);
		}

		private void bpf_rb_CheckedChanged(object sender, System.EventArgs e)
		{
			if(bpf_rb.Checked)
			{
				lcut_lbl.Text=" ---";
				hcut_lbl.Visible=true;
				hcut_freq_tb.Visible=true;
				octbpf_cb.Visible=true;
				this.ClientSize = new System.Drawing.Size(508, 159);
			}
			else
			{
				lcut_lbl.Text="[Hz]";
				hcut_lbl.Visible=false;
				hcut_freq_tb.Visible=false;
				octbpf_cb.Visible=false;
//				this.ClientSize = new System.Drawing.Size(408, 159);
				octbpf_cb.Checked=false;
			}	
		}

		private void octbpf_cb_CheckedChanged(object sender, System.EventArgs e)
		{
			center_freq_lbl.Visible=center_freq_tb.Visible=center_freq_unit.Visible=octbpf_cb.Checked;
			band_width_lbl.Visible=band_width_cb.Visible=band_width_unit.Visible=octbpf_cb.Checked;
			if(octbpf_cb.Checked)
			{
				set_band_freq(bfreq_ready);
			}
		}

		private void set_band_freq(bool ready)
		{
			double bw;
			if(ready==false) return;
			center_freq=System.Convert.ToDouble(center_freq_tb.Text);
			int ii=band_width_cb.SelectedIndex;
			double band_width=bwidth_list[ii];
			if(band_width==3.0) bw = Math.Pow(10.0,0.05) ;
			else bw = Math.Pow(2.0,0.5/band_width) ;

			lcut_freq = center_freq / bw ;
			hcut_freq = center_freq * bw ;
			lcut_freq_tb.Text=lcut_freq.ToString("F2");
			hcut_freq_tb.Text=hcut_freq.ToString("F2");
		}

		private void band_width_cb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			set_band_freq(bfreq_ready);
		}
	}
}
