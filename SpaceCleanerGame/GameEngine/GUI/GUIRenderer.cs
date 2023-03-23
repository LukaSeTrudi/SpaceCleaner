using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI
{
    public class GUIRenderer
    {
        InterfaceScreen selectedScreen;
        List<InterfaceScreen> screens;

        public GUIRenderer(InterfaceScreen selectedScreen)
        {
            this.selectedScreen = selectedScreen;
        }

        public void changeScreen(InterfaceScreen screen)
        {
            selectedScreen = screen;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            selectedScreen.draw(spriteBatch);
        }
    }
}
