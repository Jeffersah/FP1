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
    class Tugger
    {

        Player player;
        Color color;
        Rectangle srcRec;

        public bool isActive;

        const int WIDTH = 186;
        const int HEIGHT = 213;

        public Tugger(Player p, int x, int y)
        {

            player = p;
            color = Color.White;

            srcRec = new Rectangle(x, y, WIDTH, HEIGHT);

            isActive = false;

        }

        public Player getPlayer() { return player; }
        public Color getColor() { return color; }
        public Rectangle getSrc() { return srcRec; }

        public void move(int offset)
        {
            srcRec.Offset(offset, 0);
        }

        public void activate()
        {
            isActive = true;
            color = Color.Red;
        }
        public void deactivate()
        {
            isActive = false;
            color = Color.White;
        }

    }
}
