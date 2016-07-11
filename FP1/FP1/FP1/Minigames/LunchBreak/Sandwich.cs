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
        List<LunchItem> current;
        Rectangle fullBounds;

        public Sandwich(Image breadImg)
        {
            current = new List<LunchItem>();
            current.Add(new LunchItem(new Vector2(800 - LunchItem.Width / 2, 900 - LunchItem.Height), breadImg));
            current[0].isFalling = false;
        }

        public void Update(Player player1, List<LunchItem> AllLunchItems)
        {

        }
    }
}
