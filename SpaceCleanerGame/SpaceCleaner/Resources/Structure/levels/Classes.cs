using SpaceCleaner.Resources.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Structure.levels
{
    public class LevelsData
    {
        public int random_seed;
        public List<String> levels;
        public List<LevelData> levelsData = new List<LevelData>();
    }
    public class Wave
    {
        public List<Meteorites> meteorites;
        public List<Bosses> bosses;
        public float time_delay = 1;
    }
    public class Bosses
    {
        public int type = 1;
        public int number = 1;
        public string startX = "center";
        public float time_between = 1;
        public int xOffset;
        public int yOffset;

    }
    public class Meteorites
    {
        public int type = 1;
        public int number = 1;
        public float time_between = 1;
        public int xOffset;
        public int yOffset;
        public string startX = "center";
        public float xVelocity = 0;
        public float yVelocity = 0.2f;
    }

    public class LevelData
    {
        public List<Wave> waves;
    }
}
