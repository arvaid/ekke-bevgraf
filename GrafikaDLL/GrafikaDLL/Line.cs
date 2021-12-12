using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using GrafikaDLL.Extensions;

namespace GrafikaDLL
{
    public class Line
    {
        public PointF p0, p1;

        public Line()
        {
            p0 = new PointF(0f, 0f);
            p1 = new PointF(0f, 0f);
        }

        public Line(float x0, float y0, float x1, float y1)
        {
            p0 = new PointF(x0, y0);
            p1 = new PointF(x1, y1);
        }

        public Line(PointF p0, PointF p1) 
            : this(p0.X, p0.Y, p1.X, p1.Y) 
        { }

        public int Sign(PointF p)
        {
            throw new NotImplementedException();
        }

        public bool IsParrallelTo(Line line)
        {
            float d0 = line.p0.X - line.p1.X;
            float d1 = line.p0.Y - line.p1.Y;
            return d0.CloseEnough(d1) || d0.CloseEnough(-d1);
        }

        public PointF? Intersection(Line line)
        {
            float d = (p0.X - p1.X) * (line.p0.Y - line.p1.Y) -
                (p0.Y - p1.Y) * (line.p0.X - line.p1.X);

            if (d == 0) return null;

            float t = ((p0.X - line.p0.X) * (line.p0.Y - line.p1.Y) -
                (p0.Y - line.p0.Y) * (line.p0.X - line.p1.X)) / d;
            float u = ((p0.X - line.p0.X) * (p0.Y - p1.Y) -
                (p0.Y - line.p0.Y) * (p0.X - p1.X)) / d;

            if (t > 0 && t < 1 && u > 0 && u < 1)
            {
                return new PointF(
                    p0.X + t * (line.p0.X - p0.X),
                    p0.Y + t * (line.p0.Y - p0.Y));
            }
            return null;
        }
    }
}
