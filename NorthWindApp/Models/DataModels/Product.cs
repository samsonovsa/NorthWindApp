using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class Product
    {
        [Column("ProductID")]
        public int  Id { get; set; }
        [Column("ProductName")]
        public string Name { get; set; }
        [Column("SupplierID")]
        public int? SupplierId { get; set; }
        [Column("CategoryID")]
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}