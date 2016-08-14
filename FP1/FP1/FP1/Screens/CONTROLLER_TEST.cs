using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NCodeRiddian;
using NCodeRiddian.Input;
using DataLoader;


namespace FP1.Screens
{
    class CONTROLLER_TEST : Screen
    {
        public override void Start()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime time, GameScreenManager Manager)
        {
            foreach (Player p in GameManager.Players)
            {
                p.GamePad.Update();
            }
            foreach (ControllerButton b in Enum.GetValues(typeof(ControllerButton)))
            {
                if (GameManager.Players[0].GamePad.IsButtonPressed(b))
                {
                    foreach (Player p in GameManager.Players)
                    {
                        if (p.IsComputer)
                        {
                            p.GamePad.GetSimState().SetButtonDown(b);
                        }
                    }
                }
                if (GameManager.Players[0].GamePad.IsButtonReleased(b))
                {
                    foreach (Player p in GameManager.Players)
                    {
                        if (p.IsComputer)
                        {
                            p.GamePad.GetSimState().SetButtonUp(b);
                        }
                    }
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
        }
    }
}
