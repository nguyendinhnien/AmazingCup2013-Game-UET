using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Bullet : Sprite
    {
        protected float speed;
        protected int age;
        protected int mDamage;

        protected Enemy mTarget;
        protected Vector2 mTargetCenter;
        protected Vector2 mVelocity;
        Vector2 mDirection;

        protected bool mHit;

        public float Speed
        {
            get { return speed; }
        }

        public bool Alive
        {
            get { return (age > 0); }
        }

        public int Damage
        {
            get { return mDamage; }
        }

        public Bullet(Texture2D texture, Vector2 center, float speed, int pDamage)
            : base(texture, center, Anchor.CENTER)
        {
            this.speed = speed;
            mDamage = pDamage;
            mHit = false;
        }

        public virtual void HitTarget(Enemy pEnemy)
        {
            if (!mHit)
            {
                pEnemy.lostHealth(mDamage);
                mHit = true;
            }
        }

        public void setTarget(Enemy pTarget)
        {
            mTarget = pTarget;
            mTargetCenter = mTarget.Center;

            mDirection = mTarget.Center - mCenter;
            age = (int)mDirection.Length();
        }

        public void Move()
        {
            if (mTarget != null)
            {
                mDirection = mTarget.Center - mCenter;
                mTargetCenter = mTarget.Center;

                if (mDirection.Length() < 15)
                {
                    age = -1;
                    mTarget = null;
                }
                else
                {
                    mDirection.Normalize();

                    mVelocity = speed * mDirection;
                    mCenter += mVelocity;
                    age -= (int)mVelocity.Length();
                }
            }
            else
            {
                mDirection = mTargetCenter - mCenter;
                Console.WriteLine(mDirection.Length());
                if (mDirection.Length() < 15)
                {
                    age = -1;
                }
                else
                {
                    mDirection.Normalize();

                    mVelocity = speed * mDirection;
                    mCenter += mVelocity;
                    age -= (int)mVelocity.Length();
                }
            }
        }

        public void setRotation(float value)
        {
            mRotation = value;

            mVelocity = Vector2.Transform(mVelocity, Matrix.CreateRotationZ(mRotation));
        }

        public override void Update(GameTime gameTime)
        {
            if (age > 0)
            {
                Move();
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (age > 0)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
