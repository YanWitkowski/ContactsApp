using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;

namespace ContactsApp 
{
    /// <summary>
    /// Класс, хранящий информацию о контакте
    /// </summary>
    public class Contact : ICloneable
    {
        string _lastName;
        string _firstName;
        DateTime _birthDate;
        string _email;
        string _idVk;
        PhoneNumber _phoneNumber;

        /// <summary>
        /// Конструктор класса Contact
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="birthDate">Дата рождения</param>
        /// <param name="email">E-mail</param>
        /// <param name="idVk">ID Вконтакте</param>
        /// <param name="phoneNumber">Номер телефона</param>
        public Contact(string lastName, string firstName, DateTime birthDate, string email, string idVk, PhoneNumber phoneNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            Email = email;
            ID_VK = idVk;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Метод для проверки количества символов.
        /// </summary>
        ///  /// <param name="value">Строка для проверки</param>
        /// <param name="maxLength">Максимальная длина строки</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        private void CheckMaxLength(string value, int maxLength, string errorMessage)
        {
            if (value.Length <= maxLength)
            {
                return;
            }
            else
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Возвращает и задает фамилию.
        /// </summary>
        public string LastName
        {
            get => _lastName; 
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckMaxLength(value, 50, "Фамилия не должна превышать 50 символов");
                    _lastName = char.ToUpper(value[0]) + value.Substring(1);
                }
            }
        }

        /// <summary>
        /// Возвращает и задает имя.
        /// </summary>
        public string FirstName
        {
            get => _firstName; 
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckMaxLength(value, 50, "Имя не должно превышать 50 символов");
                    _firstName = char.ToUpper(value[0]) + value.Substring(1);
                }
            }
        }

        /// <summary>
        /// Возвращает и задает дату рождения.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime currentDate = DateTime.Now;
                DateTime minDate = new DateTime(1900, 1, 1);

                if (value <= currentDate && value >= minDate)
                {
                    _birthDate = value;
                }
                else
                {
                    throw new ArgumentException("Дата рождения должна быть не ранее 1900 года и не позднее текущей даты");
                }
            }
        }

        /// <summary>
        /// Возвращает и задает e-mail.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                CheckMaxLength(value, 50, "E-mail не должен превышать 50 символов");
                _email = value;
            }
        }

        /// <summary>
        /// Возвращает и задает ID-Вконтакте.
        /// </summary>
        public string ID_VK
        {
            get => _idVk; 
            set
            {
                CheckMaxLength(value, 15, "ID-Вконтакте не должен превышать 15 символов");
                _idVk = value;
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона.
        /// </summary>
        public PhoneNumber PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _phoneNumber = value;
                }
            }
        }

        /// <summary>
        /// Метод клонирования объекта Contact
        /// </summary>
        /// <returns>Клон объекта Contact</returns>
        public object Clone()
        {
                return new Contact(LastName, FirstName, BirthDate, Email, ID_VK, PhoneNumber);
        }
    }
}
