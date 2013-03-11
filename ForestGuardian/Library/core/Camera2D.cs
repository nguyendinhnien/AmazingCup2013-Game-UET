using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Library
{
    public class Camera2D
    {
        private static Matrix transform;
        public static Matrix Transform
        {
            get { return transform; }
        }
        private static Vector2 position;
        private static Viewport viewport;

        private static float zoom = 1.0f;
        
        //Kiem tra xem co su thay doi voi camera ko
        private static bool is_changed = true;

        public static float X
        {
            get { return position.X; }
            set { position.X = value; is_changed = true;}
        }
        public static float Y
        {
            get { return position.Y; }
            set { position.Y = value; is_changed = true;}
        }

        public static float Zoom
        {
            get { return zoom; }
            set { zoom = value; is_changed = true; if (zoom < 0.1f) { zoom = 0.1f; } }
        }

        public static void Reset(Viewport _viewport)
        {
            viewport = _viewport;
            position = Vector2.Zero;
            zoom = 1.0f;
            is_changed = true;
        }
        public static void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left)){ X -= 3.0f; }
            else if (keyState.IsKeyDown(Keys.Right)){ X += 3.0f; } 

            if (keyState.IsKeyDown(Keys.Up)){ Y -= 3.0f; }
            else if (keyState.IsKeyDown(Keys.Down)){ Y += 3.0f; }

            if (keyState.IsKeyDown(Keys.A)) { Zoom -= 0.01f; }
            else if (keyState.IsKeyDown(Keys.S)) { Zoom += 0.01f; }

            if (is_changed)
            {
                transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1.0f));
                is_changed = false;
            }
        }
    }
}
