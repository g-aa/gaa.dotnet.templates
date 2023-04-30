namespace Gaa.Library
{
    /// <summary>
    /// Пример класса.
    /// </summary>
    public sealed class ClassExample
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ClassExample"/>.
        /// </summary>
        public ClassExample()
        {
            this.Message = "Test message.";
        }
    }
}