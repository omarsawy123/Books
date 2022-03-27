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


        public  void GetAllStudentSelectedBooks()
        {

            var students = (from s in _selectedBooks.GetAll()
                            join agc in _academicGradeBooks.GetAll() on s.AcademicGradeBookId equals agc.Id
                            join b in _books.GetAll() on agc.BookId equals b.Id
                            into allB
                            from x in allB.DefaultIfEmpty()
                            join pu in _publisher.GetAll() on agc.PublisherId equals pu.Id
                            join grd in _grades.GetAll() on agc.GradeId equals grd.Id
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
                                IsSelected=s.IsSelected,
                            }
                            ).ToList();

            var MandatoryBooks = (from b in _books.GetAll().Where(b => b.IsMandatory)
                                  join agc in _academicGradeBooks.GetAll()
                                  on b.Id equals agc.BookId
                                  join g in _grades.GetAll() 
                                  on agc.GradeId equals g.Id
                                  join p in _publisher.GetAll() on agc.PublisherId equals p.Id
                                  //join s in _repository.GetAll() on 

                                  select new SelectedBooksDto
                                  {
                                      BookId = b.Id,
                                      BookName = b.Name,
                                      IsMandatory = b.IsMandatory,
                                      BookGradeId = g.Id,
                                      BookGradeName=g.Name,
                                      PublihserName = p.Name,
                                  }
                                 ).ToList();
            var resut = new List<SelectedBooksDto>();

            students.AddRange(MandatoryBooks);

            //foreach (var stud in students.GroupBy(a=>a.StudentId))
            //{

            //    int GradeId = stud.FirstOrDefault().BookGradeId;

            //    var MandatoryValues = MandatoryBooks.Where(b => b.BookGradeId == GradeId).ToList();
            //    int temp = stud.FirstOrDefault().StudentId;

            //    MandatoryValues.ForEach(b=>b.StudentId=temp);

            //    students.AddRange(MandatoryValues);

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
