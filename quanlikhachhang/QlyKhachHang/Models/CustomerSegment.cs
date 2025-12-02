using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    /// <summary>
    /// Phân lo?i khách hàng - ?ánh d?u khách hàng thu?c nh?ng nhóm nào
    /// </summary>
    public class CustomerSegment
    {
        [Key]
        public int SegmentId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string SegmentName { get; set; } = string.Empty; // VIP, Regular, New, Inactive, High Value

        [StringLength(200)]
        public string SegmentDescription { get; set; } = string.Empty;

        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign key
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
