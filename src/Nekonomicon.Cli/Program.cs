using Nekonomicon.Core.Runtime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Nekonomicon.Cli;

class Program
{
  static int Main(string[] args)
  {
    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            services.AddTransient<ICommandHandler, Conjure>();
            services.AddTransient<ICommandHandler, Help>();
            // Register other adapters/services as needed
        })
        .Build();

    var adapter = host.Services.GetRequiredService<IArgumentAdapter>();
    var engine = host.Services.GetRequiredService<IScriptEngine>();
  }
}
