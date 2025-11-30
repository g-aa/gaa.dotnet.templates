using Grpc.Core;

namespace Gaa.Project.Service.Grpc.Services;

/// <inheritdoc cref="Example" />
public sealed class ExampleService : Example.ExampleBase
{
    private readonly ILogger _log;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ExampleService"/>.
    /// </summary>
    /// <param name="log">Журнал протоколирования событий.</param>
    public ExampleService(ILogger<ExampleService> log)
    {
        _log = log;
    }

    /// <inheritdoc />
    public override Task<ResponseMessage> Send(
        RequestMessage request,
        ServerCallContext context)
    {
        _log.LogInformation("Получено сообщение: {Text}.", request.Text);
        return Task.FromResult(new ResponseMessage
        {
            Text = "Ok!",
        });
    }
}