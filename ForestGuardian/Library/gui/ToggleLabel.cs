using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class ToggleLabel : Label
    {
        private Texture2D textureEnable;
        private Texture2D textureDisable;
        private bool enable = true;

        public ToggleLabel(Texture2D textureEnable, Texture2D textureDisable, Vector2 position)
            : base(textureEnable, position) 
        {
            this.textureEnable = textureEnable;
            this.textureDisable = textureDisable;
        }

        public bool Active
        {
            get { return enable; }
            set { enable = value;
                if (enable) { texture = textureEnable; }
                else { texture = textureDisable; }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
