﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Data;
using Library;

namespace CustomGame
{
    public class ScoreScene : GameScene
    {
        private Button button;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private SpriteFont scoreFont;
        private Vector2[] scorePosition;

        public ScoreScene()
            : base()
        {
            scorePosition = new Vector2[8];
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\ScoreScene\scoreboard");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back_clicked");
            button = new Button(texture, null, pressTexture, new Vector2(403, 685));
            button.Clicked += BackButtonClicked;

            scoreFont = content.Load<SpriteFont>(@"fonts\ScoreScene\score");
            for (int i = 0; i < 8; i++)
            {
                scorePosition[i] = new Vector2(260, 115) + i * (new Vector2(0, 70));
            }
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScene();
        }

        public override void Update(GameTime gameTime)
        {
            button.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            button.Draw(spriteBatch);

            for (int i = 0; i < UserData.highscore.Count; i++)
            {
                spriteBatch.DrawString(scoreFont, UserData.highscore.scores[i].player_name, scorePosition[i],
                    UserData.colors[i]);
                spriteBatch.DrawString(scoreFont, UserData.highscore.scores[i].points.ToString(), scorePosition[i]
                    + new Vector2(500, 0), UserData.colors[i]);
            }

            spriteBatch.End();
        }
    }
}
