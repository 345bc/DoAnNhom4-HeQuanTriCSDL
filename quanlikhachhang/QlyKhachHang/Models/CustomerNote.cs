using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    /// <summary>
    /// Ghi chú liên h? khách hàng - L?u tr? các ghi chú v? t??ng tác v?i khách hàng
    /// </summary>
    public class CustomerNote
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(1000)]
        public string NoteContent { get; set; } = string.Empty;

        [StringLength(50)]
        public string NoteType { get; set; } = "General"; // General, Follow-up, Complaint, Suggestion, Other

        [StringLength(50)]
        public string Priority { get; set; } = "Normal"; // Low, Normal, High, Urgent

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = string.Empty;

        // Foreign key
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
