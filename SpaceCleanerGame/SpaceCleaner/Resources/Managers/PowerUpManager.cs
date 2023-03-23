using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Prefabs;
using SpaceCleaner.Resources.Prefabs.Enemies;
using SpaceCleaner.Resources.Scenes;
using SpaceCleanerGame.SpaceCleaner.Resources.Prefabs;

namespace SpaceCleaner.Resources.Managers
{
    public enum PowerUpType
    {
        Buff,
        Debuff
    }
    public abstract class AbstractPowerUp
    {
        public Texture2D icon;
        public PowerUpType type;
        public float secondsLast = 0;
        public float secondsLeft = 0;
        public Scene scene;
        public abstract void onInitalize();
        public abstract void onUpdate(GameTime gameTime);
        public abstract void onDestroy();

        public abstract AbstractPowerUp copy();
    }

    public class PowerUpBonusHealth: AbstractPowerUp
    {
        public PowerUpBonusHealth(Texture2D icon)
        {
            this.icon = icon;
            this.type = PowerUpType.Buff;
        }

        public override void onInitalize()
        {
            this.scene = SceneManager.Instance.currentScene;

            this.secondsLeft = this.secondsLast;
            PlayerGO player = scene.findTypeOf<PlayerGO>();
            player.Health++;
        }

        public override void onUpdate(GameTime gameTime)
        {
            this.secondsLeft -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public override void onDestroy()
        {
        }

        public override AbstractPowerUp copy()
        {
            return new PowerUpBonusHealth(this.icon);
        }
    }
    
    public class PowerUpBonusArmor: AbstractPowerUp {
        public PowerUpBonusArmor(Texture2D icon)
        {
            this.icon = icon;
            this.type = PowerUpType.Buff;
        }

        public override void onInitalize()
        {
            this.scene = SceneManager.Instance.currentScene;

            this.secondsLeft = this.secondsLast;
            PlayerGO player = scene.findTypeOf<PlayerGO>();
            player.Shields++;
        }

        public override void onUpdate(GameTime gameTime)
        {
            this.secondsLeft -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public override void onDestroy()
        {
        }

        public override AbstractPowerUp copy()
        {
            return new PowerUpBonusArmor(this.icon);
        }
    }
    public class PowerUpMissileSpeed : AbstractPowerUp
    {
        int bonusFirerate = 50;
        
        public PowerUpMissileSpeed(Texture2D icon)
        {
            this.icon = icon;
            this.type = PowerUpType.Buff;
            this.secondsLast = 4;
        }
        public override void onDestroy()
        {
            MissilesGO missileManager = this.scene.findTypeOf<MissilesGO>();

            missileManager.missileBonusFirerate -= bonusFirerate;
        }

        public override void onInitalize()
        {
            this.scene = SceneManager.Instance.currentScene;

            this.secondsLeft = this.secondsLast;
            MissilesGO missileManager = this.scene.findTypeOf<MissilesGO>();

            missileManager.missileBonusFirerate += bonusFirerate;
        }

        public override void onUpdate(GameTime gameTime)
        {
            this.secondsLeft -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public override AbstractPowerUp copy()
        {
            return new PowerUpMissileSpeed(this.icon);
        }
    }
    public class PowerUpDamageBonus : AbstractPowerUp
    {
        int damage = 2;
        public PowerUpDamageBonus(Texture2D icon)
        {
            this.icon = icon;
            this.type = PowerUpType.Buff;
            this.secondsLast = 3f;
        }
        public override void onDestroy()
        {
            PlayerGO player = scene.findTypeOf<PlayerGO>();
            player.bonusDamage -= damage;
        }

        public override void onInitalize()
        {
            this.scene = SceneManager.Instance.currentScene;
            this.secondsLeft = this.secondsLast;

            PlayerGO player = scene.findTypeOf<PlayerGO>();
            player.bonusDamage += damage;
        }

        public override void onUpdate(GameTime gameTime)
        {
            this.secondsLeft -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public override AbstractPowerUp copy()
        {
            return new PowerUpDamageBonus(this.icon);
        }
    }
    public class PowerUpManager
    {
        public static PowerUpManager Instance;
        List<AbstractPowerUp> powerups = new List<AbstractPowerUp>();
        public List<AbstractPowerUp> activePowerups = new List<AbstractPowerUp>();

        public Sprite powerUpBackground;
        
        public PowerUpManager(ContentManager contentManager)
        {
            Instance = this;
            powerups = new List<AbstractPowerUp>()
            {
                new PowerUpBonusArmor(contentManager.Load<Texture2D>("bonus_items/Armor_Bonus")),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Barrier_Bonus"))),
                new PowerUpDamageBonus(contentManager.Load<Texture2D>("bonus_items/Damage_Bonus")),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Enemy_Destroy_Bonus"))),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Enemy_Speed_Debuff"))),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Hero_Movement_Debuff"))),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Hero_Speed_Debuff"))),
                new PowerUpBonusHealth(contentManager.Load<Texture2D>("bonus_items/HP_Bonus")),
                //new PowerUp(new Sprite(contentManager.Load<Texture2D>("bonus_items/Magnet_Bonus"))),
                new PowerUpMissileSpeed(contentManager.Load<Texture2D>("bonus_items/Rockets_Bonus")),
            };
            powerUpBackground = new Sprite(contentManager.Load<Texture2D>("gui/controls/Bonus_BTN_01"));
        }

        public AbstractPowerUp getRandomPowerUp()
        {
            return powerups[new Random().Next(powerups.Count)];
        }

        public void addRandom(Vector2 position)
        {
            SceneManager.Instance.currentScene.addGameObject(new PowerUpGO(getRandomPowerUp(), position));
        }

        public void addActivePowerUp(AbstractPowerUp powerUp)
        {
            activePowerups.Add(powerUp);
            powerUp.onInitalize();
        }

        public void removeActivePowerUp(AbstractPowerUp powerUp)
        {
            if(activePowerups.Contains(powerUp))
            {
                powerUp.onDestroy();
                activePowerups.Remove(powerUp);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(AbstractPowerUp powerUp in activePowerups.ToList())
            {
                if(powerUp.secondsLeft <= 0)
                {
                    removeActivePowerUp(powerUp);
                    continue;
                }
                powerUp.onUpdate(gameTime);
            }
        }
    }
}
