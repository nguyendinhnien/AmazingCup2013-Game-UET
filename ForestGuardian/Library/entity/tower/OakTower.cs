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

        public static float FIRE_RELOAD = 1.0f;
        public static int COST = 1;
        public static int RANGE = 150;
        public static int DAMAGE = 5;

        public OakTower(Vector2 pCenter)
            : base(TEXTURE_LV1, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public OakTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE_LV1, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) { }


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
                mDamage = 2 * DAMAGE;
            }
            else if (level == 3)
            {
                mTexture = TEXTURE_LV3;
                mDamage = 3 * DAMAGE;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
