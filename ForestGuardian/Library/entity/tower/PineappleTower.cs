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
        public static int RANGE;
        public static int FIRE_RELOAD;
        public static int DAMAGE;

        public PineappleTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE, FIRE_RELOAD){}
        
        public PineappleTower(Vector2 pPosition, Anchor a)
            : base(TEXTURE, pPosition, a, COST, RANGE, DAMAGE, FIRE_RELOAD) { }

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
