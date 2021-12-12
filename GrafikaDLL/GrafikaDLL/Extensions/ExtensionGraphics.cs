using GrafikaDLL.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GrafikaDLL
{
    public static class ExtensionGraphics
    {
        #region DrawLine
        public static void DrawLine(this Graphics g, 
            Pen pen, Vector2 v1, Vector2 v2) 
        {
            g.DrawLine(pen, (float)v1.x, (float)v1.y, (float)v2.x, (float)v2.y);
        }

        public static void DrawLine(this Graphics g,
            Pen pen, Vector4 v1, Vector4 v2)
        {
            g.DrawLine(pen, (float)v1.x, (float)v1.y, (float)v2.x, (float)v2.y);
        }
        #endregion

        #region DrawLine DDA
        public static void DrawLineDDA(this Graphics g,
            Pen pen, float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float incX = dx / length;
            float incY = dy / length;
            float x = x1, y = y1;
            for (int i = 0; i <= length; i++)
            {
                g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
                x += incX;
                y += incY;
            }
        }

        public static void DrawLineDDA(this Graphics g,
            Pen pen, int x1, int y1, int x2, int y2)
        {
            g.DrawLineDDA(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public static void DrawLineDDA(this Graphics g,
            Pen pen, PointF p1, PointF p2)
        {
            g.DrawLineDDA(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        public static void DrawLineDDA(this Graphics g,
            Pen pen, Point p1, Point p2)
        {
            g.DrawLineDDA(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        public static void DrawLineDDA(this Graphics g,
           Color c1, Color c2, float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float incX = dx / length;
            float incY = dy / length;
            float incR = (c2.R - c1.R) / length;
            float incG = (c2.G - c1.G) / length;
            float incB = (c2.B - c1.B) / length;
            float x = x1, y = y1;
            float R = c1.R, G = c1.G, B = c1.B;
            for (int i = 0; i <= length; i++)
            {
                g.DrawRectangle(new Pen(Color.FromArgb((int)R, (int)G, (int)B)), x, y, 0.5f, 0.5f);
                x += incX;
                y += incY;
                R += incR; G += incG; B += incB;
            }
        }

        public static void DrawLineDDA(this Graphics g,
           Color c1, Color c2, PointF p1, PointF p2)
        {
            g.DrawLineDDA(c1, c2, p1.X, p1.Y, p2.X, p2.Y);
        }
        #endregion

        #region DrawLine Midpoint
        public static void DrawLineMidpoint(this Graphics g,
           Pen pen, float x1, float y1, float x2, float y2)
        {
            float dx = Math.Abs(x2 - x1);
            float dy = Math.Abs(y2 - y1);
            float sx = Math.Sign(x2 - x1);
            float sy = Math.Sign(y2 - y1);

            bool t = false;

            if (dx < dy)
            {
                t = true;
                float swp = dx;
                dx = dy;
                dy = swp;
            }

            float d = 2 * dy - dx;
            float x = x1;
            float y = y1;

            while (x != x2 || y != y2)
            {
                g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
                if (d > 0)
                {
                    if (t) { x += sx; }
                    else   { y += sy; }
                    d -= 2 * dx;
                }
                else
                {
                    if (t) { y += sy; }
                    else   { x += sx; }
                    d += 2 * dy;
                }
            }
        }

        public static void DrawLineMidpoint(this Graphics g,
          Pen pen, PointF p1, PointF p2)
        {
            g.DrawLineMidpoint(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        public static void DrawLineMidpoint(this Graphics g,
           Color c1, Color c2, float x1, float y1, float x2, float y2)
        {
            float dx = Math.Abs(x2 - x1);
            float dy = Math.Abs(y2 - y1);
            float sx = Math.Sign(x2 - x1);
            float sy = Math.Sign(y2 - y1);
            float length = Math.Max(dx, dy);
            float incR = (c2.R - c1.R) / length;
            float incG = (c2.G - c1.G) / length;
            float incB = (c2.B - c1.B) / length;
            float R = c1.R, G = c1.G, B = c1.B;

            bool t = false;

            if (dx < dy)
            {
                t = true;
                float swp = dx;
                dx = dy;
                dy = swp;
            }

            float d = 2 * dy - dx;
            float x = x1;
            float y = y1;

            do
            {
                g.DrawRectangle(new Pen(Color.FromArgb((int)R, (int)G, (int)B)), x, y, 0.5f, 0.5f);
                R = (R + incR).Clamp(0, 255);
                G = (G + incG).Clamp(0, 255);
                B = (B + incB).Clamp(0, 255);
                if (d > 0)
                {
                    if (t) { x += sx; }
                    else { y += sy; }
                    d -= 2 * dx;
                }
                else
                {
                    if (t) { y += sy; }
                    else { x += sx; }
                    d += 2 * dy;
                }
            }
            while (x != x2 || y != y2);
        }

        public static void DrawLineMidpoint(this Graphics g,
          Color c1, Color c2, PointF p1, PointF p2)
        {
            g.DrawLineMidpoint(c1, c2, p1.X, p1.Y, p2.X, p2.Y);
        }
        #endregion

        #region DrawPoint

        public static void DrawPoint(this Graphics g, Pen pen, Brush brush,
            Vector2 v, float r)
        {
            g.FillEllipse(brush, (float)(v.x - r), (float)(v.y - r), 2 * r, 2 * r);
            g.DrawEllipse(pen, (float)(v.x - r), (float)(v.y - r), 2 * r, 2 * r);
        }

        #endregion

        #region DrawPolygon
        public static void DrawPolygonDDA(this Graphics g, 
            Pen pen, PointF[] points, bool closed = false)
        {
            if (points.Length < 2)
            {
                throw new ArgumentException("Legalább 2 pontra van szükség poligon rajzoláshoz!");
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                g.DrawLineDDA(pen, points[i], points[i + 1]);
            }

            if (closed)
            {
                g.DrawLineDDA(pen, points[points.Length - 1], points[0]);
            }
        }

        public static void DrawPolygonDDA(this Graphics g,
            Color[] colors, PointF[] points, bool closed = false)
        {
            if (points.Length < 2)
            {
                throw new ArgumentException("Legalább 2 pontra van szükség poligon rajzoláshoz!");
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                g.DrawLineDDA(colors[i], colors[i + 1], points[i], points[i + 1]);
            }

            if (closed)
            {
                g.DrawLineDDA(colors[colors.Length - 1], colors[0], 
                    points[points.Length - 1], points[0]);
            }
        }
        #endregion

        #region DrawCircle
        public static void DrawCircle(this Graphics g,
            Pen pen, PointF C, float r)
        {
            float x = 0.0f;
            float y = r;
            float h = 1.0f - r;

            do
            {
                g.DrawCirclePixel(pen, x, y, C.X, C.Y);
                if (h < 0) { h += 2 * x + 3; }
                else
                {
                    h += 2 * (x - y) + 5;
                    y -= 1;
                }
                x += 1;
            }
            while (x <= y);
        }

        public static void DrawCircle(this Graphics g,
            Color c1, Color c2, PointF C, float r)
        {
            float x = 0.0f;
            float y = r;
            float h = 1.0f - r;
            float length = (float)(2 * r * Math.PI);
            float incR = (c2.R - c1.R) / length;
            float incG = (c2.G - c1.G) / length;
            float incB = (c2.B - c1.B) / length;
            float R = c1.R, G = c1.G, B = c1.G;

            float color = 0;

            do
            {
                g.DrawCirclePixel(new Pen(Color.FromArgb(
                    (int)(color / length * 255), (int)(color / length * 255), (int)(color / length * 255))), x, y, C.X, C.Y);
                R += incR; G += incG; B += incB;
                if (h < 0) { h += 2 * x + 3; }
                else
                {
                    h += 2 * (x - y) + 5;
                    y -= 1;
                }
                x += 1;
            }
            while (x <= y);
        }

        private static void DrawCirclePixel(this Graphics g,
            Pen pen, float x, float y, float cx, float cy)
        {
            g.DrawRectangle(pen, cx + x, cy + y, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx + x, cy - y, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx - x, cy - y, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx - x, cy + y, 0.5f, 0.5f);

            g.DrawRectangle(pen, cx + y, cy + x, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx + y, cy - x, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx - y, cy + x, 0.5f, 0.5f);
            g.DrawRectangle(pen, cx - y, cy - x, 0.5f, 0.5f);
        }
        #endregion

        #region DrawHermiteArc

        public static void DrawHermiteArc(this Graphics g, Pen pen,
            Vector2 p0, Vector2 p1, Vector2 t0, Vector2 t1)
        {
            g.DrawParametricCurve2D(pen,
                t => Hermite.H0(t) * p0.x + Hermite.H1(t) + p1.x +
                     Hermite.H2(t) * t0.x + Hermite.H3(t) * t1.x,
                t => Hermite.H0(t) * p0.y + Hermite.H1(t) + p1.y +
                     Hermite.H2(t) * t0.y + Hermite.H3(t) * t1.y,
                0.0, 1.0);
        }

        public static void DrawHermiteArc(this Graphics g, Pen pen,
            HermiteArc arc)
        {
            g.DrawParametricCurve2D(pen,
                t => Hermite.H0(t) * arc.p0.x + Hermite.H1(t) * arc.p1.x +
                     Hermite.H2(t) * arc.t0.x * arc.weight + Hermite.H3(t) * arc.t1.x * arc.weight,
                t => Hermite.H0(t) * arc.p0.y + Hermite.H1(t) * arc.p1.y +
                     Hermite.H2(t) * arc.t0.y * arc.weight + Hermite.H3(t) * arc.t1.y * arc.weight,
                     0.0, 1.0);
        }

        #endregion

        #region DrawBezier3Curve
        public static void DrawBezier3Arc(this Graphics g, Pen pen,
            Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            g.DrawParametricCurve2D(pen,
                t => Bezier3.B0(t) * p0.x + Bezier3.B1(t) * p1.x +
                     Bezier3.B2(t) * p2.x + Bezier3.B3(t) * p3.x,
                t => Bezier3.B0(t) * p0.y + Bezier3.B1(t) * p1.y +
                     Bezier3.B2(t) * p2.y + Bezier3.B3(t) * p3.y,
                     0.0, 1.0);
        }
        public static void DrawBSpline(this Graphics g, Pen pen,
            Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            g.DrawParametricCurve2D(pen,
                t => BSpline.N0(t) * p0.x + BSpline.N1(t) * p1.x +
                     BSpline.N2(t) * p2.x + BSpline.N3(t) * p3.x,
                t => BSpline.N0(t) * p0.y + BSpline.N1(t) * p1.y +
                     BSpline.N2(t) * p2.y + BSpline.N3(t) * p3.y,
                     0.0, 1.0);
        }
        public static void DrawBezier(this Graphics g, Pen pen,
            List<Vector2> V)
        {
            double t = 0;
            double h = 1.0 / 500.0;
            Vector2 v0 = new Vector2(0, 0);
            int n = V.Count - 1;
            for (int i = 0; i <= n; i++)
            {
                v0.x += Bezier.B(i, n, t) * V[i].x;
                v0.y += Bezier.B(i, n, t) * V[i].y;
            }
            while (t < 1)
            {
                t += h;
                Vector2 v1 = new Vector2(0, 0);
                for (int i = 0; i <= n; i++)
                {
                    v1.x += Bezier.B(i, n, t) * V[i].x;
                    v1.y += Bezier.B(i, n, t) * V[i].y;
                }
                g.DrawLine(pen, v0, v1);
                v0 = v1;
            }
        }
        #endregion

        #region DrawParametricCurve2D
        public static void DrawParametricCurve2D(this Graphics g, Pen pen,
            Func<double, double> X, Func<double, double> Y,
            double a, double b, int n = 500)
        {
            double t = a;
            double h = (b - a) / n;
            Vector2 v0 = new Vector2(X(t), Y(t));
            while (t < b)
            {
                t += h;
                Vector2 v1 = new Vector2(X(t), Y(t));
                g.DrawLine(pen, v0, v1);
                v0 = v1;
            }
        }
        #endregion

        #region DrawParametricCurve3D
        public static void DrawParametricCurve3D(this Graphics g, Pen pen,
            Func<double, double> X, Func<double, double> Y, Func<double, double> Z,
            Matrix4 projection,
            double a, double b, int n = 500)
        {
            g.DrawParametricCurve3D(pen, X, Y, Z, projection, a, b, new Vector2(0.0, 0.0), n);
        }

        public static void DrawParametricCurve3D(this Graphics g, Pen pen,
            Func<double, double> X, Func<double, double> Y, Func<double, double> Z,
            Matrix4 projection,
            double a, double b, 
            Vector2 translate2D,
            int n = 500)
        {
            g.DrawParametricCurve3D(pen, X, Y, Z, 
                Matrix4.GetIdentity(), projection, a, b, translate2D, n);
        }

        public static void DrawParametricCurve3D(this Graphics g, Pen pen,
            Func<double, double> X, Func<double, double> Y, Func<double, double> Z,
            Matrix4 transformation,
            Matrix4 projection,
            double a, double b,
            Vector2 translate2D,
            int n = 500)
        {
            double t = a;
            double h = (b - a) / n;
            Vector4 v0 = new Vector4(X(t), Y(t), Z(t));
            while (t < b)
            {
                t += h;
                Vector4 v1 = new Vector4(X(t), Y(t), Z(t));
                Vector2 pv0 = (projection * (transformation * v0)) + translate2D;
                Vector2 pv1 = (projection * (transformation * v1)) + translate2D;
                g.DrawLine(pen, pv0, pv1);
                v0 = v1;
            }
        }
        #endregion

        #region DrawParametricSurface
        public static void DrawParametricSurfaceWithParameterLines(this Graphics g, Pen pen,
            Func<double, double, double> X, 
            Func<double, double, double> Y, 
            Func<double, double, double> Z,
            Matrix4 transformation,
            Matrix4 projection,
            double a, double b, 
            double c, double d,
            Vector2 translate2D,
            int n0 = 25, int n1 = 25)
        {
            double h0 = (d - c) / n0;
            for (double u = c; u <= d; u+= h0)
            {
                    g.DrawParametricCurve3D(pen,
                        t => X(t, u),
                        t => Y(t, u),
                        t => Z(t, u),
                        transformation,
                        projection,
                        a, b,
                        translate2D,
                        n0
                    );
            }

            double h1 = (b - a) / n1;
            for (double T = a; T <= b; T += h1)
            {
                g.DrawParametricCurve3D(pen,
                        U => X(T, U),
                        U => Y(T, U),
                        U => Z(T, U),
                        transformation,
                        projection,
                        a, b,
                        translate2D,
                        n0
                    );
            }
        }

        public static void DrawParametricSurfaceWithDots(this Graphics g, Pen pen,
            Func<double, double, double> X,
            Func<double, double, double> Y,
            Func<double, double, double> Z,
            Matrix4 transformation,
            Matrix4 projection,
            double a, double b,
            double c, double d,
            Vector2 translate2D,
            int n0 = 25, int n1 = 25)
        {
            double h0 = (d - c) / n0;
            for (double u = c; u <= d; u += h0)
            {
                double h1 = (b - a) / n1;
                for (double T = a; T <= b; T += h1)
                {
                    Vector4 v1 = new Vector4(X(T, u), Y(T, u), Z(T, u));
                    Vector2 pv1 = (projection * (transformation * v1)) + translate2D;

                    g.DrawRectangle(pen, (float)pv1.x, (float)pv1.y, 0.5f, 0.5f);
                }
            }
        }

        public static void DrawParametricSurfaceWithDotsAndNormals(this Graphics g, Pen pen,
            Func<double, double, double> X,
            Func<double, double, double> Y,
            Func<double, double, double> Z,
            Matrix4 transformation,
            Matrix4 projection,
            double a, double b,
            double c, double d,
            Vector2 translate2D,
            int n0 = 25, int n1 = 25)
        {
            // FIXME: nem egészen jó...
            double h0 = (d - c) / n0;
            for (double u = c; u <= d; u += h0)
            {
                double h1 = (b - a) / n1;
                for (double T = a; T <= b; T += h1)
                {
                    Vector4 v1 = new Vector4(X(T, u), Y(T, u), Z(T, u));
                    Vector2 pv1 = (projection * (transformation * v1)) + translate2D;

                    Vector4 v0 = new Vector4(X(T, u + h0), Y(T, u + h0), Z(T, u + h0));
                    Vector4 v2 = new Vector4(X(T + h1, u), Y(T + h1, u), Z(T + h1, u));
                    Vector4 cross = (v0 - v1) ^ (v2 - v1);
                    cross.Normalize();
                    Vector4 pCross = (projection * (transformation * cross)) + translate2D;

                    g.DrawRectangle(pen, (float)pv1.x, (float)pv1.y, 0.5f, 0.5f);
                    g.DrawLine(pen, pv1, pCross);
                }
            }
        }
        #endregion

        #region DrawBRep

        private static void DrawBRepWithDots(this Graphics g, Pen pen,
            BRep model,
            Matrix4 transformation,
            Matrix4 projection,
            Vector2 translate2D)
        {
            foreach (var vertex in model.vertices)
            {
                Vector2 pv0 = (projection * (transformation * vertex)) + translate2D;
                g.DrawRectangle(pen, (float)pv0.x, (float)pv0.y, 0.5f, 0.5f);
            }
        }
        private static void DrawBRepWithEdges(this Graphics g, Pen pen,
            BRep model, 
            Matrix4 transformation,
            Matrix4 projection,
            Vector2 translate2D)
        {
            foreach (Edge edge in model.edges)
            {
                Vector2 pv0 = (projection * (transformation * edge.v0)) + translate2D;
                Vector2 pv1 = (projection * (transformation * edge.v1)) + translate2D;
                g.DrawLine(pen, pv0, pv1);
            }
        }

        private static void DrawBRepWithTriangles(this Graphics g, Pen pen,
            BRep model,
            Matrix4 transformation,
            Matrix4 projection,
            Vector2 translate2D)
        {
            foreach (Triangle triangle in model.triangles)
            {
                Vector4 tv0 = transformation * triangle.v0;
                Vector4 tv1 = transformation * triangle.v1;
                Vector4 tv2 = transformation * triangle.v2;

                Triangle transformed = new Triangle(tv0, tv1, tv2);
                Vector4 pv0 = projection * tv0 + translate2D;
                Vector4 pv1 = projection * tv1 + translate2D;
                Vector4 pv2 = projection * tv2 + translate2D;

                g.DrawLine(pen, pv0, pv1);
                g.DrawLine(pen, pv1, pv2);
                g.DrawLine(pen, pv2, pv0);
            }
        }

        public static void DrawObjectBRepWithDots(this Graphics g,
            ObjectBRep objBrep,
            Matrix4 projection, Vector2 translate2D)
        {
            g.DrawBRepWithDots(new Pen(objBrep.color), objBrep.model,
                objBrep.transform, projection, translate2D);
        }

        public static void DrawObjectBRepWithEdges(this Graphics g,
            ObjectBRep objBrep, 
            Matrix4 projection, Vector2 translate2D)
        {
            g.DrawBRepWithEdges(new Pen(objBrep.color), objBrep.model, 
                objBrep.transform, projection, translate2D);
        }

        public static void DrawObjectBRepWithTriangles(this Graphics g,
            ObjectBRep objBrep,
            Matrix4 projection, Vector2 translate2D)
        {
            g.DrawBRepWithTriangles(new Pen(objBrep.color), objBrep.model,
                objBrep.transform, projection, translate2D);
        }

        public static void DrawObjectBRepWithTriangles(this Graphics g,
           ObjectBRep objBrep, Vector4 viewVector,
           Matrix4 projection, Vector2 translate2D)
        {
            foreach (Triangle triangle in objBrep.model.triangles)
            {
                Vector4 tv0 = objBrep.transform * triangle.v0;
                Vector4 tv1 = objBrep.transform * triangle.v1;
                Vector4 tv2 = objBrep.transform * triangle.v2;

                Triangle transformed = new Triangle(tv0, tv1, tv2);
                if (transformed.isVisible(viewVector))
                {
                    Vector4 pv0 = projection * tv0 + translate2D;
                    Vector4 pv1 = projection * tv1 + translate2D;
                    Vector4 pv2 = projection * tv2 + translate2D;

                    g.DrawLine(new Pen(objBrep.color), pv0, pv1);
                    g.DrawLine(new Pen(objBrep.color), pv1, pv2);
                    g.DrawLine(new Pen(objBrep.color), pv2, pv0);
                }
            }
        }

        // FIXME: nem teljesen jól dönti el, hogy mely háromszögek láthatóak
        public static void FillObjectBRepWithTriangles(this Graphics g,
            ObjectBRep objBrep, Vector4 viewVector,
            Matrix4 projection, Vector2 translate2D)
        {
            //var visibleTriangles = new List<Tuple<Triangle, Color>>();

            foreach (Triangle triangle in objBrep.model.triangles)
            {
                Vector4 tv0 = objBrep.transform * triangle.v0;
                Vector4 tv1 = objBrep.transform * triangle.v1;
                Vector4 tv2 = objBrep.transform * triangle.v2;

                Triangle transformed = new Triangle(tv0, tv1, tv2);
                if (transformed.isVisible(viewVector))
                {
                    Vector4 pv0 = projection * tv0 + translate2D;
                    Vector4 pv1 = projection * tv1 + translate2D;
                    Vector4 pv2 = projection * tv2 + translate2D;

                    double colorIntensity = (transformed.NormalAtV0 * viewVector) /
                        (transformed.NormalAtV0.Length * viewVector.Length);
                    Color fillColor = Color.FromArgb(
                        (int)(colorIntensity * objBrep.color.R),
                        (int)(colorIntensity * objBrep.color.G),
                        (int)(colorIntensity * objBrep.color.R));
                    g.FillTriangle(fillColor, pv0, pv1, pv2);
                    //var tri2 = new Triangle(pv0, pv1, pv2);
                    //visibleTriangles.Add(new Tuple<Triangle, Color>(tri2, fillColor));
                }
            }

            //var visibleTriangles2 = visibleTriangles
            //    .OrderBy(x => x.Item1.WeightZ)
            //    .Reverse()
            //    .ToList();

            //for (int i = 0; i < visibleTriangles2.Count; i++)
            //{
            //    int index = visibleTriangles
            //        .Select(tuple => tuple.Item1)
            //        .ToList()
            //        .FindIndex(tri => tri == visibleTriangles2[i].Item1);
            //    g.FillTriangle(
            //        visibleTriangles[index].Item2, 
            //        visibleTriangles2[i].Item1.v0, 
            //        visibleTriangles2[i].Item1.v1, 
            //        visibleTriangles2[i].Item1.v2);
            //}
        }

        #endregion

        #region FillTriangle

        public static void FillTriangle(this Graphics g,
            Color color, Vector4 v0, Vector4 v1, Vector4 v2)
        {
            g.FillPolygon(new SolidBrush(color),
                new PointF[] {
                    new PointF((float)v0.x, (float)v0.y),
                    new PointF((float)v1.x, (float)v1.y),
                    new PointF((float)v2.x, (float)v2.y)                    
                });
        }

        #endregion

        #region Clip
        private const byte TOP = 8;     //1000
        private const byte BOTTOM = 4;  //0100
        private const byte LEFT = 2;    //0010
        private const byte RIGHT = 1;   //0001

        private static byte OutCode(Rectangle window, PointF p)
        {
            byte code = 0;
            if (p.X < window.Left) code |= LEFT;
            else if (p.X > window.Right) code |= RIGHT;
            if (p.Y < window.Top) code |= TOP;
            else if (p.Y > window.Bottom) code |= BOTTOM;
            return code;
        }

        public static void Clip(this Graphics g, Pen pen,
            Rectangle window, PointF p0, PointF p1)
        {
            byte code0 = OutCode(window, p0);
            byte code1 = OutCode(window, p1);
            bool accept = false;

            while (true)
            {
                if ((code0 | code1) == 0)
                {
                    accept = true;
                    break;
                }
                else if ((code0 & code1) != 0)
                {
                    break;
                }
                else
                {
                    byte code = code0 != 0 ? code0 : code1;
                    float x = 0, y = 0;

                    if ((code & TOP) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (window.Top - p0.Y) / (p1.Y - p0.Y);
                        y = window.Top;
                    }
                    else if ((code & BOTTOM) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (window.Bottom - p0.Y) / (p1.Y - p0.Y);
                        y = window.Bottom;
                    }
                    else if ((code & LEFT) != 0)
                    {
                        x = window.Left;
                        y = p0.Y + (p1.Y - p0.Y) * (window.Left - p0.X) / (p1.X - p0.X);
                    }
                    else if ((code & RIGHT) != 0)
                    {
                        x = window.Right;
                        y = p0.Y + (p1.Y - p0.Y) * (window.Right - p0.X) / (p1.X - p0.X);
                    }

                    if (code == code0)
                    {
                        p0.X = x; p0.Y = y;
                        code0 = OutCode(window, p0);
                    }
                    else
                    {
                        p1.X = x; p1.Y = y;
                        code1 = OutCode(window, p1);
                    }
                }
            }

            if (accept)
                g.DrawLine(pen, p0, p1);
        }
        public static void Clip(this Graphics g, Pen pen,
            Rectangle window, Line line)
        {
            g.Clip(pen, window, line.p0, line.p1);
        }

        public static void ClipToConvex(this Graphics g, Pen pen,
            PointF[] window, Line line)
        {
            throw new NotImplementedException();
        }
        public static void ClipToConcave(this Graphics g, Pen pen,
            PointF[] window, Line line)
        {
            throw new NotImplementedException();
        }
        public static void Clip(this Graphics g, Pen pen,
            PointF[] window, Line line)
        {
            g.ClipToConcave(pen, window, line);
        }
        #endregion
    }
}
