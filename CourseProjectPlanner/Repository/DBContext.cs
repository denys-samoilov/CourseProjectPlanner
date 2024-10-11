using CourseProjectPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ASP_Core_EF.Repository
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Spend> Spends { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
