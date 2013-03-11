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
        protected Texture2D mTexture;       
        protected Vector2 mPosition;
        protected Vector2 mCenter;

        protected float layer_depth;
        public float mRotation = 0.0f;
        public float mScale = 1.0f;

        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        public Vector2 Center
        {
            get { return mCenter; }
            set { mPosition = value; }
        }

        public Sprite(Texture2D texture)
        {
            this.mTexture = texture;
        }

        public Sprite(Texture2D texture, Vector2 center)
        {
            this.mTexture = texture;
            this.mCenter = center;

            this.mPosition.X = center.X - texture.Width / 2;
            this.mPosition.Y = center.Y - texture.Height / 2;
        }

        public Rectangle BoundingBox()
        {
            return mTexture.Bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.mPosition.X = mCenter.X - mTexture.Width / 2;
            this.mPosition.Y = mCenter.Y - mTexture.Height / 2;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, layer_depth);
            spriteBatch.Draw(mTexture, mPosition, Color.White);
        }
    }
}
