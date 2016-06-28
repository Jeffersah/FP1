using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using FP1.Screens;

namespace FP1.Minigames
{
    abstract class Minigame
    {
        Player [] Players;

        public static void Load()
        {
            /// For each minigame that has content that needs to be loaded, put the call here
        }

        /// <summary>
        /// Resets the game state, readies this game to run
        /// </summary>
        public virtual void Start(Player[] InGame)
        {
            Players = InGame;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Update(GameTime gt, MinigameScreen parentScreen)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Draw(GameTime gt, SpriteBatch sb)
        {
        }
        
    }
}
