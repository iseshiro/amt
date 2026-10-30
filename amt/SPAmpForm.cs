using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace amt
{
    public partial class SPAmpForm : Form
    {
        private amt parent = null;
        public SPAmpForm(amt Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            int volume_ch = 64;
            int volume_val = System.Convert.ToInt16(volume_tb.Text);
            parent.asio.spamp_gain_set(volume_ch, volume_val);
            volume_ch += 64;
            parent.asio.spamp_gain_set(volume_ch, volume_val);
        }
    }
}
