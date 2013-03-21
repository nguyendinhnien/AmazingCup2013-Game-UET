using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class PineappleTower: Tower
    {
        public static Texture2D TEXTURE_LV1;
        public static Texture2D TEXTURE_LV2;
        public static Texture2D TEXTURE_LV3;
        public static Texture2D BULLET_TEXTURE;

        public static float FIRE_RELOAD = 2.5f;
        public static int COST = 3;
        public static int RANGE = 250;
        public static int DAMAGE = 20;
        //public static int SPLASH_RANGE = 50;

        private List<Enemy> mEnemies;

        public PineappleTower(Vector2 pCenter)
            : base(TEXTURE_LV1, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD){}
        
        public PineappleTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE_LV1, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public override void createBullet()
        {
            bullet = new PineappleBullet(BULLET_TEXTURE, this.Center, mDamage, mEnemies);
            
            bullet.setTargetPos(this.Target.Center);
        }

        public override void Upgrade()
        {
            base.Upgrade();

            if (level == 2)
            {
                mTexture = TEXTURE_LV2;
                mDamage = 2 * DAMAGE;
            }
            else if (level == 3)
            {
                mTexture = TEXTURE_LV3;
                mDamage = 3 * DAMAGE;
            }
        }

        public override Enemy getClosestEnemy(List<Enemy> enemies)
        {
            mEnemies = enemies;

            return base.getClosestEnemy(enemies);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
