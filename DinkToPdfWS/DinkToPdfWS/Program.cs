using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace DinkToPdfWS
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
              services.AddHostedService<Worker>();
              services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            });
  }
}
