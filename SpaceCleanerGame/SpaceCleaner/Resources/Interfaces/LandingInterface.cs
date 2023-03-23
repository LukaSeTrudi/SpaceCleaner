using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleanerGame;

namespace SpaceCleaner.Resources.Interfaces
{
    public static class LandingInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();
        public static void init(ContentManager contentManager)
        {
            Image background = new Image(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/BG")));
            background.setFullscreen();

            ImageButton infoBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Info_BTN")));
            infoBtn.setTopLeft(300, 300);
            infoBtn.scale = new Vector2(2);


            ImageButton settingsBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Settings_BTN")));
            settingsBtn.setTopRight(300, 300);
            settingsBtn.scale = new Vector2(2);

            Image headerText = new Image(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Header")));
            headerText.setCenter();
            headerText.setTop(2000);
            headerText.scale = new Vector2(3);

            ImageButton startBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Start_BTN")));
            startBtn.setCenter();
            startBtn.setBottom(2100);
            startBtn.scale = new Vector2(6);
            startBtn.Click += StartBtn_Click;

            ImageButton mapBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Map_BTN")));
            mapBtn.setCenter();
            mapBtn.setBottom(1300);
            mapBtn.Click += MapBtn_Click;
            mapBtn.scale = new Vector2(4);

            ImageButton exitBtn = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/mainMenu/Exit_BTN")));
            exitBtn.setCenter();
            exitBtn.setBottom(700);
            exitBtn.scale = new Vector2(4);

            

            screen.controls = new List<AbstractControl>() { background, infoBtn, settingsBtn, headerText, startBtn, mapBtn, exitBtn};
        }

        private static void MapBtn_Click(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Map);
        }

        private static void StartBtn_Click(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Playing);
        }

        public static Boolean update()
        {
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch) {
            screen.draw(spriteBatch);
        }
    }
}
