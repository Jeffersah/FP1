using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DataLoader;

namespace FP1
{
    abstract class Settings
    {
        public const int NAMELEN = 5;
        public static List<string> PrvNames = new List<string>();

        // Global Pixel XY - No matter the screen resolution, the game will act as though it's 1600/900 and scale up/down
        static Point GPX = new Point(1600, 900);
        public static int GP_X { get { return GPX.X; } set { GPX.X = value; } }
        public static int GP_Y { get { return GPX.Y; } set { GPX.Y = value; } }
        public static Point GP_P { get { return GPX; } set { GPX = value; } }
        public static Rectangle GP_R { get { return new Rectangle(0, 0, GPX.X, GPX.Y); } }

        // Actual Screen Resolution
        static Point ActualScreen;
        public static int Screen_X { get { return ActualScreen.X; } set { ActualScreen.X = value; RECALC_TGT_RECT();  } }
        public static int Screen_Y { get { return ActualScreen.Y; } set { ActualScreen.Y = value; RECALC_TGT_RECT(); } }
        public static Point Screen_P { get { return ActualScreen; } set { ActualScreen.X = value.X; ActualScreen.Y = value.Y; RECALC_TGT_RECT();  } }
        public static Rectangle TargetRectangle;

        private static void RECALC_TGT_RECT()
        {
            double TgtRes = GP_X / (double)GP_Y;
            double ActualRes = Screen_X / (double)Screen_Y;
            if (TgtRes > ActualRes)
            {
                //TGTX adj
                TargetRectangle.Width = Screen_X;
                TargetRectangle.Height = (int)(TargetRectangle.Width / TgtRes);
                TargetRectangle.Y += (Screen_Y - TargetRectangle.Height) / 2;
            }
            else if (TgtRes == ActualRes)
            {
                TargetRectangle = new Rectangle(0, 0, Screen_X, Screen_Y);
            }
            else
            {
                //TGTY adj
                TargetRectangle.Height = Screen_Y;
                TargetRectangle.Width = (int)(TgtRes * (double)TargetRectangle.Height);
                TargetRectangle.X += (Screen_X - TargetRectangle.Width) / 2;
            }
        }

        public static void Load()
        {
            PropertyObject pvname = PropertyObject.Load("PrevNames");
            foreach (PropertyDefinition pd in pvname.GetAllFields())
            {
                if (pd.value.value<string>().Length <= NAMELEN)
                {
                    PrvNames.Add(pd.value.value<string>());
                }
            }
        }
    }
}
