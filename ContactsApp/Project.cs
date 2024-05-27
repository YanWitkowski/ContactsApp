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

        /// <summary>
        /// Возвращает список контактов, у которых день рождения совпадает с указанным днем и месяцем.
        /// </summary>
        /// <param name="birthday">День рождения для сравнения.</param>
        /// <returns>Список контактов с указанным днем рождения.</returns>
        public List<Contact> GetContactsWithBirthday(DateTime birthday)
        {
            List<Contact> contactsWithBirthday = new List<Contact>();
            for (int index = 0; index < this.Contacts.Count; ++index)
            {
                DateTime birthDate = this.Contacts[index].BirthDate;
                int num;
                if (birthDate.Day == birthday.Day)
                {
                    birthDate = this.Contacts[index].BirthDate;
                    num = birthDate.Month == birthday.Month ? 1 : 0;
                }
                else
                    num = 0;
                if (num != 0)
                    contactsWithBirthday.Add(this.Contacts[index]);
            }
            return contactsWithBirthday;
        }

        /// <summary>
        /// Сортирует список контактов по их строковому представлению.
        /// </summary>
        /// <param name="list">Список контактов для сортировки.</param>
        /// <returns>Отсортированный список контактов.</returns>
        private List<Contact> SortList(List<Contact> list)
        {
            list.Sort((contact1, contact2) => contact1.ToString().CompareTo(contact2.ToString()));
            return list;
        }

        /// <summary>
        /// Возвращает отсортированный список всех контактов.
        /// </summary>
        /// <returns>Отсортированный список контактов.</returns>
        public List<Contact> GetSortedContacts()
        {
            return this.SortList(new List<Contact>((IEnumerable<Contact>)this.Contacts));
        }

        /// <summary>
        /// Возвращает отсортированный список контактов, в именах которых содержится указанная подстрока.
        /// </summary>
        /// <param name="included">Подстрока для проверки в именах контактов.</param>
        /// <returns>Отсортированный список контактов с указанной подстрокой в именах.</returns>
        public List<Contact> GetSortedContacts(string inculded)
        {
            List<Contact> sortedContacts = this.SortList(new List<Contact>((IEnumerable<Contact>)this.Contacts));
            int count = sortedContacts.Count;
            while (count != 0)
            {
                --count;
                Contact contact = sortedContacts[count];
                if (!contact.ToString().Contains(inculded))
                    sortedContacts.Remove(contact);
            }
            return sortedContacts;
        }
    }
}
