namespace EMD.Web.Models.ViewModels
{
    public class TasksViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public List<Guid>? SelectedEmployeeIds { get; set; }
    }
}
