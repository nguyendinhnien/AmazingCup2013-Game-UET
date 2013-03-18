using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CustomGame
{
    public class GameSetting
    {
        private static int screenWidth = 1024;
        private static int screenHeight = 768;
        private static bool fullscreen = false;
        private GraphicsDeviceManager graphics;

        public static void InitSetting(GraphicsDeviceManager graphics){
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();

            Viewport viewport = new Viewport();
            viewport.X = 0; viewport.Y = 0;
            viewport.Width = screenWidth;
            viewport.Height = screenHeight;

            graphics.GraphicsDevice.Viewport = viewport;
        }

    }
}
