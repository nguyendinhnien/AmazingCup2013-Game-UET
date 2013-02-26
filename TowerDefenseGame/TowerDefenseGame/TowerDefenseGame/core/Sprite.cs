using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseGame.core
{
    public class Sprite
    {
        
        //private float rotation = 0f;
        //private float scale = 1.0f;
        //private bool visible = true;
        
        protected Texture2D texture;       
        protected Vector2 position;

        protected Vector2 center;

        public Sprite(Texture2D texture, Vector2 center)
        {
            this.texture = texture;
            this.center = center;

            this.position.X = center.X - texture.Width / 2;
            this.position.Y = center.Y - texture.Height / 2;
        }

        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Center
        {
            get { return center; }
        }

        public Rectangle BoundingBox()
        {
            return texture.Bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.position.X = center.X - texture.Width / 2;
            this.position.Y = center.Y - texture.Height / 2;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
