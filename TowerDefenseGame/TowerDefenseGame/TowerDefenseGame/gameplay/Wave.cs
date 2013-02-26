using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseGame.entity.enemy;

namespace TowerDefenseGame.gameplay
{
    public class Wave
    {
        //Thoi gian giua cac dot enemy
        private int interval;
        private int timer;
        
        //Danh sach enemies trong wave
        private Queue<int> enemy_list;
        //Cac enemy da duoc goi ra
        private List<Enemy> active_enemies; 
        
        //Tong so luong enemy
        private int total_number;
        //So luong enemy toi duoc dich
        private int reached_end_number;
        //So luong enemy duoc sinh ra
        private int spawned_number;

        private Queue<Vector2> waypoints;

        private bool finish;


        public Wave(int interval, int total_number, Queue<int> enemy_list, Queue<Vector2> waypoints)
        {
            this.interval = interval;
            this.total_number = total_number;
            this.enemy_list = enemy_list;
            this.waypoints = waypoints;

            this.timer = 0;
            this.reached_end_number = 0;
            this.spawned_number = 0;
            this.active_enemies = new List<Enemy>();
            this.finish = false;
        }

        public bool Finish
        {
            get { return finish; }
        }

        public int ReachedEndNumber
        {
            get { return reached_end_number; }
        }

        public int SpawnedNumber
        {
            get { return spawned_number; }
        }

        public List<Enemy> ActiveEnemies
        {
            get { return active_enemies; }
        }
        public Enemy getNewEnemy()
        {
            
            int enemy_type = enemy_list.Dequeue();
            Enemy enemy = null;
            switch (enemy_type)
            {
                case EnemyType.AXE_MAN:
                    //Lay ra waitpoint dau tien lam vi tri xuat phat
                    enemy = new AxeMan(waypoints.Peek());
                    enemy.setWaypoints(new Queue<Vector2>(waypoints));
                    break;
                default:
                    enemy = null; break;
            }
            return enemy;
            
        }

        public void Update(GameTime gameTime){
            //Xem da bat dau them enemy moi vao chua (phai con de dua vao)
            if (enemy_list.Count > 0)
            {
                if (timer >= interval)
                {
                    Enemy new_enemy = getNewEnemy();
                    if (new_enemy != null)
                    {
                        active_enemies.Add(new_enemy);
                        spawned_number++;
                    }
                    timer = 0;
                }
                else { timer++; }
            }

            //Update tat ca active enemy
            Enemy enemy;
            for(int i = 0 ; i < active_enemies.Count; i++){
                enemy = active_enemies[i];
                if (enemy.Alive) { enemy.Update(gameTime); }
                else
                {
                    if (enemy.atEnd) { reached_end_number++; }
                    lock (active_enemies)
                    {
                        active_enemies.RemoveAt(i);
                    }
                }
            }
            //Neu tat ca dau da di het
            if (enemy_list.Count == 0 && active_enemies.Count == 0)
            {
                finish = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in active_enemies)
                enemy.Draw(spriteBatch);
        }
    }
}
