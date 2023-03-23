
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Scenes;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Managers;
using SpaceCleanerGame;

namespace SpaceCleaner.Resources.Prefabs
{
    public class MeteorGO : GameObject
    {
        Vector2 velocity = new Vector2(0, 1);
        Meteor meteor;

        float rotationMultiplier;

        public Health health;
        
        public MeteorGO clone()
        {
            return new MeteorGO(this.meteor, this.transform.position, this.velocity);
        }
        
        public MeteorGO(Meteor meteor, Vector2 position, Vector2 velocity)
        {
            this.meteor = meteor;
            this.velocity = velocity;
            transform.position = position;
            // velocity = new Vector2(Random.NextFloat(-1, 1), Random.NextFloat(-1, 1));
            transform.scale = new Vector2(meteor.multiplier*2, meteor.multiplier*2);
            // transform.position = new Vector2(Random.NextFloat(-Game1.Instance.screen.Width/2, Game1.Instance.screen.Width/2), Random.NextFloat(-Game1.Instance.screen.Height / 2, -Game1.Instance.screen.Height / 4));
            rotationMultiplier = (3 - meteor.multiplier) * 0.01f;

            collidable = true;
            collisionType = CollisionType.Circle;
            health = new Health((int)meteor.multiplier*3, BarColor.Orange);
            if (!this.initialized) this.Initialize();
        }

        public override void onBeforeDestroy()
        {
            // MeteorsGO.Instance.addRandom();
            if(Random.NextFloat(0, 10) <= 2)
            {
                PowerUpManager.Instance.addRandom(getWorldPosition());

            }
        }

        public override void Initialize()
        {
            this.addComponent(new SpriteRenderer(this, this.meteor.sprite));
            this.addChild(health);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            transform.position.X += velocity.X * 10;
            transform.position.Y += velocity.Y * 10;
            transform.rotation += rotationMultiplier;
            if(transform.rotation >= System.Math.PI * 2f)
            {
                transform.rotation = 0f;
            }

            Rectangle bounds = objectBounds();
            Rectangle screen = Game1.Instance.screen;

            if(velocity.X >= 0 && bounds.Right >= screen.Right) {
                velocity.X *= -1;
                transform.position.X += velocity.X * 10;
            }
            else if(velocity.X <= 0 && bounds.Left <= screen.Left)
            {
                velocity.X *= -1;
                transform.position.X += velocity.X * 10;
            }

            if (bounds.Top < screen.Top || bounds.Bottom > screen.Bottom)
            {
                velocity.Y *= -1;
                transform.position.Y += velocity.Y * 10;
            }

            base.Update(gameTime);
        }


    }
    public class MeteorsGO : GameObject
    {
        public List<MeteorGO> meteors = new List<MeteorGO>();
        public static MeteorsGO Instance;
        public MeteorsGO()
        {
            Instance = this;
            for (int i = 0; i < 15; i++)
            {
                // this.addChild(new MeteorGO(MeteorManager.Instance.getRandomMeteor()));
            }
        }

        public void addMeteor(MeteorGO meteor)
        {
            this.addChild(meteor);
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void addRandom()
        {
            // this.addChild(new MeteorGO(MeteorManager.Instance.getRandomMeteor()));
        }
    }
}
