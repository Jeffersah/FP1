using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NCodeRiddian;
using DataLoader;
using NCodeRiddian.Input;

namespace FP1
{
    class ControllerDisplayHelper
    {
        static string PREFIX = "ControllerHelper\\";
        static KeyValuePair<ControllerButton, string>[] ImageNames;
        static KeyValuePair<ControllerButton, Image>[] ImageList;

        static Image BASE;
        static Image LEFT_TRIG;
        static Image RIGHT_TRIG;
        static Image P_COLOR;
        static Image LEFT_STICK;
        static Image RIGHT_STICK;

        static int STICK_MAX;

        const int RAWX = 571;
        const int RAWY = 1350;

        public static float GETAR()
        {
            return RAWX / (float)RAWY;
        }

        public static void Load()
        {
            ImageNames = new KeyValuePair<ControllerButton,string>[]{
                        new KeyValuePair<ControllerButton,string>(ControllerButton.A, "A_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.B, "B_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.X, "X_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Y, "Y_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Up, "UP_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Right, "RIGHT_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Down, "DOWN_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Left, "LEFT_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Select, "SELECT_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Start, "START_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.Big, "BIG_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.LeftShoulder, "LEFT_BUMPER_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.RightShoulder, "RIGHT_BUMPER_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.LeftStick, "LEFT_STICK_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.RightStick, "RIGHT_STICK_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.LeftTrigger, "LEFT_TRIGGER_MASK"),
                        new KeyValuePair<ControllerButton,string>(ControllerButton.RightTrigger, "RIGHT_TRIGGER_MASK"),
            };
            ImageList = new KeyValuePair<ControllerButton, Image>[ImageNames.Length];
            for (int i = 0; i < ImageList.Length; i++)
            {
                ImageList[i] = new KeyValuePair<ControllerButton, Image>(ImageNames[i].Key, new Image(PREFIX+ImageNames[i].Value));
            }
            BASE = new Image(PREFIX + "BASE");
            LEFT_TRIG = new Image(PREFIX + "LEFT_TRIGGER_MASK");
            RIGHT_TRIG = new Image(PREFIX + "RIGHT_TRIGGER_MASK");
            P_COLOR = new Image(PREFIX + "P_COLOR_MASK");

            LEFT_STICK = new Image(PREFIX + "LEFT_STICK");
            RIGHT_STICK = new Image(PREFIX + "RIGHT_STICK");
            STICK_MAX = 35;
        }

        public static float factor(Rectangle area)
        {
            return area.Width / (float)RAWX;
        }
        public static Point AddPoint(Point a, Point b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static void RENDER_CONTROLLER(SpriteBatch sb, ControllerState State, Rectangle area, Color pColor)
        {
            Camera.draw(sb, BASE, area);
            Camera.draw(sb, P_COLOR, area, pColor);
            foreach (KeyValuePair<ControllerButton, Image> pair in ImageList)
            {
                if (State.IsButtonDown(pair.Key))
                {
                    Camera.draw(sb, pair.Value, area);
                }
            }
            Color leftTrigger = pColor;
            leftTrigger.A = (byte)(255 * State.LeftTrigger);
            Color rightTrigger = pColor;
            rightTrigger.A = (byte)(255 * State.RightTrigger);
            Camera.draw(sb, LEFT_TRIG, area, leftTrigger);
            Camera.draw(sb, RIGHT_TRIG, area, rightTrigger);

            Rectangle leftStickLoc = area;
            leftStickLoc.X += (int)(factor(area) * State.LeftStick.X * STICK_MAX * .5f);
            leftStickLoc.Y -= (int)(factor(area) * State.LeftStick.Y * STICK_MAX * .5f);
            if (State.LeftStick != Vector2.Zero)
                Camera.draw(sb, LEFT_STICK, leftStickLoc, Color.Red);
            Rectangle rightStickLoc = area;
            rightStickLoc.X += (int)(factor(area) * State.RightStick.X * STICK_MAX * .5f);
            rightStickLoc.Y -= (int)(factor(area) * State.RightStick.Y * STICK_MAX * .5f);
            if(State.RightStick != Vector2.Zero)
                Camera.draw(sb, RIGHT_STICK, rightStickLoc, Color.Red);
        }
    }
}
