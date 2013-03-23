using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
namespace CustomGame.Scene
{
    public class VictoryLayer: EndGameLayer
    {
        private Label VictoryLabel;

        public VictoryLayer(GamePlayScene gameplay)
            : base(gameplay) { }

        public override void LoadContent()
        {
            ContentManager Content = gameplay.SceneManager.Game.Content;
            Texture2D texture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\victory_label");
            VictoryLabel = new Label();
            VictoryLabel.Texture = texture;
            VictoryLabel.Center = new Vector2(512, 100);            
            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            VictoryLabel.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
