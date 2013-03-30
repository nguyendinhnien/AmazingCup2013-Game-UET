using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Library
{
    public static class AudioManager
    {
        private static AudioEngine audioEngine;
        private static AudioCategory soundCategory;
        public static SoundBank soundBank;
        private static WaveBank waveBank;

        public static void Initialize()
        {
            audioEngine = new AudioEngine(@"Content\audio\AudioProject.xgs");
            soundBank = new SoundBank(audioEngine, @"Content\audio\SoundBank.xsb");
            waveBank = new WaveBank(audioEngine, @"Content\audio\WaveBank.xwb");

            soundCategory = audioEngine.GetCategory("Sound");
        }

        public static void Update()
        {
            audioEngine.Update();
        }

        public static void SetSoundVolume(int vol)
        {
            soundCategory.SetVolume((float)vol * 0.01f);
        }
    }
}