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

namespace ContactsAppUI
{
    public partial class MainForm : Form
    {
        //private Contact _contact;
        private List<Contact> contacts = new List<Contact>();

        public MainForm()
        {
                InitializeComponent();
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        void birthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void VKTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void phoneTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string lastName = lastNameTextBox.Text;
                string firstName = firstNameTextBox.Text;
                DateTime birthDate = birthDateTimePicker.Value;
                string email = emailTextBox.Text;
                string idVk = VKTextBox.Text;
                PhoneNumber phoneNumber = new PhoneNumber(phoneTextBox.Text);
                Contact contact = new Contact(lastName, firstName, birthDate, email, idVk, phoneNumber);

                contacts.Add(contact);

                Project project = new Project(contacts);

                ProjectManager.SaveProject(project);
                MessageBox.Show("Сохранено");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Project project = ProjectManager.LoadProject();

            if (project.Contacts.Count > 0)
            {
                var contact = project.Contacts[0];

                firstNameTextBox.Text = contact.FirstName;
                lastNameTextBox.Text = contact.LastName;
                emailTextBox.Text = contact.Email;
                VKTextBox.Text = contact.ID_VK;
                birthDateTimePicker.Value = contact.BirthDate;
                phoneTextBox.Text = contact.PhoneNumber.Phone;
            }
            else
            {
                MessageBox.Show("Контакты не найдены.", "Ошибка");
            }
        }
    }
}
