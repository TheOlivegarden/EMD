using EMD.Web.Models.Domain;

namespace EMD.Web.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Emd>> GetAllAsync();
        Task<Emd> GetAsync(Guid id);
        Task<Emd> AddAsync(Emd Employee);
        Task<Emd> UpdateAsync(Emd Employee);
        Task<bool> DeleteAsync(Guid id);
    }
}
