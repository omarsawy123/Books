using Books.Controllers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            string root = _webHostEnvironment.WebRootPath + "\\assets\\" + "test.pdf";

            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, ms);


            PdfPTable headerTable = new PdfPTable(3);

            headerTable.SpacingBefore = 20;

            headerTable.WidthPercentage = 100;

            decimal totalPrice = 0;



            List<string> studentHeaders = new List<string>() { "Das Schuljahr", "Name", "Klassen" };
            List<string> bookHeaders = new List<string>() { "ISBN", "Buchname", "Verlag", "Obligatorisch"
                                                            , "Kauf ja/Nein","Preis" };
            List<string> totalHeader = new List<string>() { "Zwischensumme", "Zusätze", "Gesmat" };


            Font font = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
            Font fontStudent = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);

            PdfPCell[] headerCells = new PdfPCell[]
            {
                new PdfPCell(new Phrase("Das Schuljahr",font)),
                new PdfPCell(new Phrase("Name",font)),
                new PdfPCell(new Phrase("Klassen",font)),

            };

            foreach (var cell in headerCells)
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(211, 211, 211);
            }

            PdfPRow headerRow = new PdfPRow(headerCells);
            headerTable.Rows.Add(headerRow);



            document.Open();    
            document.Add(headerTable);
            document.NewPage();
            document.Add(headerTable);
            document.Close();

            var bytes=ms.ToArray();

            var test= File(bytes, "application/pdf", "test.pdf");
            return File(bytes, "application/pdf", "test.pdf");
        }
    }
}
