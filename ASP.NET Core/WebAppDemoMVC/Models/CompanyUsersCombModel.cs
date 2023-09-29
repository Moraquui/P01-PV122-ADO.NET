using DemoClients;

namespace WebAppDemoMVC.Models
{
    public class CompanyUsersCombModel
    {
        public CompanyUsersCombModel(Organization org, IEnumerable<OrganizationUser> Users)
        {
            Organization = org;
            OrganizationUser = Users;
        }
        public Organization Organization { get; set; }
        public IEnumerable<OrganizationUser> OrganizationUser { get; set; }
    }
}
