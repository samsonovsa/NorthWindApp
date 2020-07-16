using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Categories { get; set; }
        IGenericRepository<Product> Products { get; set; }
        void Save();
        void Begin();
        void Commit();
        void Rollback();      
    }
}
