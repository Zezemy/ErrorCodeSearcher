using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace ErrorDataApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ErrorData> ErrorDatas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}
