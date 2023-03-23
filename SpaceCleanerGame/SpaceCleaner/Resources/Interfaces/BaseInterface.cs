using GameEngine.GUI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Interfaces
{
    public interface BaseInterface
    {

        public static InterfaceScreen screen = new InterfaceScreen();

        public static void init(ContentManager contentManager)
        {
            
        }

        public static Boolean update()
        {
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);
        }
    }
}
