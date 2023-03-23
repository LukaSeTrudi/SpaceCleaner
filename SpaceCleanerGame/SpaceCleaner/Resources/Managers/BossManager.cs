using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceCleaner.Resources.Managers
{
    public class Boss
    {
        public AnimatedSprite attack1;
        public AnimatedSprite attack2;
        public AnimatedSprite attack3;
        public AnimatedSprite attack4;

        public AnimatedSprite death;
        public AnimatedSprite explosion;

        public AnimatedSprite flight;

        public AnimatedSprite idle1;
        public AnimatedSprite idle2;

        public AnimatedSprite laser;
        public AnimatedSprite missile;
        public AnimatedSprite shot_start;
        public AnimatedSprite shot_end;


    }
    public class BossManager
    {
        public static BossManager Instance;
        public List<Boss> bosses;

        public BossManager(ContentManager contentManager)
        {
            Instance = this;
            bosses = new List<Boss>()
            {
                new Boss()
                {
                    attack1 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/attack1"), 1080, 639, 10),
                    attack2 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/attack2"), 1080, 637, 10),
                    attack3 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/attack3"), 1080, 702, 10),
                    attack4 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/attack4"), 1080, 677, 10),
                    death = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/death"), 1080, 686, 10),
                    explosion = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/explosion"), 620, 620, 8),
                    flight = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/flight1"), 1080, 755, 10),
                    idle1 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/idle1"), 1080, 646, 10),
                    idle2 = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/idle2"), 1080, 755, 10),
                    laser = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/laser"), 342, 613, 10),
                    missile = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/missile"), 167, 477, 10),
                    shot_start = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/shot"), 256, 643, 4),
                    shot_end = new AnimatedSprite(contentManager.Load<Texture2D>("bosses/1/shot_hit"), 256, 643, 5),

                    
                }
            };
        }
    }
}
