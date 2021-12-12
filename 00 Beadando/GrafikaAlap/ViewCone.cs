using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    internal class ViewCone : IDrawable, IRotatable
    {
        // field of view
        public float FOV { get; set; } = (float)(Math.PI / 2f);

        public PointF Location { get; set; }

        public float X => Location.X;
        public float Y => Location.Y;

        public ViewCone(float X, float Y)
        {
            this.Location = new PointF(X, Y);
        }

        public ViewCone(PointF location)
        {
            this.Location = location;
        }

        public void Draw(Graphics g)
        {
            float width = g.ClipBounds.Width;
            float height = g.ClipBounds.Width;

            g.FillPolygon(Brushes.DarkGray, new PointF[]
            {
                Location,
                new PointF(
                    (float)(Location.X + 100 * width * Math.Cos(rotation + FOV / 2)),
                    (float)(Location.Y + 100 * height * Math.Sin(rotation + FOV / 2))
                ),
                new PointF(
                    (float)(Location.X + 100 *  width * Math.Cos(rotation - FOV / 2)),
                    (float)(Location.Y + 100 * height * Math.Sin(rotation - FOV / 2))
                ),
            });
        }

        private float rotation = 0f;
        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }
    }
}
