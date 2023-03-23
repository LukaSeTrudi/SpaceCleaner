using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Animations;
using GameEngine.Components;
using GameEngine.Graphics;
using GameEngine.Scenes;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using SpaceCleaner.Resources.Managers;

namespace SpaceCleaner.Resources.Prefabs.Enemies
{
    public class BossMissileShot : GameObject
    {
        public Boss boss;
        Animator animator;
        Vector2 shot_dir;
        public int damage = 2;
        public BossMissileShot(Boss boss, Vector2 start_pos, Vector2 shot_direction)
        {
            this.boss = boss;
            this.transform.position = start_pos;
            this.shot_dir = shot_direction;
            this.collidable = true;
            this.collisionType = CollisionType.Rectangle;
            this.Initialize();
        }

        public override void Initialize()
        {
            Animation explosion = new Animation("explosion", this.boss.explosion);
            explosion.stopAfter = true;
            List<Animation> anims = new List<Animation>
            {
                new Animation("flying", this.boss.missile),
                explosion
            };
            AnimationController animControl = new AnimationController(anims);
            SpriteRenderer rend = new SpriteRenderer(this, null);
            animator = new Animator(this, animControl, rend);

            this.addComponent(animator);
            this.addComponent(rend);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!BasicUtils.insideScreenRadius(this.transform.position)) Destroy();

            this.transform.position += this.shot_dir * 50f;


            base.Update(gameTime);
        }
    }
}
