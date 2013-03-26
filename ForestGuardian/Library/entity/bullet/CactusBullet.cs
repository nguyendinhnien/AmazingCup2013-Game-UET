using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;

namespace Library
{
    public class CactusBullet : Bullet
    {
        public static float SPEED = 8.0f;
        public static ParticleEffect EFFECT = new ParticleEffect();

        public CactusBullet(Texture2D pTexture, Vector2 pCenter, int pDamage)
            : base(pTexture, pCenter, SPEED, pDamage, EFFECT) { }
    }
}
