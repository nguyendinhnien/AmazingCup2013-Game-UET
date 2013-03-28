using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Data
{
    [Serializable]
    public class Score
    {
        [XmlElement("PlayerName")]
        public string player_name;
        [XmlElement("Points")]
        public int points;

        public Score(){}

        public Score(string player_name, int points)
        {
            this.player_name = player_name;
            this.points = points;
        }
    }
}
