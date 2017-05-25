using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChangeWallpaper
{
    public partial class Form1 : Form
    {
        private ContextMenu m_menu;  
        public Form1()
        {
            InitializeComponent();

            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0, new MenuItem("Show", new System.EventHandler(Show_Click)));
            m_menu.MenuItems.Add(1, new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            m_menu.MenuItems.Add(2, new MenuItem("Exit", new System.EventHandler(Exit_Click)));

            m_menu.MenuItems.Add(1, new MenuItem("Change Wallpaper", new System.EventHandler(ChangeWallpaper_Click)));

            notifyIcon1.ContextMenu = m_menu;

        }
        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
        }
        protected void ChangeWallpaper_Click(Object sender, System.EventArgs e)
        {
            ChangeWallaperRandom();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Minimized";
            notifyIcon1.BalloonTipText = "to show form double click on icon";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangeWallaperRandom();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt16(NUDTimer.Value) * 1000;
            timer1.Interval = sec;
            timer1.Enabled = true; 
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false; 
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            //textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeWallaperRandom();
        }
        public void ChangeWallaperRandom() { 
        pictureBox1.Visible = true;
            Random rnd = new Random();
            DirectoryInfo dr = new DirectoryInfo(textBox1.Text);
            FileInfo[] files = dr.GetFiles();
            int num = rnd.Next(0, files.Count() - 1);
            string file = files[num].Name;
            lblimg.Text = file;
            string FilePath = textBox1.Text + @"\" + file;
            Wallpaper.Set(FilePath, Wallpaper.Style.Stretched);

            pictureBox1.Visible = false;
        }
    }
}
