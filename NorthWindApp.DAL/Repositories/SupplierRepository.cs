using NorthWindApp.DAL.EF;
using NorthWindApp.DAL.Interfaces;
using NorthWindApp.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWindApp.DAL.Repositories
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
            return await GetAsync();
        }
    }
}
