using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    internal class WallManager : IDrawable
    {
        readonly List<PointF> points = new List<PointF>();

        public void Draw(Graphics g)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i % 2 == 0 && i < points.Count - 1)
                {
                    g.DrawLine(new Pen(Color.Black, 2f), points[i], points[i + 1]);
                }

                g.FillCircle(Brushes.White, points[i], 3);
                g.DrawCircle(Pens.Black, points[i], 3);
            }
        }

        public IList<Tuple<PointF, PointF>> Walls
        {
            get
            {
                List<Tuple<PointF, PointF>> result = new List<Tuple<PointF, PointF>>();
                for (int i = 0; i < points.Count - 1; i += 2)
                {
                    result.Add(new Tuple<PointF, PointF>(points[i], points[i + 1]));
                }
                return result;
            }
        }

        public PointF this[int index]
        { 
            get { return points[index]; } 
            set { points[index] = value; }
        }

        public int Count => points.Count;

        public void Add(PointF p0, PointF p1)
        {
            points.Add(p0);
            points.Add(p1);
        }

        public void RemoveLast()
        {
            if (points.Count > 1)
            {
                points.RemoveAt(points.Count - 1);
                points.RemoveAt(points.Count - 1);
            }
        }

        public void Clear()
        {
            points.Clear();
        }
    }
}
