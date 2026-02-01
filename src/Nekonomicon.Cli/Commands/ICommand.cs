using Nekonomicon.Cli.Panics;

public interface ICommand
{
    Result<Ok, CliPanic> Handle(string[] args, IDictionary<string, object> modifiers);
}

public class Conjure : ICommand
{
    public Result<Ok, CliPanic> Handle(string[] args, IDictionary<string, object> modifiers)
    {
        return new Ok();
    }
}

public class Help : ICommand
{
  Result<Ok, CliPanic> Handle(string[] args, IDictionary<string, object> modifiers)
  {
    Console.WriteLine("Available commands:");
    Console.WriteLine("  conjure        | Command   | `ConjureHandler`       |");
    Console.WriteLine("  summon         | Command   | `SummonHandler`        |");
    Console.WriteLine("  unsummon       | Command   | `UnsummonHandler`      |");
    Console.WriteLine("  attune         | Command   | `AttuneHandler`        |");
    Console.WriteLine("  groom          | Command   | `GroomHandler`         |");
    Console.WriteLine("  brew           | Command   | `BrewHandler`          |");
    Console.WriteLine("  story          | Query     | `StoryHandler`         |");
    Console.WriteLine("  grimoire       | Query     | `GrimoireHandler`      |");
    Console.WriteLine("  help           | Query     | `HelpHandler`          |");

    return new Ok();
  }
}


// conjure`        | Command   | `ConjureHandler`       |
// | `summon`         | Command   | `SummonHandler`        |
// | `unsummon`       | Command   | `UnsummonHandler`      |
// | `attune`         | Command   | `AttuneHandler`        |
// | `groom`          | Command   | `GroomHandler`         |
// | `brew`           | Command   | `BrewHandler`          |
// | `story`          | Query     | `StoryHandler`         |
// | `grimoire`       | Query     | `GrimoireHandler`      |
// | `help
