using System.Xml;
using System.IO;

namespace RotMGTool.Manager
{
    public class XmlManager
    {
        public void CreateOrAppendXml(string directory, string fileName, string elementName, string value)
        {
            string filePath = Path.Combine(directory, fileName);

            XmlDocument doc = new XmlDocument();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
            }
            else
            {
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement newElement = doc.CreateElement(string.Empty, "Root", string.Empty);
                doc.AppendChild(newElement);
            }

            XmlNode rootNode = doc.DocumentElement;
            XmlElement element = doc.CreateElement(elementName);
            element.InnerText = value;
            rootNode.AppendChild(element);

            doc.Save(filePath);
        }
    }
}
