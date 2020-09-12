using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NorthWindApp.WebApiClient.ConsoleClient.Models;

namespace NorthWindApp.WebApiClient.ConsoleClient
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        private static string baseAddress = "http://localhost:61188";

        static async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> products = null;
            var path = "api/product";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }
            return products;
        }

        static async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = null;
            var path = "api/category";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<IEnumerable<Category>>();
            }
            return categories;
        }

        static void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name}"+
                    $"\tPrice: {product.UnitPrice}"+
                    $"\tQuantity Per Unit: {product.QuantityPerUnit}"+
                    $"\tUnits In Stock: {product.UnitsInStock}");
            }
        }

        static void ShowCategories(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                Console.WriteLine($"Name: {category.Name}" +
                    $"\tDescription: {category.Description}");
            }
        }

        static async Task Main()
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var categories = await GetCategoriesAsync();
                ShowCategories(categories);

                var products = await GetProductsAsync();
                ShowProducts(products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
