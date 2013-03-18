using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class BackgroundLayer
    {
        private Vector2 position;
        private Texture2D texture;

        //Z order the hien thu tu cua layer. Z order cang lon thi layer se nam tren
        private float layer_depth = 1.0f;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;

        public BackgroundLayer(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null,Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, layer_depth);
        }

        public int Width
        {
            get { return texture.Width; }
        }
        public int Height
        {
            get { return texture.Height; }
        }
    }
}
