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
    class PipeDown : Minigame
    {

        Rectangle GAMESPACE = new Rectangle(160, 90, 1280, 720);
        const int PADDING = 20;
        List<Pipe> pipes;
        Player[] players;

        Image pipeSprite;
        Image pipeEndSprite;
        Image lightSprite;
        Image lightHitSprite;
        SpriteFont myFont;

        int frameCount;
        const int RETRACT_SPEED = 1;

        int[] scores = new int[]{0, 0, 0, 0};

        public PipeDown() : base("Pipe Down", "Flash 'Em!"){ }

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

            pipes.Add(new Pipe(players[1], ControllerButton.LeftTrigger, GAMESPACE.Left, GAMESPACE.Top)); // LT
            pipes.Add(new Pipe(players[1], ControllerButton.LeftShoulder, 
                GAMESPACE.Left + (pipeSprite.getTexture().Width * 1) + PADDING,
                GAMESPACE.Top
                )); // LB
            pipes.Add(new Pipe(players[2], ControllerButton.X,
                GAMESPACE.Left + (pipeSprite.getTexture().Width * 2) + PADDING,
                GAMESPACE.Top
                )); // X
            pipes.Add(new Pipe(players[2], ControllerButton.A,
                GAMESPACE.Left + (pipeSprite.getTexture().Width * 3) + PADDING,
                GAMESPACE.Top
                )); // A
            pipes.Add(new Pipe(players[3], ControllerButton.RightShoulder,
                GAMESPACE.Left + (pipeSprite.getTexture().Width * 4) + PADDING,
                GAMESPACE.Top
                )); // RB
            pipes.Add(new Pipe(players[3], ControllerButton.RightTrigger,
                GAMESPACE.Left + (pipeSprite.getTexture().Width * 5) + PADDING,
                GAMESPACE.Top
                )); // RT
            


        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            // Start paused and then begin
            if(frameCount >= 180)
            {
                foreach (Pipe pipe in pipes)
                {
                    pipe.setSpeed(2);
                }
            }

            // FIRING AND HITTING
            foreach (Pipe pipe in pipes)
            {

                if(pipe.getPlayer().GamePad.IsButtonPressed(pipe.getButton()))
                {
                    if(pipe.canDown())
                    {
                        pipe.moveHit(true, 10);
                        pipe.checkForHit();
                        pipe.canDown(false);
                    }
                }

                if (players[0].GamePad.IsButtonPressed(pipe.getButton()))
                {
                    if (pipe.canUp())
                    {
                        pipe.moveHit(false, -10);
                        pipe.checkForHit();
                        pipe.canUp(false);
                    }
                }

            }

            // RETRACTING
            foreach (Pipe pipe in pipes)
            {

                if (pipe.isFired(true))
                {
                    pipe.moveHit(true, -RETRACT_SPEED);
                }
                if (pipe.isFired(false))
                {
                    pipe.moveHit(false, RETRACT_SPEED);
                }

            }

            // SCORING
            foreach (Pipe pipe in pipes)
            {

                if (pipe.getlightBox().Intersects(pipe.getUpEndBox()))
                {
                    scores[0]++;
                }

                if (pipe.getlightBox().Intersects(pipe.getDownEndBox()))
                {

                    for (int x = 0; x < players.Length; x++)
                    {
                        if (players[x].Equals(pipe.getPlayer()))
                            scores[x]++;
                    }

                }

            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {

            foreach (Pipe pipe in pipes)
            {

                Camera.draw(sb, pipeSprite, pipe.getSrc());

                Camera.draw(sb, lightHitSprite, pipe.getUpHitBox(), Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
                Camera.draw(sb, lightHitSprite, pipe.getDownHitBox());

                Camera.draw(sb, lightSprite, pipe.getlightBox());

                Camera.draw(sb, pipeEndSprite, pipe.getUpEndBox(), Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
                Camera.draw(sb, pipeEndSprite, pipe.getDownEndBox());

            }

            for (int x = 0; x < players.Length; x++)
            {
                Camera.drawString(sb, myFont, ""+scores[x], new Vector2(GAMESPACE.Right, GAMESPACE.Bottom), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            }

        }

        public override void RunAI(Player p, Difficulty difficulty)
        {
        }

    }
}
