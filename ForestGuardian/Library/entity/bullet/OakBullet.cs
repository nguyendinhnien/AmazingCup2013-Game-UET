using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Library
{
    public class OakBullet: Bullet
    {
        public static float SPEED = 8.0f;

        public OakBullet(Texture2D pTexture, Vector2 pCenter, int pDamage)
            : base(pTexture, pCenter, SPEED, pDamage) { }
        public OakBullet( Texture2D pTexture, Vector2 pCenter, Vector2 pTarget_center)
            : base(pTexture, pCenter, SPEED, pTarget_center) { } 
    }
}
