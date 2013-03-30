using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Library;
namespace CustomGame
{
    public class GameTipScene: GameScene
    {
        private Label tip_label;

        public GameTipScene():base()
        {
            isPopup = true;
        }
        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;
            Texture2D texture = Content.Load<Texture2D>(@"images\scene\GameTipScene\control_map_tut");
            tip_label = new Label();
            tip_label.Texture = texture;
            tip_label.Center = new Vector2(512, 384);
        }
        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsMouseJustReleased())
            {
                this.ExitScene();            }        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                tip_label.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
