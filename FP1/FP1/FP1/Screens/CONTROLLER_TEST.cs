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
        SpriteFont font;
        GamePadStateManager pad;
        public override void Start()
        {
            font = TextureManager.getFont("smallfont");
            pad = new ActiveController(PlayerIndex.One);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime time, GameScreenManager Manager)
        {
            pad.Update();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            int i = 0;
            foreach(ControllerButton button in Enum.GetValues(typeof(ControllerButton)).Cast<ControllerButton>())
            {
                Camera.drawString(sb, font, Enum.GetName(typeof(ControllerButton), button), new Vector2(10, font.MeasureString("HI").Y * i), pad.IsButtonDown(button) ? Color.Green : Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
                i++;    
            }
        }
    }
}
