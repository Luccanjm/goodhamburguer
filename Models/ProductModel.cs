using GoodHamburguer.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodHamburguer.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Price { get; set; }
        public ProductTypes Type { get; set; }
    }
}
