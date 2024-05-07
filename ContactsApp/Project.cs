using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Класс, содержащий контакты.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Хранит контакты в виде списка объектов типа Contact.
        /// </summary>
        private List<Contact> _contacts;

        /// <summary>
        /// Возвращает или устанавливает список контактов.
        /// </summary>
        public List<Contact> Contacts { get => _contacts; set => _contacts = value; }

        /// <summary>
        /// Создает новый экземпляр класса Project.
        /// </summary>
        /// <param name="contacts">Список контактов.</param>
        public Project(List<Contact> contacts)
        {
            Contacts = contacts;
        }
    }
}
