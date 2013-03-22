using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace Data
{
    public class DataSerializer
    {
        public static void SaveData<T>(T data, string filename)
        {
            using(StreamWriter writer = new StreamWriter(TitleContainer.OpenStream(filename))){
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
                writer.Close();
            }
        }
        public static T LoadData<T>(string filename)
        {
            T data = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                
                StreamReader reader = new StreamReader(TitleContainer.OpenStream(filename));
                data = (T)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            return data;
        }
    }
}
