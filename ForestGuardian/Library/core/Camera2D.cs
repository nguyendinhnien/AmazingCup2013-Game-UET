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

        private static Vector2 minPosition;
        private static Vector2 maxPosition;

        private static float minZoom;
        private static float maxZoom;

        private static int world_width, world_height;
        private static int viewport_width, viewport_height;

        private static float zoom;
        
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

        public static void Reset(int viewport_width,int viewport_height, int world_width, int world_height)
        {
            Camera2D.viewport_width = viewport_width;
            Camera2D.viewport_height = viewport_height;
            Camera2D.world_width = world_width;
            Camera2D.world_height = world_height;

            minPosition = Vector2.Zero;
            maxZoom = 1.5f;

            float ratio;
            float widthRatio = (float)world_width / viewport_width;
            float heightRatio = (float)world_height / viewport_height;

            if (widthRatio < heightRatio){
                ratio = widthRatio;              
            }
            else {ratio = heightRatio;}

            minZoom = 1.0f / ratio;
            if (minZoom > 1.5f) { zoom = minZoom = maxZoom; }
            else if (minZoom > 1.0f) { zoom = minZoom; }
            else { zoom = 1.0f; }
            
            is_changed = true;
        }
        public static void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            //Kiem tra xem co bi zoom khong
            if (keyState.IsKeyDown(Keys.A)) { Zoom -= 0.01f; }
            else if (keyState.IsKeyDown(Keys.S)) { Zoom += 0.01f; }
            
            //Kiem tra xem co bi di chuyen khong
            if (keyState.IsKeyDown(Keys.Left)){ X -= 3.0f; }
            else if (keyState.IsKeyDown(Keys.Right)){ X += 3.0f; } 

            if (keyState.IsKeyDown(Keys.Up)){ Y -= 3.0f; }
            else if (keyState.IsKeyDown(Keys.Down)){ Y += 3.0f; }

            if (is_changed)
            {
                zoom = Math.Max(Math.Min(zoom, maxZoom), minZoom);
 
                maxPosition.X = Math.Max(0, world_width - viewport_width / zoom);
                maxPosition.Y = Math.Max(0, world_height - viewport_height / zoom);
                position = Vector2.Clamp(position, minPosition, maxPosition);

                transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1.0f));
                is_changed = false;
            }
        }
    }
}
