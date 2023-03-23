using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.Utils;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI
{
    public class Image : AbstractControl
    {

        public Sprite imageSprite;
        public float opacity = 1f;
        public float rotation = 0;

        public Image(Sprite imageSprite)
        {
            this.imageSprite = imageSprite;
            this.origin = imageSprite.sourceRect(0).Center.ToVector2();
            this.setBottom(0);
        }
        public override Rectangle getBounds()
        {

            Vector2 size = Vector2.Multiply(this.imageSprite.size(), this.scale);
            if(this.origin == Vector2.Zero)
            {
                return new Rectangle((int)(this.position.X), (int)(this.position.Y), (int)size.X, (int)size.Y);
            }
            return new Rectangle((int)(this.position.X - size.X / 2), (int)(this.position.Y - size.Y / 2), (int)size.X, (int)size.Y);
        }

        public override void drawControl(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(
                texture: this.imageSprite.texture,
                position: position,
                sourceRectangle: null,
                color: Color.White * opacity,
                rotation: rotation,
                origin: origin,
                scale: scale,
                effects: SpriteEffects.None,
                layerDepth: 1);

        }

        public void setFullscreen()
        {
            position.X = Resolution.VWidth / 2;
            position.Y = Resolution.VHeight / 2;

            scale.X = Resolution.VWidth / imageSprite.size().X;
            scale.Y = Resolution.VHeight / imageSprite.size().Y;
            
        }
        
        public void setFullWidth()
        {
            position.X = Resolution.VWidth / 2;
            position.Y = Resolution.VHeight / 2;

            scale.X = Resolution.VWidth / imageSprite.size().X;
            scale.Y = scale.X;
        }
        public void setBottom(int ydist)
        {
            Vector2 imageSize = Vector2.Multiply(this.scale, this.imageSprite.size());

            this.position.X = Resolution.VWidth / 2;
            this.position.Y = Resolution.VHeight - (ydist + imageSize.Y / 2);

        }
    }
}
