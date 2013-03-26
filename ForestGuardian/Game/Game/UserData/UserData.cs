using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CustomGame
{
    public static class UserData
    {
        public static Color[] colors;
        public static bool isFullScreen;
        public static int music;
        public static int sound;
        public static int level;
        public static string mapFile = @"data\maps\map1";

        public static string[] tower;
        public static bool[] isLook;

        public static string[][] levelScene;

        public static void Init()
        {
            isFullScreen = false;
            music = 3;
            sound = 3;
            level = 0;

            colors = new Color[8];
            colors[0] = Color.Red;
            colors[1] = Color.Orange;
            colors[2] = Color.YellowGreen;
            for (int i = 3; i < 8; i++)
                colors[i] = Color.Lime;

            tower = new string[3];
            /*oak[0] = "Having grown powerful\n" +
                    "absorb age and wisdom\n" +
                    "over thousands of years,\n" +
                    "Oak is the main force\n" +
                    "of Forest Guardian.\n";*/
            tower[0] = "Name: The Protector - Oak\nDamage: 10/20/30\nRange: 70\nCost: 3$";
            /*
            cactus = new string[2];
            tower[1] = "Born in desert, heat\n" +
                        "and sand make Cactus more\n" +
                        "strong, he can strike an\n" +
                        "enemy with a concentrated\n" +
                        "shot of poison from his spines.";*/
            tower[1] = "Name: The Slower - Cactus\nDamage: 5/10/15\nSlow: 20%/30%/50%\nRange: 90\nCost: 7$";
            tower[2] = "Name: The Boomer-Pineapple\nDamage: 20/40/60\nRange: 120\nSplash damage: 50%\nSplash range: 30\nCost: 20$";

            levelScene = new string[3][];
            for (int i = 0; i < 3; i++)
                levelScene[i] = new string[2];

            levelScene[0][0] = "PROTECT PANDA";
            levelScene[0][1] = "You are my sunshine\nYou are my sunset\nYou are the only one";
            levelScene[1][0] = "MULTIWAY";
            levelScene[1][1] = "Biet ghi cai quai gi bay gio";
            levelScene[2][0] = "ZIKZAK";
            levelScene[2][1] = "Thoi ty nua google vay :)";

            isLook = new bool[4];
            isLook[0] = false;
            isLook[1] = false;
            isLook[2] = false;
            isLook[3] = true;
        }
    }
}
