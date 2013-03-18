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

        protected Vector2 target_center;
        protected Vector2 mVelocity;
        Vector2 mDirection;

        public float Speed
        {
            get { return speed; }
        }

        public bool Alive
        {
            get { return (age > 0); }
        }

        public Bullet(Texture2D texture, Vector2 center, float speed)
            : base(texture, center)
        {
            this.speed = speed;
        }

        public Bullet(Texture2D texture, Vector2 center, float speed, Vector2 target_center)
            : base(texture, center)
        {
            this.speed = speed;
            this.target_center = target_center;
        }

        public void Kill()
        {
            //alive = false;
            age = 0;
        }

        public void HitTarget()
        {

        }

        public void setTargetPos(Vector2 target_center)
        {
            this.target_center = target_center;
            mDirection = target_center - mCenter;
            age = (int)mDirection.Length();
            mDirection.Normalize();

            mVelocity = speed * mDirection;

            //Console.Write("Start Age: ");
            //Console.WriteLine(age.ToString());
        }

        public void Move()
        {
            mCenter += mVelocity;
            age -= (int)mVelocity.Length();
            //Console.Write("Age: ");
            //Console.WriteLine(age.ToString());
        }

        public void setRotation(float value)
        {
            mRotation = value;

            mVelocity = Vector2.Transform(mVelocity, Matrix.CreateRotationZ(mRotation));
        }

        public override void Update(GameTime gameTime)
        {
            if (target_center != null)
            {
                Move();
                //HitTarget();
                //alive = false;
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
