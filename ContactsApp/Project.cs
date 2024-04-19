using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContactsApp
{
    /// <summary>
    /// Класс, хранящий список всех контактов.
    /// </summary>
    public class Project
    {
        private List<Contact> contacts;

        /// <summary>
        /// Создает новый экземпляр класса Project.
        /// </summary>
        public Project()
        {
            contacts = new List<Contact>(); 
        }

        /// <summary>
        /// Добавляет контакт в хранилище.
        /// </summary>
        public void AddContact(Contact contact) 
        {
            contacts.Add(contact);
        }

        /// <summary>
        /// Удаляет контакт из хранилища.
        /// </summary>
        public void RemoveContact(Contact contact)
        {
            contacts.Remove(contact);  
        }

        /// <summary>
        /// Возвращает список всех контактов в хранилище.
        /// </summary>
        public List<Contact> GetAllContacts()
        {
            return contacts;
        }
    }
}
