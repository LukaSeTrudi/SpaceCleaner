using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Prefabs;

namespace SpaceCleaner.Resources.Structure
{
    public class Level
    {
        public int index { get; set; } = 0;
        public int stars { get; set; } = 0;
        public int score { get; set; } = 0;
        public int record { get; set; } = 0;
        public float time { get; set; } = 0.0f;
    }
    
    public class GameLoader
    {
        public int playerScore { get; set; } = 0;
        public int playerCrystals { get; set; } = 0;

        public List<Level> completedLevels { get; set; } = new List<Level>();
    

        public static void Save()
        {
            GameLoader gameLoader = new GameLoader()
            {
                playerScore = PlayerGO.Instance.Score,
                playerCrystals = PlayerGO.Instance.Health,
                completedLevels = LevelManager.Instance.playerCompletedLevels
            };
            gameLoader.completedLevels = new List<Level>() {
                new Level() { index = 0, stars = 1, score = 1000, record = 1000, time = 0.0f },
                new Level() { index = 1, stars = 2, score = 1000, record = 1000, time = 0.0f },
                new Level() { index = 2, stars = 3, score = 1000, record = 1000, time = 0.0f },
            };
            string serialized = JsonSerializer.Serialize<GameLoader>(gameLoader);
            File.WriteAllText("game.json", JsonSerializer.Serialize(gameLoader));
        }
        
        public static GameLoader Load()
        {
            try
            {
                return JsonSerializer.Deserialize<GameLoader>(File.ReadAllText("game.json"));
            } catch
            {
                return new GameLoader();
            }
        }
    }
    
}
