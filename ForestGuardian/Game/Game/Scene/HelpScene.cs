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
        private static int NUMBER_OF_ITEM_DISPLAY = 3;

        private static Vector2[] itemPosition;

        private Button backButton;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private ToggleButton backwardButton;
        private ToggleButton forwardButton;

        private int numberOfItems;
        private Texture2D[] itemTexture;

        private Texture2D hightlightSelectTexture;
        private SpriteFont helpFont;

        private int startItem;
        private int currentItemShow;

        public HelpScene()
            : base()
        {
            numberOfItems = 5;
            itemTexture = new Texture2D[numberOfItems];
            itemPosition = new Vector2[NUMBER_OF_ITEM_DISPLAY];

            itemPosition[0] = new Vector2(190, 467);
            itemPosition[1] = itemPosition[0] + new Vector2(230, 0);
            itemPosition[2] = itemPosition[1] + new Vector2(230, 0);

            startItem = currentItemShow = 0;
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

            hightlightSelectTexture = content.Load<Texture2D>(@"images\scene\HelpScene\highlight_select_card");
            helpFont = content.Load<SpriteFont>(@"fonts\HelpScene\helpscene");
        }

        public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);
            backwardButton.Update(gameTime);
            forwardButton.Update(gameTime);

            if (startItem == 0)
                backwardButton.Active = false;
            else
                backwardButton.Active = true;

            if (startItem == numberOfItems - NUMBER_OF_ITEM_DISPLAY)
                forwardButton.Active = false;
            else
                forwardButton.Active = true;

            for (int i = 0; i < 3; i++)
            {
                if (InputManager.IsMouseJustReleased() && InputManager.IsMouseHittedRectangle(new Rectangle(
                    (int)itemPosition[i].X, (int)itemPosition[i].Y,
                    itemTexture[startItem + i].Width, itemTexture[startItem + i].Height)))
                {
                    currentItemShow = i;
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            backButton.Draw(spriteBatch);
            backwardButton.Draw(spriteBatch);
            forwardButton.Draw(spriteBatch);

            for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
            {
                spriteBatch.Draw(itemTexture[startItem + i], itemPosition[i], Color.White);
            }

            spriteBatch.Draw(hightlightSelectTexture, itemPosition[currentItemShow], Color.White);
            spriteBatch.DrawString(helpFont, "con meo con. anh tuan anh", new Vector2(500, 100), Color.White);

            spriteBatch.End();
        }

        private void BackwardButtonClicked(object sender, EventArgs e)
        {
            if (startItem > 0)
                startItem--;
        }

        private void ForwardButtonClicked(object sender, EventArgs e)
        {
            if (startItem < numberOfItems - NUMBER_OF_ITEM_DISPLAY)
                startItem++;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
    }
}
