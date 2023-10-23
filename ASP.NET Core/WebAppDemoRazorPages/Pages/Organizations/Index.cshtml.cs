using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LinqKit;
using WebAppDemoRazorPages.Models;
using WebAppDemoRazorPages.Models;
namespace WebAppDemoRazorPages.Pages.Organizations
{
    public enum SearchType
    {
        StartsWith = 0,
        EndWith = 1,
        Contains = 2
    }
    public class IndexModel : PageModel, IPaginated, IFilterable
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public IndexModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
            foreach (var field in typeof(Organization).GetProperties())
            {
                if (field.PropertyType == typeof(string))
                    AvailableFields.Add(field.Name);
            }
        }
        public List<string> AvailableFields { get; set; } = new List<string>();
        public IList<Organization> Organization { get; set; } = default!;
        [BindProperty]
        public string Filter {  get; set; }
        [BindProperty]
        public SearchType searchType {  get; set; }
        [BindProperty]
        public string fieldName { get; set; } = "Name";
        [BindProperty(SupportsGet = true)]
        public int Skip { get; set; }

        public int CountItems { get; set; }
        [BindProperty]
        public int PageSize { get; set; } = 2;

        public async Task OnGetAsync()
        {
            if (_context.Organisations != null)
            {
                await GetItems(_context.Organisations.AsQueryable());
            }
        }
        async Task GetItems(IQueryable<Organization> query)
        {
            var s = new List<Organization>();
            if (!string.IsNullOrEmpty(Filter))
            {
                //delete switch from ent
                foreach (var fieldName123 in AvailableFields)
                {
                    //s.AddRange(query.Where(obj => EF.Property<string>(obj, fieldName123).Contains(Filter)).ToList());
                    var predicate = PredicateBuilder.New<Organization>(false);

                    foreach (var fieldName in AvailableFields)
                    {
                        predicate = predicate.Or(obj => EF.Property<string>(obj, fieldName).Contains(Filter));
                    }

                    query = query.Where(predicate);
                }
            }
            CountItems = await query.CountAsync();
            Organization = await query.OrderBy(o => o.Id).Skip(Skip).Take(PageSize).ToListAsync();
        }

        public async Task OnPostFilterAsync()
        {
            if (_context.Organisations != null)
            {
                var query = _context.Organisations.AsQueryable();
                // get string type property names
                await GetItems(query);

            }
        }
    }
}
