using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NorthWindApp.Models.DataModels;
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
        private IUnitOfWork _unitOfWork;

        public ProductsViewModel(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;

            int.TryParse(configuration["MaxProductCountOnPage"],out _maxProductCountOnPage);
            SetProducts();
            SetCategories();
            SetSuppliers();
        }

        public int CountProductOnPage => _maxProductCountOnPage;

        public List<Product> Products { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Categories { get; set; }

        private void SetProducts()
        {
            var products = _unitOfWork.Products.GetWithInclude(p => p.Category, p => p.Supplier);

            Products = CountProductOnPage > 0 ? 
                products.Take(CountProductOnPage).ToList()
                : products.ToList();            
        }

        private void SetCategories()
        {
            var categories =  _unitOfWork.Categories.GetCategories();
            Categories = categories.Select(
                c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }

        public void SetSuppliers()
        {
            var suppliers = _unitOfWork.Suppliers.GetSuppliers();
            Suppliers = suppliers.Select(
                s => new SelectListItem { Value = s.SupplierId.ToString(), Text = s.CompanyName })
                .ToList();
        }

    }
}
