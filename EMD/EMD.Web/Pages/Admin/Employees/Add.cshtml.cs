using EMD.Web.Models.Domain;
using EMD.Web.Models.ViewModels;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace EMD.Web.Pages.Admin.Employees
{
    public class AddEmployeesModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty]
        public AddEmployee AddEmployeeRequset { get; set; }

        public AddEmployeesModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var employee = new Emd()
            {
                Name = AddEmployeeRequset.Name,
                Surname = AddEmployeeRequset.Surname,
                Email = AddEmployeeRequset.Email,
                Phone = AddEmployeeRequset.Phone,
                Address = AddEmployeeRequset.Address,
                Department = AddEmployeeRequset.Department,
                BirthDate = AddEmployeeRequset.BirthDate,
                Description = AddEmployeeRequset.Description,
            };

            if (employee.BirthDate > DateTime.Now || employee.BirthDate < DateTime.Now.AddYears(-65))
            {
                ModelState.AddModelError("AddEmployeeRequest.BirthDate", "Please enter a date within the allowed range.");
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