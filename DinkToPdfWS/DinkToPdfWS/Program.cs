using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace DinkToPdfWS
{
  public class Program
  {
    [STAThread]
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
              services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
              services.AddHostedService<Worker>();
            });
  }
}
