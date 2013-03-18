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
        public static Texture2D TEXTURE;
        public static float MOVE_SPEED = 2.0f;
        public static float MAX_HEALTH = 3.0f;
        public static int VALUE = 1;

        public AxeMan(Vector2 center)
            : base(TEXTURE, center, MAX_HEALTH, VALUE, MOVE_SPEED){}

        public AxeMan(Vector2 position, Anchor a)
            : base(TEXTURE, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) { }

    }
}
