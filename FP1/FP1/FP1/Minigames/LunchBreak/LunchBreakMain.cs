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
        const int MIN_TIME = 100;
        const int MAX_TIME = 500;

        public static Image Bread;
        public static Image[] Goodies;
        public static Image Spider;

        Sandwich sandwich;
        List<LunchItem> fallingItems;
        bool sTimer = false;

        GameTime Last;

        bool keepSpawning = true;
        bool enableControl = true;

        Spider first;

        GameTimer victorytimer;
        SpriteFont font;

        public LunchBreakMain() : base("Lunch Break", "Disgust him!") { }

        public override void Load(ContentManager cm)
        {
            Bread = SafeImage.Get("Minigames\\LunchBreak\\Bread");
            Goodies = new Image[] { SafeImage.Get("Minigames\\LunchBreak\\Lettuce"), SafeImage.Get("Minigames\\LunchBreak\\Meat"), SafeImage.Get("Minigames\\LunchBreak\\Cheese"), SafeImage.Get("Minigames\\LunchBreak\\Tomato") };
            Spider = SafeImage.Get("Minigames\\LunchBreak\\Spider");
            font = TextureManager.getFont("GenericMenu");
        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
            if (!p.isP1)
            {
                p.GamePad.GetSimState().SetLeftStick(new Vector2((sandwich.current[0].RealX + (LunchItem.Width / 2)) / Settings.GP_X,0));
                if (p.GamePad.IsButtonDown(ControllerButton.A))
                {
                    GameTimer.AddStaticTimer(Settings.UPDATE_GT, new TimeSpan(0, 0, 0, 0, 100), (x) => { p.GamePad.GetSimState().SetButtonUp(ControllerButton.A); });
                }
                else
                {
                    GameTimer.AddStaticTimer(Settings.UPDATE_GT, new TimeSpan(0, 0, 0, 0, GlobalRandom.random.Next(1000) + 100), (x) => { p.GamePad.GetSimState().SetButtonDown(ControllerButton.A); });
                }
            }
        }

        public override void Start(Player[] InGame)
        {
            victorytimer = new GameTimer(Settings.UPDATE_GT, new TimeSpan(0, 0, 30), x =>
            {
                Finish(Players.Where(i=>!i.isP1).ToList());
            });

            sandwich = new Sandwich(Bread);
            fallingItems = new List<LunchItem>();
            base.Start(InGame);
        }

        public override void Update(GameTime gt, MinigameScreen parentScreen)
        {
            Last = gt;
            if (!sTimer)
            {
                sTimer = true;
                GameTimer.AddStaticTimer(gt, new TimeSpan(0, 0, 0, 0, GlobalRandom.random.Next(MAX_TIME - MIN_TIME) + MIN_TIME), SpawnReset);
            }
            if(enableControl)
                sandwich.Update(Player1, fallingItems);
            foreach (LunchItem item in fallingItems)
            {
                item.Update(sandwich);
            }
            fallingItems.RemoveAll(x => !x.isFalling || x.RealPosition.Y > Settings.GP_Y);

            if (!enableControl)
                return;
            // do Spider Spawn
            foreach (Player p in Players)
            {
                if (!p.isP1 && p.GamePad.IsButtonDown(ControllerButton.A))
                {
                    if (fallingItems.Any(x => x is Spider && ((Spider)x).owner == p))
                    {
                        // Already a spider falling. Wait
                    }
                    else
                    {
                        fallingItems.Add(new Spider(Spider, this, new Vector2(p.GamePad.LeftStick().X * Settings.GP_X, -LunchItem.Height), p));
                    }
                }
            }
            victorytimer.Update(gt);
        }

        public void SpiderLanded(Spider s)
        {
            victorytimer.Stop();
            if (first == null)
                first = s;
            enableControl = false;
            keepSpawning = false;

            GameTimer.AddStaticTimer(Settings.UPDATE_GT, new TimeSpan(0, 0, 3), x => {
                Finish(first.owner);
            });
        }

        public void SpawnReset(GameTimer timer)
        {
            fallingItems.Add(new LunchItem(new Vector2((float)GlobalRandom.NextBetween(0, Settings.GP_X - LunchItem.Width), -LunchItem.Height), GlobalRandom.RandomFrom(Goodies)));
            if(keepSpawning)
                GameTimer.AddStaticTimer(Last, new TimeSpan(0, 0, 0, 0, GlobalRandom.random.Next(MAX_TIME - MIN_TIME) + MIN_TIME), SpawnReset);
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sandwich.Draw(sb);
            foreach (LunchItem item in fallingItems)
            {
                item.Draw(sb);
            }
            TimeSpan remaining = victorytimer.RemainingTime();
            Rectangle StringArea = new Rectangle(0, 0, (int)font.MeasureString(remaining.Seconds + "").X, (int)font.MeasureString(remaining.Seconds + "").Y);
            StringArea.X = Settings.GP_X / 2 - StringArea.Width / 2;
            Camera.drawString(sb, font, "" + remaining.Seconds, new Vector2(StringArea.X, StringArea.Y), remaining.Seconds > 10 ? Color.White : Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
