using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NorthWindApp.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NorthWindApp.Models.ViewModels
{
    public class ProductsViewModel
    {
        readonly IDictionaryService _dictionaryService;
        private readonly IMapper _mapper;

        public ProductsViewModel(IDictionaryService dictionaryService, ILogger logger, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;

            SetProducts();
            SetCategories();
            SetSuppliers();
        }

        public List<ProductViewModel> Products { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Categories { get; set; }

        private void SetProducts()
        {
            Products = _mapper.Map<List<ProductViewModel>>(_dictionaryService.GetProductsAsync().Result);
        }

        private void SetCategories()
        {
            var categories = _dictionaryService.GetCategoriesAsync().Result;
            Categories = categories.Select(
                c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }

        private void SetSuppliers()
        {
            var suppliers = _dictionaryService.GetSuppliersAsync().Result;
            Suppliers = suppliers.Select(
                s => new SelectListItem { Value = s.SupplierId.ToString(), Text = s.CompanyName })
                .ToList();
        }
    }
}
