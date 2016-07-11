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
    class Pipe
    {

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

        public Pipe(Player p, ControllerButton cb, Rectangle src, bool isDown, bool up, bool down, float s, Rectangle upBox, Rectangle downBox, Rectangle lBox, Rectangle uEndBox, Rectangle dEndBox)
        {
            player = p;
            button = cb;
            srcRec = src;
            isGoingDown = isDown;
            canHitUp = up;
            canHitDown = down;
            speed = s;

            upHitBox = upBox;
            downHitBox = downBox;
            lightBox = lBox;
            upEndBox = uEndBox;
            downEndBox = dEndBox;
        }

        public void setlightPos(float newPos)
        {

            lightBox.Offset(0, lightBox.Y - (int)newPos);

        }
        public Rectangle getlightBox() { return lightBox; }

        public void setSpeed(float newSpeed)
        {

            speed = newSpeed;

        }
        public float getSpeed() { return speed; }

        public ControllerButton getButton() { return button; }

        public Player getPlayer() { return player; }

        public bool canDown() { return canHitDown; }
        public bool canUp() { return canHitUp; }

    }
}
