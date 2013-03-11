using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class TowerManager
    {
        private Dictionary<int,Tower> towers;

        public TowerManager()
        {
            towers = new Dictionary<int,Tower>();
        }

        public void AddTower(int key_pos,Tower tower)
        {
            towers.Add(key_pos,tower);
        }
        public void RemoveTower(int key_pos)
        {
            towers.Remove(key_pos);
        }
        public void UpgradeTower(int key_pos)
        {   
        }

        public void Update(GameTime gameTime,List<Enemy> enemies)
        {
            Tower tower;
            foreach (var tower_pair in towers)
            {
                tower = tower_pair.Value;
                
                tower.Update(gameTime);
                if (tower.Target == null || !tower.Target.Alive)
                {
                    tower.Target = tower.getClosestEnemy(enemies);
                }
                
                if (tower.Target != null)
                {
                    //Console.WriteLine("BANG");
                    tower.Attack(tower.Target);
                    //Console.WriteLine(tower.Target.Health.ToString());
                    if (tower.Target.atEnd)
                        tower.Target = null;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tower tower;
            foreach (var tower_pair in towers)
            {
                tower = tower_pair.Value;
                tower.Draw(spriteBatch);
            }
        }
    }
}
