using EMD.Web.Models.Domain;

namespace EMD.Web.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> SearchAsync(string searchTerm);
        Task<Employee> GetAsync(Guid id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(Guid id);
    }
}