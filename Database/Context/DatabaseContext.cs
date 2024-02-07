using Microsoft.EntityFrameworkCore;
using TestTask.Models.Files;
using TestTask.Models.Results;
using TestTask.Models.Values;

namespace TestTask.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<ValueModel> Values { get; set; }
        public DbSet<ResultModel> Results { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) => Database.EnsureCreated();
    }
}
