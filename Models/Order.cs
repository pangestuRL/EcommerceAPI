using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public System.DateTime OrderDate { get; set; } = System.DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Paid, Shipped, Completed, Cancelled

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public User? User { get; set; }
        public System.Collections.Generic.ICollection<OrderItem>? OrderItems { get; set; }
        public Payment? Payment { get; set; }
    }
}
