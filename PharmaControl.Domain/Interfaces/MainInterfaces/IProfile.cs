using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Interfaces.MainInterfaces
{
    public interface IProfile
    {
        Task UpdateEmployeeAsync(
            Employee model);
        Task<int> GetPharmacySaleCountAsync(
            Employee employee,
            Pharmacy pharmacy);
        Task<Employee> GetEmployeeAsync(
            Employee model);
        Task<Pharmacy> GetEmployeeParmacyAsync(
            Employee model);
    }
}
