using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CustomGame
{
    public static class UserData
    {
        public static string[] usernames;
        public static int[] topScores;
        public static Color[] colors;
        public static bool isFullScreen;
        public static int sound;

        public static void Init()
        {
            isFullScreen = false;
            sound = 3;
            usernames = new string[8];
            topScores = new int[8];

            colors = new Color[8];
            colors[0] = Color.Red;
            colors[1] = Color.Orange;
            colors[2] = Color.YellowGreen;
            for (int i = 3; i < 8; i++)
                colors[i] = Color.Lime;

            for (int i = 0; i < 8; i++)
            {
                usernames[i] = "";
                topScores[i] = 0;
            }
        }

        public static void AddHighScore(int score, string name)
        {
            for (int i = 0; i < 8; i++)
            {
                if (score < topScores[i])
                    continue;

                int score1, score2;
                string name1, name2;
                score1 = score;
                name1 = name;
                for (int j = i; j < 8; j++)
                {
                    score2 = topScores[j];
                    name2 = usernames[j];
                    topScores[j] = score1;
                    usernames[j] = name1;
                    score1 = score2;
                    name1 = name2;
                }
                return;
            }
        }
    }
}
