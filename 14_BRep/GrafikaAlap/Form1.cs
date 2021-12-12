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

        double alpha = 180.0;
        double beta = 0.0;
        double gamma = 180.0;
        double lambda = 100;

        double transX = 0.0;
        double transY = 0.0;
        double transZ = 0.0;

        Vector4 v = new Vector4(1.1, 1.2, 10.1);
        Matrix4 parallel;
        Matrix4 rotX, rotY, rotZ, translate3D, transform;
        Matrix4 scale;
        Vector2 center;

        readonly List<ObjectBRep> objects = new List<ObjectBRep>();

        public Form1()
        {
            InitializeComponent();

            string[] filepaths =
            {
                @"teapot.obj",
                @"Bunny.obj",
                @"Cube.obj",
                @"Suzanne.obj",
                @"Tetra.obj",
                @"Torus.obj"
            };

            foreach (string path in filepaths)
            {
                var obj = new ObjectBRep();
                obj.color = Color.Salmon;
                obj.model.LoadFromFile(path, ModelFileType.Wavefront);
                objects.Add(obj);

                listBox1.Items.Add(path);
            }

            listBox1.SetSelected(0, true);

            parallel = Matrix4.Parallel(v);
            rotX = Matrix4.RotX(alpha);
            rotY = Matrix4.RotY(beta);
            rotZ = Matrix4.RotZ(gamma);
            scale = Matrix4.Scale(lambda);
            translate3D = Matrix4.Translate3D(transX, transY, transZ);
            SetTransform();
            center = new Vector2(canvas.Width / 2, canvas.Height / 2);

            comboBox1.SelectedIndex = 1;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            SetTransform();

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    g.DrawObjectBRepWithDots(objects[listBox1.SelectedIndex], parallel, center);
                    break;
                case 1:
                    g.DrawObjectBRepWithEdges(objects[listBox1.SelectedIndex], parallel, center);
                    break;
                case 2:
                    g.DrawObjectBRepWithTriangles(objects[listBox1.SelectedIndex], v * (-1), parallel, center);
                    break;
                case 3:
                    g.FillObjectBRepWithTriangles(objects[listBox1.SelectedIndex], v * (-1), parallel, center);
                    break;
                default:
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
            objects[listBox1.SelectedIndex].transform = transform;
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

        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            lambda = e.NewValue;
            scale = Matrix4.Scale(lambda);
            SetTransform();
            canvas.Invalidate();
        }

        private void hScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            v = new Vector4(e.NewValue / 100.0, v.y, v.z);
            parallel = Matrix4.Parallel(v);
            canvas.Invalidate();
        }

        private void hScrollBar9_Scroll(object sender, ScrollEventArgs e)
        {
            v = new Vector4(v.x, e.NewValue / 100.0, v.z);
            parallel = Matrix4.Parallel(v);
            canvas.Invalidate();
        }

        private void hScrollBar10_Scroll(object sender, ScrollEventArgs e)
        {
            v = new Vector4(v.x, v.y, e.NewValue / 100.0);
            parallel = Matrix4.Parallel(v);
            canvas.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
