using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Graphics;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleanerGame.SpaceCleaner.Resources.Managers;

namespace SpaceCleaner.Resources.Prefabs
{
    public enum BarColor
    {
        Blue,
        Orange,
        Green,
        Red
    }
    public class Health : GameObject
    {
        public int maxHealth;
        public int currentHealth;

        private int offset = 25;

        public Texture2D background;
        public Texture2D left;
        public Texture2D right;
        public Texture2D middle;

        private int maxHealthWidth = 750;

        public Health(int maxHealth, BarColor barColor)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;

            background = LoadingBarManager.Instance.loadingBarBackgroundTexture;
            switch(barColor)
            {
                case BarColor.Blue:
                    left = LoadingBarManager.Instance.loadingBarLeftBlueTexture;
                    middle = LoadingBarManager.Instance.loadingBarMiddleBlueTexture;
                    right = LoadingBarManager.Instance.loadingBarRightBlueTexture;
                    break;
                case BarColor.Orange:
                    left = LoadingBarManager.Instance.loadingBarLeftOrangeTexture;
                    middle = LoadingBarManager.Instance.loadingBarMiddleOrangeTexture;
                    right = LoadingBarManager.Instance.loadingBarRightOrangeTexture;
                    break;
                case BarColor.Green:
                    left = LoadingBarManager.Instance.loadingBarLeftGreenTexture;
                    middle = LoadingBarManager.Instance.loadingBarMiddleGreenTexture;
                    right = LoadingBarManager.Instance.loadingBarRightGreenTexture;
                    break;
                case BarColor.Red:
                    left = LoadingBarManager.Instance.loadingBarLeftRedTexture;
                    middle = LoadingBarManager.Instance.loadingBarMiddleRedTexture;
                    right = LoadingBarManager.Instance.loadingBarRightRedTexture;
                    break;
            }
        }

        public Boolean takeDamage(int damage)
        {
            currentHealth -= damage;
            return currentHealth <= 0;
        }
        
        public void heal(int healed)
        {
            currentHealth += healed;
            currentHealth = Math.Min(currentHealth, maxHealth);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (currentHealth == maxHealth) return;
            Vector2 parentPos = this.parent.getWorldPosition();
            // draw background  
            Rectangle backgroundRect = new Rectangle((int)Math.Ceiling(parentPos.X - maxHealthWidth / 2), (int)Math.Ceiling(parentPos.Y - 500), maxHealthWidth, 100);
            spriteBatch.Draw(background, backgroundRect, Color.White);

            Rectangle middleRect = new Rectangle((int)(parentPos.X - maxHealthWidth / 2) + offset, (int)parentPos.Y - 500 + offset, (int)((float)currentHealth / (float)maxHealth * maxHealthWidth) - offset*2, 100 - offset*2);
            spriteBatch.Draw(middle, middleRect, Color.White);
        }
    }
}
