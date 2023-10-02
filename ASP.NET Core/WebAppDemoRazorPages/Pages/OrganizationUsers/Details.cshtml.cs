﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;

namespace WebAppDemoRazorPages.Pages.OrganizationUsers
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public OrganizationUser OrganizationUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organizationuser = await _context.OrganisationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (organizationuser == null)
            {
                return NotFound();
            }
            else 
            {
                OrganizationUser = organizationuser;
            }
            return Page();
        }
    }
}
