using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Library; 

namespace CustomGame
{
    public class HUDLayer
    {
        private GamePlayScene gameplay;
        public SpriteFont font;
        private Label LifeLabel;
        private Label MoneyLabel;

        private Vector2 PointPosition;

        private ToggleButton playButton;
        private ToggleButton pauseButton;
        private ToggleButton fastButton;
        private ToggleButton settingButton;

        public HUDLayer(GamePlayScene gameplay)
        {
            this.gameplay = gameplay;
        }
        public void LoadContent()
        {
            ContentManager Content = gameplay.SceneManager.Game.Content;
            Texture2D texture;
            Texture2D textureEnable;
            Texture2D textureDisable;

            font = Content.Load<SpriteFont>(@"fonts\gameplay\hud_font");
            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\life_label");
            LifeLabel = new Label(texture, new Vector2(800,50));

            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\money_label");
            MoneyLabel = new Label(texture, new Vector2(20,35));

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\play_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\play_disable_but");
            playButton = new ToggleButton(textureEnable, null, null, textureDisable, new Vector2(75,675));
            playButton.Clicked += PlayButton_Clicked;
            playButton.Active = true;

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\pause_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\pause_disable_but");
            pauseButton = new ToggleButton(textureEnable, null, null, textureDisable, new Vector2(130, 675));
            pauseButton.Clicked += PauseButton_Clicked;
            pauseButton.Active = false;

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\fast_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\fast_disable_but");
            fastButton = new ToggleButton(textureEnable, null, null, textureDisable, new Vector2(184,675));
            fastButton.Clicked += FastButton_Clicked;
            fastButton.Active = false;

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\setting_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\setting_disable_but");
            settingButton = new ToggleButton(textureEnable,null, null, textureDisable, new Vector2(246,675));
            settingButton.Clicked += SettingButton_Clicked;
            settingButton.Active = false;
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            if (!playButton.Active)
            {
                playButton.Active = true;
                pauseButton.Active = false;
                Console.WriteLine("Game play");
            }
        }
        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            if (!pauseButton.Active)
            {
                pauseButton.Active = true;
                playButton.Active = false;
                Console.WriteLine("Game pause");
            }
        }
        private void FastButton_Clicked(object sender, EventArgs e)
        {
            if (fastButton.Active)
            {
                fastButton.Active = false;
                gameplay.SceneManager.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60);
            }
            else
            {
                fastButton.Active = true;
                gameplay.SceneManager.Game.TargetElapsedTime = TimeSpan.FromMilliseconds(10);
            }
        }
        private void SettingButton_Clicked(object sender, EventArgs e)
        {
            if (!settingButton.Active)
            {
                settingButton.Active = true;
                Console.WriteLine("Game Setting");
            }           
        }
        
        public void Update(GameTime gameTime)
        {
            playButton.Update(gameTime);
            pauseButton.Update(gameTime);
            fastButton.Update(gameTime);
            settingButton.Update(gameTime);
            PointPosition.X = 512 - (30*gameplay.Points.ToString().Length)/2;
            PointPosition.Y = 48;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameplay.Lives.ToString(), new Vector2(877, 49), Color.Gold,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,0.08f);
            spriteBatch.DrawString(font, gameplay.Money.ToString(), new Vector2(82, 49), Color.Gold, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
            spriteBatch.DrawString(font, gameplay.Points.ToString(), PointPosition, Color.Gold, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
            spriteBatch.DrawString(font, "WAVE: " + gameplay.CurrentWaveNumber.ToString(), new Vector2(20,120), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
            spriteBatch.DrawString(font, "TOTAL: " + gameplay.TotalWaveNumber.ToString(), new Vector2(20, 180), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);

            LifeLabel.Draw(spriteBatch);
            MoneyLabel.Draw(spriteBatch);
            
            playButton.Draw(spriteBatch);
            pauseButton.Draw(spriteBatch);
            fastButton.Draw(spriteBatch);
            settingButton.Draw(spriteBatch);
        }
    }
}
