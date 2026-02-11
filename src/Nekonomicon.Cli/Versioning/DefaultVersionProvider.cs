using Nekonomicon.Core.Models;

namespace Nekonomicon.Cli.Versioning;

public class DefaultVersionProvider : IVersionProvider
{
    private static readonly List<string> FunnyMessages = new()
    {
        "Spellcasting has never been better at {Version}, but you may still cast insects.",
        "This magic tome is getting old! Now behold its {Version} enhanced powers!",
        "Nekonomicon has evolved to {Version}. Prepare to purr!",
    };
 
    public string CurrentVersion => "1.5.0";

    public ChangeLog GetChangeLog()
    {
        return new ChangeLog
        {
            Entries = new List<CoreVersion>
            {
                new CoreVersion
                {
                    Timestamp = new DateTime(2026, 02, 01),
                    Action = "Release",
                    Details = "v1.5.0 - Interactive REPL mode with pipeline support"
                },
                new CoreVersion
                {
                    Timestamp = new DateTime(2026, 01, 15),
                    Action = "Feature",
                    Details = "Added rich error formatting with educative, pretty, minimal, and JSON modes"
                },
                new CoreVersion
                {
                    Timestamp = new DateTime(2026, 01, 10),
                    Action = "Feature",
                    Details = "Implemented CommandRouter with dependency injection for CLI commands"
                },
                new CoreVersion
                {
                    Timestamp = new DateTime(2026, 01, 01),
                    Action = "Feature",
                    Details = "Initial CLI scaffold with run, install, uninstall, format, groom, version, list, help commands"
                },
            }
        };
    }

    public string GetFunnyMessage()
    {
        var random = new Random();
        return FunnyMessages[random.Next(FunnyMessages.Count)];
    }
}
