using NorthWindApp.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindApp.BLL.Interfaces
{
    public interface IDictionaryService: IDisposable
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Supplier>> GetSuppliersAsync();

        Task ProductCreateAsync(Product product);
        Task ProductDeleteAsync(int id);
        Task ProductUpdateAsync(Product product);
        Task<Product> ProductFindById(int id);
    }
}
