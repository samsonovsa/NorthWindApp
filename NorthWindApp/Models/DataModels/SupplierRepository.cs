using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class SupplierRepository: GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(NorthwindContext context)
         : base(context) { }


        public IEnumerable<Supplier> GetSuppliers()
        {
            return Get();
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await Task.Run(() => Get());
        }
    }
}
