using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Library
{
    public class Arrow: Bullet
    {
        private static String TEXTURE_LOCATION = @"images\bullet\arrow";
        public static Texture2D TEXTURE;
        public static float SPEED = 8.0f;
        public static int MAX_AGE = 1000;

        public static String TextureLocation
        {
            get { return TEXTURE_LOCATION; }
        }
        public Arrow(Vector2 center, int damage)
            : base(TEXTURE, center, damage, SPEED, MAX_AGE) { }
        public Arrow(Vector2 center, int damage, Vector2 target_center)
            : base(TEXTURE, center, damage, SPEED, MAX_AGE, target_center) { }
        
    }
}
