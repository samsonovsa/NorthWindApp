using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.BLL.Interfaces;
using AutoMapper;
using NorthWindApp.Models.ViewModels;
using System.Collections.Generic;
using NorthWindApp.DTO.Models;
using System.Linq;

namespace NorthWindApp.Controllers
{
    public class CategoryController : Controller
    {
        readonly IDictionaryService _dictionaryService;
        readonly IGenericCacheService<byte[]> _cacheImage;
        readonly IMapper _mapper;

        public CategoryController(
            IDictionaryService dictionaryService, 
            IGenericCacheService<byte[]> cacheImage, 
            IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _cacheImage = cacheImage;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var categories =  _mapper.Map<IEnumerable<CategoryViewModel>>(
                await _dictionaryService.GetCategoriesAsync());
            return View(categories);
        }

        public async Task<ActionResult> Image(int id)
        {
            var categories = _mapper.Map<IEnumerable<CategoryViewModel>>(
                await _dictionaryService.GetCategoriesAsync());
            var category = categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        public async Task<ActionResult> GetImage(int id)
        {
            var category = new CategoryViewModel() { Id = id };
            category.Picture =  await _dictionaryService.CategoryGetPictureAsync(id);
            return File(category.Image, "image/png");
        }

        public async Task<ActionResult> UploadImage(CategoryViewModel category)
        {
            await _dictionaryService.CategoryUpdateAsync(_mapper.Map<Category>( category));
            await _cacheImage.ClearAsync();
            return RedirectToAction("Image", new { id = category.Id});
        }

        protected override void Dispose(bool disposing)
        {
            _dictionaryService.Dispose();
            _cacheImage.ClearAsync();
            base.Dispose(disposing);
        }
    }
}