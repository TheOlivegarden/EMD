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
        public async Task<IEnumerable<Tasks>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        public async Task AddTaskAsync(Tasks task, List<Guid> employeeIds)
        {
            _context.Tasks.Add(task);

            if (employeeIds != null && employeeIds.Any())
            {
                task.Employees = await _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync();
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdateTaskAsync(Tasks task, List<Guid> employeeIds)
        {
            var existingTask = await _context.Tasks
                .Include(t => t.Employees)
                .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Deadline = task.Deadline;
                existingTask.IsCompleted = task.IsCompleted;

                existingTask.Employees.Clear();
                if (employeeIds != null)
                {
                    var employees = await _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync();
                    foreach (var employee in employees)
                    {
                        existingTask.Employees.Add(employee);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MarkTaskAsCompletedAsync(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return false;
            }



            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
