using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [ApiController]
    [Route("api/product")]
    public class ProductApiController : ControllerBase
    {
        IDictionaryService _dictionaryService;
        ILogger _logger;
        IMapper _mapper;

        public ProductApiController(IDictionaryService dictionaryService, ILogger<ProductController> logger, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductApiViewModel>>> Get()
        {
            var products = await _dictionaryService.GetProductsAsync();

            return  _mapper.Map<IEnumerable<ProductApiViewModel>>(products).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductApiViewModel>> Get(int id)
        {
            var products = await _dictionaryService.GetProductsAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                var productViewModel = _mapper.Map<ProductApiViewModel>(product);
                return productViewModel;
            }
            else
            {
                return BadRequest($"Can`t find product whit id = {id}");
            }

        }

    }
}
