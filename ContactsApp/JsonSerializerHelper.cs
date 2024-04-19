using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Сериализует объект в файл JSON.
    /// </summary>
    /// <typeparam name="TSerializable">Тип сериализуемого объекта.</typeparam>
    /// <param name="serializableObject">Сериализуемый объект.</param>
    /// <param name="filePath">Путь к файлу JSON.</param>
    public class JsonSerializerHelper
    {
        public void SerializeObject<TSerializable>(TSerializable serializableObject, string filePath)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter fileWriter = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(fileWriter))
            {
                serializer.Serialize(writer, serializableObject);
            }

        }

        /// <summary>
        /// Десериализует объект из файла JSON.
        /// </summary>
        /// <typeparam name="TSerializable">Тип десериализуемого объекта.</typeparam>
        /// <param name="filePath">Путь к файлу JSON.</param>
        /// <returns>Десериализованный объект.</returns>
        public TSerializable DeserializeObject<TSerializable>(string filePath)
        {
            TSerializable deserializableObject = default(TSerializable);

            if (File.Exists(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader fileReader = new StreamReader(filePath))
                using (JsonReader reader = new JsonTextReader(fileReader))
                {
                    deserializableObject = serializer.Deserialize<TSerializable>(reader);

                }

            }

            return deserializableObject;

        }
    }
}

