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
                if (!enable) { texture = disableTexture; }
                else { texture = normalTexture; }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (enable)
            {
                switch (state)
                {
                    case ButtonStatus.Pressing:
                        if (pressTexture != null) { texture = pressTexture; }
                        break;
                    case ButtonStatus.Hovering:
                        if (hoverTexture != null) { texture = hoverTexture; }
                        break;
                    case ButtonStatus.Normal:
                        if (enable && normalTexture != null) { texture = normalTexture; }
                        else { texture = disableTexture; }
                        break;
                }
            }
            else
                texture = disableTexture;
            spriteBatch.Draw(texture, this.position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, layer_depth);
        }
    }
}
