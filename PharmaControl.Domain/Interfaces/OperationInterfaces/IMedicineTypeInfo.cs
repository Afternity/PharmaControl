using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.OperationInterfaces
{
    public interface IMedicineTypeInfo
    {
        Task<MedicineType> GetMedicineTypeAsync(Medicine model);
    }
}
