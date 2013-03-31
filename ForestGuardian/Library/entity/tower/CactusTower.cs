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

        public static float FIRE_RELOAD = 0.8f;
        public static int COST = 15;
        public static int UP_COST = 10;
        public static int RANGE = 150;
        public static int DAMAGE = 3;
        public static float REDUCE_SPEED = 0.5f;
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

        public static string TowerInfo()
        {
            string towerInfo = "Name: " + NAME + "\n" +
                               "Damage: " + DAMAGE + "/" + (DAMAGE+1) + "/" + (DAMAGE + 2) + "\n" +
                               "Range: " + RANGE + "\n" + "Cost: " + COST + "\n" + 
                               "Slow: " + (int)(REDUCE_SPEED*100)+ "%/" + (int)((REDUCE_SPEED + 0.15f)*100) + "%/" + (int)((REDUCE_SPEED + 0.2f)*100) + "%\n" +
                               "Slow Duration: " + SLOW_DURATION + "s/" + (SLOW_DURATION + 0.5f) + "s/" + (SLOW_DURATION + 1.0f) +"s";
                                
            return towerInfo;
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
                mDamage = DAMAGE + 1;
                mSpeedReduce = REDUCE_SPEED + 0.15f;
                mSlowDuration = SLOW_DURATION + 0.5f; 
            }
            else if (level == 3)
            {
                mTexture = TEXTURE_LV3;
                mDamage = DAMAGE + 2;
                mSpeedReduce = REDUCE_SPEED + 0.2f;
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
