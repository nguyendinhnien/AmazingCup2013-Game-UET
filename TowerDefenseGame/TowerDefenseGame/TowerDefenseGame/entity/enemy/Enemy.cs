using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseGame.entity.bullet;

namespace TowerDefenseGame.entity.enemy
{
    public class Enemy : DynamicEntity
    {
        protected float move_speed;
        protected float hit_radius;

        protected Queue<Vector2> waypoints;
        protected static float destinationLimit= 4.0f;
        protected bool at_end;

        public Enemy(Texture2D texture, Vector2 center, float maxHealth, int value, float move_speed)
            : base(texture, center, maxHealth, value)
        {
            this.move_speed = move_speed;
            this.at_end = false;
        }

        public void setWaypoints(Queue<Vector2> waypoints){
            this.waypoints = waypoints;
        }

        public void Move(Vector2 direction)
        {
            direction.Normalize();
            Vector2 velocity = move_speed * direction;
            if (DistanceToDestination > velocity.Length())
            {
                center += velocity;
            }
            else
            {
                center = waypoints.Peek();
            }
        }
        /*
        public bool isHit(Bullet bullet)
        {
            Rectangle rec = this.BoundingBox((int) (0.8 * texture.Width), (int) (0.8 * texture.Height));
            return rec.Contains((int) bullet.Center.X, (int) bullet.Center.Y);
        }
        */
        public void checkHit(Bullet b)
        {
            if (Vector2.Distance(center, b.Center) < hit_radius)
            {
                this.lostHealth(b.Damage);
            }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(center, waypoints.Peek()); }
        }

        public bool atDestination
        {
            get { return DistanceToDestination <= destinationLimit; }
        }

        public bool atEnd
        {
            get { return at_end;}
        }
        public override void Update(GameTime gameTime)
        {
            //Neu chua di het duong
            if (waypoints.Count > 0)
            {
                    
                //Neu da toi mot vi tri waypoint
                if (atDestination) { waypoints.Dequeue(); }
                else
                {
                    Vector2 direction = waypoints.Peek() - center;
                    Move(direction);              
                }
            }
            else
            {
                at_end = true;
                alive = false;
            }
            base.Update(gameTime);
        }

    }
}
