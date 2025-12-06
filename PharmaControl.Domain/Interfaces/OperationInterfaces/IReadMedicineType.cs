using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.OperationInterfaces
{
    public interface IReadMedicineType
    {
        Task<IList<MedicineType>> GetAllAsync(
            Pharmacy model);
    }
}
