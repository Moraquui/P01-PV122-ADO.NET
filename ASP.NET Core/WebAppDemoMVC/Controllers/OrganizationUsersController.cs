using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoMVC.Data;

namespace WebAppDemoMVC.Controllers
{
    public class OrganizationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrganizationUsersController(ApplicationDbContext context)
        {
             
            _context = context;
        }

        // GET: OrganizationUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrganisationUsers.Include(o => o.Organization);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: OrganizationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organizationUser = await _context.OrganisationUsers
                .Include(o => o.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationUser == null)
            {
                return NotFound();
            }

            return View(organizationUser);
        }

        // GET: OrganizationUsers/Create
        public IActionResult Create()
        {
            ViewData["OrganizationId"] = new SelectList(_context.Organisations, "Id", "FullName");
            return View();
        }

        // POST: OrganizationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OrganizationId,AccessLevel")] OrganizationUser organizationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organisations, "Id", "FullName", organizationUser.OrganizationId);
            return View(organizationUser);
        }

        // GET: OrganizationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organizationUser = await _context.OrganisationUsers.FindAsync(id);
            if (organizationUser == null)
            {
                return NotFound();
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organisations, "Id", "FullName", organizationUser.OrganizationId);
            return View(organizationUser);
        }

        // POST: OrganizationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OrganizationId,AccessLevel")] OrganizationUser organizationUser)
        {
            if (id != organizationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationUserExists(organizationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organisations, "Id", "FullName", organizationUser.OrganizationId);
            return View(organizationUser);
        }

        // GET: OrganizationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organizationUser = await _context.OrganisationUsers
                .Include(o => o.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationUser == null)
            {
                return NotFound();
            }

            return View(organizationUser);
        }

        // POST: OrganizationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrganisationUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrganisationUsers'  is null.");
            }
            var organizationUser = await _context.OrganisationUsers.FindAsync(id);
            if (organizationUser != null)
            {
                _context.OrganisationUsers.Remove(organizationUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationUserExists(int id)
        {
          return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
