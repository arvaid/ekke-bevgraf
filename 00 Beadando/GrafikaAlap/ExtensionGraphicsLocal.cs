using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    static internal class ExtensionGraphicsLocal
    {
        public static void FillCircle(this Graphics g,
           Brush brush, float x, float y, float radius)
        {
            g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public static void FillCircle(this Graphics g,
           Brush brush, PointF p, float radius)
        {
            g.FillEllipse(brush, p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
        }

        public static void DrawCircle(this Graphics g,
           Pen pen, float x, float y, float radius)
        {
            g.DrawEllipse(pen, x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public static void DrawCircle(this Graphics g,
           Pen pen, PointF p, float radius)
        {
            g.DrawEllipse(pen, p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
        }
    }
}
