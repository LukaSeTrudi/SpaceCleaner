using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Graphics;

namespace GameEngine.Animations
{
    public class Animation
    {
        public String name;
        public int speed = 1;
        public AnimatedSprite animatedSprite;
        public Boolean stopAfter = false;

        public Animation(String name, AnimatedSprite animatedSprite, int speed = 1)
        {
            this.name = name;
            this.speed = speed;
            this.animatedSprite = animatedSprite;
        }
    }
}
