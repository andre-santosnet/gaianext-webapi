using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace dominio.mdm.helper
{
    public static class SerializationHelper
    {
        public static string GetXMLFromObject<T>(this T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, obj);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during serialization:", ex);
            }
        }

        public static T GetObjectFromXML<T>(string xml)
        {
            if (String.IsNullOrEmpty(xml)) throw new NotSupportedException("ERROR: input string cannot be null.");
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringReader = new StringReader(xml);
                using (var reader = XmlReader.Create(stringReader))
                {
                    return (T)xmlserializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during deserialization:", ex);
            }
        }


        public static string SerializeJson<T>(T o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T DeSerializeJson<T>(string convertido)
        {
            return JsonConvert.DeserializeObject<T>(convertido);
        }

    }
}
