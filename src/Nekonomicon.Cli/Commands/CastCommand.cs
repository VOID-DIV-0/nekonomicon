using System.CommandLine;

namespace Nekonomicon.Cli.Commands;

public class CastCommand : Command
{
    public CastCommand() : base("cast", "Execute a nekonomicon script (.spell)")
    {
        var argument = new Argument<string>("FILE_PATH");
        Add(argument);
        
        SetAction((parseResult) =>
        {
            var spellPath = parseResult.GetRequiredValue(argument);
            Console.WriteLine($"Casting spell: {spellPath}");
            // TODO: Implement spell casting logic
        });
    }
}

