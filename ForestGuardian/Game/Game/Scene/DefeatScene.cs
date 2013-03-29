using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
namespace CustomGame
{
    public class DefeatScene: EndGameScene
    {
        private Label DefeatLabel;

        public DefeatScene(int total_point, int total_kill, string map_name)
            :base(total_point,total_kill, map_name){}

        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;
            Texture2D texture = Content.Load<Texture2D>(@"images\scene\EndGameScene\defeat_label");
            DefeatLabel = new Label();
            DefeatLabel.Texture = texture;
            DefeatLabel.Center = new Vector2(512, 100);
            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            DefeatLabel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
