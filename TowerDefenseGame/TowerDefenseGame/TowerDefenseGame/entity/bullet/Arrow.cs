using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerDefenseGame.entity.bullet
{
    public class Arrow: Bullet
    {
        private static String TEXTURE_LOCATION = @"images\bullet\arrow";
        public static Texture2D TEXTURE;
        public static int MAX_AGE = 50;

        public static String TextureLocation
        {
            get { return TEXTURE_LOCATION; }
        }
        public Arrow(Vector2 center, int damage, float speed,Vector2 target_center)
            : base(TEXTURE, center, damage, speed,MAX_AGE,target_center)
        {
        }
    }
}
