using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public enum WaveState
    {
        Start,
        Active,
        InActive,
        Finish
    }
    public class Wave
    {
        //Thoi gian giua cac dot enemy
        private float spawn_rate;
        private float timer=0.0f;
        
        //Cac enemy da duoc goi ra
        private string enemy_type;
        private List<Enemy> active_enemies; 
        
        //Tong so luong enemy
        private int total_number;
        //So luong enemy toi duoc dich
        private int reached_end_number=0;
        private int death_point = 0;
        //So luong enemy duoc sinh ra
        private int spawned_number=0;

        private Queue<Vector2> waypoints;

        private WaveState state = WaveState.Start;


        public Wave(string enemy_type ,int total_number, float spawn_rate, Queue<Vector2> waypoints)
        {
            this.spawn_rate = spawn_rate;
            this.enemy_type = enemy_type;
            this.total_number = total_number;
            this.waypoints = waypoints;

            this.active_enemies = new List<Enemy>();
        }

        public WaveState State
        {
            get { return state; }
            set { state = value; }
        }

        public int ReachedEndNumber
        {
            get { return reached_end_number; }
            set { reached_end_number = value; }
        }

        public int SpawnedNumber
        {
            get { return spawned_number; }
        }

        public List<Enemy> ActiveEnemies
        {
            get { return active_enemies; }
        }

        public int DeathPoint
        {
            get { return death_point; }
        }
        public Enemy getNewEnemy()
        {
            Enemy enemy;
            switch (enemy_type)
            {           
                case EnemyType.AXE_MAN:
                    //Lay ra waitpoint dau tien lam vi tri xuat phat
                    enemy = new AxeMan(waypoints.Peek());
                    enemy.setWaypoints(new Queue<Vector2>(waypoints));
                    break;
                case EnemyType.SAW_MAN:
                    enemy = new SawMan(waypoints.Peek());
                    enemy.setWaypoints(new Queue<Vector2>(waypoints));
                    break;
                default:
                    enemy = null; break;
            }
            return enemy;
            
        }

        public void Update(GameTime gameTime){
            reached_end_number = 0;
            death_point = 0;
            //Xem da bat dau them enemy moi vao chua (phai con de dua vao)
            if (spawned_number >= total_number && active_enemies.Count == 0)
            {
                state = WaveState.InActive; return;
            }
            //Spawn  new enemy
            if (spawned_number < total_number)
            {
                if (timer >= spawn_rate)
                {
                    Enemy new_enemy = getNewEnemy();
                    if (new_enemy != null)
                    {
                        active_enemies.Add(new_enemy);
                        spawned_number++;
                    }
                    timer = 0;
                }
                else { timer += (float)gameTime.ElapsedGameTime.Milliseconds/1000; }
            }

            //Update tat ca active enemy
            Enemy enemy;
            for(int i = 0 ; i < active_enemies.Count; i++){
                enemy = active_enemies[i];
                if (enemy.Alive) { enemy.Update(gameTime); }
                else
                {
                    if (enemy.atEnd) { reached_end_number++; }
                    else { death_point += enemy.Value; }
                    lock (active_enemies)
                    {
                        active_enemies.RemoveAt(i);
                    }
                }
            }
            //Neu tat ca dau da di het
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in active_enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
