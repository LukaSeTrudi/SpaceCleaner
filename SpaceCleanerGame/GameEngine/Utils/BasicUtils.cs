using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;

namespace GameEngine.Utils
{
    public static class BasicUtils
    {
        public static Boolean insideScreen(Rectangle bounds)
        {
            Rectangle screen = new Rectangle(0, 0, Resolution.VWidth, Resolution.VHeight);

            return screen.Contains(bounds);
        }

        public static Boolean insideScreen(Vector2 point)
        {
            Rectangle screen = new Rectangle(0, 0, Resolution.VWidth, Resolution.VHeight);
            return screen.Contains(point);
        }

        public static Boolean insideScreenRadius(Vector2 point)
        {
            int maxR = Math.Max(Resolution.VWidth, Resolution.VHeight);
            Vector2 center = new Vector2(Resolution.VWidth / 2, Resolution.VHeight);
            int dist = (int)Vector2.Distance(point, center);
            return dist <= maxR + 1000;
        }
    }
}
