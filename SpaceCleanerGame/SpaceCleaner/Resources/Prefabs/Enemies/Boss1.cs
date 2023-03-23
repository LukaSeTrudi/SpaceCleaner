using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.Animations;
using GameEngine.Components;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using SpaceCleaner.Resources.Managers;
using SpaceCleanerGame.SpaceCleaner.Resources.Prefabs;

namespace SpaceCleaner.Resources.Prefabs.Enemies
{

    public class Boss1 : GameObject
    {
        Boss boss;
        Vector2 moveDirection = Vector2.Zero;
        public Health health;
        private enum AttackMode
        {
            None,
            Shot,
            Missile
        }

        public override void onCollision(GameObject other)
        {
            if (other is MissileGO)
            {
                SoundManager.Instance.playShipHurt();
                PlayerGO.Instance.Score+=10;
                other.Destroy();
                if(health.takeDamage(3 + PlayerGO.Instance.bonusDamage))
                {
                    this.Destroy();
                }
            }
        }

        public Boss1(Boss boss)
        {
            this.boss = boss;

            this.collidable = true;
            this.collisionType = CollisionType.Rectangle;
            health = new Health(100, BarColor.Red);
            this.Initialize();
        }
        public override void Initialize()
        {
            List<Animation> anims = new List<Animation>
            {
                new Animation("attack1", this.boss.attack1),
                new Animation("attack2", this.boss.attack2),
                new Animation("attack3", this.boss.attack3),
                new Animation("attack4", this.boss.attack4),
                new Animation("death", this.boss.death),
                new Animation("explosion", this.boss.explosion),
                new Animation("flight", this.boss.flight),
                new Animation("idle1", this.boss.idle1),
                new Animation("idle2", this.boss.idle2),
            };
            AnimationController animControl = new AnimationController(anims);
            SpriteRenderer rend = new SpriteRenderer(this, null);
            Animator animator = new Animator(this, animControl, rend);

            this.addComponent(animator);
            this.addComponent(rend);
            this.addChild(health);
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            AttackMode currentAttackMode = AttackMode.None;

            Vector2 playerPos = PlayerGO.Instance.getWorldPosition();
            if (this.moveDirection == Vector2.Zero || (at60frames && new Random().Next(10) < 8))
            {
                this.moveDirection = playerPos;
                this.moveDirection.Y = this.transform.position.Y;
            }
            this.MoveTowardsX((int)this.moveDirection.X, 20);
            if (at60frames)
            {
                Vector2 distanceFromPlayer = playerPos - this.getWorldPosition();
                if (Math.Abs(distanceFromPlayer.X) <= 800)
                {
                    currentAttackMode = AttackMode.Missile;
                } else
                {
                    currentAttackMode = AttackMode.Shot;
                }
                if(currentAttackMode == AttackMode.Shot)
                {
                    Vector2 shotRel1Pos = new Vector2(-160, 400);
                    Vector2 shotRel2Pos = new Vector2(160, 400);
                    Vector2 shotAbs1Pos = this.getWorldPosition() + shotRel1Pos;
                    Vector2 shotAbs2Pos = this.getWorldPosition() + shotRel2Pos;
                    Vector2 shotDir1Vector = Vector2.Normalize(playerPos - shotAbs1Pos);
                    Vector2 shotDir2Vector = Vector2.Normalize(playerPos - shotAbs2Pos);

                    SceneManager.Instance.currentScene.addGameObject(new BossShot(this.boss, shotAbs1Pos, shotDir1Vector));
                    SceneManager.Instance.currentScene.addGameObject(new BossShot(this.boss, shotAbs2Pos, shotDir2Vector));
                } else if(currentAttackMode == AttackMode.Missile)
                {
                    Vector2 shotRelPos = new Vector2(0, 500);
                    Vector2 shotAbs1Pos = this.getWorldPosition() + shotRelPos;
                    SceneManager.Instance.currentScene.addGameObject(new BossMissileShot(this.boss, shotAbs1Pos, Vector2.UnitY));
                }
                health.heal(2);

            }
            base.Update(gameTime);
        }
    }
}
