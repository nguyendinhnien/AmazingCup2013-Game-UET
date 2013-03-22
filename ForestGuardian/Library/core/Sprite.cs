using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public enum Anchor{
        TOPLEFT,
        TOPRIGHT,
        CENTER,
        BOTTOMLEFT,
        BOTTOMRIGHT
    }
    public class Sprite
    {
        protected Texture2D mTexture;
        protected Vector2 mPosition;
        protected Vector2 mCenter;

        protected float layer_depth;
        protected float mRotation = 0.0f;
        public float mScale = 1.0f;

        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        public Vector2 Center
        {
            get { return mCenter; }
            set { mCenter = value; }
        }

        public float LayerDepth
        {
            get { return layer_depth; }
            set { layer_depth = value; }
        }

        public float Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }

        public Sprite() { }

        public Sprite(Vector2 center)
        {
            mCenter = center;
        }

        public Sprite(Texture2D texture)
        {
            this.mTexture = texture;
        }

        public Sprite(Texture2D texture, Vector2 center)
            :this(texture,center,Anchor.CENTER){}

        public Sprite(Texture2D texture, Vector2 position, Anchor anchor)
        {
            this.mTexture = texture;
            switch (anchor)
            {
                case Anchor.TOPLEFT:
                    this.mCenter.X = position.X + texture.Width / 2;
                    this.mCenter.Y = position.Y + texture.Height / 2;
                    break;
                case Anchor.TOPRIGHT:
                    this.mCenter.X = position.X - texture.Width / 2;
                    this.mCenter.Y = position.Y + texture.Height / 2;
                    break;
                case Anchor.CENTER:
                    this.mCenter = position;
                    break;
                case Anchor.BOTTOMLEFT:
                    this.mCenter.X = position.X + texture.Width / 2;
                    this.mCenter.Y = position.Y - texture.Height / 2;
                    break;
                case Anchor.BOTTOMRIGHT:
                    this.mCenter.X = position.X - texture.Width / 2;
                    this.mCenter.Y = position.Y - texture.Height / 2;
                    break;
            }
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
            spriteBatch.Draw(mTexture, mPosition, null, Color.White, mRotation, Vector2.Zero, mScale, SpriteEffects.None, layer_depth);
        }
    }
}
