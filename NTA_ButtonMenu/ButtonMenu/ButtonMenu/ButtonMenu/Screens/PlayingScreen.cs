using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ButtonMenu
{
    class PlayingScreen : GameScreen
    {
        #region Data
        private const int NUMBER_OF_BUTTONS = 3;
        private const int NUMBER_OF_TOWERS = 3;

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D moneyTexture;
        private Vector2 moneyPosition;

        private Texture2D healthTexture;
        private Vector2 healthPosition;

        private Texture2D[] buttonTextures;
        private Texture2D[] buttonSelected;
        private Vector2[] buttonPositions;
        private Rectangle[] buttonRectangles;
        private bool[] isButtonSelected;

        private Texture2D[] towerEnable;
        private Texture2D[] towerDisabled;
        private Vector2[] towerPositions;
        private Rectangle[] towerRectangles;
        private bool[] isTowerEnable;

        private bool isMute;
        private bool isPlaying;

        int money = 10;
        private SpriteFont font1;

        #endregion

        public PlayingScreen()
            : base()
        {
            buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
            buttonSelected = new Texture2D[NUMBER_OF_BUTTONS];
            buttonPositions = new Vector2[NUMBER_OF_BUTTONS];
            buttonRectangles = new Rectangle[NUMBER_OF_BUTTONS];
            isButtonSelected = new bool[NUMBER_OF_BUTTONS];

            towerEnable = new Texture2D[NUMBER_OF_TOWERS];
            towerDisabled = new Texture2D[NUMBER_OF_TOWERS];
            towerPositions = new Vector2[NUMBER_OF_TOWERS];
            towerRectangles = new Rectangle[NUMBER_OF_TOWERS];
            isTowerEnable = new bool[NUMBER_OF_TOWERS];

            isMute = false;
            isPlaying = true;
        }


        public override void LoadContent()
        {
            base.LoadContent();

            ContentManager content = ScreenManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images/PlayingScreen/map");
            moneyTexture = content.Load<Texture2D>(@"images/PlayingScreen/hud_money");
            healthTexture = content.Load<Texture2D>(@"images/PlayingScreen/hud_health");

            font1 = content.Load<SpriteFont>(@"Fonts/font1");

            buttonTextures[0] = content.Load<Texture2D>(@"images/PlayingScreen/hud_play");
            buttonTextures[1] = content.Load<Texture2D>(@"images/PlayingScreen/hud_configure");
            buttonTextures[2] = content.Load<Texture2D>(@"images/PlayingScreen/hud_audio_on");

            buttonSelected[0] = content.Load<Texture2D>(@"images/PlayingScreen/hud_pause");
            buttonSelected[1] = content.Load<Texture2D>(@"images/PlayingScreen/hud_configure");
            buttonSelected[2] = content.Load<Texture2D>(@"images/PlayingScreen/hud_audio_off");

            towerEnable[0] = content.Load<Texture2D>(@"images/PlayingScreen/icon_gatling_tower");
            towerEnable[1] = content.Load<Texture2D>(@"images/PlayingScreen/icon_goo_tower");
            towerEnable[2] = content.Load<Texture2D>(@"images/PlayingScreen/icon_ice_tower");

            towerDisabled[0] = content.Load<Texture2D>(@"images/PlayingScreen/icon_gatling_tower_disabled");
            towerDisabled[1] = content.Load<Texture2D>(@"images/PlayingScreen/icon_goo_tower_disabled");
            towerDisabled[2] = content.Load<Texture2D>(@"images/PlayingScreen/icon_ice_tower_disabled");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);

            moneyPosition = new Vector2(30, 30);
            healthPosition = new Vector2(900, 30);

            buttonPositions[0] = new Vector2(30, 600);
            buttonRectangles[0] = new Rectangle(30, 600, buttonTextures[0].Width, buttonTextures[0].Height);

            buttonPositions[1] = new Vector2(buttonRectangles[0].Right + 30, buttonRectangles[0].Top);
            buttonRectangles[1] = new Rectangle(buttonRectangles[0].Right + 30, buttonRectangles[0].Top, buttonTextures[1].Width, buttonTextures[1].Height);

            buttonPositions[2] = new Vector2(buttonRectangles[1].Right + 30, buttonRectangles[1].Top);
            buttonRectangles[2] = new Rectangle(buttonRectangles[1].Right + 30, buttonRectangles[1].Top, buttonTextures[2].Width, buttonTextures[2].Height);

            towerPositions[0] = new Vector2(600, 700);
            towerRectangles[0] = new Rectangle(600, 700, towerEnable[0].Width, towerEnable[0].Height);

            towerPositions[1] = new Vector2(towerRectangles[0].Right + 10, towerRectangles[0].Top);
            towerRectangles[1] = new Rectangle(towerRectangles[0].Right + 10, towerRectangles[0].Top, towerEnable[1].Width, towerEnable[1].Height);

            towerPositions[2] = new Vector2(towerRectangles[1].Right + 10, towerRectangles[1].Top);
            towerRectangles[2] = new Rectangle(towerRectangles[1].Right + 10, towerRectangles[1].Top, towerEnable[2].Width, towerEnable[2].Height);

        }


        public override void Draw(GameTime gameTime)
        {
            //if (!isPlaying)
              //  return;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            spriteBatch.Draw(moneyTexture, moneyPosition, Color.White);
            money++;
            spriteBatch.DrawString(font1, money.ToString(), moneyPosition + new Vector2(80,20), Color.Orange);
            spriteBatch.Draw(healthTexture, healthPosition, Color.White);

            spriteBatch.Draw(buttonTextures[1], buttonPositions[1], Color.White);
            if (isMute)
                spriteBatch.Draw(buttonSelected[2], buttonPositions[2], Color.White);
            else
                spriteBatch.Draw(buttonTextures[2], buttonPositions[2], Color.White);

            if (isPlaying)
                spriteBatch.Draw(buttonTextures[0], buttonPositions[0], Color.White);
            else
                spriteBatch.Draw(buttonSelected[0], buttonPositions[0], Color.White);

            for (int i = 0; i < NUMBER_OF_TOWERS; i++)
            {
                if (isTowerEnable[i])
                    spriteBatch.Draw(towerEnable[i], towerPositions[i], Color.White);
                else
                    spriteBatch.Draw(towerDisabled[i], towerPositions[i], Color.White);
            }

            spriteBatch.End();
        }


        public override void HandleInput()
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
                if (InputManager.IsMouseHittedRectangle(buttonRectangles[i]))
                {
                    if (InputManager.IsMouseDown())
                    {
                        //mouse is currently down
                        isButtonSelected[i] = true;

                    }
                    else if (InputManager.IsMouseJustReleased())
                    {
                        //mouse is just released, do something and continue
                        TakeActionOnButton(i);
                    }
                }
                else
                {
                    //do something
                    isButtonSelected[i] = false;
                }

            for (int i = 0; i < NUMBER_OF_TOWERS; i++)
                if (InputManager.IsMouseHittedRectangle(towerRectangles[i]))
                {
                    if (InputManager.IsMouseDown())
                    {
                        //mouse is currently down
                        isTowerEnable[i] = true;

                    }
                    else if (InputManager.IsMouseJustReleased())
                    {
                        //mouse is just released, do something and continue
                        TakeActionOnTower(i);
                    }
                }
                else
                {
                    //do something
                    isTowerEnable[i] = false;
                }
        }
        

        void TakeActionOnButton(int i)
        {
            switch (i)
            {
                case 0:
                    ActionOnPauseButton();
                    break;
                case 1:
                    ActionOnConfigureButton();
                    break;
                case 2:
                    ActionOnMuteButton();
                    break;
                default:
                    break;
            }
        }

        void ActionOnPauseButton()
        {
            isPlaying = !isPlaying;
        }

        void ActionOnConfigureButton()
        {
            String message = "Are you sure want to quit?";
            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);
            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmExitMessageBox);
        }

        void ConfirmExitMessageBoxAccepted(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        void ActionOnMuteButton()
        {
            isMute = !isMute;
        }

        void TakeActionOnTower(int i)
        {
            switch (i)
            {
                case 0:
                    ActionOnTower0();
                    break;
                case 1:
                    ActionOnTower1();
                    break;
                case 2:
                    ActionOnTower2();
                    break;
                default:
                    break;
            }
        }

        void ActionOnTower0()
        {
        }

        void ActionOnTower1()
        {
        }

        void ActionOnTower2()
        {
        }
    }
}
