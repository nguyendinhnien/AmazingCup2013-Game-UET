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

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D[] towerLevelTexture = new Texture2D[3];
        private Texture2D[] towerInGameTexture;

        //private Vector2[] textPosition;
        private Texture2D lockTexture;
        private Texture2D hightlightSelectTexture;
        private SpriteFont helpFont;

        //Phan Item o duoi
        private int numberOfItems;
        private Texture2D[] itemCard;
        private Vector2[] itemPosition;

        private int startItem;
        private int currentItemShow;

        private Button backButton;
        private ToggleButton backwardButton;
        private ToggleButton forwardButton;

        private string TowerName;
        private int TowerDamage,TowerRange, TowerCost;

        public HelpScene():base()
        {
            numberOfItems = UserData.MAX_TOWER_NUMBER;
            towerInGameTexture = new Texture2D[numberOfItems];
            
            itemCard = new Texture2D[numberOfItems];
            itemPosition = new Vector2[NUMBER_OF_ITEM_DISPLAY];
            
            //Vi tri cac the
            for (int i = 0; i < itemPosition.Length; i++)
            {
                itemPosition[i] = new Vector2(190, 467) +i * new Vector2(230, 0);
            }

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

            itemCard[0] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_card");
            itemCard[1] = content.Load<Texture2D>(@"images\scene\HelpScene\cactus_card");
            itemCard[2] = content.Load<Texture2D>(@"images\scene\HelpScene\pineapple_card");

            towerInGameTexture[0] = content.Load<Texture2D>(@"images\scene\HelpScene\oak_in_game");
            towerInGameTexture[1] = content.Load<Texture2D>(@"images\scene\HelpScene\cactus_in_game");
            towerInGameTexture[2] = content.Load<Texture2D>(@"images\scene\HelpScene\pineapple_in_game");

            TowerLoadManager.LoadContent(content);

            hightlightSelectTexture = content.Load<Texture2D>(@"images\scene\CommonButton\highlight_select_card");
            lockTexture = content.Load<Texture2D>(@"images\scene\CommonButton\lock_card");
            helpFont = content.Load<SpriteFont>(@"fonts\HelpScene\helpScene");

            towerLevelTexture[0] = OakTower.TEXTURE_LV1;
            towerLevelTexture[1] = OakTower.TEXTURE_LV2;
            towerLevelTexture[2] = OakTower.TEXTURE_LV3;
        }

        public override void Update(GameTime gameTime)
        {
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
                    (int)itemPosition[i].X, (int)itemPosition[i].Y, itemCard[0].Width, itemCard[0].Height)))
                {
                    currentItemShow = i; break;
                }
            }
            switch (currentItemShow)
            {
                case (int)TowerType.Oak:
                    TowerName = OakTower.NAME; TowerDamage = OakTower.DAMAGE; TowerRange = OakTower.RANGE; TowerCost = OakTower.COST;
                    towerLevelTexture[0] = OakTower.TEXTURE_LV1;
                    towerLevelTexture[1] = OakTower.TEXTURE_LV2;
                    towerLevelTexture[2] = OakTower.TEXTURE_LV3;
                    break;
                case (int)TowerType.Cactus:
                    TowerName = CactusTower.NAME; TowerDamage = CactusTower.DAMAGE; TowerRange = CactusTower.RANGE; TowerCost = CactusTower.COST;
                    towerLevelTexture[0] = CactusTower.TEXTURE_LV1;
                    towerLevelTexture[1] = CactusTower.TEXTURE_LV2;
                    towerLevelTexture[2] = CactusTower.TEXTURE_LV3;
                    break;
                case (int)TowerType.Pineapple:
                    TowerName = PineappleTower.NAME; TowerDamage = PineappleTower.DAMAGE; TowerRange = PineappleTower.RANGE; TowerCost = PineappleTower.COST;
                    towerLevelTexture[0] = PineappleTower.TEXTURE_LV1;
                    towerLevelTexture[1] = PineappleTower.TEXTURE_LV2;
                    towerLevelTexture[2] = PineappleTower.TEXTURE_LV3;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

                spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);

                spriteBatch.DrawString(helpFont, "Infomation", new Vector2(100,120), Color.Blue);
                spriteBatch.DrawString(helpFont, "In Game", new Vector2(430,120), Color.Blue);
                spriteBatch.DrawString(helpFont, "Upgrade", new Vector2(732,120), Color.Blue);

                for (int i = 0; i < NUMBER_OF_ITEM_DISPLAY; i++)
                {
                    spriteBatch.Draw(itemCard[startItem + i], itemPosition[i], Color.White);
                    if (UserData.isTowerLock(startItem + i))
                    {
                        spriteBatch.Draw(lockTexture, itemPosition[i], Color.White);
                    }
                }

                int towerIndex = currentItemShow + startItem;
                //Highlight phan duoc select
                spriteBatch.Draw(hightlightSelectTexture, itemPosition[towerIndex] - new Vector2(2, 2), Color.White);
                if (! UserData.isTowerLock(towerIndex))
                {
                    string TowerInfo = "Name: " + TowerName + "\n" +
                                        "Damage: " + TowerDamage + "/" + (int)(TowerDamage * 1.5f) + "/" + (int)(TowerDamage * 1.8f) + "\n" +
                                        "Range: " + TowerRange + "\n" + "Cost: " + TowerCost;

                    spriteBatch.DrawString(helpFont, TowerInfo, new Vector2(75,190) , Color.Black, 0, Vector2.Zero,0.5f, SpriteEffects.None, 1.0f);
                    
                    spriteBatch.Draw(towerLevelTexture[0], new Vector2(700, 315), Color.White);
                    spriteBatch.Draw(towerLevelTexture[1], new Vector2(780, 240), Color.White);
                    spriteBatch.Draw(towerLevelTexture[2], new Vector2(860, 165), Color.White);
                    spriteBatch.Draw(towerInGameTexture[towerIndex], new Vector2(400, 200), Color.White);

                    spriteBatch.DrawString(helpFont, "level 1", new Vector2(780, 347), Color.Black, 0,
                        Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(helpFont, "level 2", new Vector2(700, 272), Color.Black, 0,
                        Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(helpFont, "level 3", new Vector2(780, 197), Color.Black, 0,
                        Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                }

                backButton.Draw(spriteBatch);
                backwardButton.Draw(spriteBatch);
                forwardButton.Draw(spriteBatch);

            spriteBatch.End();
        }

        private void BackwardButtonClicked(object sender, EventArgs e)
        {
            if (startItem > 0)
            {
                startItem--;
            }
        }

        private void ForwardButtonClicked(object sender, EventArgs e)
        {
            if (startItem < numberOfItems - NUMBER_OF_ITEM_DISPLAY)
            {
                startItem++;
            }
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            this.ExitScene();
        }
    }
}