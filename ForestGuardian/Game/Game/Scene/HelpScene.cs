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
    public class HelpScene : GameScene
    {
private Button button;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        public HelpScene()
            : base()
        {
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\HelpScene\credit");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\HelpScene\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\HelpScene\b_back_clicked");
            button = new Button(texture, null, pressTexture, new Vector2(10, 670));

            button.Clicked += BackButtonClicked;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScreen();
        }

        public override void Update(GameTime gameTime)
        {
            button.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
                button.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
