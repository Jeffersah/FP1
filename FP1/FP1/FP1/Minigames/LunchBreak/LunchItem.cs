using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCodeRiddian;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FP1.Minigames.LunchBreak
{
    class LunchItem
    {
        public const int Width = 50;
        public const int Height = 10;
        const float GRAVITY = 1.5f;

        Rectangle Position;
        Vector2 RealPos;
        public bool isFalling;
        Image myImage;

        public LunchItem(Vector2 pos, Image i)
        {
            isFalling = true;
            RealPos = pos;
            Position = new Rectangle((int)RealPos.X, (int)RealPos.Y, Width, Height);
            myImage = i;
        }

        public void Update(Sandwich sandwich)
        {
            if (isFalling)
            {
                RealPos.Y += GRAVITY;
            }

            Position.Y = (int)RealPos.Y;
            Position.X = (int)RealPos.X;
        }

        public void Draw(SpriteBatch sb)
        {
            Camera.draw(sb, myImage, Position);
        }
    }
}
