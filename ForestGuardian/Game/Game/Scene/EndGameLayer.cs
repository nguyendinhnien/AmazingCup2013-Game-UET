using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
namespace CustomGame
{
    public class EndGameLayer
    {
        protected GamePlayScene gameplay;
        protected Label MissionLabel;
        protected Button ReplayButton;
        protected Button ContinueButton;

        public EndGameLayer(GamePlayScene gameplay)
        {
            this.gameplay = gameplay;
        }
        public virtual void LoadContent()
        {
            ContentManager Content = gameplay.SceneManager.Game.Content;
            Texture2D normalTexture,pressTexture;

            normalTexture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\mission_finished");
            MissionLabel = new Label();
            MissionLabel.Texture = normalTexture;
            MissionLabel.Center = new Vector2(512, 384);
            
            normalTexture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\replay_normal_but");
            pressTexture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\replay_press_but");
            ReplayButton = new Button(normalTexture, null, pressTexture, new Vector2(200, 100));
            ReplayButton.Clicked += ReplayButton_Clicked;

            normalTexture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\continue_normal_but");
            pressTexture = Content.Load<Texture2D>(@"images\gameplay\EndGameLayer\continue_press_but");
            ContinueButton = new Button(normalTexture, null, pressTexture, new Vector2(100, 300));
            ContinueButton.Clicked += ContinueButton_Clicked;
        }

        private void ReplayButton_Clicked(object sender, EventArgs e)
        {
            
        }
        private void ContinueButton_Clicked(object sender, EventArgs e)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            ReplayButton.Update(gameTime);
            ContinueButton.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            MissionLabel.Draw(spriteBatch);
            ReplayButton.Draw(spriteBatch);
            ContinueButton.Draw(spriteBatch);
        }
    }   
}
