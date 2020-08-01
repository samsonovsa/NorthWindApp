using Microsoft.AspNetCore.Mvc;
using NorthWindApp.Models.ViewModels;
using AutoMapper;
using NorthWindApp.Test.Mapping;
using System.Threading.Tasks;
using Xunit;
using Moq;
using NorthWindApp.BLL.Interfaces;
using System.Collections.Generic;
using NorthWindApp.DTO.Models;
using System;
using NorthWindApp.Controllers;
using Microsoft.Extensions.Logging;

namespace NorthWindApp.Test
{
    public class ProductControllerTest
    {
        private IMapper _mapper;

        public ProductControllerTest()
        {
            _mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile())).CreateMapper();
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.GetProductsAsync())
                .ReturnsAsync(GetProducts());

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductsViewModel>(
                viewResult.ViewData.Model);
        }
        [Fact]
        public async Task Create_HttpGet_ReturnsViewResult()
        {
            // Arrange
            var product = new ProductViewModel();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductCreateAsync(_mapper.Map<Product>(product)));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_HttpPost_ReturnsRedirectToActionResult()
        {
            // Arrange
            var expectedProduct = new ProductViewModel();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductCreateAsync(_mapper.Map<Product>(expectedProduct)));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Create(expectedProduct);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_HttpPost_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var expectedProduct = new ProductViewModel();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductCreateAsync(_mapper.Map<Product>(expectedProduct)));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_HttpGet_ReturnsViewResult()
        {
            // Arrange
            var product = new Product();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductFindByIdAsync(1))
                .ReturnsAsync(product);
            mockDictonaryService.Setup(service => service.ProductUpdateAsync(product));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Update(1);

            // Assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async Task Update_HttpGet_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductFindByIdAsync(2))
                .ReturnsAsync((Product)null);

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Update(2);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_HttpPost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange & Act
            var productViewModel = new ProductViewModel();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductUpdateAsync(_mapper.Map<Product>(productViewModel)));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Update(productViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Update_HttpPost_ReturnsRedirectToActionResult()
        {
            // Arrange & Act
            var productViewModel = new ProductViewModel();
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductUpdateAsync(_mapper.Map<Product>(productViewModel)));

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Update(productViewModel);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Theory]
        [InlineData(1, "Index")]
        [InlineData(2, "Error")]
        public async Task Delete_ReturnsRedirectToActionResult(int id, string actionName)
        {
            // Arrange & Act
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.ProductDeleteAsync(1));
            mockDictonaryService.Setup(service => service.ProductDeleteAsync(2)).Throws(new Exception());

            var mockLogService = new Mock<ILogger<ProductController>>();

            var controller = new ProductController(mockDictonaryService.Object, mockLogService.Object, _mapper);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionName, ((RedirectToActionResult)result).ActionName);         
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            { 
                new Product()
                {
                    Id = 1,
                    Name = "Product 1",
                    CategoryId = 1,
                    SupplierId = 1
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product 2",
                    CategoryId = 2,
                    SupplierId = 2
                }
            };
        }
    }
}