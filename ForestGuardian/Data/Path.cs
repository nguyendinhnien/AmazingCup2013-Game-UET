using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Data
{
    public class Path
    {
        public byte id;
        public List<Cell> Waypoints;
        public List<Cell> Turnpoints;

        public Path(byte id)
        {
            this.id = id;
            Waypoints = new List<Cell>();
            Turnpoints = new List<Cell>();
        }

        public Path(byte id, Path path)
        {
            this.id = id;
            Waypoints = new List<Cell>(path.Waypoints);
            Turnpoints = new List<Cell>(path.Turnpoints);
        }

        public bool IsPassedTurnpoint(int x,int y)
        {
            foreach (Cell turnpoint in Turnpoints)
            {
                if (turnpoint.tile_x == x && turnpoint.tile_y == y) return true;        
            }
            return false;
        }

        public void display()
        {
            Debug.WriteLine("Path id: " + id);
            foreach (Cell turnpoint in Turnpoints)
            {
                Debug.Write("(" + turnpoint.tile_x + "," + turnpoint.tile_y + ") ");
            }
            Debug.WriteLine("");

            foreach (Cell waypoint in Waypoints)
            {
                Debug.Write("(" + waypoint.tile_x + "," + waypoint.tile_y + ") ");
            }
            Debug.WriteLine("");
        }
    }
}
