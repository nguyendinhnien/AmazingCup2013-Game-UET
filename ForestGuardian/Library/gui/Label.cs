using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Label
    {
        protected Texture2D texture;

        protected Vector2 position;
        //protected Rectangle bounds;
        protected float layer_depth = 0.2f;
        protected float rotation = 0.0f;
        protected float scale = 1.0f;
        protected Color color = Color.White;

        public Label() { }
        public Label(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            //this.bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
        }

        #region Set,Get Method
        public int Width { get { return texture.Bounds.Width; } }
        
        public int  Height { get { return texture.Bounds.Height; } }

        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value;}
        }

        public Vector2 Center
        {
            get { return new Vector2(position.X + scale * texture.Bounds.Width / 2, position.Y + scale * texture.Bounds.Height / 2); }
            set { position.X = value.X - scale * texture.Bounds.Width / 2; position.Y = value.Y - scale * texture.Bounds.Height / 2; }
            
        }
        
        public float LayerDepth{
            get { return layer_depth; }
            set { layer_depth = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        #endregion

        public bool InBound(Vector2 pos)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
            return bounds.Contains((int)pos.X, (int)pos.Y);
        }

        public bool InBound(float x,float y)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
            return bounds.Contains((int) x, (int) y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, layer_depth);
        }
    }
}
