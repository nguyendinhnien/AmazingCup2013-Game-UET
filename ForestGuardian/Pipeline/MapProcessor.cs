using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using System.Diagnostics;
namespace Pipeline
{
    [ContentProcessor(DisplayName = "MapProcessor")]
    public class MapProcessor : ContentProcessor<XmlDocument,MapContent>
    {
        public override MapContent Process(XmlDocument input, ContentProcessorContext context)
        {
            MapContent map_content = new MapContent();
            Debugger.Launch();

            return map_content;
        }
    }
}
