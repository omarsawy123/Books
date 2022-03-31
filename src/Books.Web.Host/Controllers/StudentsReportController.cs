using Books.Controllers;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Books.Web.Host.Controllers
{
    public class StudentsReportController : BooksControllerBase
    {

        IWebHostEnvironment _webHostEnvironment;


        public StudentsReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
       
        public FileContentResult Print(int id) 
        {

            string root = _webHostEnvironment.WebRootPath + "\\assets\\"+"test.pdf";

            MemoryStream ms = new MemoryStream();

            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADERtes22222t"+id.ToString())
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();

            var bytes=ms.ToArray();

            var test= File(bytes, "application/pdf", "test.pdf");
            return File(bytes, "application/pdf", "test.pdf");
        }
    }
}
