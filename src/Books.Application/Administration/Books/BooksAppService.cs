using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Books.Administration.Books.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Administration.Books
{
    public class BooksAppService : AsyncCrudAppService<StudentBooks, BooksDto>
    {


        private readonly IRepository<StudentBooks,int> _studentBooks;
        private readonly IRepository<AcademicGradeBooks, int> _academicGradeBooks;
        private readonly IRepository<StudentSelectedBooks, int> _selectedBooks;
        private readonly IRepository<AcademicStudents, int> _academicStudents;
        private readonly IRepository<Grades, int> _grades;

        IWebHostEnvironment _webHostEnv;

        public BooksAppService(IRepository<StudentBooks, int> studentBooks
            , IRepository<AcademicGradeBooks, int> academicGradeBooks,
            IRepository<StudentSelectedBooks, int> selectedBooks,
            IRepository<AcademicStudents, int> academicStudents,
             IRepository<Grades, int> grades,
             IWebHostEnvironment webHostEnv) 
            :base(studentBooks)
        {
            _studentBooks=  studentBooks;
            _academicGradeBooks= academicGradeBooks;    
            _selectedBooks= selectedBooks;
            _academicStudents= academicStudents;
            _grades = grades;
            _webHostEnv= webHostEnv;
        }


        public  async Task<PagedResultDto<BooksDto>> GetAllBooks(BookInput input)
        {

            var result = new PagedResultDto<BooksDto>();

            var query = await (from b in _studentBooks.GetAll()
                               .WhereIf(!input.filter.IsNullOrWhiteSpace(),
                               a=>a.Isbn.Contains(input.filter)||
                               a.Name.Contains(input.filter)||
                               a.Price.ToString().Contains(input.filter))

                               join ac in _academicGradeBooks.GetAll()
                               .Include(a => a.Grade).Include(a=>a.Publisher)
                               .WhereIf(input.gradId!=0,a=>a.GradeId==input.gradId)
                               on b.Id equals ac.BookId
                             
                               select new BooksDto
                               {
                                   Id=b.Id,
                                   Name =b.Name,
                                   ISBN=b.Isbn,
                                   Price=b.Price,
                                   BookGradeId= (int)ac.GradeId,
                                   BookGradeName=ac.Grade.Name,
                                   IsMandatory=b.IsMandatory,
                                   IsPrevoius=b.IsPreviousYear,
                                   IsAdditional=b.IsAdditional,
                                   PublisherName=ac.Publisher.Name,

                               }
                       ).ToListAsync();

            result.Items = query.OrderBy(a => a.BookGradeId).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            result.TotalCount = query.Count;

            return result;

        }


        // Get selected books then add mandatory books
        public async Task<PagedResultDto<RequiredBooksDto>> GetRequiredBooks(BookInput input)
        {

            var Books = new List<RequiredBooksDto>();

            var query = (from s in _selectedBooks.GetAll().Where(a => a.IsSelected)
                         join ac in _academicGradeBooks.GetAll()
                         .Include(a => a.Grade).Include(a => a.Book).Include(a => a.Publisher)
                         .WhereIf(!input.filter.IsNullOrWhiteSpace(),
                         a => a.Book.Name.Contains(input.filter) ||
                         a.Book.Isbn.Contains(input.filter) ||
                         a.Publisher.Name.Contains(input.filter))
                         .WhereIf(input.gradId != 0, b => b.Grade.Id == input.gradId)
                         on s.AcademicGradeBookId equals ac.Id
                         select new
                         {
                             bookId = ac.BookId,
                             Isbn = ac.Book.Isbn,
                             name = ac.Book.Name,
                             gradeId = ac.GradeId,
                             gradeName = ac.Grade.Name,
                             publisher = ac.Publisher.Name,
                             mandatory = ac.Book.IsMandatory,

                         }
                       ).AsEnumerable().GroupBy(a=>new { a.Isbn, a.gradeId }).ToList();

            var MandBooks = (from s in _studentBooks.GetAll().Where(a => a.IsMandatory)
                             join ac in _academicGradeBooks.GetAll()
                             .Include(a => a.Grade).Include(a => a.Book).Include(a => a.Publisher)
                             .WhereIf(!input.filter.IsNullOrWhiteSpace(),
                             a => a.Book.Name.Contains(input.filter) ||
                             a.Book.Isbn.Contains(input.filter) ||
                             a.Publisher.Name.Contains(input.filter))
                             .WhereIf(input.gradId != 0, b => b.GradeId == input.gradId)
                             on s.Id equals ac.BookId
                             select new
                             {
                                 bookId = s.Id,
                                 Isbn = s.Isbn,
                                 name = s.Name,
                                 gradeId = ac.GradeId,
                                 gradeName = ac.Grade.Name,
                                 publisher = ac.Publisher.Name,
                                 mandatory = s.IsMandatory,

                             }
                       ).AsEnumerable().GroupBy(a => new { a.Isbn, a.gradeId }).ToList();



            var AcademicStudents = _academicStudents.GetAll().Include(a=>a.AcademicGradeClasses);

            foreach (var isb in query)
            {
               
                var book = new RequiredBooksDto();

                book.Isbn = isb.Key.Isbn;
                book.GradeId = (int)isb.Key.gradeId;
                book.GradeName = isb.FirstOrDefault().gradeName;
                book.BookName = isb.FirstOrDefault().name;
                book.PublisherName = isb.FirstOrDefault().publisher;
                book.Count=isb.Count();
                book.IsMandatory = isb.FirstOrDefault().mandatory;

                Books.Add(book);
            }

            foreach (var isb in MandBooks)
            {
                int studentsCount = AcademicStudents
                    .Where(a=>a.AcademicGradeClasses.GradeId== isb.Key.gradeId).Count();

                var Mbook = new RequiredBooksDto();

                Mbook.Isbn = isb.Key.Isbn;
                Mbook.GradeId = (int)isb.Key.gradeId;
                Mbook.GradeName = isb.FirstOrDefault().gradeName;
                Mbook.BookName = isb.FirstOrDefault().name;
                Mbook.PublisherName = isb.FirstOrDefault().publisher;
                Mbook.Count = isb.Count() * studentsCount;
                Mbook.IsMandatory = isb.FirstOrDefault().mandatory;

                Books.Add(Mbook);
            }

            var result = new PagedResultDto<RequiredBooksDto>();
            result.Items = Books.OrderBy(b => b.GradeId)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            result.TotalCount= Books.Count;

            return result; 

        }



        public List<GradesDropDownDto> GetAllGradesForDropDown()
        {
            var result = _grades.GetAll()
                .Select(a => new GradesDropDownDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    NameAr = a.NameAr,
                    StudyOrder = a.StudyOrder
                }).OrderBy(g => g.StudyOrder).ToList();

            return result;
           
            
        }


        //public PagedResultDto<BooksDto> PreviewBooksExcel(IFormFile file, PagedResultRequestDto input)
        //{

        //    var Books = new List<BooksDto>();

        //    var Grades = _grades.GetAll().ToList();

        //    var ms = new MemoryStream();
        //    try
        //    {
        //        file.CopyTo(ms);

        //        using (var package = new ExcelPackage(ms))
        //        {
        //            ExcelWorksheet sheet = package.Workbook.Worksheets[0];

        //            var rowCount = sheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                Books.Add(new BooksDto()
        //                {
        //                    ISBN = sheet.Cells[row, 1].Value.ToString(),
        //                    Name = sheet.Cells[row, 2].Value.ToString(),
        //                    Price = Convert.ToDecimal(sheet.Cells[row, 3].Value),
        //                    BookGradeName = sheet.Cells[row, 4].Value.ToString(),
        //                    BookGradeId = Grades.
        //                    FirstOrDefault(a => a.Name == sheet.Cells[row, 4].Value.ToString()).Id,

        //                    IsMandatory = sheet.Cells[row, 5].Value.ToString().ToLower() == "true" ? true : false,
        //                    IsPrevoius = sheet.Cells[row, 6].Value.ToString().ToLower() == "true" ? true : false,
        //                    IsAdditional = sheet.Cells[row, 7].Value.ToString().ToLower() == "true" ? true : false,
        //                    PublisherName = sheet.Cells[row, 8].Value.ToString(),
        //                });
        //            }
        //        }

        //        var result = new PagedResultDto<BooksDto>();

        //        result.Items = Books.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        //        result.TotalCount = Books.Count;

        //        return result;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new UserFriendlyException("Error", ex);
        //    }

        //}

    }
}
