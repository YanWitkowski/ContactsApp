using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ContactsApp 
{
    /// <summary>
    /// Класс, хранящий информацию о контакте
    /// </summary>
    public class Contact : ICloneable
    {
        private string _lastName;
        private string _firstName;
        private DateTime _birthDate;
        private string _email;
        private string _idVk;
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
        /// <param name="value">Строка для проверки</param>
        /// <param name="maxLength">Максимальная длина строки</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        private void CheckMaxLength(string value, int maxLength, string errorMessage)
        {
            //if (maxLength == 0)
            //{
            //    throw new ArgumentException("Максимальная длина строки не может быть нулевой.");
            //}

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
        /// Метод для проверки кириллических символов.
        /// </summary>
        /// <param name="input">Строка для проверки</param>
        private bool ContainsCyrillic(string input)
        {
            Regex regex = new Regex(@"\p{IsCyrillic}");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Метод для проверки на допустимые символы для e-mail.
        /// </summary>
        /// <param name="email">e-mail для проверки</param>
        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$");
            return regex.IsMatch(email);
        }

        /// <summary>
        /// Возвращает и задает фамилию.
        /// </summary>
        public string LastName
        {
            get => _lastName; 
            set
            {
                // Проверка на пустое значение
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Фамилия не может быть пустой");
                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (value != null)
                    {
                        CheckMaxLength(value, 50, "Фамилия не должна превышать 50 символов");
                        foreach (char c in value)
                        {
                            if (!char.IsLetter(c)) // Проверка, что символ не является буквой
                            {
                                throw new ArgumentException("Фамилия должна содержать только буквы");
                            }
                        }
                    }

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
                // Проверка на пустое значение
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым");
                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (value != null)
                    {
                        CheckMaxLength(value, 50, "Имя не должно превышать 50 символов");
                        foreach (char c in value)
                        {
                            if (!char.IsLetter(c)) // Проверка, что символ не является буквой
                            {
                                throw new ArgumentException("Имя должно содержать только буквы");
                            }
                        }
                    }

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
                // Проверка на пустое значение
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email не может быть пустым");
                }

                if (value != null)
                {
                    CheckMaxLength(value, 50, "E-mail не должен превышать 50 символов");
                    if (ContainsCyrillic(value))
                    {
                        throw new ArgumentException("Email не может содержать кириллические символы");
                    }
                }

                if (IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Введите корректный Email");
                }
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
                try
                {
                    // Проверка на пустое значение
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("ID-Вконтакте не может быть пустым");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
                    if (value != null)
                    {
                        CheckMaxLength(value, 15, "ID-Вконтакте не должен превышать 15 символов");
                        if (ContainsCyrillic(value))
                        {
                            throw new ArgumentException("ID-Вконтакте не может содержать кириллические символы");
                        }
                    }

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

        /// <summary>
        /// Возвращает строку, представляющую текущий объект.
        /// </summary>
        /// <returns>Строка с именем контакта.</returns>
        public override string ToString()
        {
            return $"{LastName}";
        }
    }
}
