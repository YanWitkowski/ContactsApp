using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace ContactsApp
{
    /// <summary>
    /// Класс, реализующий сериализацию проекта с контактами.
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Путь к файлу, в котором хранятся данные проекта.
        /// </summary>
        /// C:\Users\USER\source\repos\ContactsApp
        //private const string FilePath = @".\Save.json";
        private const string FilePath = @"C:\Users\USER\source\repos\ContactsApp\Save.json";
        /// <summary>
        /// Метод для сохранения объекта «Проект» в файл.
        /// </summary>
        /// /// <param name="project"></param>
        public static void SaveProject(Project project)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(FilePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            }
        }

        /// <summary>
        /// Метод для загрузки объекта «Проект» из файла.
        /// </summary>
        /// <returns>Эксземпляр класса Project из файла</returns>
        public static Project LoadProject()
        {
            Project project = null;
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(FilePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    project = serializer.Deserialize<Project>(reader);  // Инициализация объекта Project
                    // Инициализация дочерних объектов (если необходимо)
                    foreach (Contact contact in project.Contacts.Values)
                    {
                        contact.PhoneNumber = new PhoneNumber(contact.PhoneNumber.Phone);
                    }

                    //    project = (Project)serializer.Deserialize<Project>(reader);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл проекта не найден.");
            }

            return project;
        }
    }
}
