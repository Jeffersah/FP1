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
    class Sheet
    {

        struct Note
        {

            ControllerButton button;
            bool isMute;

            public Note(ControllerButton cb, bool mute)
            {
                button = cb;
                isMute = mute;
            }

        }

        Rectangle src;
        public bool isFinished;
        int currentNote;

        Note[] melody;

        public Sheet(int x, int y, int width, int height)
        {

            src = new Rectangle(x, y, width, height);
            isFinished = false;
            currentNote = 0;

            melody = new Note[6];

        }

        public Rectangle getSrc() { return src; }
        public int getCurrentNote() { return currentNote; }
        //public Note[] getMelody() { return melody; }

        public void advance(ControllerButton cb, bool isMute)
        {

            melody[currentNote] = new Note(cb, isMute);

            currentNote++;

            if (currentNote >= 6)
                isFinished = true;

        }

    }
}
