using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;
namespace Library
{
    public class Loader
    {
        public static void LoadEntitiesFromFile(String filename, ContentManager Content)
        {
            StreamReader sr = new StreamReader(TitleContainer.OpenStream(Content.RootDirectory + "/" + filename));
            XmlTextReader xr = new XmlTextReader(sr);
            XmlDocument doc = new XmlDocument();
            doc.Load(xr);

            XmlNode node,child_node;
            //Node Enemy
            node = doc.DocumentElement.FirstChild;
            if (node.Name == "Enemy")
            {
                child_node = node.FirstChild;
                //Node AxeMan
                AxeMan.MAX_HEALTH = float.Parse(child_node.Attributes["health"].Value);
                AxeMan.VALUE = int.Parse(child_node.Attributes["value"].Value);
                AxeMan.MOVE_SPEED = float.Parse(child_node.Attributes["speed"].Value);
                string texture_location = child_node.Attributes["texture_location"].Value;
                AxeMan.TEXTURE = Content.Load<Texture2D>(texture_location);

                //Node SawMan
                child_node = child_node.NextSibling;
                SawMan.MAX_HEALTH = float.Parse(child_node.Attributes["health"].Value);
                SawMan.VALUE = int.Parse(child_node.Attributes["value"].Value);
                SawMan.MOVE_SPEED = float.Parse(child_node.Attributes["speed"].Value);
                texture_location = child_node.Attributes["texture_location"].Value;
                //SawMan.TEXTURE = Content.Load<Texture2D>(texture_location);
            }

            node = node.NextSibling;
            if (node.Name == "Tower")
            {
                child_node = node.FirstChild;
                //Node ArrowTower
                ArrowTower.MAX_HEALTH = float.Parse(child_node.Attributes["health"].Value);
                ArrowTower.COST = int.Parse(child_node.Attributes["cost"].Value);
                ArrowTower.RANGE = int.Parse(child_node.Attributes["range"].Value);
                ArrowTower.FIRE_RELOAD = int.Parse(child_node.Attributes["fire_reload"].Value);
                ArrowTower.DAMAGE = int.Parse(child_node.Attributes["damage"].Value);
                string bullet_texLoc = child_node.Attributes["bullet_texture"].Value;
                ArrowTower.BULLET_TEXTURE = Content.Load<Texture2D>(bullet_texLoc);
                string texture_location = child_node.Attributes["texture_location"].Value;
                ArrowTower.TEXTURE = Content.Load<Texture2D>(texture_location);
                
            }
        }
    }
}
