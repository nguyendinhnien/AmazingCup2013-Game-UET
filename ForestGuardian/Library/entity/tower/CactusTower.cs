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
        public static Texture2D TEXTURE;
        public static Texture2D BULLET_TEXTURE;

        public static int COST = 2;
        public static int RANGE = 150;
        public static int FIRE_RELOAD = 2;
        public static int DAMAGE = 0;
        public static float REDUCE_SPEED = 0.7f;
        public static float SLOW_DURATION = 3;

        private float mSpeedReduce;
        private float mSlowDuration;

        public CactusTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD) 
        {
            mSpeedReduce = REDUCE_SPEED;
            mSlowDuration = SLOW_DURATION;
        }
        
        public CactusTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) 
        {
            mSpeedReduce = REDUCE_SPEED;
            mSlowDuration = SLOW_DURATION;
        }

        public override void createBullet()
        {
            bullet = new CactusBullet(BULLET_TEXTURE, this.Center, damage);

            bullet.setTargetPos(this.Target.Center);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (target != null && !bullet.Alive && target.SlowDuration < mSlowDuration)
            {
                target.SpeedReduce = mSpeedReduce;
                target.SlowDuration = mSlowDuration;
            }
        }

    }
}
