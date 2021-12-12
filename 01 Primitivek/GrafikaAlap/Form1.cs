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
        private Graphics g;
        Point center;
        Pen penCoordSys = new Pen(Brushes.Black, 3);

        public Form1()
        {
            InitializeComponent();
            center = new Point(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawLine(penCoordSys, 0, center.Y, canvas.Width, center.Y);
            g.DrawLine(penCoordSys, center.X, 0, center.X, canvas.Height);

            g.FillRectangle(Brushes.Yellow, new Rectangle(100, 100, 150, 350));
            g.DrawRectangle(Pens.Black, new Rectangle(100, 100, 150, 350));

            g.FillEllipse(new SolidBrush(Color.HotPink), new Rectangle(100, 100, 150, 350));
            g.DrawEllipse(new Pen(Color.Green, 5), new Rectangle(100, 100, 150, 350));

            PointF P = new PointF(400, 100);
            float r = 50;
            g.DrawEllipse(Pens.Red, P.X - r, P.Y - r, 2 * r, 2 * r);

            //g.FillRectangle(Brushes.Black, P.X, P.Y, 1, 1);
            g.DrawRectangle(Pens.Black, P.X, P.Y, 0.5f, 0.5f);

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(j, i, 0)), P.X+i, P.Y+j, 0.5f, 0.5f);
                }
            }

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
