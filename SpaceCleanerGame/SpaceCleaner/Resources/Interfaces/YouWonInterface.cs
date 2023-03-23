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
    public static class YouWonInterface
    {

        public static InterfaceScreen screen = new InterfaceScreen();

        public static Text scoreText;
        public static void init(ContentManager contentManager)
        {
            // background pause
            Image background = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Window")));
            background.setCenter();
            background.scale = new Vector2(4);

            Rectangle backgroundRect = background.getBounds();

            // pause text
            Image header = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Header")));
            header.scale = new Vector2(4);
            header.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 300);

            // Stars
            Image star1 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Star_03")));
            star1.scale = new Vector2(3);
            star1.position = new Vector2(backgroundRect.Center.X - 1000, backgroundRect.Top + 1500);


            Image star2 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Star_03")));
            star2.scale = new Vector2(4);
            star2.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 1300);


            Image star3 = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Star_02")));
            star3.scale = new Vector2(3);
            star3.position = new Vector2(backgroundRect.Center.X + 1000, backgroundRect.Top + 1500);

            // score
            Image scoreHeader = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Score")));
            scoreHeader.scale = new Vector2(4);
            scoreHeader.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 2400);

            Image scoreBack = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Table")));
            scoreBack.scale = new Vector2(4);
            scoreBack.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2400);

            scoreText = new Text();
            scoreText.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2375);
            scoreText.text = "0";

            // score record
            Image scoreHeaderRecord = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Record")));
            scoreHeaderRecord.scale = new Vector2(4);
            scoreHeaderRecord.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 3000);

            Image scoreBackRecord = new Image(new Sprite(contentManager.Load<Texture2D>("gui/win/Table")));
            scoreBackRecord.scale = new Vector2(4);
            scoreBackRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 3000);

            Text scoreTextRecord = new Text();
            scoreTextRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2975);
            scoreTextRecord.text = "569";

            Text timeHeader = new Text();
            timeHeader.scale = new Vector2(3);
            timeHeader.position = backgroundRect.Center.ToVector2();
            timeHeader.alignment = Text.Alignment.Right;
            timeHeader.text = "The Quick";


            // Resume button
            Sprite playSprite = new Sprite(contentManager.Load<Texture2D>("gui/pauseMenu/Play_BTN"));
            ImageButton playButton = new ImageButton(playSprite, playSprite);
            playButton.Click += PlayButton_Click;
            playButton.scale = new Vector2(3);
            playButton.position = new Vector2(backgroundRect.Right - 500, backgroundRect.Bottom - 500);
            

            screen.controls = new List<AbstractControl>() { background, header, scoreHeader, scoreBack, scoreText, playButton, star1, star2, star3, scoreHeaderRecord, scoreBackRecord, scoreTextRecord };
        }

        private static void PlayButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.restart();
            Game1.Instance.changeState(Game1.GameState.Map);
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
