using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TowerDefenseGame.entity.tower;
using TowerDefenseGame.entity.enemy;

namespace TowerDefenseGame.manager
{
    class TowerManager
    {
        private List<Tower> towers;

        public TowerManager()
        {
            towers = new List<Tower>();
        }

        public void AddTower(Tower tower)
        {
            towers.Add(tower);
        }

        public void Update(GameTime gameTime,List<Enemy> enemies)
        {
            foreach (Tower tower in towers)
            {
                tower.Update(gameTime);
                if (tower.Target == null || !tower.Target.Alive)
                    tower.Target = tower.getClosestEnemy(enemies);
                if (tower.Target != null)
                {
                    tower.Attack(tower.Target);
                    if (tower.Target.atEnd)
                        tower.Target = null;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
                tower.Draw(spriteBatch);
        }
    }
}
