using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Interfaces;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleanerGame.SpaceCleaner.Resources.Interfaces
{
    public class LevelInfoInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();

        public static Boolean enabled = false;
        
        private static Texture2D backgroundTexture;
        private static Texture2D star1Texture;
        private static Texture2D star2Texture;
        private static Texture2D star3Texture;

        private static Texture2D scoreHeaderTexture;
        private static Texture2D scoreRecordHeaderTexture;
        private static Texture2D scoreBackTexture;

        private static Texture2D playTexture;
        private static Texture2D exitTexture;

        private static LevelInfoEventArgs levelInfoEventArgs;

        public static void init(ContentManager contentManager)
        {
            backgroundTexture = contentManager.Load<Texture2D>("gui/win/Window");
            star1Texture = contentManager.Load<Texture2D>("gui/win/Star_01");
            star2Texture = contentManager.Load<Texture2D>("gui/win/Star_02");
            star3Texture = contentManager.Load<Texture2D>("gui/win/Star_03");

            scoreHeaderTexture = contentManager.Load<Texture2D>("gui/win/Score");
            scoreRecordHeaderTexture = contentManager.Load<Texture2D>("gui/win/Record");
            scoreBackTexture = contentManager.Load<Texture2D>("gui/win/Table");

            playTexture = contentManager.Load<Texture2D>("gui/win/Play_BTN");
            exitTexture = contentManager.Load<Texture2D>("gui/win/Close_BTN");
        }

        private static void PlayButton_Click(object sender, EventArgs e)
        {
            MainScene.LoadLevelData(LevelManager.Instance.getLevelData(levelInfoEventArgs.level));
            Game1.Instance.restart();
            // Game1.Instance.changeState(Game1.GameState.Map);
        }
        
        public static Boolean update()
        {
            return screen.checkForInputs();
        }

        public static void setLevel(LevelInfoEventArgs levelArgs)
        {
            Image background = new Image(new Sprite(backgroundTexture));
            background.setCenter();
            background.scale = new Vector2(4);

            levelInfoEventArgs = levelArgs;
            
            Rectangle backgroundRect = background.getBounds();

            // pause text

            Text headerText = new Text();
            headerText.text = "Level " + (levelArgs.level + 1);
            headerText.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 300);
            headerText.scale = new Vector2(4);

            // Stars
            Image star1 = new Image(new Sprite(star1Texture));
            star1.scale = new Vector2(3);
            star1.position = new Vector2(backgroundRect.Center.X - 1000, backgroundRect.Top + 1500);


            Image star2 = new Image(new Sprite(star1Texture));
            star2.scale = new Vector2(4);
            star2.position = new Vector2(backgroundRect.Center.X, backgroundRect.Top + 1300);


            Image star3 = new Image(new Sprite(star1Texture));
            star3.scale = new Vector2(3);
            star3.position = new Vector2(backgroundRect.Center.X + 1000, backgroundRect.Top + 1500);
            
            if(levelArgs.stars == 1)
            {
                star1.imageSprite.texture = star2Texture;
            }
            if (levelArgs.stars == 2)
            {
                star1.imageSprite.texture = star2Texture;
                star2.imageSprite.texture = star2Texture;
            }
            else if (levelArgs.stars == 3)
            {
                star1.imageSprite.texture = star3Texture;
                star2.imageSprite.texture = star3Texture;
                star3.imageSprite.texture = star3Texture;
            }
            

            // score
            Image scoreHeader = new Image(new Sprite(scoreHeaderTexture));
            scoreHeader.scale = new Vector2(4);
            scoreHeader.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 2400);

            Image scoreBack = new Image(new Sprite(scoreBackTexture));
            scoreBack.scale = new Vector2(4);
            scoreBack.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2400);

            Text scoreText = new Text();
            scoreText.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2375);
            scoreText.text = levelArgs.score.ToString();

            // score record
            Image scoreHeaderRecord = new Image(new Sprite(scoreRecordHeaderTexture));
            scoreHeaderRecord.scale = new Vector2(4);
            scoreHeaderRecord.position = new Vector2(backgroundRect.Center.X - 800, backgroundRect.Top + 3000);

            Image scoreBackRecord = new Image(new Sprite(scoreBackTexture));
            scoreBackRecord.scale = new Vector2(4);
            scoreBackRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 3000);

            Text scoreTextRecord = new Text();
            scoreTextRecord.position = new Vector2(backgroundRect.Center.X + 700, backgroundRect.Top + 2975);
            scoreTextRecord.text = levelArgs.recordScore.ToString();

            // Resume button
            Sprite playSprite = new Sprite(playTexture);
            ImageButton playButton = new ImageButton(playSprite, playSprite);
            playButton.Click += PlayButton_Click;
            playButton.scale = new Vector2(3);
            playButton.position = new Vector2(backgroundRect.Right - 500, backgroundRect.Bottom - 500);

            Sprite exitSprite = new Sprite(exitTexture);
            ImageButton exitButton = new ImageButton(exitSprite, exitSprite);
            exitButton.Click += ExitButton_Click;
            exitButton.scale = new Vector2(3);
            exitButton.position = new Vector2(backgroundRect.Left + 500, backgroundRect.Bottom - 500);


            screen.controls = new List<AbstractControl>() { background, headerText, scoreHeader, scoreBack, scoreText, playButton, star1, star2, star3, scoreHeaderRecord, scoreBackRecord, scoreTextRecord, exitButton };
        }

        private static void ExitButton_Click(object sender, EventArgs e)
        {
            enabled = false;
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);
        }
    }
}
