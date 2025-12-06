using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces
{
    public interface IPharmacyStockInfo
    {
        Task<PharmacyStock> GetPharmacyStockAsync(
            Medicine medicine,
            Pharmacy pharmacy);
    }
}
