using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

using Library;
using Data;
namespace CustomGame
{
    public class TextBoxScene : GameScene
    {
        protected Texture2D BackgroundTexture;
        protected Rectangle BackgroundRectangle;

        private TextBox textBox;
        private Button SaveButton;
        private Button CancelButton;
        private int total_points;

        public TextBoxScene(int total_points)
            : base()
        {
            this.total_points = total_points;
            isPopup = true;
        }
        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;

            BackgroundTexture = Content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            BackgroundRectangle = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
            
            Texture2D textbox_texture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\textbox_dialog");
            Texture2D caret_texture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\caret");
            SpriteFont text_font = Content.Load<SpriteFont>(@"fonts\TextBoxScene\textinput");
            textBox = new TextBox(textbox_texture, caret_texture, text_font);
            textBox.Center = new Vector2(512, 384);
            textBox.TextOffset = new Vector2(60, 95);
            textBox.Width = 500;
            textBox.OnEnterPressed += TextBoxEnter_Pressed;
            GameManager.keyboard_dispatcher.Subscriber = textBox;

            Texture2D normalTexture, pressTexture;
            normalTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_save_normal");
            pressTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_save_press");
            SaveButton = new Button(normalTexture, null, pressTexture, new Vector2(230, 420));
            SaveButton.Clicked += SaveButton_Clicked;

            normalTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_cancel_normal");
            pressTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_cancel_press");
            CancelButton = new Button(normalTexture, null, pressTexture, new Vector2(580, 420));
            CancelButton.Clicked += CancelButton_Clicked;

        }

        private void TextBoxEnter_Pressed(TextBox sender)
        {
            string player_name = textBox.Text;
            UserData.highscore.AddScore(new Score(player_name, total_points));
            DataSerializer.SaveData<HighScore>(UserData.highscore, UserData.HighScoreDirectory ,UserData.HighScoreFile);
            sceneManager.ExitToMainMenu();
            sceneManager.AddScene(new ScoreScene());
        }
        
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string player_name = textBox.Text;
            UserData.highscore.AddScore(new Score(player_name,total_points));
            DataSerializer.SaveData<HighScore>(UserData.highscore, UserData.HighScoreDirectory ,UserData.HighScoreFile);
            sceneManager.ExitToMainMenu();
            sceneManager.AddScene(new ScoreScene());
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            sceneManager.ExitToMainMenu();
        }

        public override void Update(GameTime gameTime)
        {
            SaveButton.Update(gameTime);
            CancelButton.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
                spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, null, Color.White);
                textBox.Draw(spriteBatch, gameTime);
                SaveButton.Draw(spriteBatch);
                CancelButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
