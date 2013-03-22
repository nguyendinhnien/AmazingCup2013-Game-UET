using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Animation
    {
        private Texture2D texture;
        private int frameNumber;
        private float frameTime;
        private int frameWidth;
        private int frameHeight;
        private bool isLoop;
        Rectangle[] frames;

        public Animation(Texture2D texture, int cols, int rows, float frameTime, bool isLoop)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLoop = isLoop;
            this.frameNumber = cols * rows;

            frames = new Rectangle[frameNumber];

            frameWidth = texture.Width / cols;
            frameHeight = texture.Height / rows;

            int x = 0, y = 0;
            for (int i = 0; i < frameNumber; i++)
            {
                frames[i] = new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                x++;
                if (x % cols == 0)
                {
                    x = 0; y++;
                }
            }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }
        public int FrameNumber
        {
            get { return frameNumber; }
        }
        public float FrameTime
        {
            get { return frameTime; }
        }
        public int FrameWidth
        {
            get { return frameWidth; }
        }
        public int FrameHeight
        {
            get { return frameHeight; }
        }
        public bool IsLoop
        {
            get { return isLoop;}
        }
        public Rectangle[] Frames
        {
            get { return frames; }
        }
    }
}
