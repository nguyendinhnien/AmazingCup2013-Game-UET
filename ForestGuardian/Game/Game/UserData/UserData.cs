using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Data;
namespace CustomGame
{
    public enum Mode
    {
        Classic,Death,Time
    }
    public static class UserData
    {     
        public static bool isFullScreen = false;

        public static Setting setting;
        public static string CustomSettingDirectory = "CustomSetting";
        public static string CustomSettingFile = "customsetting.sav";
        public static string DefaultSettingFilePath = "Content/data/default_setting/setting.xml";

        public static string HighScoreDirectory = "HighScore";
        public static string HighScoreFile = "highscore.sav";
        public static HighScore highscore;
        public static Color[] colors = {Color.Red,Color.Orange,Color.YellowGreen,
                                           Color.Lime, Color.Lime, Color.Lime, Color.Lime, Color.Lime};
        
        //Do kho
        public static int mode;        
        public static int mapIndex = 0;

        public static void LoadSetting()
        {
            //Phan score
            if (DataSerializer.FileExists(HighScoreDirectory, HighScoreFile))
            {
                highscore = DataSerializer.LoadData<HighScore>(HighScoreDirectory, HighScoreFile);
            }
            else { 
                highscore = new HighScore();
                DataSerializer.SaveData<HighScore>(highscore, HighScoreDirectory, HighScoreFile);
            }

            //Phan custom setting
            if (DataSerializer.FileExists(CustomSettingDirectory, CustomSettingFile))
            {
                setting = DataSerializer.LoadData<Setting>(CustomSettingDirectory, CustomSettingFile);
            }
            //Neu chua co thi load default setting
            else
            {
                setting = DataSerializer.LoadStaticData<Setting>(DefaultSettingFilePath);
            }
        }

        public static bool isTowerLock(int towerIndex)
        {
            return towerIndex >= setting.towerLockIndex;
        }
    }
}
