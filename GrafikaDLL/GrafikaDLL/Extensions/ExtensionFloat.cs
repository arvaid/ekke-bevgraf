using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL.Extensions
{
    public static class ExtensionFloat
    {
        // Egy értéknek az adott intervallumba kényszerítése
        public static float Clamp(this float value,
            float min, float max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        // Egy érték lineáris leképezése egyik intervallumról a másikra
        public static float Remap(this float value,
            float A, float B, float a, float b)
        {
            return (value - A) * (b - a) / (B - A) + a;
        }

        // Lebegőpontos egyenlőség teszt, alapértelmezésben 2 tizedesjegy pontossággal
        public static bool CloseEnough(this float value, 
            float otherValue, float eps = 0.01f) 
        {
            return (float)Math.Abs(value - otherValue) <= eps;
        }
    }
}
