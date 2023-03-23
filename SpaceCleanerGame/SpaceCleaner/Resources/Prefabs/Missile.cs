using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Animations;
using GameEngine.Components;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Prefabs;

namespace SpaceCleanerGame.SpaceCleaner.Resources.Prefabs
{
    public class MissilesGO : GameObject
    {
        private PlayerGO player;

        float timer = 0;
        float missileCooldown = 250f;
        public int missileBonusFirerate = 0;
        public Boolean shooting = false;
        public static MissilesGO Instance;
        public MissilesGO(PlayerGO player)
        {
            this.player = player;
            collidable = true;
            Instance = this;
            collisionType = CollisionType.Rectangle;
        }
        
        
        public override void Update(GameTime gameTime)
        {
            if(shooting)
            {
                if (timer >= missileCooldown - missileBonusFirerate)
                {
                    Vector2 l = new Vector2(player.transform.position.X - 100, player.transform.position.Y);
                    Vector2 r = new Vector2(player.transform.position.X + 100, player.transform.position.Y);
                    Vector2 m = new Vector2(player.transform.position.X, player.transform.position.Y - 300);
                    this.addChild(new MissileGO(MissileManager.Instance.missiles[0], m));
                    this.addChild(new MissileGO(MissileManager.Instance.missiles[0], l));
                    this.addChild(new MissileGO(MissileManager.Instance.missiles[0], r));
                    timer = 0;
                }
                timer += gameTime.ElapsedGameTime.Milliseconds;
            }
            base.Update(gameTime);
        }

    }
    public class MissileGO : GameObject
    {
        Missile missile;
        Vector2 initialPosition;
        float flyingVelocity = 100f;
        Animator animator;
        public MissileGO(Missile missile, Vector2 initialPos)
        {
            this.missile = missile;
            this.initialPosition = initialPos;
            transform.position = initialPos;
            transform.scale = new Vector2(2);
            List<Animation> anims = new List<Animation>
            {
                new Animation("flying", missile.flying),
                new Animation("explosion", missile.explosion),
            };
            AnimationController animControl = new AnimationController(anims);
            SpriteRenderer rend = new SpriteRenderer(this, null);
            animator = new Animator(this, animControl, rend);

            this.addComponent(animator);
            this.addComponent(rend);

            this.collidable = true;
            this.collisionType = CollisionType.Rectangle;

            SoundManager.Instance.playMissileShot();
        }

        public override void Update(GameTime gameTime)
        {
            transform.position.Y -= flyingVelocity;
            if(transform.position.Y < 0)
            {
                Destroy();
                return;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void onCollision(GameObject other)
        {
            if(other is MeteorGO)
            {
                this.flyingVelocity = 0;
                PlayerGO.Instance.Score++;
                this.animator.changeAnimation("explosion");
                MeteorGO other_meteor = (MeteorGO)other;
                if (other_meteor.health.takeDamage(1 + PlayerGO.Instance.bonusDamage))
                {
                    other.Destroy();
                }
                this.Destroy();
            }
        }

    }
}
