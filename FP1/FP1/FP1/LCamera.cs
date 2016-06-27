using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FP1
{
    class LCamera
    {
        RenderTarget2D PersonalTarget;
        Rectangle ScreenTarget;
        SpriteBatch Sprite;
        GraphicsDevice gm;

        public SpriteBatch spriteBatch { get { return Sprite; } }

        public LCamera(GraphicsDevice gd, SpriteBatch batch, Rectangle STgt, Point ptgtSize)
        {
            PersonalTarget = new RenderTarget2D(gd, ptgtSize.X, ptgtSize.Y);
            ScreenTarget = STgt;
            gm = gd;
            Sprite = batch;
        }
        public void Begin()
        {
            gm.SetRenderTarget(PersonalTarget);
            gm.Clear(Color.Black);
            Sprite.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }

        public void End(Color clearColor)
        {
            Sprite.End();
            gm.SetRenderTarget(null);
            gm.Clear(clearColor);
            Sprite.Begin();
            Sprite.Draw(PersonalTarget, ScreenTarget, Color.White);
            Sprite.End();
        }

        public void End()
        {
            End(Color.Black);
        }
    }
}
