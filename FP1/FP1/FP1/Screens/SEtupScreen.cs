using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NCodeRiddian;


namespace FP1.Screens
{
    /// <summary>
    /// Screen for user options. How many points to win? Random or vote or choice for minigame selection? Ect...
    /// </summary>
    class SetupScreen : Screen
    {
        static int[] PTW = { 2, 3, 5, 10, 15, 20, 25, 30, 50 };
        int cptw;
        public static SpriteFont generic;
        GameSelectionMode SelectionMode;

        static Vector2 WinCountBox;
        static Vector2 MinigameTypeBox;

        static Image bg;

        public static void Load()
        {
            generic = TextureManager.getFont("GenericMenu");
            bg = new Image("Menus\\tmpSetup");
            WinCountBox = new Vector2(866, 154 + 25);
            MinigameTypeBox = new Vector2(866, 262 + 25);
        }
    

        public override void Start()
        {
            SelectionMode = GameSelectionMode.Vote;
            cptw = 3;
        }

        public override void Update(GameTime time, GameScreenManager Manager)
        {
            foreach (Player p in GameManager.Players)
                p.GamePad.Update();
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            Camera.draw(sb, bg, Settings.GP_R, Color.White);
            Camera.drawString(sb, generic, PTW[cptw] + "", WinCountBox, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
            Camera.drawString(sb, generic, Enum.GetName(typeof(GameSelectionMode), SelectionMode) + "", MinigameTypeBox, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
