namespace EMD.Web.Models.Domain
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<Emd> Assignees { get; set; }
    }
}
