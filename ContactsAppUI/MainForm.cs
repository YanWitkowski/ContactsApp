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
        private Project _contactsProject;

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

            UpdateBirthdayList();
            CreateContactList();
        }

        /// <summary>
        /// Обновляет список контактов с днями рождения на текущую дату.
        /// </summary>
        private void UpdateBirthdayList()
        {
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
        }

        /// <summary>
        /// Создает список контактов, возможно с фильтрацией и сортировкой.
        /// </summary>
        /// <param name="selectSomething">Указывает, нужно ли выбрать элемент в списке.</param>
        /// <param name="selectedIndex">Индекс элемента, который нужно выбрать.</param>
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

            ContactsListBox.SelectedIndexChanged -= ContactsListBox_SelectedIndexChanged;
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

            ContactsListBox.SelectedIndexChanged += ContactsListBox_SelectedIndexChanged;
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
                ProjectManager.SaveProject(ContactsProject);
                RecreateContactList(contact.Id);
                UpdateBirthdayList();
            }
        }

        /// <summary>
        /// Редактирует выбранный контакт.
        /// </summary>
        private void EditContact()
        {
            if (ContactsListBox.SelectedItem is Contact selectedContact)
            {
                AddEditForm addEditContactForm = new AddEditForm();
                addEditContactForm.CurrentContact = selectedContact;
                var dialogResult = addEditContactForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    ProjectManager.SaveProject(ContactsProject);
                    RecreateContactList(selectedContact.Id);
                    UpdateBirthdayList();
                }
            }
            else
            {
                MessageBox.Show("Please select a valid contact to edit.");
            }
        }

        /// <summary>
        /// Удаляет выбранный контакт.
        /// </summary>
        private void DeleteContact()
        {
            if (ContactsListBox.SelectedItem is Contact contactToRemove)
            {
                var confirmResult = MessageBox.Show(
                    $"Вы уверены, что хотите удалить контакт {contactToRemove.LastName}?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    ContactsProject.Contacts.Remove(contactToRemove);
                    ProjectManager.SaveProject(ContactsProject);
                    RecreateContactList();
                    UpdateBirthdayList();
                }
            }
        }

        /// <summary>
        /// Пересоздаёт список всех контактов.
        /// </summary>
        /// <param name="selectedContactId">Id контакта, который будет выделен после пересоздания.</param>
        private void RecreateContactList(int selectedContactId = 0)
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

            ContactsListBox.SelectedIndexChanged -= ContactsListBox_SelectedIndexChanged;
            ContactsListBox.Items.Clear();
            int newIndex = -1;

            foreach (var contact in filteredContacts)
            {
                ContactsListBox.Items.Add(contact);
                if (contact.Id == selectedContactId)
                {
                    newIndex = ContactsListBox.Items.Count - 1;
                }
            }

            if (newIndex >= 0 && newIndex < ContactsListBox.Items.Count)
            {
                ContactsListBox.SelectedIndex = newIndex;
            }
            else
            {
                ContactsListBox.SelectedIndex = -1;
                ClearContactDetails();
            }

            ContactsListBox.SelectedIndexChanged += ContactsListBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Очищает детали контакта.
        /// </summary>
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
        /// Обработчик события нажатия кнопки "Создать контакт".
        /// </summary>
        private void создатьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewContact();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Добавить контакт" на панели инструментов.
        /// </summary>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewContact();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Редактировать контакт".
        /// </summary>
        private void редактироватьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Редактировать контакт" на панели инструментов.
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Удалить контакт".
        /// </summary>
        private void удалитьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Удалить контакт" на панели инструментов.
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Обработчик события отображения информации о программе.
        /// </summary>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Обработчик события изменения выбранного контакта в списке.
        /// </summary>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedItem is Contact selectedContact)
            {
                lastNameTextBox.Text = selectedContact.LastName;
                firstNameTextBox.Text = selectedContact.FirstName;
                birthDateTimePicker.Value = selectedContact.BirthDate;
                emailTextBox.Text = selectedContact.Email;
                VKTextBox.Text = selectedContact.ID_VK;
                phoneTextBox.Text = selectedContact.PhoneNumber.Phone;
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveProject(ContactsProject);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Выйти".
        /// </summary>
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события таймера для скрытия панели дней рождений.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            birthdayPanel.Visible = false;
            timer1.Stop();
        }

        /// <summary>
        /// Обработчик события изменения текста поиска контактов.
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, EventArgs e) => CreateContactList();
    }
}

