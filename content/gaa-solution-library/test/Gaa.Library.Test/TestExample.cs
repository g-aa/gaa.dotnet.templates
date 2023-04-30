using NUnit.Framework;

namespace Gaa.Library.Test
{
    /// <summary>
    /// Набор тестов для <see cref="ClassExample"/>.
    /// </summary>
    [TestFixture]
    public sealed class TestExample
    {
        private ClassExample example;

        /// <summary>
        /// Настройка класса.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            this.example = new ClassExample();
        }

        /// <summary>
        /// Положительные тесты для <see cref="ClassExample.Message"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        [Test]
        [TestCase("Test message.")]
        public void MessagePositiveTest(string message)
        {
            Assert.That(this.example.Message, Is.EqualTo(message));
        }

        /// <summary>
        /// Отрицательные тесты для <see cref="ClassExample.Message"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        [Test]
        [TestCase("QWERTY")]
        public void MessageNegativeTest(string message)
        {
            Assert.That(this.example.Message, Is.Not.EqualTo(message));
        }
    }
}