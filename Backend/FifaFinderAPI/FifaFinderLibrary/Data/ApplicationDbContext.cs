using FifaFinderAPI.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace FifaFinderAPI.Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // Creating User and Post Tables on migration to MYSQL
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
