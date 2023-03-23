using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameEngine.GUI
{
    public abstract class AbstractControl
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 origin = Vector2.Zero;
        public Vector2 scale = new Vector2(2, 2);

        public virtual void drawControl(SpriteBatch spriteBatch)
        {
        }

        public virtual Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }

        public virtual Boolean update()
        {
            return false;
        }

        public void setTopLeft(int top, int left)
        {
            position.X = left;
            position.Y = top;
        }

        public void setTopRight(int top, int right)
        {
            position.X = Resolution.VWidth - right;
            position.Y = top;
        }

        public void setBottomLeft(int bottom, int left)
        {
            position.X = left;
            position.Y = Resolution.VHeight - bottom;
        }
        public void setBottomRight(int bottom, int right)
        {
            position.X = Resolution.VWidth - right;
            position.Y = Resolution.VHeight - bottom;
        }

        public void setTop(int top)
        {
            position.Y = top;
        }
        public void setBottom(int bottom)
        {
            position.Y = Resolution.VHeight - bottom;
        }
        public void setCenter()
        {
            position.X = Resolution.VWidth / 2;
            position.Y = Resolution.VHeight / 2;
        }
  
    }
}
