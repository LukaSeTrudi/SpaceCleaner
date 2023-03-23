using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleanerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Interfaces
{
    public class PauseInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();

        public static void init(ContentManager contentManager)
        {
            // background pause
            Image background = new Image(new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Window")));
            background.setCenter();
            background.scale = new Vector2(4);

            Rectangle backgroundRect = background.getBounds();

            // pause text
            Image header = new Image(new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Header")));
            header.scale = new Vector2(4);
            header.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 300);

            // score

            Image scoreHeader = new Image(new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Score")));
            scoreHeader.scale = new Vector2(4);
            scoreHeader.position = new Vector2(backgroundRect.Center.X - 900, backgroundRect.Top + 1000);

            Image scoreBack = new Image(new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Table")));
            scoreBack.scale = new Vector2(4);
            scoreBack.position = new Vector2(backgroundRect.Center.X + 800, backgroundRect.Top + 1000);


            Text timeHeader = new Text();
            timeHeader.scale = new Vector2(3);
            timeHeader.position = backgroundRect.Center.ToVector2();
            timeHeader.alignment = Text.Alignment.Right;
            timeHeader.text = "The Quick";


            // Resume button
            Sprite resumeSprite = new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Play_BTN"));
            ImageButton resumeButton = new ImageButton(resumeSprite, resumeSprite);
            resumeButton.Click += ResumeButton_Click;
            resumeButton.scale = new Vector2(3);
            resumeButton.position = new Vector2(backgroundRect.Right - 500, backgroundRect.Bottom - 500);

            screen.controls = new List<AbstractControl>() { background, header, scoreHeader, scoreBack, resumeButton };
        }

        public static Boolean update()
        {
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);
        }


        private static void ResumeButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Playing);
        }

        private static void PauseButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Paused);
        }
    }
}
