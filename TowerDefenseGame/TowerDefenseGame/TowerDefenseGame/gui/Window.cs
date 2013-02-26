using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseGame.core;

namespace TowerDefenseGame.gui
{
    class Window : Sprite
    {
        protected MouseState current_mouseState;
        protected MouseState previous_mouseState;

        protected Rectangle windowRectangle;
        protected bool visible;

        public Window(Texture2D texture, Vector2 position, int width, int height)
            :base(texture,position){
            
            windowRectangle = new Rectangle((int)position.X,(int)position.Y,width,height);
            
            current_mouseState = Mouse.GetState();
            previous_mouseState = current_mouseState;

            visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            previous_mouseState = current_mouseState;
            current_mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
