using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.DTO.Models;
using NorthWindApp.Models.ViewModels;

namespace NorthWindApp.Controllers
{
    public class ProductController : Controller
    {
        IDictionaryService _dictionaryService;
        ILogger _logger;
        IMapper _mapper;

        public ProductController(IDictionaryService dictionaryService, ILogger<ProductController> logger, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var productsViewModel = await Task.Run(
                ()=> new ProductsViewModel(_dictionaryService, _logger, _mapper));

            return  View(productsViewModel);
        }

        [HttpGet()]
        public async Task<ActionResult> Create()
        {
            var productsViewModel = await Task.Run(
                () => new ProductsViewModel(_dictionaryService, _logger, _mapper));
            ViewBag.Categories = productsViewModel.Categories;
            ViewBag.Suppliers = productsViewModel.Suppliers;

            return  View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dictionaryService.ProductCreateAsync(_mapper.Map<Product>(productViewModel));
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
            Product product = await _dictionaryService.ProductFindByIdAsync(id); 
            if (product != null)
            {
                var productsViewModel = new ProductsViewModel(_dictionaryService, _logger, _mapper);
                ViewBag.Categories = productsViewModel.Categories;
                ViewBag.Suppliers = productsViewModel.Suppliers;

                return View(_mapper.Map<ProductViewModel>(product));
            }
            else
                return BadRequest($"Error finding product with id = {id} ");
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dictionaryService.ProductUpdateAsync(_mapper.Map<Product>(productViewModel));

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Error));
                }

            }
            else
            {
                var productsViewModel = new ProductsViewModel(_dictionaryService, _logger, _mapper);
                ViewBag.Categories = productsViewModel.Categories;
                ViewBag.Suppliers = productsViewModel.Suppliers;

                return View(productViewModel);
            }

        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _dictionaryService.ProductDeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }

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
            _dictionaryService.Dispose();
            base.Dispose(disposing);
        }

    }
}
