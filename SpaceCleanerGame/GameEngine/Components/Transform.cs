using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class Transform : GameComponent
    {
        public Vector2 position = new Vector2(0, 0);
        public float rotation = 0;
        public Vector2 scale = new Vector2(1, 1);

        public Transform() : base(null)
        {
        }
        public Transform(Vector2 pos, float rot, Vector2 scale) : base(null)
        {
            this.position = pos;
            this.rotation = rot;
            this.scale = scale;
        }
    }
}
