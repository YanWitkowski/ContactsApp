using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactsApp.UnitTests
{
    internal class ProjectTest
    {
        /// <summary>
        /// Коллекция контактов проекта.
        /// </summary>
        Project _contactsProject;

        /// <summary>
        /// Создает новый проект.
        /// </summary>
        [SetUp]
        public void CreateContact()
        {
            List<Contact> contacts = new List<Contact>();
            _contactsProject = new Project(contacts);
        }

        /// <summary>
        /// Присвоение проекту верного списка контактов не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного списка")]
        public void TestContactsSet_CorrectString()
        {
            Assert.DoesNotThrow(
            () => {
                List<Contact> contacts = new List<Contact>();
                contacts.Add(new Contact("Витковски", "Ян", DateTime.Now, "vitkovskijan1103@gmail.com", "vitjan11", new PhoneNumber("+77445885655")));
                _contactsProject.Contacts = contacts;
            },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер Contacts должен возвращать верный список контактов.
        /// </summary>
        [Test(Description = "Получение верного списка")]
        public void TestContactsGet_CorrectString()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact("Витковски", "Ян", DateTime.Now, "vitkovskijan1103@gmail.com", "vitjan11", new PhoneNumber("+77445885655")));
            var expected = contacts;

            _contactsProject.Contacts = expected;
            var actual = _contactsProject.Contacts;
            ClassicAssert.AreEqual(expected, actual, "Геттер Contacts возвращает неправильный список");
        }
    }
}
