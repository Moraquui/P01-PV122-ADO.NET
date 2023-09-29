using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoMVC.Data;
using WebAppDemoMVC.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace WebAppDemoMVC.Controllers
{

    public class OrganizationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Organizations
        public async Task<IActionResult> Index(string fieldName, string? filter, SearchType ASD = SearchType.StartsWith)
        {
            ViewData["ActivePage"] = "Organization";
            if(_context.Organisations != null)
            {
                var myModel = new OrganizationSearchedView<Organization>() { SearchType = ASD, Filter = filter ?? string.Empty, FieldName = fieldName };
                var query = _context.Organisations.AsQueryable();

                myModel.Values = await myModel.GetSortedQueue(query).ToListAsync();
                return View(myModel);
                
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Organisations'  is null.");
            }
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organization = await _context.Organisations.Include(o => o.OrganizationUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organization == null)
            {
                return NotFound();
            }
            var organizationUsers = await _context.OrganisationUsers.Where(o => o.Organization.Id == organization.Id).ToListAsync();

            var CombinedModel = new CompanyUsersCombModel(organization, organizationUsers);

            return View(CombinedModel);
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FullName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }

        // GET: Organizations/Edit/id=5[FromQuery]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organization = await _context.Organisations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FullName")] Organization organization)
        {
            if (id != organization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.Id))
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
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organization = await _context.Organisations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organisations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organisations'  is null.");
            }
            var organization = await _context.Organisations.FindAsync(id);
            if (organization != null)
            {
                _context.Organisations.Remove(organization);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(int id)
        {
            return (_context.Organisations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
