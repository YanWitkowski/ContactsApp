using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ContactsApp;
using Newtonsoft.Json;

namespace ContactsAppUI
{
    /// <summary>
    /// Основная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        Project _contactsProject;

        /// <summary>
        /// Список всех контактов
        /// </summary>
        public Project ContactsProject
        {
            get => _contactsProject;
            set => _contactsProject = value;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            bool projectLoaded = false;

            do
            {
                ContactsProject = ProjectManager.LoadProject();

                if (ContactsProject == null)
                {
                    var result = MessageBox.Show("Проект не был корректно загружен. Хотите попробовать загрузить его снова?", "Ошибка загрузки проекта", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.No)
                    {
                        projectLoaded = true; // Прекращаем пытаться загрузить проект
                    }
                }
                else
                {
                    projectLoaded = true; // Проект успешно загружен, выходим из цикла
                    RecreateContactList();
                }
            } while (!projectLoaded);

            List<Contact> contactsWithBirthday = ContactsProject.GetContactsWithBirthday(DateTime.Now);
            if (contactsWithBirthday.Count == 0)
            {
                birthdayListLabel.Text = "Сегодня нет контактов с днем рождения";
            }
            else
            {
                birthdayListLabel.Text = "Сегодня день рождения: \n";
                for (int index = 0; index < contactsWithBirthday.Count; ++index)
                {
                    birthdayListLabel.Text += contactsWithBirthday[index].ToString();
                    if (index < contactsWithBirthday.Count - 1)
                        birthdayListLabel.Text += ", ";
                }
            }
            CreateContactList();
        }

        private void CreateContactList(bool selectSomething = true, int selectedIndex = 0)
        {
            string searchText = SearchTextBox.Text.ToLower();
            List<Contact> filteredContacts;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredContacts = ContactsProject.GetSortedContacts().ToList();
            }
            else
            {
                filteredContacts = ContactsProject.Contacts
                    .Where(contact => contact.FirstName.ToLower().Contains(searchText) ||
                                      contact.LastName.ToLower().Contains(searchText))
                    .OrderBy(contact => contact.LastName)
                    .ThenBy(contact => contact.FirstName)
                    .ToList();
            }

            ContactsListBox.Items.Clear();

            foreach (var contact in filteredContacts)
            {
                ContactsListBox.Items.Add(contact);
            }

            if (selectSomething && ContactsListBox.Items.Count > 0)
            {
                selectedIndex = selectedIndex < 0 || selectedIndex >= ContactsListBox.Items.Count ? 0 : selectedIndex;
                ContactsListBox.SelectedIndex = selectedIndex;
            }
        }


        /// <summary>
        /// Добавляет новый контакт.
        /// </summary>
        private void AddNewContact()
        {
            AddEditForm addEditContactForm = new AddEditForm();
            var dialogResult = addEditContactForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Contact contact = addEditContactForm.CurrentContact;
                ContactsProject.Contacts.Add(contact);
                RecreateContactList(ContactsProject.Contacts.ToArray().Length - 1);
                ProjectManager.SaveProject(ContactsProject);
            }
        }

        /// <summary>
        /// Редактирует выбранный контакт.
        /// </summary>
        private void EditContact()
        {
            AddEditForm addEditContactForm = new AddEditForm();
            Contact contact = ContactsProject.Contacts[ContactsListBox.SelectedIndex];
            addEditContactForm.CurrentContact = contact;
            var dialogResult = addEditContactForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                RecreateContactList(ContactsListBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Удаляет выбранный контакт.
        /// </summary>
        private void DeleteContact()
        {
            if (ContactsListBox.SelectedIndex >= 0)
            {
                var contactToRemove = ContactsProject.Contacts[ContactsListBox.SelectedIndex];

                // Добавление всплывающего окна с запросом подтверждения
                var confirmResult = MessageBox.Show(
                    $"Вы уверены, что хотите удалить контакт {contactToRemove.LastName}?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    ContactsProject.Contacts.Remove(contactToRemove);
                    RecreateContactList();
                    ProjectManager.SaveProject(ContactsProject);
                }
            }
        }

        /// <summary>
        /// Пересоздаёт лист со всеми контактами
        /// </summary>
        /// <param name="defaultSelectedIndex">номер контакта, который будет выделен после пересоздания</param>
        void RecreateContactList(int defaultSelectedIndex = 0)
        {
            string searchText = SearchTextBox.Text.ToLower();
            List<Contact> filteredContacts;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredContacts = ContactsProject.GetSortedContacts().ToList();
            }
            else
            {
                filteredContacts = ContactsProject.Contacts
                    .Where(contact => contact.FirstName.ToLower().Contains(searchText) ||
                                      contact.LastName.ToLower().Contains(searchText))
                    .OrderBy(contact => contact.LastName)
                    .ThenBy(contact => contact.FirstName)
                    .ToList();
            }

            ContactsListBox.Items.Clear();

            foreach (var contact in filteredContacts)
            {
                ContactsListBox.Items.Add(contact.FirstName + " " + contact.LastName);
            }

            if (ContactsListBox.Items.Count > 0)
            {
                int selectedIndex = defaultSelectedIndex < 0 || defaultSelectedIndex >= ContactsListBox.Items.Count ? 0 : defaultSelectedIndex;
                ContactsListBox.SelectedIndex = selectedIndex;
            }
            else
            {
                ContactsListBox.SelectedIndex = -1;
                ClearContactDetails();
            }
        }

        private void ClearContactDetails()
        {
            lastNameTextBox.Text = string.Empty;
            firstNameTextBox.Text = string.Empty;
            birthDateTimePicker.Value = DateTime.Today;
            emailTextBox.Text = string.Empty;
            VKTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Обработка кнопки "Добавить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void создатьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewContact();
        }

        /// <summary>
        /// Обработка события нажатия кнопки "Добавить контакт".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о событии.</param>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewContact();
        }

        /// <summary>
        /// Обработка кнопки "Редактировать контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void редактироватьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Обработка события нажатия кнопки "Редактировать контакт" на панели инструментов.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о событии.</param>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Обработка кнопки "Удалить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void удалитьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Обработка события нажатия кнопки "Удалить контакт" на панели инструментов.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о событии.</param>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Информация о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Изменение выделения текущего контакта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex != -1)
            {
                var displayedContacts = ContactsProject.GetSortedContacts();
                var selectedContact = displayedContacts[ContactsListBox.SelectedIndex];

                lastNameTextBox.Text = selectedContact.LastName;
                firstNameTextBox.Text = selectedContact.FirstName;
                birthDateTimePicker.Value = selectedContact.BirthDate;
                emailTextBox.Text = selectedContact.Email;
                VKTextBox.Text = selectedContact.ID_VK;
                phoneTextBox.Text = selectedContact.PhoneNumber.Phone;
            }
        }

        /// <summary>
        /// Сохранение всех контактов при выходе из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveProject(ContactsProject);
        }

        /// <summary>
        /// Обработка события нажатия кнопки "Выйти".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о событии.</param>
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработка события срабатывания таймера для скрытия панели с напоминанием о днях рождения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о событии.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            birthdayPanel.Visible = false;
            timer1.Stop();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e) => CreateContactList();
    }
}
