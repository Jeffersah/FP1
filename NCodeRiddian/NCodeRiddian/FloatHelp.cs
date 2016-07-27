using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NCodeRiddian
{
    public abstract class FloatHelp
    {
        public static bool ApproxEquals(float A, float B, float margin)
        {
            return Math.Abs(A - B) <= margin; 
        }

        public static bool ApproxEquals(Vector2 A, Vector2 B, float margin)
        {
            return ApproxEquals(A.X, B.X, margin) && ApproxEquals(A.Y, B.Y, margin);
        }

        public static bool ApproxEquals(DBLV A, DBLV B, double margin)
        {
            return Math.Abs(A.X - B.X) < margin && Math.Abs(A.Y - B.Y) < margin;
        }
    }
}
