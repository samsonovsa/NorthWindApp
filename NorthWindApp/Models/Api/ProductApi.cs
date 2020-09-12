using System;
using System.ComponentModel.DataAnnotations;

namespace NorthWindApp.Models.Api
{
    public class ProductApi: Resource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        [Range(0, 999.99)]
        public decimal? UnitPrice { get; set; }

        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
