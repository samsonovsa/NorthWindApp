using AutoMapper;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.Models.ViewModels.Mapping
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<ProductViewModel, Product>();
        }
    }
}
