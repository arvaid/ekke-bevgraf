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
        double R = 100.0;

        double alpha = 0.0;
        double beta = 0.0;
        double gamma = 0.0;
        double lambda = 0.5;

        double transX = 0.0;
        double transY = 0.0;
        double transZ = 0.0;

        Vector4 v = new Vector4(1.1, 3.4, 1.2);
        Matrix4 parallel;
        Matrix4 rotX, rotY, rotZ, translate3D, transform;
        Matrix4 scale;
        Vector2 center;

        Pen pen = new Pen(Color.Blue, 2f);

        public Form1()
        {
            InitializeComponent();

            parallel = Matrix4.Parallel(v);
            rotX = Matrix4.RotX(alpha);
            rotY = Matrix4.RotX(beta);
            rotZ = Matrix4.RotX(gamma);
            scale = Matrix4.Scale(lambda);
            translate3D = Matrix4.Translate3D(transX, transY, transZ);
            SetTransform();
            center = new Vector2(canvas.Width / 2, canvas.Height / 2);

            comboBox1.SelectedIndex = 0;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    g.DrawParametricSurfaceWithDots(
                        pen,
                        (t, u) => (R + r * Math.Cos(t)) * Math.Cos(u),
                        (t, u) => (R + r * Math.Cos(t)) * Math.Sin(u),
                        (t, u) => r * Math.Sin(t),
                        transform,
                        parallel,
                        0, 2 * Math.PI,
                        0, 2 * Math.PI,
                        center
                    );
                    break;
                case 1:
                    g.DrawParametricSurfaceWithParameterLines(
                        pen,
                        (t, u) => (R + r * Math.Cos(t)) * Math.Cos(u),
                        (t, u) => (R + r * Math.Cos(t)) * Math.Sin(u),
                        (t, u) => r * Math.Sin(t),
                        transform,
                        parallel,
                        0, 2 * Math.PI,
                        0, 2 * Math.PI,
                        center
                    );
                    break;
                case 2:
                    g.DrawParametricSurfaceWithDotsAndNormals(
                        pen,
                        (t, u) => (R + r * Math.Cos(t)) * Math.Cos(u),
                        (t, u) => (R + r * Math.Cos(t)) * Math.Sin(u),
                        (t, u) => r * Math.Sin(t),
                        transform,
                        parallel,
                        0, 2 * Math.PI,
                        0, 2 * Math.PI,
                        center
                    );
                    break;
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

        private void SetTransform()
        {
            transform = translate3D * scale * rotX * rotY * rotZ;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            alpha = e.NewValue * Math.PI / 180;
            rotX = Matrix4.RotX(alpha);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            beta = e.NewValue * Math.PI / 180;
            rotY = Matrix4.RotY(beta);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            gamma = e.NewValue * Math.PI / 180;
            rotZ = Matrix4.RotZ(gamma);
            SetTransform();
            canvas.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            lambda = e.NewValue / 100.0;
            scale = Matrix4.Scale(lambda);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            transX = e.NewValue;
            translate3D = Matrix4.Translate3D(transX, transY, transZ);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            transY = e.NewValue;
            translate3D = Matrix4.Translate3D(transX, transY, transZ);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            transZ = e.NewValue;
            translate3D = Matrix4.Translate3D(transX, transY, transZ);
            SetTransform();
            canvas.Invalidate();
        }
    }
}
