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
using FP1.Screens;
using NCodeRiddian.Input;
using NCodeRiddian;
using DataLoader;

namespace FP1.Minigames.LunchBreak
{
    class Sandwich
    {
        const float MAX_ACCEL = 3;
        const float FRIC = .9f;
        float sandwhichV;
        public List<LunchItem> current;
        Rectangle fullBounds;

        float TotalForce;

        public Sandwich(Image breadImg)
        {
            current = new List<LunchItem>();
            current.Add(new LunchItem(new Vector2(800 - LunchItem.Width / 2, 900 - LunchItem.Height), breadImg));
            current[0].isFalling = false;
            fullBounds = current[0].Position;
            sandwhichV = 0;
        }

        public void Update(Player player1, List<LunchItem> AllLunchItems)
        {
            TotalForce = 0;
            sandwhichV += MAX_ACCEL * player1.GamePad.LeftStick().X;
            float initdir = sandwhichV;
            foreach (LunchItem item in current)
            {
                item.RealPosition = item.RealPosition + new Vector2(sandwhichV, 0);
                if (item.RealPosition.X < 0)
                {
                    ForceBack(item.RealPosition.X * -1);
                }
                else if (item.RealPosition.X + item.Position.Width > Settings.GP_X)
                {
                    ForceBack(Settings.GP_X - (item.RealPosition.X + item.Position.Width));
                }
            }
            foreach (LunchItem item in current)
            {
                item.RealPosition = item.RealPosition + new Vector2(TotalForce, 0);
            }
            if (TotalForce != 0 && Math.Sin(TotalForce) != Math.Sin(sandwhichV))
                sandwhichV = 0;
            foreach (LunchItem item in AllLunchItems)
            {
                foreach (LunchItem mine in current)
                {
                    if (item.Position.Intersects(mine.Position))
                    {
                        if (initdir > 0)
                        {
                            item.RealX = mine.RealX + LunchItem.Width + 1;
                        }
                        else
                        {
                            item.RealX = mine.RealX - LunchItem.Width - 1;
                        }
                    }
                }
            }
            sandwhichV *= FRIC;
        }

        public void ForceBack(float dist)
        {
            TotalForce += dist;
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (LunchItem item in current)
                item.Draw(sb);
        }
    }
}
