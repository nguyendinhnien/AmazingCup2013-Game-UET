using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TowerDefenseGame.core;
using TowerDefenseGame.entity.enemy;

namespace TowerDefenseGame.entity.bullet
{
    public class Bullet : Sprite
    {
        
        protected int damage;
        protected float speed;
        //protected int max_age;
        protected int age;
        //protected bool alive;


        protected Vector2 target_center;

        public Bullet(Texture2D texture, Vector2 center, int damage, float speed,int max_age, Vector2 target_center)
            :base(texture,center)
        {
            this.damage = damage;
            this.speed = speed;
            this.target_center = target_center;
            //this.max_age = max_age;
            this.age = max_age;
            //this.alive = true;
        }

        public int Damage
        {
            get { return damage; }
        }

        public bool Alive
        {
            get { return (age > 0); }
        }
        
        public void Kill()
        {
            //alive = false;
            age = 0;
        }
        
        public void HitTarget()
        {
            
        }

        public void Move()
        {
            Vector2 direction = (target_center - center);
            direction.Normalize();
            Vector2 velocity = speed * direction;
            center += velocity;
            age--;
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
