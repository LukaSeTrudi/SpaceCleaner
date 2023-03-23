using GameEngine.Graphics;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Managers;
using SpaceCleaner.Resources.Prefabs;
using SpaceCleanerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCleaner.Resources.Interfaces
{
    public class GameInterface
    {
        public static InterfaceScreen screen = new InterfaceScreen();
        private static List<Image> healthDots = new List<Image>();
        private static List<Image> shieldDots = new List<Image>();
        public static Text scoreBottomText;
        private static Text timeBottomText;
        private static Text crystalsBottomText;

        private static AbsoluteLayout powerupsLayout;
    
        public static void init(ContentManager contentManager)
        {
            // Pause button
            ImageButton pauseButton = new ImageButton(new Sprite(contentManager.Load<Texture2D>("gui/controls/BTNs/Pause_BTN")), new Sprite(contentManager.Load<Texture2D>("gui/controls/BTNs_Active/Pause_BTN")));
            pauseButton.setTopRight(350, 350);
            pauseButton.Click += PauseButton_Click;


            // Stats menu
            Image statsImage = new Image(new Sprite(contentManager.Load<Texture2D>("gui/controls/Stats_Bar")));
            statsImage.scale = new Vector2(3, 4);
            statsImage.setBottom(100);

            // Stats score
            scoreBottomText = new Text();
            scoreBottomText.scale = new Vector2(2);
            scoreBottomText.setCenter();
            scoreBottomText.setBottom(250);
            scoreBottomText.alignment = Text.Alignment.Center;
            scoreBottomText.text = Game1.Instance.gameLoader.playerScore + "";

            Image timeImage = new Image(new Sprite(contentManager.Load<Texture2D>("gui/controls/Clock_Icon")));
            timeImage.scale = new Vector2(3);
            timeImage.setBottomLeft(250, 800);

            Image crystalImage = new Image(new Sprite(contentManager.Load<Texture2D>("gui/controls/Cristal_Icon")));
            crystalImage.scale = new Vector2(3);
            crystalImage.setBottomRight(250, 1350);

            timeBottomText = new Text();
            timeBottomText.setBottomLeft(260, 1200);
            timeBottomText.scale = new Vector2(1.5f);
            timeBottomText.alignment = Text.Alignment.Center;
            timeBottomText.text = "0:00";

            crystalsBottomText = new Text();
            crystalsBottomText.setBottomRight(250, 1200);
            crystalsBottomText.scale = new Vector2(1.5f);
            crystalsBottomText.alignment = Text.Alignment.Left;
            crystalsBottomText.text = "0";

            // Health bar
            Image healthImage = new Image(new Sprite(contentManager.Load<Texture2D>("gui/controls/Health_Bar_Table")));
            healthImage.scale = new Vector2(4);
            healthImage.origin = Vector2.Zero;
            healthImage.setTopLeft(150, 150);

            // Armor bar
            Image armorImage = new Image(new Sprite(contentManager.Load<Texture2D>("gui/controls/Armor_Bar_Table")));
            armorImage.scale = new Vector2(4);
            armorImage.origin = Vector2.Zero;
            armorImage.setTopLeft(500, 150);


            // Powerups
            powerupsLayout = new AbsoluteLayout();

            screen.controls = new List<AbstractControl>() { pauseButton, statsImage, healthImage, armorImage, scoreBottomText, timeImage, crystalImage, timeBottomText, crystalsBottomText, powerupsLayout };

            //dinamic
            Texture2D healthBarTexture = contentManager.Load<Texture2D>("gui/controls/Health_Dot");
            healthDots = new List<Image>();
            for (int i = 0; i < 11; i++)
            {
                Image healthDot = new Image(new Sprite(healthBarTexture));
                healthDot.scale = new Vector2(4, 4);
                healthDot.origin = Vector2.Zero;
                healthDot.setTopLeft(175, 175 + i * 120);
                healthDots.Add(healthDot);
            }

            Texture2D armorBarTexture = contentManager.Load<Texture2D>("gui/controls/Armor_Bar_Dot");
            shieldDots = new List<Image>();
            for (int i = 0; i < 8; i++)
            {
                Image shieldDot = new Image(new Sprite(armorBarTexture));
                shieldDot.scale = new Vector2(4, 4);
                shieldDot.origin = Vector2.Zero;
                shieldDot.setTopLeft(520, 175 + i * 120);
                shieldDots.Add(shieldDot);

            }
        }
        public static Boolean update()
        {
            // formatted minutes and seconds in format 00:00
            int minutes = (int)Game1.Instance.playingTime.ElapsedGameTime.TotalMinutes;
            int seconds = (int)Game1.Instance.playingTime.ElapsedGameTime.TotalSeconds % 60;
            timeBottomText.text = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);

            // powerups

            powerupsLayout.children.Clear();
            Vector2 currentPos = new Vector2(500, 2000);
            int spacing = 400;
            foreach (AbstractPowerUp powerup in PowerUpManager.Instance.activePowerups)
            {
                Image pw = new Image(new Sprite(powerup.icon));
                Image bg = new Image(PowerUpManager.Instance.powerUpBackground);

                pw.scale = new Vector2(1f);
                bg.scale = new Vector2(2f);

                pw.position = currentPos;
                pw.opacity = (float)powerup.secondsLeft / powerup.secondsLast;
                bg.position = currentPos;

                powerupsLayout.addControl(bg);
                powerupsLayout.addControl(pw);

                currentPos.Y += bg.getBounds().Height / 2 + spacing;

            }
            return screen.checkForInputs();
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            screen.draw(spriteBatch);

            for (int i = 0; i < PlayerGO.Instance.Health; i++)
                healthDots[i].drawControl(spriteBatch);

            for (int i = 0; i < PlayerGO.Instance.Shields; i++)
                shieldDots[i].drawControl(spriteBatch);
        }

        private static void PauseButton_Click(object sender, EventArgs e)
        {
            Game1.Instance.changeState(Game1.GameState.Paused);
        }
    }
}
