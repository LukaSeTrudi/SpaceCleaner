using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Animations
{
    public class AnimationController
    {
        public List<Animation> animations;
        int activeAnimation = 0;
        public int activeAnimationIndex = 0;
        
        public AnimationController(List<Animation> animations)
        {
            this.animations = animations;
        }

        public Animation getAnimation()
        {
            return this.animations[activeAnimation];
        }

        public void increaseAnimationIndex()
        {
            if(activeAnimationIndex + 1 == this.getAnimation().animatedSprite.spriteCount && this.getAnimation().stopAfter)
            {
                return;
            }
            activeAnimationIndex++;
            if (activeAnimationIndex >= this.getAnimation().animatedSprite.spriteCount) 
                activeAnimationIndex = 0;
        }

        public void changeAnimation(String name)
        {
            for(int i = 0; i < this.animations.Count; i++)
            {
                Animation animation = this.animations[i];
                if(animation.name == name)
                {
                    activeAnimation = i;
                    activeAnimationIndex = 0;
                    break;
                }
            }
        }
    }
}
