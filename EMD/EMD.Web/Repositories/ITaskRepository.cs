using EMD.Web.Models.Domain;
using EMD.Web.Models.ViewModels;

namespace EMD.Web.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetTasksAsync();
        Task<IEnumerable<Tasks>> SearchAsync(string searchTerm);
        Task<Tasks> GetTaskByIdAsync(Guid taskId);
        Task AddTaskAsync(Tasks task, List<Guid> employeeIds = null);
        Task UpdateTaskAsync(Tasks task, List<Guid> employeeIds);
        Task<bool> MarkTaskAsCompletedAsync(Guid taskId);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
