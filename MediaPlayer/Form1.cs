using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form, IMessageFilter
    {
        private bool enableShortcut = false;
        public Form1()
        {
            InitializeComponent();
            //axWindowsMediaPlayer1.uiMode = "invisible";
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = ofd.FileName;
                enableShortcut = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        #region IMessageFilter

        private const UInt32 WM_KEYDOWN = 0x0100;

        public bool PreFilterMessage(ref Message m)

        {
            if (enableShortcut)
            {
                if (m.Msg == WM_KEYDOWN)
                {

                    Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;

                    /* Exit Fullscreen */
                    if (keyCode == Keys.Escape)
                    {

                        this.axWindowsMediaPlayer1.fullScreen = false;

                    }
                    /* Fullscreen */
                    else if (keyCode == Keys.F)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = true;

                    }
                    /* Pause and Play */
                    else if (keyCode == Keys.Space)
                    {
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.pause();
                        }
                        else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                    }
                    /*  */
                    return true;

                }
            }

            return false;

        }

        #endregion
    }
}
