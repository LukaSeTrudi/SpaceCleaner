using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public abstract class Script : BaseGameComponent
    {
        public Script(GameObject parent) : base(parent)
        {
        }
    }
}
