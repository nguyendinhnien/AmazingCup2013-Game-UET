using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Data
{
    public class Wave
    {
        [ContentSerializerIgnore]
        public static int NUMBER_BETWEEN_GROW = 1;
        
        [ContentSerializerIgnore]
        public static float BASE_GROW_RATE = 0.33f;
        
        public string EnemyType;
        public int EnemyNumber;
        public float SpawnRate;
        [ContentSerializerIgnore]
        public int path_order;
        [ContentSerializerIgnore]
        public float GrowRate;
    }
}
