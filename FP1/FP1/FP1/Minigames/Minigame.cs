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
using NCodeRiddian;

namespace FP1.Minigames
{
    abstract class Minigame
    {
        protected Player [] Players;

        string MinigameName;
        string Objective;

        public Image thumbnail;

        public Minigame(string Name, string Obj)
        {
            MinigameName = Name;
            Objective = Obj;
        }

        public virtual void Load(ContentManager cm)
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

        public abstract void RunAI(Player p, Difficulty difficulty);

        /// <summary>
        /// 
        /// </summary>
        public abstract void Update(GameTime gt, MinigameScreen parentScreen);

        /// <summary>
        /// 
        /// </summary>
        public abstract void Draw(GameTime gt, SpriteBatch sb);

        protected void Finish(List<Player> Winners)
        {

        }
    }
}
