using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp;
using NUnit.Framework;
using ContactsApp;
using NUnit.Framework.Legacy;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс модельных тестов номера телефона
    /// </summary>
    public class PhoneNumberTest
    {
        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        PhoneNumber _phone;

        /// <summary>
        /// Создает новый контакт.
        /// </summary>
        [SetUp]
        public void CreateContact()
        {
            _phone = new PhoneNumber("+77777777777");
        }

        /// <summary>
        /// Присвоение номера телефона не начинающегося с 7 должно вызывать исключение ArgumentException.
        /// </summary>
        /// <param name="wrongPhone">Неправильный номер телефона.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        [TestCase("+91231231223", "Должно возникать исключение, если номер начинается не с +7",
            TestName = "Присвоение номера не начинающегося с +7")]

        /// <summary>
        /// Присвоение короткого номера телефона должно вызывать исключение ArgumentException.
        /// </summary>
        /// <param name="wrongPhone">Неправильный номер телефона.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        [TestCase("+7444555", "Должно возникать исключение, номер не 12 символов длиной",
            TestName = "Присвоение короткого номера телефона")]
        public void TestPhoneNumberSet_ArgumentException(string wrongPhone, string message)
        {
            Assert.Throws<ArgumentException>(
            () => { _phone.Phone = wrongPhone; },
            message);
        }

        /// <summary>
        /// Присвоение верного номера телефона не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного номера")]
        public void TestSurnameSet_CorrectString()
        {
            var expected = ("+77777777778");
            Assert.DoesNotThrow(
            () => { _phone.Phone = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер Phone должен возвращать правильный номер телефона.
        /// </summary>
        [Test(Description = "Получение верного номера")]
        public void TestPhoneNumberGet_CorrectString()
        {
            var expected = ("+77777777778");

            _phone.Phone = expected;
            var actual = _phone.Phone;
            ClassicAssert.AreEqual(expected, actual, "Геттер Phone возвращает неправильный номер");
        }
    }
}
