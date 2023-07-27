using EMD.Web.Models.Domain;

namespace EMD.Web.Repositories
{
    public interface ITaskRepository
    {
        Task AddTaskAsync(Tasks task);
    }
}
