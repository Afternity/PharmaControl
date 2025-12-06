namespace PharmaControl.Domain.Models
{
    public class Employee
        : BaseModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public Guid PharmacyId { get; set; }  
        public virtual Pharmacy Pharmacy { get; set; } = null!;
    }
}
