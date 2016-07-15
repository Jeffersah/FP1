using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NCodeRiddian;
using DataLoader;

namespace FP1.Minigames
{
    class TugLifeMain : Minigame
    {

        Image ropeSprite;
        Image muscleManSprite;
        SpriteFont myFont;

        public TugLifeMain() : base("Tug Life", "Tug 'Em!"){ }

        public override void Load(ContentManager cm)
        {

            ropeSprite = new Image("Minigames\\TugLife\\rope");
            muscleManSprite = new Image("Minigames\\TugLife\\muscleMan");

            myFont = TextureManager.getFont("Minigames\\TugLife\\tugFont");

        }

        public override void Start(Player[] InGame)
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
        }

    }
}
