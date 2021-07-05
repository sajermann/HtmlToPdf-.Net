using DinkToPdf;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DinkToPdfWS
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

      var barCode = Barcode.Generate("12345678910");
      var qrCode = QrCode.Generate("http://google.com.br");
      var html = Html.Generate();
      Pdf.Generate();
      Console.WriteLine("Hello World!");
      Console.ReadKey();
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}
