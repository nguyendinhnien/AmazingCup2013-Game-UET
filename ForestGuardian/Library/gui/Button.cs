using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public enum ButtonStatus
    {
        Normal,
        Hovering,
        Pressing,
    }

    public class Button
    {
        protected MouseState previousState;

        // The the different state textures.
        protected Texture2D hoverTexture;
        protected Texture2D pressTexture;
        protected Texture2D buttonTexture;
        protected Texture2D drawTexture;

        protected Vector2 position;
        protected Rectangle bounds;

        protected float layer_depth = 0.1f;
        protected float Rotation = 0.0f;
        protected float Scale = 1.0f;
        
        private ButtonStatus state = ButtonStatus.Normal;

        public event EventHandler Clicked;
        public event EventHandler Pressed;
        public event EventHandler Hovered;

        public Button(Texture2D texture, Texture2D hoverTexture, Texture2D pressTexture, Vector2 position)
        {
            this.hoverTexture = hoverTexture;
            this.pressTexture = pressTexture;
            this.buttonTexture = texture;
            this.position = position;

            this.bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; bounds = new Rectangle((int)position.X, (int)position.Y, (int)(buttonTexture.Width), (int)(buttonTexture.Height)); }
        }

        public Vector2 Center
        {
            get { return new Vector2(position.X + buttonTexture.Bounds.Width / 2, position.Y + buttonTexture.Bounds.Height / 2); }
            set { position.X = value.X - buttonTexture.Bounds.Width / 2; position.Y = value.Y - buttonTexture.Bounds.Height / 2;
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)(buttonTexture.Width), (int)(buttonTexture.Height));}
        }

        public bool InBound(Vector2 pos)
        {
            return bounds.Contains((int)pos.X, (int)pos.Y);
        }

        public virtual void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            bool isMouseOver = bounds.Contains(mouseState.X, mouseState.Y);

            if (isMouseOver && state != ButtonStatus.Pressing)
            {
                //Trang thai la MouseOver
                state = ButtonStatus.Hovering;
                //Neu khong phai hover
                if (Hovered != null) { Hovered(this, EventArgs.Empty); }
            }
            else if (isMouseOver == false && state != ButtonStatus.Pressing)
            {
                state = ButtonStatus.Normal;
            }

            //Trang thai Pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.Pressing;
                    if (Pressed != null) { Pressed(this, EventArgs.Empty); }
                }
                //NTA added
                else
                {
                    state = ButtonStatus.Normal;
                }
            }

            //Trang thai Clicked
            if (mouseState.LeftButton == ButtonState.Released &&
                previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.Hovering;
                    if (Clicked != null) { Clicked(this, EventArgs.Empty); }
                }
                //Neu click ra ngoai thi coi nhu ko click
                else if (state == ButtonStatus.Pressing)
                {
                    state = ButtonStatus.Normal;
                }
            }
            //Cap nhat mouse state
            previousState = mouseState;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (state == ButtonStatus.Pressing && pressTexture != null)
                drawTexture = pressTexture;
            else if (state == ButtonStatus.Hovering && hoverTexture != null)
                drawTexture = hoverTexture;
            else
                drawTexture = buttonTexture;
            spriteBatch.Draw(drawTexture, this.position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, layer_depth);
        }
    }
}