using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace amt
{
    public partial class EscKeyForm : Form
    {
        private Amt parent;
        public EscKeyForm(Amt Parent, string Script_name)
        {
            InitializeComponent();
            parent = Parent;
            label1.Text = "Script '" + Script_name + "' is now executing.";
        }

        private void script_break_btn_Click(object sender, EventArgs e)
        {
            parent.script_continue = false;
            Dispose();
        }
    }
}
