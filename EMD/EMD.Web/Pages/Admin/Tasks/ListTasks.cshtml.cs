using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMD.Web.Models.ViewModels;
using System.Collections.Generic;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class ListTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;

        public ListTasksModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<TasksViewModel> TaskList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var tasks = await _taskRepository.GetTasksAsync();
            TaskList = new List<TasksViewModel>();

            foreach (var task in tasks)
            {
                TaskList.Add(new TasksViewModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    IsCompleted = task.IsCompleted
                });
            }

            return Page();
        }
    }
}
