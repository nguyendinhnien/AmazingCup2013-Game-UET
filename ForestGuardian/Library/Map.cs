//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;

//using Data;
//using Library;

//namespace Game
//{
//    public class GamePlayScene
//    {
//        private GameManager game;

//        private int width, height;
//        private int tile_size;
//        private BackgroundLayer background_layer;
//        private byte[] interactive_map;
//        private HudLayer hud_layer;
        
//        private WaveManager wave_manager;
//        private TowerManager tower_manager;

//        public void LoadMap(Map map)
//        {
//            width = map.Width;
//            height = map.Height;
//            tile_size = map.TileSize;
//            background_layer = new BackgroundLayer(Vector2.Zero, map.BackgroundTexture);
//            interactive_map = map.InteractiveMap;

//            Queue<Library.Wave> waves = new Queue<Library.Wave>();
//            Library.Wave wave; Path path;
            
//            Random random = new Random();
//            int path_number = map.paths.Count;

//            for (int i = 0; i < map.Waves.Count; i++)
//            {
//                //Lay ngau nhien mot duong di
//                path = map.paths[random.Next(path_number)];
//                wave = new Library.Wave(map.Waves[i].EnemyType,
//                                        map.Waves[i].EnemyNumber,
//                                        map.Waves[i].SpawnRate,
//                                        GetWaypoints(path));
//                //Dua wave moi tao vao queue
//                waves.Enqueue(wave);
//            }
//            //Khoi tao wave manager
//            wave_manager = new WaveManager(waves);
//        }

//        private Vector2 GetVector2FromCell(Cell c)
//        {
//            return new Vector2(c.tile_x * tile_size + tile_size/2 , c.tile_y * tile_size + tile_size/2);
//        }

//        private Queue<Vector2> GetWaypoints(Path path)
//        {
//            Queue<Vector2> waypoints = new Queue<Vector2>();
//            int number = path.Waypoints.Count;
//            for (int i = 0; i < number; i++)
//            {
//                waypoints.Enqueue(GetVector2FromCell(path.Waypoints[i]));
//            }
//            return waypoints;
//        }

//        public void Update(GameTime gameTime)
//        {
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            //Ve background 1 lan thoi
//            background_layer.Draw(spriteBatch);
//            //wave_manager.Draw(spriteBatch);
//        }
//    }
//}
