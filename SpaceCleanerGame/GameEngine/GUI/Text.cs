using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI
{
    public class Text : AbstractControl
    {
        public static SpriteFont defaultFont;
        public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 };

        public Alignment alignment = Alignment.Center;
        public float opacity = 1.0f;
        public String text
        {
            get { return _text; }
            set { _text = value; calculateOrigin(); }
        }
        private String _text;
        public Text()
        {
            this._text = "";
            this.calculateOrigin();
        }

        public override Rectangle getBounds()
        {
            throw new NotImplementedException();
        }

        public void calculateOrigin()
        {
            Vector2 size = defaultFont.MeasureString(_text);
            origin = size * 0.5f;

            switch(alignment)
            {
                case Alignment.Left:
                    origin.X = 0;
                    break;
                case Alignment.Right:
                    origin.X = size.X;
                    break;
                case Alignment.Bottom:
                    origin.Y = 0;
                    break;
                case Alignment.Top:
                    origin.Y = size.Y;
                    break;
            }


        }

        public override void drawControl(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                spriteFont: defaultFont,
                position: position,
                text: this._text,
                color: Color.White * opacity,
                rotation: 0,
                origin: origin,
                scale: scale,
                effects: SpriteEffects.None,
                layerDepth: 1);
        }
    }
}
