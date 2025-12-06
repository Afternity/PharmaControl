namespace PharmaControl.Domain.Models
{
    public class Pharmacy
        : BaseModel 
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = [];
        public virtual ICollection<PharmacyStock> PharmacyStocks { get; set; } = [];
    }
}
