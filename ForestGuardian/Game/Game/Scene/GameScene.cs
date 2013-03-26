using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CustomGame
{
    public enum SceneState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class GameScene
    {      
        protected bool isPopup = false;
        protected float transitionPosition = 1;
        protected TimeSpan transitionOnTime = TimeSpan.FromSeconds(5);
        protected TimeSpan transitionOffTime = TimeSpan.Zero;
        SceneState sceneState = SceneState.TransitionOn;
        protected bool otherSceneHasFocus;
        protected bool isExiting = false;
        protected SceneManager sceneManager;

        public event EventHandler Exiting;


        public bool IsPopup
        {
            get { return isPopup; }
            protected set { isPopup = value; }
        }

        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            protected set { transitionOnTime = value; }
        }

        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }
        
        public float TransitionPosition
        {
            get { return transitionPosition; }
            protected set { transitionPosition = value; }
        }

        public byte TransitionAlpha
        {
            get { return (byte)(255 - TransitionPosition * 255); }
        }

        public SceneState SceneState
        {
            get { return sceneState; }
            protected set { sceneState = value; }
        }

        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set
            {
                bool fireEvent = !isExiting && value;
                isExiting = value;
                if (fireEvent && (Exiting != null))
                {
                    Exiting(this, EventArgs.Empty);
                }
            }
        }

        public bool IsActive
        {
            get{
                return !otherSceneHasFocus && (sceneState == SceneState.TransitionOn || sceneState == SceneState.Active);
            }
        }

        public SceneManager SceneManager
        {
            get { return sceneManager; }
            internal set { sceneManager = value; }
        }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void StateUpdate(GameTime gameTime, bool otherSceneHasFocus,bool coveredByOtherScene)
        {
            this.otherSceneHasFocus = otherSceneHasFocus;

            if (IsExiting)
            {
                // If the screen is going away to die, it should transition off.
                sceneState = SceneState.TransitionOff;

                if (!UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    // When the transition finishes, remove the screen.
                    SceneManager.RemoveScene(this);
                }
            }
            else if (coveredByOtherScene)
            {
                // If the screen is covered by another, it should transition off.
                if (UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    // Still busy transitioning.
                    sceneState = SceneState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    sceneState = SceneState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (UpdateTransition(gameTime, transitionOnTime, -1))
                {
                    // Still busy transitioning.
                    sceneState = SceneState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    sceneState = SceneState.Active;
                }
            }
        }


        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);

            // Update the transition position.
            transitionPosition += transitionDelta * direction;

            // Did we reach the end of the transition?
            if ((transitionPosition <= 0) || (transitionPosition >= 1))
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }

        public virtual void Update(GameTime gameTime) { }

        //public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }

        public void ExitScreen()
        {
            // flag that it should transition off and then exit.
            IsExiting = true;
            // If the screen has a zero transition time, remove it immediately.
            if (TransitionOffTime == TimeSpan.Zero)
            {
                SceneManager.RemoveScene(this);
            }
        }
    }
}