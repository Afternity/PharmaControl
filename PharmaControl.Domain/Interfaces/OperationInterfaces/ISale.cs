using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.OperationInterfaces
{
    public interface ISale
    {
        Task CreateAsync(Sale model);
    }
}
