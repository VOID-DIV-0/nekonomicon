using Nekonomicon.Core.Models;

namespace Nekonomicon.Cli.Versioning;

public interface IVersionProvider
{
    string CurrentVersion { get; }
    ChangeLog GetChangeLog();
    string GetFunnyMessage();
}
