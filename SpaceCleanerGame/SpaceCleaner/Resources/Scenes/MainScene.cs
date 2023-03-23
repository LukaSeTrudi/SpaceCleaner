using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GameEngine;
using GameEngine.GUI;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Prefabs;
using SpaceCleaner.Resources.Prefabs.Enemies;
using SpaceCleaner.Resources.Structure.levels;
using SpaceCleanerGame;
using SpaceCleanerGame.SpaceCleaner.Resources.Prefabs;

namespace SpaceCleaner.Resources.Scenes
{
    // GAMEOBJECTS

    public class MeteoritesSet {
        public Meteorites meteorites;
        public float? timeLeftToSpawnMeteorite;
        public int meteoritesToSpawn;

        public MeteoritesSet(Meteorites meteorites)
        {
            this.meteorites = meteorites;
            this.timeLeftToSpawnMeteorite = meteorites.time_between;
            this.meteoritesToSpawn = meteorites.number;
        }

        public void update(GameTime gameTime)
        {
            if(timeLeftToSpawnMeteorite != null)
            {
                timeLeftToSpawnMeteorite -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeLeftToSpawnMeteorite <= 0)
                {
                    spawnMeteorite();
                }
            }
        }

        private void spawnMeteorite()
        {
            Meteor meteorType = MeteorManager.Instance.meteors[meteorites.type];
            Vector2 vel = new Vector2(meteorites.xVelocity, meteorites.yVelocity);
            Vector2 position;
            if (meteorites.startX == "left") position = new Vector2(0, meteorites.yOffset);
            else if (meteorites.startX == "right") position = new Vector2(Game1.Instance.screen.Right - meteorites.xOffset, meteorites.yOffset);
            else position = new Vector2(Game1.Instance.screen.Center.X, meteorites.yOffset);

            MeteorGO meteorGO = new MeteorGO(meteorType, position, vel);
            MeteorsGO.Instance.addMeteor(meteorGO);
            meteoritesToSpawn--;
            timeLeftToSpawnMeteorite = meteorites.time_between;
            if(meteoritesToSpawn <= 0)
            {
                timeLeftToSpawnMeteorite = null;
            }
        }
    }

    public static class MainScene
    {
        private static int waveIndex = 0;
        private static LevelData levelData;
        private static Wave activeWave;

        private static float? timeLeftToSpawnWave = null;
        private static float? timeToEnd = null;


        private static List<MeteoritesSet> meteoritesSets = new List<MeteoritesSet>();

        public static Scene LoadContent()
        {
            // Init scene
            Scene scene = new Scene();

            // Player gameobject

            Vector2 center = new Vector2(Game1.Instance.screen.Width / 2, Game1.Instance.screen.Height / 2);


            MeteorsGO meteors = new MeteorsGO();
            // meteors.transform.position = center;
            scene.addGameObject(meteors);

            PlayerGO player = new PlayerGO();
            player.transform.position = new Vector2(center.X, Game1.Instance.screen.Height - 1000);

            MissilesGO missiles = new MissilesGO(player);
            scene.addGameObject(missiles);
            scene.addGameObject(player);

            // Boss1 boss = new Boss1(BossManager.Instance.bosses[0]);
            // boss.transform.position = new Vector2(center.X, 1000);
            // scene.addGameObject(boss);

            scene.onAfterUpdate += Scene_onAfterUpdate;

            return scene;
        }
        private static int meteoritesToSpawn()
        {
            int count = 0;
            foreach(MeteoritesSet meteoritesSet in meteoritesSets)
            {
                count += meteoritesSet.meteoritesToSpawn;
            }
            return count;
        }
        private static void Scene_onAfterUpdate(object sender, GameTime e)
        {
            if(timeToEnd != null)
            {
                timeToEnd -= (float)e.ElapsedGameTime.TotalSeconds;
                if(timeToEnd <= 0)
                {
                    Game1.Instance.changeState(Game1.GameState.Win);
                }
                return;
            }
            if(timeLeftToSpawnWave != null)
            {
                timeLeftToSpawnWave -= (float)e.ElapsedGameTime.TotalSeconds;
                if(timeLeftToSpawnWave <= 0)
                {
                    spawnNextWave();
                    timeLeftToSpawnWave = null;
                }
            }
            foreach(MeteoritesSet meteoritesSet in meteoritesSets)
            {
                meteoritesSet.update(e);
            }

            if (meteoritesToSpawn() <= 0 && MeteorsGO.Instance.children.Count == 0)
            {
                MissilesGO.Instance.shooting = false;
                if (waveIndex >= levelData.waves.Count)
                {
                    // END;
                    timeToEnd = 3;
                    //Game1.Instance.changeState(Game1.GameState.Win);
                    return;
                }
                if (timeLeftToSpawnWave == null)
                    timeLeftToSpawnWave = levelData.waves[waveIndex].time_delay;
            }
            else
                timeToEnd = null;
        }

        private static void spawnNextWave()
        {
            MissilesGO.Instance.shooting = true;
            activeWave = levelData.waves[waveIndex];
            meteoritesSets.Clear();
            foreach (Meteorites meteorites in activeWave.meteorites) {
                meteoritesSets.Add(new MeteoritesSet(meteorites));
            }
            waveIndex++;
        }
       
        private static void startLevel()
        {
            waveIndex = 0;
            timeLeftToSpawnWave = levelData.waves[0].time_delay;
            MissilesGO.Instance.shooting = false;
        }
        
        public static void LoadLevelData(LevelData data)
        {
            levelData = data;
            startLevel();
        }
    }
}
