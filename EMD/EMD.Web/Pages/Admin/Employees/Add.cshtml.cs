using EMD.Web.Data;
using EMD.Web.Migrations;
using EMD.Web.Models.Domain;
using EMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages.Admin.Employees
{
    public class AddEmployeesModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        [BindProperty]
        public AddEmployee AddEmployeeRequset { get; set; }

        public AddEmployeesModel(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public void OnGet()
        {

        }

        public void OnPost()
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
            _eMDDbContext.Emds.Add(employee);
            _eMDDbContext.SaveChanges();
        }
    }
}
