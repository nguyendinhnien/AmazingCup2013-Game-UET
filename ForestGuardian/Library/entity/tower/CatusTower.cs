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

        public CactusTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD) {}
        
        public CactusTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) {}

        public override void createBullet()
        {
            bullet = new CactusBullet(BULLET_TEXTURE, this.Center);

            bullet.setTargetPos(this.Target.Center);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
