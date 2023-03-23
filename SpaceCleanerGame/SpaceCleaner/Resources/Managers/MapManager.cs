using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Managers
{
    public class Map
    {
        public Texture2D background_full;

        public Texture2D layer0;
        public Texture2D layer1;
        public Texture2D layer2;
    }
    public class MapManager
    {
        public static MapManager Instance;
        
        public List<Texture2D> levelNumberTextures = new List<Texture2D>();
        public List<Texture2D> levelButtons = new List<Texture2D>();
        public List<Map> maps = new List<Map>();
        public Texture2D arrow;

        public MapManager(ContentManager contentManager)
        {
            Instance = this;
            for (int i = 0; i < 10; i++)
            {
                levelNumberTextures.Add(contentManager.Load<Texture2D>("map/Level_Elements/" + i));
            }
            for (int i = 1; i <= 4; i++)
            {
                string btn = "map/Level_Elements/Button_0" + i;
                levelButtons.Add(contentManager.Load<Texture2D>(btn));
            }

            arrow = contentManager.Load<Texture2D>("map/Level_Elements/Arrows");

            for (int i = 1; i <= 4; i++)
            {   
                Texture2D bg_full = contentManager.Load<Texture2D>("map/Maps/Map_0" + i + "/Map_0" + i + "_1080x3000");
                
                Texture2D layer0 = contentManager.Load<Texture2D>("map/Maps/Map_0" + i + "/Layers/BG_1080x3000");
                Texture2D layer1 = contentManager.Load<Texture2D>("map/Maps/Map_0" + i + "/Layers/Decor_A_1080x3000");
                Texture2D layer2 = contentManager.Load<Texture2D>("map/Maps/Map_0" + i + "/Layers/Decor_B_1080x3000");
                maps.Add(new Map() { background_full = bg_full, layer0 = layer0, layer1 = layer1, layer2 = layer2 });
            }
        }

        public AbsoluteLayout getLevelButtonInstance(Vector2 position, int text_number, Boolean unlocked = true, Boolean unlockable = false)
        {
            AbsoluteLayout layout = new AbsoluteLayout();
            Text numberText = new Text();
            numberText.text = text_number.ToString();
            // Image number = new Image(new Sprite(levelNumberTextures[0]));
            ImageButton button;
            
            button = new ImageButton(new Sprite(levelButtons[LevelManager.Instance.getStarIndex(text_number - 1)]));
            numberText.scale = new Vector2(2f);
            button.scale = new Vector2(3.5f);

            button.position = position;
            numberText.position = position;

            numberText.position.Y += 50;

            if (!unlocked && !unlockable)
            {
                button.opacity = 0.4f;
                numberText.opacity = 0.4f;
                button.disabled = true;
            }
            
            
            layout.addControl(button);
            layout.addControl(numberText);
            return layout;
        }
        public float GetArrowRotation(Vector2 startPoint, Vector2 endPoint)
        {
            Vector2 direction = endPoint - startPoint;
            float angleRadians = (float)Math.Atan2(direction.Y, direction.X) - MathHelper.PiOver2;

            return angleRadians;
        }

        public float GetArrowScale(Vector2 startPoint, Vector2 endPoint)
        {
            float distance = Vector2.Distance(startPoint, endPoint);
            float scale = distance / arrow.Height;

            return scale;
        }
    }
}
