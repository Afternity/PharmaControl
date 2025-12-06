using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.OperationInterfaces
{
    public interface IReadMedicine
    {
        Task<IList<Medicine>> GetAllAsync(
            Pharmacy model);
    }
}
