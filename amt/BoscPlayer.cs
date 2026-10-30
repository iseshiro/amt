using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using WMPLib;

namespace amt
{
    public partial class BoscPlayer : Form
    {
        private Amt parent = null;
        private string mediaURL = null;
        public BoscPlayer(Amt Parent)
        {
            InitializeComponent();
            parent = Parent;
            MediaPlayer.settings.autoStart = false;
        }

        private void button_set_sound_Click(object sender, EventArgs e)
        {
            textBox_console.AppendText("test\n");
        }

        private void button_set_movie_Click(object sender, EventArgs e)
        {
            string ret = string.Empty;

            //OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "ファイル選択ダイアログ";
                openFileDialog.Filter = "ムービーファイル(*.mp4)|*.mp4|すべてのファイル(*.*)|*.*";
                openFileDialog.InitialDirectory = @"C:\Users\ise\Videos";

                //ファイル選択ダイアログを開く
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ret = openFileDialog.FileName;
                }
            }

            if (ret != string.Empty)
            {
                mediaURL = ret;
                textBox_console.AppendText(mediaURL);
                MediaPlayer.URL = mediaURL;
                button_play_start.Enabled = true;
            }
        }

        private void button_play_start_Click(object sender, EventArgs e)
        {
            if( !parent.without_asio )
            {
                if (parent.asio.get_stop() == true)
                {
                    parent.PlayControlStart();
                }
            }
            else
            {
                textBox_console.AppendText("No Asio Device\n");
            }
            if(MediaPlayer.URL.Length > 0)
            {
                MediaPlayer.Ctlcontrols.play();
                button_play_start.Enabled = false;
                button_play_stop.Enabled = false;
            }
        }

        private void button_play_stop_Click(object sender, EventArgs e)
        {
            parent.asio.set_stop(true);
            MediaPlayer.Ctlcontrols.stop();
        }


    }
}
