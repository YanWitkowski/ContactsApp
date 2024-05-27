using System;
using System.Collections.Generic;
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
    /// Контактная информация.
    /// </summary>
    public class ContactTest
    {
        /// <summary>
        /// Контакт, чья информация тестируется.
        /// </summary>
        private Contact _contact = null;

        /// <summary>
        /// Создает новый контакт.
        /// </summary>
        [SetUp]
        public void CreateContact()
        {
            _contact = new Contact("Витковски", "Ян", DateTime.Now, "vitkovskijan1103@gmail.com", "vitjan11", new PhoneNumber("+77445885655"));
        }

        /// <summary>
        /// Присвоение фамилии, содержащей цифры, должно вызывать исключение ArgumentException.
        /// </summary>
        [Test(Description = "Цифры в фамилии")]
        public void TestLastNameSet_Number()
        {
            var wrongLastName = "2134433";
            Assert.Throws<ArgumentException>(
            () => { _contact.LastName = wrongLastName; },
            "Должно возникать исключение, если фамилия имеет цифры");
        }

        /// <summary>
        /// Присвоение фамилии, длина которой больше 50 символов, должно вызывать исключение ArgumentException.
        /// </summary>
        [Test(Description = "Присвоение строки длиннее 50 символов в качестве фамилии")]
        public void TestLastNameSet_LongString()
        {
            var wrongLastName = "kdcvnslvndlnvjlgsigeisldkvnnnlsdkdfgggggggvvvvvvvvvggggggggggvnskg";
            Assert.Throws<ArgumentException>(
            () => { _contact.LastName = wrongLastName; },
            "Должно возникать исключение, если фамилия длиннее 50");
        }

        /// <summary>
        /// Присвоение корректной фамилии не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верной фамилии")]
        public void TestLastNameSet_CorrectString()
        {
            var expected = "Иванов";
            Assert.DoesNotThrow(
            () => { _contact.LastName = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер LastName должен возвращать правильную фамилию.
        /// </summary>
        [Test(Description = "Получение верной фамилии")]
        public void TestLastNameGet_CorrectString()
        {
            var expected = "Иванов";
            _contact.LastName = expected;
            var actual = _contact.LastName;
            ClassicAssert.AreEqual(expected, actual, "Геттер LastName возвращает неправильную фамилию");
        }

        /// <summary>
        /// Присвоение имени, длина которого больше 50 символов, должно вызывать исключение ArgumentException.
        /// </summary>
        [Test(Description = "Присвоение строки длиннее 50 символов в качестве имени")]
        public void TestFirstNameSet_LongString()
        {
            var wrongFirstName = "seeeeeeeeeeeeeeeeeefffffffsssssssssddddddddddddgvggrrrrrrrrrrrrrrrrrh";
            Assert.Throws<ArgumentException>(
            () => { _contact.FirstName = wrongFirstName; },
            "Должно возникать исключение, если имя длиннее 50");
        }

        /// <summary>
        /// Присвоение имени, содержащего цифры, должно вызывать исключение ArgumentException.
        /// </summary>
        [Test(Description = "Цифры в имени")]
        public void TestFirstNameSet_Number()
        {
            var wrongLastName = "2sd134433";
            Assert.Throws<ArgumentException>(
            () => { _contact.LastName = wrongLastName; },
            "Должно возникать исключение, если имя имеет цифры");
        }

        /// <summary>
        /// Присвоение корректного имени не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного имени")]
        public void TestFirstNameSet_CorrectString()
        {
            var expected = "Иван";
            Assert.DoesNotThrow(
            () => { _contact.FirstName = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер FirstName должен возвращать правильное имя.
        /// </summary>
        [Test(Description = "Получение верного имени")]
        public void TestFirstNameGet_CorrectString()
        {
            var expected = "Иван";
            _contact.FirstName = expected;
            var actual = _contact.FirstName;
            ClassicAssert.AreEqual(expected, actual, "Геттер Name возвращает неправильное имя");
        }

        /// <summary>
        /// Присвоение Email, введенного некорректно, должно вызывать исключение ArgumentException.
        /// </summary>
        /// <param name="wrongEmail">Некорректно введенный Email.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        [TestCase("aaaa", "Должно возникать исключение, если Email введен неккоректно",
            TestName = "Присвоение Email неправильного значения")]

        /// <summary>
        /// Присвоение Email, длина которого больше 50 символов, должно вызывать исключение ArgumentException.
        /// </summary>
        /// <param name="wrongEmail">Email, длина которого больше 50 символов.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        [TestCase("jvcd3ssssssssssssssssssssssssssssssssssssssssssssssssssssssssfs@sc.af", "Должно возникать исключение, если Email более 50 символов длинной",
            TestName = "Присвоение длинного Email")]

        /// <summary>
        /// Присвоение Email, содержащего кириллические символы, должно вызывать исключение ArgumentException.
        /// </summary>
        /// <param name="wrongEmail">Email, содержащий кириллические символы.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        [TestCase("вьств@а.уыа", "Должно возникать исключение, если Email содержит кириллицу",
            TestName = "Присвоение кириллических символов в Email")]
        public void TestEmailSet_ArgumentException(string wrongEmail, string message)
        {
            Assert.Throws<ArgumentException>(
            () => { _contact.Email = wrongEmail; },
            message);
        }

        /// <summary>
        /// Присвоение верного Email не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного Email")]
        public void TestEmailSet_CorrectString()
        {
            var expected = "aa@mail.ru";
            Assert.DoesNotThrow(
            () => { _contact.Email = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер Email должен возвращать верное значение.
        /// </summary>
        [Test(Description = "Получение верного Email")]
        public void TestEmailGet_CorrectString()
        {
            var expected = "aa@mail.ru";
            _contact.Email = expected;
            var actual = _contact.Email;
            ClassicAssert.AreEqual(expected, actual, "Геттер Email возвращает неправильное имя");
        }

        /// <summary>
        /// Присваивание VK_ID содержащего кириллицу должно вызывать исключение.
        /// </summary>
        [Test(Description = "Ввод кириллицы")]
        public void TestIdVKSet_Cyrillic()
        {
            var wrongIdVK = "дтмовдтмоли";
            Assert.Throws<ArgumentException>(
            () => { _contact.ID_VK = wrongIdVK; },
            "Должно возникать исключение, если символы кириллические");
        }

        /// <summary>
        /// Присваивание VKID длиннее 15 символов должно вызывать исключение.
        /// </summary>
        [Test(Description = "Присвоение строки длиннее 15 символов в качестве VK_ID")]
        public void TestIdVKSet_LongString()
        {
            var wrongIdVK = "111111111111111111111111";
            Assert.Throws<ArgumentException>(
            () => { _contact.ID_VK = wrongIdVK; },
            "Должно возникать исключение, если имя длиннее 15");
        }

        /// <summary>
        /// Присвоение верного VK_ID не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного VK_ID")]
        public void TestIdVKSet_CorrectString()
        {
            var expected = "123456";
            Assert.DoesNotThrow(
            () => { _contact.ID_VK = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер ID_VK должен возвращать верное значение.
        /// </summary>
        [Test(Description = "Получение верного VK_ID")]
        public void TestIdVKGet_CorrectString()
        {
            var expected = "123456";
            _contact.ID_VK = expected;
            var actual = _contact.ID_VK;
            ClassicAssert.AreEqual(expected, actual, "Геттер ID_VK возвращает неправильное имя");
        }

        /// <summary>
        /// Присваивание birthDate раньше 1900 года должно вызывать исключение.
        /// </summary>
        [Test(Description = "Присвоение даты рождения раньше 1900 года")]
        public void TestBirthDateSet_LongString()
        {
            var wrongBirthDate = DateTime.Parse("1600.01.01");
            Assert.Throws<ArgumentException>(
            () => { _contact.BirthDate = wrongBirthDate; },
            "Должно возникать исключение, если дата раньше 1900 года");
        }

        /// <summary>
        /// Присвоение верной даты рождения не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верной даты рождения")]
        public void TestBirthDateSet_CorrectString()
        {
            var expected = DateTime.Now;
            Assert.DoesNotThrow(
            () => { _contact.BirthDate = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер BirthDate должен возвращать верное значение.
        /// </summary>
        [Test(Description = "Получение верной даты рождения")]
        public void TestBirthDateGet_CorrectString()
        {
            var expected = DateTime.Now;
            _contact.BirthDate = expected;
            var actual = _contact.BirthDate;
            ClassicAssert.AreEqual(expected, actual, "Геттер BirthDate возвращает неправильную дату");
        }

        /// <summary>
        /// Присвоение верного номера телефона не должно вызывать исключения.
        /// </summary>
        [Test(Description = "Присвоение верного номера телефона")]
        public void TestContactNumberSet_CorrectString()
        {
            var expected = new PhoneNumber("+77777777777");
            Assert.DoesNotThrow(
            () => { _contact.PhoneNumber = expected; },
            "Не должно возникать исключения");
        }

        /// <summary>
        /// Геттер ContactNumber должен возвращать верное значение.
        /// </summary>
        [Test(Description = "Получение верного номера телефона")]
        public void TestContactNumberGet_CorrectString()
        {
            var expected = new PhoneNumber("+77777777777");
            _contact.PhoneNumber = expected;
            var actual = _contact.PhoneNumber;
            ClassicAssert.AreEqual(expected, actual, "Геттер ContactNumber возвращает неправильный номер");
        }
    }
}
