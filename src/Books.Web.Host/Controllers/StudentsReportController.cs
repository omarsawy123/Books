using Abp.UI;
using Books.Administration.Dto;
using Books.Administration.StudentsAppServices;
using Books.Controllers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace Books.Web.Host.Controllers
{
    public class StudentsReportController : BooksControllerBase
    {

        IWebHostEnvironment _webHostEnvironment;
        readonly private StudentsAppService _studentsAppService;


        public StudentsReportController(IWebHostEnvironment webHostEnvironment,StudentsAppService studentsAppService)
        {
            _webHostEnvironment = webHostEnvironment;
            _studentsAppService = studentsAppService;   
        }

        public async Task<FileContentResult> Print(string IdQuery)
        {

            // Get info from API
            var input = new StudentReportInputDto();
            input.Id = new List<int>();

            foreach(var str in IdQuery.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    input.Id.Add(int.Parse(str));
                }
            }

            

            var students = await _studentsAppService.GetSelectedBooksForReport(input);

            if(students.Count <= 0)
            {
                throw new UserFriendlyException("Error 404");
            }
            

            string root = _webHostEnvironment.WebRootPath + "\\assets\\" + "test.pdf";

            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            PdfWriter.GetInstance(document, ms);
            document.Open();


            #region Generate PDF Table
            float[] widths = new float[] { 1.5f, 7f, 12f, 4f, 3f, 3f, 3f };
            int counter = 1;

            PdfPTable headerTable = new PdfPTable(3);

            headerTable.SpacingBefore = 20;
            headerTable.WidthPercentage = 100;
            headerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            List<string> studentHeaders = new List<string>() { "Das Schuljahr", "Name", "Klassen" };
           
            Font font = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
            Font fontStudent = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);

            foreach (var text in studentHeaders)
            {
                var cell = new PdfPCell(new Phrase(text, font));
                cell.BackgroundColor = new BaseColor(211, 211, 211);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTable.AddCell(cell);
            }


            foreach (var stud in students)
            {
                if(headerTable.Rows.Count > 1)
                {
                    headerTable.DeleteLastRow();
                }

                headerTable.AddCell(new PdfPCell(new Phrase(stud.AcademicYearName, font)) { HorizontalAlignment=Element.ALIGN_CENTER});

                headerTable.AddCell(new PdfPCell(new Phrase(stud.Name + ' ' + stud.FamilyName, font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                headerTable.AddCell(new PdfPCell(new Phrase(stud.ClassName, font)) { HorizontalAlignment = Element.ALIGN_CENTER });


                PdfPTable bodyTable =new PdfPTable(7);
                bodyTable.WidthPercentage = 100;
                bodyTable.SetWidths(widths);
                bodyTable.SpacingBefore = 20;

                bodyTable.AddCell(new PdfPCell(new Phrase("#", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("ISBN", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("Buchname", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("Verlag", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("Obligatorisch", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("Kauf ja/Nein", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                bodyTable.AddCell(new PdfPCell(new Phrase("Preis", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), HorizontalAlignment = Element.ALIGN_CENTER });

                

                foreach (var book in stud.SelectedBooks)
                {
                    var color=new BaseColor(255,255,255);

                    if (book.IsPrevious)
                    {
                        color = new BaseColor(211, 211, 211);
                    }
                    if (book.IsMandatory)
                    {
                        color = new BaseColor(255, 255, 0);

                    }
                    bodyTable.AddCell(new PdfPCell(new Phrase(counter.ToString(), fontStudent))
                    { BackgroundColor = color });

                    bodyTable.AddCell(new PdfPCell(new Phrase(book.Isbn, fontStudent))
                    { BackgroundColor = color });

                    bodyTable.AddCell(new PdfPCell(new Phrase(book.BookName, fontStudent)) 
                    { BackgroundColor = color});

                    bodyTable.AddCell(new PdfPCell(new Phrase(book.PublihserName, fontStudent)) 
                    { BackgroundColor = color });

                    

                    bodyTable.AddCell(new PdfPCell(new Phrase(book.BookPrice.ToString(), fontStudent))
                    { BackgroundColor = color });

                    bodyTable.AddCell(new PdfPCell(new Phrase(book.BookPrice.ToString(), fontStudent))
                    { BackgroundColor = color });


                    bodyTable.AddCell(new PdfPCell(new Phrase(book.BookPrice.ToString(), fontStudent))
                    { BackgroundColor = color});

                    counter++;
                }

                bodyTable.AddCell(new PdfPCell(new Phrase("Zwischensumme", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 2 });

                bodyTable.AddCell(new PdfPCell(new Phrase(stud.SelectedBooksTotal.ToString(), fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT });

                bodyTable.AddCell(new PdfPCell(new Phrase("Zusätze", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 2 });

                bodyTable.AddCell(new PdfPCell(new Phrase("0", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT });

                bodyTable.AddCell(new PdfPCell(new Phrase("Gesmat", fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 2 });

                bodyTable.AddCell(new PdfPCell(new Phrase(stud.SelectedBooksTotal.ToString(), fontStudent))
                { BackgroundColor = new BaseColor(211, 211, 211), Colspan = 5,HorizontalAlignment=Element.ALIGN_RIGHT });

                counter = 1;
                document.Add(headerTable);
                document.Add(bodyTable);
                document.NewPage();

            }

            #endregion
            
            
            document.Close();
            var bytes=ms.ToArray();

            var test= File(bytes, "application/pdf", "test.pdf");
            return File(bytes, "application/pdf", "test.pdf");
        }
    }
}
