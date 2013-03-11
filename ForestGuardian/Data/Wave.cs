using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Data
{
    public class Wave
    {
        public string EnemyType;
        public int EnemyNumber;
        public float SpawnRate;
        [ContentSerializerIgnore]
        public int path_order;
    }
}
