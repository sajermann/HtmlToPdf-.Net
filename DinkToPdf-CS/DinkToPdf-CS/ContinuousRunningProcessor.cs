using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DinkToPdf
{
  internal class ContinuousRunningProcessor
  {
    private readonly ILogger<ContinuousRunningProcessor> logger;

    public ContinuousRunningProcessor(ILogger<ContinuousRunningProcessor> logger)
    {
      this.logger = logger;
    }

    public void Process()
    {
      var count = 0;

      Task.Factory.StartNew(() =>
      {
        logger.LogInformation("Process started!");
      });

      Console.WriteLine("Press Ctrl + C to cancel!");
      Console.CancelKeyPress += ((s, a) =>
      {
        Console.WriteLine("Bye!");

      });

     
    }
  }
}
