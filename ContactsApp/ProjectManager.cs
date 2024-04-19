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
    /// Класс, реализующий метод для сохранения объекта «Проект» в файл и метод загрузки проекта из файла.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Путь к файлу, в котором хранятся данные проекта.
        /// </summary>
        private const string FilePath = @"C:\My Documents\ContactsApp.notes";

        /// <summary>
        /// Метод для сохранения объекта «Проект» в файл.
        /// </summary>
        public void SaveProject(Project project)
        {
            // Преобразование объекта в JSON-строку.
            string jsonData = JsonConvert.SerializeObject(project);

            // Сохранение JSON-строки в файл.
            File.WriteAllText(FilePath, jsonData); 
        }

        /// <summary>
        /// Метод для загрузки объекта «Проект» из файла.
        /// </summary>
        public Project LoadProject()
        {
            // Проверка наличия файла
            if (File.Exists(FilePath)) 
            {
                // Загрузка JSON-строки из файла
                string jsonData = File.ReadAllText(FilePath);

                // Преобразование JSON-строки в объект типа Project
                return JsonConvert.DeserializeObject<Project>(jsonData); 
            }
            else
            {
                throw new FileNotFoundException("Файл не найден");
            }
        }
    }
}
