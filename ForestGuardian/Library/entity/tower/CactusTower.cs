using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class CactusTower : Tower
    {
        public static Texture2D TEXTURE_LV1;
        public static Texture2D TEXTURE_LV2;
        public static Texture2D TEXTURE_LV3;
        public static string NAME = "Cactus-The Slower";

        public static Texture2D BULLET_TEXTURE;

        public static float FIRE_RELOAD = 1.0f;
        public static int COST = 10;
        public static int UP_COST = 5;
        public static int RANGE = 150;
        public static int DAMAGE = 0;
        public static float REDUCE_SPEED = 0.4f;
        public static float SLOW_DURATION = 3.0f;

        private float mSpeedReduce;
        private float mSlowDuration;

        public CactusTower(Vector2 pCenter)
            : base(TEXTURE_LV1, pCenter, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD)
        {
            mSpeedReduce = REDUCE_SPEED;
            mSlowDuration = SLOW_DURATION;
        }

        public CactusTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE_LV1, pPosition, a, COST, UP_COST, RANGE, DAMAGE, FIRE_RELOAD)
        {
            mSpeedReduce = REDUCE_SPEED;
            mSlowDuration = SLOW_DURATION;
        }

        public override void createBullet()
        {
            bullet = new CactusBullet(BULLET_TEXTURE, this.Center, mDamage);
            bullet.setTarget(target);
        }

        public override void Upgrade()
        {
            base.Upgrade();

            if (level == 2)
            {
                mTexture = TEXTURE_LV2;
                mDamage = (int) (DAMAGE * 1.5f);
                mSpeedReduce = REDUCE_SPEED + 0.15f;
                mSlowDuration = SLOW_DURATION + 0.5f; 
            }
            else if (level == 3)
            {
                mTexture = TEXTURE_LV3;
                mDamage = (int)(DAMAGE * 1.8f);
                mSpeedReduce = REDUCE_SPEED + 0.3f;
                mSlowDuration = SLOW_DURATION + 1.0f; 
            }
        }

        public override void Update(GameTime gameTime, bool isPause)
        {
            base.Update(gameTime,isPause);
            if (!isPause && target != null && !bullet.Alive && target.SlowDuration < mSlowDuration)
            {
                target.SpeedReduce = mSpeedReduce;
                target.SlowDuration = mSlowDuration;
            }
        }

    }
}
