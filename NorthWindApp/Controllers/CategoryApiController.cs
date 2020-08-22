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
    [ApiController]
    [Route("api/category")]
    public class CategoryApiController : ControllerBase
    {
        IDictionaryService _dictionaryService;
        IMapper _mapper;

        public CategoryApiController(
            IDictionaryService dictionaryService, 
            IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryApiViewModel>>> GetAsync()
        {
            var categories = _mapper.Map<IEnumerable<CategoryApiViewModel>>(
                await _dictionaryService.GetCategoriesAsync());
            return categories.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryApiViewModel>> Get(int id)
        {
            var categories = _mapper.Map<IEnumerable<CategoryApiViewModel>>(
                await _dictionaryService.GetCategoriesAsync());
            var category = categories.FirstOrDefault(c => c.Id == id);
            return category;
        }
    }
}