using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj2
{
    public partial class Form1 : Form
    {
        PictureBox[] load = new PictureBox[20];
        int count = 0;
        int index = 0;
        float zoomScale = 1.0f;
        const float ZOOM_SPEED = 0.1f;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in openFileDialog1.FileNames)
                    count++;
                if (count > 20)
                    MessageBox.Show("حداکثر 20 تصویر میتوانید آپلود کنید");
                for (int i = 0; i < count; i++)
                {
                    load[i] = new PictureBox();
                    load[i].BorderStyle = BorderStyle.FixedSingle;
                    load[i].SizeMode = PictureBoxSizeMode.StretchImage;


                    this.Controls.Add(load[i]);
                    load[i].Image = Image.FromFile(openFileDialog1.FileNames[i]);
                    load[i].Location = pictureBox1.Location;
                    pictureBox1.Image = load[0].Image;
                    button4.Enabled = false;
                    button5.Enabled = false;


                }
            }

            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index <count-1)
            {
                index++;
                ShowImage();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (index> 0)
            {
                index--;
                ShowImage();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            index = 0;
            ShowImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            index = count-1;
            ShowImage();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                index = 0;
                pictureBox1.Image = load[index].Image;

                timer1.Interval = 3000;
                timer1.Start();
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                timer1.Stop();
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;

                if (index > 0)
                {
                    button4.Enabled = true;
                    button5.Enabled = true;
                }
                else
                {
                    button4.Enabled = false;
                    button5.Enabled = false;
                }

                if (index < count-1)
                {
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == 0) return;

            index++;
            if (index >= count)
            {
                index = 0;
                timer1.Stop();
                checkBox1.Checked = false;
            }
            pictureBox1.Image = load[index].Image;
        }
        private void ShowImage()
        {
            if (count == 0) return;

            pictureBox1.Image = load[index].Image;

            if (index > 0)
                button4.Enabled = true;
            else
                button4.Enabled = false;

            if (index > 0)
                button5.Enabled = true;
            else
                button5.Enabled = false;

            if (index < count-1)
                button2.Enabled = true;
            else
                button2.Enabled = false;

            if (index < count-1)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void ApplyZoom()
        {
            if (pictureBox1.Image == null) return;

            pictureBox1.Width = (int)(pictureBox1.Image.Width * zoomScale);
            pictureBox1.Height = (int)(pictureBox1.Image.Height * zoomScale);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                zoomScale += ZOOM_SPEED;
                ApplyZoom();
            }
            else if (e.Button == MouseButtons.Right)
            {
                zoomScale = Math.Max(0.5f, zoomScale - ZOOM_SPEED);
                ApplyZoom();
            }
        }
    }
}
