using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EMD.Web.Pages.Admin.Employees
{
    public class ViewModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        public Emd Employee { get; set; }

        public ViewModel(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Employee = await _eMDDbContext.Emds.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
