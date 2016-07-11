using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NCodeRiddian;
using FP1.Screens;

namespace FP1
{
    abstract class Screen
    {
        public abstract void Start();
        public abstract void Update(GameTime time, GameScreenManager Manager);
        public abstract void Draw(GameTime time, SpriteBatch sb);

        public static void Load(ContentManager cm)
        {
            MainMenu.Load();
            MinigameScreen.Load(cm);
        }

        public GameScreenManager Manager;
    }
}
