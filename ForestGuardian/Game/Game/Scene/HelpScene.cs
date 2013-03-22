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

namespace CustomGame
{
    public class HelpScene : GameScene
    {
        private Button backButton;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private ToggleButton backwardButton;
        private ToggleButton forwardButton;

        private int numberOfItems;
        private Texture2D[] itemTexture;

        private int currentItem;

        public HelpScene()
            : base()
        {
            numberOfItems = 5;
            itemTexture = new Texture2D[numberOfItems];

            currentItem = 0;
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\HelpScene\credit");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\HelpScene\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\HelpScene\b_back_clicked");
            backButton = new Button(texture, null, pressTexture, new Vector2(423, 690));
            backButton.Clicked += BackButtonClicked;

            Texture2D disableButton = content.Load<Texture2D>(@"images\scene\HelpScene\b_backward_disable");
            texture = content.Load<Texture2D>(@"images\scene\HelpScene\b_backward");
            pressTexture = content.Load<Texture2D>(@"images\scene\HelpScene\b_backward_clicked");
            backwardButton = new ToggleButton(texture, null, pressTexture, disableButton, new Vector2(50, 510));
            backwardButton.Clicked += BackwardButtonClicked;

            disableButton = content.Load<Texture2D>(@"images\scene\HelpScene\b_forward_disable");
            texture = content.Load<Texture2D>(@"images\scene\HelpScene\b_forward");
            pressTexture = content.Load<Texture2D>(@"images\scene\HelpScene\b_forward_clicked");
            forwardButton = new ToggleButton(texture, null, pressTexture, disableButton, new Vector2(900, 510));
            forwardButton.Clicked += ForwardButtonClicked;

            itemTexture[0] = content.Load<Texture2D>(@"images\scene\HelpScene\catus_card");
            itemTexture[1] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_card");
            itemTexture[2] = content.Load<Texture2D>(@"images\scene\HelpScene\pine_apple_card");
            itemTexture[3] = content.Load<Texture2D>(@"images\scene\HelpScene\pine_apple_card");
            itemTexture[4] = content.Load<Texture2D>(@"images\scene\HelpScene\pine_apple_card");
        }

        public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);
            backwardButton.Update(gameTime);
            forwardButton.Update(gameTime);

            if (currentItem == 0)
                backwardButton.Active = false;
            else
                backwardButton.Active = true;

            if (currentItem == numberOfItems - 3)
                forwardButton.Active = false;
            else
                forwardButton.Active = true;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            backButton.Draw(spriteBatch);
            backwardButton.Draw(spriteBatch);
            forwardButton.Draw(spriteBatch);

            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(itemTexture[currentItem + i], new Vector2(190, 467)
                    + i * new Vector2(230, 0), Color.White);
            }
            spriteBatch.End();
        }

        private void BackwardButtonClicked(object sender, EventArgs e)
        {
            if (currentItem > 0)
                currentItem--;
        }

        private void ForwardButtonClicked(object sender, EventArgs e)
        {
            if (currentItem < numberOfItems - 3)
                currentItem++;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
    }
}
