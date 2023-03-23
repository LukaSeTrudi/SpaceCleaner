using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GUI
{
    public class AbsoluteLayout : AbstractControl
    {
        public List<AbstractControl> children = new List<AbstractControl>();

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
            return ret;
        }

        public override void drawControl(SpriteBatch spriteBatch)
        {
            foreach (AbstractControl control in children)
            {
                control.drawControl(spriteBatch);
            }
        }
    }
}
