using AutoMapper;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.Models.ViewModels.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CategoryApiViewModel>();
            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductApiViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<CategoryApiViewModel, Category>();
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductApiViewModel, Product>();
        }
    }
}
