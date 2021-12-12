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
        Vector4 v = new Vector4(-1.1, 1.5, 10);
        Matrix4 proj;
        Pen pen0 = new Pen(Color.Blue, 2f);
        Pen pen1 = new Pen(Color.Blue, 2f);
        Vector2 center;

        ObjectBRep suzanne = new ObjectBRep();

        public Form1()
        {
            InitializeComponent();

            suzanne.model.LoadFromFile("Suzanne.obj", ModelFileType.Wavefront);

            proj = Matrix4.Parallel(v);
            suzanne.transform = Matrix4.RotX(0.0) * Matrix4.Scale(150);
         
            center = new Vector2(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {

            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.DrawObject3DBRepEdges(new Pen(Color.Red, 1f), suzanne, proj, center);
            //g.DrawObject3DBRepTriangles(new Pen(Color.Red, 1f), suzanne, proj, center, -v);

            if(checkBox1.Checked)
            {
                g.FillObjectBRepWithTriangles(suzanne, v * -1.0, proj, center);
                //g.FillObject3DBRepTrianglesWithDotsAndAllNormals(Color.Salmon, Color.Black, suzanne, proj, center, -v);
            }
            else
            {
                //g.FillObject3DBRepTrianglesWithDotsAndNormals(Color.Salmon, Color.Black, suzanne, proj, center, -v);

            }
            
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }

        private void sbAlpha_ValueChanged(object sender, EventArgs e)
        {
            double alpha = sbAlpha.Value / 1000.0;
            suzanne.transform = Matrix4.RotX(-alpha) * Matrix4.Scale(150);
            canvas.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
