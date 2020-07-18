using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorthWindApp.Models.DataModels;
using NorthWindApp.Models.Entities;
using NorthWindApp.Models.ViewModels;

namespace NorthWindApp.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;
        IConfiguration _configuration;

        public ProductController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ActionResult> Index()
        {
            var products = await Task<IEnumerable<Product>>
                .Run(() => _unitOfWork.Products
                    .GetWithInclude(p => p.Category, p => p.Supplier));

            var productsViewModel = new ProductsViewModel(_configuration);
            productsViewModel.SetProducts(products);

            return View(productsViewModel.Products);
        }

        [HttpGet()]
        public ActionResult Create()
        {
            return  View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                    await Task.Run(() => _unitOfWork.Products.Create(product));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet()]
        public async Task<ActionResult> Update(int id)
        {
            Product product = await Task.Run(()=> _unitOfWork.Products.FindById(id));
            return View(product);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task Update(Product product)
        {
            if (ModelState.IsValid)
                await Task.Run(() => _unitOfWork.Products.Update(product));
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Product product)
        {
            try
            {
                await Task.Run(() => _unitOfWork.Products.Remove(product));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
