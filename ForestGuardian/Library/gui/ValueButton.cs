using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class ValueButton:Button
    {
        //private bool visible = false;
        private bool enable = false;
        private int value;
        SpriteFont font;


        public ValueButton(Texture2D texture, Texture2D hoverTexture, Texture2D pressTexture, Vector2 position)
            : base(texture, hoverTexture, pressTexture, position)
        {
            //this.value = value;
            //this.font = font;
            layer_depth = 0.2f;
        }

        //public bool Visible{
        //    get { return visible; }
        //    set { visible = value; }
        //}

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        public override void Update(GameTime gameTime)
        {
            if (enable)
            {
                base.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(visible){
                //spriteBatch.DrawString(font, value.ToString(), position, Color.White);
                base.Draw(spriteBatch);
            //}
        }
    }
}
