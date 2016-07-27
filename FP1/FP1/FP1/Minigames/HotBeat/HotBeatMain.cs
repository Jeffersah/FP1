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
    class HotBeatMain : Minigame
    {

        Rectangle GAMESPACE = new Rectangle(32, 18, 1536, 864);
        Player[] players;

        Sheet mySheet;
        const int FRAMES_PER_BEAT = 36;

        Image sheetSprite;
        Dictionary<ControllerButton, Image> buttonSprites;
        SpriteFont myFont;

        int frameCount;

        public HotBeatMain() : base("hotbeat.wav", "Groove 'Em!"){}

        public override void Load(ContentManager cm)
        {

            sheetSprite = new Image("Minigames\\HotBeat\\musicSheet");

            buttonSprites = new Dictionary<ControllerButton, Image>();
            buttonSprites.Add(ControllerButton.A, new Image("ControllerImages\\A"));
            buttonSprites.Add(ControllerButton.B, new Image("ControllerImages\\B"));
            buttonSprites.Add(ControllerButton.X, new Image("ControllerImages\\X"));
            buttonSprites.Add(ControllerButton.Y, new Image("ControllerImages\\Y"));

            myFont = TextureManager.getFont("Minigames\\HotBeat\\myfont");


        }

        public override void Start(Player[] InGame)
        {

            players = InGame;

            mySheet = new Sheet(GAMESPACE.Left, GAMESPACE.Bottom - sheetSprite.getTexture().Height, sheetSprite.getTexture().Width, sheetSprite.getTexture().Height);

            frameCount = 0;

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            if ((frameCount % FRAMES_PER_BEAT) == 0)
                mySheet.advance(ControllerButton.A, false);

            frameCount++;
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {

            //Camera.drawGeneric(sb, GAMESPACE, Color.Red);

            Camera.draw(sb, sheetSprite, mySheet.getSrc());

        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
            
        }

    }
}
