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

/*
 * TODO: A vektor hosszait beszorozni skalárral, amit scrollbar változtat.
 */

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        private Graphics g;

        Vector2 p0, p1, t0, t1;

        public Form1()
        {
            InitializeComponent();
            p0 = new Vector2(200, 300);
            t0 = new Vector2(450, 50);
            p1 = new Vector2(600, 200);
            t1 = new Vector2(650, 450);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawLine(Pens.Black, p0, t0);
            g.DrawLine(Pens.Black, p1, t1);
            g.DrawPoint(Pens.Black, Brushes.White, p0, 5);
            g.DrawPoint(Pens.Black, Brushes.White, p1, 5);
            g.DrawHermiteArc(new Pen(Color.Blue, 2f), p1, p0, t0 - p0, t1 - p1);
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
