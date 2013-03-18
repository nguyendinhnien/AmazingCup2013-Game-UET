using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Library
{
    public class ArrowTower : Tower
    {
        public static Texture2D TEXTURE;
        public static Texture2D BULLET_TEXTURE;

        public static float MAX_HEALTH;
        public static int COST;
        public static int RANGE;
        public static int FIRE_RELOAD;
        public static int DAMAGE;

        public ArrowTower(Vector2 pCenter)
            : base(TEXTURE, pCenter, COST, RANGE, DAMAGE ,FIRE_RELOAD)
        {
            
        }

        public override void createBullet()
        {
            bullet = new Bullet(BULLET_TEXTURE, this.Center, this.damage, 5, this.fire_reload);

            bullet.setTargetPos(this.Target.Center);
            //bullet.Move();
        }

        //public override void Reload()
        //{  
        //    Bullet bullet = new Arrow(center,DAMAGE);
        //    Vector2 target_center = predictTargetPos(bullet);
        //    bullet.setTargetPos(target_center);
            
        //    bullets.Add(bullet);
        //    timer = fire_reload;
        //}

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
        }

    }
}
