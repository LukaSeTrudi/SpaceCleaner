using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceCleaner.Resources.Managers
{
    public class Missile
    {
        public int level;
        public AnimatedSprite flying;
        public AnimatedSprite explosion;
    }
    public class MissileManager
    {
        public static MissileManager Instance;

        public List<Missile> missiles;

        public MissileManager(ContentManager contentManager)
        {
            Instance = this;
            missiles = new List<Missile>()
            {
                new Missile()
                {
                    level = 0,
                    flying = new AnimatedSprite(contentManager.Load<Texture2D>("ammo/Fire_Shot_4_2")),
                    explosion = new AnimatedSprite(contentManager.Load<Texture2D>("missiles/missile_1_explosion"), 810, 810, 9),
                }
            };
        }

    }
}
