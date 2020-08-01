using AutoMapper;
using NorthWindApp.DTO.Models;
using NorthWindApp.Models.ViewModels;

namespace NorthWindApp.Test.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
