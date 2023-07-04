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

        public async Task<Emd> AddAsync(Emd Employee)
        {
            await _eMDDbContext.Emds.AddAsync(Employee);
            await _eMDDbContext.SaveChangesAsync();

            return Employee;
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

        public async Task<Emd> UpdateAsync(Emd Employee)
        {
            var existingEmployee = await _eMDDbContext.Emds.FindAsync(Employee.Id);

            if (existingEmployee != null)
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

            return existingEmployee;
        }
    }
}
