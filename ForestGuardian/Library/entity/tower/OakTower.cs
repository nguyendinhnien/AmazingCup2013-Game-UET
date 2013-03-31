using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class OakTower : Tower
    {
        public static Texture2D TEXTURE_LV1;
        public static Texture2D TEXTURE_LV2;
        public static Texture2D TEXTURE_LV3;
        public static string NAME = "Oak-The Protector";

        public static Texture2D BULLET_TEXTURE;

        public static float FIRE_RELOAD = 0.5f;
        public static int COST = 8;
        public static int UP_COST = 6;
        public static int RANGE = 130;
        public static int DAMAGE = 10;

        public OakTower(Vector2 pCenter)
            : base(TEXTURE_LV1, pCenter, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public OakTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE_LV1, pPosition, a, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD) { }


        public static string TowerInfo()
        {
            string towerInfo = "Name: " + NAME + "\n" +
                               "Damage: " + DAMAGE + "/" + (int)(DAMAGE * 1.3f) + "/" + (int)(DAMAGE * 1.5f) + "\n" +
                               "Range: " + RANGE + "\n" + "Cost: " + COST;
            return towerInfo;
        }
        public override void createBullet()
        {
            bullet = new OakBullet(BULLET_TEXTURE, this.Center, mDamage);

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
    }
}
