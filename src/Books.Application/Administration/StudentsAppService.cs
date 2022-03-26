using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Books.Administration;
using Books.Administration.Dto;
using Books.Authorization.Users;
using Books.EntityFrameworkCore;

namespace Books.StudentsAppServices
{
    public  class StudentsAppService: AsyncCrudAppService<Students, StudentsDto>
    {

        private readonly IRepository<Students,int> _repository;
        private readonly IRepository<User,long> _users;
        private readonly IRepository<AcademicStudents> _academicStudents;
        private readonly IRepository<AcademicGradeClasses> _academicGradeClasses;
        private readonly IRepository<AcademicGradeBooks> _academicGradeBooks;
        private readonly IRepository<Grades> _grades;
        private readonly IRepository<StudentSelectedBooks> _selectedBooks;
        private readonly IRepository<StudentBooks> _books;
        private readonly IRepository<Publishers> _publisher;
        private readonly IRepository<StudentMandatoryBooks> _studentMandatoryBooks;










        public StudentsAppService(IRepository<Students,int> repository, IRepository<User, long> users, IRepository<AcademicStudents> academicStudents,
             IRepository<AcademicGradeClasses> academicGradeClasses,
             IRepository<Grades> grades,
             IRepository<StudentSelectedBooks> selectedBooks,
             IRepository<AcademicGradeBooks> academicGradeBooks,
             IRepository<StudentBooks> books,
             IRepository<Publishers> publisher,
             IRepository<StudentMandatoryBooks> studentMandatoryBooks


            )
        : base(repository)
        {
            _repository = repository;
            _users = users;
            _academicStudents = academicStudents;  
            _academicGradeClasses = academicGradeClasses;
            _grades = grades;
            _selectedBooks = selectedBooks;
            _academicGradeBooks = academicGradeBooks;   
            _books = books; 
            _publisher= publisher;  
            _studentMandatoryBooks= studentMandatoryBooks;
           
        }


        public List<StudentsDashBoardDto> GetStudentsForDashboard()
        {
            var result = new List<StudentsDashBoardDto>();

            var query = (from s in _repository.GetAll()
                                      join u in _users.GetAll() on s.UserId equals u.Id
                                      into studs
                                      from st in studs.DefaultIfEmpty()
                                      join ac in _academicStudents.GetAll() on s.Id equals ac.StudentId
                                      join agc in _academicGradeClasses.GetAll() on ac.AcademicGradeClassId equals agc.Id

                                      select new 
                                      {
                                          studentId = s.Id,
                                          userId = s.UserId,
                                          GradeName = agc.Grade.Name
                                      }).ToList();


            foreach (var grd in _grades.GetAll())
            {
                var student = new StudentsDashBoardDto();
                student.RegisteredStudents = query
                    .Where(s => s.GradeName == grd.Name && s.userId != null).Count();

                student.UnRegisteredStudents = query
                    .Where(s => s.GradeName == grd.Name && s.userId == null).Count();

                student.GradeName=grd.Name;

                student.TotalStudents = student.UnRegisteredStudents + student.RegisteredStudents;

                result.Add(student);
            }


            return result;
        }

        public void MergeBooks()
        {

            var students = (from b in _books.GetAll()
                            join ac in _academicGradeBooks.GetAll()
                            on b.Id equals ac.BookId
                            join st in _selectedBooks.GetAll()
                            on ac.Id equals st.AcademicGradeBook.BookId
                            //into studs
                            //from st in studs.DefaultIfEmpty()
                            select new
                            {
                                Bookname = b.Name,
                                mandatory = b.IsMandatory,
                                Id = st == null ? 0 : st.Id


                            }).ToList();
                


        }


        public void UpdateStudentSelectedBooks()
        {

            var students = (from s in _selectedBooks.GetAll().Where(s => s.IsSelected)
                            join agc in _academicGradeBooks.GetAll() on s.AcademicGradeBookId equals agc.Id
                            join b in _books.GetAll() on agc.BookId equals b.Id
                            into allB
                            from x in allB.DefaultIfEmpty()
                            join pu in _publisher.GetAll() on agc.PublisherId equals pu.Id
                            join grd in _grades.GetAll() on agc.GradeId equals grd.Id
                            //join std in _studentMandatoryBooks.GetAll() on agc.Id equals std.AcademicGradeBookId
                            select new SelectedBooksDto
                            {
                                BookId = x.Id,
                                BookName = x.Name,
                                BookGradeName = grd.Name,
                                BookGradeId = grd.Id,
                                PublihserName = pu.Name,
                                IsMandatory = x.IsMandatory,
                                IsPrevious = x.IsPreviousYear,
                                StudentId = s.StudentId,
                            }
                            ).ToList();

            //var MandatoryBooks = (from b in _books.GetAll().Where(b => b.IsMandatory)
            //                      join agc in _academicGradeBooks.GetAll()
            //                      on b.Id equals agc.BookId
            //                      select new
            //                      {
            //                          BookId = b.Id,
            //                          BookName = b.Name,
            //                          IsMandatory = b.IsMandatory,
            //                          BookGradeId = agc.GradeId
            //                      }
            //                     ).ToList();

            //foreach (var stud in students.Select(s => s.StudentId).Distinct())
            //{
            //    MandatoryBooks.Where()
            //}

            //foreach (var stud in students)
            //{
            //    var book = new SelectedBooksDto();

            //    book.BookName = _academicGradeBooks.FirstOrDefault(b => b.Id == stud.AcademicGradeBookId);
            //    book.BookId = stud.AcademicGradeBook.Book.Id;
            //    book.BookGradeName = stud.AcademicGradeBook.Grade.Name;
            //    book.IsSelected=stud.IsSelected;
            //    book.IsMandatory = stud.AcademicGradeBook.Book.IsMandatory;
            //    book.PublihserName=stud.AcademicGradeBook.Publisher.Name;
            //    book.IsPrevious = stud.AcademicGradeBook.Book.IsPreviousYear;

            //    books.Add(book);

            //}

            //foreach (var item in books.Select(b=>b.BookGradeName))
            //{
            //    var b=_academicGradeBooks.GetAll().Where(b=>b.Grade.Name==item).ToList();

            //}

            //List<AcademicGradeBooks> mandBooks;

            //students.ForEach(s=>s.AcademicGradeBook.GradeId)

            //foreach (var student in )
            //{
            //    mandBooks= _academicGradeBooks.GetAll().Where(a => a.GradeId == student && a.Book.IsMandatory).ToList();


            //}
        }

        //public async void UpdateStudentsUserId()
        //{

        //    var result = (from s in _repository.GetAll()
        //                  join u in _users.GetAll() on
        //                  s.Name + ' ' + s.FamilyName equals u.Name
        //                  select new
        //                  {
        //                      userid = u.Id,
        //                      name = s.Name + ' ' + s.FamilyName
        //                  }).ToList();


        //    foreach (var item in _repository.GetAll().ToList())
        //    {
        //        var check = result.
        //             Where(r => r.name==item.Name + ' ' + item.FamilyName).Any();
        //        if(check)
        //        {
        //            item.UserId = result.Where(r => r.name==item.Name+' '+item.FamilyName)
        //                                .FirstOrDefault().userid;
        //             _repository.Update(item);
        //            //_context.SaveChanges();
        //        }



        //    }
        //}
    }
}
