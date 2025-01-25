namespace Gaa.Project.Console;

/// <summary>
/// Базовый класс приложения.
/// </summary>
public static class Program
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы для запуска приложения.</param>
    public static void Main(string[] args)
    {
        Console.WriteLine("Application started!");

        /*
         * Логика приложения...
         */

        Console.WriteLine("Press any key to exit...");
        Console.Read();
    }
}