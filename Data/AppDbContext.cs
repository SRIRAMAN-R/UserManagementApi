using Microsoft.EntityFrameworkCore;
using UserManagementApi.Models; // Ensure this is the correct namespace for your models

namespace UserManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // Ensure this matches your model
    }
}
