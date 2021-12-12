﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    /// <summary>
    /// 3D vektor homogén koordinátákkal
    /// </summary>
    public class Vector4
    {
        public double x, y, z, w;

        public Vector4() :this(0, 0, 0) { }

        public Vector4(double x, double y, double z) :this(x, y, z, 1.0) {}

        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector4 v) : this(v.x, v.y, v.z, v.w) { }

        public static Vector4 operator +(Vector4 v0, Vector4 v1)
        {
            return new Vector4(v0.x + v1.x, v0.y + v1.y, v0.z + v1.z, 1.0);
        }

        public static Vector4 operator -(Vector4 v0, Vector4 v1)
        {
            return new Vector4(v0.x - v1.x, v0.y - v1.y, v0.z - v1.z, 1.0);
        }

        public static Vector4 operator *(Vector4 v0, double l)
        {
            return new Vector4(v0.x * l, v0.y * l, v0.z * l, 1.0);
        }

        public static Vector4 operator *(double l, Vector4 v0)
        {
            return new Vector4(v0.x * l, v0.y * l, v0.z * l, 1.0);
        }
        public static Vector4 operator +(Vector4 v0, Vector2 v1)
        {
            return new Vector4(v0.x + v1.x, v0.y + v1.y, v0.z, 1.0);
        }

        public static Vector4 operator +(Vector2 v0, Vector4 v1)
        {
            return new Vector4(v0.x + v1.x, v0.y + v1.y, v1.z, 1.0);
        }

        public static implicit operator Vector2(Vector4 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static double operator *(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector4 operator ^(Vector4 a, Vector4 b)
        {
            return new Vector4(
                a.y * b.z - a.z - b.z,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x);
        }

        public double Length 
        {
            get { return Math.Sqrt(x * x + y * y + z * z); }
        }

        public void Normalize() {
            double len = Length;
            x /= len; y /= len; z /= len;
        }
    }
}
