using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class PineappleTower : Tower
    {
        public static Texture2D TEXTURE_LV1;
        public static Texture2D TEXTURE_LV2;
        public static Texture2D TEXTURE_LV3;
        public static string NAME = "Pineapple-The Bomber";

        public static Texture2D BULLET_TEXTURE;

        public static float FIRE_RELOAD = 1.5f;
        public static int COST = 25;
        public static int UP_COST = 20;

        public static int RANGE = 200;
        public static int DAMAGE = 25;

        private List<Enemy> mEnemies;

        public PineappleTower(Vector2 pCenter)
            : base(TEXTURE_LV1, pCenter, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public PineappleTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE_LV1, pPosition, a, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public static string TowerInfo()
        {
            string towerInfo = "Name: " + NAME + "\n" +
                               "Damage: " + DAMAGE + "/" + (int)(DAMAGE * 1.3f) + "/" + (int)(DAMAGE * 1.5f) + "\n" +
                               "Range: " + RANGE + "\n" + "Cost: " + COST + "\n" +
                               "Splash Range: " + PineappleBullet.SPLASH_RANGE;
            return towerInfo;
        }

        public override void createBullet()
        {
            bullet = new PineappleBullet(BULLET_TEXTURE, this.Center, mDamage, mEnemies);

            bullet.setTarget(target);

        }

        public override void Upgrade()
        {
            base.Upgrade();

            if (level == 2)
            {
                mTexture = TEXTURE_LV2;
                mDamage = (int)(DAMAGE * 1.3f);
            }
            else if (level == 3)
            {
                mTexture = TEXTURE_LV3;
                mDamage = (int)(DAMAGE * 1.5f);
            }
        }

        public override Enemy getClosestEnemy(List<Enemy> enemies)
        {
            mEnemies = enemies;

            return base.getClosestEnemy(enemies);
        }

    }
}
