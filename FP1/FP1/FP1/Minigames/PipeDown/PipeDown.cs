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
    class PipeDown : Minigame
    {
        /// Pipe structure used to hold data associated with each of the six pipes
        ///
        public struct Pipe
        {
            Player player;
            ControllerButton button;
            float xCoord;
            float yCoord;
            float lightPos;
            bool isGoingDown;
            bool canHitUp;
            bool canHitDown;
            float speed;

            Rectangle upHitBox;
            Rectangle downHitBox;
            Rectangle lightBox;
            Rectangle upEndBox;
            Rectangle downEndBox;

            public Pipe(Player p, ControllerButton cb, float x, float y, float lp, bool isDown, bool up, bool down, float s, Rectangle upBox, Rectangle downBox, Rectangle lBox, Rectangle uEndBox, Rectangle dEndBox)
            {
                player = p;
                button = cb;
                xCoord = x;
                yCoord = y;
                lightPos = lp;
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
        }

        Rectangle gameSpace;
        List<Pipe> pipes;

        Image pipeSprite;
        Image pipeEndSprite;
        Image lightSprite;
        Image lightHitSprite;
        SpriteFont myFont;

        int frameCount;

        public TestMinigame : base("Pipe Down", "Flash 'Em!"){ }

        public override void Start(Player[] InGame)
        {

            //gameSpace = new Rectangle
            pipes = new List<Pipe>();
            //pipes.Add( new Pipe(InGame[1], ) );
            pipes.Add( new Pipe(InGame[2], ControllerButton.LeftShoulder, ) )

        }

        public override void Load(ContentManager cm)
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
