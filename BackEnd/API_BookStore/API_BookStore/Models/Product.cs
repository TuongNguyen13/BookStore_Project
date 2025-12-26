using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

public partial class Product
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    [StringLength(255)]
    public string ProductName { get; set; } = null!;

    [StringLength(255)]
    public string ProductType { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int? ProductYear { get; set; }

    public int? StockQuantity { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ProductImageUrl { get; set; }

    [InverseProperty("ProductCodeNavigation")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("ProductCodeNavigation")]
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
