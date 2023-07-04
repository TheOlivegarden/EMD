using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EMD.Web.Pages.Admin.Employees
{
    public class EditModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        [BindProperty]
        public Emd Employee { get; set; }

        public EditModel(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public async Task OnGet(Guid id)
        {
            Employee =  await _eMDDbContext.Emds.FindAsync(id);
        }
        public async Task<IActionResult> OnPostUpdate() 
        {
            var existingEmployee = await _eMDDbContext.Emds.FindAsync(Employee.Id);

            if(existingEmployee != null)
            {
                existingEmployee.Name = Employee.Name;
                existingEmployee.Surname = Employee.Surname;
                existingEmployee.Email = Employee.Email;
                existingEmployee.Phone = Employee.Phone;
                existingEmployee.Address = Employee.Address;
                existingEmployee.Department = Employee.Department;
                existingEmployee.BirthDate = Employee.BirthDate;
                existingEmployee.Description = Employee.Description;
            }

            await _eMDDbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Employees/List");

        }

        public async Task<IActionResult> OnPostDelete() 
        {
            var existingEmployee = await _eMDDbContext.Emds.FindAsync(Employee.Id);
            
            if(existingEmployee != null)
            {
                _eMDDbContext.Emds.Remove(existingEmployee);
                await _eMDDbContext.SaveChangesAsync();

                return RedirectToPage("/Admin/Employees/List");
            }

            return Page();
        }
    }
}