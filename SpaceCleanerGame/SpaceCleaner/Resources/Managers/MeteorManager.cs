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
    public class Meteor
    {
        public Sprite sprite;
        public float multiplier;
    }
    public class MeteorManager
    {
        public static MeteorManager Instance;

        public List<Meteor> meteors;

        public MeteorManager(ContentManager contentManager)
        {
            Instance = this;
            meteors = new List<Meteor>()
            {
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_01")),
                    multiplier = 1,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_02")),
                    multiplier = 1.1f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_03")),
                    multiplier = 1.2f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_04")),
                    multiplier = 1.4f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_05")),
                    multiplier = 1.5f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_06")),
                    multiplier = 1.7f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_07")),
                    multiplier = 1.8f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_08")),
                    multiplier = 1.9f,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_09")),
                    multiplier = 2,
                },
                new Meteor()
                {
                    sprite = new Sprite(contentManager.Load<Texture2D>("meteors/Meteor_10")),
                    multiplier = 2.3f,
                },
            };
        }

        public Meteor getRandomMeteor()
        {
            return meteors[new Random().Next(10)];
        }
    }
}
