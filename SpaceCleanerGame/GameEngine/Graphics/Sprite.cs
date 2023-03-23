using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public class Sprite
    {
        public Texture2D texture;
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual Rectangle sourceRect(int index)
        {
            return new Rectangle(Point.Zero, new Point(this.texture.Width, this.texture.Height));
        }

        public virtual Vector2 size()
        {
            return new Vector2(this.texture.Width, this.texture.Height);
        }
    }
}
