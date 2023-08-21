using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EMDDbContext _eMDDbContext;

        public EmployeeRepository(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _eMDDbContext.Employees.AddAsync(employee);
            await _eMDDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingEmployee = await _eMDDbContext.Employees.FindAsync(id);

            if (existingEmployee != null)
            {
                _eMDDbContext.Employees.Remove(existingEmployee);
                await _eMDDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _eMDDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            return await _eMDDbContext.Employees.FindAsync(id);
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var existingEmployee = await _eMDDbContext.Employees.FindAsync(employee.Id);

            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Surname = employee.Surname;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Address = employee.Address;
                existingEmployee.Department = employee.Department;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.Description = employee.Description;
            }

            await _eMDDbContext.SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string searchTerm)
        {
            return await _eMDDbContext.Employees
                .Where(e => e.Name.Contains(searchTerm) ||
                            e.Email.Contains(searchTerm) ||
                            e.Department.Contains(searchTerm) ||
                            e.BirthDate.ToString().Contains(searchTerm))
                .ToListAsync();
        }
    }
}
