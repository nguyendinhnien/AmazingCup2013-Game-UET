using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Library; 

namespace CustomGame
{
    public class HUDLayer
    {
        private GamePlayScene gameplay;
        public SpriteFont FONT;
        private Label LifeLabel;
        private Label MoneyLabel;

        private Button playButton;
        private Button pauseButton;
        private Button settingButton;

        public HUDLayer(GamePlayScene gameplay)
        {
            this.gameplay = gameplay;
        }
        public void LoadContent()
        {
            Texture2D texture;
            texture = gameplay.SceneManager.Game.Content.Load<Texture2D>(@"");
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
