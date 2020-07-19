using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.Entities
{
    public class Product: IValidatableObject
    {
        [Column("ProductID")]
        public int  Id { get; set; }

        [Column("ProductName")]
        [Required]
        public string Name { get; set; }

        [Column("SupplierID")]
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Display(Name="Category")]
        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        [Range(0, 999.99)]
        public decimal? UnitPrice { get; set; }

        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
       
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UnitPrice>500 && UnitsInStock>100)
            {
                yield return new ValidationResult(
                    $"Products whith price {UnitPrice} should be less then 100 in stock.",
                    new[] { nameof(UnitsInStock) });
            }
        }
    }
}