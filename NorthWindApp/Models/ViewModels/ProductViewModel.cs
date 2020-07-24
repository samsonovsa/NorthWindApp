using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.ViewModels
{
    public class ProductViewModel: IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        [Range(0, 999.99)]
        public decimal? UnitPrice { get; set; }

        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public CategoryViewModel Category { get; set; }
        public SupplierViewModel Supplier { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UnitPrice > 500 && UnitsInStock > 100)
            {
                yield return new ValidationResult(
                    $"Products whith price {UnitPrice} should be less then 100 in stock.",
                    new[] { nameof(UnitsInStock) });
            }
        }
    }
}
