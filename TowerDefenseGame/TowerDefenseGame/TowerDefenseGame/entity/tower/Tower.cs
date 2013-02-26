using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseGame.entity.enemy;
using TowerDefenseGame.entity.bullet;

namespace TowerDefenseGame.entity.tower
{
    public class Tower : DynamicEntity 
    {
        //Cac thuoc tinh co ban cua moi tower
        protected int range;
        protected int damage;

        protected int max_fire_reload;
        protected int fire_reload;
        
        protected int level;

        protected Enemy target;
        protected bool attacking;

        protected List<Bullet> bullets;
    
        public Enemy Target
        {
            get { return target; }
            set { target = value; }
        }
        public int Range
        {
            get { return range; }
        }

        public Tower(Texture2D texture, Vector2 center, float maxHealth, int value, int range, int damage, int max_fire_reload )
            : base(texture, center, maxHealth, value)
        {
            this.range = range;
            this.damage = damage;

            this.max_fire_reload = max_fire_reload;
            this.fire_reload = 0;

            this.bullets = new List<Bullet>();

            //Ban dau chua co dich, set la false. Neu set la true se lam chuyen
            this.attacking = false;
            this.level = 1;
        }

        public virtual void Upgrage()
        {
            level++;
        }

        public bool isInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= range) return true;
            else return false;
        }

        public Enemy getClosestEnemy(List<Enemy> enemies){
            Enemy closest_enemy = null;
            float smallest_range = range;

            foreach(Enemy enemy in enemies){
                if(isInRange(enemy.Center)){
                    if (Vector2.Distance(center, enemy.Center) < smallest_range)
                    {
                        smallest_range = Vector2.Distance(center, enemy.Center);
                        closest_enemy = enemy;
                    }
                }
            }
            return closest_enemy;
        }

        public void Attack(Enemy enemy)
        {
            attacking = true;
            target = enemy;
        }

        public virtual void Reload() { }

        public override void Update(GameTime gameTime)
        {
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

                        
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet b = bullets[i];
                b.Update(gameTime);
                if (target != null) { target.checkHit(b); }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
