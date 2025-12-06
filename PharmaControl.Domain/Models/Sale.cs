namespace PharmaControl.Domain.Models
{
    public class Sale
        : BaseModel
    {
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public int Amount { get; set; } = 0;
        public PaymentState PaymentMethod { get; set; } = PaymentState.Card;

        public Guid PharmacyId { get; set; }
        public Guid MedicineId { get; set; }
        public virtual PharmacyStock PharmacyStock { get; set; } = null!;
    }

    public enum PaymentState
    {
        Cash,
        Card,
        Insurance
    }
}
