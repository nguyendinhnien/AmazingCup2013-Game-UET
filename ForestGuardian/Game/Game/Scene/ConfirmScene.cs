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
    public class ConfirmScene : GameScene
    {
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Button yesButton;
        private Button noButton;

        public ConfirmScene()
            : base()
        {
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\ConfirmScene\confirm_form");
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);

            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");

            Texture2D texture = content.Load<Texture2D>(@"images\scene\ConfirmScene\b_yes");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\ConfirmScene\b_yes_clicked");
            Vector2 position = backgroundPosition + new Vector2(170, 173);
            yesButton = new Button(texture, null, pressTexture, position);
            yesButton.Clicked += YesButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\ConfirmScene\b_no");
            pressTexture = content.Load<Texture2D>(@"images\scene\ConfirmScene\b_no_clicked");
            position = backgroundPosition + new Vector2(380, 173);
            noButton = new Button(texture, null, pressTexture, position);
            noButton.Clicked += NoButtonClicked;
        }

        public override void Update(GameTime gameTime)
        {
            yesButton.Update(gameTime);
            noButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);

            yesButton.Draw(spriteBatch);
            noButton.Draw(spriteBatch);

            spriteBatch.End();
        }


        private void NoButtonClicked(object sender, EventArgs e)
        {
            this.ExitScene();
        }

        private void YesButtonClicked(object sender, EventArgs e)
        {
            SceneManager.ExitToMainMenu();
            //SceneManager.Game.Exit();
        }
    }
}