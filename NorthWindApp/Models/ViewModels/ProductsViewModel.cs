using Microsoft.Extensions.Configuration;
using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.ViewModels
{
    public class ProductsViewModel
    {
        private int _maxProductCountOnPage;

        public ProductsViewModel(IConfiguration configuration)
        {
            int.TryParse(configuration["MaxProductCountOnPage"],out _maxProductCountOnPage);
            Products = new List<Product>();
        }

        public int CountProductOnPage => _maxProductCountOnPage;

        public List<Product> Products { get; set; }

        public void SetProducts(IEnumerable<Product> products)
        {
            Products = CountProductOnPage > 0 ? 
                products.Take(CountProductOnPage).ToList()
                : products.ToList();            
        }

    }
}
