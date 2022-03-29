using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Books.Administration;
using Books.Administration.Dto;
using Books.Authorization.Users;
using Books.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Books.Administration.StudentsAppServices
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


        public void GetAl(PagedAndSortedResultRequestDto input)
        {

            var query = (from s in _repository.GetAll().Where(s=>s.Id==493)
                         join ac in _academicStudents.GetAll()
                         on s.Id equals ac.StudentId
                         join agc in _academicGradeClasses.GetAll()
                         on ac.AcademicGradeClassId equals agc.Id
                         join st in _selectedBooks.GetAll()
                         on s.Id equals st.StudentId
                         select new
                         {
                             Name=s.Name,
                             FamilyName=s.FamilyName,
                             NameAr=s.NameAr,
                             FamilyNameAr=s.FamilyNameAr,
                             GradeName=agc.Grade.Name,
                             GradeId=agc.GradeId,
                             ClassName=agc.Class.Name,
                             ClassId=agc.ClassId,
                             

                         }
                       ).ToList();

        }

        public async Task<DashBoardStatisticsDto> GetStudentsForDashboard()
        {
            var result =new DashBoardStatisticsDto();
            result.StudentsDashBoard = new List<StudentsDashBoardDto>();

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

                result.StudentsDashBoard.Add(student);
            }

            result.TotalRegisteredStudents= result.StudentsDashBoard.Sum(a => a.RegisteredStudents);


            result.TotalRequiredBooks = CalculateRequiredBooks();

            return result;
        }

        public int CalculateRequiredBooks()
        {
            int result = 0;
            var query = (from s in _selectedBooks.GetAll().Where(s => s.IsSelected)
                         join ac in _academicGradeBooks.GetAll()
                         on s.AcademicGradeBookId equals ac.Id
                         select ac
                       ).ToList();

            result = query.Count();
            var MandBooks = _academicGradeBooks.GetAll().Where(a => a.Book.IsMandatory).ToList();
            var AcademicStudents = _academicStudents.GetAll().Include(a => a.AcademicGradeClasses).ToList();

            foreach (var book in MandBooks.GroupBy(b => b.GradeId))
            {

                var studentsGrade = AcademicStudents.Where(a => a.AcademicGradeClasses.GradeId == book.Key).Count();

                result += (studentsGrade * book.Count());

            }


            return result;
        }


        public StudentAllBooksDto GetAllStudentSelectedBooks()
        {

            var result = new List<StudentAllBooksDto>();

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
                                BookPrice=x.Price,
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
                                      BookPrice=b.Price
                                  }
                                 ).ToList();
     

            students.AddRange(MandatoryBooks);

            var AcademicStudents= _academicStudents.GetAll().Include(a=>a.Student).Include(a=>a.AcademicGradeClasses).Include(a=>a.AcademicGradeClasses.Grade).
                Include(a => a.AcademicGradeClasses.Class).ToList();

            foreach (var stud in AcademicStudents)
            {
                var item = new StudentAllBooksDto();

                item.Id = stud.Student.Id;
                item.Name= stud.Student.Name;
                item.NameAr = stud.Student.NameAr;
                item.FamilyName = stud.Student.FamilyName;
                item.FamilyNameAr= stud.Student.FamilyNameAr;
                item.GradeId = (int)stud.AcademicGradeClasses.GradeId;
                item.GradeName = stud.AcademicGradeClasses.Grade.Name;
                item.ClassId = (int)stud.AcademicGradeClasses.ClassId;
                item.ClassName = stud.AcademicGradeClasses.Class.Name;

                item.SelectedBooks = students.Where(a => a.StudentId == stud.Student.Id || (a.BookGradeId == stud.AcademicGradeClasses.GradeId && a.IsMandatory)).ToList();

                item.SelectedBooksCount = item.SelectedBooks.Count;

                item.SelectedBooksTotal = item.SelectedBooks.Sum(b => b.BookPrice);

                result.Add(item);
                
            }

            return result.FirstOrDefault(s=>s.Id==493); 

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
