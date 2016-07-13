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
    class Pipe
    {

        const int PIPE_WIDTH = 100;
        const int PIPE_HEIGHT = 660;
        const int LIGHT_HEIGHT = 43;
        const int END_HEIGHT = 91;
        const int HIT_HEIGHT = 173;

        Player player;
        ControllerButton button;
        Rectangle srcRec;
        bool isGoingDown;
        bool canHitUp;
        bool canHitDown;
        float speed;

        Rectangle upHitBox;
        Rectangle downHitBox;
        Rectangle lightBox;
        Rectangle upEndBox;
        Rectangle downEndBox;

        public Pipe(Player p, ControllerButton cb, int x, int y)
        {

            player = p;
            button = cb;
            srcRec = new Rectangle(x, y, PIPE_WIDTH, PIPE_HEIGHT);
            
            isGoingDown = true;
            canHitUp = true;
            canHitDown = true;
            speed = 0;

            upHitBox = new Rectangle(
                x,
                y - LIGHT_HEIGHT,
                PIPE_WIDTH,
                LIGHT_HEIGHT
                );
            downHitBox = new Rectangle(
                x,
                srcRec.Bottom,
                PIPE_WIDTH,
                LIGHT_HEIGHT
                );
            lightBox = new Rectangle(
                x,
                y + (PIPE_HEIGHT / 2),
                PIPE_WIDTH,
                LIGHT_HEIGHT
                );
            upEndBox = new Rectangle(
                x,
                y,
                PIPE_WIDTH,
                END_HEIGHT
                );
            downEndBox = new Rectangle(
                x,
                srcRec.Bottom - (END_HEIGHT),
                PIPE_WIDTH,
                END_HEIGHT
                );

        }

        public void setlightPos(float newPos)
        {
            lightBox.Offset(0, lightBox.Y - (int)newPos);
        }
        public Rectangle getlightBox() { return lightBox; }
        public void moveLightBox()
        {
            if (isGoingDown)
                lightBox.Offset(0, (int)speed);
            else
                lightBox.Offset(0, -((int)speed));
        }

        public void setSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
        public float getSpeed() { return speed; }


        public void moveHit(bool isUpBox, int offset)
        {
            if (isUpBox)
                upHitBox.Offset(0, offset);
            else
                downHitBox.Offset(0, offset);
        }


        public Rectangle getSrc() { return srcRec; }
        public Rectangle getUpHitBox() { return upHitBox; }
        public Rectangle getDownHitBox() { return downHitBox; }
        public Rectangle getUpEndBox() { return upEndBox; }
        public Rectangle getDownEndBox() { return downEndBox; }
        public ControllerButton getButton() { return button; }
        public Player getPlayer() { return player; }
        public bool isFired(bool isUpHit)
        {

            if (isUpHit)
                return upHitBox.Bottom >= upEndBox.Bottom;

            return downHitBox.Top <= downEndBox.Top;

        }

        public bool canDown() { return canHitDown; }
        public void canDown(bool can)
        {
            canHitDown = can;
        }
        public bool canUp() { return canHitUp; }
        public void canUp(bool can)
        {
            canHitUp = can;
        }

        public void hitUpwards(int distance)
        {

            if (distance <= 30 && isGoingDown)
                setSpeed(4);
            else
                setSpeed(2);

            isGoingDown = false;
            canUp(true);

        }
        public void hitDownwards(int distance)
        {

            if (distance <= 30 && !isGoingDown)
                setSpeed(4);
            else
                setSpeed(2);

            isGoingDown = true;

        }

        public void checkForHit()
        {

            if (lightBox.Intersects(upHitBox))
            {
                if (canHitDown)
                {
                    hitDownwards(Math.Abs(lightBox.Top - upEndBox.Bottom));
                }
            }

        }

    }
}
