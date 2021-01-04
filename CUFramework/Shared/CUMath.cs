using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUFramework.Shared
{
    public class CUMath
    {
        public static float FMax(float f, float max)
        {
            return f > max ? max : f;
        }

        public static float FMin(float f, float min)
        {
            return f < min ? min : f;
        }
    }
}
