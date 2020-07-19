using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
