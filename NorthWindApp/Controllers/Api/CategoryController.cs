using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.BLL.Interfaces;
using AutoMapper;
using NorthWindApp.Models.Api;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NorthWindApp.Models.ViewModels;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.Controllers.Api
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;
        private readonly IMapper _mapper;

        public CategoryController(
            IDictionaryService dictionaryService, 
            IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryApi>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryApi>>> GetAsync()
        {
            var categories = _mapper.Map<IEnumerable<CategoryApi>>(
                await _dictionaryService.GetCategoriesAsync());

            if (categories == null)
                return NotFound();

            return categories.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryApi), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryApi>> Get(int id)
        {
            var categories = _mapper.Map<IEnumerable<CategoryApi>>(
                await _dictionaryService.GetCategoriesAsync());
            var category = categories?.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet("image/{id}")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<byte[]>> GetImage(int id)
        {
            var category = new CategoryViewModel()
            {
                Id = id,
                Picture = await _dictionaryService.CategoryGetPictureAsync(id)
            };
            
            if (category.Picture == null)
                return NotFound();

            return category.Picture;
        }

        [HttpPut("image/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateImage(int id, IFormFile image)
        {
            if (image == null)
                return NotFound();

            var categories = await _dictionaryService.GetCategoriesAsync();
            var category = _mapper.Map<CategoryViewModel>(
                categories.FirstOrDefault(c => c.Id == id));

            if (category == null)
                return NotFound();

            category.ImageUpload = image;

            await _dictionaryService.CategoryUpdateAsync(_mapper.Map<Category>(category));

            return NoContent();
        }
    }
}