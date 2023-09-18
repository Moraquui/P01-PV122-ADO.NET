using DemoClients;
using Microsoft.EntityFrameworkCore;
namespace WebAppDemoApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationUser> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
    }
}
