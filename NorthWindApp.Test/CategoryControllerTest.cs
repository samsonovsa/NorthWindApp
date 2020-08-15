using Xunit;
using Moq;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.Controllers;
using System.Collections.Generic;
using NorthWindApp.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.Models.ViewModels;
using AutoMapper;
using NorthWindApp.Test.Mapping;
using System.Threading.Tasks;
using System.Linq;

namespace NorthWindApp.Test
{
    public class CategoryControllerTest
    {
        private readonly IMapper _mapper;

        public CategoryControllerTest()
        {
            _mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile())).CreateMapper();
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCategeries()
        {
            // Arrange
            var mockDictonaryService = new Mock<IDictionaryService>();
            var mockCacheService = new Mock<IGenericCacheService<byte[]>>();
            mockDictonaryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(GetCategories());

            var controller = new CategoryController(mockDictonaryService.Object, mockCacheService.Object, _mapper);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Category 1"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Category 2"
                }
            };
        }
    }
}
