using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Library
{
    class PineappleBullet : Bullet
    {
        public static float SPEED = 8.0f;

        public PineappleBullet( Texture2D pTexture, Vector2 pCenter)
            : base(pTexture, pCenter, SPEED) { }
        public PineappleBullet(Texture2D pTexture, Vector2 pCenter, Vector2 pTarget_center)
            : base(pTexture, pCenter, SPEED, pTarget_center) { } 
    }
}
