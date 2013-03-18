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

namespace CustomGame
{
    public class CellType{
        public const byte BLANK = 0;
        public const byte TOWER = 1;
        public const byte OTHER = 2;
    }
    
    public class GamePlayScene : GameScene
    {
        private BackgroundLayer background_layer;

        private int width, height;
        private int tile_size;
        
        private byte[] tower_map;
        
        //Phan game play
        private int lives=20;
        private int money=1000;
        private int points = 0;

        //private HudLayer hud_layer;
        //private TowerHandleLayer tower_handle_layer;
        private bool is_tower_add = false;
        private TowerType tower_type = TowerType.NONE;
        private Texture2D tower_texture;
        
        private bool is_tower_select = false;
        private int tower_keypos = -1;

        private WaveManager wave_manager;
        private TowerManager tower_manager;

        private MouseState previousState;
        #region Buttons
        ToggleValueLabel OakTowerLabel;
        ToggleValueLabel CatusTowerLabel;
        ToggleValueLabel PineappleTowerLabel;
        ToggleValueLabel UpgradeLabel;
        ValueLabel SellLabel;
        #endregion

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
            ContentManager content = SceneManager.Game.Content;
            //Load Enemy texture
            Enemy.HEALTH_BAR_TEXTURE = content.Load<Texture2D>(@"images\gameplay\health_bar");
            AxeMan.TEXTURE = content.Load<Texture2D>(@"images\gameplay\enemies\axeman");
            
            //Load Tower texture
            OakTower.TEXTURE = content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level1");
            CatusTower.TEXTURE = content.Load<Texture2D>(@"images\gameplay\towers\catus_tower_level1");
            PineappleTower.TEXTURE = content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level1");

            //Load bullets texture
            OakTower.BULLET_TEXTURE = content.Load<Texture2D>(@"images\gameplay\bullets\acorn");

            //Load cac label
            Texture2D texture;
            Texture2D textureEnable, textureDisable;

            ValueLabel.FONT = content.Load<SpriteFont>(@"fonts\gameplay\value_font");
            textureEnable = content.Load<Texture2D>(@"images\gameplay\buttons\oak_tower_enable_but");
            textureDisable = content.Load<Texture2D>(@"images\gameplay\buttons\oak_tower_disable_but");
            OakTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(600, 640), new Vector2(32,72),OakTower.COST);

            textureEnable = content.Load<Texture2D>(@"images\gameplay\buttons\catus_tower_enable_but");
            textureDisable = content.Load<Texture2D>(@"images\gameplay\buttons\catus_tower_disable_but");
            CatusTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(700, 640), new Vector2(32, 72), CatusTower.COST);

            textureEnable = content.Load<Texture2D>(@"images\gameplay\buttons\pineapple_tower_enable_but");
            textureDisable = content.Load<Texture2D>(@"images\gameplay\buttons\pineapple_tower_disable_but");
            PineappleTowerLabel = new ToggleValueLabel(textureEnable, textureDisable, new Vector2(800, 640), new Vector2(32, 72), PineappleTower.COST);

            textureEnable = content.Load<Texture2D>(@"images\gameplay\buttons\upgrade_enable_but");
            textureDisable = content.Load<Texture2D>(@"images\gameplay\buttons\upgrade_disable_but");

            UpgradeLabel = new ToggleValueLabel(textureEnable, textureDisable, Vector2.Zero, new Vector2(12, 41));

            texture = content.Load<Texture2D>(@"images\gameplay\buttons\sell_but");
            SellLabel = new ValueLabel(texture, Vector2.Zero, new Vector2(12, 41));
            
            LoadMap(@"data\maps\map1");
            
            //Load du lieu cho camera
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            Camera2D.Reset(viewport.Width,viewport.Height,background_layer.Width,background_layer.Height);
        }

        public void LoadMap(string map_file)
        {
            Map map = SceneManager.Game.Content.Load<Map>(map_file);
            map.PostReading();

            width = map.Width;
            height = map.Height;
            tile_size = map.TileSize;
            
            Texture2D background_texture = SceneManager.Game.Content.Load<Texture2D>(map.BackgroundFile);
            background_layer = new BackgroundLayer(Vector2.Zero, background_texture);
            
            tower_map = new byte[width*height];
            for (int i = 0; i < tower_map.Length; i++)
            {
                if (map.InteractiveMap[i] == Map.BLANK) { tower_map[i] = CellType.BLANK; }
                else tower_map[i] = CellType.OTHER; 
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
            tower_texture = OakTower.TEXTURE;
            is_tower_add = true;
        }

        private void CatusTowerLabel_Clicked()
        {
            Console.WriteLine("SlowTower is selected to add");
            tower_type = TowerType.CatusTower;
            tower_texture = CatusTower.TEXTURE;
            is_tower_add = true;
        }
        private void PineappleTowerLabel_Clicked()
        {
            Console.WriteLine("SlowTower is selected to add");
            tower_type = TowerType.PineappleTower;
            tower_texture = PineappleTower.TEXTURE;
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
            //Cap nhat Camera
            Camera2D.Update(gameTime);

            OakTowerLabel.Update(money);
            CatusTowerLabel.Update(money);
            PineappleTowerLabel.Update(money);
            if(is_tower_select){ UpgradeLabel.Update(money);}

            MouseState mouseState = Mouse.GetState();

            //Neu la trang thai click chuot phai
            if (mouseState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed)
            {
                Console.WriteLine("Right mouse clicked");
                is_tower_add = false; tower_type = TowerType.NONE;
                is_tower_select = false; tower_keypos = -1;
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
                            SellLabel.Center = tower.Center - new Vector2(tile_size, 0);
                            SellLabel.Value = tower.SellCost;

                            UpgradeLabel.Center = tower.Center + new Vector2(tile_size, 0);
                            UpgradeLabel.Value = tower.UpgradeCost;

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

                    if (UpgradeLabel.InBound(mouseRealPosition) && UpgradeLabel.Active) { UpgradeLabel_Clicked(); }
                    if (SellLabel.InBound(mouseRealPosition)) { SellLabel_Clicked(); }
                    //Neu click ra ngoai
                    if (is_tower_select) { is_tower_select = false; tower_keypos = -1; }
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
                                tower_manager.AddTower(key_pos, tower); money -= OakTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;
                            
                            case TowerType.CatusTower:
                                Console.WriteLine("Catus Tower is added");
                                tower = new CatusTower(GetBottomLeftFromCell(tile_x, tile_y), Anchor.BOTTOMLEFT);
                                tower_manager.AddTower(key_pos, tower); money -= CatusTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;

                            case TowerType.PineappleTower:
                                Console.WriteLine("Pineapple Tower is added");
                                tower = new PineappleTower(GetBottomLeftFromCell(tile_x, tile_y), Anchor.BOTTOMLEFT);
                                tower_manager.AddTower(key_pos, tower); money -= PineappleTower.COST;
                                tower_map[key_pos] = CellType.TOWER;
                                break;
                        }
                    }
                    is_tower_add = false; tower_type = TowerType.NONE;

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

                if (IsBlank(tile_x, tile_y)){
                    Cursor.getInstance().SetCursor(tower_texture, mouseRealPosition, Color.Yellow);
                }
                else{
                    Cursor.getInstance().SetCursor(tower_texture, mouseRealPosition, Color.Red);
                }
            }
            
            previousState = mouseState;
            
            
            wave_manager.Update(gameTime);
            if (!wave_manager.Finish)
            {
                tower_manager.Update(gameTime, wave_manager.CurrentWave.ActiveEnemies);
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
                    UpgradeLabel.Draw(spriteBatch);
                    SellLabel.Draw(spriteBatch);
                }
                if (is_tower_add){
                    Cursor.getInstance().Draw(spriteBatch);
                }            
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);          
                OakTowerLabel.Draw(spriteBatch);
                CatusTowerLabel.Draw(spriteBatch);
                PineappleTowerLabel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

