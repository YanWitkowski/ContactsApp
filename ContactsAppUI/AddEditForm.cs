using ContactsApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ContactsAppUI
{
    /// <summary>
    /// Форма, позволяющая добавлять новые и редактировать текущие контакты
    /// </summary>
    public partial class AddEditForm : Form
    {
        Contact _currentContact;
        /// <summary>
        /// Редактируемый/добавляемый контакт
        /// </summary>
        public Contact CurrentContact { get => _currentContact; set => _currentContact = value; }
        public AddEditForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                string lastName = lastNameTextBox.Text;
                string firstName = firstNameTextBox.Text;
                DateTime birthDate = birthDateTimePicker.Value;
                string email = emailTextBox.Text;
                string idVK = VKTextBox.Text;
                PhoneNumber phoneNumber = new PhoneNumber(phoneTextBox.Text);
                if (CurrentContact == null)
                {
                    CurrentContact = new Contact(lastName, firstName,  birthDate, email, idVK, phoneNumber);
                }
                else
                {
                    CurrentContact.LastName = lastName;
                    CurrentContact.FirstName = firstName;
                    CurrentContact.Email = email;
                    CurrentContact.ID_VK = idVK;
                    CurrentContact.BirthDate = birthDate;
                    CurrentContact.PhoneNumber = phoneNumber;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Обработчик события появления этой формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditForm_Shown(object sender, EventArgs e)
        {
            if (CurrentContact != null)
            {
                lastNameTextBox.Text = CurrentContact.LastName;
                firstNameTextBox.Text = CurrentContact.FirstName;
                emailTextBox.Text = CurrentContact.Email;
                VKTextBox.Text = CurrentContact.ID_VK;
                birthDateTimePicker.Value = CurrentContact.BirthDate;
                phoneTextBox.Text = CurrentContact.PhoneNumber.Phone.ToString();
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
