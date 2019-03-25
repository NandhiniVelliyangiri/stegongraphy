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
    public partial class Mediator : Form
    {
        public Mediator()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            OpenFileDialog open1 = new OpenFileDialog();
            open1.InitialDirectory = Application.StartupPath + "\\Image\\EncryptedImage.jpg";
            open1.Filter = "JPG|*.JPG";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = open1.FileName;
                pictureBox1.Image = Image.FromFile(textBox2.Text);
            }
            FileInfo imginf = new FileInfo(textBox2.Text);
            float fs = (float)imginf.Length / 1024;
            label5.Text = smalldecimal(fs.ToString(), 2) + " KB";
        }























        private void button1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(textBox2.Text);
            pictureBox2.Image = resizeImage(img, new Size(100, 100));
           
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            MessageBox.Show("File Compressed Successfully");
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

        private Image resizeImage(Image imgToResize, Size size)
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.Save(Application.StartupPath+"\\Image\\CompressedImage.jpg", ImageFormat.Jpeg);
            FileInfo imginf = new FileInfo(Application.StartupPath + "\\Image\\CompressedImage.jpg");
            float fs = (float)imginf.Length / 1024;
            label2.Text = smalldecimal(fs.ToString(), 2) + " KB";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            lblError.Text = "File Sending..";
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            lblError.Text = "Image Forwarded Successfully..";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }

        private void Mediator_Load(object sender, EventArgs e)
        {

        }
    }
}
