using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefenseGame.tile
{
    class Tile
    {
        public static int TILE_WIDTH = 64;
        public static int TILE_HEIGHT = 64;

        //Chuyen tu toa do tile sang toa do thuc
        public static Vector2 TileToVector2(int tile_x, int tile_y)
        {
            Vector2 position = new Vector2(tile_x * TILE_WIDTH, tile_y * TILE_HEIGHT);
            return position;
        }


    }
}
