using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс для тестирования класса ProjectManager.
    /// </summary>
    internal class ProjectManagerTest
    {
        /// <summary>
        /// Объект проекта контактов.
        /// </summary>
        Project _contactsProject;

        /// <summary>
        /// Метод, выполняемый перед каждым тестом. Создает проект контактов и добавляет в него два контакта.
        /// </summary>
        [SetUp]
        public void CreateContact()
        {
            List<Contact> contacts = new List<Contact>();
            _contactsProject = new Project(contacts);
            contacts.Add(new Contact("Витковски", "Ян", DateTime.Now, "vitkovskijan1103@gmail.com", "vitjan11", new PhoneNumber("+77445885655")));
            contacts.Add(new Contact("Смирнов", "Аркадий", DateTime.Now, "SmirnovArcady2003@mail.ru", "0320smiarc", new PhoneNumber("+77235552328")));
            _contactsProject.Contacts = contacts;
        }

        /// <summary>
        /// Тест на сохранение проекта контактов. Проверяет, что при сохранении проекта не возникает исключений.
        /// </summary>
        [Test(Description = "Сохранение проекта контактов")]
        public void TestSave_CorrectString()
        {
            Assert.DoesNotThrow(
            () => {
                ProjectManager.SaveProject(_contactsProject);
            },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Тест на загрузку проекта контактов. Проверяет, что при загрузке проекта не возникает исключений.
        /// </summary>
        [Test(Description = "Загрузка проекта контактов")]
        public void TestLoad_CorrectString()
        {
            Assert.DoesNotThrow(
            () => {
                _contactsProject = ProjectManager.LoadProject();
            },
            "Не должно возникать исключения");
        }
    }
}
