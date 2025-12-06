using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.OperationInterfaces
{
    public interface IReadPharmacy
    {
        Task<IList<Pharmacy>> GetAllAsync();

        Task<IList<Pharmacy>> GetAllByMedicineAsync(
            Medicine model);
    }
}
