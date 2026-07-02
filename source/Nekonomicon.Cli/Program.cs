using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

using Nekonomicon.Cli.Commands;
using Nekonomicon.Core.Runtime;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<ScriptEngine>();
        services.AddSingleton<CastCommand>();
        services.AddSingleton<VersionCommand>();
    })
    .Build();

var serviceProvider = host.Services;

// Create commands with DI-resolved dependencies
var rootCommand = new RootCommand("Nekonomicon CLI - The neko interpreter");

foreach (var command in serviceProvider.GetServices<Command>())
{
  rootCommand.Add(command);
}

// Disable the built-in --version option (use 'neko version' instead)
rootCommand.Options.Clear();

try
{
    return await rootCommand.InvokeAsync(args);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Fatal Error: {ex.Message}, exiting.");
    return 1;
}
