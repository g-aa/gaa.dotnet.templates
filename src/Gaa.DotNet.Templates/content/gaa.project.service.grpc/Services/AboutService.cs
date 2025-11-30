using System.Diagnostics;

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Gaa.Project.Service.Grpc.Services;

/// <inheritdoc cref="About"/>
public sealed class AboutService : About.AboutBase
{
    /// <inheritdoc />
    public override Task<VersionResponse> Version(
        Empty request,
        ServerCallContext context)
    {
        var assembly = typeof(AboutService).Assembly;
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        return Task.FromResult(new VersionResponse
        {
            Version = assembly.GetName().Version?.ToString(3),
            FileVersion = fvi.FileVersion,
            ProductVersion = fvi.ProductVersion,
        });
    }
}