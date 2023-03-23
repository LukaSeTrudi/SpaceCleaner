using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI
{
    public class InterfaceScreen
    {
        public List<AbstractControl> controls = new List<AbstractControl>();

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (AbstractControl control in controls)
            {
                control.drawControl(spriteBatch);
            }
        }

        public Boolean checkForInputs()
        {
            Boolean something = false;
            foreach (AbstractControl control in controls)
            {
                if(control.update())
                {
                    something = true;
                };
            }
            return something;
        }
    }
}
