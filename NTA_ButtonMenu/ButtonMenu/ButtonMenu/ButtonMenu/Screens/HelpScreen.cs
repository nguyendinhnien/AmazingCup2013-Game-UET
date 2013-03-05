#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace ButtonMenu
{
    /// <summary>
    /// Shows the help screen, explaining the basic game idea to the user.
    /// </summary>
    class HelpScreen : GameScreen
    {
        #region Fields

        private const int NUMBER_OF_HELP_SCREENS = 3;
        private const int NUMBER_OF_BUTTONS = 3;

        private Texture2D[] backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D[] buttonSelected;
        private Vector2[] buttonPositions;
        private Rectangle[] buttonRectangles;

        private bool[] isButtonSelected;

        private int currentBackground;

        #endregion


        #region Initialization

        public HelpScreen()
            : base()
        {
            isButtonSelected = new bool[NUMBER_OF_BUTTONS];
            buttonPositions = new Vector2[NUMBER_OF_BUTTONS];
            buttonRectangles = new Rectangle[NUMBER_OF_BUTTONS];
            buttonSelected = new Texture2D[NUMBER_OF_BUTTONS];
            backgroundTexture = new Texture2D[NUMBER_OF_HELP_SCREENS];
            currentBackground = 0;
        }

        /// <summary>
        /// Loads the graphics content for this screen
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            ContentManager content = ScreenManager.Game.Content;

            backgroundTexture[0] = content.Load<Texture2D>(@"images\HelpScreen\help_build_creatively_wide");
            backgroundTexture[1] = content.Load<Texture2D>(@"images\HelpScreen\help_towers_01");
            backgroundTexture[2] = content.Load<Texture2D>(@"images\HelpScreen\help_towers_02");

            buttonSelected[0] = content.Load<Texture2D>(@"images\HelpScreen\button_selection_vertical");
            buttonSelected[1] = buttonSelected[0];
            buttonSelected[2] = content.Load<Texture2D>(@"images\HelpScreen\button_selection_square");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture[0].Width) / 2,
                (viewport.Height - backgroundTexture[0].Height) / 2);

            buttonPositions[0] = new Vector2(1000, 300);
            buttonRectangles[0] = new Rectangle(1000, 300, buttonSelected[0].Width, buttonSelected[0].Height);

            buttonPositions[1] = new Vector2(100, 300);
            buttonRectangles[1] = new Rectangle(100, 300, buttonSelected[1].Width, buttonSelected[1].Height);

            buttonPositions[2] = new Vector2(900, 30);
            buttonRectangles[2] = new Rectangle(900, 30, buttonSelected[2].Width, buttonSelected[2].Height);
        }


        #endregion


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture[currentBackground], backgroundPosition, Color.White);
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                if (isButtonSelected[i])
                {
                    spriteBatch.Draw(buttonSelected[i], buttonPositions[i], Color.White);
                }
            }

            spriteBatch.End();
        }

/*        public override void HandleInput()
        {
            //check every image can click in the screen
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
        }*/

        /// <summary>
        /// Logic for each Image when clicked by mouse
        /// </summary>
        void TakeActionOnImage(int i)
        {
            switch (i)
            {
                case 0:
                    ActionOnNextButton();
                    break;
                case 1:
                    ActionOnBackButton();
                    break;
                case 2:
                    ActionOnExitButton();
                    break;
                default:
                    break;
            }
        }

        void ActionOnNextButton()
        {
            currentBackground++;
            if (currentBackground >= NUMBER_OF_HELP_SCREENS)
                currentBackground = 0;
        }

        void ActionOnBackButton()
        {
            currentBackground--;
            if (currentBackground < 0)
                currentBackground = NUMBER_OF_HELP_SCREENS - 1;
        }

        void ActionOnExitButton()
        {
            ExitScreen();
        }
    }
}