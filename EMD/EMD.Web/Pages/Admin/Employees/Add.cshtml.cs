using EMD.Web.Models.Domain;
using EMD.Web.Models.ViewModels;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EMD.Web.Pages.Admin.Employees
{
    public class AddEmployeesModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty]
        public AddEmployee AddEmployeeRequest { get; set; }

        public AddEmployeesModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var employee = new Employee()
            {
                Name = AddEmployeeRequest.Name,
                Surname = AddEmployeeRequest.Surname,
                Email = AddEmployeeRequest.Email,
                Phone = AddEmployeeRequest.Phone,
                Address = AddEmployeeRequest.Address,
                Department = AddEmployeeRequest.Department,
                BirthDate = AddEmployeeRequest.BirthDate,
                Description = AddEmployeeRequest.Description,
                DateCreated = DateTime.Now
            };

            if (employee.BirthDate > DateTime.Now || employee.BirthDate < DateTime.Now.AddYears(-65))
            {
                TempData["InvalidDate"] = "Invalid Date.";
                return Page();
            }

            await _employeeRepository.AddAsync(employee);

            if (employee.Id != Guid.Empty)
            {
                TempData["SuccessMessage"] = "Employee was successfully added.";
                return RedirectToPage("/Admin/Employees/List");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add employee.";
                return Page();
            }
        }
    }
}