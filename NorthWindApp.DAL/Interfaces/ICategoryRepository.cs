using NorthWindApp.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWindApp.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<byte[]> GetPictureAsync(int id);
        Task UpdateCategoryAsync(Category category);
    }
}
