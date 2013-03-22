using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Data
{
    [XmlRoot]
    [Serializable]
    public class HighScore
    {
        [XmlIgnore]
        private static int MAX_SOCRE = 8;
        [XmlArray("Scores")]
        public List<Score> scores;

        public void AddScore(Score score)
        {
            int i;
            for (i=0; i < scores.Count; i++)
            {
                if (score.point > scores[i].point) break;
            }
            if (i < MAX_SOCRE)
            {
                scores.Insert(i, score);
                scores.Reverse(0, MAX_SOCRE);
            }
        }

        public int Count
        {
            get { return scores.Count; }
        }
    }
}
