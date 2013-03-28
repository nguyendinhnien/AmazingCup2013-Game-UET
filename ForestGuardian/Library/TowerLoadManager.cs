using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Library
{
    public class TowerLoadManager
    {
        public static bool isLoaded = false;
        public static void LoadContent(ContentManager Content)
        {
            if (!isLoaded)
            {
                OakTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level1");
                OakTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level2");
                OakTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level3");

                CactusTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level1");               
                CactusTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level2");
                CactusTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level3");

                PineappleTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level1");
                PineappleTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level2");
                PineappleTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level3");
                
                isLoaded = true;
            }
        }
    }
}
