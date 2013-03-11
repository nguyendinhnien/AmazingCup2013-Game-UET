using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Scene
    {
        protected GameManager game;

        public Scene(GameManager game)
        {
            this.game = game;
        }

        public ContentManager Content
        {
            get { return game.Content; }
        }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch){ }
    }
}
