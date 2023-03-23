using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.GUI;
using GameEngine.Utils;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceCleaner.Resources;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Scenes;
using SpaceCleaner.Resources.Structure;
using SpaceCleanerGame.SpaceCleaner.Resources.Managers;

namespace SpaceCleanerGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static GamePlatform platform;
        public SpriteBatch spriteBatch;
        public static Game1 Instance;
        public GameState State;
        public Rectangle screen;
        public GameLoader gameLoader;

        public GameTime playingTime = new GameTime();
        private Texture2D backgroundTexture; 
        
        ParallaxImage parallax = new ParallaxImage();

        public enum GamePlatform
        {
            Desktop,
            Android
        };

        public enum GameState
        {
            Menu,
            Playing,
            Paused,
            Dead,
            Win,
            Map,
            Settings
        }

        public Game1()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            State = GameState.Menu;


            gameLoader = GameLoader.Load();
        }

        public void changeState(GameState state)
        {
            State = state;
        }

        public void restart()
        {
            changeState(GameState.Playing);
            playingTime = new GameTime();
            SceneManager.Instance.setScene(MainScene.LoadContent());
            PowerUpManager.Instance.activePowerups.Clear();
            
        }
        
        protected override void Initialize()
        {

            Resolution.Init(ref _graphics);

            int width = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            int height = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height - 100;

            Resolution.SetVirtualResolution(4320, 7680);
            float aspect = (float)4320 / 7680;


            Resolution.SetResolution((int)(height * aspect), height, false);
            
            ///Resolution.SetResolution(width, height, true);

            DebugRenderer.Init(GraphicsDevice);

            screen = new Rectangle(0, 0, 4320, 7680);

            new LevelManager(Content);
            new MapManager(Content);
            new ShipManager(Content);
            new MeteorManager(Content);
            new MissileManager(Content);
            new PowerUpManager(Content);
            new BossManager(Content);
            new SoundManager(Content);
            new LoadingBarManager(Content);

            parallax.addLayer(Content.Load<Texture2D>("map/Maps/Map_01/Layers/BG_1080x3000"), 0.5f);
            parallax.addLayer(Content.Load<Texture2D>("map/Maps/Map_01/Layers/Decor_A_1080x3000"), 1.2f, true);
            parallax.addLayer(Content.Load<Texture2D>("map/Maps/Map_01/Layers/Decor_B_1080x3000"), 2.3f, true);

            SceneManager.Instance.setScene(MainScene.LoadContent());
            MainScene.LoadLevelData(LevelManager.Instance.levelsData[1]);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = this.Content.Load<Texture2D>("gui/backgrounds/Space_BG_01");
            GameEngine.GUI.Text.defaultFont = Content.Load<SpriteFont>("font/ethnocentric_normal_72");
            SceneManager.Instance.currentScene.LoadContent(this.Content);
            InterfaceManager.init(Content);

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(!InterfaceManager.update())
            {
                InputManager.Update(gameTime);
            };
            

            if(State == GameState.Playing)
            {
                playingTime.ElapsedGameTime += gameTime.ElapsedGameTime;
                parallax.update();
                SceneManager.Instance.currentScene.Update(gameTime);
                PowerUpManager.Instance.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void startBatchDraw(Matrix otherMatrix)
        {
            spriteBatch.Begin(transformMatrix: Resolution.getTransformationMatrix() * otherMatrix);
        }

        public void endBatchDraw()
        {
            spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            spriteBatch.Begin(transformMatrix: Resolution.getTransformationMatrix());
            parallax.drawControl(spriteBatch);
            
            if (State == GameState.Playing || State == GameState.Paused || State == GameState.Dead || State == GameState.Win)
            {
                SceneManager.Instance.currentScene.Draw(gameTime, spriteBatch);
            }
            InterfaceManager.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}