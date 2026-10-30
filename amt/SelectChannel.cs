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
    public partial class SelectChannel : Form
    {
        public int selected_ch = 1;
        public SelectChannel(int maxch)
        {
            InitializeComponent();
            numericUpDown_ch.Minimum = 1;
            numericUpDown_ch.Maximum = maxch;
            numericUpDown_ch.Value = 1;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            selected_ch = Decimal.ToInt32(numericUpDown_ch.Value);
        }
    }
}
