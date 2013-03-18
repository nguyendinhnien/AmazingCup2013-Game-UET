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
        protected int damage;
        protected float speed;
        //protected int max_age;
        protected int age;
        //protected bool alive;

        protected Vector2 target_center;
        protected Vector2 mVelocity;
        Vector2 mDirection;

        public float Speed
        {
            get { return speed; }
        }
        public int Damage
        {
            get { return damage; }
        }

        public bool Alive
        {
            get { return (age > 0); }
        }
        
        public Bullet(Texture2D texture, Vector2 center, int damage, float speed, int max_age)
            :base(texture,center)
        {
            this.damage = damage;
            this.speed = speed;
            this.age = max_age;
        }

        public Bullet(Texture2D texture, Vector2 center, int damage, float speed,int max_age, Vector2 target_center)
            :base(texture,center)
        {
            this.damage = damage;
            this.speed = speed;       
            //this.max_age = max_age;
            this.age = max_age;
            //this.alive = true;
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

            Console.Write("Start Age: ");
            Console.WriteLine(age.ToString());
        }
        
        public void Move()
        {
            mCenter += mVelocity;
            age -= (int)mVelocity.Length();
            Console.Write("Age: ");
            Console.WriteLine(age.ToString());
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
