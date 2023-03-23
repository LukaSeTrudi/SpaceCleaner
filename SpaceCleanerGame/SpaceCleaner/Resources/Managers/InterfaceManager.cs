using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.Utils;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Interfaces;
using SpaceCleaner.Resources.Prefabs;
using SpaceCleanerGame;

namespace SpaceCleaner.Resources.Managers
{
    
    public static class InterfaceManager
    {
        private static readonly InterfaceScreen pauseScreen = new InterfaceScreen();


        public static void updateScore(int score)
        {
            if (GameInterface.scoreBottomText == null) return;
            GameInterface.scoreBottomText.text = score + "";
            YouWonInterface.scoreText.text = score + "";
            YouLoseInterface.scoreText.text = score + "";
        }
        public static void draw(SpriteBatch spriteBatch)
        {
            if (Game1.Instance.State == Game1.GameState.Playing || Game1.Instance.State == Game1.GameState.Paused || Game1.Instance.State == Game1.GameState.Dead || Game1.Instance.State == Game1.GameState.Win)
            {
                GameInterface.draw(spriteBatch);
            }
  
            if (Game1.Instance.State == Game1.GameState.Paused)
            {
                PauseInterface.draw(spriteBatch);
            } else if(Game1.Instance.State == Game1.GameState.Dead)
            {
                YouLoseInterface.draw(spriteBatch);
            } else if(Game1.Instance.State == Game1.GameState.Menu)
            {
                LandingInterface.draw(spriteBatch);
            } else if(Game1.Instance.State == Game1.GameState.Win)
            {
                YouWonInterface.draw(spriteBatch);
            } else if(Game1.Instance.State == Game1.GameState.Map)
            {
                MapInterface.draw(spriteBatch);
            }
            else if (Game1.Instance.State == Game1.GameState.Settings)
            {
                SettingsInterface.draw(spriteBatch);
            }
        }
        

        public static Boolean update()
        {
            if (Game1.Instance.State == Game1.GameState.Playing)
            {
                return GameInterface.update();
            } else if(Game1.Instance.State == Game1.GameState.Paused)
            {
                return PauseInterface.update();
            } else if(Game1.Instance.State == Game1.GameState.Dead)
            {
                return YouLoseInterface.update();
            }
            else if (Game1.Instance.State == Game1.GameState.Menu)
            {
                return LandingInterface.update();
            } else if(Game1.Instance.State == Game1.GameState.Win)
            {
                return YouWonInterface.update();
            } else if(Game1.Instance.State == Game1.GameState.Map)
            {
                return MapInterface.update();
            }
            else if (Game1.Instance.State == Game1.GameState.Settings)
            {
                return SettingsInterface.update();
            }
            return false;
        }
        public static void init(ContentManager contentManager)
        {
            PauseInterface.init(contentManager);
            GameInterface.init(contentManager);
            LandingInterface.init(contentManager);
            YouWonInterface.init(contentManager);
            YouLoseInterface.init(contentManager);
            MapInterface.init(contentManager);
            SettingsInterface.init(contentManager);
        }
        
    }
}
