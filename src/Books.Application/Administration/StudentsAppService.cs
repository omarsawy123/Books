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

namespace Books.StudentsAppServices
{
    public  class StudentsAppService: AsyncCrudAppService<Students, StudentsDto>
    {

        private readonly IRepository<Students> _repository;
        private readonly IRepository<User,long> _users;
        private readonly IRepository<AcademicStudents> _academicStudents;
        private readonly IRepository<AcademicGradeClasses> _academicGradeClasses;
        private readonly IRepository<Grades> _grades;




        public StudentsAppService(IRepository<Students> repository, IRepository<User, long> users, IRepository<AcademicStudents> academicStudents,
             IRepository<AcademicGradeClasses> academicGradeClasses,
             IRepository<Grades> grades
            )
        : base(repository)
        {
            _repository = repository;
            _users = users;
            _academicStudents = academicStudents;  
            _academicGradeClasses = academicGradeClasses;
            _grades = grades;
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


        public async void UpdateStudentsUserId()
        {

            var result = from s in _repository.GetAll()
                         join u in _users.GetAll() on
                         s.Name + ' ' + s.FamilyName equals u.Name
                         select new
                         {
                             userid = u.Id,
                             name = s.Name + ' ' + s.FamilyName
                         };

            foreach (var item in _repository.GetAll())
            {
                var check = result.
                     Where(r => r.name == item.Name + ' ' + item.FamilyName).Any();
                if(check)
                {
                    item.UserId = result.Where(r => r.name == item.Name + ' ' + item.FamilyName)
                                        .FirstOrDefault().userid;
                    await _repository.UpdateAsync(item);
                }
               
                

            }
        }
    }
}
