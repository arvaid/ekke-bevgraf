using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public struct Vector2
    {
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector2 v) :this(v.x, v.y)
        { }

        public double x, y;

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator *(Vector2 v, double n)
        {
            return new Vector2(v.x * n, v.y * n);
        }

        public static Vector2 operator *(double n, Vector2 v)
        {
            return new Vector2(v.x * n, v.y * n);
        }

        public static double Dot(Vector2 v0, Vector2 v1)
        {
            return v0.x * v1.x + v0.y * v1.y;
        }

        public static explicit operator Vector2(PointF p)
        {
            return new Vector2(p.X, p.Y);
        }


        public override string ToString()
        {
            return string.Format("({0:0.00}; {1:0.00}", x, y);
        }
    }
}
