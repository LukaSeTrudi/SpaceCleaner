using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GameEngine.Graphics;
using GameEngine.Scenes;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class SpriteRenderer : BaseDrawGameComponent
    {
        public Sprite sprite;
        public Color color = Color.White;
        public Boolean flipX = false;
        public Boolean flipY = false;

        // Used with animator
        public int animationIndex = 0;

        public SpriteRenderer(GameObject parent, Sprite sprite) : base(parent)
        {
            this.sprite = sprite;
        }

        public Rectangle globalBounds()
        {
            if (this.sprite == null) return Rectangle.Empty;

            Vector2 position = gameObject.getWorldPosition();
            Vector2 assetSize = this.sprite.size();

            Vector2 scaledSize = Vector2.Multiply(gameObject.getWorldScale(), assetSize);

            return new Rectangle((int)(position.X - (scaledSize.X / 2)), (int)(position.Y - (scaledSize.Y / 2)), (int)scaledSize.X, (int)scaledSize.Y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(sprite == null)
                return;
            Vector2 spritePos = globalBounds().Center.ToVector2();
            
            Rectangle sourceRect = sprite.sourceRect(animationIndex);

            Vector2 origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

            spriteBatch.Draw(
                texture: sprite.texture,
                position: spritePos,
                sourceRectangle: sourceRect,
                color: color,
                origin: origin,
                rotation: gameObject.getWorldRotation(),
                scale: gameObject.getWorldScale() * gameObject.zoomedSprite,
                effects: SpriteEffects.None,
                layerDepth: 1);
        }
    }
}
