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
        public static float MOVE_SPEED;
        public static float MAX_HEALTH;
        public static int VALUE;

        //public static String TextureLocation{
        //    get { return TEXTURE_LOCATION; }
        //}
        
        public SawMan(Vector2 center)
            : base(TEXTURE,center,MAX_HEALTH,VALUE,MOVE_SPEED)
        {
                    
        }
    }
}
