using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Data;
using Library;

namespace CustomGame
{
    public class MenuPause : GameScene
    {
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Button resumeButton;
        private Button restartButton;
        private Button optionButton;
        private Button exitButton;

        public MenuPause()
            : base()
        {
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\MenuPause\menu");
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);
            
            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");

            Texture2D texture = content.Load<Texture2D>(@"images\scene\MenuPause\b_resume");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\MenuPause\b_resume_clicked");
            Vector2 position = backgroundPosition + new Vector2(49, 100);
            resumeButton = new Button(texture, null, pressTexture, position);
            resumeButton.Clicked += ResumeButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\MenuPause\b_reset");
            pressTexture = content.Load<Texture2D>(@"images\scene\MenuPause\b_reset_clicked");
            position = backgroundPosition + new Vector2(49, 200);
            restartButton = new Button(texture, null, pressTexture, position);
            restartButton.Clicked += ResetButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\MenuPause\b_options");
            pressTexture = content.Load<Texture2D>(@"images\scene\MenuPause\b_options_clicked");
            position = backgroundPosition + new Vector2(49, 300);
            optionButton = new Button(texture, null, pressTexture, position);
            optionButton.Clicked += OptionButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\MenuPause\b_exits");
            pressTexture = content.Load<Texture2D>(@"images\scene\MenuPause\b_exits_clicked");
            position = backgroundPosition + new Vector2(49, 400);
            exitButton = new Button(texture, null, pressTexture, position);
            exitButton.Clicked += ExitButtonClicked;
        }

        public override void Update(GameTime gameTime)
        {
            resumeButton.Update(gameTime);
            restartButton.Update(gameTime);
            optionButton.Update(gameTime);
            exitButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);

            resumeButton.Draw(spriteBatch);
            restartButton.Draw(spriteBatch);
            optionButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);

            spriteBatch.End();
        }


        private void ResumeButtonClicked(object sender, EventArgs e)
        {
            this.ExitScene();
        }

        private void ResetButtonClicked(object sender, EventArgs e)
        {
            //sceneManager.ResetGame();
            GamePlayScene.Instance.LoadNewGame();
            this.ExitScene();
        }

        private void OptionButtonClicked(object sender, EventArgs e)
        {
            SceneManager.AddScene(new OptionScene());
        }

        private void ExitButtonClicked(object sender, EventArgs e)
        {          
            SceneManager.AddScene(new ConfirmScene());
        }
    }
}