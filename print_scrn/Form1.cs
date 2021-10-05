using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Collections.Specialized;
using Newtonsoft.Json;

using Keys = System.Windows.Forms.Keys;
namespace print_scrn
{

    
    [Flags]

    internal enum WindowStyles : int

    {

        ExToolWindow = 0x00000080,

        ExAppWindow = 0x00040000,

    }

     public partial class Form1: Form
    {
        int selectX;
        int selectY;
        int selectWidth;
        int selectHeight;
        public Pen selectPen;

        bool start = false;


        bool tset = true;

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }


        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd_HH-mm_ss-ffff");
        }



        private static async Task RunAsync(Image img)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            img.Save("test.png");
            var imageContent = new ByteArrayContent(File.ReadAllBytes(@"test.png"));
            

            form.Add(imageContent, "image", "image.png");
          
            using (var Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");
                Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate,sdch");
                Client.DefaultRequestHeaders.Add("Referer", "http://Something");
                Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/png,*/*;q=0.8");
                try { 

                var response = await Client.PostAsync(Properties.Settings.Default.webServer, form).ConfigureAwait(false);
                    var id = response.Headers.GetValues("pageid").FirstOrDefault();
                string responseBody = await response.Content.ReadAsStringAsync();


                    Form form3 = new Form3(id);
                    form3.ShowDialog();
                    
                }
catch (HttpRequestException e)
            {
                    MessageBox.Show("Cannot Connect to Server. ");
                    Console.WriteLine(e);
            }
        }
        }

         public void saveCache()
        {
            if (selectWidth > 0)
            {
                Rectangle rect = new Rectangle(selectX, selectY, selectWidth, selectHeight);
                Bitmap OrginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap _img = new Bitmap(selectWidth, selectHeight);
                Graphics g = Graphics.FromImage(_img);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(OrginalImage, 0, 0, rect, GraphicsUnit.Pixel);
                Image img = Image.FromHbitmap(_img.GetHbitmap());

               
                Console.WriteLine(Convert.ToBase64String(imageToByteArray(img)).Length);
                var b64 = Convert.ToBase64String(imageToByteArray(img));
                using (var client = new WebClient())
                {
                    var valueler = ChunksUpto(b64, 5);
                    var values = new NameValueCollection
                {
                {"image", Convert.ToBase64String(imageToByteArray(img))}
                };

   
                }



                string path = Properties.Settings.Default.cacheFolder;
                String timeStamp = GetTimestamp(DateTime.Now);
                if (Directory.Exists(path))
                {
                    img.Save(path + "/" + timeStamp + ".png");

                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    img.Save(path + "/" + timeStamp + ".png");

                }
            }
        }

        private void upload()
        {
            if (selectWidth > 0)
            {
                Rectangle rect = new Rectangle(selectX, selectY, selectWidth, selectHeight);
                Bitmap OrginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap _img = new Bitmap(selectWidth, selectHeight);
                Graphics g = Graphics.FromImage(_img);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(OrginalImage, 0, 0, rect, GraphicsUnit.Pixel);
                Image img = Image.FromHbitmap(_img.GetHbitmap());
                this.Close();
                Close();



              
                    RunAsync(img);
                
            }

            this.Close();
            Close();


        }


        private void SaveToClipboard()
        {
            if (selectWidth > 0)
            {
                Rectangle rect = new Rectangle(selectX, selectY, selectWidth, selectHeight);
                Bitmap OrginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap _img = new Bitmap(selectWidth, selectHeight);
                Graphics g = Graphics.FromImage(_img);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(OrginalImage, 0, 0, rect, GraphicsUnit.Pixel);
                Image img = Image.FromHbitmap(_img.GetHbitmap());
                Clipboard.SetImage(img);
                this.Close();
                Close();



                if (Properties.Settings.Default.web == true)
                {
                    RunAsync(img);
                }
            }

            this.Close();
            Close();


        }

        public  Form1() : base()
        {
            this.KeyPreview = true;
            InitializeComponent();
          

        }

         private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            this.Hide();
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphic = Graphics.FromImage(printscreen as Image);
            graphic.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            using (MemoryStream s = new MemoryStream())
            {
                printscreen.Save(s, ImageFormat.Bmp);
                pictureBox1.Size = new System.Drawing.Size(this.Width, this.Height);
                pictureBox1.Image = Image.FromStream(s);

            }
            tset = true;
            this.Show();
            Cursor = Cursors.Cross;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tset)
            {

                if (pictureBox1.Image == null)
                    return;
                if (start)
                {
                    pictureBox1.Refresh();
                    selectWidth = e.X - selectX;
                    selectHeight = e.Y - selectY;
                    pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);

                }
            }

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tset)
            {

                if (pictureBox1.Image == null)
                    return;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {

                
                    pictureBox1.Refresh();
                    selectWidth = e.X - selectX;
                    selectHeight = e.Y - selectY;
                    pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);

                
                }
                tset = false;
            }
           
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)

        {
         
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
                
            }
            if (e.KeyCode == System.Windows.Forms.Keys.D && e.Modifiers == System.Windows.Forms.Keys.Control)
            {
                start = false;
                upload();
            }

                if (e.KeyCode == System.Windows.Forms.Keys.C && e.Modifiers == System.Windows.Forms.Keys.Control)
            {
                start = false;
                if (Properties.Settings.Default.cache)
                {
                    saveCache();
                }
                SaveToClipboard();

            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tset)
            {
                if (!start)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        selectX = e.X;
                        selectY = e.Y;
                        selectPen = new Pen(Color.LightSlateGray, 1);
                        selectPen.DashStyle = DashStyle.DashDot;
                    }
                    pictureBox1.Refresh();
                    start = true;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
