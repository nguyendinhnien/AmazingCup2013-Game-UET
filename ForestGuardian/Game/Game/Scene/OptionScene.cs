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
    public class OptionScene : GameScene
    {
        private static bool isFullScreen = false;

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Texture2D tickTexture;
        private Vector2 tickPosition;

        private Button closeButton;
        private Button increButton;
        private Button decreButton;

        public OptionScene() : base() 
        {
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\OptionScene\dialog_options");
            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\OptionScene\FadeScreen");
            tickTexture = content.Load<Texture2D>(@"images\scene\OptionScene\tick_full_screen");
            tickPosition = backgroundPosition + new Vector2(70, 70);

            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\OptionScene\b_close");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\b_close_clicked");
            Vector2 position = backgroundPosition + new Vector2(backgroundTexture.Width - 70, 10);
            closeButton = new Button(texture, null, pressTexture, position);
            closeButton.Clicked += CloseButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\OptionScene\decrease_volume");
            pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\decrease_volume_clicked");
            position = backgroundPosition + new Vector2(290, 190);
            increButton = new Button(texture, null, pressTexture, position);

            texture = content.Load<Texture2D>(@"images\scene\OptionScene\increase_volume");
            pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\increase_volume_clicked");
            position = backgroundPosition + new Vector2(620, 183);
            decreButton = new Button(texture, null, pressTexture, position);
        }

        public override void Update(GameTime gameTime)
        {
            closeButton.Update(gameTime);
            increButton.Update(gameTime);
            decreButton.Update(gameTime);

            if (InputManager.IsMouseJustReleased() && InputManager.IsMouseHittedRectangle(new Rectangle(
                (int)tickPosition.X, (int)tickPosition.Y, tickTexture.Width, tickTexture.Height)))
            {
                isFullScreen = !isFullScreen;
                SceneManager.ToggleFullScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            closeButton.Draw(spriteBatch);
            increButton.Draw(spriteBatch);
            decreButton.Draw(spriteBatch);
            if (isFullScreen)
                spriteBatch.Draw(tickTexture,tickPosition, Color.White);

            spriteBatch.End();
        }

        private void CloseButtonClicked(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
    }
}