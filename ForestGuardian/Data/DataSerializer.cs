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
        private static StorageDevice device;
        private static StorageContainer container;
        //Dung de chi controller dung trong game
        private static PlayerIndex playerIndex = PlayerIndex.One;
        private static string ContainerName = "ForestGuardian";

        public static void Initialize()
        {
            IAsyncResult result = StorageDevice.BeginShowSelector(playerIndex, null, null);
            if (result.IsCompleted)
            {
                device = StorageDevice.EndShowSelector(result);
            }
        }

        public static bool Ready(){
            if (device == null || !device.IsConnected) return false;
            else
            {
                IAsyncResult result = device.BeginOpenContainer(ContainerName, null, null);
                if (result.IsCompleted)
                {
                    container = device.EndOpenContainer(result);
                    return true;
                }
                return false;
            }
        }
        
        public static void SaveData<T>(T data, string directoryname, string filename)
        {

            Stream file_stream;
            string file_path = filename;

            if(Ready()){
                if (directoryname != null )
                {
                    if (!container.DirectoryExists(directoryname))
                    {
                        container.CreateDirectory(directoryname);
                    }
                    file_path = directoryname + "/" + filename;
                }
                    
                file_stream = container.OpenFile(file_path, FileMode.Create, FileAccess.Write);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(file_stream, data);
                
                file_stream.Close();
                container.Dispose();
            }
        }

        public static T LoadData<T>(string directoryname , string filename)
        {
            T data = default(T);

            string file_path = filename;
            if (directoryname != null) { file_path = directoryname + "/" + filename; }

            if (Ready() && container.FileExists(file_path))
            {
                Stream file_stream = container.OpenFile(file_path, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                data = (T)serializer.Deserialize(file_stream);

                file_stream.Close();
                container.Dispose();
            }

            return data;
        }

        public static T LoadStaticData<T>(string filepath)
        {
            T data = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StreamReader reader = new StreamReader(TitleContainer.OpenStream(filepath));
                data = (T)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            return data;
        }

        public static bool FileExists(string directoryname, string filename)
        {
            string file_path = filename;
            if (directoryname != null) { file_path = directoryname + "/" + filename; }
            if (Ready() && container.FileExists(file_path)) { return true; }
            return false;
        }
        
    }
}
