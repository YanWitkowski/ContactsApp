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
        /// Хранит контакты в виде словаря со строковыми ключами и значениями типа Contact.
        /// </summary>
        private Dictionary<string, Contact> _contacts;

        /// <summary>
        /// Возвращает или устанавливает словарь контактов.
        /// </summary>
        public Dictionary<string, Contact> Contacts { get => _contacts; set => _contacts = value; }

        /// <summary>
        /// Создает новый экземпляр класса Project.
        /// </summary>
        /// <param name="contacts">Словарь контактов.</param>
        public Project(Dictionary<string, Contact> contacts)
            {
                Contacts = contacts;
            }
    }
}
