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
    public class SelectLevelScene : GameScene
    {
        private static int NUMBER_OF_ITEM_DISPLAY = 3;
        //private static int MAX_DEFAULT_MAPS = 3;

        private Texture2D selectLevelTexture;

        private ToggleButton backwardButton;
        private ToggleButton forwardButton;

        private float PreviewScale;
        //private Texture2D[] mapPreviewTexture;

        private int numberOfItems;
        //private Texture2D[] itemTexture;
        private Vector2[] itemPosition;

        private Texture2D hightlightSelectTexture;
        private SpriteFont selectLevelFont;

        private int startItem;
        private int currentItemShow;

        private Button backButton;
        private Button startButton;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        public SelectLevelScene()
            : base()
        {
            numberOfItems = MapLoadManager.MAX_DEFAULT_MAPS;
            //itemTexture = new Texture2D[numberOfItems];
            itemPosition = new Vector2[NUMBER_OF_ITEM_DISPLAY];
            //Map preview
            //mapPreviewTexture = new Texture2D[numberOfItems];

            for (int i = 0; i < itemPosition.Length; i++)
            {
                itemPosition[i] = new Vector2(190, 462) + i * new Vector2(230, 0);
            }

            startItem = currentItemShow = 0;
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"images\scene\SelectLevelScene\selectmaps");
            backgroundPosition = new Vector2(0, 0);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\CommonButton\b_back_clicked");
            backButton = new Button(texture, null, pressTexture, new Vector2(627, 685));
            backButton.Clicked += BackButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\SelectLevelScene\b_start_game");
            pressTexture = content.Load<Texture2D>(@"images\scene\SelectLevelScene\b_start_game_clicked");
            startButton = new Button(texture, null, pressTexture, new Vector2(180, 685));
            startButton.Clicked += StartButtonClicked;

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

            PreviewScale = Math.Min(368/(float) MapLoadManager.getMap(0).BackgroundTexture.Width, 
                276/(float)MapLoadManager.getMap(0).BackgroundTexture.Height);

            hightlightSelectTexture = content.Load<Texture2D>(@"images\scene\CommonButton\highlight_select_card");
            selectLevelFont = content.Load<SpriteFont>(@"fonts\SelectLevelScene\select_level");

            selectLevelTexture = content.Load<Texture2D>(@"images\scene\SelectLevelScene\b_select_level");
        }

        public override void Update(GameTime gameTime)
        {
            startButton.Update(gameTime);
            backButton.Update(gameTime);
            backwardButton.Update(gameTime);
            forwardButton.Update(gameTime);

            if (startItem == 0) { backwardButton.Active = false; }
            else { backwardButton.Active = true; }

            if (startItem == numberOfItems - NUMBER_OF_ITEM_DISPLAY) { forwardButton.Active = false; }
            else { forwardButton.Active = true; }

            for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
            {
                if (InputManager.IsMouseJustReleased() && InputManager.IsMouseHittedRectangle(new Rectangle(
                    (int)itemPosition[i].X, (int)itemPosition[i].Y,
                    MapLoadManager.getMapThumbnail(startItem + i).Width, MapLoadManager.getMapThumbnail(startItem + i).Height)))
                {
                    currentItemShow = i;
                    UserData.mapIndex = i;
                    PreviewScale = Math.Min(368 / (float)MapLoadManager.getMap(i).BackgroundTexture.Width,
                                            276 / (float)MapLoadManager.getMap(i).BackgroundTexture.Height);
                    break;
                }
            }

            for (int i = 0; i < 3; i++){
                if (InputManager.IsMouseJustReleased() && InputManager.IsMouseHittedRectangle(new Rectangle(
                    458, 167 + 40 * i, 101, 40)))
                {
                    UserData.mode = i;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            startButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
            backwardButton.Draw(spriteBatch);
            forwardButton.Draw(spriteBatch);

            for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
            {
                spriteBatch.Draw(MapLoadManager.getMapThumbnail(startItem + i), itemPosition[i], Color.White);
            }

            
            spriteBatch.Draw(hightlightSelectTexture, itemPosition[currentItemShow]- new Vector2(2,2), Color.White);
            spriteBatch.Draw(MapLoadManager.getMap(currentItemShow).BackgroundTexture, new Vector2(44, 107), null, Color.White, 0.0f, Vector2.Zero,PreviewScale,SpriteEffects.None,0.0f);
            spriteBatch.Draw(selectLevelTexture, new Vector2(456, 168) + new Vector2(0, 43) * UserData.mode, Color.White);

            Vector2 mapNamePos = new Vector2(780, 120);
            mapNamePos.X -= selectLevelFont.MeasureString(MapLoadManager.getMap(currentItemShow).Name).X / 2;

            spriteBatch.DrawString(selectLevelFont, MapLoadManager.getMap(currentItemShow).Name, mapNamePos, Color.Green);

            spriteBatch.DrawString(selectLevelFont, MapLoadManager.getMap(currentItemShow).Description, new Vector2(629, 167), Color.Black, 0, Vector2.Zero,0.8f, SpriteEffects.None, 1.0f);

            spriteBatch.End();
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            SceneManager.AddScene(GamePlayScene.Instance);
            this.ExitScene();
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
            this.ExitScene();
        }
    }
}