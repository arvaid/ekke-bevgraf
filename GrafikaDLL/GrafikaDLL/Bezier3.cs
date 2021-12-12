﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    class Bezier3
    {
        public static double B0(double t)
        {
            return (1 - t) * (1 - t) * (1 - t);
        }

        public static double B1(double t)
        {
            return 3 * (1 - t) * (1 - t) * t;
        }

        public static double B2(double t)
        {
            return 3 * t * t * (1 - t);
        }

        public static double B3(double t)
        {
            return t * t * t;
        }
    }
}
