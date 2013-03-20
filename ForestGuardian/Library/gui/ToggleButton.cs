using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Library
{
    public class ToggleButton : Button
    {
        private bool enable;

        private Texture2D disableTexture;

        public ToggleButton(Texture2D normalTexture, Texture2D hoverTexture, Texture2D pressTexture, Texture2D disableTexture, Vector2 position)
            : base(normalTexture, hoverTexture, pressTexture, position)
        {
            this.disableTexture = disableTexture;
        }

        public bool Active
        {
            get { return enable; }
            set { enable = value;
            if (!enable) texture = disableTexture;
            }
        }
    }
}
