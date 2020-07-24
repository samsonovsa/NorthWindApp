using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.BLL.Interfaces;

namespace NorthWindApp.Controllers
{
    public class CategoryController : Controller
    {
        IDictionaryService _dictionaryService;

        public CategoryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await _dictionaryService.GetCategoriesAsync();
            return View(categories);
        }

        protected override void Dispose(bool disposing)
        {
            _dictionaryService.Dispose();
            base.Dispose(disposing);
        }
    }
}