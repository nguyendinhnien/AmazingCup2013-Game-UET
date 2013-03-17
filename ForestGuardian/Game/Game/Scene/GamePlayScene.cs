using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Data;
using Library;

namespace Forest
{
    public enum TowerPut
    {
        NONE,
        ARROW_TOWER,
        SLOW_TOWER,
        CANON_TOWER
    }
    public class CellType{
        public const byte BLANK = 0;
        public const byte TOWER = 1;
        public const byte OTHER = 2;
    }
    public class GamePlayScene:GameScreen
    {
        private BackgroundLayer background_layer;

        private int width, height;
        private int tile_size;
        
        private byte[] interactive_map;
        
        //Phan game play
        private int lives;
        private int money;
        private int points = 0;

        private HudLayer hud_layer;
        private WaveManager wave_manager;
        private TowerManager tower_manager;

        public TowerPut tower_put = TowerPut.NONE;
        public bool tower_selecting = false;
        public int tower_selected_keypos = -1;
        
        //Phan input handler
        private ValueButton UpgradeButton;
        private Texture2D UpgradeButtonTexture;

        private ValueButton SellButton;
        private Texture2D SellButtonTexture;
        
        private MouseState previousState;
        
        public int Lives { get { return lives; } }
        public int Money { get { return money; } }
        public int Points { get { return points; } }

        public GamePlayScene():base(){}

        public override void LoadContent()
        {
            LoadGameData();
            hud_layer = new HudLayer(this);
            UpgradeButtonTexture = ScreenManager.Game.Content.Load<Texture2D>(@"images\gameplay\buttons\upgrade_but");
            SellButtonTexture = ScreenManager.Game.Content.Load<Texture2D>(@"images\gameplay\buttons\sell_but");
            UpgradeButton = new ValueButton(UpgradeButtonTexture, null, null, new Vector2(-1000.0f, -1000.0f));
            SellButton = new ValueButton(SellButtonTexture, null, null, new Vector2(-1000.0f,-1000.0f));
            SellButton.Clicked += SellButton_Clicked;
            UpgradeButton.Clicked += UpgradeButton_Clicked;
        }

        public void LoadGameData()
        {
            Loader.LoadEntitiesFromFile("data/entities/entities.xml", ScreenManager.Game.Content);
            loadMap(@"data\maps\map1");
            Camera2D.Reset(ScreenManager.Game.GraphicsDevice.Viewport);
            
        }

        public void loadMap(string map_file)
        {
            Map map = ScreenManager.Game.Content.Load<Map>(map_file);
            map.PostReading();

            width = map.Width;
            height = map.Height;
            tile_size = map.TileSize;

            Texture2D background_texture = ScreenManager.Game.Content.Load<Texture2D>(map.BackgroundFile);
            background_layer = new BackgroundLayer(Vector2.Zero, background_texture);
            
            interactive_map = new byte[width*height];
            for (int i = 0; i < interactive_map.Length; i++)
            {
                if (map.InteractiveMap[i] == Map.BLANK) { interactive_map[i] = CellType.BLANK; }
                else interactive_map[i] = CellType.OTHER; 
            }

            Queue<Library.Wave> waves = new Queue<Library.Wave>();
            Library.Wave wave; Path path;
            
            Random random = new Random();
            int path_count = map.paths.Count;

            for (int i = 0; i < map.Waves.Count; i++)
            {
                //Lay ngau nhien mot duong di
                path = map.paths[random.Next(path_count)];
                wave = new Library.Wave(map.Waves[i].EnemyType,
                                        map.Waves[i].EnemyNumber,
                                        map.Waves[i].SpawnRate,
                                        GetWaypoints(path));
                //Dua wave moi tao vao queue
                waves.Enqueue(wave);
            }
            
            //Khoi tao manager
            wave_manager = new WaveManager(waves);
            tower_manager = new TowerManager();
        }
        
        #region Utility
        public Vector2 GetVector2FromCell(int x,int y)
        {
            return new Vector2(x * tile_size + tile_size/2 , y * tile_size + tile_size/2);
        }

        public bool IsBlank(int x,int y)
        {
            if (InBound(x, y) && interactive_map[y * width + x] == CellType.BLANK)
                return true;
            else return false;
        }

        public bool IsTower(int x, int y)
        {
            if (InBound(x, y) && interactive_map[y * width + x] == CellType.TOWER)
                return true;
            else return false;
        }
        private bool InBound(int x, int y)
        {
            return (x >= 0 && x <= width - 1 && y >= 0 && y <= height - 1);
        }

        private Queue<Vector2> GetWaypoints(Path path)
        {
            Queue<Vector2> waypoints = new Queue<Vector2>();
            int number = path.Waypoints.Count;
            for (int i = 0; i < number; i++)
            {
                waypoints.Enqueue(GetVector2FromCell(path.Waypoints[i].tile_x,path.Waypoints[i].tile_y));
            }
            return waypoints;
        }
        #endregion

        //2 event handler cho 2 button
        private void SellButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Tower is sell");
            tower_manager.RemoveTower(tower_selected_keypos);
            interactive_map[tower_selected_keypos] = CellType.BLANK;
            SellButton.Position = new Vector2(-1000.0f, -1000.0f); SellButton.Enable = false;
            tower_selected_keypos = -1; tower_selecting = false;
        }
        private void UpgradeButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Tower is upgrade");
            tower_manager.UpgradeTower(tower_selected_keypos);
            tower_selected_keypos = -1; tower_selecting = false;
        }

        public override void  HandleInput(GameTime gameTime)
        {
            //Cap nhat Camera          
            Camera2D.Update(gameTime);
            SellButton.Update(gameTime);
            UpgradeButton.Update(gameTime);

            MouseState mouseState = Mouse.GetState();

            //Neu la trang thai click chuot phai
            if (mouseState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed)
            {
                Console.WriteLine("Dismissed");
                tower_put = TowerPut.NONE;
                tower_selecting = false;
            }
            //Neu la trang thai click chuot trai
            else if (mouseState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
            {
                int tile_x, tile_y;               
                Matrix inverseTransform = Matrix.Invert(Camera2D.Transform);
                Console.WriteLine(Camera2D.Transform);
                Vector2 mouseRealPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y),inverseTransform);
                Console.WriteLine(mouseRealPosition);
                tile_x = (int)(mouseRealPosition.X / tile_size);
                tile_y = (int)(mouseRealPosition.Y / tile_size);
                
                if (tower_put!=TowerPut.NONE && !tower_selecting && IsBlank(tile_x,tile_y))
                {
                    int key_pos = tile_y * width + tile_x;
                    switch (tower_put)
                    {
                        case TowerPut.ARROW_TOWER:
                            Tower tower = new ArrowTower(GetVector2FromCell(tile_x, tile_y));                        
                            tower_manager.AddTower(key_pos,tower);
                            interactive_map[key_pos] = CellType.TOWER;
                            Console.WriteLine("Arrow put");
                            break;
                    }
                    tower_put = TowerPut.NONE;
                }
                //Neu chon tower de nang cap
                else if (tower_put == TowerPut.NONE && !tower_selecting && IsTower(tile_x, tile_y))
                {
                    Vector2 position = GetVector2FromCell(tile_x, tile_y);
                    UpgradeButton.Enable = true;
                    UpgradeButton.Center = new Vector2(position.X + tile_size, position.Y);
                    
                    SellButton.Enable = true;
                    SellButton.Center = new Vector2(position.X - tile_size, position.Y);

                    tower_selected_keypos = tile_y * width + tile_x;
                    tower_selecting = true;
                    Console.WriteLine("Dang chon tower nay");
                }
                //else if(!(tower_selecting && (UpgradeButton.InBound(mouseRealPosition) || SellButton.InBound(mouseRealPosition)))){
                else{ 
                    Console.WriteLine("Not in bound");
                    tower_put = TowerPut.NONE;
                    tower_selecting = false;
                }
            }

            hud_layer.Update(gameTime);
            wave_manager.Update(gameTime);
            if (!wave_manager.Finish)
            {
                tower_manager.Update(gameTime, wave_manager.CurrentWave.ActiveEnemies);
            }
            
            previousState = mouseState;
            //if (!wave_manager.Finish)
            //{
            //    if (wave_manager.CurrentWave.ReachedEndNumber > 0)
            //    {
            //        lives -= wave_manager.CurrentWave.ReachedEndNumber;
            //    }
            //    if (lives <= 0) { Console.WriteLine("You loose"); }
            //}
            //else
            //{
            //    Console.WriteLine("You win");
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Ve background 1 lan thoi
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,null,null,null,null,Camera2D.Transform);
            if (tower_selecting)
            {
                SellButton.Draw(spriteBatch);
                UpgradeButton.Draw(spriteBatch);
            }
            tower_manager.Draw(spriteBatch);
            wave_manager.Draw(spriteBatch);
            background_layer.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            hud_layer.Draw(spriteBatch);
            spriteBatch.End();
            //wave_manager.Draw(spriteBatch);
        }
    }
}

