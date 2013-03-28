using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public enum TowerType
    {
        OakTower,
        CactusTower,
        PineappleTower
    }
    public class Tower : Sprite
    {
        //Cac thuoc tinh co ban cua moi tower
        protected int mRange;
        protected int mDamage;

        protected float mFireReload;
        protected float reloadDuration;

        protected int cost;
        protected int upgradeCost;
        protected int level;

        protected Enemy target;
        protected Bullet bullet;

        public Enemy Target
        {
            get { return target; }
            set { target = value; }
        }
        public int Range
        {
            get { return mRange; }
        }

        public int Level
        {
            get { return level; }
        }

        public int Cost
        {
            get { return cost; }
        }
        public int UpgradeCost
        {
            get { return upgradeCost; }
        }
        public int SellCost
        {
            get { return (cost/2); }
        }

        public Tower(Texture2D texture, Vector2 pCenter, int cost, int upgradeCost, int range, int damage, float fire_reload)
            : this(texture, pCenter, Anchor.CENTER, cost, upgradeCost, range, damage, fire_reload) { }

        public Tower(Texture2D texture, Vector2 pPosition, Anchor a, int cost, int upgradeCost, int range, int damage, float fire_reload)
            : base(texture, pPosition, a)
        {
            this.cost = cost;
            this.upgradeCost = upgradeCost;
            this.mRange = range;
            this.mDamage = damage;
            this.mFireReload = fire_reload;

            this.level = 1;
            this.layer_depth = 0.5f;
        }

        public virtual void Upgrade()
        {
            if (level < 3)
            {
                level++;
            }
        }

        public bool isInRange(Vector2 position)
        {
            if (Vector2.Distance(mCenter, position) <= mRange) return true;
            else return false;
        }

        public virtual Enemy getClosestEnemy(List<Enemy> enemies)
        {
            Enemy closest_enemy = null;
            float smallest_range = mRange;

            foreach (Enemy enemy in enemies)
            {
                if (isInRange(enemy.Center))
                {
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
            if (reloadDuration <= 0)
            {
                reloadDuration = mFireReload;

                target = enemy;

                createBullet();
            }
        }

        public virtual void createBullet() { }

        public void Update(GameTime gameTime , bool isPause)
        {
            base.Update(gameTime);

            if (!isPause) //NTA added
            {
                if (reloadDuration >= 0)
                {
                    reloadDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (target != null && !isInRange(target.Center))
                {
                    target = null;
                }

                if (bullet != null)
                {
                    bullet.Update(gameTime);
                    if (!bullet.Alive && target != null)
                    {
                        bullet.HitTarget(target);
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
        }
    }
}
