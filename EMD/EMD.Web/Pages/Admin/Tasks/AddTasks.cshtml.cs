using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class AddTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;

        public AddTasksModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [BindProperty]
        public TasksViewModel TaskViewModel { get; set; } = new TasksViewModel();

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var task = new Models.Domain.Tasks
                {
                    Id = Guid.NewGuid(),
                    Title = TaskViewModel.Title,
                    Description = TaskViewModel.Description,
                    Deadline = TaskViewModel.Deadline,
                    IsCompleted = false
                };

                await _taskRepository.AddTaskAsync(task);

                return RedirectToPage();
            }

            return Page();
        }
    }
}