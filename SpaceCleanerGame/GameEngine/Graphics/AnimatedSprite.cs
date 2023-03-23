using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public class AnimatedSprite : Sprite
    {
        int spriteWidth;
        int spriteHeight; 
        public int spriteCount;
        private Boolean isVertical;

        public AnimatedSprite(Texture2D texture):base(texture)
        {
            this.spriteWidth = texture.Width;
            this.spriteHeight = texture.Height;
            this.spriteCount = 1;
            this.isVertical = false;
        }
        public AnimatedSprite(Texture2D texture, int spriteWidth, int spriteHeight, int spriteCount, Boolean isVertical = false) : base(texture)
        {
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.spriteCount = spriteCount;
            this.isVertical = isVertical;
        }

        public override Vector2 size()
        {
            return new Vector2(spriteWidth, spriteHeight);
        }

        public override Rectangle sourceRect(int index)
        {
            if(this.isVertical)
            {
                return new Rectangle(0, this.spriteHeight * index, this.spriteWidth, this.spriteHeight);
            }
            return new Rectangle(this.spriteWidth * index, 0, this.spriteWidth, this.spriteHeight);
        }
    }
}
