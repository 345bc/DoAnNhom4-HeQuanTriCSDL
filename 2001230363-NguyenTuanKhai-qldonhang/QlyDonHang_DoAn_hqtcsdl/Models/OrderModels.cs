using System;
using System.Collections.Generic;

namespace QlyDonHang_DoAn_hqtcsdl.Models
{
    // Model cho danh sách đơn hàng
    public class OrderViewModel
    {
        public int Order_Id { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }

    // Model chi tiết đơn hàng
    public class OrderDetailViewModel
    {
        public int Order_Id { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductImage { get; set; }
    }
}