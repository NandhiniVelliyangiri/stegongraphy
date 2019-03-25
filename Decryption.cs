using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace ETC
{
    public partial class Decryption : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3TG6ONP\\SQLEXPRESS;Initial Catalog=cryptoo;Integrated Security=True;Pooling=False");

        //SqlConnection con = new SqlConnection("Data Source=cryptoo.sdf");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public string Cipher_text = "";
        public string key = "";
        public string cipher_text = "";
        public Decryption()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.InitialDirectory = Application.StartupPath + "\\Image\\DecompressedImage.jpg";
            open1.Filter = "JPG|*.JPG";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open1.FileName;
                pictureBox1.Image = Image.FromFile(textBox1.Text);
            }
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }  

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\Image\\encrypted.jpg";
                panelCanvas.Image = Image.FromFile(DecryptFile(path));
                Application.DoEvents();
                System.Threading.Thread.Sleep(1500);

                con.Open();
                cmd = new SqlCommand("select * from encryption where key1='" + textBox2.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cipher_text = dr.GetValue(2).ToString();
                    key = dr.GetValue(1).ToString();
                }
                con.Close();
                txt_plain_text.Text = Decrypt(cipher_text, key);
                MessageBox.Show("Original File Received Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Decryption "+ex.Message.ToString());
            }
        }
        public string DecryptFile(string ImagePath_to_Save)
        {
            byte[] ImageBytes;

            ImageBytes = File.ReadAllBytes(ImagePath_to_Save);

            for (int i = 0; i < ImageBytes.Length; i++)
            {
                ImageBytes[i] = (byte)(ImageBytes[i] ^ 5);
            }

            File.WriteAllBytes(ImagePath_to_Save, ImageBytes);
            return ImagePath_to_Save;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Decompression decom = new Decompression();
            decom.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }
    }
}
