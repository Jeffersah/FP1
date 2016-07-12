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
using FP1.Minigames.LunchBreak;

namespace FP1.Minigames
{
    class LunchBreakMain:Minigame
    {
        public static Image Bread;
        public static Image[] Goodies;
        public static Image Spider;

        Sandwich sandwich;
        List<LunchItem> fallingItems;

        public LunchBreakMain() : base("Lunch Break", "Disgust him!") { }

        public override void Load(ContentManager cm)
        {
            Bread = SafeImage.Get("Minigames\\LunchBreak\\Bread");
            Goodies = new Image[] { SafeImage.Get("Minigames\\LunchBreak\\Lettuce"), SafeImage.Get("Minigames\\LunchBreak\\Meat"), SafeImage.Get("Minigames\\LunchBreak\\Cheese"), SafeImage.Get("Minigames\\LunchBreak\\Tomato") };
            Spider = SafeImage.Get("Minigames\\LunchBreak\\Spider");
        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
        }

        public override void Start(Player[] InGame)
        {
            sandwich = new Sandwich(Bread);
            fallingItems = new List<LunchItem>();
            base.Start(InGame);
        }

        public override void Update(GameTime gt, MinigameScreen parentScreen)
        {
            sandwich.Update(Player1, fallingItems);
        }

        public void SpawnReset(/*GameTimer timer*/)
        {
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sandwich.Draw(sb);
        }
    }
}
