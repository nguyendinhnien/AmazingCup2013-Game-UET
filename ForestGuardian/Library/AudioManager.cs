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
        private static AudioCategory musicCategory;
        private static AudioCategory soundCategory;
        public static SoundBank musicBank;
        public static SoundBank soundBank;
        private static WaveBank waveBank;

        public static Cue openGame;
        public static Cue song1;
        public static Cue song2;
        public static Cue song3;

        public static Cue bullet1;
        public static Cue bullet2;
        public static Cue bullet3;

        public static Cue enemyDeath1;
        public static Cue enemyDeath2;
        public static Cue enemyDeath3;

        public static Cue enemyMoving1;
        public static Cue enemyMoving2;
        public static Cue enemyMoving3;

        public static Cue towerSelect;
        public static Cue placeTower;
        public static Cue upgradeTower;
        public static Cue sellTower;

        public static Cue mouseClick;
        public static Cue escape;

        public static void Initialize()
        {
            audioEngine = new AudioEngine(@"Content\audio\AudioProject.xgs");
            soundBank = new SoundBank(audioEngine, @"Content\audio\SoundBank.xsb");
            musicBank = new SoundBank(audioEngine, @"Content\audio\MusicBank.xsb");
            waveBank = new WaveBank(audioEngine, @"Content\audio\WaveBank.xwb");

            musicCategory = audioEngine.GetCategory("Music");
            soundCategory = audioEngine.GetCategory("Sound");

            openGame = musicBank.GetCue("open_game");
            //openGame.Play();
            song1 = musicBank.GetCue("song_1");
            song2 = musicBank.GetCue("song_2");
            song3 = musicBank.GetCue("song_3");

            bullet1 = soundBank.GetCue("bullet_1");
            bullet2 = soundBank.GetCue("bullet_2");
            bullet3 = soundBank.GetCue("bullet_3");

            enemyDeath1 = soundBank.GetCue("death_1");
            enemyDeath2 = soundBank.GetCue("death_2");
            enemyDeath3 = soundBank.GetCue("death_3");

            enemyMoving1 = soundBank.GetCue("move_loop_1");
            enemyMoving2 = soundBank.GetCue("move_loop_2");
            enemyMoving3 = soundBank.GetCue("move_loop_3");

            towerSelect = soundBank.GetCue("tower_select");
            placeTower = soundBank.GetCue("place_tower");
            upgradeTower = soundBank.GetCue("upgrade_tower");
            sellTower = soundBank.GetCue("sell_tower");

            mouseClick = soundBank.GetCue("mouse_click");
            escape = soundBank.GetCue("escape");
        }

        public static void Update()
        {
            audioEngine.Update();
        }

        public static void SetMusicVolume(int vol)
        {
            musicCategory.SetVolume((float)vol * 0.01f);
        }

        public static void SetSoundVolume(int vol)
        {
            soundCategory.SetVolume((float)vol * 0.01f);
        }
    }
}