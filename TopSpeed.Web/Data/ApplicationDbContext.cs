using Microsoft.EntityFrameworkCore;
using TopSpeed.Web.Models;

namespace TopSpeed.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Brand> Brand { get; set; }
    }
}
