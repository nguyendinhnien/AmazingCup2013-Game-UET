using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Sprite
    {     
        protected Texture2D texture;       
        protected Vector2 position;
        protected Vector2 center;

        protected float layer_depth;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
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
            set { position = value; }
        }
        public Vector2 Center
        {
            get { return center; }
            set { position = value; }
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
            //spriteBatch.Draw(texture, position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, layer_depth);
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
