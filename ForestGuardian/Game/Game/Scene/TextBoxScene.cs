using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
using Data;
namespace CustomGame
{
    public class TextBoxScene : GameScene
    {
        private TextBox textBox;
        private Button SaveButton;
        private Button CancelButton;

        public TextBoxScene()
            : base()
        {
            isPopup = true;
        }
        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;
            Texture2D textbox_texture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\textbox_dialog");
            Texture2D caret_texture = Content.Load<Texture2D>(@"images\scene\EndGameScene\caret");
            SpriteFont text_font = Content.Load<SpriteFont>(@"fonts\TextBoxScene\textinput");
            textBox = new TextBox(textbox_texture, caret_texture, text_font);
            textBox.Center = new Vector2(512, 384);
            textBox.TextOffset = new Vector2(60, 110);
            textBox.Width = 500;
            textBox.OnEnterPressed += TextBoxEnter_Pressed;

            Texture2D normalTexture, pressTexture;
            normalTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_save_normal");
            pressTexture = Content.Load<Texture2D>(@"images\scene\TextBoxScene\b_save_press");
            SaveButton = new Button(normalTexture, null, pressTexture, new Vector2(400, 400));
            SaveButton.Clicked += SaveButton_Clicked;

            GameManager.keyboard_dispatcher.Subscriber = textBox;

            base.LoadContent();
        }

        private void TextBoxEnter_Pressed(TextBox sender)
        {
            Console.WriteLine(sender.Text);
        }
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            textBox.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
