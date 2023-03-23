
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.Components;
using GameEngine.Graphics;
using GameEngine.Scenes;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using SpaceCleaner.Resources.Managers;
using SpaceCleanerGame;

namespace SpaceCleaner.Resources.Prefabs
{
    public class PowerUpGO : GameObject
    {
        public AbstractPowerUp powerUp;
        Vector2 velocity;
        public PowerUpGO(AbstractPowerUp powerUp, Vector2 position)
        {
            this.powerUp = powerUp;
            this.transform.position = position;
            this.addComponent(new SpriteRenderer(this, new Sprite(this.powerUp.icon)));
            velocity = new Vector2(Random.NextFloat(-1, 1), Random.NextFloat(-1, 1));
            
            this.collidable = true;
            this.collisionType = CollisionType.Circle;
        }

        public override void Update(GameTime gameTime)
        {
            transform.position.X += velocity.X * 10;
            transform.position.Y += velocity.Y * 10;
            transform.rotation += 0.02f;
            if (transform.rotation >= System.Math.PI * 2f)
            {
                transform.rotation = 0f;
            }

            Rectangle bounds = objectBounds();
            Rectangle screen = Game1.Instance.screen;

            if (bounds.Left <= screen.Left || bounds.Right >= screen.Right)
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
}
