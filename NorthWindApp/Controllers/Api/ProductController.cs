using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.DTO.Models;
using NorthWindApp.Models.Api;

namespace NorthWindApp.Controllers.Api
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        readonly IDictionaryService _dictionaryService;
        readonly ILogger _logger;
        readonly IMapper _mapper;

        public ProductController(IDictionaryService dictionaryService, ILogger<Controllers.ProductController> logger, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>),404)]
        public async Task<ActionResult<IEnumerable<ProductApi>>> Get()
        {
            var products = await _dictionaryService.GetProductsAsync();

            if (products == null)
                return NotFound();

            return  _mapper.Map<IEnumerable<ProductApi>>(products).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductApi),404)]
        public async Task<ActionResult<ProductApi>> Get(int id)
        {
            var product = await _dictionaryService.ProductFindByIdAsync(id);

            if(product == null)
                return BadRequest($"Can`t find product whit id = {id}");

            var productApi = _mapper.Map<ProductApi>(product);
            productApi.Href = $"api/product/{nameof(Get)}";
            return productApi;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductApi productApi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dictionaryService.ProductCreateAsync(_mapper.Map<Product>(productApi));
                return CreatedAtAction(nameof(Get), productApi);
            }
            catch(Exception ex)
            {
                var message = $"Create product error. {ex.Message}";
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        // PUT: api/product/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductApi productApi)
        {
            if (!ModelState.IsValid || id != productApi.Id)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dictionaryService.ProductUpdateAsync(_mapper.Map<Product>(productApi));
                return CreatedAtAction(nameof(Get), productApi);
            }
            catch (Exception ex)
            {
                var message = $"Update product error. {ex.Message}";
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        // DELETE: api/product/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await _dictionaryService.ProductFindByIdAsync(id);

                if (product == null)
                    return NotFound($"Delete product error. Can`t find product whit id = {id}");

                await _dictionaryService.ProductDeleteAsync(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
