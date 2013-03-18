using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public struct Cell
    {
        public int tile_x;
        public int tile_y;

        public bool Equal(Cell c)
        {
            return (tile_x == c.tile_x && tile_y == c.tile_y);
        }
        public Cell(int x, int y)
        {
            tile_x = x;
            tile_y = y;
        }
        public Cell(Cell c)
        {
            tile_x = c.tile_x;
            tile_y = c.tile_y;
        }
    }
}
