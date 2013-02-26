using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseGame.core;

namespace TowerDefenseGame.entity
{
    public class DynamicEntity : Sprite
    {
        protected float maxHealth;
        protected float health;

        protected bool alive;
        //Gia tri cua Sprite (voi Enemy la tien thu duoc con voi Tower la tien mua)
        protected int value;

        public DynamicEntity(Texture2D texture, Vector2 center,float maxHealth, int value)
            :base(texture,center)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;

            this.value = value;
            this.alive = true;
        }
        public void lostHealth(float amount)
        {
            if (amount > 0)
            {
                health = health - amount;
            }
        }
        
        public bool Alive
        {
            get { return alive; }
        }
        public int Value
        {
            get { return value; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
