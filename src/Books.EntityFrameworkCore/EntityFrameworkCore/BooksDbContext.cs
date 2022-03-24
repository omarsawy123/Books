using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Books.Authorization.Roles;
using Books.Authorization.Users;
using Books.MultiTenancy;
using Books.Administration;

namespace Books.EntityFrameworkCore
{
    public class BooksDbContext : AbpZeroDbContext<Tenant, Role, User, BooksDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<AcademicGradeBooks> AcademicGradeBooks { get; set; }
        public DbSet<AcademicGradeClasses> AcademicGradeClasses { get; set; }
        public DbSet<AcademicStudents> AcademicStudents { get; set; }
        public DbSet<AcademicYears> AcademicYears { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<StudentBooks> StudentBooks { get; set; }
        public DbSet<StudentSelectedBooks> StudentSelectedBooks { get; set; }
    }
}
