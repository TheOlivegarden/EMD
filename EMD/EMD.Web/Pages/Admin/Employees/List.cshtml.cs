using EMD.Web.Models.Domain;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages.Admin.Employees
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ListModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Employee> EmdList { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public bool ShowNotification { get; set; }

        public bool IsPostBack => HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);
        
        public ListModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                EmdList = (await _employeeRepository.SearchAsync(SearchTerm))?.ToList();
            }
            else
            {
                EmdList = (await _employeeRepository.GetAllAsync())?.ToList();
            }

            foreach (var employee in EmdList)
            {
                employee.Tasks = (List<Models.Domain.Tasks>)await _employeeRepository.GetTasksForEmployeeAsync(employee.Id);
            }

            if (!IsPostBack)
            {
                ShowNotification = !string.IsNullOrEmpty(SuccessMessage) || !string.IsNullOrEmpty(ErrorMessage);
            }
            else
            {
                ShowNotification = false;
            }

            return Page();
        }
    }
}