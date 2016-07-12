using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCodeRiddian;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace FP1.Minigames.LunchBreak
{
    class Spider : LunchItem
    {
        LunchBreakMain mainRunner;
        public Player owner;
        public Spider(Image img, LunchBreakMain main, Vector2 pos, Player own)
            : base(pos, img)
        {
            mainRunner = main;
            owner = own;
        }

        public override void HitSammich(Sandwich target, LunchItem item)
        {
            mainRunner.SpiderLanded(this);
            base.HitSammich(target, item);
        }
        public override void Draw(SpriteBatch sb)
        {
            Camera.draw(sb, myImage, new Rectangle(Position.X - 1, Position.Y - 1, Position.Width + 2, Position.Height + 2), Color.Plum);
            //base.Draw(sb);
        }
    }
}
