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

namespace FP1.Minigames.PipeDown
{
    class PipeDown : Minigame
    {

        const Rectangle GAMESPACE = new Rectangle(160, 90, 1280, 720);
        List<Pipe> pipes;
        Player[] players;

        Image pipeSprite;
        Image pipeEndSprite;
        Image lightSprite;
        Image lightHitSprite;
        SpriteFont myFont;

        int frameCount;

        public PipeDown : base("Pipe Down", "Flash 'Em!"){ }

        public override void Start(Player[] InGame)
        {

            players = InGame;
            pipes = new List<Pipe>();
            
            frameCount = 0;

        }

        public override void Load(ContentManager cm)
        {

            pipeSprite = new Image("Minigames\\PipeDown\\pipe");
            pipeEndSprite = new Image("Minigames\\PipeDown\\pipeEnd");
            lightSprite = new Image("Minigames\\PipeDown\\light");
            lightHitSprite = new Image("Minigames\\PipeDown\\lightHit");
            myFont = TextureManager.getFont("Minigames\\PipeDown\\myfont");

            // TODO: Add the rest of the pipes
            Rectangle ltRec = new Rectangle(GAMESPACE.Left, GAMESPACE.Top, pipeSprite.getTexture().Width, pipeSprite.getTexture().Height);
            Rectangle pipeEnd1u = new Rectangle(ltRec.Left, ltRec.Top, pipeEndSprite.getTexture().Width, pipeEndSprite.getTexture().Height);
            Rectangle pipeEnd1d = new Rectangle(ltRec.Left, ltRec.Bottom - pipeEndSprite.getTexture().Height, pipeEndSprite.getTexture().Width, pipeEndSprite.getTexture().Height);
            Rectangle hitLight1u = new Rectangle(ltRec.Left, ltRec.Top - (pipeEndSprite.getTexture().Height/2), lightHitSprite.getTexture().Width, lightHitSprite.getTexture().Height);
            Rectangle hitLight1d = new Rectangle(ltRec.Left, ltRec.Bottom - (pipeEndSprite.getTexture().Height/2), lightHitSprite.getTexture().Width, lightHitSprite.getTexture().Height);
            Rectangle lightBox1 = new Rectangle(ltRec.Left, ltRec.Top + (ltRec.Height/2), lightSprite.getTexture().Width, lightSprite.getTexture().Height);
            pipes.Add( new Pipe(players[1], ControllerButton.LeftTrigger, ltRec, true, true, true, 0, hitLight1u, hitLight1d, lightBox1, pipeEnd1u, pipeEnd1d) );

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            // Start paused and then begin
            if(frameCount >= 180)
            {
                foreach (Pipe pipe in pipes)
                {
                    pipe.setSpeed(2f);
                }
            }

            foreach (Pipe pipe in pipes)
            {

                if(pipe.getPlayer().GamePad.IsButtonPressed(pipe.getButton()))
                {

                    if(pipe.canDown())
                    {

                        // OTHER PLAYER HITS

                    }

                }

            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {



        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
        }

    }
}
