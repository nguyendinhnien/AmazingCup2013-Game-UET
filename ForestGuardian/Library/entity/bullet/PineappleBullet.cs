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
    public class PineappleBullet : Bullet
    {
        public static float SPEED = 7.0f;
        public static int SPLASH_RANGE = 150;
        public static ParticleEffect EFFECT = new ParticleEffect();

        private List<Enemy> mEnemies;

        public PineappleBullet(Texture2D pTexture, Vector2 pCenter, int pDamage)
            : base(pTexture, pCenter, SPEED, pDamage, EFFECT) { }
        public PineappleBullet(Texture2D pTexture, Vector2 pCenter, int pDamage, List<Enemy> pEnemies)
            : base(pTexture, pCenter, SPEED, pDamage, EFFECT)
        {
            mEnemies = pEnemies;
        }

        private bool isInRange(Vector2 position)
        {
            if (Vector2.Distance(mCenter, position) <= SPLASH_RANGE) return true;
            else return false;
        }

        public override void HitTarget(Enemy pEnemy)
        {
            foreach (Enemy enemy in mEnemies)
            {
                if (!mHit && isInRange(enemy.Center))
                {
                    enemy.lostHealth(mDamage);
                }
            }

            if (!mHit)
            {
                Vector2 tmp = pEnemy.Center;
                mEffect.Trigger(ref tmp);
                mHit = true;

                BulletSound();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void BulletSound()
        {
            AudioManager.soundBank.GetCue("bullet_3").Play();
        }
    }
}
