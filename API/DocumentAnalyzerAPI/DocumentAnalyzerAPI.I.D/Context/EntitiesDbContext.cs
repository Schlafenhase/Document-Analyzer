using DocumentAnalyzerAPI.D.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentAnalyzerAPI.I.D.Context
{
    public class EntitiesDbContext : DbContext
    {
        public EntitiesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<File> Files { get; set; }
    }
}
