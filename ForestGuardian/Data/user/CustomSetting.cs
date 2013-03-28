using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Data
{
    [Serializable]
    [XmlRoot("Setting")] 
    public class Setting
    {
        [XmlElement("MusicVolume")]
        public int music_volume;
        [XmlElement("SoundVolume")]
        public int sound_volume;
        [XmlElement("TowerLockIndex")]
        public int towerLockIndex;
        [XmlElement("MapLockIndex")]
        public int mapLockIndex;
    }
}
