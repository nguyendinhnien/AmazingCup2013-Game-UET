using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TowerDefenseGame.entity.bullet;

namespace TowerDefenseGame.entity.tower
{
    public class ArrowTower : Tower
    {
        public static String TEXTURE_LOCATION = @"images\tower\arrow_tower";
        public static Texture2D TEXTURE;

        private static float MAX_HEALTH =1;
        private static int VALUE =1;
        private static int RANGE = 500;
        private static int FIRE_RELOAD = 60;
        private static int DAMAGE = 1;

        public ArrowTower(Vector2 center)
            : base(TEXTURE, center, MAX_HEALTH, VALUE, RANGE, DAMAGE ,FIRE_RELOAD)
        {
        }

        public static String TextureLocation
        {
            get { return TEXTURE_LOCATION; }
        }

        public override void Reload()
        {
            Bullet bullet = new Arrow(center,DAMAGE,8.0f,target.Center);
            bullets.Add(bullet);
            fire_reload = max_fire_reload;
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
        }

    }
}
