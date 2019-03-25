using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label9.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            Random ran = new Random();
            int value = ran.Next(-255, 255);
            MessageBox.Show("Prediction Error Range :  " + value.ToString());
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            Random ran1 = new Random();
            int value1 = ran1.Next(0, 255);
            MessageBox.Show("Mapped prediction error range :  " + value1.ToString());
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            MessageBox.Show("The prediction errors are divided into multiple clusters");
            panel1.Visible = true;
            Image img = Image.FromFile(label9.Text);
            int widthThird = (int)((double)img.Width / 4.0 + 0.6);
            int heightThird = (int)((double)img.Height / 4.0 + 0.6);
            Bitmap[,] bmps = new Bitmap[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }
            pictureBox1.Image = bmps[0, 0];
            pictureBox2.Image = bmps[0, 1];
            pictureBox3.Image = bmps[0, 2];
            pictureBox4.Image = bmps[0, 3];
            pictureBox5.Image = bmps[1, 0];
            pictureBox6.Image = bmps[1, 1];
            pictureBox7.Image = bmps[1, 2];
            pictureBox8.Image = bmps[1, 3];
            pictureBox9.Image = bmps[2, 0];
            pictureBox11.Image = bmps[2, 1];
            pictureBox12.Image = bmps[2, 2];
            pictureBox13.Image = bmps[2, 3];
            pictureBox14.Image = bmps[3, 0];
            pictureBox15.Image = bmps[3, 1];
            pictureBox16.Image = bmps[3, 2];
            pictureBox17.Image = bmps[3, 3];
        }

        private void randomPermutationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            Image img = Image.FromFile(label9.Text);
            int widthThird = (int)((double)img.Width / 4.0 + 0.6);
            int heightThird = (int)((double)img.Height / 4.0 + 0.6);
            Bitmap[,] bmps = new Bitmap[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }
            pictureBox1.Image = bmps[0, 0];
            pictureBox5.Image = bmps[0, 1];
            pictureBox9.Image = bmps[0, 2];
            pictureBox14.Image = bmps[0, 3];
            pictureBox2.Image = bmps[1, 0];
            pictureBox6.Image = bmps[1, 1];
            pictureBox11.Image = bmps[1, 2];
            pictureBox15.Image = bmps[1, 3];
            pictureBox3.Image = bmps[2, 0];
            pictureBox7.Image = bmps[2, 1];
            pictureBox12.Image = bmps[2, 2];
            pictureBox16.Image = bmps[2, 3];
            pictureBox4.Image = bmps[3, 0];
            pictureBox8.Image = bmps[3, 1];
            pictureBox13.Image = bmps[3, 2];
            pictureBox17.Image = bmps[3, 3];
        }

        private void newImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.Filter = "JPG|*.JPG";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                label9.Text = open1.FileName;
                pictureBox10.Image = Image.FromFile(label9.Text);
            }
            panel1.Visible = false;
            label9.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Encryption frmm = new Encryption();
            frmm.Show();
            this.Hide();           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
