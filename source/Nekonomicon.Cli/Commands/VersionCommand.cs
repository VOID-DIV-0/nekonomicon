using System.CommandLine;

namespace Nekonomicon.Cli.Commands;

public class VersionCommand : Command
{
    private const string Version = "v0.2.0";

    public VersionCommand() : base("version", "Displays the current version of Nekonomicon CLI")
    {
        var simpleOption = new Option<bool>("--simple")
        {
            Description = "Print only the version number without text (e.g. v1.5.0)"
        };
        
        var historyOption = new Option<bool>("--history")
        {
            Description = "Print the full changelog history"
        };

        Add(simpleOption);
        Add(historyOption);

        SetAction((parseResult) =>
        {
            var simple = parseResult.GetRequiredValue(simpleOption);
            var history = parseResult.GetRequiredValue(historyOption);

            if (history)
            {
                // TODO: Implement changelog history display
                Console.WriteLine("Changelog history not yet implemented.");
                return;
            }

            if (simple)
            {
                Console.WriteLine(Version);
            }
            else
            {
                Console.WriteLine($"Nekonomicon CLI {Version}");
            }
        });
    }
}

