using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Forest
{
    public static class InputManager
    {
        private static MouseState currentMouseState;
        private static bool currentMousePressed;
        private static bool previousMousePressed;

        public static MouseState getMouseState()
        {
            return currentMouseState;
        }

        /// <summary>
        /// Check if the mouse is down.
        /// </summary>
        public static bool IsMouseDown()
        {
            return currentMousePressed;
        }

        /// <summary>
        /// check if the mouse is triggered
        /// </summary>
        public static bool IsMouseTriggered()
        {
            return !previousMousePressed && currentMousePressed;
        }

        /// <summary>
        /// Check if the mouse is just released
        /// </summary>
        public static bool IsMouseJustReleased()
        {
            return previousMousePressed && !currentMousePressed;
        }

        /// <summary>
        /// determine if the mouse hitted the rectangle
        /// </summary>
        public static Boolean IsMouseHittedRectangle(Rectangle rect)
        {
            float x = currentMouseState.X;
            float y = currentMouseState.Y;
            return (x >= rect.Left &&
                x <= rect.Right &&
                y >= rect.Top &&
                y <= rect.Bottom);

        }

        /// <summary>
        /// Initializes the default control keys for all actions.
        /// </summary>
        public static void Initialize()
        {
        }

        /// <summary>
        /// Updates the keyboard and gamepad control states.
        /// </summary>
        public static void Update()
        {
            // update the mouse state
            previousMousePressed = currentMousePressed;
            currentMouseState = Mouse.GetState();
            currentMousePressed =
                (currentMouseState.LeftButton == ButtonState.Pressed);
        }
    }
}
