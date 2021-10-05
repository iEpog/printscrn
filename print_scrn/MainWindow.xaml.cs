using print_scrn.Keys;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace print_scrn
{



    // KEY HOOK GITHUB
    /*
     * 
 .d8888b.  8888888 88888888888 888    888 888     888 888888b.   
d88P  Y88b   888       888     888    888 888     888 888  "88b  
888    888   888       888     888    888 888     888 888  .88P  
888          888       888     8888888888 888     888 8888888K.  
888  88888   888       888     888    888 888     888 888  "Y88b 
888    888   888       888     888    888 888     888 888    888 
Y88b  d88P   888       888     888    888 Y88b. .d88P 888   d88P 
 "Y8888P88 8888888     888     888    888  "Y88888P"  8888888P" 
 
  https://github.com/AngryCarrot789/KeyDownTester                                                              
  https://github.com/AngryCarrot789/KeyDownTester
  https://github.com/AngryCarrot789/KeyDownTester
  https://github.com/AngryCarrot789/KeyDownTester
                                                                 
                                                                 


    */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        bool settingsActive = false;
        bool isActive = false;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        string path = Properties.Settings.Default.cacheFolder;


        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MouseEventArgs mouseEventArgs = (System.Windows.Forms.MouseEventArgs)e;
           if( mouseEventArgs.Button ==MouseButtons.Left){ if (!isActive) { isActive = true; Form1 frm = new Form1(); frm.Show(); frm.FormClosing += new FormClosingEventHandler(frm_FormClosed); } }
        }

        private void settings_Closed(object sender, FormClosingEventArgs e)
        {
            settingsActive = false;
        }
        private void frm_FormClosed(object sender, FormClosingEventArgs e)
        {
            isActive = false;
        }


        ContextMenuStrip contextMenu1 = new ContextMenuStrip();


        void contextMenu1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (e.ClickedItem.Text == "Settings")
            {
                if (!settingsActive) { settingsActive = true; Form2 frm = new Form2(); frm.Show(); frm.FormClosing += new FormClosingEventHandler(settings_Closed); }
            }

            if (e.ClickedItem.Text == "Cache")
            {
                string path = Properties.Settings.Default.cacheFolder;
                if (Directory.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);

                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    System.Diagnostics.Process.Start(path);

                }
            }
            if (e.ClickedItem.Text == "Exit")
            {
                this.Close();
            }
        }
    
        public MainWindow()
        {
            //Cache

            cacheCheck();

        //Cache


        //Context And Trayy
        contextMenu1.Items.Add("Epog");
            contextMenu1.Items.Add(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            contextMenu1.Items.Add("");

            contextMenu1.Items.Add("Cache");
            contextMenu1.Items.Add("Settings");
            contextMenu1.Items.Add("");
            contextMenu1.Items.Add("Exit");
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Text = "Print Screen";
            notifyIcon.Visible = true;
            notifyIcon.Icon = print_scrn.Properties.Resources.prntsc;
            contextMenu1.Opening  +=new CancelEventHandler(contextMenu1_Opening);
            contextMenu1.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu1_ItemClicked);
            notifyIcon.ContextMenuStrip = contextMenu1;
            notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            //Context And Trayy

            InitializeComponent();
            //Keyboard Hook
            HotkeysManager.SetupSystemHook();
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.None, Key.PrintScreen, () => {

                if (!isActive) { isActive = true; Form1 frm = new Form1(); frm.Show(); frm.FormClosing += new FormClosingEventHandler(frm_FormClosed); }
            }));
            //Keyboard Hook


            Closing += MainWindow_Closing;
        }
        public void cacheCheck()
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));


            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        private void contextMenu1_Opening(object sender, CancelEventArgs e)
    {
        contextMenu1.Items[0].Enabled = false;
        contextMenu1.Items[1].Enabled = false;
            contextMenu1.Items[2].Enabled = false;

            contextMenu1.Items[5].Enabled = false;

    }

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotkeysManager.ShutdownSystemHook();
        }
    }
}
