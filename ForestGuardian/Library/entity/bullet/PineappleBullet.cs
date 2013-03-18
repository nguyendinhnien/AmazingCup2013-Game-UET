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
        public static float SPEED = 5.0f;
        public static int SPLASH_RANGE = 500;

        private List<Enemy> mEnemies;

        public PineappleBullet( Texture2D pTexture, Vector2 pCenter, int pDamage)
            : base(pTexture, pCenter, SPEED, pDamage) { }
        public PineappleBullet(Texture2D pTexture, Vector2 pCenter, Vector2 pTarget_center)
            : base(pTexture, pCenter, SPEED, pTarget_center) { } 
        public PineappleBullet(Texture2D pTexture, Vector2 pCenter, int pDamage, List<Enemy> pEnemies)
            : base(pTexture, pCenter, SPEED, pDamage) 
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
                if (isInRange(enemy.Center))
                {
                    enemy.lostHealth(mDamage);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }
    }
}
