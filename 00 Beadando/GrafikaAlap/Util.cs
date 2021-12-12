using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    internal static class Util
    {
        public static float eps = 5f;

        public static bool isCloseEnough(PointF p0, PointF p1)
        {
            return Math.Abs(p0.X - p1.X) <= eps && Math.Abs(p0.Y - p1.Y) <= eps;
        }

        public static float Deg2Rad(float deg) => 
            deg / 180.0f * (float)Math.PI;
        
        public static float Rad2Deg(float rad) => 
            rad * 180.0f / (float)Math.PI;
    }
}
