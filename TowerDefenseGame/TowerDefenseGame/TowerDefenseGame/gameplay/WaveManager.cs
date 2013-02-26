﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseGame.gameplay
{
    class WaveManager
    {
        private int total_wave_number;
        private int current_wave_number;

        private Queue<Wave> waves;

        private bool waiting;
        private bool finish;

        public Wave CurrentWave
        {
            get { return waves.Peek(); }
        }

        public int CurrentWaveNumber
        {
            get { return current_wave_number; }
        }
        public WaveManager(Queue<Wave> waves)
        {
            this.waves = waves;
            this.total_wave_number = waves.Count;
            this.current_wave_number = this.total_wave_number;
            this.finish = false;
        }

        
        public void Update(GameTime gameTime)
        {
            //Neu van con wave
            if (waves.Count > 0)
            {
                Wave current_wave = waves.Peek();
                current_wave.Update(gameTime);
                if (current_wave.Finish)
                {
                    waves.Dequeue();
                }
            }
            //Neu da het wave
            else
            {
                finish = true;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Wave current_wave = waves.Peek();
            current_wave.Draw(spriteBatch);
        }
    }
}
