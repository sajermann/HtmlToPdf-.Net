using DinkToPdf.Models;
using DinkToPdf_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WkHtmlToPdfDotNet;
using static QRCoder.PayloadGenerator;

namespace DinkToPdf_2._0.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      #region Gerando Código Barras
      BarcodeLib.Barcode b = new BarcodeLib.Barcode();
      Image codeBar = b.Encode(BarcodeLib.TYPE.UPCA, "77777777977", Color.Black, Color.White, 290, 120);
      Bitmap codeBarImg = new Bitmap(codeBar);
      ViewBag.Codebar = BitmapToBytes(codeBarImg);
      //codeBarImg.Save($"{Directory.GetCurrentDirectory()}\\wwwroot\\img\\img.bmp");
      #endregion

      #region Gerando QRCode
      Url generator = new Url("https://google.com.br");
      string payload = generator.ToString();

      QRCodeGenerator qrGenerator = new QRCodeGenerator();
      QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
      QRCode qrCode = new QRCode(qrCodeData);
      Bitmap qrCodeImage = qrCode.GetGraphic(5);
      ViewBag.Qrcode = BitmapToBytes(qrCodeImage);
      #endregion

      #region Mexendo com XML
      //var meuXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\"><infNFe Id=\"NFe35210658328758001377550010000206711100009452\" versao=\"4.00\"><ide><cUF>35</cUF><cNF>10000945</cNF><natOp>OUTRA SAIDA DE MERC. OU PREST. SERV. NAO ESPECIFICADO</natOp><mod>55</mod><serie>1</serie><nNF>20671</nNF><dhEmi>2021-06-14T11:15:00-03:00</dhEmi><dhSaiEnt>2021-06-14T11:15:00-03:00</dhSaiEnt><tpNF>1</tpNF><idDest>1</idDest><cMunFG>3534401</cMunFG><tpImp>1</tpImp><tpEmis>1</tpEmis><cDV>2</cDV><tpAmb>1</tpAmb><finNFe>1</finNFe><indFinal>1</indFinal><indPres>9</indPres><procEmi>0</procEmi><verProc>12.1.027 | 3.0</verProc></ide><emit><CNPJ>58328758001377</CNPJ><xNome>TELELOK CENTRAL DE LOCACOES E COM.LTDA</xNome><xFant>OSASCO</xFant><enderEmit><xLgr>AVENIDA EDMUNDO AMARAL</xLgr><nro>91</nro><xBairro>PIRATININGA</xBairro><cMun>3534401</cMun><xMun>OSASCO</xMun><UF>SP</UF><CEP>06230150</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>551150777000</fone></enderEmit><IE>492998257110</IE><IM>0000130395</IM><CNAE>7729202</CNAE><CRT>3</CRT></emit><dest><CPF>39856373859</CPF><xNome>BRUNO SAJERMANN DA SILVA</xNome><enderDest><xLgr>AVENIDA ARMANDO SALLES DE OLIVEIRA - DE</xLgr><nro>1301/1302</nro><xCpl>BL05 AP28</xCpl><xBairro>CONJUNTO RESIDENCIAL IRAI </xBairro><cMun>3552502</cMun><xMun>SUZANO</xMun><UF>SP</UF><CEP>08673115</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>11975929454</fone></enderDest><indIEDest>2</indIEDest><email>bruno.sajermann@avanade.com</email></dest><entrega><CPF>39856373859</CPF><xNome>BRUNO SAJERMANN DA SILVA</xNome><xLgr>AVENIDA ARMANDO SALLES DE OLIVEIRA - DE 1301/1302 A 99998/</xLgr><nro>2160</nro><xCpl>BL05 AP28</xCpl><xBairro>CONJUNTO RESIDENCIAL IRAI </xBairro><cMun>3552502</cMun><xMun>SUZANO</xMun><UF>SP</UF><CEP>08673115</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>11975929454</fone><email>bruno.sajermann@avanade.com</email></entrega><det nItem=\"1\"><prod><cProd>KITERGC</cProd><cEAN>SEM GTIN</cEAN><xProd>KIT ERGONOMICO COMPLETO</xProd><NCM>02071200</NCM><cBenef></cBenef><CFOP>5949</CFOP><uCom>PC</uCom><qCom>1.0000</qCom><vUnCom>100.00000000</vUnCom><vProd>100.00</vProd><cEANTrib>SEM GTIN</cEANTrib><uTrib>PC</uTrib><qTrib>1.0000</qTrib><vUnTrib>100.00000000</vUnTrib><indTot>1</indTot></prod><imposto><ICMS><ICMS40><orig>0</orig><CST>41</CST></ICMS40></ICMS><IPI><cEnq>999</cEnq><IPINT><CST>53</CST></IPINT></IPI><PIS><PISNT><CST>08</CST></PISNT></PIS><COFINS><COFINSNT><CST>08</CST></COFINSNT></COFINS></imposto></det><total><ICMSTot><vBC>0</vBC><vICMS>0</vICMS><vICMSDeson>0</vICMSDeson><vFCPUFDest>0</vFCPUFDest><vICMSUFDest>0</vICMSUFDest><vICMSUFRemet>0</vICMSUFRemet><vFCP>0</vFCP><vBCST>0</vBCST><vST>0</vST><vFCPST>0</vFCPST><vFCPSTRet>0</vFCPSTRet><vProd>100.00</vProd><vFrete>0</vFrete><vSeg>0</vSeg><vDesc>0</vDesc><vII>0</vII><vIPI>0</vIPI><vIPIDevol>0</vIPIDevol><vPIS>0</vPIS><vCOFINS>0</vCOFINS><vOutro>0</vOutro><vNF>100.00</vNF></ICMSTot></total><transp><modFrete>0</modFrete><vol><qVol>1</qVol><esp>UNIDADE</esp><pesoL>2.000</pesoL><pesoB>2.000</pesoB></vol></transp><pag><detPag><indPag>0</indPag><tPag>99</tPag><vPag>100.00</vPag></detPag></pag><infAdic><infCpl>Pedido: C26388                                                                  Pedido Web: 000051591.</infCpl></infAdic><infRespTec><CNPJ>53113791000122</CNPJ><xContato>Rodrigo de Almeida Sartorio</xContato><email>resp_tecnico_dfe_protheus@totvs.com.br</email><fone>1128593904</fone></infRespTec></infNFe><Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"></CanonicalizationMethod><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"></SignatureMethod><Reference URI=\"#NFe35210658328758001377550010000206711100009452\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"></Transform><Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"></Transform></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"></DigestMethod><DigestValue>zYFFudZ/sKOESxVOfe3kYlElfTY=</DigestValue></Reference></SignedInfo><SignatureValue>vAN0n3HtpCjMXHoRkPh5dI2P6HXNqRKU8RnvkZeAzTxnCSBlPRwKlGZ/v164tKGUf0UOx4jPLyZDRhBV6Uhe1shM1eBcXZyUBZWJn3BnSpqQjH3OuNmDidYfNdyWpSOEnsanefkKAc/Zak3Xi6aCNE/cLXR3zxj0tQ0FwS18g0qFiTiHSel5LLXnu40/zDxhCj5PmMYf9CkVWX6Mz9eAxU2O1QrFqxlXTwYuXkkdm2FJrRxIZy8sxNDbokoKOKblBHpRajex1fPGup/wvD6aSgc1kbvCnrx0XL5Xx8hhBZ7BUJzyh1yMvldCOFK8VvBtG26/sdYmTtH8kxJenm52Mg==</SignatureValue><KeyInfo><X509Data><X509Certificate>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</X509Certificate></X509Data></KeyInfo></Signature></NFe><protNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><infProt><tpAmb>1</tpAmb><verAplic>SP_NFE_PL009_V4</verAplic><chNFe>35210658328758001377550010000206711100009452</chNFe><dhRecbto>2021-06-14T11:20:24-03:00</dhRecbto><nProt>135210658696214</nProt><digVal>zYFFudZ/sKOESxVOfe3kYlElfTY=</digVal><cStat>100</cStat><xMotivo>Autorizado o uso da NF-e</xMotivo></infProt></protNFe></nfeProc>";
      //var entity = FromXml<EnderDest>(meuXml);
      #endregion
      return View();
    }

    private static Byte[] BitmapToBytes(Bitmap img)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        return stream.ToArray();
      }
    }

    public FileContentResult Bruno()
    {

      var converter = new SynchronizedConverter(new PdfTools());
      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
          ColorMode = ColorMode.Color,
          Orientation = Orientation.Portrait,
          PaperSize = PaperKind.A4,
          Margins = new MarginSettings() { Top = 10 },
          //Out = $"{Directory.GetCurrentDirectory()}\\wwwroot\\img\\test.pdf",
          },
        Objects = {
          new ObjectSettings()
          {
              //Page = "http://localhost:44392/Home/",
              HtmlContent = @"<b>Lorem</b><div style='display:none'>bruno</div> ",
          },
        }
      };

      var pdf = converter.Convert(doc);

      return File(pdf, "application/pdf");
    }

    private void ReadXml()
    {
      XmlSerializer ser = new XmlSerializer(typeof(NFe));
      TextReader textReader = (TextReader)new StreamReader("‪C:\\Users\\b.sajermann.da.silva\\Deskto\\test.xml");
      XmlTextReader reader = new XmlTextReader(textReader);
      reader.Read();

      NFe nota = (NFe)ser.Deserialize(reader);
    }

    protected T FromXml<T>(String xml)
    {
      T returnedXmlClass = default(T);

      try
      {
        using (TextReader reader = new StringReader(xml))
        {
          try
          {
            returnedXmlClass =
                (T)new XmlSerializer(typeof(T)).Deserialize(reader);
          }
          catch (Exception e)
          {
            var tes = e.Message;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"Deu erro aqui oh: {ex}");
      }

      return returnedXmlClass;
    }

  }
}
