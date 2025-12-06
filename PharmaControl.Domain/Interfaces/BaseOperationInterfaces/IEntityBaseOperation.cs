using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaControl.Domain.Interfaces.BaseInterfaces
{
    public interface IEntityBaseOperation<T>
    {
        Task CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(T model);
        Task GetAllAsync();
    }
}
