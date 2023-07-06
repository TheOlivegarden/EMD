using EMD.Web.Data;
using EMD.Web.Models.Domain;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMD.Web.Pages.Admin.Employees
{
    public class ListModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Emd> EmdList { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public ListModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                EmdList = (await _employeeRepository.SearchAsync(SearchTerm))?.ToList();
            }
            else
            {
                EmdList = (await _employeeRepository.GetAllAsync())?.ToList();
            }
        }
    }
}
