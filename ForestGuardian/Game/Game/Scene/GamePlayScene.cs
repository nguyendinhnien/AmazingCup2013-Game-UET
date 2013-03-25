﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using Data;
using Library;

namespace CustomGame
{
    public class CellType{
        public const byte BLANK = 0;
        public const byte TOWER = 1;
        public const byte OTHER = 2;
    }
    
    public class GamePlayScene : GameScene
    {
        private float LAYER_DEPTH_CHANGE = 0.005f; 
        private BackgroundLayer background_layer;

        //Phan map va kich thuoc map
        private int width, height;
        private int tile_size;      
        private byte[] tower_map;
        
        private List<Song> songs;
        private int currentSongIndex;

        //Phan game play
        private int lives=20;
        private int money=100;
        private int points = 0;

        private bool is_tower_add = false;
        private TowerType tower_type;
        private Label CursorLabel;   

        private bool is_tower_select = false;
        private int tower_keypos = -1;
        private float tower_layer_depth;
        private ToggleLabel RangeLabel;

        private SpriteFont wave_font;
        private WaveManager wave_manager;
        private TowerManager tower_manager;

        private MouseState previousState;
        #region Buttons,Labels

        private ToggleValueLabel OakTowerLabel;
        private ToggleValueLabel CatusTowerLabel;
        private ToggleValueLabel PineappleTowerLabel;
        private ToggleValueLabel UpgradeLabel;
        private ValueLabel SellLabel;
        #endregion
        private NextWavesTable WaveTable;
        private HUDLayer HudLayer;

        public GamePlayScene() : base(){}

        public int Lives { get { return lives; } }
        public int Money { get { return money; } }
        public int Points { get { return points; } }

        public override void LoadContent()
        {
            LoadGameContent();
        }

        public void LoadGameContent()
        {
            ContentManager Content = SceneManager.Game.Content;
            Texture2D texture;
            //Load Enemy texture
            Enemy.HEALTH_BAR_TEXTURE = Content.Load<Texture2D>(@"images\gameplay\health_bar");

            AxeMan.TEXTURE = Content.Load<Texture2D>(@"images\gameplay\enemies\axeman");
            texture = Content.Load<Texture2D>(@"images\gameplay\enemies\axeman_walk");
            AxeMan.WALK_ANIMATION = new Animation(texture, 6, 1, 0.2f, true);

            texture = Content.Load<Texture2D>(@"images\gameplay\enemies\sawman_walk");
            SawMan.WALK_ANIMATION = new Animation(texture, 6, 1, 0.2f, true);


            //Load Tower texture
            OakTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level1");
            CactusTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level1");
            PineappleTower.TEXTURE_LV1 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level1");
            OakTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level2");
            CactusTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level2");
            PineappleTower.TEXTURE_LV2 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level2");
            OakTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level3");
            CactusTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level3");
            PineappleTower.TEXTURE_LV3 = Content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level3");
            
            //Load bullets texture
            OakTower.BULLET_TEXTURE = Content.Load<Texture2D>(@"images\gameplay\bullets\oakbullet");
            CactusTower.BULLET_TEXTURE = Content.Load<Texture2D>(@"images\gameplay\bullets\cactusbullet");
            PineappleTower.BULLET_TEXTURE = Content.Load<Texture2D>(@"images\gameplay\bullets\pineapplebullet");

            //Load cac label
            Texture2D textureEnable, textureDisable;

            ValueLabel.FONT = Content.Load<SpriteFont>(@"fonts\gameplay\value_font");
            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\oak_tower_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\oak_tower_disable_but");
            OakTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(600, 640), new Vector2(32,72),OakTower.COST);

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\cactus_tower_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\cactus_tower_disable_but");
            CatusTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(700, 640), new Vector2(32, 72), CactusTower.COST);

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\pineapple_tower_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\pineapple_tower_disable_but");
            PineappleTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(800, 640), new Vector2(32, 72), PineappleTower.COST);

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\buttons\upgrade_enable_but");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\buttons\upgrade_disable_but");
            UpgradeLabel = new ToggleValueLabel(textureEnable, textureDisable, Vector2.Zero, new Vector2(12, 41));

            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\sell_but");
            SellLabel = new ValueLabel(texture, Vector2.Zero, new Vector2(12, 41));

            textureEnable = Content.Load<Texture2D>(@"images\gameplay\range_enable");
            textureDisable = Content.Load<Texture2D>(@"images\gameplay\range_disable");
            RangeLabel = new ToggleLabel(textureEnable, textureDisable, Vector2.Zero);
            RangeLabel.LayerDepth = 0.31f;

            CursorLabel = new Label();
            CursorLabel.LayerDepth = 0.3f;

            wave_font = Content.Load<SpriteFont>(@"fonts\gameplay\wave_info");
            LoadMap(@"data\maps\map1");
            
            //Load du lieu cho camera
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            Camera2D.Reset(viewport.Width,viewport.Height,background_layer.Width,background_layer.Height);

            //Load content sau khi LoadMap
            WaveTable.LoadContent(Content);

            HudLayer = new HUDLayer(this);
            HudLayer.LoadContent();
        }

        public void LoadMap(string map_file)
        {
            ContentManager Content = sceneManager.Game.Content;
            Map map = Content.Load<Map>(map_file);
            map.PostReading();

            width = map.Width;
            height = map.Height;
            tile_size = map.TileSize;
            
            Texture2D background_texture = Content.Load<Texture2D>(map.BackgroundFile);
            background_layer = new BackgroundLayer(Vector2.Zero, background_texture);
            
            tower_map = new byte[width*height];
            for (int i = 0; i < tower_map.Length; i++)
            {
                if (map.InteractiveMap[i] == Map.BLANK) { tower_map[i] = CellType.BLANK; }
                else tower_map[i] = CellType.OTHER; 
            }
            //Dat waves info cho WaveTable
            WaveTable = new NextWavesTable(map.Waves);

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

            //Phan sound
            songs = new List<Song>();
            Song song;
            for (int i = 0; i < map.SongFiles.Count; i++)
            {
                song = Content.Load<Song>(map.SongFiles[i]);
                songs.Add(song);
            }
            currentSongIndex = 0;
        }

        public int TileSize { get { return tile_size; } }
        #region Utility
        public Vector2 GetCenterFromCell(int x,int y)
        {
            return new Vector2(x * tile_size + tile_size/2 , y * tile_size + tile_size/2);
        }

        public Vector2 GetBottomLeftFromCell(int x, int y)
        {
            return new Vector2(x * tile_size, (y + 1) * tile_size - 1);
        }
        public bool IsBlank(int x,int y)
        {
            if (InBound(x, y) && tower_map[y * width + x] == CellType.BLANK)
                return true;
            else return false;
        }

        public bool IsTower(int x, int y)
        {
            if (InBound(x, y) && tower_map[y * width + x] == CellType.TOWER)
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
                waypoints.Enqueue(GetCenterFromCell(path.Waypoints[i].tile_x,path.Waypoints[i].tile_y));
            }
            return waypoints;
        }
        #endregion

        private void OakTowerLabel_Clicked()
        {
            Console.WriteLine("ArrowTower is selected to add");
            tower_type = TowerType.OakTower;
            CursorLabel.Texture = OakTower.TEXTURE_LV1;
            RangeLabel.Scale = (float)OakTower.RANGE / 250;
            is_tower_add = true;
        }
        private void CatusTowerLabel_Clicked()
        {
            Console.WriteLine("SlowTower is selected to add");
            tower_type = TowerType.CactusTower;
            CursorLabel.Texture = CactusTower.TEXTURE_LV1;
            RangeLabel.Scale = (float)CactusTower.RANGE / 250;
            is_tower_add = true;
        }
        private void PineappleTowerLabel_Clicked()
        {
            Console.WriteLine("SlowTower is selected to add");
            tower_type = TowerType.PineappleTower;
            CursorLabel.Texture = PineappleTower.TEXTURE_LV1;
            RangeLabel.Scale = (float)PineappleTower.RANGE / 250;
            is_tower_add = true;
        }

        private void SellLabel_Clicked()
        {
            Console.WriteLine("Tower is sold");
            tower_manager.RemoveTower(tower_keypos);
            money += SellLabel.Value;
            tower_map[tower_keypos] = CellType.BLANK;
            tower_keypos = -1; is_tower_select = false;
        }
        private void UpgradeLabel_Clicked()
        {
            Console.WriteLine("Tower is upgraded");
            tower_manager.UpgradeTower(tower_keypos);
            money -= UpgradeLabel.Value;
            tower_keypos = -1; is_tower_select = false;
        }

        public override void  Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(songs[currentSongIndex]);
                currentSongIndex = (currentSongIndex + 1) % songs.Count;
            }
            HudLayer.Update(gameTime);
            
            if (wave_manager.Waiting){ WaveTable.Update(gameTime, wave_manager.CurrentWaveNumber); }
            else { WaveTable.Update(gameTime, wave_manager.CurrentWaveNumber + 1); }
            //Cap nhat Camera
            Camera2D.Update(gameTime);

            OakTowerLabel.Update(money);
            CatusTowerLabel.Update(money);
            PineappleTowerLabel.Update(money);
            if (is_tower_select)
            {
                UpgradeLabel.Update(money);
            }

            MouseState mouseState = Mouse.GetState();

            //Neu la trang thai click chuot phai
            if (mouseState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed)
            {
                Console.WriteLine("Right mouse clicked");
                is_tower_add = false;
                is_tower_select = false;
                if (tower_keypos != -1)
                {
                    Tower tower = tower_manager.GetTower(tower_keypos);
                    tower.LayerDepth = tower_layer_depth;
                    tower_keypos = -1;
                }
            }
            //Neu la trang thai click chuot trai
            else if (mouseState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
            {
                //Neu chua co gi xay ra
                if (!is_tower_add && !is_tower_select)
                {
                    //Kiem tra xem nut nao duoc click
                    if (OakTowerLabel.InBound(mouseState.X, mouseState.Y) && OakTowerLabel.Active) { OakTowerLabel_Clicked(); }
                    if (CatusTowerLabel.InBound(mouseState.X, mouseState.Y) && CatusTowerLabel.Active) { CatusTowerLabel_Clicked(); }
                    if (PineappleTowerLabel.InBound(mouseState.X, mouseState.Y) && PineappleTowerLabel.Active) { PineappleTowerLabel_Clicked(); }

                    if (!is_tower_add)
                    {
                        //Console.WriteLine("No tower add");
                        int tile_x, tile_y;
                        Matrix inverseTransform = Matrix.Invert(Camera2D.Transform);
                        Vector2 mouseRealPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverseTransform);

                        tile_x = (int)(mouseRealPosition.X / tile_size);
                        tile_y = (int)(mouseRealPosition.Y / tile_size);

                        if (IsTower(tile_x, tile_y))
                        {
                            Console.WriteLine("Tower is selected");
                            tower_keypos = tile_y * width + tile_x;

                            Tower tower = tower_manager.GetTower(tower_keypos);
                            //Dat sell label
                            SellLabel.Center = tower.Center - new Vector2(tile_size, 0);
                            SellLabel.Value = tower.SellCost;
                            
                            //Dat upgrade label
                            UpgradeLabel.Center = tower.Center + new Vector2(tile_size, 0);
                            UpgradeLabel.Value = tower.UpgradeCost;
                            UpgradeLabel.Max = false;
                            if (tower.Level == 3) { UpgradeLabel.Max = true; }
                            
                            //Dat range label
                            RangeLabel.Scale = (float)tower.Range / 250;
                            RangeLabel.Center = tower.Center;
                            RangeLabel.Active = true;

                            tower_layer_depth = tower.LayerDepth;
                            tower.LayerDepth = RangeLabel.LayerDepth - 0.01f;

                            is_tower_select = true;
                        }
                    }
                    goto exit;

                }
                //Neu dang chon tower
                if (!is_tower_add && is_tower_select)
                {
                    Matrix inverseTransform = Matrix.Invert(Camera2D.Transform);
                    Vector2 mouseRealPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverseTransform);

                    if (tower_keypos != -1){
                        Tower tower = tower_manager.GetTower(tower_keypos);
                        tower.LayerDepth = tower_layer_depth;
                    }

                    if (UpgradeLabel.InBound(mouseRealPosition) && UpgradeLabel.Active) { UpgradeLabel_Clicked(); }
                    if (SellLabel.InBound(mouseRealPosition)) { SellLabel_Clicked(); }
                    //Neu click ra ngoai
                    if (is_tower_select) { 
                        is_tower_select = false;
                        tower_keypos = -1;
                    }
                    
                    goto exit;
                }
                //Neu dang dinh add tower
                if (is_tower_add && !is_tower_select)
                {
                    int tile_x, tile_y;
                    Matrix inverseTransform = Matrix.Invert(Camera2D.Transform);
                    Vector2 mouseRealPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverseTransform);

                    tile_x = (int)(mouseRealPosition.X / tile_size);
                    tile_y = (int)(mouseRealPosition.Y / tile_size);

                    if (IsBlank(tile_x, tile_y))
                    {
                        int key_pos = tile_y * width + tile_x;

                        Tower tower;
                        switch (tower_type)
                        {
                            case TowerType.OakTower:
                                Console.WriteLine("Oak Tower is added");
                                tower = new OakTower(GetBottomLeftFromCell(tile_x, tile_y),Anchor.BOTTOMLEFT);
                                tower.LayerDepth += (width - tile_x) * LAYER_DEPTH_CHANGE + (height - tile_y) * LAYER_DEPTH_CHANGE; 
                                tower_manager.AddTower(key_pos, tower); money -= OakTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;
                            
                            case TowerType.CactusTower:
                                Console.WriteLine("Catus Tower is added");
                                tower = new CactusTower(GetBottomLeftFromCell(tile_x, tile_y), Anchor.BOTTOMLEFT);
                                tower.LayerDepth += (width - tile_x) * LAYER_DEPTH_CHANGE + (height - tile_y) * LAYER_DEPTH_CHANGE; 
                                tower_manager.AddTower(key_pos, tower); money -= CactusTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;

                            case TowerType.PineappleTower:
                                Console.WriteLine("Pineapple Tower is added");
                                tower = new PineappleTower(GetBottomLeftFromCell(tile_x, tile_y), Anchor.BOTTOMLEFT);
                                tower.LayerDepth += (width - tile_x) * LAYER_DEPTH_CHANGE + (height - tile_y) * LAYER_DEPTH_CHANGE; 
                                tower_manager.AddTower(key_pos, tower); money -= PineappleTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;
                        }
                    }
                    is_tower_add = false;

                    goto exit;
                }
            exit: ;
            }
            
            if (is_tower_add)
            {
                int tile_x, tile_y;
                Matrix inverseTransform = Matrix.Invert(Camera2D.Transform);
                Vector2 mouseRealPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverseTransform);

                tile_x = (int)(mouseRealPosition.X / tile_size);
                tile_y = (int)(mouseRealPosition.Y / tile_size);

                RangeLabel.Center = mouseRealPosition;
                CursorLabel.Center = mouseRealPosition;
                if (IsBlank(tile_x, tile_y)){
                    //Cursor.getInstance().SetCursor(tower_texture, mouseRealPosition, Color.Yellow);
                    CursorLabel.Color = Color.Yellow;
                    RangeLabel.Active = true;
                }
                else{
                    //Cursor.getInstance().SetCursor(tower_texture, mouseRealPosition, Color.Red);
                    CursorLabel.Color = Color.Red;
                    RangeLabel.Active = false;
                }
            }
            
            previousState = mouseState;
                       
            wave_manager.Update(gameTime);

            if (!wave_manager.Finish)
            {
                points += wave_manager.CurrentWave.DeathPoint;
                lives -= wave_manager.CurrentWave.ReachedEndNumber;
                lives = Math.Max(lives, 0);
                if (lives <= 0){
                    tower_manager.Update(gameTime, null);
                    Console.WriteLine("You loose");
                }
                else{
                    tower_manager.Update(gameTime, wave_manager.CurrentWave.ActiveEnemies);
                }
            }
            else
            {
                tower_manager.Update(gameTime, null);
                Console.WriteLine("You win");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Ve background 1 lan thoi
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,null,null,null,null,Camera2D.Transform);
            //tower_handle_layer.Draw(spriteBatch);
                background_layer.Draw(spriteBatch);
                wave_manager.Draw(spriteBatch);
                tower_manager.Draw(spriteBatch);                
                if (is_tower_select){
                    RangeLabel.Draw(spriteBatch);
                    UpgradeLabel.Draw(spriteBatch);
                    SellLabel.Draw(spriteBatch);
                }
                if (is_tower_add){
                    CursorLabel.Draw(spriteBatch);
                    RangeLabel.Draw(spriteBatch);
                }            
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                if (wave_manager.Waiting)
                {
                    spriteBatch.DrawString(wave_font, "Waiting: " + (int)wave_manager.Timer, new Vector2(430, 110), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
                }
                spriteBatch.DrawString(wave_font, "Wave: " + wave_manager.CurrentWaveNumber, new Vector2(20, 110), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
                spriteBatch.DrawString(wave_font, "Total: " + wave_manager.TotalWaveNumber, new Vector2(20, 160), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.08f);
                OakTowerLabel.Draw(spriteBatch);
                CatusTowerLabel.Draw(spriteBatch);
                PineappleTowerLabel.Draw(spriteBatch);
                WaveTable.Draw(spriteBatch);
                HudLayer.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

