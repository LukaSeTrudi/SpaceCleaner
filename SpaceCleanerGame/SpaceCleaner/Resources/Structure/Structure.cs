using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace SpaceCleaner.Resources.Structure
{
    

    public class Root
    {
        //public Player Player;
        public string Test;
    }

    static public class Structure
    {
        public static Root Deserialize(ContentManager content)
        {
            return content.Load<Root>("structure");
        }

        public static void Serialize()
        {
            Root testData = new Root();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            {
                //IntermediateSerializer.Serialize(writer, testData, null);
            }
        }
    }
}
