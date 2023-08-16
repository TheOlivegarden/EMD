using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMD.Web.Models.ViewModels;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class ViewTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;

        public ViewTasksModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TasksViewModel TaskViewModel { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            TaskViewModel = new TasksViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                IsCompleted = task.IsCompleted
            };

            return Page();
        }
    }
}
