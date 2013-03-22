using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class AxeMan : Enemy
    {
        public static Animation WALK_ANIMATION;
        public static float MOVE_SPEED = 10.0f;
        public static float MAX_HEALTH = 120.0f;
        public static int VALUE = 1;

        public AxeMan(Vector2 center)
            : base(center, MAX_HEALTH, VALUE, MOVE_SPEED){}

        public AxeMan(Animation animation, Vector2 position, Anchor a)
            : base(animation, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) { }

        public override void setWalkAnimation()
        {
            animation = WALK_ANIMATION;
        }
    }
}
