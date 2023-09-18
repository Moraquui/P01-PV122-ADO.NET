
using System.Collections.Generic;

namespace DemoClients
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public IEnumerable<OrganizationUser> OrganizationUsers { get; set; }
    }
}