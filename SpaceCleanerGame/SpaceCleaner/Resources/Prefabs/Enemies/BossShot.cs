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
    public class BossShot : GameObject
    {
        public Boss boss;
        Animator animator;
        Vector2 shot_dir;
        public int damage = 1;
        public BossShot(Boss boss, Vector2 start_pos, Vector2 shot_direction)
        {
            this.boss = boss;
            this.transform.position = start_pos;
            this.transform.scale = new Vector2(1);
            this.shot_dir = shot_direction;

            this.collidable = true;
            this.collisionType = CollisionType.Circle;
            this.zoomedSprite = 5f;
            this.transform.scale = new Vector2(0.2f);
            this.Initialize();
        }

        public override void Initialize()
        {
            Animation shot_st = new Animation("shot_start", this.boss.shot_start);
            shot_st.stopAfter = true;
            Animation shot_en = new Animation("shot_end", this.boss.shot_end);
            shot_en.stopAfter = true;
            List<Animation> anims = new List<Animation>
            {
                shot_st,shot_en
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
