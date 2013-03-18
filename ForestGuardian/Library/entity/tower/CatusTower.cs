using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class CatusTower : Tower
    {
        public static Texture2D TEXTURE;
        public static Texture2D BULLET_TEXTURE;

        public static int COST=2;
        public static int RANGE=2;
        public static int FIRE_RELOAD=2;
        public static int DAMAGE=2;

        public CatusTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD) {}
        
        public CatusTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) {}

        public override void createBullet()
        {
            bullet = new Bullet(BULLET_TEXTURE, this.Center, this.damage, 5, this.fire_reload);

            bullet.setTargetPos(this.Target.Center);
            bullet.Move();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
