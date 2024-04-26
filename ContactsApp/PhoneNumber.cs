using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp 
{
    /// <summary>
    /// Класс, хранящий информацию о номере телефона.
    /// </summary>
    public class PhoneNumber 
    {
        private string _phone;

        /// <summary>
        /// Конструктор класса PhoneNumber.
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        public PhoneNumber(string phone)
        {
            Phone = phone;
        }

        /// <summary>
        /// Возвращает и задает номер телефона.
        /// </summary>
        public string Phone
        {
            get => _phone; 
            set
            {
                if (value.StartsWith("+7") && value.Length == 12 && value.Substring(1).All(char.IsDigit))
                {
                    _phone = value;
                }
                else
                {
                    throw new ArgumentException("Телефон должен начинаться с +7 и содержать ровно 11 цифр");
                }
            }
        }

    }
    
}
