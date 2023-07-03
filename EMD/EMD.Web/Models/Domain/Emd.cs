namespace EMD.Web.Models.Domain
{
    public class Emd
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
    }
}
