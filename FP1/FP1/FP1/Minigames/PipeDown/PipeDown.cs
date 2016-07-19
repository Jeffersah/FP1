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
        const int PADDING = 100;
        List<Pipe> pipes;
        Player[] players;

        Image pipeSprite;
        Image pipeEndSprite;
        Image lightSprite;
        Image lightHitSprite;
        Image background;
        SpriteFont myFont;

        const int RETRACT_SPEED = 2;

        int[] scores = new int[]{0, 0, 0, 0};

        public PipeDown() : base("Pipe Down", "Flash 'Em!"){ }

        public override void Load(ContentManager cm)
        {

            pipeSprite = new Image("Minigames\\PipeDown\\pipe");
            pipeEndSprite = new Image("Minigames\\PipeDown\\pipeEnd");
            lightSprite = new Image("Minigames\\PipeDown\\light");
            lightHitSprite = new Image("Minigames\\PipeDown\\lightHit");
            background = new Image("Minigames\\PipeDown\\pipedownBackground");

            myFont = TextureManager.getFont("Minigames\\PipeDown\\myfont");

        }

        public override void Start(Player[] InGame)
        {

            players = InGame;
            pipes = new List<Pipe>();

            pipes.Add(new Pipe(players[1], ControllerButton.LeftTrigger, GAMESPACE.Left, GAMESPACE.Top)); // LT
            pipes.Add(new Pipe(players[1], ControllerButton.LeftShoulder,
                GAMESPACE.Left + ((pipeSprite.getTexture().Width + PADDING) * 1),
                GAMESPACE.Top
                )); // LB
            pipes.Add(new Pipe(players[2], ControllerButton.X,
                GAMESPACE.Left + ((pipeSprite.getTexture().Width + PADDING) * 2),
                GAMESPACE.Top
                )); // X
            pipes.Add(new Pipe(players[2], ControllerButton.A,
                GAMESPACE.Left + ((pipeSprite.getTexture().Width + PADDING) * 3),
                GAMESPACE.Top
                )); // A
            pipes.Add(new Pipe(players[3], ControllerButton.RightShoulder,
                GAMESPACE.Left + ((pipeSprite.getTexture().Width + PADDING) * 4),
                GAMESPACE.Top
                )); // RB
            pipes.Add(new Pipe(players[3], ControllerButton.RightTrigger,
                GAMESPACE.Left + ((pipeSprite.getTexture().Width + PADDING) * 5),
                GAMESPACE.Top
                )); // RT

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            // Start paused and then begin
            
            foreach (Pipe pipe in pipes)
            {

                if (pipe.fc() == 120)
                    pipe.setSpeed(2);

            }

            foreach (Pipe pipe in pipes)
            {
                pipe.moveLightBox();
            }

            // FIRING AND HITTING
            foreach (Pipe pipe in pipes)
            {

                if(pipe.getPlayer().GamePad.IsButtonPressed(pipe.getButton()))
                {
                    if(pipe.canDown())
                    {
                        pipe.moveHit(true, lightHitSprite.getTexture().Height);
                        pipe.checkForHit();
                        pipe.canDown(false);
                    }
                }

                // DUBUG ONLY
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (pipe.canDown())
                    {
                        pipe.moveHit(true, lightHitSprite.getTexture().Height);
                        pipe.checkForHit();
                        pipe.canDown(false);
                    }
                }

                if (players[0].GamePad.IsButtonPressed(pipe.getButton()))
                {
                    if (pipe.canUp())
                    {
                        pipe.moveHit(false, -(lightHitSprite.getTexture().Height));
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
                else
                {
                    pipe.canDown(true);
                }
                if (pipe.isFired(false))
                {
                    pipe.moveHit(false, RETRACT_SPEED);
                }
                else
                {
                    pipe.canUp(true);
                }

            }
            
            // SCORING
            foreach (Pipe pipe in pipes)
            {

                if (pipe.getlightBox().Intersects(pipe.getUpEndBox()))
                {
                    scores[0]++;
                    pipe.reset();
                }

                if (pipe.getlightBox().Intersects(pipe.getDownEndBox()))
                {

                    for (int x = 0; x < players.Length; x++)
                    {
                        if (players[x].Equals(pipe.getPlayer()))
                            scores[x]++;
                    }
                    pipe.reset();

                }

            }
            
            // INCREMENT FRAME COUNTS
            foreach (Pipe pipe in pipes)
            {
                pipe.incrementFC();
            }
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {

            // Background
            Camera.draw(sb, background, new Rectangle(0, 0, background.getTexture().Width, background.getTexture().Height));
            
            foreach (Pipe pipe in pipes)
            {

                Camera.draw(sb, pipeSprite, pipe.getSrc());

                Camera.draw(sb, lightHitSprite, pipe.getUpHitBox(), Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
                Camera.draw(sb, lightHitSprite, pipe.getDownHitBox());

                Camera.draw(sb, lightSprite, pipe.getlightBox());

                Camera.draw(sb, pipeEndSprite, pipe.getUpEndBox(), Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 1);
                Camera.draw(sb, pipeEndSprite, pipe.getDownEndBox());

            }

            Camera.drawString(sb, myFont, "P1: " + scores[0], new Vector2((GAMESPACE.Right - 80), (GAMESPACE.Bottom - 200)), Color.Orange, 0, Vector2.Zero, SpriteEffects.None, 1);
            for (int x = 1; x < players.Length; x++)
            {
                Camera.drawString(sb, myFont, "P"+(x+1)+": "+scores[x], new Vector2((GAMESPACE.Right - 80), (GAMESPACE.Bottom - 200) + (50*x)), Color.Black, 0, Vector2.Zero, SpriteEffects.None, 1);
            }

        }

        public override void RunAI(Player p, Difficulty difficulty)
        {

            foreach (Pipe pipe in pipes)
            {

                if (!p.isP1)
                {
                    if (pipe.getPlayer().Equals(p))
                    {
                        switch (difficulty)
                        {

                            case Difficulty.SEasy:    // Super Easy
                                break;

                            case Difficulty.Easy:      // Easy
                                break;

                            case Difficulty.Medium:    // Medium
                                break;

                            case Difficulty.Hard:      // Hard
                                break;

                            case Difficulty.SHard:     // Super Hard
                                break;

                        }
                    }
                }
                else
                {

                    switch (difficulty)
                    {

                        case Difficulty.SEasy:    // Super Easy
                            break;

                        case Difficulty.Easy:      // Easy
                            break;

                        case Difficulty.Medium:    // Medium
                            break;

                        case Difficulty.Hard:      // Hard
                            break;

                        case Difficulty.SHard:     // Super Hard
                            break;

                    }

                }

            }

        }

    }
}
