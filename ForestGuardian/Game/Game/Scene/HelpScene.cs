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
        private int NUMBER_OF_ITEM_DISPLAY = 3;

        private Texture2D[][] towerTexture;
        private Texture2D[] towerInGameTexture;

        private Vector2[] textPosition;

        private Vector2[] itemPosition;

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
            numberOfItems = 3;

            towerInGameTexture = new Texture2D[numberOfItems];
            towerTexture = new Texture2D[numberOfItems][];
            for (int i = 0; i < numberOfItems; i++)
                towerTexture[i] = new Texture2D[3];

            itemTexture = new Texture2D[numberOfItems];
            itemPosition = new Vector2[NUMBER_OF_ITEM_DISPLAY];

            itemPosition[0] = new Vector2(190, 467);
            itemPosition[1] = itemPosition[0] + new Vector2(230, 0);
            itemPosition[2] = itemPosition[1] + new Vector2(230, 0);

            textPosition = new Vector2[10];
            textPosition[0] = new Vector2(160, 120); //Infor text
            textPosition[1] = new Vector2(430, 120); //Ingame text
            textPosition[2] = new Vector2(732, 120); //Upgrade text

            textPosition[3] = new Vector2(75, 190); //History
            textPosition[4] = new Vector2(100, 200); //Damage
            textPosition[5] = new Vector2(75, 343); //Range
            textPosition[6] = new Vector2(160, 500); //Cost
            textPosition[7] = new Vector2(160, 600); //Slow, Splash damage
            textPosition[9] = new Vector2(160, 700); //Slash range
            startItem = currentItemShow = 0;
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\HelpScene\help");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back_clicked");
            backButton = new Button(texture, null, pressTexture, new Vector2(403, 685));
            backButton.Clicked += BackButtonClicked;

            Texture2D disableButton = content.Load<Texture2D>(@"images\scene\CommonButton\b_backward_disable");
            texture = content.Load<Texture2D>(@"images\scene\CommonButton\b_backward");
            pressTexture = content.Load<Texture2D>(@"images\scene\CommonButton\b_backward_clicked");
            backwardButton = new ToggleButton(texture, null, pressTexture, disableButton, new Vector2(50, 510));
            backwardButton.Clicked += BackwardButtonClicked;

            disableButton = content.Load<Texture2D>(@"images\scene\CommonButton\b_forward_disable");
            texture = content.Load<Texture2D>(@"images\scene\CommonButton\b_forward");
            pressTexture = content.Load<Texture2D>(@"images\scene\CommonButton\b_forward_clicked");
            forwardButton = new ToggleButton(texture, null, pressTexture, disableButton, new Vector2(900, 510));
            forwardButton.Clicked += ForwardButtonClicked;

            itemTexture[0] = content.Load<Texture2D>(@"images\scene\HelpScene\catus_card");
            itemTexture[1] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_card");
            itemTexture[2] = content.Load<Texture2D>(@"images\scene\HelpScene\pine_apple_card");

            hightlightSelectTexture = content.Load<Texture2D>(@"images\scene\CommonButton\highlight_select_card");
            helpFont = content.Load<SpriteFont>(@"fonts\HelpScene\helpScene");

            towerTexture[0][0] = content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level1");
            towerTexture[0][1] = content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level2");
            towerTexture[0][2] = content.Load<Texture2D>(@"images\gameplay\towers\oak_tower_level3");

            towerTexture[1][0] = content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level1");
            towerTexture[1][1] = content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level2");
            towerTexture[1][2] = content.Load<Texture2D>(@"images\gameplay\towers\cactus_tower_level3");

            towerTexture[2][0] = content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level1");
            towerTexture[2][1] = content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level2");
            towerTexture[2][2] = content.Load<Texture2D>(@"images\gameplay\towers\pineapple_tower_level3");

            towerInGameTexture[0] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_in_game");
            towerInGameTexture[1] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_in_game");
            towerInGameTexture[2] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_in_game");
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

            for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
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

            
            spriteBatch.DrawString(helpFont, "Info", textPosition[0], Color.Black);
            spriteBatch.DrawString(helpFont, "In Game", textPosition[1], Color.Black);
            spriteBatch.DrawString(helpFont, "Upgrade", textPosition[2], Color.Black);

            spriteBatch.DrawString(helpFont, UserData.tower[currentItemShow], textPosition[3], Color.Black, 0, Vector2.Zero,
                .5f, SpriteEffects.None, 1.0f);
            
            backButton.Draw(spriteBatch);
            backwardButton.Draw(spriteBatch);
            forwardButton.Draw(spriteBatch);

            for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
            {
                spriteBatch.Draw(itemTexture[startItem + i], itemPosition[i], Color.White);
            }

            spriteBatch.Draw(hightlightSelectTexture, itemPosition[currentItemShow], Color.White);

            spriteBatch.Draw(towerTexture[currentItemShow][0], new Vector2(700, 315), Color.White);
            spriteBatch.Draw(towerTexture[currentItemShow][1], new Vector2(780, 240), Color.White);
            spriteBatch.Draw(towerTexture[currentItemShow][2], new Vector2(860, 165), Color.White);


            spriteBatch.DrawString(helpFont, "level 1", new Vector2(780, 347), Color.Black, 0, Vector2.Zero,
                .5f, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(helpFont, "level 2", new Vector2(700, 272), Color.Black, 0, Vector2.Zero,
                .5f, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(helpFont, "level 3", new Vector2(780, 197), Color.Black, 0, Vector2.Zero,
                .5f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(towerInGameTexture[currentItemShow], new Vector2(400, 200), Color.White);

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