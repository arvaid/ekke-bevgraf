using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    internal struct Ray
    {
        const float MAX_LENGTH = 1000f;

        PointF p1, p2;

        public Ray(PointF p1, PointF p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Ray(PointF p1, float angle)
        {
            this.p1 = p1;
            this.p2 = new PointF(
                p1.X + MAX_LENGTH * (float)Math.Cos(angle), 
                p1.Y + MAX_LENGTH * (float)Math.Sin(angle));
        }

        public PointF? Cast(PointF p3, PointF p4)
        {
            float d = (p1.X - p2.X) * (p3.Y - p4.Y) -
                (p1.Y - p2.Y) * (p3.X - p4.X);

            if (d == 0) return null;

            float t = ((p1.X - p3.X) * (p3.Y - p4.Y) -
                (p1.Y - p3.Y) * (p3.X - p4.X)) / d;
            float u = ((p1.X - p3.X) * (p1.Y - p2.Y) -
                (p1.Y - p3.Y) * (p1.X - p2.X)) / d;

            if (t > 0 && t < 1 && u > 0 && u < 1)
            {
                return new PointF(
                    p1.X + t * (p2.X - p1.X),
                    p1.Y + t * (p2.Y - p1.Y));
            }
            return null;
        }
    }
}
