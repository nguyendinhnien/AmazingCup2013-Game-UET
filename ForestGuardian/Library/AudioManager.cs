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

        public static bool isMoveLoopPlaying = false; //first playing
        public static bool isMoveLoopPause = false;

        public static Cue moveLoop1;
        public static Cue moveLoop2;
        public static Cue moveLoop3;

        public static void Initialize()
        {
            audioEngine = new AudioEngine(@"Content\audio\AudioProject.xgs");
            soundBank = new SoundBank(audioEngine, @"Content\audio\SoundBank.xsb");
            waveBank = new WaveBank(audioEngine, @"Content\audio\WaveBank.xwb");

            soundCategory = audioEngine.GetCategory("Sound");

            moveLoop1 = soundBank.GetCue("move_loop_1");
            moveLoop2 = soundBank.GetCue("move_loop_2");
            moveLoop3 = soundBank.GetCue("move_loop_3");
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

