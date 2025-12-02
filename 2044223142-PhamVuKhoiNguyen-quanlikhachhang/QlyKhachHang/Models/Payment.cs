using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Cash"; // Cash, CreditCard, BankTransfer, MobilePayment

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string TransactionId { get; set; } = string.Empty; // Reference from payment gateway

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Reference bank info (for bank transfer)
        [StringLength(50)]
        public string BankName { get; set; } = string.Empty;

        [StringLength(20)]
        public string BankAccountNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string BankAccountName { get; set; } = string.Empty;

        // Metadata
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        // Foreign key
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
