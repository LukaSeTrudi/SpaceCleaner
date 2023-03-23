using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.Interfaces
{
    internal interface IBatchDrawable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
