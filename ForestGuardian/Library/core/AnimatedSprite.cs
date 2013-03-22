using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class AnimatedSprite
    {
        protected Animation animation;
        protected int currentFrameIndex = 0;
        protected float timer = 0.0f;
        protected bool done = false;

        protected Vector2 mPosition;
        protected Vector2 mCenter;

        protected float layer_depth;
        public float mRotation = 0.0f;
        public float mScale = 1.0f;

        public AnimatedSprite() { }
        public AnimatedSprite(Vector2 center)
        {
            mCenter = center;
        }
        public AnimatedSprite(Animation animation)
        {
            this.animation = animation;
        }
        public AnimatedSprite(Animation animation, Vector2 center)
            : this(animation, center, Anchor.CENTER) { }
        public AnimatedSprite(Animation animation, Vector2 position, Anchor anchor)
        {
            this.animation = animation;
            int width = animation.FrameWidth;
            int height = animation.FrameHeight;

            switch (anchor)
            {
                case Anchor.TOPLEFT:
                    this.mCenter.X = position.X + width / 2;
                    this.mCenter.Y = position.Y + height / 2;
                    break;
                case Anchor.TOPRIGHT:
                    this.mCenter.X = position.X - width / 2;
                    this.mCenter.Y = position.Y + height / 2;
                    break;
                case Anchor.CENTER:
                    this.mCenter = position;
                    break;
                case Anchor.BOTTOMLEFT:
                    this.mCenter.X = position.X + width / 2;
                    this.mCenter.Y = position.Y - height / 2;
                    break;
                case Anchor.BOTTOMRIGHT:
                    this.mCenter.X = position.X - width / 2;
                    this.mCenter.Y = position.Y - height / 2;
                    break;
            }
        }
        
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

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer > animation.FrameTime)
            {
                timer -= animation.FrameTime;
                if (animation.IsLoop)
                {
                    currentFrameIndex = (currentFrameIndex + 1) % animation.FrameNumber;
                }
                else
                {
                    currentFrameIndex = Math.Min(currentFrameIndex + 1, animation.FrameNumber - 1);
                    if (currentFrameIndex >= animation.FrameNumber - 1)
                    {
                        done = true;
                    }
                }
            }
            this.mPosition.X = mCenter.X - animation.FrameWidth / 2;
            this.mPosition.Y = mCenter.Y - animation.FrameHeight / 2;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animation == null) return;
            spriteBatch.Draw(animation.Texture, mPosition, animation.Frames[currentFrameIndex], Color.White, mRotation, Vector2.Zero, mScale, SpriteEffects.None, layer_depth);
        }
    }
}
