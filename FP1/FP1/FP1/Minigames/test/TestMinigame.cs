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

namespace FP1.Minigames.test
{
    class TestMinigame : Minigame
    {

        Image pipeSprite;
        Image pipeEndSprite;
        Image lightSprite;
        Image lightHitSprite;
        SpriteFont myFont;

        const float PIPE_POS_X = 300;
        const float PIPE_POS_Y = 50;

        float lightHit1Pos;
        float lightHit2Pos;

        Rectangle lightBox;
        Rectangle lightHitBox1;
        Rectangle lightHitBox2;
        Rectangle pipeEndBox1;
        Rectangle pipeEndBox2;

        bool lightGoingDown;
        bool canHitUp;
        bool canHitDown;
        int p1score;
        int p2score;

        float lightLocation;

        int lightSpeed;
        int frameCount;

        Player[] players;

        public override void Start(Player[] InGame)
        {
            players = InGame;
            lightGoingDown = true;
            canHitUp = true;
            canHitDown = true;
            p1score = 0;
            p2score = 0;
            lightSpeed = 0;
            frameCount = 0;

            base.Start(InGame);
        }
        public override void Load(ContentManager cm)
        {

            pipeSprite = new Image("Minigame\\test\\pipe");
            pipeEndSprite = new Image("Minigame\\test\\pipeEnd");
            lightSprite = new Image("Minigame\\test\\light");
            lightHitSprite = new Image("Minigame\\test\\lightHit");
            myFont = TextureManager.getFont("Minigame\\test\\myFont");

            lightHit1Pos = pipeSprite.getTexture().Height + PIPE_POS_Y + 1;
            lightHit2Pos = PIPE_POS_Y - lightHitSprite.getTexture().Height - 1;

            lightLocation = (pipeSprite.getTexture().Height / 2) + PIPE_POS_Y;
            lightBox = new Rectangle((int)PIPE_POS_X, (int)lightLocation, lightSprite.getTexture().Width, lightSprite.getTexture().Height);
            lightHitBox1 = new Rectangle((int)PIPE_POS_X, (int)(lightHit1Pos), lightHitSprite.getTexture().Width, lightHitSprite.getTexture().Height);
            lightHitBox2 = new Rectangle((int)PIPE_POS_X, (int)(lightHit2Pos), lightHitSprite.getTexture().Width, lightHitSprite.getTexture().Height);
            pipeEndBox1 = new Rectangle((int)PIPE_POS_X, (int)(pipeSprite.getTexture().Height + PIPE_POS_Y), pipeEndSprite.getTexture().Width, pipeEndSprite.getTexture().Height);
            pipeEndBox2 = new Rectangle((int)PIPE_POS_X, (int)(PIPE_POS_Y - pipeEndSprite.getTexture().Height), pipeEndSprite.getTexture().Width, pipeEndSprite.getTexture().Height);

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            if (frameCount == 120)
                lightSpeed = 2;

            // PLAYER ONE
            if (lightHitBox1.Y < lightHit1Pos)
            {

                lightHitBox1.Offset(0, 1);

            }
            else if (!players[0].GamePad.IsButtonPressed(ControllerButton.A))
                canHitUp = true;
            if (players[0].GamePad.IsButtonPressed(ControllerButton.A))
            {

                if (canHitUp)
                {

                    lightHitBox1.Offset(0, -(lightHitSprite.getTexture().Height + 1));

                }
                canHitUp = false;
                
            }
            if (lightHitBox1.Intersects(lightBox))
            {

                lightGoingDown = false;

                if (lightBox.Bottom >= pipeEndBox1.Top - 10)
                    lightSpeed = 6;
                else
                    lightSpeed = 2;

            }


            // OTHER PLALYERS
            if (lightHitBox2.Y > lightHit2Pos)
            {

                lightHitBox2.Offset(0, -1);

            }
            else
                canHitDown = true;
            if (/*players[1].GamePad.IsButtonPressed(ControllerButton.A)*/Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (canHitDown)
                {

                    lightHitBox2.Offset(0, (lightHitSprite.Height + 1));

                }
                canHitDown = false;

            }
            if (lightHitBox2.Intersects(lightBox))
            {

                lightGoingDown = true;

                if (lightBox.Top <= pipeEndBox2.Bottom + 10)
                {
                    lightSpeed = 6;
                }
                else
                    lightSpeed = 2;

            }


            //LIGHT
            if (lightGoingDown)
                lightBox.Offset(0, lightSpeed);
            else
                lightBox.Offset(0, -lightSpeed);


            if (lightBox.Intersects(pipeEndBox1))
            {

                p2score++;
                lightBox.Offset(0, -(pipeSprite.getTexture().Height / 2));

            }
            if (lightBox.Intersects(pipeEndBox2))
            {

                p1score++;
                lightBox.Offset(0, (pipeSprite.getTexture().Height - (int)PIPE_POS_Y) / 2);

            }

            frameCount++;

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            
            Camera.draw(sb, pipeSprite, new Rectangle((int)PIPE_POS_X, (int)PIPE_POS_Y, pipeSprite.getTexture().Width, pipeSprite.getTexture().Height));
            Camera.draw(sb, lightSprite, lightBox);

            if (!canHitUp)
                Camera.draw(sb, lightHitSprite, lightHitBox1);
            if (!canHitDown)
                Camera.draw(sb, lightHitSprite, lightHitBox2, Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);

            Camera.draw(sb, pipeEndSprite, pipeEndBox2);
            Camera.draw(sb, pipeEndSprite, pipeEndBox1, Color.White, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);

        }
    }
}
