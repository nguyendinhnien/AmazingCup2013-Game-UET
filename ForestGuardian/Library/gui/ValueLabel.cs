using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class ValueLabel: Label
    {
        public static SpriteFont FONT;
        protected int value;
        protected Vector2 value_offset;
        protected Vector2 value_position;
        protected bool max = false;

        public ValueLabel(Texture2D texture, Vector2 position, Vector2 value_offset)
            :base(texture,position){

            this.value_offset = value_offset;
            this.value_position = position + value_offset;
        }

        public ValueLabel(Texture2D texture, Vector2 position, Vector2 value_offset, int value)
            :base(texture,position)
        {
            this.value = value;
            this.value_offset = value_offset;
            this.value_position = position + value_offset;
        }
        
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public bool Max
        {
            get { return max; }
            set { max = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { 
                position = value;
                value_position = position + value_offset;
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(position.X + texture.Bounds.Width / 2, position.Y + texture.Bounds.Height / 2); }
            set
            {
                position.X = value.X - texture.Bounds.Width / 2; position.Y = value.Y - texture.Bounds.Height / 2;
                value_position = position + value_offset;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (max)
            {
                spriteBatch.DrawString(FONT, "X", value_position, Color.Black, rotation, Vector2.Zero, scale, SpriteEffects.None, layer_depth - 0.05f);
            }
            else
            {
                spriteBatch.DrawString(FONT, value.ToString(), value_position, Color.Black, rotation, Vector2.Zero, scale, SpriteEffects.None, layer_depth - 0.05f);
            }
            base.Draw(spriteBatch);
        }
    }
}
