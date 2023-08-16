using EMD.Web.Models.Domain;
using EMD.Web.Models.ViewModels;
using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class EditTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EditTasksModel(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public TasksViewModel TaskViewModel { get; set; } = new TasksViewModel();

        public List<Emd> AvailableEmployees { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            TaskViewModel.Id = task.Id;
            TaskViewModel.Title = task.Title;
            TaskViewModel.Description = task.Description;
            TaskViewModel.Deadline = task.Deadline;
            TaskViewModel.IsCompleted = task.IsCompleted;

            AvailableEmployees = (await _employeeRepository.GetAllAsync()).ToList();

            if (task.Assignees != null)
            {
                TaskViewModel.SelectedEmployeeIds = task.Assignees.Select(e => e.Id).ToList();
            }
            else
            {
                TaskViewModel.SelectedEmployeeIds = new List<Guid>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input data.";
                return Page();
            }

            var task = new Models.Domain.Tasks
            {
                Id = TaskViewModel.Id,
                Title = TaskViewModel.Title,
                Description = TaskViewModel.Description,
                Deadline = TaskViewModel.Deadline,
                IsCompleted = TaskViewModel.IsCompleted
            };

            var employeeIds = TaskViewModel.SelectedEmployeeIds;

            var isMarkedAsCompleted = await _taskRepository.MarkTaskAsCompletedAsync(task.Id);

            if (!isMarkedAsCompleted)
            {
                TempData["ErrorMessage"] = "Task wasn't marked.";
                return Page();
            }

            await _taskRepository.UpdateTaskAsync(task, employeeIds);

            return RedirectToPage("/Admin/Tasks/ListTasks");
        }


        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _taskRepository.DeleteTaskAsync(TaskViewModel.Id);

            if (deleted)
            {
                return RedirectToPage("/Admin/Tasks/ListTasks");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete task.";
                return Page();
            }
        }
    }
}