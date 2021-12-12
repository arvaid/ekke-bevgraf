using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF P = new PointF(100, 100);
        float size = 250;
        Brush brushSquare = new SolidBrush(Color.Salmon);
        float dx, dy;
        float speedX = 2, speedY = 2;
        int counter = 0;
        bool gotcha = false;

        Random rn = new Random();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.FillRectangle(brushSquare, P.X, P.Y, size, size);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (P.X <= e.X && e.X < P.X + size && P.Y <= e.Y && e.Y < P.Y + size)
            {
                gotcha = true;
                //dx = e.X - P.X;
                //dy = e.Y - P.Y;
                counter++;
                if (counter == 5)
                {
                    MessageBox.Show("Nyertél");
                    
                }
                dx = rn.Next(0, canvas.Width);
                dy = rn.Next(0, canvas.Height);
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (gotcha)
            {
                P.X = Clamp(e.X - dx, 0, canvas.Width - size);
                P.Y = Clamp(e.Y - dy, 0, canvas.Height - size);
                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (gotcha)
            {
                size -= 10;
                speedX = speedX < 0 ? speedX - 2 : speedX + 2;
                speedY = speedY < 0 ? speedY - 2 : speedY + 2;
                if (rn.NextDouble() >= 0.5)
                {
                    speedX *= -1;
                }
                brushSquare = new SolidBrush(Color.FromArgb(rn.Next(255), rn.Next(255), rn.Next(255)));
            }

            gotcha = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!gotcha)
            {
                P.X += speedX;
                P.Y += speedY;

                if (P.X < 0 || P.X > canvas.Width - size)
                {
                    speedX *= -1;
                }

                if (P.Y < 0 || P.Y > canvas.Height - size)
                {
                    speedY *= -1;
                }

                canvas.Invalidate();
            }
        }

        private float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

    }
}
