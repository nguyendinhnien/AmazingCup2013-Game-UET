using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TowerDefenseGame.manager;
using TowerDefenseGame.entity.enemy;
using TowerDefenseGame.entity.tower;
using TowerDefenseGame.entity.bullet;
using TowerDefenseGame.gameplay;

namespace TowerDefenseGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Enemy enemy;
        Queue<int> enemy_list;
        Wave wave;
        Tower tower;
        Bullet bullet;
        TowerManager tower_manager;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            AxeMan.TEXTURE = Content.Load<Texture2D>(AxeMan.TextureLocation);
            ArrowTower.TEXTURE = Content.Load<Texture2D>(ArrowTower.TextureLocation);
            Arrow.TEXTURE = Content.Load<Texture2D>(Arrow.TextureLocation);
            //Test
            Texture2D enemy_texture = Content.Load<Texture2D>("demo_enemy");
            Vector2 center = new Vector2(20, 30);
            //enemy = new Enemy(enemy_texture, position, 1, 1, 1);
            
            Queue<Vector2> waypoints = new Queue<Vector2>();
            waypoints.Enqueue(center);
            waypoints.Enqueue(new Vector2(20, 200));
            waypoints.Enqueue(new Vector2(160, 200));
            waypoints.Enqueue(new Vector2(160, 80));
            waypoints.Enqueue(new Vector2(50, 80));
            //enemy.setWaypoints(waypoints);
            enemy_list = new Queue<int>();
            enemy_list.Enqueue(1);
            enemy_list.Enqueue(0); enemy_list.Enqueue(0); enemy_list.Enqueue(0);
            enemy_list.Enqueue(1); enemy_list.Enqueue(1); enemy_list.Enqueue(1);

            wave = new Wave(100, 4, enemy_list, waypoints);
            tower = new ArrowTower(new Vector2(400, 300));

            bullet = new Arrow(new Vector2(20,20), 10, 10.0f, new Vector2(800, 600));
            tower_manager = new TowerManager();
            tower_manager.AddTower(tower);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //enemy.Update(gameTime);
            bullet.Update(gameTime);
            tower_manager.Update(gameTime, wave.ActiveEnemies);
            wave.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
                bullet.Draw(spriteBatch);
                tower_manager.Draw(spriteBatch);
                wave.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
