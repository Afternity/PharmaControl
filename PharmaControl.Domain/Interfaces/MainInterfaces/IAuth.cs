using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.MainInterfaces
{
    public interface IAuth
    {
        Task<Employee> AuthAsync(
            string email,
            string password);
    }
}
