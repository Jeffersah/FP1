using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NCodeRiddian
{
    /// <summary>
    /// GameComponent that updates the Cursor and KeyboardInputManager classes
    /// </summary>
    public class InputComponent : GameComponent
    {
        public InputComponent(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Cursor.update();
            Cursor2.Update();
            KeyboardInputManager.update();
        }
    }
}
