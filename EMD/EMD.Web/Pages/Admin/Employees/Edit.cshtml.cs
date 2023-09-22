using EMD.Web.Models.Domain;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages.Admin.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty]
        public Employee Employee { get; set; }

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Employee = await _employeeRepository.GetAsync(id);

            if (Employee == null)
            {
                TempData["ErrorMessage"] = "Employee not found.";
                return RedirectToPage("/Admin/Employees/List");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            if (Employee.BirthDate > DateTime.Now || Employee.BirthDate < DateTime.Now.AddYears(-65))
            {
                TempData["ErrorMessage"] = "Invalid birth date.";
                return RedirectToPage();
            }

            var updatedEmployee = await _employeeRepository.UpdateAsync(Employee);

            if (updatedEmployee != null)
            {
                TempData["SuccessMessage"] = "Employee updated successfully.";
                return RedirectToPage("/Admin/Employees/List");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update employee.";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _employeeRepository.DeleteAsync(Employee.Id);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Employee was successfully deleted.";
                return RedirectToPage("/Admin/Employees/List");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete employee.";
                return Page();
            }
        }
    }
}