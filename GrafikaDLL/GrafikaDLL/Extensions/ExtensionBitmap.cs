using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GrafikaDLL
{
    public static class ExtensionBitmap
    {
        public static void SetLine(this Bitmap bmp,
            float x1, float y1, float x2, float y2, Color color)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float incX = dx / length;
            float incY = dy / length;
            float x = x1, y = y1;
            for (int i = 0; i <= length; i++)
            {
                bmp.SetPixel((int)x, (int)y, color);
                x += incX;
                y += incY;
            }
        }

        // FIXME: fix "striped" fill
        public static void FillEdgeFlag(this Bitmap bmp,
            Color background, Color fillColor)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                bool inside = false;
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (!bmp.GetPixel(x, y).IsTheSameAs(background))
                    {
                        inside = !inside;
                        continue;
                    }
                    if (inside)
                    {
                        bmp.SetPixel(x, y, fillColor);
                    }
                }
            }
        }

        public static void FillRec4(this Bitmap bmp,
            Color background, Color fillColor, int x, int y)
        {
            if (bmp.GetPixel(x, y).IsTheSameAs(background))
            {
                bmp.SetPixel(x, y, fillColor);
                bmp.FillRec4(background, fillColor, x + 1, y);
                bmp.FillRec4(background, fillColor, x - 1, y);
                bmp.FillRec4(background, fillColor, x, y + 1);
                bmp.FillRec4(background, fillColor, x, y - 1);
            }
        }
        public static void FillRec8(this Bitmap bmp,
            Color background, Color fillColor, int x, int y)
        {
            if (x < 0 || x > bmp.Width ||
                y < 0 || y > bmp.Height) { return; }

            if (bmp.GetPixel(x, y).IsTheSameAs(background))
            {
                bmp.SetPixel(x, y, fillColor);
                bmp.FillRec8(background, fillColor, x + 1, y);
                bmp.FillRec8(background, fillColor, x - 1, y);
                bmp.FillRec8(background, fillColor, x, y + 1);
                bmp.FillRec8(background, fillColor, x, y - 1);
                bmp.FillRec8(background, fillColor, x + 1, y + 1);
                bmp.FillRec8(background, fillColor, x - 1, y - 1);
                bmp.FillRec8(background, fillColor, x - 1, y + 1);
                bmp.FillRec8(background, fillColor, x + 1, y - 1);
            }
        }

        public static void FillStack4(this Bitmap bmp,
            Color background, Color fillColor, int x, int y)
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };

            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            Point p;
            while (stack.Count > 0) {
                p = stack.Pop();
                bmp.SetPixel(p.X, p.Y, fillColor);
                for (int i = 0; i < 4; i++)
                {
                    int x0 = p.X + dx[i];
                    int y0 = p.Y + dy[i];

                    if (0 < x0 && x0 < bmp.Width &&
                        0 < y0 && y0 < bmp.Height) {
                        if (bmp.GetPixel(x0, y0).IsTheSameAs(background))
                        {
                            stack.Push(new Point(p.X + dx[i], p.Y + dy[i]));
                        }
                    }
                }
            }
        }

        // BUG: kifut az alakzatból átlós vonalaknál
        // BUG: kifut a canvasról
        public static void FillStack8(this Bitmap bmp,
            Color background, Color fillColor, int x, int y)
        {
            int[] dx = { 0, 1, 0, -1, 1, 1, -1, -1 };
            int[] dy = { 1, 0, -1, 0, 1, -1, 1, -1 };

            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            Point p;
            while (stack.Count > 0)
            {
                p = stack.Pop();
                bmp.SetPixel(p.X, p.Y, fillColor);
                for (int i = 0; i < 8; i++)
                {
                    if (bmp.GetPixel(p.X + dx[i], p.Y + dy[i]).IsTheSameAs(background))
                    {
                        stack.Push(new Point(p.X + dx[i], p.Y + dy[i]));
                    }
                }
            }

            //int[] dx = new int[] { 0, 1, 0, -1, 1, 1, -1, -1 };
            //int[] dy = new int[] { 1, 0, -1, 0, 1, -1, 1, -1 };
            //Stack<Point> stack = new Stack<Point>();
            //stack.Push(p);
            //Point p0, p1;
            //while (stack.Count > 0)
            //{
            //    p0 = stack.Pop();
            //    bmp.SetPixel(p0.X, p0.Y, fillColor);
            //    for (int i = 0; i < 4; i++)
            //    {
            //        p1 = new Point(p0.X + dx[i], p0.Y + dy[i]);
            //        if (p1.X < 0 || p1.Y < 0)
            //            continue;
            //        if (bmp.GetPixel(p1.X, p1.Y).IsTheSameAs(background))
            //            stack.Push(p1);
            //    }
            //    for (int i = 4; i < 8; i++)
            //    {
            //        p1 = new Point(p0.X + dx[i], p0.Y + dy[i]);
            //        if (bmp.GetPixel(p1.X, p1.Y).IsTheSameAs(background) &&
            //            (bmp.GetPixel(p0.X + dx[i], p0.Y).IsTheSameAs(background) ||
            //            bmp.GetPixel(p0.X, p0.Y + dy[i]).IsTheSameAs(background)))
            //            stack.Push(p1);
            //    }
            //}
        }
    }
}
