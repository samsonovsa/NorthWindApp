using Xunit;
using Moq;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.Controllers;
using System.Collections.Generic;
using NorthWindApp.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using NorthWindApp.Models.ViewModels;

namespace NorthWindApp.Test
{
    public class CategoryControllerTest
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithA()
        {
            // Arrange
            var mockDictonaryService = new Mock<IDictionaryService>();
            mockDictonaryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(GetCategories());
            var controller = new CategoryController(mockDictonaryService.Object);

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
