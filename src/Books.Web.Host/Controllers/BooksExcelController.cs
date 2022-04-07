using Abp.Domain.Repositories;
using Abp.UI;
using Books.Administration;
using Books.Administration.Books.Dto;
using Books.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Books.Web.Host.Controllers
{
    public class BooksExcelController : BooksControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Grades, int> _grades;


        public BooksExcelController(IWebHostEnvironment webHostEnvironment, IRepository<Grades, int> grades)
        {
            _webHostEnvironment = webHostEnvironment;
            _grades = grades;   
        }


        public FileContentResult DownloadExampleFile()
        {

            string filePath = _webHostEnvironment.WebRootPath + "\\assets\\exampleExcel\\ExampleFile.xlsx";

            return File(System.IO.File.ReadAllBytes(filePath), "application/xlsx", "ExampleFile.xlsx");
        }

        public int PreviewBooksExcel(IFormFile file)
        {

            var Books = new List<BooksDto>();

            var Grades = _grades.GetAll().ToList();

            var ms = new MemoryStream();
            try
            {
                file.CopyTo(ms);

                using (var package = new ExcelPackage(ms))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                    var rowCount = sheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                       
                        Books.Add(new BooksDto()
                        {
                            ISBN = sheet.Cells[row, 1].Value.ToString(),
                            Name = sheet.Cells[row, 2].Value.ToString(),
                            Price = Convert.ToDecimal(sheet.Cells[row, 3].Value),
                            BookGradeName = sheet.Cells[row, 4].Value.ToString(),
                            BookGradeId = Grades.
                            FirstOrDefault(a => a.Name == sheet.Cells[row, 4].Value.ToString()).Id,

                            IsMandatory = sheet.Cells[row, 5].Value.ToString().ToLower() == "true" ? true : false,
                            IsPrevoius = sheet.Cells[row, 6].Value.ToString().ToLower() == "true" ? true : false,
                            IsAdditional = sheet.Cells[row, 7].Value.ToString().ToLower() == "true" ? true : false,
                            PublisherName = sheet.Cells[row, 8].Value.ToString(),
                        });
                    }
                }

                //var result = new PagedResultDto<BooksDto>();

                //result.Items = Books.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                //result.TotalCount = Books.Count;

                //return result;

                return 123333;

            }
            catch (Exception ex)
            {

                throw new UserFriendlyException("Error", ex);
            }

        }


    }
}
