
using System;
using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GUI
{
    public class ImageButton : AbstractControl
    {
        public Sprite imageSprite;
        public Sprite imageSpriteActive;

        public Boolean isActive = false;

        public Boolean isDown = false;
        public Boolean wasPressed = false;
        public Boolean wasReleased = false;
        public Boolean disabled = false;
        public float opacity = 1;

        public event EventHandler Click;

        public EventArgs eventArgs = EventArgs.Empty;
        
        public ImageButton(Sprite imageSprite, Sprite imageSpriteActive = null)
        {
            this.imageSprite = imageSprite;
            this.origin = imageSprite.sourceRect(0).Center.ToVector2();
            this.imageSpriteActive = imageSpriteActive;
        }

        

        public override Rectangle getBounds()
        {
            Vector2 size = Vector2.Multiply(this.imageSprite.size(), this.scale);
            return new Rectangle((int)(this.position.X - size.X / 2), (int)(this.position.Y - size.Y / 2), (int)size.X, (int)size.Y);
        }

        public override Boolean update()
        {
            if (disabled)
                return false;
            Boolean wasDown = isDown;

            isDown = false;

            MouseState mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePos = mouseState.Position.ToVector2();
                int pixelX = (int)((mousePos.X / Resolution.Width) * Resolution.VWidth);
                int pixelY = (int)((mousePos.Y / Resolution.Height) * Resolution.VHeight);

                Vector2 spriteSize = this.imageSprite.size();

                if (getBounds().Contains(pixelX, pixelY))
                {   
                    isDown = true;
                } else
                {
                    isActive = false;
                    return false;
                }
            }


            if(isDown && !wasDown)
            {
                isActive = true;
            } else if (!isDown && wasDown)
            {
                isActive = false;
                if(this.Click != null)
                    this.Click(this, eventArgs);
            }

            return wasPressed;
        }

        public override void drawControl(SpriteBatch spriteBatch)
        {
            Texture2D selectedTexture = this.imageSprite.texture;
            if (this.isActive && this.imageSpriteActive != null) selectedTexture = this.imageSpriteActive.texture;

            spriteBatch.Draw(
                texture: selectedTexture,
                position: position,
                sourceRectangle: null,
                color: Color.White * opacity,
                rotation: 0,
                origin: origin,
                scale: scale,
                effects: SpriteEffects.None,
                layerDepth: 1);
        }
    }
}
