using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Thunder.Platform.Core.Helpers
{
    public static class XmlHelper
    {
        public static TType XmlStringToObject<TType>(string xmlString)
        {
            using (var stringReader = new StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof(TType));
                return (TType)xmlSerializer.Deserialize(stringReader);
            }
        }

        public static string ObjectToXmlString<TType>(TType @object)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new CustomXmlTextWriter(stream))
                {
                    var xmlSerializer = new XmlSerializer(@object.GetType());
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty); // Disable the xmlns:xsi and xmlns:xsd lines.

                    xmlSerializer.Serialize(writer, @object, namespaces);

                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }

        private class CustomXmlTextWriter : XmlTextWriter
        {
            public CustomXmlTextWriter(Stream stream) : base(stream, new UTF8Encoding(false))
            {
                Formatting = Formatting.None;
            }

            public override void WriteEndElement()
            {
                WriteFullEndElement();
            }
        }
    }
}
