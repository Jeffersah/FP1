using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NCodeRiddian;

namespace FP1.Screens
{
    class MainMenu : Screen
    {
        public static Color[] PCLR = { Color.Red, Color.Yellow, Color.Orange, Color.Purple, Color.Blue, Color.Teal, Color.ForestGreen, Color.LawnGreen};

        public static SpriteFont generic;

        TemporaryPlayer[] Players;
        Rectangle[] PlayerAreas;

        public static void Load()
        {
            generic = TextureManager.getFont("GenericMenu");
        }
    
        public override void Start()
        {
            Players = new TemporaryPlayer[4];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new TemporaryPlayer("COM " + (i + 1), Difficulty.Medium, (Microsoft.Xna.Framework.PlayerIndex)i);
                Players[i].PlayerColor = Color.Gray;
            }
            PlayerAreas = new Rectangle[4];
            int AreaOver5 = Settings.GP_X / 5;
            for (int i = 0; i < PlayerAreas.Length; i++)
            {
                PlayerAreas[i] = new Rectangle((AreaOver5 / 5) + ((AreaOver5 + (AreaOver5 / 5)) * i), 100, AreaOver5, 700);
            }
        }

        public override void Update(GameTime time, GameScreenManager Manager)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].IsComputer)
                {
                    if (GamePad.GetState((PlayerIndex)i).IsButtonDown(Buttons.Start))
                    {
                        Players[i] = new TemporaryPlayer("", Difficulty.NON_COMP, (PlayerIndex)i);
                        int color = 0;
                        for (int j = 0; j < Players.Length; j++ )
                        {
                            if (Players[j].PlayerColor == PCLR[color])
                            {
                                color++;
                                j = -1;
                            }
                        }
                        Players[i].PlayerColor = PCLR[color];
                        
                    }
                }
                else
                {
                    Players[i].MenuUpdate(this);
                }
            }
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            Camera.drawGeneric(sb, new Rectangle(0, 0, 1600, 900), Color.Teal);
            Camera.drawString(sb, generic, "F*CK PLAYER ONE TMP MAIN MENU", new Vector2(0, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            for (int i = 0; i < 4; i++)
            {
                Players[i].MenuDraw(sb, PlayerAreas[i], this);
            }
        }
    }
}
