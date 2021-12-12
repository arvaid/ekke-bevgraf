﻿using System;
using System.Collections.Generic;
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
        PointF p1 = new PointF(120, 130);
        PointF p2 = new PointF(650, 250);
        int gotcha = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawPolygonDDA(Pens.Green, new PointF[] {
                new PointF(300, 250),
                new PointF(150, 200),
                new PointF(500, 70),
                new PointF(190, 300),
            }, true);

            g.DrawPolygonDDA(new Color[] {
                Color.Red,
                Color.Green,
                Color.Yellow,
                Color.Blue,
            }, new PointF[] {
                new PointF(450, 300),
                new PointF(120, 450),
                new PointF(70, 500),
                new PointF(360, 210),
            }, true);

            g.DrawLineDDA(Color.Blue, Color.Red, p1, p2);
            g.DrawLineDDA(Color.Blue, Color.Red, p1.X, p1.Y+20, p2.X, p2.Y + 20);

            g.DrawCircle(Color.Red, Color.Blue, p1, 30);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.CloseEnough(p1)) gotcha = 1;
            else if (e.CloseEnough(p2)) gotcha = 2;
            else gotcha = 0;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (gotcha != 0)
            {
                if (gotcha == 1) p1 = e.Location;
                else p2 = e.Location;

                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            gotcha = 0;
        }
    }
}
