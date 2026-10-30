using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// CreateDataForm の概要の説明です。
	/// </summary>
	public class FirstSettingForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button ok_button;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label data_length_lbl;
        private System.Windows.Forms.TextBox data_length_tb;
        private Label label3;
        private TextBox asio_bufsize_tb;
        private Amt parent;
        private int dleng;
        private Label message_lbl;
        private Label max_length_lbl;
        private int asio_buf_size;
        private int max_dleng_kword;
        private Label label1;
        private TextBox max_row_of_button_tb;
        private Label label4;
        private TextBox input_button_count_tb;
        private Label label5;
        private TextBox output_button_count_tb;
        private Label max_outbuf_lbl;
        private Label max_inbuf_lbl;
        private CheckBox assign_ch_cb;
        private ComboBox asio_driver_list_cb;
        private Label label6;
        private GroupBox assign_ch_gb;
        private Label label7;
        private Label label8;
        private TextBox out_ch_tb;
        private TextBox in_ch_tb;
        private CheckBox use_bosc96ch_cb;
        private CheckBox without_asio_cb;
        private string old_data_length_text;

        public FirstSettingForm(Amt Parent, int thread_count)
        {
            InitializeComponent();
            parent = Parent;
            AddDriver();
            assign_ch_cb.Checked = parent.assign_ch;

            int ii = parent.in_btn_cnt;
            input_button_count_tb.Text = ii.ToString();
            ii = parent.out_btn_cnt;
            output_button_count_tb.Text = ii.ToString();

            if (parent.assign_in_ch != "")
            {
                in_ch_tb.Text = parent.assign_in_ch;
            }
            else
            {
                in_ch_tb.Text = "1-" + input_button_count_tb.Text;
            }

            if (parent.assign_out_ch != "")
            {
                out_ch_tb.Text = parent.assign_out_ch;
            }
            else
            {
                out_ch_tb.Text = "1-" + output_button_count_tb.Text;
            }

            int jj = parent.max_row_of_button;
            max_row_of_button_tb.Text = jj.ToString();
            //max_core_count_lbl.Text = "Max core count = " + thread_count.ToString();
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.ok_button = new System.Windows.Forms.Button();
            this.data_length_tb = new System.Windows.Forms.TextBox();
            this.data_length_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.asio_bufsize_tb = new System.Windows.Forms.TextBox();
            this.message_lbl = new System.Windows.Forms.Label();
            this.max_length_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.max_row_of_button_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.input_button_count_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.output_button_count_tb = new System.Windows.Forms.TextBox();
            this.max_outbuf_lbl = new System.Windows.Forms.Label();
            this.max_inbuf_lbl = new System.Windows.Forms.Label();
            this.assign_ch_cb = new System.Windows.Forms.CheckBox();
            this.asio_driver_list_cb = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.assign_ch_gb = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.out_ch_tb = new System.Windows.Forms.TextBox();
            this.in_ch_tb = new System.Windows.Forms.TextBox();
            this.use_bosc96ch_cb = new System.Windows.Forms.CheckBox();
            this.without_asio_cb = new System.Windows.Forms.CheckBox();
            this.assign_ch_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(289, 247);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(106, 40);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "OK";
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // data_length_tb
            // 
            this.data_length_tb.Location = new System.Drawing.Point(490, 51);
            this.data_length_tb.Name = "data_length_tb";
            this.data_length_tb.Size = new System.Drawing.Size(85, 25);
            this.data_length_tb.TabIndex = 5;
            this.data_length_tb.Text = "8192";
            this.data_length_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.data_length_tb.TextChanged += new System.EventHandler(this.data_length_tb_TextChanged);
            // 
            // data_length_lbl
            // 
            this.data_length_lbl.Location = new System.Drawing.Point(384, 52);
            this.data_length_lbl.Name = "data_length_lbl";
            this.data_length_lbl.Size = new System.Drawing.Size(113, 24);
            this.data_length_lbl.TabIndex = 6;
            this.data_length_lbl.Text = "Buffer Size";
            this.data_length_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "ASIO buffer size";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // asio_bufsize_tb
            // 
            this.asio_bufsize_tb.Enabled = false;
            this.asio_bufsize_tb.Location = new System.Drawing.Point(210, 84);
            this.asio_bufsize_tb.Name = "asio_bufsize_tb";
            this.asio_bufsize_tb.Size = new System.Drawing.Size(84, 25);
            this.asio_bufsize_tb.TabIndex = 9;
            this.asio_bufsize_tb.Text = "fail";
            this.asio_bufsize_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // message_lbl
            // 
            this.message_lbl.Location = new System.Drawing.Point(433, 84);
            this.message_lbl.Name = "message_lbl";
            this.message_lbl.Size = new System.Drawing.Size(232, 34);
            this.message_lbl.TabIndex = 11;
            this.message_lbl.Text = "Can not use stream mode";
            this.message_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // max_length_lbl
            // 
            this.max_length_lbl.Location = new System.Drawing.Point(590, 48);
            this.max_length_lbl.Name = "max_length_lbl";
            this.max_length_lbl.Size = new System.Drawing.Size(84, 30);
            this.max_length_lbl.TabIndex = 12;
            this.max_length_lbl.Text = "8192";
            this.max_length_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "Max row of buttons";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // max_row_of_button_tb
            // 
            this.max_row_of_button_tb.Location = new System.Drawing.Point(210, 200);
            this.max_row_of_button_tb.Name = "max_row_of_button_tb";
            this.max_row_of_button_tb.Size = new System.Drawing.Size(59, 25);
            this.max_row_of_button_tb.TabIndex = 14;
            this.max_row_of_button_tb.Text = "16";
            this.max_row_of_button_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(30, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "Number of Input";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // input_button_count_tb
            // 
            this.input_button_count_tb.Location = new System.Drawing.Point(210, 131);
            this.input_button_count_tb.Name = "input_button_count_tb";
            this.input_button_count_tb.Size = new System.Drawing.Size(84, 25);
            this.input_button_count_tb.TabIndex = 16;
            this.input_button_count_tb.Text = "6";
            this.input_button_count_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(30, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Number of Output";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // output_button_count_tb
            // 
            this.output_button_count_tb.Location = new System.Drawing.Point(210, 165);
            this.output_button_count_tb.Name = "output_button_count_tb";
            this.output_button_count_tb.Size = new System.Drawing.Size(84, 25);
            this.output_button_count_tb.TabIndex = 18;
            this.output_button_count_tb.Text = "6";
            this.output_button_count_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // max_outbuf_lbl
            // 
            this.max_outbuf_lbl.Location = new System.Drawing.Point(306, 165);
            this.max_outbuf_lbl.Name = "max_outbuf_lbl";
            this.max_outbuf_lbl.Size = new System.Drawing.Size(84, 28);
            this.max_outbuf_lbl.TabIndex = 20;
            this.max_outbuf_lbl.Text = "8192";
            this.max_outbuf_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // max_inbuf_lbl
            // 
            this.max_inbuf_lbl.Location = new System.Drawing.Point(306, 131);
            this.max_inbuf_lbl.Name = "max_inbuf_lbl";
            this.max_inbuf_lbl.Size = new System.Drawing.Size(84, 28);
            this.max_inbuf_lbl.TabIndex = 21;
            this.max_inbuf_lbl.Text = "8192";
            this.max_inbuf_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // assign_ch_cb
            // 
            this.assign_ch_cb.AutoSize = true;
            this.assign_ch_cb.Location = new System.Drawing.Point(408, 136);
            this.assign_ch_cb.Name = "assign_ch_cb";
            this.assign_ch_cb.Size = new System.Drawing.Size(148, 22);
            this.assign_ch_cb.TabIndex = 22;
            this.assign_ch_cb.Text = "Assign Channel";
            this.assign_ch_cb.UseVisualStyleBackColor = true;
            this.assign_ch_cb.CheckedChanged += new System.EventHandler(this.assign_ch_cb_CheckedChanged);
            // 
            // asio_driver_list_cb
            // 
            this.asio_driver_list_cb.Enabled = false;
            this.asio_driver_list_cb.FormattingEnabled = true;
            this.asio_driver_list_cb.Location = new System.Drawing.Point(149, 46);
            this.asio_driver_list_cb.Name = "asio_driver_list_cb";
            this.asio_driver_list_cb.Size = new System.Drawing.Size(214, 26);
            this.asio_driver_list_cb.TabIndex = 25;
            this.asio_driver_list_cb.SelectedIndexChanged += new System.EventHandler(this.asio_driver_list_cb_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(30, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 26);
            this.label6.TabIndex = 26;
            this.label6.Text = "ASIO Driver";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // assign_ch_gb
            // 
            this.assign_ch_gb.Controls.Add(this.label7);
            this.assign_ch_gb.Controls.Add(this.label8);
            this.assign_ch_gb.Controls.Add(this.out_ch_tb);
            this.assign_ch_gb.Controls.Add(this.in_ch_tb);
            this.assign_ch_gb.Enabled = false;
            this.assign_ch_gb.Location = new System.Drawing.Point(406, 136);
            this.assign_ch_gb.Name = "assign_ch_gb";
            this.assign_ch_gb.Size = new System.Drawing.Size(301, 102);
            this.assign_ch_gb.TabIndex = 27;
            this.assign_ch_gb.TabStop = false;
            this.assign_ch_gb.Text = "Assign Channel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "OUT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "IN";
            // 
            // out_ch_tb
            // 
            this.out_ch_tb.Location = new System.Drawing.Point(99, 63);
            this.out_ch_tb.Name = "out_ch_tb";
            this.out_ch_tb.Size = new System.Drawing.Size(181, 25);
            this.out_ch_tb.TabIndex = 10;
            this.out_ch_tb.Text = "1-2";
            // 
            // in_ch_tb
            // 
            this.in_ch_tb.Location = new System.Drawing.Point(99, 26);
            this.in_ch_tb.Name = "in_ch_tb";
            this.in_ch_tb.Size = new System.Drawing.Size(181, 25);
            this.in_ch_tb.TabIndex = 9;
            this.in_ch_tb.Text = "1-2";
            // 
            // use_bosc96ch_cb
            // 
            this.use_bosc96ch_cb.AutoSize = true;
            this.use_bosc96ch_cb.Location = new System.Drawing.Point(289, 206);
            this.use_bosc96ch_cb.Name = "use_bosc96ch_cb";
            this.use_bosc96ch_cb.Size = new System.Drawing.Size(101, 22);
            this.use_bosc96ch_cb.TabIndex = 29;
            this.use_bosc96ch_cb.Text = "use 96ch";
            this.use_bosc96ch_cb.UseVisualStyleBackColor = true;
            this.use_bosc96ch_cb.CheckedChanged += new System.EventHandler(this.use_bosc96ch_cb_CheckedChanged);
            // 
            // without_asio_cb
            // 
            this.without_asio_cb.AutoSize = true;
            this.without_asio_cb.Checked = true;
            this.without_asio_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.without_asio_cb.Location = new System.Drawing.Point(35, 12);
            this.without_asio_cb.Name = "without_asio_cb";
            this.without_asio_cb.Size = new System.Drawing.Size(134, 22);
            this.without_asio_cb.TabIndex = 30;
            this.without_asio_cb.Text = "Without ASIO";
            this.without_asio_cb.UseVisualStyleBackColor = true;
            this.without_asio_cb.CheckedChanged += new System.EventHandler(this.without_asio_cb_CheckedChanged);
            // 
            // FirstSettingForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(735, 295);
            this.ControlBox = false;
            this.Controls.Add(this.without_asio_cb);
            this.Controls.Add(this.use_bosc96ch_cb);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.asio_driver_list_cb);
            this.Controls.Add(this.assign_ch_cb);
            this.Controls.Add(this.assign_ch_gb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.max_inbuf_lbl);
            this.Controls.Add(this.max_outbuf_lbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.output_button_count_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.input_button_count_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.max_row_of_button_tb);
            this.Controls.Add(this.max_length_lbl);
            this.Controls.Add(this.message_lbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.asio_bufsize_tb);
            this.Controls.Add(this.data_length_lbl);
            this.Controls.Add(this.data_length_tb);
            this.Name = "FirstSettingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "First setting";
            this.assign_ch_gb.ResumeLayout(false);
            this.assign_ch_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void ok_button_Click(object sender, System.EventArgs e)
        {
            parent.in_btn_cnt = System.Convert.ToInt32(input_button_count_tb.Text);
            parent.out_btn_cnt = System.Convert.ToInt32(output_button_count_tb.Text);
            parent.max_row_of_button = System.Convert.ToInt32(max_row_of_button_tb.Text);
            parent.assign_ch = assign_ch_cb.Checked;

            parent.assign_in_ch = in_ch_tb.Text;
            parent.assign_out_ch = out_ch_tb.Text;
            parent.default_dleng = dleng;

            if (parent.asio.SetDataLength(dleng, parent.without_asio))
            {
                Dispose();
            }
            else
            {
                int ll = dleng / parent.asio.GetPreferredSize();
                int new_leng = ll * parent.asio.GetPreferredSize();
                data_length_tb.Text = new_leng.ToString();
            }
        }

        private void data_length_tb_TextChanged(object sender, EventArgs e)
        {
            //ok_button.Enabled = false;
            if (data_length_tb.Text != "")
            {
                dleng = System.Convert.ToInt32(data_length_tb.Text);
                if (!without_asio_cb.Checked)
                {
                    if (dleng > max_dleng_kword * 1000)
                    {
                        data_length_tb.Text = old_data_length_text;
                        dleng = System.Convert.ToInt32(data_length_tb.Text);
                    }
                    else
                    {
                        old_data_length_text = data_length_tb.Text;
                    }
                    if (dleng > asio_buf_size)
                    {
                        if (dleng % asio_buf_size == 0)
                        {
                            //ok_button.Enabled = true;
                            this.message_lbl.Text = "Stream mode : available";
                        }
                        else
                        {
                            this.message_lbl.Text = "Stream mode : not available";
                        }
                    }
                    else
                    {
                        this.message_lbl.Text = "Error: Data length < Buffer size";
                    }
                }
            }
        }


        private void asio_driver_list_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int in_ch = 2;
            int out_ch = 2;

            parent.selected_driver_name = asio_driver_list_cb.SelectedItem.ToString();
            parent.Cnsl_msg("Driver =" + parent.selected_driver_name + "\r\n");
            int result;
            unsafe
            {
                fixed (Char* ss = parent.selected_driver_name)
                {
                    result = parent.asio.infoget(ss);
                }
            }
            if (result < 0)
            {
                String error_msg = "";
                unsafe
                {
                    error_msg = new String(parent.asio.error_message());
                }
                System.Windows.Forms.MessageBox.Show("Error:" + error_msg);
                asio_driver_list_cb.SelectedIndex = 0;
                parent.selected_driver_name = "";
                return;
                //                    System.Environment.Exit(0);
            }
            else
            {
                parent.without_asio = false;
                without_asio_cb.Checked = false;
            }
            in_ch = parent.asio.GetAsioInputChannels();
            out_ch = parent.asio.GetAsioOutputChannels();

            input_button_count_tb.Text = in_ch.ToString();
            in_ch_tb.Text = "1-" + in_ch.ToString();

            output_button_count_tb.Text = out_ch.ToString();
            out_ch_tb.Text = "1-" + out_ch.ToString();

            int pp = parent.asio.GetByteSizePerData(in_ch, out_ch);
            max_dleng_kword = parent.pysical_memory_sizeKB / pp;
            max_length_lbl.Text = "< " + max_dleng_kword.ToString() + "K";
            max_inbuf_lbl.Text = "<= " + in_ch.ToString();
            max_outbuf_lbl.Text = "<= " + out_ch.ToString();

            if (parent.without_asio) asio_buf_size = 8192;
            else asio_buf_size = parent.asio.GetPreferredSize();

            int ii = 131072;
            if (parent.default_dleng > 0)
            {
                ii = parent.default_dleng;
            }
            int tt = ii / asio_buf_size;
            dleng = asio_buf_size * tt;
            if (dleng < ii) dleng += asio_buf_size;
            old_data_length_text = dleng.ToString();
            data_length_tb.Text = dleng.ToString();
            asio_bufsize_tb.Text = asio_buf_size.ToString();
            if (dleng % asio_buf_size == 0)
            {
                this.message_lbl.Text = "Stream mode : available";
            }
            else
            {
                this.message_lbl.Text = "Stream mode : not available";
            }
        }

        private void assign_ch_cb_CheckedChanged(object sender, EventArgs e)
        {
            assign_ch_gb.Enabled = assign_ch_cb.Checked;
        }

        private void use_bosc96ch_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (use_bosc96ch_cb.Checked)
            {
                assign_ch_cb.Checked = true;
                input_button_count_tb.Text = "96";
                output_button_count_tb.Text = "96";
                in_ch_tb.Text = "1-48, 65-112";
                out_ch_tb.Text = "1-48, 65-112";
            }
        }

        private void AddDriver()
        {
            int dev_num = parent.asio.driver_cnt;
            if (dev_num == 0)
                parent.Cnsl_msg("No asio device.\r\n");
            else
            {
                int drv_no = -1;
                unsafe
                {
                    for (int i = 0; i < dev_num; i++)
                    {
                        string driver = new String(parent.asio.DriversList[i]);
                        int result;
                        unsafe
                        {
                            fixed (Char* ss = driver)
                            {
                                result = parent.asio.infoget(ss);
                            }
                        }
                        if (result < 0)
                        {
                            String error_msg = "";
                            unsafe
                            {
                                error_msg = new String(parent.asio.error_message());
                            }
                            //console_msg_tb.AppendText("Error in getting ASIO driver info:" + error_msg + "\r\n");
                        }
                        else
                        {
                            parent.Cnsl_msg("Found ASIO Driver: " + driver + "\r\n");
                            asio_driver_list_cb.Items.Add(driver);
                            if (driver == parent.selected_driver_name)
                            {
                                drv_no = i;
                            }
                        }
                    }
                    if (drv_no < 0)
                    {
                        set_without_asio(true);
                    }
                    else
                    {
                        asio_driver_list_cb.SelectedIndex = drv_no;
                        set_without_asio(false);
                    }
                }
            }
        }

        private void set_without_asio(bool without_asio)
        {
            parent.without_asio = without_asio;
            without_asio_cb.Checked = without_asio;
            asio_driver_list_cb.Enabled = !without_asio;
        }

        private void without_asio_cb_CheckedChanged(object sender, EventArgs e)
        {
            set_without_asio(without_asio_cb.Checked);
        }
    }
}
