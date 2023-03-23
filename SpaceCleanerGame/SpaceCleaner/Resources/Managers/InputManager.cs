using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceCleanerGame.SpaceCleaner.Resources.Managers
{
    public static class InputManager
    {
        public static Boolean dragging = false;
        public static Vector2 lastDragDistance;
        public static Vector2 lastDragDelta;

        private static Vector2 toWorldPosition(Vector2 mousePos)
        {
            int pixelX = (int)((mousePos.X / Resolution.Width) * Resolution.VWidth);
            int pixelY = (int)((mousePos.Y / Resolution.Height) * Resolution.VHeight);
            return new Vector2(pixelX, pixelY);
        }

        public static void Update(GameTime gameTime)
        {
          
            
            MouseState mouseState = Mouse.GetState();

            if(mouseState.LeftButton != ButtonState.Pressed)
            {
                dragging = false;
            } else if(!dragging)
            {
                dragging = true;
                lastDragDistance = toWorldPosition(mouseState.Position.ToVector2());
            } else if(mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 newPos = toWorldPosition(mouseState.Position.ToVector2());
                if(newPos.X >= 0 && newPos.X <= Resolution.VWidth && newPos.Y >= 0 && newPos.Y <= Resolution.VHeight)
                {
                    lastDragDelta = Vector2.Subtract(newPos, lastDragDistance);
                    lastDragDistance = newPos;
                } else
                {
                    dragging = false;
                }
            }
        }

    }
}
