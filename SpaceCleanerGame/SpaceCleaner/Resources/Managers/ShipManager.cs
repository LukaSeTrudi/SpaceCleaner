using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceCleaner.Resources.Managers
{
    public class PlayerShipLevel
    {
        public AnimatedSprite exhaust_slow;
        public AnimatedSprite exhaust_fast;
        public AnimatedSprite explosion;

    }
    public class PlayerShip
    {
        public Sprite icon;

        public List<PlayerShipLevel> ship_levels;
    }

    public class ShipManager
    {
        public static ShipManager Instance;

        public List<PlayerShip> ships;

        public ShipManager(ContentManager contentManager)
        {
            Instance = this;
            ships = new List<PlayerShip>()
            {
                new PlayerShip()
                {
                    icon = new Sprite(contentManager.Load<Texture2D>("ships/ship_1/ship")),
                    ship_levels = new List<PlayerShipLevel>()
                    {
                        new PlayerShipLevel()
                        {
                            exhaust_slow = new AnimatedSprite(contentManager.Load<Texture2D>("ships/ship_1/level_1/exhaust_slow"), 1063, 1192, 10),
                            exhaust_fast = new AnimatedSprite(contentManager.Load<Texture2D>("ships/ship_1/level_1/exhaust_fast"), 1063, 1192, 10),
                            explosion = new AnimatedSprite(contentManager.Load<Texture2D>("ships/ship_1/level_1/explosion"), 1728, 942, 9),
                        }
                    }

                }
            };
        }
    }
}
