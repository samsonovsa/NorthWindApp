using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(NorthwindContext context)
            : base(context) { }

        public IEnumerable<Category> GetCategories()
        {
            return Get();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await Task.Run(()=> Get());
        }
    }
}
