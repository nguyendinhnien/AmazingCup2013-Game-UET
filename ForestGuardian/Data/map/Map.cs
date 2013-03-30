using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Data
{
    public class Direction
    {
        public static int[] dx = { 1, 0, 0, -1 };
        public static int[] dy = { 0, 1, -1, 0 };
    }

    public class Map
    {
        [ContentSerializerIgnore]
        public static byte BLANK = 0;
        [ContentSerializerIgnore]
        public static byte PATH = 1;
        [ContentSerializerIgnore]
        public static byte OTHER = 2;

        public string Name;
        public string Description;
        public int Width;
        public int Height;
        public int TileSize;
        public string BackgroundFile;
        private Texture2D mBackgroundTexture;

        [ContentSerializerIgnore]
        public Texture2D BackgroundTexture
        {
            get { return mBackgroundTexture; }
            set { mBackgroundTexture = value; }
        }

        public byte[] InteractiveMap;
        public Cell StartCell;
        public Cell EndCell;
        public List<Wave> Waves;
        public List<string> SongFiles;
        //[XmlElement]
        //public List<Path> Paths;
        [ContentSerializerIgnore]
        public List<Path> paths;
        [ContentSerializerIgnore]
        public byte path_number;

        public void PostReading()
        {
            byte[,]visit = new byte[Width, Height];
            path_number = 0;
            paths = new List<Path>();
            
            Path path;

            path_number++;
            path = new Path(path_number);
            path.Waypoints.Add(StartCell);
            visit[StartCell.tile_x, StartCell.tile_y] = path.id;
            
            DFS_Search(StartCell.tile_x, StartCell.tile_y, path, -1, visit);

            Random random = new Random();
            for (int i = 0; i < Waves.Count;i++ )
            {
                Waves[i].path_order = random.Next(Waves.Count);
                Waves[i].GrowRate = i / Data.Wave.NUMBER_BETWEEN_GROW * Data.Wave.BASE_GROW_RATE;
            }
        }

        private void DFS_Search(int x, int y, Path path, int direct, byte[,] visit)
        {
            //Neu la cell cuoi
            if (EndCell.tile_x == x && EndCell.tile_y == y)
            {
                path.Waypoints.Add(new Cell(EndCell));
                paths.Add(path);
            }
            int temp_x, temp_y;
            //int count = 0;
            //Du doan truoc
            bool is_turn_point = IsTurnPoint(x, y, direct);

            for (int i = 0; i < 4; i++)
            {
                //Chi xet cac huong khong nguoc voi huong da cho
                if (i + direct != 3)
                {
                    temp_x = x + Direction.dx[i]; temp_y = y + Direction.dy[i];
                    if (InBound(temp_x,temp_y) && InteractiveMap[temp_y * Width + temp_x] == PATH )
                    {
                        if (visit[temp_x, temp_y] <= path.id && path.IsPassedTurnpoint(temp_x,temp_y)){                            
                            return; 
                        }
                        
                        Path p;                      
                        //count++;
                        //if (count > 1) { path_number++; }
                        if (is_turn_point) {
                            path_number++;
                            p = new Path(path_number, path);
                            p.Turnpoints.Add(new Cell(x, y));
                        }
                        else { p = path; } 
                        
                        //Neu doi huong ma ko di thang
                        if (direct != -1 && direct != i)
                        {
                            p.Waypoints.Add(new Cell(x, y));
                        }
                        
                        //Gan lai visit
                        visit[temp_x,temp_y] = p.id;

                        //Tiep tuc search voi o do
                        DFS_Search(temp_x, temp_y, p, i, visit);
                    }
                }
            }
        }

        private bool InBound(int x, int y)
        {
            return (x >= 0 && x <= Width - 1 && y >= 0 && y <= Height - 1);
        }
        private bool IsTurnPoint(int x, int y, int direct)
        {
            int count = 0;
            int temp_x,temp_y;
            for (int i = 0; i < 4; i++)
            {
                if (i + direct != 3)
                {
                    temp_x = x + Direction.dx[i]; temp_y = y + Direction.dy[i];
                    if (InBound(temp_x, temp_y) && InteractiveMap[temp_y * Width + temp_x] == PATH)
                    {
                        count++;
                    }
                }
            }
            if (count > 1) return true;
            else return false;
        }

        public void display()
        {
            Debug.WriteLine(Name);
            Debug.WriteLine("Width: " + Width + "," + "Height: " + Height);
            Debug.WriteLine("Tile size: " + TileSize);
            Debug.WriteLine(BackgroundFile);
            Debug.WriteLine(InteractiveMap);
            Debug.WriteLine(paths.Count);
        }
    }
}

