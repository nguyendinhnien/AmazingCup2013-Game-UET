using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Data
{
    public class MapReader: ContentTypeReader<Map>
    {
        protected override Map Read(ContentReader input, Map existingInstance)
        {
            Map map = existingInstance;
            if (map == null)
            {
                map = new Map();
            }
            map.Name = input.ReadString();
            map.Width = input.ReadInt32(); 
            map.Height = input.ReadInt32();
            map.TileSize = input.ReadInt32();
            map.BackgroundFile = input.ReadString();
            map.InteractiveMap = input.ReadObject<byte[]>();
            map.StartCell = input.ReadObject<Cell>();
            map.EndCell = input.ReadObject<Cell>();
            map.Waves = input.ReadObject <List<Wave>>();

            //map.PostReading();
            return map;
        }
    }

    public class EntityReader
    {
        public E Read<E>(string filename, ContentManager Content)
        {
            E entity = default(E);
            try
            {
                StreamReader sr = new StreamReader(TitleContainer.OpenStream(Content.RootDirectory + "/" + filename));
                XmlSerializer serializer = new XmlSerializer(typeof(E));
                entity = (E)serializer.Deserialize(sr);
                sr.Close();
            }
            catch (Exception) { }
            return entity;
        }
    }
}
