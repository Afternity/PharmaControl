namespace PharmaControl.Domain.Models
{
    public class Medicine 
        : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool RequiresPrescription { get; set; }
        public DateTime BestBeforeDate { get; set; }

        public Guid MedicineTypeId { get; set; }
        public virtual MedicineType MedicineType { get; set; } = null!;

        public virtual ICollection<PharmacyStock> PharmacyStocks { get; set; } = [];

    }
}
