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
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Prefabs.Enemies;
using SpaceCleaner.Resources.Structure;
using SpaceCleanerGame;
using SpaceCleanerGame.SpaceCleaner.Resources.Managers;
using SpaceCleanerGame.SpaceCleaner.Resources.Prefabs;

namespace SpaceCleaner.Resources.Prefabs
{
    public class PlayerGO : GameObject
    {
        private const float movementSpeed = 5000f;
        public PlayerShip currentShip;
        public PlayerShipLevel currentShipLevel;


        private int maxHealth = 11;
        private int maxShields = 8;

        private int health = 11;
        private int shields = 0;
        private int damage = 3;
        public int bonusDamage = 0;
        public int Health
        {
            get { return health; }
            set {
                health = Math.Min(value, maxHealth);
                GameLoader.Save();
            }
        }
        public int Shields
        {
            get { return shields; }
            set
            {
                shields = Math.Min(value, maxShields);
            }
        }
       

        private int score = 0;
        public int Score { 
            get { return score; } 
            set { 
                score = value;
                InterfaceManager.updateScore(score);
                GameLoader.Save();
            }
        }

        public int totalDamage()
        {
            return this.damage + this.bonusDamage;
        }
        
        public void takeDamage(int damage)
        {
            SoundManager.Instance.playPlayerHurt();
            if (Shields > 0)
            {
                Shields--;
                return;
            }
            Health -= damage;
            if (Health <= 0)
            {
                Game1.Instance.changeState(Game1.GameState.Dead);
            }
        }
        
        public static PlayerGO Instance;

        public PlayerGO()
        {
            Instance = this;
            currentShip = ShipManager.Instance.ships[0];
            currentShipLevel = currentShip.ship_levels[0];
            collidable = true;
            this.transform.scale = new Vector2(0.8f);
            this.zoomedSprite = 1.25f;
            collisionType = CollisionType.Circle;
            // Score = Game1.Instance.gameLoader.playerScore;
            // Health = Game1.Instance.gameLoader.playerLives;
        }

        public override void onCollision(GameObject other)
        {
            //Debug.WriteLine(other);
            //other.Destroy();
            if(other is PowerUpGO)
            {
                AbstractPowerUp powerUp = ((PowerUpGO)other).powerUp.copy();

                PowerUpManager.Instance.addActivePowerUp(powerUp);
                
                other.Destroy();
                SoundManager.Instance.playPowerUpPickup();
            }
            else if(other is MeteorGO)
            {
                other.Destroy();
                takeDamage(1);
            } else if(other is BossMissileShot)
            {
                other.Destroy();
                takeDamage(((BossMissileShot)other).damage);
            } else if(other is BossShot)
            {
                other.Destroy();
                takeDamage(((BossShot)other).damage);
            } else if(other is Boss1)
            {
                takeDamage(1);
            }
        }

        public override void Initialize()
        {
            List<Animation> anims = new List<Animation>
            {
                new Animation("exhaust_slow", currentShipLevel.exhaust_slow),
                new Animation("exhaust_fast", currentShipLevel.exhaust_fast),
                new Animation("explosion", currentShipLevel.explosion)
            };
            AnimationController animControl = new AnimationController(anims);
            SpriteRenderer rend = new SpriteRenderer(this, null);
            Animator animator = new Animator(this, animControl, rend);

            this.addComponent(animator);
            this.addComponent(rend);


            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Left)) {
                transform.position.X -= (int)(movementSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(keyboardState.IsKeyDown(Keys.Right)) {
                transform.position.X += (int)(movementSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if(InputManager.dragging)
            {
                Vector2 moveDelta = InputManager.lastDragDelta;
                transform.position.X += moveDelta.X;
                transform.position.Y += moveDelta.Y;

                Rectangle newBound = objectBounds();
                int offset = 400;
                if (newBound.Left < - offset || newBound.Right >= Resolution.VWidth + offset || newBound.Top < -offset || newBound.Bottom >= Resolution.VHeight + offset)
                {
                    transform.position.X -= moveDelta.X;
                    transform.position.Y -= moveDelta.Y;
                }
            }

            if(SceneManager.Instance.currentScene.findTypeOf<Boss1>() == null)
            {
                //asd
                // Game1.Instance.changeState(Game1.GameState.Win);
            }


            base.Update(gameTime);
        }



    }
}
