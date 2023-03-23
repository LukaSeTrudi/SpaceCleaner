using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceCleanerGame.SpaceCleaner.Resources.Managers
{
    public class LoadingBarManager
    {
        public static LoadingBarManager Instance;

        public Texture2D loadingBarBackgroundTexture;

        // Blue
        public Texture2D loadingBarLeftBlueTexture;
        public Texture2D loadingBarMiddleBlueTexture;
        public Texture2D loadingBarRightBlueTexture;

        // Green
        public Texture2D loadingBarLeftGreenTexture;
        public Texture2D loadingBarMiddleGreenTexture;
        public Texture2D loadingBarRightGreenTexture;

        // Orange
        public Texture2D loadingBarLeftOrangeTexture;
        public Texture2D loadingBarMiddleOrangeTexture;
        public Texture2D loadingBarRightOrangeTexture;

        // Red
        public Texture2D loadingBarLeftRedTexture;
        public Texture2D loadingBarMiddleRedTexture;
        public Texture2D loadingBarRightRedTexture;


        public LoadingBarManager(ContentManager manager)
        {
            Instance = this;
            LoadContent(manager);
        }

        public void LoadContent(ContentManager manager)
        {
            loadingBarBackgroundTexture = manager.Load<Texture2D>("gui/loadingBars/Table");

            // Blue
            loadingBarLeftBlueTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_3_1");
            loadingBarMiddleBlueTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_3_2");
            loadingBarRightBlueTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_3_3");

            // Green
            loadingBarLeftGreenTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_2_1");
            loadingBarMiddleGreenTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_2_2");
            loadingBarRightGreenTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_2_3");

            // Orange
            loadingBarLeftOrangeTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_1_1");
            loadingBarMiddleOrangeTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_1_2");
            loadingBarRightOrangeTexture = manager.Load<Texture2D>("gui/loadingBars/Loading_Bar_1_3");


            // Red
            loadingBarLeftRedTexture = manager.Load<Texture2D>("gui/controls/Boss_HP_Bar_1");
            loadingBarMiddleRedTexture = manager.Load<Texture2D>("gui/controls/Boss_HP_Bar_2");
            loadingBarRightRedTexture = manager.Load<Texture2D>("gui/controls/Boss_HP_Bar_3");
        }


    }
}
