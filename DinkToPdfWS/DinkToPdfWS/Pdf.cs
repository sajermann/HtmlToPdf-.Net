using System;
using System.Collections.Generic;
using System.Text;
using WkHtmlToPdfDotNet;

namespace DinkToPdf
{
  public static class Pdf
  {
    public static void Generate()
    {
      var converter = new SynchronizedConverter(new PdfTools());
      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
          ColorMode = ColorMode.Color,
          Orientation = Orientation.Portrait,
          PaperSize = PaperKind.A4,
          Margins = new MarginSettings() { Top = 10 },
          Out = $"‪C:\\Users\\b.sajermann.da.silva\\Desktop\\test.pdf",
          },
        Objects = {
          new ObjectSettings()
          {
              //Page = "http://localhost:44392/Home/",
              HtmlContent = @"<b>Lorem</b><div style='display:none'>bruno</div> ",
              
          },
        }
      };

      converter.Convert(doc);

      //var pdf = converter.Convert(doc);
      //return File(pdf, "application/pdf");
    }
  }
}
