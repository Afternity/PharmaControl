using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace PharmaControl.Domain.Models
{
    public class PharmacyStock
    {
        public Guid PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; } = null!;
        public Guid MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; } = null!;

        public int Quantity { get; set; }
        public int MinStockLevel { get; set; } = 10;
        public int ReorderQuantity { get; set; } = 50;

        public virtual ICollection<Sale> Sales { get; set; } = []; 
    }
}
