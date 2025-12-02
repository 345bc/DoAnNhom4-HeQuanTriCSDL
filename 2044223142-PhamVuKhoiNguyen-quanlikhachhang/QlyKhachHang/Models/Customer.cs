using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlyKhachHang.Models
{
    [Table("KhachHang")]
    public class Customer
    {
        [Key]
        [Column("MaKH")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TenKH")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Phone]
        [Column("SoDT")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Column("BirthDayKH")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        [Column("GioiTinhKH")]
        public string Gender { get; set; } = string.Empty; // Nam/Nữ

        // Authentication fields
        [Required]
        [StringLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Blocked

        [Column("NgayDangKy")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<CustomerNote> Notes { get; set; } = new List<CustomerNote>();
        public virtual ICollection<CustomerContact> Contacts { get; set; } = new List<CustomerContact>();
        public virtual ICollection<CustomerActivity> Activities { get; set; } = new List<CustomerActivity>();
        public virtual ICollection<CustomerSegment> Segments { get; set; } = new List<CustomerSegment>();
    }
}
