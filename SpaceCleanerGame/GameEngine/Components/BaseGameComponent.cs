using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public abstract class BaseGameComponent : GameComponent
    {
        public GameObject gameObject;
        public BaseGameComponent(GameObject parent) : base(null)
        {
            this.gameObject = parent;
        }
    }
}
