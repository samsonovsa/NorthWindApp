using NorthWindApp.DAL.EF;
using NorthWindApp.DAL.Interfaces;
using NorthWindApp.DTO.Models;
using System;

namespace NorthWindApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private NorthwindContext _context;

        public IGenericRepository<Product> Products { get; set; }
        public ICategoryRepository Categories { get; set; }
        public ISupplierRepository Suppliers { get; set; }

        public UnitOfWork(NorthwindContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Suppliers = new SupplierRepository(_context);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

        #region IDisposable impliment
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
