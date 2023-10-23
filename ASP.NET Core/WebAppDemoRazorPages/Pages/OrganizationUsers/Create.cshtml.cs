using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoClients;
using WebAppDemoRazorPages.Data;

namespace WebAppDemoRazorPages.Pages.OrganizationUsers
{
    public class CreateModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public CreateModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;


        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public OrganizationUser OrganizationUser { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid || _context.OrganisationUsers == null || OrganizationUser == null)
            {
                return Page();
            }
            if((int)TempData["myData"] != null)
            {
                OrganizationUser.OrganizationId = (int)TempData["myData"];
                _context.OrganisationUsers.Add(OrganizationUser);
                await _context.SaveChangesAsync();

                return RedirectToPage($"/Organizations/Details", new { id = (int?)TempData["myData"] });
            }

            return RedirectToPage($"/Organizations/Index");

        }
    }
}
