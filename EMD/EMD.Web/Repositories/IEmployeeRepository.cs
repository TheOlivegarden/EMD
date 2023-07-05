using EMD.Web.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMD.Web.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Emd>> GetAllAsync();
        Task<IEnumerable<Emd>> SearchAsync(string searchTerm);
        Task<Emd> GetAsync(Guid id);
        Task<Emd> AddAsync(Emd employee);
        Task<Emd> UpdateAsync(Emd employee);
        Task<bool> DeleteAsync(Guid id);
    }
}