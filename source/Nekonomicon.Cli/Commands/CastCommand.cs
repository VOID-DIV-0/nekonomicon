using System.CommandLine;

namespace Nekonomicon.Cli.Commands;

public class CastCommand : Command
{
    public CastCommand() : base("cast", "Execute a nekonomicon script (.spell)")
    {
        var fileArgument = new Argument<string>("file")
        {
            Arity = ArgumentArity.ExactlyOne,
            Description = "Path to the .spell file to cast"
        };

        var inlineOption = new Option<string>("--inline")
        {
            Description = "Indicates that the spell script is provided directly as a string argument instead of a file path"
        };


        Add(fileArgument);
        Add(inlineOption);
        
        SetAction((parseResult) =>
        {
            var spellPath = parseResult.GetRequiredValue(fileArgument);
            var inlineScript = parseResult.GetRequiredValue(inlineOption);
            if (!string.IsNullOrEmpty(inlineScript))
            {
                Console.WriteLine($"Casting inline spell: {inlineScript}");
                // TODO: Implement inline spell casting logic
            }
            else
            {
                Console.WriteLine($"Casting spell: {spellPath}");
                // TODO: Implement spell casting logic
            }
        });
    }
}

