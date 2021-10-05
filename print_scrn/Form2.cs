using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace print_scrn
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (Properties.Settings.Default.cache == true)
            {
                button1.BackColor = System.Drawing.Color.Green;
            }else
            {
                button1.BackColor = System.Drawing.Color.Red;

            }
            if (Properties.Settings.Default.web == true)
            {
                button3.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                button3.BackColor = System.Drawing.Color.Red;

            }
            textBox1.Text = Properties.Settings.Default.cacheFolder;
            textBox2.Text = Properties.Settings.Default.webServer;
        }
        private void CBchanged(object sender, EventArgs e)
        {
          //  Properties.Settings.Default.cache = radioButton1.Checked;
            Properties.Settings.Default.Save();
        
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);
                    textBox1.Text = fbd.SelectedPath;
                    Properties.Settings.Default.cacheFolder = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                    
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (button1.BackColor == System.Drawing.Color.Green)
            {
                button1.BackColor = System.Drawing.Color.Red;
                Properties.Settings.Default.cache = false;
                Properties.Settings.Default.Save();

            }
            else if (button1.BackColor == System.Drawing.Color.Red)
            {
                button1.BackColor = System.Drawing.Color.Green;
                Properties.Settings.Default.cache = true;
                Properties.Settings.Default.Save();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == System.Drawing.Color.Green)
            {
                button3.BackColor = System.Drawing.Color.Red;
                Properties.Settings.Default.web = false;
                Properties.Settings.Default.Save();

            }
            else if (button3.BackColor == System.Drawing.Color.Red)
            {
                button3.BackColor = System.Drawing.Color.Green;
                Properties.Settings.Default.web = true;
                Properties.Settings.Default.Save();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.webServer = textBox2.Text;
            Properties.Settings.Default.Save();
        }
    }
}
