using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces
{
    public interface IMedicineTypeInfo
    {
        Task<MedicineType> GetMedicineTypeAsync(Medicine model);
    }
}
