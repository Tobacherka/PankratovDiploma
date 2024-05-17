using System.Text;
using System.Xml.Serialization;

namespace ApiPunk.Classes
{
    public static class XmlHelper
    {
        /// <summary>
        /// Сериализация объекта в файл указанного пути
        /// </summary>
        public static void SerializeToXmlForFile<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Сериализация объекта в переменную типа string
        /// </summary>
        public static string SerializeToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, obj);
                memoryStream.Position = 0; // Reset position for reading
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Десиреализация объекта из файла
        /// </summary>
        public static T DeserializeFromXmlForFile<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Десиреализация объекта из строки
        /// </summary>
        public static T DeserializeFromXml<T>(string xmlString)
        {
            XmlSerializer serializer = new(typeof(T));
            using (MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(xmlString)))
            {
                return (T)serializer.Deserialize(memoryStream);
            }
        }
    }
}
