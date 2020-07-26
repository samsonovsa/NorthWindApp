using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.BLL.Interfaces;
using AutoMapper;
using NorthWindApp.Models.ViewModels;
using System.Collections.Generic;

namespace NorthWindApp.Controllers
{
    public class CategoryController : Controller
    {
        IDictionaryService _dictionaryService;
        IMapper _mapper;

        public CategoryController(IDictionaryService dictionaryService, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var categories =  _mapper.Map<IEnumerable<CategoryViewModel>>(
                await _dictionaryService.GetCategoriesAsync());
            return View(categories);
        }

        protected override void Dispose(bool disposing)
        {
            _dictionaryService.Dispose();
            base.Dispose(disposing);
        }
    }
}