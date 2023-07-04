using EMD.Web.Data;
using EMD.Web.Migrations;
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
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;


        [BindProperty]
        public AddEmployee AddEmployeeRequset { get; set; }

        public AddEmployeesModel(IEmployeeRepository employeeRepository, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _employeeRepository = employeeRepository;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
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

            await _employeeRepository.AddAsync(employee);

            var tempData = _tempDataDictionaryFactory.GetTempData(HttpContext);
            if (employee.Id != Guid.Empty)
            {
                tempData["SuccessMessage"] = "Employee was successfully added.";
            }
            else
            {
                tempData["ErrorMessage"] = "Failed to add employee.";
            }

            return RedirectToPage("/Admin/Employees/List");
        }
    }
}
