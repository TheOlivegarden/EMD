using EMD.Web.Data;
using EMD.Web.Models.Domain;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EMD.Web.Pages.Admin.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty]
        public Emd Employee { get; set; }

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task OnGet(Guid id)
        {
            Employee = await _employeeRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            var updatedEmployee = await _employeeRepository.UpdateAsync(Employee);

            if (updatedEmployee != null)
            {
                TempData["SuccessMessage"] = "Changes were successfully saved.";
                return RedirectToPage("/Admin/Employees/List");
            }

            TempData["ErrorMessage"] = "Failed to update employee details.";
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _employeeRepository.DeleteAsync(Employee.Id);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Employee was successfully deleted.";
                return RedirectToPage("/Admin/Employees/List");
            }

            TempData["ErrorMessage"] = "Failed to delete employee.";
            return Page();
        }
    }
}
