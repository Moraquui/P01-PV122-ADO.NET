using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoClients;
namespace WebAppDemoMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Organization> Organisations { get; set; }
        public DbSet<OrganizationUser> OrganisationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }

}