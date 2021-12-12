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
        Matrix4 parallel;
        Matrix4 rotX, rotY, rotZ, rot;
        Vector2 center;

        Pen pen = new Pen(Color.Blue, 2f);

        public Form1()
        {
            InitializeComponent();

            parallel = Matrix4.Parallel(v);
            rotX = Matrix4.RotX(alpha);
            rotY = Matrix4.RotY(beta);
            rotZ = Matrix4.RotZ(gamma);
            rot = rotX * rotY * rotZ;
            center = new Vector2(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawParametricCurve3D(pen,
                (t) => r * Math.Cos(t),
                (t) => r * Math.Sin(t),
                (t) => t * m / (2 * Math.PI),
                rot,
                parallel,
                -4 * Math.PI, 4 * Math.PI,
                center);
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
