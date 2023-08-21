using EMD.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EMD.Web.Models.Domain;

namespace EMD.Web.Pages.Admin.Tasks
{
    public class AddTasksModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddTasksModel(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public TasksViewModel TaskViewModel { get; set; } = new TasksViewModel();

        public List<Employee> AvailableEmployees { get; set; }

        public async Task OnGetAsync()
        {
            AvailableEmployees = (await _employeeRepository.GetAllAsync()).ToList();
        }

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

                List<Guid> employeeIds = TaskViewModel.SelectedEmployeeIds;

                await _taskRepository.AddTaskAsync(task, employeeIds);

                return RedirectToPage();
            }

            AvailableEmployees = (await _employeeRepository.GetAllAsync()).ToList();
            return Page();
        }

    }
}
