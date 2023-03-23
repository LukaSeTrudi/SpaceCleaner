using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GUI
{
    public class FlowLayout : AbstractControl
    {
        public Boolean isVertical = false;
        public int spacing = 0;

        public List<AbstractControl> children = new List<AbstractControl>();

        public FlowLayout(Vector2 position, Boolean isVertical = false, int spacing = 0)
        {
            this.position = position;
            this.isVertical = isVertical;
            this.spacing = spacing;
        }
        
        public void addControl(AbstractControl control)
        {
            if(children.Count == 0)
            {
                control.position = position;
                children.Add(control);
                return;
            }
            AbstractControl lastControl = children.Last();
            Rectangle lastBounds = lastControl.getBounds();
            if(isVertical)
            {
                control.position = new Vector2(position.X, lastBounds.Bottom + spacing + control.getBounds().Height/2);
            }
            children.Add(control);
        }

        public void removeControl(AbstractControl control)
        {
            if(!children.Contains(control))
            {
                return;
            }

            int index = children.IndexOf(control);

            for(int i = index + 1; i < children.Count; i++)
            {
                Rectangle lastBounds = children[i - 1].getBounds();
                children[i].position = new Vector2(position.X, lastBounds.Bottom + spacing + control.getBounds().Height / 2);
            }

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
            foreach(AbstractControl control in children)
            {
                control.drawControl(spriteBatch);
            }    
        }



    }
}
