using System.ComponentModel.DataAnnotations.Schema;

namespace GoodHamburguer.Models
{
    public class OrderModel
    {

        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        public Guid UserId { get; set; }
        public virtual UserModel? User { get; set; }
        public List<Guid?>? ProductIds { get; set; }
        public virtual List<ProductModel?>? Products { get; set; } 
    }
}
