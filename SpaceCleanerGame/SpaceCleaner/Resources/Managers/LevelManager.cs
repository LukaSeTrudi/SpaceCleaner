using Microsoft.Xna.Framework.Content;
using SpaceCleaner.Resources.Structure.levels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpaceCleaner.Resources.Structure;
using SpaceCleanerGame;
using SpaceCleaner.Resources.Interfaces;

namespace SpaceCleaner.Resources.Managers
{
    public class LevelManager
    {

        public static LevelManager Instance;

        public LevelsData levels;

        public List<LevelData> levelsData = new List<LevelData>();

        public List<Level> playerCompletedLevels = new List<Level>();
        
        public LevelManager(ContentManager contentManager)
        {
            Instance = this;

            string json = File.ReadAllText(Path.Combine(contentManager.RootDirectory, "levels/levels.json"));
            levels = JsonConvert.DeserializeObject<LevelsData>(json);

            foreach(String lvl in levels.levels)
            {
                string jsonLvl = File.ReadAllText(Path.Combine(contentManager.RootDirectory, lvl));
                levelsData.Add(JsonConvert.DeserializeObject<LevelData>(jsonLvl));
            }

            Game1.Instance.gameLoader.completedLevels.ForEach(lvl =>
            {
                playerCompletedLevels.Add(lvl);
            });

            // asd
        }

        public int levelsCompleted()
        {
            return playerCompletedLevels.Count;
        }

        public int getStarIndex(int index)
        {
            if(index < playerCompletedLevels.Count)
            {
                return playerCompletedLevels[index].stars;
            }
            return 0;
        }

        public LevelData getLevelData(int index)
        {
            if(index < levelsData.Count)
            {
                return levelsData[index];
            }
            return null;
        }

        public LevelInfoEventArgs getLevelInfoEventArgs(int index)
        {
            if(index < playerCompletedLevels.Count)
            {
                Level completedLevel = playerCompletedLevels[index];
                return new LevelInfoEventArgs()
                {
                    level = completedLevel.index,
                    score = completedLevel.score,
                    recordScore = completedLevel.record,
                    stars = completedLevel.stars,
                    isUnlocked = true,
                    isNextToUnlock = false
                };
            } else
            {
                return new LevelInfoEventArgs()
                {
                    level = index,
                    score = 0,
                    recordScore = 0,
                    stars = 0,
                    isUnlocked = false,
                    isNextToUnlock = index == playerCompletedLevels.Count
                };
            }
        }
    }
}
