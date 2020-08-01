using Microsoft.Extensions.Logging;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.DAL.Interfaces;
using NorthWindApp.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.BLL.Services
{
    public class DictionaryService : IDictionaryService
    {
        IUnitOfWork _unitOfWork;
        int _countItemsOnPage;
        ILogger _logger;

        public DictionaryService(IUnitOfWork unitOfWork, ILogger<DictionaryService> logger, int countItemsOnPage)
        {
            _unitOfWork = unitOfWork;
            _countItemsOnPage = countItemsOnPage;
            _logger = logger ;

            _logger.LogInformation($"Read configuration MaxProductCountOnPage = {countItemsOnPage}");
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetCategoriesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await Task.Run(()=> _unitOfWork.Products.GetWithInclude(p => p.Category, p => p.Supplier));

            return _countItemsOnPage > 0 ?
                products.Take(_countItemsOnPage).ToList()
                : products;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _unitOfWork.Suppliers.GetSuppliersAsync();
        }

        public async Task ProductCreateAsync(Product product)
        {
            await _unitOfWork.Products.CreateAsync(product);
        }

        public async Task ProductDeleteAsync(int id)
        {
            Product product = await _unitOfWork.Products.FindByIdAsync(id);
            if (product != null)
            {
                try
                {
                    await _unitOfWork.Products.RemoveAsync(product);
                }
                catch
                {
                    throw new Exception("Error product delete");
                }
            }
        }

        public async Task ProductUpdateAsync(Product product)
        {
            await _unitOfWork.Products.UpdateAsync(product);
        }

        public async Task<Product> ProductFindByIdAsync(int id)
        {
           return await Task.Run(() => _unitOfWork.Products.FindById(id));
        }

        public async Task<byte[]> CategoryGetPictureAsync(int id)
        {
            return await _unitOfWork.Categories.GetPictureAsync(id);
        }

        public async Task CategoryUpdateAsync(Category category)
        {
            await _unitOfWork.Categories.UpdateCategoryAsync(category);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
