using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FP1.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FP1
{
    class GameScreenManager
    {
        // Used  in preventing changing screens to draw before first update
        bool sleepout = false;
        bool activesleep = false;

        Screen currentScreen;
        public GameScreenManager()
        {
            ChangeScreen(new MainMenu());
        }
        public void Update(GameTime gt)
        {
            sleepout = true;
            currentScreen.Update(gt, this);
        }
        public void Draw(GameTime gt, SpriteBatch sb)
        {
            if (!activesleep)
            {
                currentScreen.Draw(gt, sb);
            }
            sleepout = false;
            activesleep = false;
        }

        public void ChangeScreen(Screen newscr)
        {
            newscr.Start();
            newscr.Manager = this;
            currentScreen = newscr;
            if (sleepout)
                activesleep = true;
        }
    }
}
