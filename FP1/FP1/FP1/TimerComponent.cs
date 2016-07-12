using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace FP1
{
    class TimerComponent:GameComponent
    {
        public TimerComponent(Game g) : base(g) { }
        public override void Update(GameTime gameTime)
        {
            for (int i = GameTimer.StaticTimers.Count - 1; i >= 0; i--)
            {
                GameTimer.StaticTimers[i].Update(gameTime);
            }
        }
    }
}
