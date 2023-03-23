using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleanerGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameEngine.GUI
{
    public class Layer
    {
        public Texture2D image;
        public float speed;

        private Vector2 position1;
        private Vector2 position2;
        private Vector2 origin;
        private Vector2 scale;
        private Boolean flipFirst = false;
        private Boolean flip = false;
        public Layer(Texture2D image, float speed, Boolean flip = false)
        {
            this.image = image;
            this.speed = speed;
            this.position1 = Game1.Instance.screen.Center.ToVector2();
            this.position2 = this.position1;
            this.origin = new Vector2(this.image.Width / 2, this.image.Height / 2);
            this.scale = new Vector2(getScale());
            this.flip = flip;
        }

        public float getScale()
        {
            return Game1.Instance.screen.Width / this.image.Width;
        }

        public void update()
        {
            if(this.position1.Y >= Game1.Instance.screen.Height + (this.image.Height * this.scale.Y)/2)
            {
                this.position1.Y = this.position2.Y;
                flipFirst = !flipFirst;
            }
            this.position1.Y += this.speed * 10;
            this.position2.Y = this.position1.Y - this.image.Height * this.scale.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            SpriteEffects first = SpriteEffects.None;
            SpriteEffects second = SpriteEffects.None;

            if (flip && flipFirst) first = SpriteEffects.FlipHorizontally;
            else if (flip) second = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(
                texture: this.image,
                position: this.position1,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0,
                origin: this.origin,
                scale: this.scale,
                effects: first,
                layerDepth: 1);
            spriteBatch.Draw(
                texture: this.image,
                position: this.position2,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0,
                origin: this.origin,
                scale: this.scale,
                effects: second,
                layerDepth: 1);

        }
    }
    public class ParallaxImage : AbstractControl
    {
        public List<Layer> layers = new List<Layer>();
        public ParallaxImage() { 
        }

        public void addLayer(Texture2D image, float speed, Boolean flip=false)
        {
            layers.Add(new Layer(image, speed, flip));
        }

        public override bool update()
        {
            foreach(Layer layer in layers)
            {
                layer.update();
            }
            return false;
        }

        public override void drawControl(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in layers)
            {
                layer.draw(spriteBatch);
            }
        }
    }
}
