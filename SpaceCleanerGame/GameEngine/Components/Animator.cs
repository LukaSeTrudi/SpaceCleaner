using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GameEngine.Animations;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class Animator : BaseGameComponent
    {
        public AnimationController controller;

        float timer = 0f;
        float threshold = 50f;

        Boolean frozen = false;
        
        SpriteRenderer spriteRenderer;
        public Animator(GameObject parent, AnimationController controller, SpriteRenderer spriteRenderer) : base(parent)
        {
            this.controller = controller;
            this.spriteRenderer = spriteRenderer;
            this.spriteRenderer.sprite = this.controller.getAnimation().animatedSprite;
        }

        public override void Update(GameTime gameTime)
        {
            if (frozen) return;
            if(timer <= threshold)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                return;
            }
            this.controller.increaseAnimationIndex();
            this.spriteRenderer.animationIndex = this.controller.activeAnimationIndex;
            timer = 0f;
        }

        public void changeAnimation(String name)
        {
            this.controller.changeAnimation(name);
            this.spriteRenderer.sprite = this.controller.getAnimation().animatedSprite;
        }
    }
}
