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
    }
}
