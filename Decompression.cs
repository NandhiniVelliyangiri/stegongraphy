using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ETC
{
    public partial class Decompression : Form
    {
        public Decompression()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(Application.StartupPath+"\\Image\\CompressedImage.jpg");
            panelCanvas.Image = resizeMax(img, new Size(1650, 1650));
           
        }

        private Image resizeMax(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;  
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Decryption dfrm = new Decryption();
            dfrm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.InitialDirectory = Application.StartupPath + "\\Image\\CompressedImage.jpg";
            open1.Filter = "JPG|*.JPG";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open1.FileName;
                pictureBox1.Image = Image.FromFile(textBox1.Text);
            }
            FileInfo imginf = new FileInfo(textBox1.Text);
            float fs = (float)imginf.Length / 1024;
            label5.Text = smalldecimal(fs.ToString(), 2) + " KB";
           
        }

        private string smalldecimal(string inp, int dec)
        {
            int i;
            for (i = inp.Length - 1; i > 0; i--)
                if (inp[i] == '.')
                    break;
            try
            {
                return inp.Substring(0, i + dec + 1);
            }
            catch
            {
                return inp;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCanvas.Image.Save(Application.StartupPath + "\\Image\\DecompressedImage.jpg", ImageFormat.Jpeg);
            FileInfo imginf = new FileInfo(Application.StartupPath + "\\Image\\DecompressedImage.jpg");
            float fs = (float)imginf.Length / 1024;
            label7.Text = smalldecimal(fs.ToString(), 2) + " KB";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Decompression_Load(object sender, EventArgs e)
        {

        }
    }
}
