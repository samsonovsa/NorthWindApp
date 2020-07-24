using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthWindApp.DTO.Models
{
    public class Category
    {
        [Column("CategoryID")]
        public int Id { get; set; }

        [Column("CategoryName")]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
