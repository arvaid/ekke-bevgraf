using GrafikaDLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    internal class Character : IDrawable, IRotatable
    {
        public const float RADIUS = 10f;

        public PointF Location { get; set; }

        public float X => Location.X;
        public float Y => Location.Y;

        public Character(float x, float y)
        {
            this.Location = new PointF(x, y);
        }

        public PointF Center
        {
            get => new PointF(X + RADIUS, Y + RADIUS);
            set => Location = new PointF(value.X - RADIUS / 2, value.Y - RADIUS / 2);
        } 

        public void Draw(Graphics g)
        {
            g.FillCircle(Brushes.Yellow, Center, Character.RADIUS);
            g.DrawCircle(Pens.Black, Center, Character.RADIUS);

            PointF arrowEndPoint = new PointF(
                (float)(Center.X + Character.RADIUS * Math.Cos(rotation)),
                (float)(Center.Y + Character.RADIUS * Math.Sin(rotation)));
            g.DrawLine(Pens.Black, Center, arrowEndPoint);
        }

        float rotation = 0f;
        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }
    }
}
