using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NorthWindApp.Models.DataModels;
using NorthWindApp.Models.Entities;
using NorthWindApp.Models.ViewModels;

namespace NorthWindApp.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;
        IConfiguration _configuration;
        ILogger _logger;

        public ProductController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }

        public ActionResult Index()
        {
            //var products = await Task<IEnumerable<Product>>
            //    .Run(() => _unitOfWork.Products
            //        .GetWithInclude(p => p.Category, p => p.Supplier));

            var productsViewModel = new ProductsViewModel(_unitOfWork, _configuration, _logger);
            //productsViewModel.SetProducts(products);

            return View(productsViewModel);
        }

        [HttpGet()]
        public ActionResult Create()
        {
            var productsViewModel = new ProductsViewModel(_unitOfWork, _configuration, _logger);
            ViewBag.Categories = productsViewModel.Categories;
            ViewBag.Suppliers = productsViewModel.Suppliers;

            return  View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _unitOfWork.Products.Create(product));
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet()]
        public async Task<ActionResult> Update(int id)
        {
            Product product = await Task.Run(()=> _unitOfWork.Products.FindById(id));
            if (product != null)
            {
                var productsViewModel = new ProductsViewModel(_unitOfWork, _configuration, _logger);
                ViewBag.Categories = productsViewModel.Categories;
                ViewBag.Suppliers = productsViewModel.Suppliers;

                return View(product);
            }
            else
                return View();
            
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _unitOfWork.Products.Update(product));

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    return RedirectToAction(nameof(Error));
                }

            }
            else
            {
                var productsViewModel = new ProductsViewModel(_unitOfWork, _configuration, _logger);
                ViewBag.Categories = productsViewModel.Categories;
                ViewBag.Suppliers = productsViewModel.Suppliers;

                return View(product);
            }

        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await Task.Run(() => _unitOfWork.Products.FindById(id));
            if (product != null)
            {
                try
                {
                    await Task.Run(() => _unitOfWork.Products.Remove(product));

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction(nameof(Error));
                }
            }
            else
                return RedirectToAction(nameof(Error));
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            var error = HttpContext?.Features?.Get<IExceptionHandlerFeature>()?.Error;
            if(error !=null)
                _logger.LogError(error.Message);

            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
