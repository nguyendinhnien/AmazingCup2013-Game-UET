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
    public class LoadingScene : GameScene
    {
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Texture2D loadingBarTexture;
        private Vector2 loadingBarPosition;

        private Texture2D loadingTextTexture;
        private Vector2 loadingTextPosition;

        private bool[] isDraw;
        private int start = 0;
        private float duration = 0;

        public LoadingScene()
            : base()
        {
            IsPopup = true;
            isDraw = new bool[10];

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\LoadingScene\loading_border");
            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");

            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);

            loadingBarTexture = content.Load<Texture2D>(@"images\scene\LoadingScene\loading_bar");
            loadingBarPosition = backgroundPosition + new Vector2(10, 9);

            loadingTextTexture = content.Load<Texture2D>(@"images\scene\LoadingScene\loading_text");
            loadingTextPosition = backgroundPosition + new Vector2(200, -70);
        }


        public override void Update(GameTime gameTime)
        {
            if (isDraw[9])
                SceneManager.AddScene(GamePlayScene.Instance);

            duration += (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 100);

            if (duration >= 1)
            {
                duration = 0;
                isDraw[start] = true;
                start++;
            }
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);

            for (int i = 0; i < 10; i++)
            {
                if (isDraw[i])
                    spriteBatch.Draw(loadingBarTexture, loadingBarPosition + new Vector2(69, 0) * i, Color.White);
            }

            spriteBatch.Draw(loadingTextTexture, loadingTextPosition, Color.White);

            spriteBatch.End();
        }
    }
}