using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace DinkToPdf
{
  class Program
  {
    static void Main(string[] args)
    {
      var serviceProvider = ContainerConfiguration.Configure();
      serviceProvider.GetService<ContinuousRunningProcessor>().Process();
      var barCode = Barcode.Generate("12345678910");
      var qrCode = QrCode.Generate("http://google.com.br");
      var html = Html.Generate();
      Pdf.Generate();
      Console.WriteLine("Hello World!");
    }

    internal static class ContainerConfiguration
    {
      public static ServiceProvider Configure()
      {
        return new ServiceCollection()
            .AddLogging(l => l.AddConsole())
            .Configure<LoggerFilterOptions>(c => c.MinLevel = LogLevel.Trace)
            .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()))
            .BuildServiceProvider();
      }
    }
    static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools())));

  }
}
