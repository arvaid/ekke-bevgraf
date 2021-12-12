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
        private Graphics g;
        double r = 100;
        double scale = 50;
        Vector2 o;

        public Form1()
        {
            InitializeComponent();
            o = new Vector2(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            
            g.DrawParametricCurve2D(Pens.Black, 
                t => r * Math.Cos(t) + o.x, 
                t => r *  Math.Sin(t) + o.y, 
                0, 
                2 * Math.PI);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.DrawLine(Pens.Black, 0, (float)o.y, canvas.Width, (float)o.y);
            g.DrawLine(Pens.Black, (float)o.x, 0, (float)o.x, canvas.Height);

            g.DrawParametricCurve2D(new Pen(Color.Blue, 2),
                t => scale * t + o.x,
                t => scale * -Math.Sin(t) + o.y,
                -2 * Math.PI,
                2 * Math.PI);

            g.DrawParametricCurve2D(Pens.Purple,
                t => scale * Math.Sin(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 12), 5)) + o.x,
                t => scale * -Math.Cos(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 12), 5)) + o.y,
                0,
                24 * Math.PI, 2000);

            // logaritmikus spirál

            g.DrawParametricCurve2D(new Pen(Color.Cyan, 2),
                t => Math.Exp(t) * Math.Cos(t) + o.x,
                t => Math.Exp(t) * Math.Sin(t) + o.y,
                0,
                2 * Math.PI);

            // Lissajous görbe
            g.DrawParametricCurve2D(new Pen(Color.Orange, 2),
                t => 2 * scale * Math.Cos(3 * t) + o.x,
                t => 2 * scale * Math.Sin(2 * t) + o.y,
                0,
                2 * Math.PI);

            // cardioid
            g.DrawParametricCurve2D(new Pen(Color.Red, 2),
                t => 2 * scale * (1 - Math.Cos(t)) * Math.Cos(t) + o.x,
                t => 2 * scale * (1 - Math.Cos(t)) * Math.Sin(t) + o.y,
                0,
                2 * Math.PI);
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
