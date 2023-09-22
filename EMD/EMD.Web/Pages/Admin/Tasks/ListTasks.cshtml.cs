using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMD.Web.Models.ViewModels;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class ListTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;

        public ListTasksModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<TasksViewModel> TaskList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var searchedTasks = await _taskRepository.SearchAsync(SearchTerm);
                TaskList = searchedTasks.Select(task => new TasksViewModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    IsCompleted = task.IsCompleted
                }).ToList();
            }
            else
            {
                var allTasks = await _taskRepository.GetTasksAsync();
                TaskList = allTasks.Select(task => new TasksViewModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    IsCompleted = task.IsCompleted
                }).ToList();
            }

            return Page();
        }
    }
}
