using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Cursor
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;
        private Color color;
        private float layer_depth = 0.3f;

        private static Cursor cursor;
        private Cursor(){}

        public static Cursor getInstance()
        {
            if (cursor == null) cursor = new Cursor();
            return cursor;
        }

        public void SetCursor(Texture2D texture, Vector2 center, Color color){
            this.texture = texture;
            this.center = center;
            this.position.X = center.X - texture.Bounds.Width / 2;
            this.position.Y = center.Y - texture.Bounds.Height / 2;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0.0f ,Vector2.Zero, 1.0f ,SpriteEffects.None,layer_depth);
        }
    }
}
