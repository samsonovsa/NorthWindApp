using NorthWindApp.DTO.Models;
using System;

namespace NorthWindApp.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Categories { get; set; }
        IGenericRepository<Product> Products { get; set; }
        ISupplierRepository Suppliers { get; set; }
        void Save();   
    }
}
