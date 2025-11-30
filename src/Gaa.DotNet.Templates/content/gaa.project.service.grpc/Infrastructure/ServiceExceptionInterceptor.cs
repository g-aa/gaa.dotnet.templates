using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Gaa.Project.Service.Grpc.Infrastructure;

/// <summary>
/// Обработчики исключений.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class ServiceExceptionInterceptor : Interceptor
{
    private readonly ILogger _log;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ServiceExceptionInterceptor"/>.
    /// </summary>
    /// <param name="log">Журнал протоколирования событий.</param>
    public ServiceExceptionInterceptor(ILogger<ServiceExceptionInterceptor> log)
    {
        _log = log;
    }

    /// <inheritdoc />
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await base.UnaryServerHandler(request, context, continuation);
        }
        catch (ValidationException vEx)
        {
            const string vMsg = "Сработала ошибка при валидации параметров!";
            _log.LogWarning(vEx, vMsg);
            throw new RpcException(new Status(StatusCode.InvalidArgument, vMsg, vEx));
        }
        catch (InvalidOperationException opEx)
        {
            const string opMsg = "Сработала ошибка в процессе выполнения цепочки операций!";
            _log.LogError(opEx, opMsg);
            throw new RpcException(new Status(StatusCode.FailedPrecondition, opMsg, opEx));
        }
        catch (Exception ex)
        {
            const string msg = "Сработало не обрабатываемое исключение!";
            _log.LogCritical(ex, msg);
            throw new RpcException(new Status(StatusCode.Unknown, msg, ex));
        }
    }
}