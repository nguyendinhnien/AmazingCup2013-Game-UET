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
        private static int MAX_SCORE = 8;
        [XmlArray("Scores")]
        public List<Score> scores;

        public HighScore()
        {
            scores = new List<Score>();
        }
        public void AddScore(Score score)
        {
            int i;
            for (i=0; i < scores.Count; i++)
            {
                if (score.points > scores[i].points) break;
            }
            if (i < MAX_SCORE){
                scores.Insert(i, score);
            }
            
            /*if (scores.Count > MAX_SCORE)
            {
                scores.RemoveRange(scores.Count, MAX_SCORE - scores.Count);
            }*/
            if (scores.Count > MAX_SCORE)
            {
                scores.RemoveRange(MAX_SCORE, scores.Count - MAX_SCORE);
            }
        }

        public int Count
        {
            get { return scores.Count; }
        }
    }
}
