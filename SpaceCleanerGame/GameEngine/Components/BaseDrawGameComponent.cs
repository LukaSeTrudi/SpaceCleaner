using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Components;
using GameEngine.Interfaces;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{

    public abstract class BaseDrawGameComponent : DrawableGameComponent, IBatchDrawable
    {

        public GameObject gameObject;

        public BaseDrawGameComponent(GameObject parent) : base(null)
        {
            this.gameObject = parent;
        }
        public BaseDrawGameComponent() : base(null)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual void LoadContent(ContentManager contentManager)
        {

        }
    }
}
