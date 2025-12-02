using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    /// <summary>
    /// Ho?t ??ng khách hàng - Theo dõi các ho?t ??ng c?a khách hàng
    /// </summary>
    public class CustomerActivity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(100)]
        public string ActivityType { get; set; } = string.Empty; // Login, Purchase, Review, Contact, etc

        [StringLength(500)]
        public string ActivityDescription { get; set; } = string.Empty;

        [StringLength(50)]
        public string ActivityStatus { get; set; } = "Completed"; // Pending, In Progress, Completed, Failed

        public DateTime ActivityDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Details { get; set; } = string.Empty;

        public int? RelatedInvoiceId { get; set; }
        public int? RelatedProductId { get; set; }
        public int? RelatedReviewId { get; set; }

        // Foreign key
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("RelatedInvoiceId")]
        public virtual Invoice? RelatedInvoice { get; set; }

        [ForeignKey("RelatedProductId")]
        public virtual Product? RelatedProduct { get; set; }

        [ForeignKey("RelatedReviewId")]
        public virtual Review? RelatedReview { get; set; }
    }
}
