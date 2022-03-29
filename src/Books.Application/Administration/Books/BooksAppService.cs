using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Books.Administration.Books.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Books
{
    public class BooksAppService : AsyncCrudAppService<StudentBooks, BooksDto>
    {


        private readonly IRepository<StudentBooks,int> _studentBooks;
        private readonly IRepository<AcademicGradeBooks, int> _academicGradeBooks;



        public BooksAppService(IRepository<StudentBooks, int> studentBooks, IRepository<AcademicGradeBooks, int> academicGradeBooks) :base(studentBooks)
        {
            _studentBooks=  studentBooks;
            _academicGradeBooks= academicGradeBooks;    
        }


        public  async Task<PagedResultDto<BooksDto>> GetAllBooks(BookInput input)
        {

            var result = new PagedResultDto<BooksDto>();

            var query = await (from b in _studentBooks.GetAll()
                               .WhereIf(!input.filter.IsNullOrWhiteSpace(),
                               a=>a.Isbn.Contains(input.filter)||
                               a.Name.Contains(input.filter)||
                               a.Price.ToString().Contains(input.filter))
                               join ac in _academicGradeBooks.GetAll().Include(a => a.Grade)
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
                               }
                       ).ToListAsync();

            result.Items = query;
            result.TotalCount = query.Count;

            return result;

        }


    }
}
