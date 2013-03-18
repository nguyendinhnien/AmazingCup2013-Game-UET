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

namespace Forest
{
    public class OptionScene : GameScreen
    {
        private static bool isFullScreen = false;

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Button backButton;

        public OptionScene() : base() 
        {
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\OptionScene\options");
            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\OptionScene\FadeScreen");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\OptionScene\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\b_back_clicked");

            Vector2 backPosition = backgroundPosition + new Vector2(50f,
                backgroundTexture.Height - 100);
            
            backButton = new Button(texture, null, pressTexture, backPosition);
            backButton.Clicked += BackButtonClicked;
        }

        public override void HandleInput(GameTime gameTime)
        {
            backButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            backButton.Draw(spriteBatch);

            spriteBatch.End();
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
    }
}