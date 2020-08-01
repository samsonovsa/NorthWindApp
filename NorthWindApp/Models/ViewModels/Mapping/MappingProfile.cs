using AutoMapper;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.Models.ViewModels.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
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
