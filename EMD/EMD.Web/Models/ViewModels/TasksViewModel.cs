namespace EMD.Web.Models.ViewModels
{
    public class TasksViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
    }
}
