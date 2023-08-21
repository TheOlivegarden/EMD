namespace EMD.Web.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Tasks> Tasks { get; set; }

    }
}
