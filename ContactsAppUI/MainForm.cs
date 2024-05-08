using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        }

        /// <summary>
        /// Пересоздаёт лист со всеми контактами
        /// </summary>
        /// <param name="defaultSelectedIndex">номер контакта, который будет выделен после пересоздания</param>
        void RecreateContactList(int defaultSelectedIndex = 0)
        {
            ContactsListBox.Items.Clear(); // Очистка списка перед добавлением новых элементов

            foreach (var contact in ContactsProject.Contacts)
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
            }
        }

        /// <summary>
        /// Обработка кнопки "Добавить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void создатьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// Обработка кнопки "Редактировать контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void редактироватьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// Обработка кнопки "Удалить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void удалитьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex >= 0)
            {
                var contactToRemove = ContactsProject.Contacts[ContactsListBox.SelectedIndex];
                ContactsProject.Contacts.Remove(contactToRemove);
                RecreateContactList();
                ProjectManager.SaveProject(ContactsProject);
            }
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
            var contact = ContactsProject.Contacts[ContactsListBox.SelectedIndex];

            lastNameTextBox.Text = contact.LastName;
            firstNameTextBox.Text = contact.FirstName;
            birthDateTimePicker.Text = contact.BirthDate.ToString("d");
            emailTextBox.Text = contact.Email;
            VKTextBox.Text = contact.ID_VK;
            phoneTextBox.Text = contact.PhoneNumber.Phone;
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

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
