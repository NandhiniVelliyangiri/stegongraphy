using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using Steganography;
using System.Data.SqlClient;

namespace ETC
{
    public partial class Encryption : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3TG6ONP\\SQLEXPRESS;Initial Catalog=cryptoo;Integrated Security=True;Pooling=False");
        SqlCommand cmd = new SqlCommand();
        private Bitmap bmp = null;
        private Bitmap bmp1 = null;
       public string Cipher_text="";
       public string key = "";

        public Encryption()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog open1 = new OpenFileDialog();
            open1.Filter = "JPG|*.JPG";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open1.FileName;
                pictureBox1.Image = Image.FromFile(textBox1.Text);
                button2.Enabled = true;
                FileInfo fi = new FileInfo(textBox1.Text);

                label9.Text = "File Name: " + fi.Name;
                label10.Text = "Image Resolution: " + pictureBox1.Image.PhysicalDimension.Height + " X " + pictureBox1.Image.PhysicalDimension.Width;

                double imageMB = ((fi.Length / 1024f) / 1024f);

                label11.Text = "Image Size: " + imageMB.ToString(".##") + "MB";
            }
            else
            {
                textBox1.Text = "";
                label9.Text = "File Name: ";
                label10.Text = "Image Resolution: ";
                label11.Text = "Image Size: ";

                pictureBox1.Image = Properties.Resources.blank;

            }
        }

        public static string Encrypt_text(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
      
           
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                Application.DoEvents();
                System.Threading.Thread.Sleep(1500);
                string fpath = Application.StartupPath + "\\Image\\encryptedText.txt";
                EncryptFile(pictureBox1.Image, fpath);
                string fpath1 = Application.StartupPath + "\\Image\\encrypted.jpg";
                EncryptFile(pictureBox1.Image, fpath1);
                string text = File.ReadAllText(textBox1.Text);
                bmp1 = new Bitmap(Bitmap.FromFile(Application.StartupPath + "\\input\\blank.jpg"));
                bmp = SteganographyHelper.embedText(text, bmp1);
                bmp.Save(Application.StartupPath + "\\Image\\EncryptedImage.jpg", ImageFormat.Jpeg); // Save the encrypted image   
                panelCanvas.Image = Image.FromFile(Application.StartupPath + "\\Image\\EncryptedImage.jpg");
                Application.DoEvents();
                System.Threading.Thread.Sleep(1500);
                Cipher_text = Encrypt_text(txt_plain_text.Text, key);

                con.Open();
                cmd = new SqlCommand("insert into encryption values('" + txt_plain_text.Text + "','" + key + "','" + Cipher_text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Input Image and Text Encrypted  Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void encryption()
        {
            var originalbmp = new Bitmap(Bitmap.FromFile(Application.StartupPath + "\\input\\blank.jpg")); //Actual image used to encrypt the message

            var encryptbmp = new Bitmap(originalbmp.Width, originalbmp.Height);
            var originalText = File.ReadAllText(textBox1.Text);
            var ascii = new List<int>(); // To store individual value of the pixels 
            foreach (char character in originalText)
            {
                int asciiValue = Convert.ToInt32(character); // Convert the character to ASCII
                var firstDigit = asciiValue / 1000; // Extract the first digit of the ASCII
                var secondDigit = (asciiValue - (firstDigit * 1000)) / 100; //Extract the second digit of the ASCII
                var thirdDigit = (asciiValue - ((firstDigit * 1000) + (secondDigit * 100))) / 10;//Extract the third digit of the ASCII
                var fourthDigit = (asciiValue - ((firstDigit * 1000) + (secondDigit * 100) + (thirdDigit * 10))); //Extract the third digit of the ASCII
                ascii.Add(firstDigit); // Add the first digit of the ASCII
                ascii.Add(secondDigit); // Add the second digit of the ASCII
                ascii.Add(thirdDigit); // Add the third digit of the ASCII
                ascii.Add(fourthDigit); // Add the fourth digit of the ASCII
            }
            var count = 0; // Have a count
            for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            {
                for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
                {
                    var color = originalbmp.GetPixel(row, column); // Get the pixel from each and every row and column
                    encryptbmp.SetPixel(row, column, Color.FromArgb(color.A - ((count < ascii.Count) ? ascii[count] : 0), color)); // Set ascii value in A of the pixel
                }
            }
            encryptbmp.Save(Application.StartupPath + "\\Image\\EncryptedImage.jpg", ImageFormat.Jpeg); // Save the encrypted image   
            panelCanvas.Image = Image.FromFile(Application.StartupPath + "\\Image\\EncryptedImage.jpg");
        }

        public string EncryptFile(Image img, string ImagePath_to_Save)
        {
            byte[] ImageBytes;
            ImageBytes = imageToByteArray(img);

            for (int i = 0; i < ImageBytes.Length; i++)
            {
                ImageBytes[i] = (byte)(ImageBytes[i] ^ 5);
            }
            File.WriteAllBytes(ImagePath_to_Save, ImageBytes);
            return ImagePath_to_Save;
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms,ImageFormat.Jpeg);
            return ms.ToArray();
        }


        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                string filePath = saveFileDialog1.FileName;
                string savePath = fileName;
                panelCanvas.Image.Save(Application.StartupPath + "\\Image\\EncryptedImage.jpg", ImageFormat.Jpeg);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            lblError.Text = "File Sending..";
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            lblError.Text = "Encrypted File Transferred..";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Hide();
        }

        private void Encryption_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        textBox3.Text= (UTF8Encoding.UTF8.GetByteCount(textBox2.Text) * 8).ToString();
            key = textBox2.Text;
        }
    }
}
