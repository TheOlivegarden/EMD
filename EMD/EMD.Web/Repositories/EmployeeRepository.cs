using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMD.Web.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EMDDbContext _eMDDbContext;

        public EmployeeRepository(EMDDbContext eMDDbContext)
        {
            _eMDDbContext = eMDDbContext;
        }

        public async Task<Emd> AddAsync(Emd employee)
        {
            await _eMDDbContext.Emds.AddAsync(employee);
            await _eMDDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingEmployee = await _eMDDbContext.Emds.FindAsync(id);

            if (existingEmployee != null)
            {
                _eMDDbContext.Emds.Remove(existingEmployee);
                await _eMDDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Emd>> GetAllAsync()
        {
            return await _eMDDbContext.Emds.ToListAsync();
        }

        public async Task<Emd> GetAsync(Guid id)
        {
            return await _eMDDbContext.Emds.FindAsync(id);
        }

        public async Task<Emd> UpdateAsync(Emd employee)
        {
            var existingEmployee = await _eMDDbContext.Emds.FindAsync(employee.Id);

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

        public async Task<IEnumerable<Emd>> SearchAsync(string searchTerm)
        {
            return await _eMDDbContext.Emds
                .Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm) || e.Department.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
