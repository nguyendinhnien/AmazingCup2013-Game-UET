using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Tower : Sprite
    {
        //Cac thuoc tinh co ban cua moi tower
        protected int range;
        protected int damage;

        protected int fire_reload;
        protected float timer = 0;

        protected int cost;
        protected int level;

        protected Enemy target;
        protected bool attacking;

        protected Bullet bullet;
    
        public Enemy Target
        {
            get { return target; }
            set { target = value; }
        }
        public int Range
        {
            get { return range; }
        }

        public int Cost
        {
            get { return cost; }
        }

        public Tower(Texture2D texture, Vector2 pCenter, int cost, int range, int damage, int fire_reload )
            : base(texture, pCenter)
        {
            Vector2 anchor = new Vector2();
            anchor.X = pCenter.X;
            anchor.Y = pCenter.Y - 32;
            mCenter = anchor;

            this.cost = cost;
            this.range = range;
            this.damage = damage;
            
            this.fire_reload = fire_reload;

            //Ban dau chua co dich, set la false. Neu set la true se lam chuyen
            this.attacking = false;
            this.level = 1;
            this.layer_depth = 0.6f;
        }

        public virtual void Upgrage()
        {
            level++;
        }

        public bool isInRange(Vector2 position)
        {
            if (Vector2.Distance(mCenter, position) <= range) return true;
            else return false;
        }

        public Enemy getClosestEnemy(List<Enemy> enemies){
            Enemy closest_enemy = null;
            float smallest_range = range;

            foreach(Enemy enemy in enemies){
                if(isInRange(enemy.Center)){
                    if (Vector2.Distance(mCenter, enemy.Center) < smallest_range)
                    {
                        smallest_range = Vector2.Distance(mCenter, enemy.Center);
                        closest_enemy = enemy;
                    }
                }
            }
            return closest_enemy;
        }

        public void Attack(Enemy enemy)
        {
            if (!attacking)
            {
                attacking = true;
                target = enemy;

                createBullet();

                //target.lostHealth(damage);
            }
        }

        public virtual void Reload() { }

        public virtual void createBullet() { }

        //public Vector2 predictTargetPos(Bullet bullet)
        //{
        //    Vector2 to_target = target.Center - bullet.Center;
        //    Vector2 target_velocity = target.Speed * target.getDirection();

        //    float a = target.Speed * target.Speed - bullet.Speed * bullet.Speed;
        //    float b = Vector2.Dot(target_velocity, to_target);
        //    float c = Vector2.Dot(to_target, to_target);

        //    float p = -b / (2 * a);
        //    float q = (float)Math.Sqrt((b * b) - 4 * a * c) / (2 * a);

        //    float t1 = p - q;
        //    float t2 = p + q;
        //    float t;

        //    if (t1 > t2 && t2 > 0) { t = t2; }
        //    else { t = t1; }

        //    Vector2 aimSpot = target.Center + target_velocity * (int)t;
        //    return aimSpot;
        //}
        
        public override void Update(GameTime gameTime)
        {

            /*
            //Check xem co dang attack hay ko
            if (target == null) { attacking = false; }
            else if (!isInRange(target.Center))
            {
                target = null;
                attacking = false;
            }

            //Neu chua den luc reload
            if (fire_reload > 0) { fire_reload--; } //Tiep tuc doi
            else if(attacking) { Reload(); }    //Neu dang tan cong moi reload

            Console.WriteLine(bullets.Count);           
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet b = bullets[i];
                b.Update(gameTime);
                if (target != null) { target.checkHit(b); }
            }
            */
            base.Update(gameTime);

            if (target != null)
            {
                Vector2 disVec = this.mCenter - this.target.Center;
                float distance = disVec.Length();
                if (distance > range)
                {
                    target = null;
                }
            }

            if (bullet != null)
            {
                bullet.Update(gameTime);
                if (!bullet.Alive)
                {
                    attacking = false;

                    if (target != null)
                    {
                        target.lostHealth(damage);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (bullet != null)
            {
                bullet.Draw(spriteBatch);
            }
            
            //foreach (Bullet bullet in bullets)
            //{
            //    bullet.Draw(spriteBatch);
            //}
        }
    }
}
