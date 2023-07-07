using EMD.Web.Models.Domain;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EMD.Web.Pages.Admin.Employees
{
    public class ViewModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Emd Employee { get; set; }

        public ViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Employee = await _employeeRepository.GetAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}