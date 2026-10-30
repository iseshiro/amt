namespace amt
{
    partial class BoscPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoscPlayer));
            this.button_set_movie = new System.Windows.Forms.Button();
            this.button_play_start = new System.Windows.Forms.Button();
            this.button_play_stop = new System.Windows.Forms.Button();
            this.textBox_console = new System.Windows.Forms.TextBox();
            this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // button_set_movie
            // 
            this.button_set_movie.Location = new System.Drawing.Point(14, 55);
            this.button_set_movie.Margin = new System.Windows.Forms.Padding(2);
            this.button_set_movie.Name = "button_set_movie";
            this.button_set_movie.Size = new System.Drawing.Size(76, 26);
            this.button_set_movie.TabIndex = 1;
            this.button_set_movie.Text = "Set Movie";
            this.button_set_movie.UseVisualStyleBackColor = true;
            this.button_set_movie.Click += new System.EventHandler(this.button_set_movie_Click);
            // 
            // button_play_start
            // 
            this.button_play_start.Enabled = false;
            this.button_play_start.Location = new System.Drawing.Point(14, 92);
            this.button_play_start.Margin = new System.Windows.Forms.Padding(2);
            this.button_play_start.Name = "button_play_start";
            this.button_play_start.Size = new System.Drawing.Size(76, 26);
            this.button_play_start.TabIndex = 2;
            this.button_play_start.Text = "Play Start";
            this.button_play_start.UseVisualStyleBackColor = true;
            this.button_play_start.Click += new System.EventHandler(this.button_play_start_Click);
            // 
            // button_play_stop
            // 
            this.button_play_stop.Enabled = false;
            this.button_play_stop.Location = new System.Drawing.Point(14, 131);
            this.button_play_stop.Margin = new System.Windows.Forms.Padding(2);
            this.button_play_stop.Name = "button_play_stop";
            this.button_play_stop.Size = new System.Drawing.Size(76, 26);
            this.button_play_stop.TabIndex = 3;
            this.button_play_stop.Text = "Play Stop";
            this.button_play_stop.UseVisualStyleBackColor = true;
            this.button_play_stop.Click += new System.EventHandler(this.button_play_stop_Click);
            // 
            // textBox_console
            // 
            this.textBox_console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_console.Location = new System.Drawing.Point(11, 372);
            this.textBox_console.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_console.Multiline = true;
            this.textBox_console.Name = "textBox_console";
            this.textBox_console.Size = new System.Drawing.Size(639, 77);
            this.textBox_console.TabIndex = 5;
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(104, 12);
            this.MediaPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(546, 353);
            this.MediaPlayer.TabIndex = 6;
            // 
            // BoscPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 460);
            this.Controls.Add(this.MediaPlayer);
            this.Controls.Add(this.textBox_console);
            this.Controls.Add(this.button_play_stop);
            this.Controls.Add(this.button_play_start);
            this.Controls.Add(this.button_set_movie);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BoscPlayer";
            this.Text = "Bosc Player";
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_set_movie;
        private System.Windows.Forms.Button button_play_start;
        private System.Windows.Forms.Button button_play_stop;
        private System.Windows.Forms.TextBox textBox_console;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
    }
}