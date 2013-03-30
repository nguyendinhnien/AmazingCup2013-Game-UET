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
    public class OakBullet: Bullet
    {
        public static float SPEED = 10.0f;
        public static ParticleEffect EFFECT = new ParticleEffect();

        public OakBullet(Texture2D pTexture, Vector2 pCenter, int pDamage)
            : base(pTexture, pCenter, SPEED, pDamage, EFFECT) { }

        public override void BulletSound()
        {
            AudioManager.soundBank.GetCue("bullet_1").Play();
        }
    }
}
