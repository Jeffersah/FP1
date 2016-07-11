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

namespace FP1.Minigames
{
    class LunchBreakMain:Minigame
    {
        public static Image Bread;
        public static Image[] Goodies;
        public static Image Spider;

        public override void Load(ContentManager cm)
        {
            Bread = SafeImage.Get("Minigames\\LunchBreak\\Bread");
            Goodies = new Image[] { SafeImage.Get("Minigames\\LunchBreak\\Lettuce"), SafeImage.Get("Minigames\\LunchBreak\\Meat"), SafeImage.Get("Minigames\\LunchBreak\\Cheese"), SafeImage.Get("Minigames\\LunchBreak\\Tomato") };
            Spider = SafeImage.Get("Minigames\\LunchBreak\\Spider");
        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gt, MinigameScreen parentScreen)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            throw new NotImplementedException();
        }
    }
}
