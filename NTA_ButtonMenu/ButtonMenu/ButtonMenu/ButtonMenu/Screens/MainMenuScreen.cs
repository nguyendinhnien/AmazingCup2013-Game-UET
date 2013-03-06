using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ButtonMenu
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainMenuScreen : GameScreen
    {
        private const int NUMBER_OF_BUTTONS = 6;
        
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D[] buttonTextures;
        private Vector2[] buttonPositions;
        private Rectangle[] buttonRectangles;

        private bool[] isButtonSelected;

        public MainMenuScreen() : base()
        {
            buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
            buttonPositions = new Vector2[NUMBER_OF_BUTTONS];
            buttonRectangles = new Rectangle[NUMBER_OF_BUTTONS];
            isButtonSelected = new bool[NUMBER_OF_BUTTONS];
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            
            backgroundTexture =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_computer_wide");
            buttonTextures[0] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_quit_select");
            buttonTextures[1] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_button1_select");
            buttonTextures[2] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_button2_select");
            buttonTextures[3] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_button3_select");
            buttonTextures[4] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_button4_select");
            buttonTextures[5] =
                content.Load<Texture2D>(@"images/MainMenu/title_screen_button5_select");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
    
            buttonPositions[0] = new Vector2(0, 10);
            buttonRectangles[0] = new Rectangle(0, 10,
                buttonTextures[0].Width, buttonTextures[0].Height);

            buttonPositions[1] = new Vector2(-10, 615);
            buttonRectangles[1] = new Rectangle(-10, 615,
                buttonTextures[1].Width, buttonTextures[1].Height);

            buttonPositions[2] = new Vector2(180, 640);
            buttonRectangles[2] = new Rectangle(180, 640,
                buttonTextures[2].Width, buttonTextures[2].Height);

            buttonPositions[3] = new Vector2(380, 615);
            buttonRectangles[3] = new Rectangle(380, 615,
                buttonTextures[3].Width, buttonTextures[3].Height);

            buttonPositions[4] = new Vector2(570, 635);
            buttonRectangles[4] = new Rectangle(570, 635,
                buttonTextures[4].Width, buttonTextures[4].Height);

            buttonPositions[5] = new Vector2(830, 645);
            buttonRectangles[5] = new Rectangle(830, 645,
                buttonTextures[5].Width, buttonTextures[5].Height);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                if (isButtonSelected[i])
                {
                    spriteBatch.Draw(buttonTextures[i], buttonPositions[i], Color.White);
                }
            }

            spriteBatch.End();
        }

        public override void HandleInput()
        {
            //quit
            bool a = false;
            if (a)
            {
                ExitScreen();
                return;
            }
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
                        TakeActionOnButton(i);
                    }
                }
                else
                {
                    //do something
                    isButtonSelected[i] = false;
                }
        }

        /// <summary>
        /// Logic for each Image when clicked by mouse
        /// </summary>
        void TakeActionOnButton(int i)
        {
            switch (i)
            {
                case 0:
                    ActionOnQuitButton();
                    break;
                case 1:
                    ActionOnPlayButton();
                    break;
                case 2:
                    ActionOnResumeButton();
                    break;
                case 3:
                    ActionOnScoresButton();
                    break;
                case 4:
                    ActionOnOptionsButton();
                    break;
                case 5:
                    ActionOnHelpButton();
                    break;
                default:
                    break;
            }
        }

        void ActionOnQuitButton()
        {
            String message = "Are you sure want to quit?";
            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);
            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmExitMessageBox);
        }

        void ActionOnPlayButton()
        {
            ScreenManager.AddScreen(new NewGameScreen());
        }

        void ActionOnResumeButton()
        {
        }

        void ActionOnScoresButton()
        {
        }

        void ActionOnOptionsButton()
        {
        }

        void ActionOnHelpButton()
        {
            ScreenManager.AddScreen(new HelpScreen());
        }

        void ConfirmExitMessageBoxAccepted(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }

}
