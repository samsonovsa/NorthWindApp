using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.Models.DataModels;
using NorthWindApp.Models.Entities;

namespace NorthWindApp.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Index()
        {
            var products = await Task<IEnumerable<Product>>
                .Run(()=> _unitOfWork.Products
                    .GetWithInclude(p=>p.Category,p=> p.Supplier));

            return View(products);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
