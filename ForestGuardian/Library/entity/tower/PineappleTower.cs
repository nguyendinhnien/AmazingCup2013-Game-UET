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
        public static Texture2D TEXTURE;
        public static Texture2D BULLET_TEXTURE;

        public static int COST = 3;
        public static int RANGE = 200;
        public static int FIRE_RELOAD = 10;
        public static int DAMAGE = 20;

        public PineappleTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD){}
        
        public PineappleTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) { }

        public override void createBullet()
        {
            bullet = new PineappleBullet(BULLET_TEXTURE, this.Center);

            bullet.setTargetPos(this.Target.Center);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
