using System.ComponentModel.DataAnnotations;

namespace DemoClients
{
    public class Organization
    {
        public int Id { get; set; }
        [Display(Name = "DisplayName")]
        public string Name { get; set; } = default!;
        public string FullName { get; set; } = default!;

        public IEnumerable<OrganizationUser>? OrganizationUsers { get; set; }
    }
}