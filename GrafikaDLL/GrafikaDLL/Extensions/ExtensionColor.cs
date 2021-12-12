using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GrafikaDLL
{
    public static class ExtensionColor
    {
        public static bool IsTheSameAs(this Color color, Color other)
        {
            return color.R == other.R && 
                   color.B == other.B && 
                   color.G == other.G;
        }
    }
}
