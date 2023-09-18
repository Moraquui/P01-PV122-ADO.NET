using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DemoClients
{
    public class OrganizationUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public UserAccess AccessLevel { get; set; }
    }
}
