using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CustomGame
{
    public class SceneManager : DrawableGameComponent
    {
        List<GameScene> scenes = new List<GameScene>();
        List<GameScene> scenesToUpdate = new List<GameScene>();

        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;

        bool isInitialized;
        bool traceEnabled;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        public SceneManager(Game game)
            : base(game)
        {
            this.graphics = ((GameManager)game).getGraphics();
        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            // Load content belonging to the scene manager.
            ContentManager content = Game.Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Tell each of the screens to load their content.
            foreach (GameScene scene in scenes)
            {
                scene.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScene scene in scenes)
            {
                scene.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Make a copy of the master scene list, to avoid confusion if
            // the process of updating one scene adds or removes others.
            scenesToUpdate.Clear();

            foreach (GameScene scene in scenes)
                scenesToUpdate.Add(scene);

            bool otherSceneHasFocus = !Game.IsActive;
            bool coveredByOtherScene = false;

            // Loop as long as there are screens waiting to be updated.
            while (scenesToUpdate.Count > 0)
            {
                // Pop the topmost scene off the waiting list.
                GameScene scene = scenesToUpdate[scenesToUpdate.Count - 1];

                scenesToUpdate.RemoveAt(scenesToUpdate.Count - 1);

                // Update the scene.
                scene.StateUpdate(gameTime, otherSceneHasFocus, coveredByOtherScene);

                if (scene.SceneState == SceneState.TransitionOn ||
                    scene.SceneState == SceneState.Active)
                {
                    // If this is the first active scene we came across,
                    // give it a chance to handle input.
                    if (!otherSceneHasFocus)
                    {
                        //NTA change scene.HandleInput() to scene.Update(gameTime)
                        scene.Update(gameTime);
                        otherSceneHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!scene.IsPopup)
                        coveredByOtherScene = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScenes();
        }

        void TraceScenes()
        {
            List<string> sceneNames = new List<string>();

            foreach (GameScene scene in scenes)
                sceneNames.Add(scene.GetType().Name);

            #if WINDOWS
            Trace.WriteLine(string.Join(", ", sceneNames.ToArray()));
            #endif
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScene scene in scenes)
            {
                if (scene.SceneState == SceneState.Hidden)
                    continue;

                scene.Draw(spriteBatch);
            }
        }

        public void AddScene(GameScene scene)
        {
            scene.SceneManager = this;
            scene.IsExiting = false;

            // If we have a graphics device, tell the scene to load content.
            if (isInitialized)
            {
                scene.LoadContent();
            }

            scenes.Add(scene);
        }

        public void RemoveScene(GameScene scene)
        {
            // If we have a graphics device, tell the scene to unload content.
            if (isInitialized)
            {
                scene.UnloadContent();
            }

            scenes.Remove(scene);
            scenesToUpdate.Remove(scene);
        }

        public GameScene[] GetScenes()
        {
            return scenes.ToArray();
        }

        public void ToggleFullScreen()
        {
            graphics.ToggleFullScreen();
        }

        public void ResetGame()
        {
            scenes.Clear();
            this.AddScene(new GamePlayScene());
        }

        //back to main menu
        public void ExitGame()
        {
            scenes.Clear();
            this.AddScene(new MainMenuScene());
        }
    }
}

