using AutoMapper;
using NorthWindApp.DTO.Models;
using NorthWindApp.Models.Api;

namespace NorthWindApp.Models.ViewModels.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CategoryApi>();
            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductApi>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<CategoryApi, Category>();
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductApi, Product>();
        }
    }
}
