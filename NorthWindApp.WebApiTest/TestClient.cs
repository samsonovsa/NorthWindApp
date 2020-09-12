using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NorthWind;
using Xunit;

namespace NorthWindApp.WebApiTest
{
    public class TestClient
    {
        [Fact]
        public async Task NorthWindClient_Should_Return_Products_And_Categories()
        {
            using INorthWindClient client = new NorthWindClient(new HttpClient(), true);
            var products = await client.ProductApi.GetWithHttpMessagesAsync();
            var categories = await client.CategoryApi.GetWithHttpMessagesAsync();

            Assert.NotNull(products);
            Assert.NotNull(categories);
        }
    }
}
