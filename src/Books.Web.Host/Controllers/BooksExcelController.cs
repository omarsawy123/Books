using Books.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Host.Controllers
{
    public class BooksExcelController : BooksControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;
        public BooksExcelController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public FileContentResult DownloadExampleFile()
        {

            string filePath = _webHostEnvironment.WebRootPath + "\\assets\\exampleExcel\\ExampleFile.xlsx";

            return File(System.IO.File.ReadAllBytes(filePath), "application/xlsx", "ExampleFile.xlsx");
        }

    }
}
