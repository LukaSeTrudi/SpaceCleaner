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
    public static class YouLoseInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();

        public static Text scoreText;
        public static void init(ContentManager contentManager)
        {
            // background pause
            Image background = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Window")));
            background.setCenter();
            background.scale = new Vector2(4);

            Rectangle backgroundRect = background.getBounds();

            // dead text
            Image header = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Header")));
            header.scale = new Vector2(4);
            header.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 300);


            // Stars
            Image star1 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Star_01")));
            star1.scale = new Vector2(3);
            star1.position = new Vector2(backgroundRect.Center.X - 1000, backgroundRect.Top + 1500);


            Image star2 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Star_01")));
            star2.scale = new Vector2(4);
            star2.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 1300);


            Image star3 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Star_01")));
            star3.scale = new Vector2(3);
            star3.position = new Vector2(backgroundRect.Center.X + 1000, backgroundRect.Top + 1500);

            // score
            Image scoreHeader = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Score")));
            scoreHeader.scale = new Vector2(4);
            scoreHeader.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 2400);

            Image scoreBack = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Table")));
            scoreBack.scale = new Vector2(4);
            scoreBack.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2400);

            scoreText = new Text();
            scoreText.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2375);
            scoreText.text = "0";

            // score record
            Image scoreHeaderRecord = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Record")));
            scoreHeaderRecord.scale = new Vector2(4);
            scoreHeaderRecord.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 3000);

            Image scoreBackRecord = new Image(new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Table")));
            scoreBackRecord.scale = new Vector2(4);
            scoreBackRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 3000);

            Text scoreTextRecord = new Text();
            scoreTextRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2975);
            scoreTextRecord.text = "569";
            
            // Replay button
            Sprite replaySprite = new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Replay_BTN"));
            ImageButton replayButton = new ImageButton(replaySprite, replaySprite);
            replayButton.Click += ReplayButton_Click; ;
            replayButton.scale = new Vector2(3);
            replayButton.position = new Vector2(backgroundRect.Right - 500, backgroundRect.Bottom - 500);

            // Home button
            Sprite homeSprite = new Sprite(contentManager.Load<Texture2D>("gui/deadMenu/Close_BTN"));
            ImageButton homeButton = new ImageButton(homeSprite, homeSprite);
            homeButton.Click += HomeButton_Click;
            homeButton.scale = new Vector2(3);
            homeButton.position = new Vector2(backgroundRect.Left + 500, backgroundRect.Bottom - 500);

            screen.controls = new List<AbstractControl>() { background, header, star1, star2, star3, scoreBack, scoreHeader, scoreText, scoreBackRecord, scoreHeaderRecord, scoreTextRecord, replayButton, homeButton };
        }

        private static void HomeButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.restart();
            Game1.Instance.changeState(Game1.GameState.Menu);
        }

        private static void ReplayButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.restart();
        }

        public static Boolean update()
        {
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);
        }
    }
}
