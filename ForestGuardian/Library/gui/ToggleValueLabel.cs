using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class ToggleValueLabel: ValueLabel
    {
        private Texture2D textureEnable;
        private Texture2D textureDisable;
        private bool enable = true;

        public ToggleValueLabel(Texture2D textureEnable, Texture2D textureDisable, Vector2 position, Vector2 value_offset)
            : base(textureEnable, position,value_offset) 
        {
            this.textureEnable = textureEnable;
            this.textureDisable = textureDisable;
        }
        
        public ToggleValueLabel(Texture2D textureEnable, Texture2D textureDisable, Vector2 position, Vector2 value_offset, int value)
            :base(textureEnable,position,value_offset,value)
        {
            this.textureEnable = textureEnable;
            this.textureDisable = textureDisable;
        }

        public bool Active
        {
            get { return enable; }
            set { enable = value; }
        }

        public void Update(int current_value)
        {
            if ((!max) && value <= current_value) {
                enable = true; texture = textureEnable;
            }
            else { enable = false; texture = textureDisable;}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {           
            base.Draw(spriteBatch);
        }
    }
}
