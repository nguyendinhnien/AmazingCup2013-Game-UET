using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ButtonMenu
{
    class NewGameScreen : GameScreen
    {
        private const int NUMBER_OF_BUTTONS = 2;

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D[] buttonSelected;
        private Vector2[] buttonPositions;
        private Rectangle[] buttonRectangles;

        private bool[] isButtonSelected;

        public NewGameScreen()
            : base()
        {
            buttonSelected = new Texture2D[NUMBER_OF_BUTTONS];
            buttonPositions = new Vector2[NUMBER_OF_BUTTONS];
            buttonRectangles = new Rectangle[NUMBER_OF_BUTTONS];
            isButtonSelected = new bool[NUMBER_OF_BUTTONS];
        }


        public override void LoadContent()
        {
            base.LoadContent();

            ContentManager content = ScreenManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images/NewGameScreen/NewGameScreen");
            buttonSelected[0] = content.Load<Texture2D>(@"images/NewGameScreen/StartButtonSelected");
            buttonSelected[1] = content.Load<Texture2D>(@"images/NewGameScreen/ExitButtonSelected");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);

            buttonPositions[0] = new Vector2(400, 650);
            buttonRectangles[0] = new Rectangle(400, 650, buttonSelected[0].Width, buttonSelected[0].Height);

            buttonPositions[1] = new Vector2(950, 30);
            buttonRectangles[1] = new Rectangle(950, 30, buttonSelected[1].Width, buttonSelected[1].Height);
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                if (isButtonSelected[i])
                {
                    spriteBatch.Draw(buttonSelected[i], buttonPositions[i], Color.White);
                }
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
                        TakeActionOnImage(i);
                    }
                }
                else
                {
                    //do something
                    isButtonSelected[i] = false;
                }
        }


        void TakeActionOnImage(int i)
        {
            switch (i)
            {
                case 0:
                    ActionOnStartButton();
                    break;
                case 1:
                    ActionOnExitButton();
                    break;
                default:
                    break;
            }
        }

        void ActionOnStartButton()
        {

        }

        void ActionOnExitButton()
        {
            ExitScreen();
        }
    }
}
