using NorthWindApp.DAL.EF;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.DAL.Repositories
{
    public class ProductRepository: GenericRepository<Product>
    {
        public ProductRepository(NorthwindContext context)
            : base(context) { }
    }
}
