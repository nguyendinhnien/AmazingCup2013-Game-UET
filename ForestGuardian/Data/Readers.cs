using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
