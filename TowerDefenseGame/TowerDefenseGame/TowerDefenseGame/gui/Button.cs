using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseGame.gui
{
    class Button : InteractiveSprite
    {
        private String name;

        public Button(Texture2D texture, Vector2 position, String name)
            : base(texture, position)
        {
            this.name = name;
        }

    }
}
