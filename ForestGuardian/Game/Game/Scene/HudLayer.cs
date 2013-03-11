using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;

namespace Game
{
    enum GamePlayButtons
    {
        //PlayBut,
        //PauseBut,
        //QuitBut,
        ArrowTowerBut,
        //SlowTowerBut,
        //CanonTowerBut
    }
    public class HudLayer
    {
        private GamePlayScene game_play;
        private List<Button> buttons;
        private SpriteFont font;

        private Texture2D cursorTexture = null;
        private Texture2D ArrowTowerCursorTexture;
        private Texture2D SlowTowerCursorTexture;
        private Texture2D CanonTowerCursorTexture;

        public HudLayer(GamePlayScene game_play)
        {
            this.game_play = game_play;
            buttons = new List<Button>();
            LoadContent();
        }

        public void LoadContent()
        {
            Button button;
            Texture2D texture, hoverTexture, pressTexture;
            ContentManager Content = game_play.Content;
            
            ////Play button
            //texture = Content.Load<Texture2D>("");
            //hoverTexture = Content.Load<Texture2D>("");
            //pressTexture = Content.Load<Texture2D>("");
            //button = new Button(texture, hoverTexture, pressTexture, new Vector2(0, 0));
            //button.Clicked += PlayBut_Clicked;

            ////Pause button
            //texture = Content.Load<Texture2D>("");
            //hoverTexture = Content.Load<Texture2D>("");
            //pressTexture = Content.Load<Texture2D>("");
            //button = new Button(texture, hoverTexture, pressTexture, new Vector2(0, 0));
            //button.Clicked += PlayBut_Clicked;

            ////Quit button
            //texture = Content.Load<Texture2D>("");
            //hoverTexture = Content.Load<Texture2D>("");
            //pressTexture = Content.Load<Texture2D>("");
            //button = new Button(texture, hoverTexture, pressTexture, new Vector2(0, 0));
            //button.Clicked += PlayBut_Clicked;

            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\arrow_tower_but");
            hoverTexture = null;
            pressTexture = null;
            button = new Button(texture, hoverTexture, pressTexture, new Vector2(100, 100));
            button.Clicked += ArrowTowerBut_Clicked;
            buttons.Add(button);
        }

        private void PlayBut_Clicked(object sender, EventArgs e)
        {
            
        }
        private void PauseBut_Clicked(object sender, EventArgs e)
        {

        }
        private void QuitBut_Clicked(object sender, EventArgs e)
        {

        }
        private void ArrowTowerBut_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Kien dep trai");
            game_play.tower_put = TowerPut.ARROW_TOWER;
            cursorTexture = ArrowTowerCursorTexture;
        }
        private void SlowTowerBut_Clicked(object sender, EventArgs e)
        {

        }
        private void CanonTowerBut_Clicked(object sender, EventArgs e)
        {

        }
        public void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, game_play.Money.ToString(), Vector2.Zero, Color.White);
            //spriteBatch.DrawString(font, game_play.Points.ToString(), Vector2.Zero, Color.White);
            //spriteBatch.DrawString(font, game_play.Lives.ToString(), Vector2.Zero, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        private void DrawCursor(SpriteBatch spriteBatch)
        {
        }
    }
}
