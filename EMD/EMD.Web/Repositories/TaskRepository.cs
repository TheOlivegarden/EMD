using EMD.Web.Data;
using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly EMDDbContext _context;

        public TaskRepository(EMDDbContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(Tasks task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }
    }
}
