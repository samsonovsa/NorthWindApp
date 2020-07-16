using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class ProductRepository: GenericRepository<Product>
    {
        public ProductRepository(NorthwindContext context)
            : base(context) { }
    }
}
