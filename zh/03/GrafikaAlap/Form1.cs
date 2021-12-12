using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrafikaDLL;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        double r = 100;
        Vector2 center;

        public Form1()
        {
            InitializeComponent();


            center = new Vector2(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            double h = Math.PI / 100;
            for (double T = 0; T < 2 * Math.PI; T += h + h) {
                g.DrawParametricCurve2D(Pens.Black,
                    t => r * Math.Sin(2 * t) + center.x,
                    t => r * Math.Sin(3 * t) + center.y,
                    T, T + h);
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
