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
        private const string FilePath = @".\Save.json";
        /// <summary>
        /// Метод для сохранения объекта «Проект» в файл.
        /// </summary>
        /// /// <param name="project"></param>
        public static void SaveProject(Project project)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(FilePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, project);
                }
                Console.WriteLine("Проект успешно сохранен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении проекта: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод для загрузки объекта «Проект» из файла.
        /// </summary>
        /// <returns>Эксземпляр класса Project из файла</returns>
        /// 
        public static Project LoadProject()
        {
            Project project = null;
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(FilePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    project = serializer.Deserialize<Project>(reader);
                }
                Console.WriteLine("Проект успешно загружен.");
            }
            catch
            {
                Console.WriteLine("Файл проекта не найден.");
            }
            return project;
        }
    }
}
