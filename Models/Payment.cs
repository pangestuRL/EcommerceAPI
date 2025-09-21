using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public System.DateTime PaymentDate { get; set; } = System.DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Cash"; // Credit Card, Bank Transfer, etc.

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed

        public Order? Order { get; set; }
    }
}
