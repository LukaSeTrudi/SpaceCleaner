using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Utils
{
    public static class Random
    {
        static System.Random randGen = new System.Random();
        public static float NextFloat(int min, int max)
        {
            return (float)(randGen.NextDouble() * (max - min) + min);
        }
    }
}
