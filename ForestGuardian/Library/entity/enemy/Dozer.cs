using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Dozer:Enemy
    {
        public static Texture2D TEXTURE;
        public static Animation MOVE_ANIMATION;
        public static float MOVE_SPEED = 1.0f;
        public static float MAX_HEALTH = 2000.0f;
        public static int VALUE = 10;
        public static string DEATH_SOUND = "death_3";

        public Dozer(Vector2 center)
            : base(center, MAX_HEALTH, VALUE, MOVE_SPEED) 
        {
            deathSound = DEATH_SOUND;
            movingSound = AudioManager.moveLoop3;
        }

        public Dozer(Animation animation, Vector2 position, Anchor a)
            : base(animation, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) 
        { 
            deathSound = DEATH_SOUND;
            movingSound = AudioManager.moveLoop3;
        }

        public override void setMoveAnimation()
        {
            animation = MOVE_ANIMATION;
        }
    }
}
