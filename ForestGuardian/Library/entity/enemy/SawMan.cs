using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class SawMan: Enemy
    {
        public static Texture2D TEXTURE;
        public static Animation WALK_ANIMATION;
        public static float MOVE_SPEED = 1.5f;
        public static float MAX_HEALTH = 180.0f;
        public static int VALUE = 2;

        //public static String TextureLocation{
        //    get { return TEXTURE_LOCATION; }
        //}
        
        public SawMan(Vector2 center)
            : base(center, MAX_HEALTH, VALUE, MOVE_SPEED){}

        public SawMan(Animation animation, Vector2 position, Anchor a)
            : base(animation, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) { }

        public override void setWalkAnimation()
        {
            animation = WALK_ANIMATION;
        }
    }
}
