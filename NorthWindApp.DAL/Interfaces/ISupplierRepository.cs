using NorthWindApp.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWindApp.DAL.Interfaces
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
