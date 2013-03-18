using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using System.Diagnostics;
namespace Pipeline
{
    [ContentTypeWriter]
    public class MapWriter : ContentTypeWriter<MapContent>
    {
        protected override void Write(ContentWriter output, MapContent content)
        {
            Debugger.Launch();
        }
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Library.tile.MapReader, Library";
        }
    }
}
