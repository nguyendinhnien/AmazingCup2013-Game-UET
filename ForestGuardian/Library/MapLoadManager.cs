using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Data;
namespace Library
{
    public class MapLoadManager
    {
        //public static int MAX_DEFAULT_MAPS = 3;
        private static bool isLoad = false;
        private static Data.Map[] maps;
        private static Texture2D[] mapThumbnails;

        public static void LoadAllMap(ContentManager Content, int MaxMapNumber){
            if(!isLoad){
                maps = new Data.Map[MaxMapNumber];
                mapThumbnails = new Texture2D[MaxMapNumber];
                for (int i = 0; i < MaxMapNumber; i++)
                {
                    maps[i] = Content.Load<Map>(@"data\maps\map" + (i + 1));
                    maps[i].BackgroundTexture = Content.Load<Texture2D>(maps[i].BackgroundFile);
                    mapThumbnails[i] = Content.Load<Texture2D>(@"images\maps\map_level_" + (i+1) + @"_thumb");
                }
                isLoad = true;
            }
        }

        public static Map getMap(int i) { return maps[i]; }
        public static Texture2D getMapThumbnail(int i) { return mapThumbnails[i]; }
    }
}
