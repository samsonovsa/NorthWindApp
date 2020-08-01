using NorthWindApp.DAL.EF;
using NorthWindApp.DAL.Interfaces;
using NorthWindApp.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWindApp.DAL.Repositories
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
            return await GetAsync();
        }

        public async Task<byte[]> GetPictureAsync(int id)
        {
            var category = await FindByIdAsync(id);
            return category.Picture;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await UpdateAsync(category);
        }
    }
}
