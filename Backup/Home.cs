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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "" && textBox1.Text != "")
            {
                if (comboBox1.SelectedItem.ToString() != "")
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Please select any one Name");
                    }
                    else
                    {
                        if (comboBox1.SelectedItem.ToString() == "Source" && textBox1.Text == "source")
                        {
                            Form1 frm2 = new Form1();
                            frm2.Show();
                            this.Hide();
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Mediator" && textBox1.Text == "mediator")
                        {
                            Mediator frm3 = new Mediator();
                            frm3.Show();
                            this.Hide();
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Destination" && textBox1.Text == "destination")
                        {
                            Decompression dcfrm = new Decompression();
                            dcfrm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("unAurthorized");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select any one Name at once");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
