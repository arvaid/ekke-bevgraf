using GrafikaDLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using GrafikaDLL.Extensions;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Graphics g3D;

        private readonly Character character;
        private readonly ViewCone viewCone;
        private readonly WallManager wallManager = new WallManager();
        private readonly List<Ray> rays = new List<Ray>();

        private readonly List<float> rayDistances = new List<float>();

        PointF mouseCursor = new PointF();

        int? grabbed = null;
        bool characterGrabbed = false;
        float angle = (float)(Math.PI / 4);
        const float dAngle = 0.05f;

        public Form1()
        {
            InitializeComponent();

            character = new Character(canvas.Width / 2, canvas.Height / 2);
            viewCone = new ViewCone(character.Center);

            character.SetRotation(angle);
            viewCone.SetRotation(angle);

            this.MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            angle += e.Delta > 0 ? dAngle : -dAngle; 
            character.SetRotation(angle);
            viewCone.SetRotation(angle);
            canvas.Invalidate();
        }

        public static float Distance(PointF p0, PointF p1)
        {
            float diffX = p0.X - p1.X;
            float diffY = p0.Y - p1.Y;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.Clear(Color.LightGray);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (useFOV.Checked)
            {
                viewCone.Draw(g);
            }
            wallManager.Draw(g);

            createRays();
            rayDistances.Clear();
            foreach (var ray in rays)
            {
                PointF? closest = null;
                float min = float.PositiveInfinity;
                foreach (var wall in wallManager.Walls)
                {
                    PointF? pt = ray.Cast(wall.Item1, wall.Item2);
                    if (pt.HasValue)
                    {
                        float dist = Distance(character.Center, pt.Value);
                        if (dist < min)
                        {
                            closest = pt.Value;
                            min = dist;
                        }
                    }
                }
                if (closest.HasValue)
                {
                    g.DrawLine(Pens.Blue, character.Center, closest.Value);
                    
                }
                rayDistances.Add(min);
            }

            character.Draw(g);
            canvas3D.Invalidate();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                for (int i = 0; i < wallManager.Count; i++)
                {
                    if (e.CloseEnough(wallManager[i]))
                    {
                        grabbed = i;
                    }
                }

                if (!grabbed.HasValue)
                {
                    wallManager.Add(e.Location, e.Location);
                    grabbed = wallManager.Count - 1;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                character.Center = e.Location;
                viewCone.Location = character.Center;
                characterGrabbed = true;
            }

            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCursor = new PointF(((float)e.X).Clamp(0, canvas.Width), 
                ((float)e.Y).Clamp(0, canvas.Height));

            if (grabbed.HasValue)
            {
                wallManager[grabbed.Value] = mouseCursor;
            }
            else if (characterGrabbed)
            {
                character.Center = mouseCursor;
                viewCone.Location = mouseCursor;
            }
            canvas.Invalidate();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = null;
            characterGrabbed = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wallManager.Clear();
            canvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wallManager.RemoveLast();
            canvas.Invalidate();
        }

        private void createRays()
        {
            float fov = viewCone.FOV;
            int start = useFOV.Checked ? (int)Util.Rad2Deg(angle - fov / 2) : 0;
            int end = useFOV.Checked ? (int)Util.Rad2Deg(angle + fov / 2) : 360;
            rays.Clear();
            for (int i = start; i < end; i++)
            {
                rays.Add(new Ray(character.Center, Util.Deg2Rad(i)));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        private void canvas3D_Paint(object sender, PaintEventArgs e)
        {
            g3D = e.Graphics;

            if (rayDistances.Count == 0) return;

            float w = canvas3D.Width / rayDistances.Count;
            for (int i = 0; i < rayDistances.Count; i++)
            {
                if (!float.IsInfinity(rayDistances[i]))
                {
                    int b = (int)((float)rayDistances[i]).Remap(0, canvas.Width, 0, 255);
                    var h = ((float)rayDistances[i]).Remap(0, canvas3D.Width, canvas3D.Height, 0);

                    var color = Color.FromArgb(b, b, b);
                    var brush = new SolidBrush(color);
                    g3D.FillRectangle(brush, i * w + 30, canvas3D.Height / 20, w, h);
                }
            }
        }
    }
}