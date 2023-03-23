using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceCleanerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GUI
{
    public class ScrollableBackground : AbstractControl
    {
        public List<AbstractControl> children = new List<AbstractControl>();
        private Matrix matrix = Matrix.Identity;
        private float scrollY = 0;
        public float maxScrollY = 2000;
        Vector2 mousePosition;
        Vector2? prevMousePosition;
        public void addControl(AbstractControl control)
        {
            children.Add(control);
        }

        public void removeControl(AbstractControl control)
        {
            children.Remove(control);
        }

        public override Boolean update()
        {
            Boolean ret = false;
            foreach (AbstractControl control in children)
            {
                if (control.update())
                    ret = true;
            }
            
            matrix = Matrix.CreateTranslation(0, scrollY, 0);
            
            if(!ret)
            {
                MouseState mouseState = Mouse.GetState();
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    mousePosition = new Vector2(mouseState.X, mouseState.Y);
                    if(prevMousePosition != null)
                    {
                        Vector2 delta = (Vector2)(mousePosition - prevMousePosition);
                        scrollY += delta.Y;
                        scrollY = Math.Max(Math.Min(scrollY, maxScrollY), 0);
                    }
                    prevMousePosition = mousePosition;
                } else
                {
                    prevMousePosition = null;
                }
            }
            
            return ret;
        }

        public override void drawControl(SpriteBatch spriteBatch)
        {
            Game1.Instance.endBatchDraw();
            Game1.Instance.startBatchDraw(matrix);
            foreach (AbstractControl control in children)
            {
                control.drawControl(spriteBatch);
            }
            Game1.Instance.endBatchDraw();
            Game1.Instance.startBatchDraw(Matrix.Identity);
        }
    }
}
