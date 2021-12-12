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
        double T;
        double a, b;
        int N;
        double h;

        double scale = 50;
        Vector2 o;

        public Form1()
        {
            InitializeComponent();
            a = 0;
            b = 24 * Math.PI;
            N = 2000;
            h = (b - a) / N;
            o = new Vector2(canvas.Width / 2, canvas.Height / 2);

            timer1.Start();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.Clear(Color.White);


            g.DrawParametricCurve2D(Pens.Purple,
                t => scale * Math.Sin(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 12), 5)) + o.x,
                t => scale * -Math.Cos(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 12), 5)) + o.y,
                0,
                T, N);

            g.FillEllipse(Brushes.Purple,
                (float)(scale * Math.Sin(T) * (Math.Exp(Math.Cos(T)) - 2 * Math.Cos(4 * T) - Math.Pow(Math.Sin(T / 12), 5)) + o.x),
                (float)(scale * -Math.Cos(T) * (Math.Exp(Math.Cos(T)) - 2 * Math.Cos(4 * T) - Math.Pow(Math.Sin(T / 12), 5)) + o.y),
                5, 5);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            T += h;
            if (T > b)
            {
                T = a;
            }

            canvas.Invalidate();
        }
    }
}
