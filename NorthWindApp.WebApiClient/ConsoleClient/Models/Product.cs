using System;

namespace NorthWindApp.WebApiClient.ConsoleClient.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public Int16? UnitsInStock { get; set; }

        public Int16? UnitsOnOrder { get; set; }

        public Int16? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
