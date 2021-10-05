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
    public partial class Form3 : Form
    {
        string lsa;
        public Form3(string sad)
        {
            InitializeComponent();
            lsa = sad;
            textBox1.Text = sad;
            Rectangle res = Screen.PrimaryScreen.Bounds;

            this.StartPosition = FormStartPosition.Manual;

            this.Location = new Point(res.Width - Size.Width, res.Height -Size.Height-90);
        }


    
    }
}
