using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    /// <summary>
    /// M?i liên h? khách hàng - L?u thông tin liên h? thêm (?i?n tho?i, email ph?, etc)
    /// </summary>
    public class CustomerContact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string ContactType { get; set; } = "Primary"; // Primary, Secondary, Emergency

        [StringLength(100)]
        public string ContactName { get; set; } = string.Empty; // Tên ng??i liên h?

        [StringLength(50)]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(100)]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [StringLength(100)]
        public string Relationship { get; set; } = string.Empty; // M?i quan h? (Cha, M?, Anh, Ch?, v.v)

        public bool IsPreferred { get; set; } = false; // Liên h? ???c ?u tiên?

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        // Foreign key
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
