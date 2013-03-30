using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Library;
namespace CustomGame
{
    public class EndGameScene : GameScene
    {
        //protected GamePlayScene gameplay;
        protected Label MissionLabel;
        protected Button RestartButton;
        protected Button ContinueButton;
        protected Texture2D BackgroundTexture;
        protected Rectangle BackgroundRectangle;

        protected SpriteFont font;
        protected string map_name;
        protected Vector2 MapNamePosition; 
        protected int total_points;
        protected Vector2 TotalPointPosition; 
        protected int total_killed;
        protected Vector2 TotalKillPosition;

        public EndGameScene(int total_points, int total_killed, string map_name):base()
        {
            this.total_points = total_points;
            this.total_killed = total_killed;
            this.map_name = map_name;
            isPopup = true;
        }
        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;
            Texture2D normalTexture,pressTexture;

            BackgroundTexture = Content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            BackgroundRectangle = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);

            normalTexture = Content.Load<Texture2D>(@"images\scene\EndGameScene\mission_finished");
            MissionLabel = new Label();
            MissionLabel.Texture = normalTexture;
            MissionLabel.Center = new Vector2(512, 450);

            normalTexture = Content.Load<Texture2D>(@"images\scene\EndGameScene\replay_normal_but");
            pressTexture = Content.Load<Texture2D>(@"images\scene\EndGameScene\replay_press_but");
            RestartButton = new Button(normalTexture, null, pressTexture, new Vector2(280, 580));
            RestartButton.Clicked += RestartButton_Clicked;

            normalTexture = Content.Load<Texture2D>(@"images\scene\EndGameScene\continue_normal_but");
            pressTexture = Content.Load<Texture2D>(@"images\scene\EndGameScene\continue_press_but");
            ContinueButton = new Button(normalTexture, null, pressTexture, new Vector2(560, 580));
            ContinueButton.Clicked += ContinueButton_Clicked;

            font = Content.Load<SpriteFont>(@"fonts\EndGameScene\end_game");
        }

        private void RestartButton_Clicked(object sender, EventArgs e)
        {
            this.ExitScene();
            MediaPlayer.Stop();
            GamePlayScene.Instance.LoadNewGame();
        }
        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            this.sceneManager.AddScene(new TextBoxScene(total_points));
            MediaPlayer.Stop();
            this.ExitScene();
        }

        public override void Update(GameTime gameTime)
        {
            RestartButton.Update(gameTime);
            ContinueButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, null, Color.White ,0.0f,Vector2.Zero,SpriteEffects.None,1.0f);
                MissionLabel.Draw(spriteBatch);  

                RestartButton.Draw(spriteBatch);
                ContinueButton.Draw(spriteBatch);

                MapNamePosition = new Vector2(512, 300);
                MapNamePosition.X -= (font.MeasureString(map_name)).X / 2;
                TotalPointPosition = new Vector2(670,480); 
                TotalPointPosition.X -= (font.MeasureString(total_points.ToString())).X/2;
                TotalKillPosition = new Vector2(670,530);
                TotalKillPosition.X -= (font.MeasureString(total_killed.ToString())).X/2;

                spriteBatch.DrawString(font, map_name, MapNamePosition, Color.Red, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(font, total_points.ToString(), TotalPointPosition, Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(font, total_killed.ToString(), TotalKillPosition, Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
            spriteBatch.End();
        }
    }   
}
