using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseGame.entity.enemy
{
    class AxeMan : Enemy
    {
        private static String TEXTURE_LOCATION = @"images\enemy\axeman";
        public static Texture2D TEXTURE;
        
        private static float MOVE_SPEED = 10.0f;
        private static float MAX_HEALTH = 1;
        private static int VALUE = 1;

        public static String TextureLocation{
            get { return TEXTURE_LOCATION; }
        }
        
        public AxeMan(Vector2 center)
            : base(TEXTURE,center,MAX_HEALTH,VALUE,MOVE_SPEED)
        {
                    
        }
    }
}
