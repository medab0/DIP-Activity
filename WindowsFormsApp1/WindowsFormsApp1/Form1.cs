﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;
using ImageProcess2;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed, subtractImage;
        Device [] device;
        Bitmap b;

        Bitmap imageB, imageA, colorgreen, resultImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = DeviceManager.GetAllDevices();
        }

        private void inversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color inv = Color.FromArgb(255-pixel.R, 255 - pixel.G, 255 - pixel.B);
                    processed.SetPixel(x, y, inv);
                }
            }
            pictureBox2.Image = processed;
        }

        private void pixelCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }

        private void grayscalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int ave;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    ave = (int) (pixel.R + pixel.G + pixel.B) / 3;
                    Color gray = Color.FromArgb(ave,ave,ave);
                    processed.SetPixel(x, y, gray);
                }
            }
            pictureBox2.Image = processed;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void mirrorHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel((int)x, (int)y);
                    processed.SetPixel((loaded.Width - 1) - x, y, pixel);
                }
            }

            pictureBox2.Image = processed;
        }

        private void mirrorVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel((int)x, (int)y);
                    processed.SetPixel(x, (loaded.Height - 1) - y, pixel);
                }
            }

            pictureBox2.Image = processed;
        }

        private void sephiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color original = loaded.GetPixel(x, y);

                    int red = (int)(original.R * 0.393 + original.G * 0.769 + original.B * 0.189);
                    int green = (int)(original.R * 0.349 + original.G * 0.686 + original.B * 0.168);
                    int blue = (int)(original.R * 0.272 + original.G * 0.534 + original.B * 0.131);

                    red = Math.Min(255, red);
                    green = Math.Min(255, green);
                    blue = Math.Min(255, blue);

                    Color sepia = Color.FromArgb(red, green, blue);

                    processed.SetPixel(x, y, sepia);
                }
            }

            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Histogram(ref loaded, ref processed);
            pictureBox2.Image = processed;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BasicDIP.Brightness(ref loaded, ref processed, trackBar1.Value);
            pictureBox2.Image = processed;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            BasicDIP.Equalisation(ref loaded, ref processed, trackBar2.Value/100);
            pictureBox2.Image = processed;
        }

        private void dIPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            BasicDIP.Rotate(ref loaded, ref processed, trackBar3.Value);
            pictureBox2.Image = processed;
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Scale(ref loaded, ref processed, 100, 100);
            pictureBox2.Image = processed;
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int ave;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    ave = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    if (ave < 180)
                    {
                        processed.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        processed.SetPixel(x, y, Color.White);
                    }
                    
                }
            }
            pictureBox2.Image = processed;
        }

        private void webcamOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            device[0].ShowWindow(pictureBox1);
        }

        private void webcamOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            device[0].Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageA = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = imageA;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                imageB = new Bitmap(openFileDialog2.FileName);
                pictureBox2.Image = imageB; // Display imageB in pictureBox2
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapFilter.Smooth(loaded, 1)) 
            {
                processed = (Bitmap)loaded.Clone();  
                pictureBox2.Image = processed;       
            }
            else
            {
                MessageBox.Show("Smoothing operation failed.");
            }
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapFilter.GaussianBlur(loaded, 4))
            {
                processed = (Bitmap)loaded.Clone();
                pictureBox2.Image = processed;
            }
            else
            {
                MessageBox.Show("Gaussian Blur operation failed.");
            }
        }

        private void meanRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapFilter.MeanRemoval(loaded, 9))
            {
                processed = (Bitmap)loaded.Clone();
                pictureBox2.Image = processed;
            }
            else
            {
                MessageBox.Show("Mean Removal operation failed.");
            }
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapFilter.Sharpen(loaded, 11))
            {
                processed = (Bitmap)loaded.Clone();
                pictureBox2.Image = processed;
            }
            else
            {
                MessageBox.Show("Sharpen operation failed.");
            }
        }

        private void embossLaplacianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapFilter.EmbossLaplacian(loaded))
            {
                processed = (Bitmap)loaded.Clone();
                pictureBox2.Image = processed;
            }
            else
            {
                MessageBox.Show("Emboss Laplacian operation failed.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                return;
            }

            Bitmap input = (Bitmap)loaded.Clone();
            Bitmap output = new Bitmap(input.Width, input.Height);

            for (int y = 0; y < output.Height; y++)
            {
                for (int x = 0; x < output.Width; x++)
                {
                    Color camColor = input.GetPixel(x, y);

                    byte max = Math.Max(Math.Max(camColor.R, camColor.G), camColor.B);
                    byte min = Math.Min(Math.Min(camColor.R, camColor.G), camColor.B);

                    bool replace =
                        camColor.G != min
                        && (camColor.G == max
                        || max - camColor.G < 8)
                        && (max - min) > 96;

                    if (replace)
                        camColor = imageB.GetPixel(x, y);

                    output.SetPixel(x, y, camColor);
                }
            }

            pictureBox3.Image = output;
            subtractImage = output;
        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Get one frame
            IDataObject data;
            Image bmap;
            device[0].Sendmessage();
            data = Clipboard.GetDataObject();
            bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));
            b = new Bitmap(bmap);

            BitmapFilter.GrayScale(b);
            pictureBox2.Image = b;
            /*
            processed = new Bitmap(b.Width, b.Height);
            Color pixel;
            int ave;
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    pixel = b.GetPixel(x, y);
                    ave = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    Color gray = Color.FromArgb(ave, ave, ave);
                    processed.SetPixel(x, y, gray);
                }
            }
            pictureBox2.Image = processed;
            */
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }


    }
}
