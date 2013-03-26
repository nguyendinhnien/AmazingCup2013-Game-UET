using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class WaveManager
    {
        private int total_wave_number;
        private int current_wave_number;

        private Queue<Wave> waves;

        private bool finish=false;
        //Dang cho doi giua cac wave
        private bool waiting = true;
        private float maxWaveDelay = 3.8f;
        private float timer;

        public Wave CurrentWave
        {
            get { return waves.Peek(); }
        }

        public int CurrentWaveNumber
        {
            get { return current_wave_number; }
        }
        public int TotalWaveNumber
        {
            get { return total_wave_number; }
        }

        public bool Finish
        {
            get { return finish; }
        }

        public bool Waiting
        {
            get { return waiting; }
        }

        public float Timer
        {
            get { return timer; }
        }

        public WaveManager(Queue<Wave> waves)
        {
            this.waves = waves;
            this.total_wave_number = waves.Count;
            this.current_wave_number = 1;
            this.timer = maxWaveDelay;
        }

        
        public void Update(GameTime gameTime)
        {           
            //Neu van con wave
            if (waves.Count > 0)
            {
                Wave current_wave = waves.Peek();
                switch (current_wave.State)
                {
                    case WaveState.Start:
                        timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timer <= 0.0f)
                        {
                            timer = maxWaveDelay; current_wave.State = WaveState.Active;
                            waiting = false;
                        }
                        break;
                    case WaveState.Active:
                        current_wave.Update(gameTime);
                        break;
                    case WaveState.InActive:
                        current_wave.Update(gameTime);
                        current_wave.State = WaveState.Finish;
                        break;
                    case WaveState.Finish:
                        waves.Dequeue();
                        if (current_wave_number < total_wave_number) { current_wave_number++; waiting = true; }
                        else { finish = true; }
                        break;
                }
            }
            else
            {
                finish = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (waves.Count > 0)
            {
                Wave current_wave = waves.Peek();
                current_wave.Draw(spriteBatch);
            }
        }
    }
}

