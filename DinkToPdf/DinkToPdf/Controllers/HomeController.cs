using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using System.Drawing;
using System.IO;
using WkHtmlToPdfDotNet;
using static QRCoder.PayloadGenerator;

namespace DinkToPdf.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      #region Gerando Código Barras
      BarcodeLib.Barcode b = new BarcodeLib.Barcode();
      Image img = b.Encode(BarcodeLib.TYPE.UPCA, "77777777977", Color.Black, Color.White, 290, 120);
      img.Save($"{Directory.GetCurrentDirectory()}\\wwwroot\\img\\img.bmp");
      #endregion

      #region Gerando QRCode
      Url generator = new Url("https://google.com.br");
      string payload = generator.ToString();

      QRCodeGenerator qrGenerator = new QRCodeGenerator();
      QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
      QRCode qrCode = new QRCode(qrCodeData);
      Bitmap qrCodeImage = qrCode.GetGraphic(5);
      qrCodeImage.Save($"{Directory.GetCurrentDirectory()}\\wwwroot\\img\\qrcode.bmp");
      #endregion

      return View();
    }

    public void Bruno()
    {

      var converter = new SynchronizedConverter(new PdfTools());
      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10 },
                //Out = @"C:\WkHtmlToPdf-DotNet\src\TestThreadSafe\test.pdf",
                Out = $"{Directory.GetCurrentDirectory()}\\wwwroot\\img\\test.pdf",
                },
        Objects = {
                    new ObjectSettings()
                    {
                        //Page = "http://google.com/",
                        Page = "https://localhost:44392/Home/",
                    },
                }
      };

      converter.Convert(doc);
      RedirectToAction("Index");
    }

  }
}
