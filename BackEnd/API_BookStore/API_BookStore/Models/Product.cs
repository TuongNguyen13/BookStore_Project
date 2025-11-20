using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Microsoft.EntityFrameworkCore.Index("ProductCode", Name = "UQ__Products__2F4E024F736CAE97", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    [StringLength(255)]
    public string ProductName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int? ProductYear { get; set; }

    public int? StockQuantity { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ProductImageUrl { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [InverseProperty("Product")]
    public virtual Book? Book { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
