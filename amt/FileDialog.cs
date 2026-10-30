using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace amt
{
    public class FileDialog1
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public string filename = "";

        public FileDialog1()
        {
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        }

        public bool OpenFileDialog(string title, string ext)
        {
            bool result = false;
            openFileDialog1.Filter = "Files (" + ext + ")|" + ext + "|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = title;

            if (STAShowDialog(openFileDialog1) == DialogResult.OK)
//                if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                result = true;
            }
            return result;
        }

        public bool SaveFileDialog(string title, string ext)
        {
            bool result = false;
            saveFileDialog1.Title = title;
            saveFileDialog1.Filter = ext + " files (*." + ext + ")|*." + ext + "|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            if (STAShowDialog(saveFileDialog1) == DialogResult.OK)
 //               if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
                result = true;
            }
            return result;
        }

        private DialogResult STAShowDialog(FileDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }

        public class DialogState
        {
            public DialogResult result;
            public FileDialog dialog;
            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }
        }
    }
}
