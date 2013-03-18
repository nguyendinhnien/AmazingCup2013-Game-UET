﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Library
{
    class CactusBullet : Bullet
    {
        public static float SPEED = 8.0f;

        public CactusBullet( Texture2D pTexture, Vector2 pCenter)
            : base(pTexture, pCenter, SPEED) { }
        public CactusBullet(Texture2D pTexture, Vector2 pCenter, Vector2 pTarget_center)
            : base(pTexture, pCenter, SPEED, pTarget_center) { } 
    }
}
