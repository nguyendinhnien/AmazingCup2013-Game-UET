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

        private Button playButton;
        private Button pauseButton;
        private Button fastButton;
        private Button settingButton;

        public HUDLayer(GamePlayScene gameplay)
        {
            this.gameplay = gameplay;
        }
        public void LoadContent()
        {
            ContentManager Content = gameplay.SceneManager.Game.Content;
            Texture2D texture;
            Texture2D textureNormal;
            Texture2D texturePress;

            font = Content.Load<SpriteFont>(@"fonts\gameplay\hud_font");
            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\life_label");
            LifeLabel = new Label(texture, new Vector2(800,50));

            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\money_label");
            MoneyLabel = new Label(texture, new Vector2(20,35));

            textureNormal = Content.Load<Texture2D>(@"images\gameplay\buttons\play_normal_but");
            texturePress = Content.Load<Texture2D>(@"images\gameplay\buttons\play_press_but");
            playButton = new Button(textureNormal, null, texturePress, new Vector2(75,675));
            playButton.Clicked += PlayButton_Clicked;

            textureNormal = Content.Load<Texture2D>(@"images\gameplay\buttons\pause_normal_but");
            texturePress = Content.Load<Texture2D>(@"images\gameplay\buttons\pause_press_but");
            pauseButton = new Button(textureNormal, null, texturePress, new Vector2(130, 675));
            pauseButton.Clicked += PauseButton_Clicked;

            textureNormal = Content.Load<Texture2D>(@"images\gameplay\buttons\fast_normal_but");
            texturePress = Content.Load<Texture2D>(@"images\gameplay\buttons\fast_press_but");
            fastButton = new Button(textureNormal, null, texturePress, new Vector2(184,675));
            fastButton.Clicked += FastButton_Clicked;

            textureNormal = Content.Load<Texture2D>(@"images\gameplay\buttons\setting_normal_but");
            texturePress = Content.Load<Texture2D>(@"images\gameplay\buttons\setting_press_but");
            settingButton = new Button(textureNormal, null, texturePress, new Vector2(246,675));
            settingButton.Clicked += SettingButton_Clicked;
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            
        }
        private void PauseButton_Clicked(object sender, EventArgs e)
        {

        }
        private void FastButton_Clicked(object sender, EventArgs e)
        {

        }
        private void SettingButton_Clicked(object sender, EventArgs e)
        {

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
            
            LifeLabel.Draw(spriteBatch);
            MoneyLabel.Draw(spriteBatch);
            
            playButton.Draw(spriteBatch);
            pauseButton.Draw(spriteBatch);
            fastButton.Draw(spriteBatch);
            settingButton.Draw(spriteBatch);
        }
    }
}
