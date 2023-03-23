using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Structure.levels;
using SpaceCleanerGame;
using SpaceCleanerGame.SpaceCleaner.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Interfaces
{
    public class LevelInfoEventArgs : EventArgs
    {
        public int level;
        public int score;
        public int recordScore;
        public int stars;
        public Boolean isUnlocked;
        public Boolean isNextToUnlock;
    }
    public static class MapInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();
        private static ScrollableBackground scrollable;
        private static int maxLevelUnlocked = 0;

        
        
        public static void init(ContentManager contentManager)
        {
            scrollable = new ScrollableBackground();

            LevelInfoInterface.init(contentManager);

            AbsoluteLayout bgs = new AbsoluteLayout();
            Texture2D bgText = contentManager.Load<Texture2D>("map/Maps/Map_01/Map_01_1080x3000");
            Image bgEx = new Image(new Sprite(bgText));
            bgEx.setFullWidth();
            float yOffset = bgEx.imageSprite.texture.Height * bgEx.scale.Y;
            for (int i = 0; i < 5; i++) {
                Image bg = new Image(new Sprite(bgText));
                bg.setFullWidth();
                bg.position.Y = Game1.Instance.screen.Center.Y - i * yOffset;
                bgs.addControl(bg);
            }

            AbsoluteLayout layout = new AbsoluteLayout();
            LevelsData levelsData = LevelManager.Instance.levels;

            Random rand = new Random(levelsData.random_seed + 2);

            maxLevelUnlocked = LevelManager.Instance.levelsCompleted();
            
            int positionY = Game1.Instance.screen.Height - 500;
            int positionYMultiplier = 1250;

            for(int i = 0; i < levelsData.levels.Count; i++)
            {
                Vector2 currentPos = new Vector2(rand.Next(500, Game1.Instance.screen.Width - 500), positionY);
      
                layout.addControl(MapManager.Instance.getLevelButtonInstance(currentPos, i + 1, i <= maxLevelUnlocked, i == maxLevelUnlocked));
                positionY -= positionYMultiplier;

            }

            for (int i = 0; i < layout.children.Count; i++)
            {
                ImageButton imgBtn = (ImageButton)((AbsoluteLayout)layout.children[i]).children[0];

                LevelInfoEventArgs levelArgs = LevelManager.Instance.getLevelInfoEventArgs(i);
                imgBtn.Click += ImgBtn_Click;
                imgBtn.eventArgs = levelArgs;
            }
            
            AbsoluteLayout arrowLayout = new AbsoluteLayout();
            for(int i = 1; i < layout.children.Count; i++)
            {
                Vector2 lastPos = ((ImageButton)((AbsoluteLayout)layout.children[i-1]).children[0]).position;
                Vector2 nowPos = ((ImageButton)((AbsoluteLayout)layout.children[i]).children[0]).position;

                float arrowRotation = MapManager.Instance.GetArrowRotation(nowPos, lastPos);
                Vector2 arrowScale = new Vector2(6.5f);
                
                float totalDistance = Vector2.Distance(lastPos, nowPos);

                float dist = 0;

                float distMult = 43 * arrowScale.X;

                int k = 7;
                while(dist <= totalDistance)
                {
                    Vector2 pos = Vector2.Lerp(lastPos, nowPos, dist / totalDistance);
                    AnimatedImage arr = new AnimatedImage(new AnimatedSprite(MapManager.Instance.arrow, 70, 43, 8, true));
                    arr.animationIndex = k--;
                    arr.position = pos;
                    arr.scale = arrowScale;
                    arr.rotation = arrowRotation;
                    if (i > maxLevelUnlocked)
                    {
                        arr.opacity = 0.2f;
                        arr.stopped = true;
                    }

                    arrowLayout.addControl(arr);
                    dist += distMult;
                    if (k < 0) k = 7;
                }


            }

            scrollable.addControl(bgs);
            scrollable.addControl(arrowLayout);
            scrollable.addControl(layout);

            ImageButton backBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/win/Close_BTN")));
            backBtn.position = new Vector2(Game1.Instance.screen.Width - 300, 300);
            backBtn.scale = new Vector2(2f);
            backBtn.Click += goToMainMenu;

            screen.controls = new List<AbstractControl>() { scrollable, backBtn };
        }

        private static void ImgBtn_Click(object sender, EventArgs e)
        {
            LevelInfoEventArgs levelArgs = (LevelInfoEventArgs)e;
            LevelInfoInterface.setLevel(levelArgs);

            LevelInfoInterface.enabled = true;
        }

        private static void goToMainMenu(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Menu);
        }

        public static Boolean update()
        {
            if (LevelInfoInterface.enabled)
                return LevelInfoInterface.update();
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);
            
            if(LevelInfoInterface.enabled)
            {
                LevelInfoInterface.draw(spriteBatch);
            }
        }
    }
}
