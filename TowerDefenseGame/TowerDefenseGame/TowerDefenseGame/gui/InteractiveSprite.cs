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
    class InteractiveSprite: Sprite
    {
        protected MouseState current_mouseState;
        protected MouseState previous_mouseState;

        //Dinh nghia con tro ham
        protected delegate void MouseEvent(object sender);

        //MouseEvent LeftMouseClick;
        MouseEvent LeftMousePress;
        MouseEvent LeftMouseRelease;
        MouseEvent MouseOver;
        MouseEvent MouseLeave;

        protected bool visible;
        
        public InteractiveSprite(Texture2D texture, Vector2 position)
            :base(texture, position)
        {           
            //LeftMouseClick = new MouseEvent(doLeftMouseClick);
            LeftMousePress = new MouseEvent(doLeftMousePress);
            LeftMouseRelease = new MouseEvent(doLeftMouseRelease);
            MouseOver = new MouseEvent(doMouseOver);
            MouseLeave = new MouseEvent(doMouseLeave);

            current_mouseState = Mouse.GetState();
            previous_mouseState = current_mouseState;

            visible = false;
        }

        //protected void doLeftMouseClick(object sender){}
        protected void doLeftMousePress(object sender){}
        protected void doLeftMouseRelease(object sender){}
        protected void doMouseOver(object sender){}
        protected void doMouseLeave(object sender){}

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public override void Update(GameTime gameTime)
        {
            previous_mouseState = current_mouseState;
            current_mouseState = Mouse.GetState();
            if (visible)
            {
                //Chu y den viec scale sprite
                Rectangle spriteRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                if (spriteRectangle.Contains(new Point(current_mouseState.X, current_mouseState.Y)))
                {

                    doMouseOver(this);

                    if (current_mouseState.LeftButton == ButtonState.Pressed
                        && previous_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        doLeftMousePress(this);
                    }
                    else if (current_mouseState.LeftButton == ButtonState.Released
                        && previous_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        doLeftMouseRelease(this);
                    }
                }
                else
                {
                    doMouseLeave(this);
                }
            }
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
