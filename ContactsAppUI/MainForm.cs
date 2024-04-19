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
using ContactsApp;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ContactsAppUI
{
    public partial class MainForm : Form
    {
        private Contact _contact;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                _contact = new Contact("Петров", "Сергей",
                    new DateTime(1999, 12, 1), "petrovsergey1980@gmail.com",
                    "petrov1980", new PhoneNumber("+79189007866"));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }

            string filePath = Path.Combine(Environment.CurrentDirectory, "json.txt");
            JsonSerializerHelper jsonHelper = new JsonSerializerHelper();

            // Сериализация объекта
            jsonHelper.SerializeObject(_contact, filePath);

            // Десериализация объекта
            Contact contact = jsonHelper.DeserializeObject<Contact>(filePath);

            if (contact != null)
            {
                MessageBox.Show("Данные контакта: " + contact.ToString());
            }

            else
            {
                MessageBox.Show("Файл не найден!");
            }
        }
    }
}
