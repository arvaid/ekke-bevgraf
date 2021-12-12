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
    // görbe rajzolás
    // transzformációk leprogramozása, vezérlés checkbox, stb.
    public partial class Form1 : Form
    {
        private Graphics g;

        double r = 50.0;
        double m = 20.0;
        double alpha = 0.0;
        double beta = 0.0;
        double gamma = 0.0;
        
        Vector4 v = new Vector4(1.1, 3.4, 1.2);
        Matrix4 scale;
        Matrix4 proj;
        Matrix4 rotX, rotY, rotZ, rot;
        Vector2 center;

        Pen pen = new Pen(Color.Blue, 2f);

        Vector2 pOrigo;
        Vector2 pXAxis;
        Vector2 pYAxis;
        Vector2 pZAxis;

        int grabbed = -1;

        public Form1()
        {
            InitializeComponent();

            rotX = Matrix4.RotX(alpha);
            rotY = Matrix4.RotY(beta);
            rotZ = Matrix4.RotZ(gamma);
            rot = rotX * rotY * rotZ;
            center = new Vector2(canvas.Width / 2, canvas.Height / 2);
            pOrigo = center;
            pXAxis = new Vector2(200, 300);
            pYAxis = new Vector2(700, 300);
            pZAxis = new Vector2(center.x, 100);

            scale = Matrix4.Scale(0.01);
            proj = Matrix4.Axonometric(pOrigo - pXAxis, pOrigo - pYAxis, pOrigo - pZAxis);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawLine(Pens.Black, (float)pOrigo.x, (float)pOrigo.y, (float)pXAxis.x, (float)pXAxis.y);
            g.DrawLine(Pens.Black, (float)pOrigo.x, (float)pOrigo.y, (float)pYAxis.x, (float)pYAxis.y);
            g.DrawLine(Pens.Black, (float)pOrigo.x, (float)pOrigo.y, (float)pZAxis.x, (float)pZAxis.y);
            g.DrawPoint(Pens.Black, Brushes.White, pOrigo, 4);
            g.DrawPoint(Pens.Black, Brushes.White, pXAxis, 3);
            g.DrawPoint(Pens.Black, Brushes.White, pYAxis, 3);
            g.DrawPoint(Pens.Black, Brushes.White, pZAxis, 3);

            g.DrawParametricCurve3D(pen,
                (t) => r * Math.Cos(t),
                (t) => r * Math.Sin(t),
                (t) => t * m / (2 * Math.PI),
                rot * scale,
                proj,
                -4 * Math.PI, 4 * Math.PI,
                center);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.CloseEnough(pOrigo))
            {
                grabbed = 0;
            }
            else if (e.CloseEnough(pXAxis))
            {
                grabbed = 1;
            }
            else if (e.CloseEnough(pYAxis))
            {
                grabbed = 2;
            }
            else if (e.CloseEnough(pZAxis)) 
            {
                grabbed = 3;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (grabbed) 
            {
                case -1:
                    return;
                case 0:
                    pOrigo = new Vector2(e.X, e.Y);
                    break;
                case 1:
                    pXAxis = new Vector2(e.X, e.Y);
                    break;
                case 2:
                    pYAxis = new Vector2(e.X, e.Y);
                    break;
                case 3:
                    pZAxis = new Vector2(e.X, e.Y);
                    break;
            }
            proj = Matrix4.Axonometric(pOrigo - pXAxis, pOrigo - pYAxis, pOrigo - pZAxis);
            canvas.Invalidate();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            alpha = e.NewValue * Math.PI / 180;
            rotX = Matrix4.RotX(alpha);
            rot = rotX * rotY * rotZ;
            canvas.Invalidate();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            beta = e.NewValue * Math.PI / 180;
            rotY = Matrix4.RotY(beta);
            rot = rotX * rotY * rotZ;
            canvas.Invalidate();
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            gamma = e.NewValue * Math.PI / 180;
            rotZ = Matrix4.RotZ(gamma);
            rot = rotX * rotY * rotZ;
            canvas.Invalidate();
        }
    }
}
