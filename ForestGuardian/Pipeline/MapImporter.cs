using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using System.Diagnostics;
namespace Pipeline
{
    [ContentImporter(".map", DisplayName = "MapImporter",
        DefaultProcessor = "MapProcessor")]
    class MapImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            Debug.WriteLine("Kien dep trai");
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            return doc;
        }
    }
}
