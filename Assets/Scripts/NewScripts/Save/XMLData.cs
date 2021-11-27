using System.Xml;
using UnityEngine;

namespace AS
{
    public sealed class XMLData : IDataSave<SavedData>
    {
        public void Save(SavedData player, string path = "")
        {
            var xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("Player");
            xmlDoc.AppendChild(rootNode);

            var element = xmlDoc.CreateElement("Name");
            element.SetAttribute("value", player.Name);
            rootNode.AppendChild(element);

            var element2 = xmlDoc.CreateElement("Test");
            element2.SetAttribute("X", player.Health.ToString());
            //element2.SetAttribute("Y", player.Health.Y.ToString());
            //element2.SetAttribute("Z", player.Health.Z.ToString());
            element.AppendChild(element2);

            //element = xmlDoc.CreateElement("PosX");
            //element.SetAttribute("value", player.Health.X.ToString());
            //rootNode.AppendChild(element);

            //element = xmlDoc.CreateElement("PosY");
            //element.SetAttribute("value", player.Health.Y.ToString());
            //rootNode.AppendChild(element);

            //element = xmlDoc.CreateElement("PosZ");
            //element.SetAttribute("value", player.Health.Z.ToString());
            //rootNode.AppendChild(element);


            XmlNode userNode = xmlDoc.CreateElement("Info");
            var attribute = xmlDoc.CreateAttribute("Unity");
            attribute.Value = Application.unityVersion;
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "System Language: " +
                                 Application.systemLanguage;
            rootNode.AppendChild(userNode);

            xmlDoc.Save(path);
        }

        public SavedData Load(string path = "")
        {
            var result = new SavedData();
            if (!System.IO.File.Exists(path)) return result;
            using (var reader = new XmlTextReader(path))
            {
                while (reader.Read())
                {
                    var key = "Name";
                    if (reader.IsStartElement(key))
                    {
                        result.Name = reader.GetAttribute("value");
                    }
                    key = "Test";
                    if (reader.IsStartElement(key))
                    {
                        var ss = reader.GetAttribute("X");
                        int a = System.Convert.ToInt32(ss);
                        result.Health = a;
                    }
                    //key = "PosY";
                    //if (reader.IsStartElement(key))
                    //{
                    //    result.Health.Y = reader.GetAttribute("value").TrySingle();
                    //}
                    //key = "PosZ";
                    //if (reader.IsStartElement(key))
                    //{
                    //    result.Health.Z = reader.GetAttribute("value").TrySingle();
                    //}

                }
            }

            return result;
        }
    }
}


