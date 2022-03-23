using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Books.EntityFrameworkCore
{
    public static class BooksDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BooksDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BooksDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
