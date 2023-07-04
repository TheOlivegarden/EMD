using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Pages.Admin.Employees
{
    public class ListModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        public List<Emd> EmdList { get; set; }

        public ListModel(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public async Task OnGet()
        {
            EmdList = await _eMDDbContext.Emds.ToListAsync();
        }
    }
}
