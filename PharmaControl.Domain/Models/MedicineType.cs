namespace PharmaControl.Domain.Models
{
    public class MedicineType 
        : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Medicine> Medicines { get; set; } = [];
    }
}
