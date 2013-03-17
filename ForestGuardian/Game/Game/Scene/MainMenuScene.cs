using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Data;
using Library;

namespace Forest
{
    public class MainMenuScene : GameScreen
    {
        private int NUMBER_OF_BUTTONS = 5;
        private Button[] button;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;


        public MainMenuScene() : base()
        {
            button = new Button[NUMBER_OF_BUTTONS];
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\main");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_play");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_play_clicked");
            button[0] = new Button(texture, null, pressTexture, new Vector2(10, 670));

            texture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_scores");
            pressTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_scores_clicked");
            button[1] = new Button(texture, null, pressTexture, new Vector2(190, 670));

            texture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_options");
            pressTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_options_clicked");
            button[2] = new Button(texture, null, pressTexture, new Vector2(425, 670));

            texture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_help");
            pressTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_help_clicked");
            button[3] = new Button(texture, null, pressTexture, new Vector2(660, 670));

            texture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_quit");
            pressTexture = content.Load<Texture2D>(@"images\scene\MainMenuScene\b_quit_clicked");
            button[4] = new Button(texture, null, pressTexture, new Vector2(880, 670));

            button[0].Clicked += PlayButtonClicked;
            button[1].Clicked += ScoresButtonClicked;
            button[2].Clicked += OptionsButtonClicked;
            button[3].Clicked += HelpButtonClicked;
            button[4].Clicked += QuitButtonClicked;
        }


        public override void  HandleInput(GameTime gameTime)
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                button[i].Update(gameTime);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
                button[i].Draw(spriteBatch);


                spriteBatch.End();
        }


        private void PlayButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new SelectLevelScene());     
        }

        private void ScoresButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new ScoreScene());
        }

        private void OptionsButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new OptionScene());
        }

        private void HelpButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new HelpScene());
        }

        private void QuitButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
