using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;

namespace SpaceCleaner.Resources.Scripts
{
    public class TestScript : Script
    {
        public TestScript(GameObject parent) : base(parent)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //gameObject.transform.position.X += 5;
            if (gameObject.transform.position.X >= 300)
            {
                //gameObject.transform.position.X = 0;
            }
        }
    }
}
